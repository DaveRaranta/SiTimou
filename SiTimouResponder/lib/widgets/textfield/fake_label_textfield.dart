import 'package:flutter/material.dart';

import 'package:sitimoufr/helper/colors.dart';
import 'package:sitimoufr/widgets/panel/round_container.dart';

/*
  REQUIRED:
  - colors.dart
  - default_textfield.dart
*/

class FakeTextFieldWithLabel extends StatelessWidget {
  final String labelText;
  final double labelFontSize;
  final FontWeight labelFontWeight;
  final Color labelTextColor;
  final double fakeHeight;
  final String fakeText;
  final double fakeTextSize;
  final Color fakeTextColor;
  final Color backgroundColor;
  final bool enableInteractiveSelection;

  const FakeTextFieldWithLabel({
    Key? key,
    required this.labelText,
    required this.fakeText,
    this.labelFontSize = 16.0,
    this.labelFontWeight = FontWeight.w600,
    this.labelTextColor = Colors.black54,
    this.fakeHeight = 40.0,
    this.fakeTextSize = 15.0,
    this.fakeTextColor = Colors.black,
    this.backgroundColor = bSecondaryColor,
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
        SizedBox(height: 7.0),
        RoundedSmallContainer(
          height: fakeHeight,
          fillColor: backgroundColor,
          child: Align(
            alignment: Alignment.centerLeft,
            child: Text(
              fakeText,
              textAlign: TextAlign.left,
              style: TextStyle(
                fontSize: fakeTextSize,
                color: fakeTextColor,
              ),
            ),
          ),
        )
      ],
    );
  }
}
