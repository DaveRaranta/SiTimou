import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:sitimoufr/helper/colors.dart';
import 'package:sitimoufr/widgets/panel/round_container.dart';

/*
  REQUIRED:
  - colors.dart
  - round_container.dart
*/

class RoundedTextField extends StatelessWidget {
  final String hintText;
  final TextEditingController? controller;
  final List<TextInputFormatter>? inputFormatters;
  final TextCapitalization capitalization;
  final TextInputType inputType;
  final bool readOnly;
  final int maxLines;
  final double height;
  final int maxLenght;
  final Color backgroundColor;
  final Color textColor;
  final double textSize;
  final bool obscureText;
  final Widget? icon;
  final Widget? suffixIcon;
  final bool enableInteractiveSelection;
  final ValueChanged<String>? onChanged;

  const RoundedTextField(
      {Key? key,
      required this.hintText,
      this.inputFormatters,
      this.controller,
      this.inputType = TextInputType.text,
      this.capitalization = TextCapitalization.none,
      this.readOnly = false,
      this.maxLines = 1,
      this.height = 40.0,
      this.maxLenght = 100,
      this.backgroundColor = bSecondaryColor,
      this.textColor = Colors.black,
      this.textSize = 15.0,
      this.obscureText = false,
      this.enableInteractiveSelection = true,
      this.icon,
      this.suffixIcon,
      this.onChanged})
      : super(key: key);

  @override
  Widget build(BuildContext context) {
    return RoundedSmallContainer(
      height: height,
      fillColor: backgroundColor,
      child: TextField(
        controller: controller,
        cursorColor: Colors.black87,
        enableInteractiveSelection: enableInteractiveSelection,
        style: TextStyle(color: textColor, fontSize: textSize),
        onChanged: onChanged,
        decoration: InputDecoration(
            hintText: hintText,
            icon: icon,
            suffixIcon: suffixIcon,
            hintStyle: TextStyle(
              color: Colors.grey,
              fontStyle: FontStyle.italic,
              fontSize: textSize,
            ),
            border: InputBorder.none,
            counterStyle: const TextStyle(
              height: double.minPositive,
            ),
            counterText: ""),
        keyboardType: inputType,
        inputFormatters: inputFormatters,
        textCapitalization: capitalization,
        readOnly: readOnly,
        maxLines: maxLines,
        maxLength: maxLenght,
        obscureText: obscureText,
      ),
    );
  }
}
