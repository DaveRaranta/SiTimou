import 'package:cached_network_image/cached_network_image.dart';
import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:image_picker/image_picker.dart';
import 'package:sitimou/controllers/home_controller.dart';
import 'package:sitimou/helper/colors.dart';

import 'package:sitimou/helper/globals.dart' as g;

String fotoUrl = "${g.apiUrl}/home/foto_profil_pengguna";

class UbahFotoProfil extends StatelessWidget {
  final ImagePicker imagePicker = ImagePicker();
  final HomeController controller = Get.find<HomeController>();

  UbahFotoProfil({super.key});

  @override
  Widget build(BuildContext context) {
    return MediaQuery(
      data: MediaQuery.of(context).copyWith(textScaleFactor: 1.0),
      child: Scaffold(
        backgroundColor: primaryColor,
        body: Center(
          child: Column(
            mainAxisAlignment: MainAxisAlignment.center,
            crossAxisAlignment: CrossAxisAlignment.center,
            children: [
              GestureDetector(
                  child: Container(
                    alignment: Alignment.center,
                    height: 140.0,
                    width: 140.0,
                    decoration: const BoxDecoration(
                      color: Colors.white,
                      shape: BoxShape.circle,
                      boxShadow: [
                        BoxShadow(
                          color: Colors.white,
                          blurRadius: 0.0,
                          spreadRadius: 2.0,
                        ),
                      ],
                    ),
                    child: Obx(() {
                      if (controller.isLoading.value) {
                        return const Center(
                          child: CircularProgressIndicator(),
                        );
                      }
                      return CircleAvatar(
                        radius: 70.0,
                        backgroundImage: CachedNetworkImageProvider("$fotoUrl/${g.userId}", headers: g.httpHeaders),
                      );
                    }),
                  ),
                  onTap: () {
                    controller.pickImage(context);
                  }),
              const SizedBox(height: 25.0),
              const Text(
                "TAP UNTUK PILIH FOTO",
                style: TextStyle(color: Colors.black, fontSize: 15.0, fontWeight: FontWeight.w400),
              )
            ],
          ),
        ),
      ),
    );
  }
}
