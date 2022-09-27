import 'package:fluentui_system_icons/fluentui_system_icons.dart';
import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:flutter_html/flutter_html.dart';

import 'package:sitimoufr/controllers/info_controller.dart';
import 'package:sitimoufr/helper/colors.dart';
import 'package:sitimoufr/helper/ui_toast.dart';
import 'package:sitimoufr/widgets/appbar/app_bar.dart';

class DetailAturanPage extends StatelessWidget {
  DetailAturanPage({super.key});

  final controller = Get.find<InfoController>();

  @override
  Widget build(BuildContext context) {
    return Material(
      child: MediaQuery(
        data: MediaQuery.of(context).copyWith(textScaleFactor: 1.0),
        child: Scaffold(
          backgroundColor: primaryColor,
          appBar: appBar(
            context,
            titleText: "DETAIL ATURAN",
            backgroundColor: primaryColor,
            //titleFontSize: 20.0,
            titleTextColor: Colors.black54,
            showLeading: true,
            leadingIcon: const Icon(
              Icons.arrow_back_ios_new,
              color: Colors.black54,
            ),
            leadingFunction: () {
              Get.back();
            },
            showTailing: false,
            tailingIcons: const Icon(
              FluentIcons.history_24_filled,
              color: Colors.black54,
            ),
          ),
          body: SafeArea(
            child: SizedBox(
              height: double.infinity,
              width: double.infinity,
              child: Padding(
                padding: const EdgeInsets.only(top: 20.0, left: 20.0, right: 20.0),
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    GestureDetector(
                      child: Text(
                        controller.detailAturan.value.judul.toString(),
                        maxLines: 2,
                        softWrap: false,
                        overflow: TextOverflow.ellipsis,
                        style: const TextStyle(
                          fontSize: 20.0,
                          fontWeight: FontWeight.w500,
                        ),
                      ),
                      onTap: () {
                        toastPesan(
                          "JUDUL ATURAN",
                          controller.detailAturan.value.judul.toString(),
                        );
                      },
                    ),
                    const SizedBox(height: 5),
                    Text(
                      controller.detailAturan.value.namaOpd.toString(),
                      maxLines: 2,
                      softWrap: false,
                      overflow: TextOverflow.ellipsis,
                      style: const TextStyle(
                        fontSize: 14.0,
                        fontWeight: FontWeight.w600,
                        color: Colors.black54,
                      ),
                    ),
                    const SizedBox(height: 1),
                    Text(
                      "Tanggal update: ${controller.detailAturan.value.namaOpd}",
                      maxLines: 1,
                      softWrap: false,
                      overflow: TextOverflow.ellipsis,
                      style: const TextStyle(
                        fontSize: 12.0,
                        fontWeight: FontWeight.w600,
                        color: Colors.black54,
                        fontStyle: FontStyle.italic,
                      ),
                    ),
                    const SizedBox(height: 10),
                    Expanded(
                      child: Html(data: controller.detailAturan.value.isi),
                    ),
                    const SizedBox(height: 30.0),
                  ],
                ),
              ),
            ),
          ),
        ),
      ),
    );
  }
}
