import 'dart:ui';
import 'package:avatar_glow/avatar_glow.dart';
import 'package:cached_network_image/cached_network_image.dart';
import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:holding_gesture/holding_gesture.dart';

import 'package:sitimou/controllers/laporan_controller.dart';
import 'package:sitimou/helper/globals.dart' as g;
import 'package:sleek_button/sleek_button.dart';

String fotoUrl = "${g.apiUrl}/home/foto_profil_pengguna";

class PanicPage extends StatelessWidget {
  PanicPage({super.key});

  final LaporanController controller = Get.find<LaporanController>();

  @override
  Widget build(BuildContext context) {
    Size size = MediaQuery.of(context).size;
    return MediaQuery(
      data: MediaQuery.of(context).copyWith(textScaleFactor: 1.0),
      child: Obx(
        () => Scaffold(
          backgroundColor: controller.isPanicPressed.value ? const Color.fromARGB(255, 248, 88, 12) : const Color.fromARGB(255, 149, 19, 36),
          body: Obx(() {
            if (!controller.isPanicPressed.value) {
              return panic(size);
            } else {
              return panicked(context);
            }
          }),
        ),
      ),
    );
  }
}

Widget panic(Size size) {
  final LaporanController controller = Get.find<LaporanController>();

  return Stack(
    children: [
      Center(
        child: AvatarGlow(
          glowColor: Colors.white,
          endRadius: 135.0,
          child: HoldTimeoutDetector(
            onTimerInitiated: () => controller.onPanicTimerInitiated(),
            onTimeout: () => controller.onPanicTimeout(),
            holdTimeout: const Duration(seconds: 5),
            child: ElevatedButton(
              onPressed: () {},
              style: ElevatedButton.styleFrom(
                onPrimary: const Color.fromARGB(255, 149, 19, 36),
                elevation: 7.0,
                shape: const CircleBorder(
                  side: BorderSide(
                    width: 7,
                    color: Colors.white,
                    style: BorderStyle.solid,
                  ),
                ),
                primary: Colors.red,
                fixedSize: const Size(145, 145),
              ),
              child: const Text(
                "PANIK",
                style: TextStyle(
                  color: Colors.white,
                  fontSize: 28.0,
                  fontWeight: FontWeight.w600,
                ),
              ),
            ),
          ),
        ),
      ),
      Align(
        alignment: Alignment.bottomCenter,
        child: SizedBox(
          width: size.width,
          child: Padding(
            padding: const EdgeInsets.only(bottom: 35.0, left: 20.0, right: 20.0),
            child: Container(
              height: 110.0,
              width: double.infinity,
              margin: const EdgeInsets.symmetric(vertical: 0),
              padding: const EdgeInsets.symmetric(horizontal: 10, vertical: 10),
              decoration: BoxDecoration(
                color: const Color.fromARGB(255, 149, 19, 36),
                borderRadius: BorderRadius.circular(10),
              ),
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.center,
                mainAxisAlignment: MainAxisAlignment.center,
                children: const [
                  Text(
                    "TEKAN DAN TAHAN TOMBOL PANIK",
                    maxLines: 1,
                    style: TextStyle(
                      color: Colors.white,
                      fontSize: 18.0,
                      fontWeight: FontWeight.w600,
                    ),
                    overflow: TextOverflow.ellipsis,
                  ),
                  SizedBox(height: 10.0),
                  Text(
                    "PERHATIAN...! GUNAKAN TOMBOL PANIK HANYA JIKA ANDA MENGALAMI KEADAAN DARURAT.",
                    textAlign: TextAlign.center,
                    style: TextStyle(
                      color: Colors.white,
                      fontSize: 15.0,
                      fontWeight: FontWeight.w500,
                    ),
                  ),
                ],
              ),
            ),
          ),
        ),
      ),
    ],
  );
}

Widget panicked(BuildContext context) {
  final LaporanController controller = Get.find<LaporanController>();

  return SizedBox(
    width: double.infinity,
    child: Padding(
      padding: const EdgeInsets.only(left: 20.0, right: 20.0),
      child: Column(
        mainAxisAlignment: MainAxisAlignment.center,
        crossAxisAlignment: CrossAxisAlignment.center,
        children: [
          ClipOval(
            child: CachedNetworkImage(
              imageUrl: "$fotoUrl/${g.userId}",
              httpHeaders: g.httpHeaders,
              height: 128.0,
              width: 128.0,
              progressIndicatorBuilder: (context, url, downloadProgress) => CircularProgressIndicator(value: downloadProgress.progress),
              errorWidget: (context, url, error) => const Icon(Icons.error),
              fit: BoxFit.cover,
            ),
          ),
          const SizedBox(height: 12.0),
          Text(
            controller.detailPengguna.value.namaLengkap.toString(),
            maxLines: 1,
            style: const TextStyle(
              color: Colors.white,
              fontSize: 25.0,
              fontWeight: FontWeight.w600,
              overflow: TextOverflow.ellipsis,
            ),
          ),
          const SizedBox(height: 2.0),
          controller.detailPengguna.value.umur.toString() != "-"
              ? Text(
                  "UMUR ${controller.detailPengguna.value.umur.toString()} TAHUN",
                  maxLines: 1,
                  style: const TextStyle(
                    color: Colors.white,
                    fontSize: 18.0,
                    fontWeight: FontWeight.w500,
                    overflow: TextOverflow.ellipsis,
                  ),
                )
              : Container(),
          const SizedBox(height: 35.0),
          const Text(
            "SAAT INI SEDANG DALAM KONDISI DARURAT. MOHON UNTUK DI DAMPINGI DAN DIBERIKAN PERTOLONGAN PERTAMA JIKA DIBUTUHKAN SAMBIL MENUNGGU TIM MEDIS TIBA DI LOKASI",
            textAlign: TextAlign.center,
            style: TextStyle(
              color: Colors.white,
              fontSize: 20.0,
              fontWeight: FontWeight.w500,
            ),
          ),
          const SizedBox(height: 80.0),
          SizedBox(
            height: 50.0,
            child: SleekButton(
              style: SleekButtonStyle.flat(
                color: const Color.fromARGB(255, 149, 19, 36),
                context: context,
              ),
              child: Row(
                mainAxisAlignment: MainAxisAlignment.center,
                children: const [
                  Text(
                    'KEMBALI',
                    style: TextStyle(
                      color: Colors.white,
                      fontSize: 17.0,
                    ),
                  ),
                ],
              ),
              onTap: () {
                Get.back();
              },
            ),
          ),
        ],
      ),
    ),
  );
}
