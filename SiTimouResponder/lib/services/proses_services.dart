import 'dart:convert';
import 'dart:io';

import 'package:http/http.dart' as http;
import 'package:flutter/material.dart';

import 'package:sitimoufr/helper/globals.dart' as g;
import 'package:sitimoufr/models/daftar_laporan.dart';
import 'package:sitimoufr/models/daftar_riwayat_proses.dart';
import 'package:sitimoufr/models/detail_laporan.dart';
import 'package:sitimoufr/models/detail_panik.dart';

const String mainUrl = g.apiUrl;

Map<String, String> httpHeaders = {
  "Content-type": "application/json",
  "accept": "application/json",
  'Authorization': "Bearer ${g.authToken}",
  "Cache-Control": "no-cache",
};

class ProsesServices {
  static var client = http.Client();

  //
  // Daftar Laporan Masuk
  //

  static Future<List<DaftarLaporan?>> getListLaporan() async {
    final url = Uri.parse('$mainUrl/proses/daftar_laporan_masuk/${g.userId}');

    try {
      var result = await client.get(url, headers: httpHeaders);

      debugPrint("[!] DebugInfo: [RESULT] ProsesServices.getListLaporan() => ${result.body}");

      if (result.statusCode == 200) {
        var body = result.body;
        return daftarLaporanFromJson(body);
      } else {
        return [];
      }
    } on Exception catch (e) {
      debugPrint("[!] DebugInfo: [ERROR] ProsesServices.getListLaporan() => ${e.toString()}");
      return [];
    }
  }

  //
  // Detail Laporan / Panik
  //
  static Future<DetailLaporan?> detailLaporan(int idLaporan) async {
    final url = Uri.parse('$mainUrl/proses/detail_laporan');

    // "Payload"
    var map = {
      'JenisLaporan': "1",
      'IdLaporan': idLaporan,
    };
    var payload = json.encode(map);

    try {
      var result = await client.post(url, body: payload, headers: g.httpHeaders);

      debugPrint("[!] DebugInfo: [RESULT] ProsesServices.detailLaporan() => ${result.body}");

      if (result.statusCode == 200) {
        //var body = result.body;
        return detailLaporanFromJson(result.body);
      }
      return null;
    } on Exception catch (e) {
      debugPrint("[!] DebugInfo: [ERROR] ProsesServices.detailLaporan() => ${e.toString()}");
      return null;
    }
  }

  static Future<DetailPanik?> detailPanik(int idLaporan) async {
    final url = Uri.parse('$mainUrl/proses/detail_laporan');

    // "Payload"
    var map = {
      'JenisLaporan': "2",
      'IdLaporan': idLaporan,
    };
    var payload = json.encode(map);

    try {
      var result = await client.post(url, body: payload, headers: g.httpHeaders);

      debugPrint("[!] DebugInfo: [RESULT] ProsesServices.detailPanik() => ${result.body}");

      if (result.statusCode == 200) {
        //var body = result.body;
        return detailPanikFromJson(result.body);
      }
      return null;
    } on Exception catch (e) {
      debugPrint("[!] DebugInfo: [ERROR] ProsesServices.detailLaporan() => ${e.toString()}");
      return null;
    }
  }

  //
  // Terima
  //
  static Future<bool> terimaLaporan(int idDisposisi) async {
    final url = Uri.parse('$mainUrl/proses/terima_laporan/$idDisposisi');

    try {
      var result = await client.get(url, headers: httpHeaders);

      debugPrint("[!] DebugInfo: [RESULT] ProsesServices.terimaLaporan() => ${result.body}");

      return result.statusCode == 200;
    } on Exception catch (e) {
      debugPrint("[!] DebugInfo: [ERROR] ProsesServices.terimaLaporan() => ${e.toString()}");
      return false;
    }
  }

  //
  // BATAL
  //
  static Future<bool> tolakLaporan(int idLaporan) async {
    final url = Uri.parse('$mainUrl/proses/batal_laporan');

    // "Payload"
    var map = {
      'IdLaporan': idLaporan,
      'IdUser': g.userId,
    };
    var payload = json.encode(map);

    try {
      var result = await client.post(url, body: payload, headers: g.httpHeaders);

      debugPrint("[!] DebugInfo: [RESULT] ProsesServices.batalLaporan() => ${result.body}");

      return result.statusCode == 200;
    } on Exception catch (e) {
      debugPrint("[!] DebugInfo: [ERROR] ProsesServices.batalLaporan() => ${e.toString()}");
      return false;
    }
  }

  //
  // SIMPAN / PROSES
  //
  static Future<bool> prosesLapaporan(
      String jenisLaporan, String judul, String uraian, String status, File lampiran, int idDisposisi, int idLaporan) async {
    final url = Uri.parse('$mainUrl/proses/laporan');

    try {
      var request = http.MultipartRequest('POST', url)
        ..headers.addAll(httpHeaders)
        ..fields['IdDisposisi'] = idDisposisi.toString()
        ..fields['IdUser'] = g.userId.toString()
        ..fields['IdLaporan'] = idLaporan.toString()
        ..fields['JenisLaporan'] = jenisLaporan
        ..fields['Judul'] = judul
        ..fields['Uraian'] = uraian
        ..fields['Status'] = status
        ..files.add(await http.MultipartFile.fromPath('FileFoto', lampiran.path));

      var result = await request.send();
      // Cek Response
      var responseData = await result.stream.toBytes();
      debugPrint("[!] DebugInfo: [ResultStatusCode] ProsesServices.prosesLapaporan() => ${String.fromCharCodes(responseData)}");

      return result.statusCode == 200;
    } on Exception catch (e) {
      debugPrint("[!] DebugInfo: [ERROR] ProsesServices.prosesLapaporan() => ${e.toString()}");
      return false;
    }
  }

  //
  // Riwayat Laporan
  //
  static Future<List<DaftarRiwayatProses?>> getListRiwayatProses(String jenisProses) async {
    final url = Uri.parse('$mainUrl/proses/riwayat_laporan');

    // "Payload"
    var map = {
      'JenisData': jenisProses,
      'IdUser': g.userId,
    };
    var payload = json.encode(map);

    try {
      var result = await client.post(url, body: payload, headers: g.httpHeaders);

      debugPrint("[!] DebugInfo: [RESULT] ProsesServices.getListRiwayatProses() => ${result.body}");

      if (result.statusCode == 200) {
        var body = result.body;
        return daftarRiwayatProsesFromJson(body);
      } else {
        return [];
      }
    } on Exception catch (e) {
      debugPrint("[!] DebugInfo: [ERROR] ProsesServices.getListRiwayatProses() => ${e.toString()}");
      return [];
    }
  }
}
