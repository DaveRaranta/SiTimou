import 'package:flutter/material.dart';
import 'package:cached_network_image/cached_network_image.dart';

import 'package:sitimou/helper/colors.dart';
import 'package:sitimou/helper/globals.dart' as g;

const String mainUrl = "${g.apiUrl}/info/cover_berita";

class CardRecentBeritaSmall extends StatelessWidget {
  final String imageCover;
  final double imageWidth;
  final double imageHeight;
  final double imageBorder;
  final String textTitle;
  final String textFooter;
  final double height;
  final Color backgroundColor;

  const CardRecentBeritaSmall({
    Key? key,
    required this.textTitle,
    required this.textFooter,
    required this.imageCover,
    this.imageWidth = 80.0,
    this.imageHeight = 60.0,
    this.imageBorder = 8.0,
    this.backgroundColor = primaryColor,
    this.height = 60,
  }) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Container(
      width: double.infinity,
      height: height,
      color: backgroundColor,
      child: Row(
        mainAxisAlignment: MainAxisAlignment.start,
        children: [
          Column(
            mainAxisAlignment: MainAxisAlignment.center,
            children: [
              // Leading Image
              Container(
                child: ClipRRect(
                  borderRadius: BorderRadius.circular(imageBorder),
                  child: CachedNetworkImage(
                    imageUrl: "$mainUrl/$imageCover",
                    httpHeaders: g.httpHeaders,
                    height: imageHeight,
                    width: imageWidth,
                    progressIndicatorBuilder: (context, url, downloadProgress) =>
                        Center(child: CircularProgressIndicator(value: downloadProgress.progress)),
                    errorWidget: (context, url, error) => Icon(Icons.error),
                    fit: BoxFit.cover,
                  ),
                ),
              ),
            ],
          ),
          SizedBox(width: 7.0),
          Expanded(
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                Spacer(),
                Text(
                  textTitle,
                  maxLines: 2,
                  softWrap: false,
                  overflow: TextOverflow.ellipsis,
                  style: TextStyle(
                    fontSize: 15.0,
                    fontWeight: FontWeight.w600,
                  ),
                ),
                Spacer(),
                SizedBox(
                  height: 20.0,
                  child: Text(
                    textFooter,
                    maxLines: 1,
                    style: TextStyle(
                      fontSize: 11.0,
                      fontWeight: FontWeight.w600,
                      color: Colors.black45,
                    ),
                  ),
                ),
              ],
            ),
          )
        ],
      ),
    );
  }
}
