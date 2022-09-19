import 'package:flutter/material.dart';
import 'package:sitimou/helper/colors.dart';

AppBar appBar(context,
    {required String titleText,
    double? titleFontSize = 20.0,
    Color titleTextColor = Colors.black54,
    bool showTailing = false,
    bool showLeading = false,
    Widget? leadingIcon,
    Widget? tailingIcons,
    PreferredSizeWidget? bottom,
    bool centerTitle = true,
    VoidCallback? leadingFunction,
    VoidCallback? tailingFunction,
    Color backgroundColor = primaryColor}) {
  return AppBar(
    elevation: 0,
    title: Text(
      titleText,
      style: TextStyle(
        color: titleTextColor,
        fontSize: titleFontSize,
      ),
    ),
    centerTitle: centerTitle,
    backgroundColor: backgroundColor,
    leading: showLeading == true
        ? GestureDetector(
            onTap: leadingFunction,
            child: leadingIcon,
          )
        : Container(),
    actions: [
      if (showTailing)
        GestureDetector(
          onTap: tailingFunction,
          child: Padding(
            padding: const EdgeInsets.only(right: 20.0),
            child: tailingIcons,
          ),
        )
    ],
    bottom: bottom,
  );
}
