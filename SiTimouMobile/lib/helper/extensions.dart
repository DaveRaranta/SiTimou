import 'package:intl/date_symbol_data_local.dart';
import 'package:intl/intl.dart';
//
// Extensions Methode
//

// String Extensions
extension E on String {
  String lastChars(int n) {
    int from = length - n;
    return (from.isNegative) ? "" : substring(from);
  }
}

extension DateTimeExtension on DateTime {
  String format([String pattern = 'EEEE, MMMM d y', String? locale]) {
    if (locale != null && locale.isNotEmpty) {
      initializeDateFormatting(locale);
    }
    return DateFormat(pattern, locale).format(this);
  }
}
