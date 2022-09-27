import 'dart:io';
import 'dart:convert';
import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;

import 'package:sitimou/helper/globals.dart' as g;
import 'package:sitimou/helper/ui_toast.dart';
import 'package:sitimou/models/daftar_desa.dart';
import 'package:sitimou/models/daftar_kecamatan.dart';
import 'package:sitimou/models/detail_pengguna.dart';

const String mainUrl = g.apiUrl;

Map<String, String> httpHeaders = {
  "Content-type": "application/json",
  "accept": "application/json",
  'Authorization': "Bearer ${g.authToken}",
  "Cache-Control": "no-cache",
};

class HomeServices {
  static var client = http.Client();

  //
  // INFO USER
  //
  static Future<DetailPengguna?> getDetailPengguna() async {
    final url = Uri.parse('$mainUrl/home/info_pengguna/${g.userId}');

    try {
      var result = await client.get(url, headers: httpHeaders);

      debugPrint("[!] DebugInfo: [RESULT] HomeServices.getDetailPengguna() => ${result.body}");

      if (result.statusCode == 200) {
        var body = result.body;
        return detailPenggunaFromJson(body);
      } else {
        return null;
      }
    } on Exception catch (e) {
      debugPrint("[!] DebugInfo: [ERROR] HomeServices.getDetailPengguna() => ${e.toString()}");
      return null;
    }
  }

  static Future<String> getInfoPengumuman() async {
    final url = Uri.parse('$mainUrl/home/info_pengumuman');

    try {
      var result = await client.get(url, headers: httpHeaders);

      debugPrint("[!] DebugInfo: [RESULT] HomeServices.getDetailPengguna() => ${result.body}");

      if (result.statusCode == 200) {
        var body = result.body.toString().replaceAll('"', "");
        return body;
      } else {
        return "-";
      }
    } on Exception catch (e) {
      debugPrint("[!] DebugInfo: [ERROR] HomeServices.getDetailPengguna() => ${e.toString()}");
      return "-";
    }
  }

  //
  // Update Profil
  //

  static Future<List<DaftarKecamatan?>> getListKecamatan() async {
    final url = Uri.parse('$mainUrl/home/daftar_kecamatan');

    try {
      var result = await client.get(url, headers: httpHeaders);

      debugPrint("[!] DebugInfo: [RESULT] HomeServices.getListKecamatan() => ${result.body}");

      if (result.statusCode == 200) {
        var body = result.body;
        return daftarKecamatanFromJson(body);
      } else {
        return [];
      }
    } on Exception catch (e) {
      debugPrint("[!] DebugInfo: [ERROR] HomeServices.getListKecamatan() => ${e.toString()}");
      return [];
    }
  }

  static Future<List<DaftarDesa?>> getListDesa(int idKecamatan) async {
    final url = Uri.parse('$mainUrl/home/daftar_desa/$idKecamatan');

    try {
      var result = await client.get(url, headers: httpHeaders);

      debugPrint("[!] DebugInfo: [RESULT] HomeServices.getListDesa() => ${result.body}");

      if (result.statusCode == 200) {
        var body = result.body;
        return daftarDesaFromJson(body);
      } else {
        return [];
      }
    } on Exception catch (e) {
      debugPrint("[!] DebugInfo: [ERROR] HomeServices.getListDesa() => ${e.toString()}");
      return [];
    }
  }

  static Future<bool> gantiFotoProfil(File? foto) async {
    final url = Uri.parse('$mainUrl/home/update_foto_pengguna');

    try {
      var request = http.MultipartRequest('POST', url)
        ..headers.addAll(httpHeaders)
        ..fields['IdUser'] = g.userId.toString()
        ..files.add(await http.MultipartFile.fromPath('FileFoto', foto!.path));

      var result = await request.send();

      // Cek Response
      var responseData = await result.stream.toBytes();
      debugPrint("[!] DebugInfo: [ResultStatusCode] HomeServices.gantiFotoProfil() => ${String.fromCharCodes(responseData)}");

      return result.statusCode == 200;
    } on Exception catch (e) {
      debugPrint("[!] DebugInfo: [ERROR] HomeServices.gantiFotoProfil() => ${e.toString()}");
      return false;
    }
  }

  static Future<bool> updateProfil(
      String nik, String namaLengkap, String tanggalLahir, String jenisKelamin, String alamat, int idDesa, idKecamatan, String noTelp) async {
    final url = Uri.parse('$mainUrl/home/update_profil');

    var map = {
      'IdUser': g.userId,
      'Nik': nik,
      'NamaLengkap': namaLengkap,
      'TanggalLahir': tanggalLahir,
      'JenisKelamin': jenisKelamin,
      'Alamat': alamat,
      'IdDesa': idDesa,
      'IdKecamatan': idKecamatan,
      'NoTelp': noTelp,
    };
    var payload = json.encode(map);

    try {
      var result = await client.post(url, body: payload, headers: g.httpHeaders).timeout(const Duration(seconds: 30));
      debugPrint("[!] DebugInfo: [RESULT] HomeServices.getListKecamatan() => ${result.body}");

      return result.statusCode == 200;
    } on Exception catch (e) {
      debugPrint("[!] DebugInfo: [ERROR] HomeServices.updateProfil() => ${e.toString()}");
      return false;
    }
  }
}
