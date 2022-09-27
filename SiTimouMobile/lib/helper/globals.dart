library app.globals;

// const apiUrl = "http://10.0.2.2:6002/api";
const apiUrl = "http://36.67.30.6:86/api";

// App
String authToken = "";

// Pengguna
int? userId;
String? userFcmToken;

Map<String, String> httpHeaders = {
  "Content-type": "application/json",
  "accept": "application/json",
  'Authorization': "Bearer $authToken",
  "Cache-Control": "no-cache",
};
