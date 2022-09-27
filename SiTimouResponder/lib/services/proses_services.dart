import 'package:http/http.dart' as http;
import 'package:flutter/material.dart';

import 'package:sitimoufr/helper/globals.dart' as g;

const String mainUrl = g.apiUrl;

Map<String, String> httpHeaders = {
  "Content-type": "application/json",
  "accept": "application/json",
  'Authorization': "Bearer ${g.authToken}",
  "Cache-Control": "no-cache",
};

class ProsesServices {
  static var client = http.Client();
}
