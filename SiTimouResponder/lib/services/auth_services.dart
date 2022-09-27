import 'dart:convert';
import 'package:flutter/material.dart';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import 'package:http/http.dart' as http;

import 'package:sitimoufr/helper/globals.dart' as g;
import 'package:sitimoufr/helper/ui_toast.dart';

const String mainUrl = g.apiUrl;
const storage = FlutterSecureStorage();

Map<String, String> httpHeaders = {
  "Content-type": "application/json",
  "accept": "application/json",
  'Authorization': "Bearer ${g.authToken}",
  "Cache-Control": "no-cache",
};

class AuthServices {
  static var client = http.Client();

  //
  // === LOGIN ===
  //

  static Future<bool> masuk(String userLogin, String userPwd) async {
    final url = Uri.parse('$mainUrl/auth/masuk_pegawai/');

    // "Payload"
    var map = {
      'UserLogin': userLogin,
      'UserPwd': userPwd,
    };
    var payload = json.encode(map);

    try {
      var result = await client.post(url, body: payload).timeout(const Duration(seconds: 20));

      debugPrint("[!] DebugInfo: [ReadToken] AuthServices.masuk() => ${result.body}");

      switch (result.statusCode) {
        case 200:
          // Login OK, simpan JWT TOken
          var apiToken = json.decode(result.body);
          storage.write(key: "authToken", value: apiToken['authToken']);
          g.authToken = apiToken['authToken'];

          return true;

        case 404:
          toastPesan("LOGIN", "NIK anda belum terdaftar.");
          return false;

        case 401:
          var x = result.body.toString();
          if (x == "USER_NOT_VERIFIED") {
            toastPesan("LOGIN", "Akun anda belum di verifikasi.");
          } else if (x == "USER_ACCESS_DENIED") {
            toastPesan("LOGIN", "Anda tidak diizinkan untuk menggunakan aplikasi ini.");
          } else if (x == "INVALID_PASSWORD") {
            toastPesan("LOGIN", "Password anda masih salah.");
          } else {
            toastPesan("LOGIN", "Anda belum bisa login. Hubungi admin untuk info selanjutnya.");
          }
          return false;

        default:
          toastPesan("LOGIN", "Login bermasalah. Cek koneksi interent atau hubungi admin untuk info selanjutnya.");
          return false;
      }
    } on Exception catch (e) {
      debugPrint("[!] DebugInfo: [ERROR] AuthServices.masuk() => ${e.toString()}");
      toastPesan("LOGIN", "Login bermasalah. Cek koneksi interent atau hubungi admin untuk info selanjutnya.");
      return false;
    }
  }

  static Future<bool> keluar() async {
    try {
      await storage.delete(key: "authToken");
      return true;
    } on Exception catch (e) {
      debugPrint("[!] DebugInfo: [ERROR] AuthServices.getDaftarOpd() => ${e.toString()}");
      return false;
    }
  }

  //
  // === Validasi ===
  //

  static Future<bool> validasiAuthToken() async {
    try {
      var token = await storage.read(key: "authToken");

      debugPrint("[!] DebugInfo: [ReadToken] AuthServices.validateToken() => ${token.toString()}");

      // Return false jika token tidak ada
      if (token == null) {
        return false;
      }

      // Proses token
      var split = token.split(".");
      var parse = parseJWT(token);

      if (split.length != 3) {
        return false;
      } else {
        if (DateTime.fromMillisecondsSinceEpoch(parse!["exp"] * 1000).isAfter(DateTime.now())) {
          g.authToken = token;
          g.userId = int.parse(parse["nameid"]);
          storage.write(key: "userid", value: parse["nameid"]);

          return true;
        } else {
          return false;
        }
      }
    } catch (e) {
      debugPrint("[!] DebugInfo: [ERROR] AuthServices.validateToken() => ${e.toString()}");
      return false;
    }
  }

  static Map<String, dynamic>? parseJWT(String jwtString) {
    final parts = jwtString.split('.');

    if (parts.length != 3) return null;

    final payload = parts[1];
    var normalized = base64Url.normalize(payload);
    var resp = utf8.decode(base64.decode(normalized));
    final payloadMap = json.decode(resp);

    if (payloadMap is! Map<String, dynamic>) {
      return null;
    } else {
      return payloadMap;
    }
  }

  static Future<bool> validasiPengguna() async {
    final url = Uri.parse('$mainUrl/auth/status_pegawai/${g.userId}');

    try {
      var result = await client.get(url, headers: httpHeaders);

      switch (result.statusCode) {
        case 200:
          var userInfo = json.decode(result.body);

          // Cek Status User
          if (userInfo["flg"] != 'N') {
            toastPesan("LOGIN", "Anda tidak diizinkan untuk menggunakan aplikasi ini. Hubungi admin untuk info selanjutnya.");
            return false;
          }

          return true;

        default:
          toastPesan("LOGIN", "Gagal verifikasi Akun anda. Hubungi admin untuk info selanjutnya.");
          return false;
      }
    } on Exception catch (e) {
      debugPrint("[!] DebugInfo: [ERROR] AuthServices.validasiPegawai() => ${e.toString()}");
      toastPesan("LOGIN", "Gagal verifikasi Akun anda. Hubungi admin untuk info selanjutnya.");
      return false;
    }
  }

  // FCM TOKEN

  static Future<bool> updateFcmToken(String? value) async {
    final url = Uri.parse('$mainUrl/auth/update_fcm_token_pegawai');

    // "Payload"
    var map = {
      'IdUser': g.userId,
      'FcmToken': value,
    };
    var payload = json.encode(map);

    try {
      var result = await client.post(url, body: payload, headers: g.httpHeaders);

      // debugPrint("[!] DebugInfo: [RESULT] UserServices.updateFcmToekn() => " + g.userId.toString());
      // debugPrint("[!] DebugInfo: [RESULT] UserServices.updateFcmToekn() => " + value.toString());
      // debugPrint("[!] DebugInfo: [RESULT] UserServices.updateFcmToekn() => " + result.statusCode.toString());

      return result.statusCode == 200;
    } on Exception catch (e) {
      debugPrint("[!] DebugInfo: [ERROR] UserServices.updateFcmToekn() => ${e.toString()}");
      return false;
    }
  }
}
