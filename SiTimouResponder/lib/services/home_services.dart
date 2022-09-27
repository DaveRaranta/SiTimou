import 'dart:io';
import 'dart:convert';
import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;

import 'package:sitimoufr/helper/globals.dart' as g;
import 'package:sitimoufr/models/detail_pegawai.dart';

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
  static Future<DetailPegawai?> getDetailPegawai() async {
    final url = Uri.parse('$mainUrl/home/info_pegawai/${g.userId}');

    try {
      var result = await client.get(url, headers: httpHeaders);

      debugPrint("[!] DebugInfo: [RESULT] HomeServices.getDetailPegawai() => ${result.body}");

      if (result.statusCode == 200) {
        var body = result.body;
        return detailPegawaiFromJson(body);
      } else {
        return null;
      }
    } on Exception catch (e) {
      debugPrint("[!] DebugInfo: [ERROR] HomeServices.getDetailPegawai() => ${e.toString()}");
      return null;
    }
  }

  //
  // Pengumuman
  //
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
}
