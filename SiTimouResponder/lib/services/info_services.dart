import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;

import 'package:sitimoufr/helper/globals.dart' as g;
import 'package:sitimoufr/models/daftar_aturan.dart';
import 'package:sitimoufr/models/detail_aturan.dart';

const String mainUrl = g.apiUrl;

Map<String, String> httpHeaders = {
  "Content-type": "application/json",
  "accept": "application/json",
  'Authorization': "Bearer ${g.authToken}",
  "Cache-Control": "no-cache",
};

class InfoServices {
  static var client = http.Client();

  //
  // Daftar Info
  //

  static Future<List<DaftarAturan?>> getListAturan() async {
    final url = Uri.parse('$mainUrl/info/daftar_aturan');

    try {
      var result = await client.get(url, headers: httpHeaders);

      debugPrint("[!] DebugInfo: [RESULT] InfoServices.getListAturan() => ${result.body}");

      if (result.statusCode == 200) {
        var body = result.body;
        return daftarAturanFromJson(body);
      } else {
        return [];
      }
    } on Exception catch (e) {
      debugPrint("[!] DebugInfo: [ERROR] InfoServices.getListAturan() => ${e.toString()}");
      return [];
    }
  }

  //
  // Detail Aturan
  //
  static Future<DetailAturan?> getDetailLokasi(int aturanId) async {
    final url = Uri.parse('$mainUrl/info/detail_aturan/$aturanId');

    try {
      var result = await client.get(url, headers: httpHeaders);

      debugPrint("[!] DebugInfo: [RESULT] InfoServices.getDetailLokasi() => ${result.body}");

      if (result.statusCode == 200) {
        var body = result.body;
        return detailAturanFromJson(body);
      } else {
        return null;
      }
    } on Exception catch (e) {
      debugPrint("[!] DebugInfo: [ERROR] InfoServices.getDetailLokasi() => ${e.toString()}");
      return null;
    }
  }
}
