import 'package:cached_network_image/cached_network_image.dart';
import 'package:flutter/material.dart';
import 'package:sitimoufr/helper/colors.dart';

import 'package:sitimoufr/helper/globals.dart' as g;

const String mainUrl = "${g.apiUrl}/info/cover_berita";

class CardBeritaSmall extends StatelessWidget {
  final String imageCover;
  final double imageWidth;
  final double imageHeight;
  final double imageBorder;
  final String textHeader;
  final String textContent;
  final String textFooter;
  final String textJenis;

  final double height;
  final String idData;
  final Color backgroundColor;

  const CardBeritaSmall({
    Key? key,
    required this.textHeader,
    required this.textContent,
    required this.textJenis,
    required this.imageCover,
    required this.idData,
    this.textFooter = "",
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
              ClipRRect(
                borderRadius: BorderRadius.circular(imageBorder),
                child: CachedNetworkImage(
                  imageUrl: "$mainUrl/$imageCover",
                  httpHeaders: g.httpHeaders,
                  height: imageHeight,
                  width: imageWidth,
                  progressIndicatorBuilder: (context, url, downloadProgress) =>
                      Center(child: CircularProgressIndicator(value: downloadProgress.progress)),
                  errorWidget: (context, url, error) => const Icon(Icons.error),
                  fit: BoxFit.cover,
                ),
              ),
            ],
          ),
          SizedBox(width: 7.0),
          Expanded(
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                Row(
                  mainAxisAlignment: MainAxisAlignment.spaceBetween,
                  children: [
                    Expanded(
                      child: Text(
                        textHeader,
                        maxLines: 1,
                        softWrap: false,
                        overflow: TextOverflow.ellipsis,
                        style: const TextStyle(
                          fontSize: 12.0,
                          fontWeight: FontWeight.w600,
                          color: Colors.black54,
                        ),
                      ),
                    ),
                    Text(
                      textJenis,
                      maxLines: 1,
                      softWrap: false,
                      style: const TextStyle(
                        fontSize: 11.0,
                        fontWeight: FontWeight.w600,
                        color: Colors.black54,
                      ),
                    ),
                  ],
                ),
                SizedBox(height: 2),
                Text(
                  textContent,
                  maxLines: 2,
                  softWrap: false,
                  overflow: TextOverflow.ellipsis,
                  style: const TextStyle(
                    fontSize: 15.0,
                    fontWeight: FontWeight.w500,
                  ),
                ),
                /*
                SizedBox(height: 2),
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
                ),*/
              ],
            ),
          )
        ],
      ),
    );
  }
}
