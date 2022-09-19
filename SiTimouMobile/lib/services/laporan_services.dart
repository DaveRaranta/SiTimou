import 'dart:convert';
import 'dart:io';

import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;

import 'package:sitimou/helper/globals.dart' as g;
import 'package:sitimou/models/daftar_laporan.dart';
import 'package:sitimou/models/detail_laporan.dart';

const String mainUrl = g.apiUrl;

Map<String, String> httpHeaders = {
  "Content-type": "application/json",
  "accept": "application/json",
  'Authorization': "Bearer ${g.authToken}",
  "Cache-Control": "no-cache",
};

class LaporanServices {
  static var client = http.Client();

  //
  // Daftar Laporan
  //

  static Future<List<DaftarLaporan?>> getListLaporanId() async {
    final url = Uri.parse('$mainUrl/lapor/daftar_laporan_id/${g.userId}');

    try {
      var result = await client.get(url, headers: httpHeaders);

      debugPrint("[!] DebugInfo: [RESULT] LaporanServices.getListLaporanId() => ${result.body}");

      if (result.statusCode == 200) {
        var body = result.body;
        return daftarLaporanFromJson(body);
      } else {
        return [];
      }
    } on Exception catch (e) {
      debugPrint("[!] DebugInfo: [ERROR] LaporanServices.getListLaporanId() => ${e.toString()}");
      return [];
    }
  }

  static Future<List<DaftarLaporan?>> getListLaporanAll() async {
    final url = Uri.parse('$mainUrl/lapor/daftar_laporan_all');

    try {
      var result = await client.get(url, headers: httpHeaders);

      debugPrint("[!] DebugInfo: [RESULT] LaporanServices.getListLaporanAll() => ${result.body}");

      if (result.statusCode == 200) {
        var body = result.body;
        return daftarLaporanFromJson(body);
      } else {
        return [];
      }
    } on Exception catch (e) {
      debugPrint("[!] DebugInfo: [ERROR] LaporanServices.getListLaporanAll() => ${e.toString()}");
      return [];
    }
  }

  //
  // Detail
  //
  static Future<DetailLaporan?> getDetailLaporan(int idLaporan) async {
    final url = Uri.parse('$mainUrl/lapor/detail_laporan/$idLaporan');

    try {
      var result = await client.get(url, headers: httpHeaders);

      debugPrint("[!] DebugInfo: [RESULT] LaporanServices.getDetailLaporan() => ${result.body}");

      if (result.statusCode == 200) {
        var body = result.body;
        return detailLaporanFromJson(body);
      } else {
        return null;
      }
    } on Exception catch (e) {
      debugPrint("[!] DebugInfo: [ERROR] LaporanServices.getDetailLaporan() => ${e.toString()}");
      return null;
    }
  }

  static Future<bool> hapusLaporan(int idLaporan) async {
    final url = Uri.parse('$mainUrl/lapor/hapus_laporan/$idLaporan');

    try {
      var result = await client.get(url, headers: httpHeaders);

      debugPrint("[!] DebugInfo: [RESULT] LaporanServices.hapusLaporan() => ${result.body}");

      return result.statusCode == 200;
    } on Exception catch (e) {
      debugPrint("[!] DebugInfo: [ERROR] LaporanServices.hapusLaporan() => ${e.toString()}");
      return false;
    }
  }

  //
  // Buat Laporan
  //

  static Future<bool> kirimLaporan(String perihal, String isiLaporan, double gpsLat, double gpsLng, File? lampiran) async {
    final url = Uri.parse('$mainUrl/lapor/simpan_laporan');

    try {
      var request = http.MultipartRequest('POST', url)
        ..headers.addAll(httpHeaders)
        ..fields['IdUser'] = g.userId.toString()
        ..fields['Perihal'] = perihal
        ..fields['IsiLaporan'] = isiLaporan
        ..fields['GpsLat'] = gpsLat.toString()
        ..fields['GpsLng'] = gpsLng.toString()
        ..files.add(await http.MultipartFile.fromPath('FileFoto', lampiran!.path));

      var result = await request.send();

      // Cek Response
      var responseData = await result.stream.toBytes();
      debugPrint("[!] DebugInfo: [ResultStatusCode] LaporanServices.kirimLaporan() => ${String.fromCharCodes(responseData)}");

      return result.statusCode == 200;
    } on Exception catch (e) {
      debugPrint("[!] DebugInfo: [ERROR] LaporanServices.kirimLaporan() => ${e.toString()}");
      return false;
    }
  }

  //
  // PANIK
  //

  static Future<bool> kirimPanik(double gpsLat, double gpsLng) async {
    final url = Uri.parse('$mainUrl/lapor/panik');

    var map = {
      'IdUser': g.userId,
      'GpsLat': gpsLat,
      'GpsLng': gpsLng,
    };
    var payload = json.encode(map);

    try {
      var result = await client.post(url, body: payload, headers: g.httpHeaders).timeout(const Duration(seconds: 30));

      return result.statusCode == 200;
    } on Exception catch (e) {
      debugPrint("[!] DebugInfo: [ERROR] LaporanServices.kirimPanik() => ${e.toString()}");
      return false;
    }
  }
}
