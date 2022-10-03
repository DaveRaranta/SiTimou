import 'dart:convert';

import 'package:cached_network_image/cached_network_image.dart';
import 'package:connectivity_plus/connectivity_plus.dart';
import 'package:flutter/foundation.dart';
import 'package:flutter/services.dart';
import 'package:internet_connection_checker/internet_connection_checker.dart';
import 'package:intl/date_symbol_data_local.dart';
import 'package:intl/intl.dart';
import 'package:http/http.dart' as http;
import 'package:sitimoufr/helper/globals.dart' as g;

const String mainUrl = g.apiUrl;

Map<String, String> httpHeaders = {
  "Content-type": "application/json",
  "accept": "application/json",
  'Authorization': "Bearer " + g.authToken,
  "Cache-Control": "no-cache",
};

// import 'package:html/parser.dart';

class AppHelper {
  // Cek Koneksi
  Future<bool> connectionChecker() async {
    var connectResult = await Connectivity().checkConnectivity();
    if (connectResult == ConnectivityResult.mobile || connectResult == ConnectivityResult.wifi) {
      // Cek aktual Koneksi Internet
      return await InternetConnectionChecker().hasConnection;
    } else {
      return false;
    }
  }

  // Remove HTML tags
  /*
  static String removeTag({htmlString}) {
    var document = parse(htmlString);
    String parsedString = parse(document.body!.text).documentElement!.text;
    //callback(parsedString);
    return parsedString;
  }
  */

  static Future deleteImageFromCache(String url) async {
    await CachedNetworkImage.evictFromCache(url);
  }

  static String formatTanggal(String tanggal) {
    if (tanggal == "") return 'Pilih tanggal.';

    initializeDateFormatting();
    var f = DateFormat.yMMMMEEEEd('id_ID');
    return f.format(DateTime.parse(tanggal.toString()));
  }

  static Future<String> base64encodedImageFromUrl(String url) async {
    final http.Response response = await http.get(Uri.parse(url), headers: httpHeaders);
    final String base64Data = base64Encode(response.bodyBytes);
    return base64Data;
  }

  static Future<String> base64encodedImageFromAsset(String assetName) async {
    ByteData bytes = await rootBundle.load(assetName);
    var buffer = bytes.buffer;
    var base64Data = base64Encode(Uint8List.view(buffer));
    return base64Data;
  }
}


// # Konesi stuff

