import 'package:cached_network_image/cached_network_image.dart';
import 'package:connectivity_plus/connectivity_plus.dart';
import 'package:internet_connection_checker/internet_connection_checker.dart';
import 'package:intl/date_symbol_data_local.dart';
import 'package:intl/intl.dart';

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
}


// # Konesi stuff

