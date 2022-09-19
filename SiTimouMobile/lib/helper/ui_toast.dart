import 'package:flutter/material.dart';
import 'package:get/get.dart';

void toastPesan(
  String title,
  String message, [
  SnackPosition snackPosition = SnackPosition.BOTTOM,
  Color backgroundColor = Colors.black,
  double opacity = 0.5,
  Color textColor = Colors.white,
  double borderRadius = 10.0,
  double margin = 5,
  int duration = 3,
]) {
  Get.snackbar(
    title,
    message,
    snackPosition: snackPosition,
    colorText: textColor,
    backgroundColor: backgroundColor.withOpacity(opacity),
    borderRadius: borderRadius,
    margin: EdgeInsets.all(margin),
    duration: Duration(seconds: duration),
  );
}
