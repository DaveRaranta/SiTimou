import 'dart:io';
import 'dart:ui';

import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:sitimou/controllers/laporan_controller.dart';
import 'package:sitimou/helper/colors.dart';
import 'package:sitimou/helper/scroll_settings.dart';
import 'package:sitimou/helper/text_helper.dart';
import 'package:sitimou/widgets/appbar/app_bar.dart';
import 'package:sitimou/widgets/textfield/label_textfield.dart';

class BuatLaporanPage extends StatelessWidget {
  BuatLaporanPage({super.key});

  final LaporanController controller = Get.find<LaporanController>();

  @override
  Widget build(BuildContext context) {
    return MediaQuery(
      data: MediaQuery.of(context).copyWith(textScaleFactor: 1.0),
      child: GestureDetector(
        onTap: () => FocusScope.of(context).unfocus(),
        child: Scaffold(
          backgroundColor: primaryColor,
          appBar: appBar(context,
              titleText: "BUAT LAPORAN",
              backgroundColor: primaryColor,
              //titleFontSize: 20.0,
              titleTextColor: Colors.black54,
              showLeading: true,
              leadingIcon: const Icon(
                Icons.arrow_back_ios_new,
                color: Colors.black54,
              ),
              leadingFunction: () {
                controller.cleanUp();
                Get.back();
              },
              showTailing: true,
              tailingIcons: const Icon(
                Icons.check_sharp,
                color: Colors.black54,
                size: 28.0,
              ),
              tailingFunction: () {
                controller.simpanLaporan(context);
              }),
          body: SafeArea(
            child: SizedBox(
              height: double.infinity,
              width: double.infinity,
              child: Padding(
                padding: const EdgeInsets.only(top: 20.0, left: 20.0, right: 20.0),
                child: ScrollConfiguration(
                  behavior: ScrollSettings(),
                  child: SingleChildScrollView(
                    child: Column(
                      crossAxisAlignment: CrossAxisAlignment.start,
                      children: [
                        TextField(
                          enableInteractiveSelection: true,
                          decoration: const InputDecoration(
                            border: OutlineInputBorder(
                              // width: 0.0 produces a thin "hairline" border
                              borderRadius: BorderRadius.all(Radius.circular(10.0)),
                              borderSide: BorderSide.none,
                            ),
                            filled: true,
                            fillColor: Colors.white,
                            //hintText: 'Reply',
                            labelText: 'Perihal',
                            labelStyle: TextStyle(
                              color: Colors.blue,
                              fontSize: 16.0,
                              fontWeight: FontWeight.w500,
                            ),
                          ),
                          cursorColor: Colors.grey,
                          autofocus: false,
                          maxLines: null,
                          controller: controller.textTentang,
                          keyboardType: TextInputType.text,
                          inputFormatters: [UpperCaseTextFormatter()],
                        ),
                        const SizedBox(height: 20),
                        TextField(
                          enableInteractiveSelection: true,
                          decoration: const InputDecoration(
                            border: OutlineInputBorder(
                              // width: 0.0 produces a thin "hairline" border
                              borderRadius: BorderRadius.all(Radius.circular(10.0)),
                              borderSide: BorderSide.none,
                            ),
                            filled: true,
                            fillColor: Colors.white,
                            //hintText: 'Reply',
                            labelText: 'Isi Laporan',
                            labelStyle: TextStyle(
                              color: Colors.blue,
                              fontSize: 16.0,
                              fontWeight: FontWeight.w500,
                            ),
                          ),
                          cursorColor: Colors.grey,
                          autofocus: false,
                          maxLines: null,
                          controller: controller.textIsiLaporan,
                          keyboardType: TextInputType.multiline,
                          inputFormatters: [UpperCaseTextFormatter()],
                        ),
                        //
                        // Icon Pilih foto
                        //
                        const SizedBox(height: 10),
                        Align(
                          alignment: Alignment.centerRight,
                          child: GestureDetector(
                            onTap: () {
                              controller.pickImage(context);
                            },
                            child: const Icon(
                              Icons.image_outlined,
                              color: Colors.blue,
                            ),
                          ),
                        ),
                        const SizedBox(height: 10),
                        Obx(() {
                          if (controller.imageFile.isEmpty) {
                            return Container();
                          }

                          var file = File.fromUri(Uri.parse(controller.imageFile.value));

                          return ClipRRect(
                            borderRadius: BorderRadius.circular(10.0),
                            child: Image.file(
                              file,
                              fit: BoxFit.scaleDown,
                            ),
                          );
                        }),
                        const SizedBox(height: 30.0),
                      ],
                    ),
                  ),
                ),
              ),
            ),
          ),
        ),
      ),
    );
  }
}
