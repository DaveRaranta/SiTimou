import 'package:cached_network_image/cached_network_image.dart';
import 'package:flutter/material.dart';

import 'package:sitimoufr/helper/globals.dart' as g;
import 'package:sitimoufr/helper/colors.dart';

const String mainUrl = "${g.apiUrl}/lokasi/foto_tmb_lokasi";

class CardLokasi extends StatelessWidget {
  final double imageWidth;
  final double imageHeight;
  final double imageBorder;
  final String textContent;
  final String textFooter;
  final String idData;
  final Color backgroundColor;

  const CardLokasi({
    Key? key,
    // required this.textHeader,
    required this.textContent,
    // required this.textJenis,
    required this.idData,
    this.textFooter = "",
    this.imageWidth = 70.0,
    this.imageHeight = 60.0,
    this.imageBorder = 8.0,
    this.backgroundColor = primaryColor,
  }) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Container(
      width: double.infinity,
      height: imageHeight,
      decoration: BoxDecoration(
        color: Colors.white,
        borderRadius: BorderRadius.all(Radius.circular(imageBorder)),
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
              ClipRRect(
                borderRadius: BorderRadius.circular(imageBorder - 2),
                child: CachedNetworkImage(
                  imageUrl: "$mainUrl/$idData",
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
                  const SizedBox(height: 5),
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
