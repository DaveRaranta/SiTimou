import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;

import 'package:sitimoufr/helper/globals.dart' as g;
import 'package:sitimoufr/models/alamat_osm.dart';
import 'package:sitimoufr/models/daftar_lokasi.dart';
import 'package:sitimoufr/models/detail_lokasi.dart';

const String mainUrl = g.apiUrl;

Map<String, String> httpHeaders = {
  "Content-type": "application/json",
  "accept": "application/json",
  'Authorization': "Bearer ${g.authToken}",
  "Cache-Control": "no-cache",
};

class LokasiServices {
  static var client = http.Client();

  //
  // Daftar Lokasi
  //

  static Future<List<DaftarLokasi?>> getListLokasi() async {
    final url = Uri.parse('$mainUrl/lokasi/daftar_lokasi');

    try {
      var result = await client.get(url, headers: httpHeaders);

      debugPrint("[!] DebugInfo: [RESULT] LokasiServices.getListLokasi() => ${result.body}");

      if (result.statusCode == 200) {
        var body = result.body;
        return daftarLokasiFromJson(body);
      } else {
        return [];
      }
    } on Exception catch (e) {
      debugPrint("[!] DebugInfo: [ERROR] LokasiServices.getListLokasi() => ${e.toString()}");
      return [];
    }
  }

  //
  // Detail Lokasi
  //
  static Future<DetailLokasi?> getDetailLokasi(int lokasiId) async {
    final url = Uri.parse('$mainUrl/lokasi/detail_lokasi/$lokasiId');

    try {
      var result = await client.get(url, headers: httpHeaders);

      debugPrint("[!] DebugInfo: [RESULT] LokasiServices.getDetailLokasi() => ${result.body}");

      if (result.statusCode == 200) {
        var body = result.body;
        return detailLokasiFromJson(body);
      } else {
        return null;
      }
    } on Exception catch (e) {
      debugPrint("[!] DebugInfo: [ERROR] LokasiServices.getDetailLokasi() => ${e.toString()}");
      return null;
    }
  }

  //
  // OpenStreetMap
  //
  static Future<AlamatOpenSreetMap?> getAlamatOsm(double gpsLat, double gpsLon) async {
    final url = Uri.parse('https://nominatim.openstreetmap.org/reverse?format=json&lat=$gpsLat&lon=$gpsLon');

    try {
      var result = await client.get(url, headers: httpHeaders);

      debugPrint("[!] DebugInfo: [RESULT] LokasiServices.getAlamatOsm() => ${result.body}");

      if (result.statusCode == 200) {
        var body = result.body;
        return alamatOpenSreetMapFromJson(body);
      } else {
        return null;
      }
    } on Exception catch (e) {
      debugPrint("[!] DebugInfo: [ERROR] LokasiServices.getAlamatOsm() => ${e.toString()}");
      return null;
    }
  }
}
