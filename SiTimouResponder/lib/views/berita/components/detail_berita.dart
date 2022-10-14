import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:flutter_html/flutter_html.dart';
import 'package:cached_network_image/cached_network_image.dart';
import 'package:fluentui_system_icons/fluentui_system_icons.dart';

import 'package:sitimoufr/helper/colors.dart';
import 'package:sitimoufr/helper/globals.dart' as g;
import 'package:sitimoufr/helper/scroll_settings.dart';
import 'package:sitimoufr/views/common/image_viewer.dart';

const String mainUrl = "${g.apiUrl}/info/cover_berita";

class DetailBeritaPage extends StatelessWidget {
  final String imageCover;
  final String textTanggal;
  final String textTitle;
  final String textIsi;

  const DetailBeritaPage({
    Key? key,
    required this.imageCover,
    required this.textTanggal,
    required this.textTitle,
    required this.textIsi,
  }) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Material(
      child: MediaQuery(
        data: MediaQuery.of(context).copyWith(textScaleFactor: 1.0),
        child: Scaffold(
          backgroundColor: primaryColor,
          body: Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              Stack(
                children: [
                  Container(
                    height: 200,
                    decoration: BoxDecoration(
                      color: Colors.black87,
                      boxShadow: const [
                        BoxShadow(color: Colors.black54, spreadRadius: 7, blurRadius: 25),
                      ],
                      image: DecorationImage(
                        fit: BoxFit.fitWidth,
                        colorFilter: ColorFilter.mode(Colors.black.withOpacity(0.4), BlendMode.dstATop),
                        image: CachedNetworkImageProvider(
                          "$mainUrl/$imageCover",
                          headers: g.httpHeaders,
                          maxHeight: 200,
                        ),
                      ),
                    ),
                  ),
                  SafeArea(
                    child: Padding(
                      padding: const EdgeInsets.only(top: 14, left: 17.0, right: 17.0),
                      child: Column(
                        crossAxisAlignment: CrossAxisAlignment.start,
                        children: [
                          Row(
                            mainAxisAlignment: MainAxisAlignment.spaceBetween,
                            children: [
                              GestureDetector(
                                child: const Icon(
                                  Icons.arrow_back_ios_new,
                                  color: Colors.white,
                                ),
                                onTap: () => Get.back(),
                              ),
                              GestureDetector(
                                child: const Icon(
                                  FluentIcons.resize_large_24_regular,
                                  color: Colors.white,
                                ),
                                onTap: () {
                                  // debugPrint(_controller.detailBeritaKota.value.fotoCover);
                                  Get.to(
                                    () => ImageViewer(
                                      provider: CachedNetworkImageProvider(
                                        "$mainUrl/$imageCover",
                                      ),
                                    ),
                                  );
                                },
                              ),
                            ],
                          ),
                          const SizedBox(height: 30.0),
                          Text(
                            textTanggal,
                            maxLines: 1,
                            style: const TextStyle(
                              color: Colors.white,
                              fontSize: 12.0,
                              fontWeight: FontWeight.w600,
                            ),
                          ),
                          Text(
                            textTitle,
                            maxLines: 2,
                            overflow: TextOverflow.ellipsis,
                            style: const TextStyle(
                              color: Colors.white,
                              fontSize: 20.0,
                              fontWeight: FontWeight.w600,
                            ),
                          ),
                          /*
                          SizedBox(height: 5),
                          Text(
                            "Oleh: " + _controller.detailBeritaKota.value.namaPenulis.toString(),
                            maxLines: 1,
                            style: TextStyle(
                              color: Colors.white,
                              fontSize: 12.0,
                              fontWeight: FontWeight.w600,
                              fontStyle: FontStyle.italic,
                            ),
                          ),
                          */
                        ],
                      ),
                    ),
                  )
                ],
              ),
              const SizedBox(height: 35.0),
              Padding(
                padding: const EdgeInsets.only(left: 20.0, right: 20.0),
                child: Text(
                  '"$textTitle"',
                  style: const TextStyle(
                    color: Colors.black,
                    fontSize: 12.5,
                    fontWeight: FontWeight.w600,
                    fontStyle: FontStyle.italic,
                  ),
                ),
              ),
              const SizedBox(height: 15.0),
              Expanded(
                child: Padding(
                  padding: const EdgeInsets.only(left: 20.0, right: 20.0),
                  child: SizedBox(
                    width: double.infinity,
                    child: ScrollConfiguration(
                      behavior: ScrollSettings(),
                      child: SingleChildScrollView(
                        child: Html(
                          data: textIsi,
                        ),
                      ),
                    ),
                  ),
                ),
              ),
              const SizedBox(height: 15.0),
            ],
          ),
        ),
      ),
    );
  }
}
