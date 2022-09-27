import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:sitimoufr/widgets/textfield/default_textfield.dart';

/*
  REQUIRED:
  - colors.dart
  - default_textfield.dart
*/

class TextFieldWithLabel extends StatelessWidget {
  final String labelText;
  final double labelFontSize;
  final FontWeight labelFontWeight;
  final Color labelTextColor;
  final TextEditingController controller;
  final String hintText;
  final List<TextInputFormatter> inputFormatters;
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
  final bool enableInteractiveSelection;

  const TextFieldWithLabel({
    Key? key,
    required this.labelText,
    required this.controller,
    required this.hintText,
    required this.inputFormatters,
    this.labelFontSize = 16.0,
    this.labelFontWeight = FontWeight.w600,
    this.labelTextColor = Colors.black54,
    this.inputType = TextInputType.text,
    this.capitalization = TextCapitalization.none,
    this.readOnly = false,
    this.maxLines = 1,
    this.height = 40.0,
    this.maxLenght = 100,
    this.backgroundColor = Colors.white,
    this.textColor = Colors.black,
    this.textSize = 15.0,
    this.obscureText = false,
    this.enableInteractiveSelection = true,
  }) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        Text(
          labelText,
          style: TextStyle(
            fontSize: labelFontSize,
            fontWeight: labelFontWeight,
            color: labelTextColor,
          ),
        ),
        const SizedBox(height: 7.0),
        RoundedTextField(
          inputType: inputType,
          capitalization: capitalization,
          readOnly: readOnly,
          maxLines: maxLines,
          maxLenght: maxLenght,
          height: height,
          backgroundColor: backgroundColor,
          controller: controller,
          hintText: hintText,
          inputFormatters: inputFormatters,
          enableInteractiveSelection: enableInteractiveSelection,
          textColor: textColor,
          textSize: textSize,
          obscureText: obscureText,
        ),
      ],
    );
  }
}
