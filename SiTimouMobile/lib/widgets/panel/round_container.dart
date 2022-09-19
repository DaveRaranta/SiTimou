import 'package:flutter/material.dart';
import 'package:sitimou/helper/colors.dart';

class RoundedSmallContainer extends StatelessWidget {
  final Widget child;
  final double height;
  final double width;
  final Color fillColor;
  final Color borderColor;
  final double borderWidth;
  final double borderRadius;
  const RoundedSmallContainer({
    Key? key,
    required this.child,
    this.height = 40.0,
    this.width = double.infinity,
    this.fillColor = bSecondaryColor,
    this.borderColor = bSecondaryColor,
    this.borderWidth = 1.0,
    this.borderRadius = 10,
  }) : super(key: key);

  @override
  Widget build(BuildContext context) {
    // Size size = MediaQuery.of(context).size;
    return Container(
      margin: const EdgeInsets.symmetric(vertical: 0),
      padding: const EdgeInsets.symmetric(horizontal: 10, vertical: 1),
      // width: size.width * 0.8,
      width: width,
      height: height,
      decoration: BoxDecoration(
          color: fillColor,
          borderRadius: BorderRadius.circular(borderRadius),
          border: Border.all(
            color: borderColor,
            width: borderWidth,
          )),
      child: child,
    );
  }
}
