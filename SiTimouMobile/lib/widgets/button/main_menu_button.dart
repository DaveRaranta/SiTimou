import 'package:flutter/material.dart';

class MainMenuButton extends StatelessWidget {
  const MainMenuButton({
    Key? key,
    required this.icon,
    required this.text,
    required this.hero,
    required this.onTap,
    required this.color,
    this.height = 48.0,
    this.width = 48.0,
    this.textSize = 12.0,
    this.textColor = Colors.black,
    this.iconSize = 24.0,
    this.iconColor = Colors.grey,
    this.borderColor = Colors.grey,
    this.borderRadius = 10,
    this.depth = 5,
    this.intens = .5,
  }) : super(key: key);

  final IconData icon;
  final String text, hero;
  final VoidCallback onTap;
  final Color color, textColor, iconColor, borderColor;
  final double? height, width, textSize, iconSize, depth, intens;
  final double borderRadius;

  @override
  Widget build(BuildContext context) {
    return InkWell(
      onTap: onTap,
      child: SizedBox(
        width: 70,
        child: Column(
          children: [
            Container(
              width: width! > 70 ? 70 : width,
              height: height! > 70 ? 70 : height,
              decoration: BoxDecoration(
                color: color,
                border: Border.all(color: borderColor),
                borderRadius: BorderRadius.all(Radius.circular(borderRadius)),
                /*
                boxShadow: [
                  BoxShadow(
                    color: Colors.grey.withOpacity(0.5),
                    spreadRadius: 1,
                    blurRadius: 5,
                    offset: Offset(3, 6), // changes position of shadow
                  ),
                ],
                */
              ),
              child: Center(
                child: Icon(
                  icon,
                  size: iconSize,
                  color: iconColor,
                ),
              ),
            ),
            const SizedBox(height: 7),
            Text(
              text,
              style: TextStyle(
                color: textColor,
                fontSize: textSize,
              ),
              maxLines: 2,
              textAlign: TextAlign.center,
            )
          ],
        ),
      ),
    );
  }
}
