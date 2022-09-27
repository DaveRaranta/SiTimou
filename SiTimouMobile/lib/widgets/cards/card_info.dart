import 'package:flutter/material.dart';
import 'package:sitimou/helper/colors.dart';

class CardInfo extends StatelessWidget {
  final Color leadBoxColor;
  final double leadBoxSize;
  final double borderRadius;
  final String textContent;
  final String textFooter;
  final IconData iconStatus;
  //final String idData;
  final Color backgroundColor;

  const CardInfo({
    Key? key,
    required this.textContent,
    //required this.idData,
    required this.textFooter,
    required this.iconStatus,
    required this.leadBoxColor,
    this.leadBoxSize = 60,
    this.borderRadius = 8.0,
    this.backgroundColor = primaryColor,
  }) : super(key: key);

  @override
  Widget build(BuildContext context) {
    double minSize = leadBoxSize < 60 ? 60 : leadBoxSize;

    return Container(
      width: double.infinity,
      height: minSize,
      decoration: BoxDecoration(
        color: Colors.white,
        borderRadius: BorderRadius.all(Radius.circular(borderRadius)),
        boxShadow: [
          BoxShadow(
            color: Colors.grey.withOpacity(0.5),
            spreadRadius: 1,
            blurRadius: 4,
            offset: const Offset(0, 3), // changes position of shadow
          ),
        ],
      ),
      child: Row(
        mainAxisAlignment: MainAxisAlignment.start,
        children: [
          Column(
            mainAxisAlignment: MainAxisAlignment.center,
            children: [
              // Leading Image
              Container(
                height: minSize,
                width: minSize,
                decoration: BoxDecoration(
                  color: leadBoxColor,
                  borderRadius: BorderRadius.all(Radius.circular(borderRadius)),
                ),
                child: Icon(
                  iconStatus,
                  size: minSize - 25, //40.0,
                  color: Colors.white,
                ),
              ),
            ],
          ),
          const SizedBox(width: 7.0),
          Expanded(
            child: Padding(
              padding: const EdgeInsets.only(right: 10.0),
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                mainAxisAlignment: MainAxisAlignment.center,
                children: [
                  Text(
                    textContent,
                    maxLines: 2,
                    softWrap: false,
                    overflow: TextOverflow.ellipsis,
                    style: const TextStyle(
                      fontSize: 20.0,
                      fontWeight: FontWeight.w500,
                    ),
                  ),
                  const SizedBox(height: 2),
                  Text(
                    textFooter,
                    maxLines: 1,
                    style: const TextStyle(
                      fontSize: 11.0,
                      fontWeight: FontWeight.w600,
                      color: Colors.black45,
                    ),
                    overflow: TextOverflow.ellipsis,
                  ),
                ],
              ),
            ),
          )
        ],
      ),
    );
  }
}
