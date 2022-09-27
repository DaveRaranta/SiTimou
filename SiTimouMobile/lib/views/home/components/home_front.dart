import 'package:get/get.dart';
import 'package:flutter/material.dart';
import 'package:cached_network_image/cached_network_image.dart';
import 'package:fluentui_system_icons/fluentui_system_icons.dart';

import 'package:sitimou/controllers/bindings/bindings.dart';
import 'package:sitimou/controllers/home_controller.dart';
import 'package:sitimou/helper/colors.dart';
import 'package:sitimou/helper/scroll_settings.dart';
import 'package:sitimou/helper/ui_toast.dart';
import 'package:sitimou/views/aturan/aturan.dart';
import 'package:sitimou/views/home/components/ubah_foto.dart';
import 'package:sitimou/views/laporan/my_laporan.dart';
import 'package:sitimou/views/lokasi/lokasi.dart';
import 'package:sitimou/widgets/button/main_menu_button.dart';
import 'package:sitimou/helper/globals.dart' as g;

String fotoUrl = "${g.apiUrl}/home/foto_profil_pengguna";

class HomeFrontTab extends StatelessWidget {
  const HomeFrontTab({super.key});

  @override
  Widget build(BuildContext context) {
    return Container(
      color: primaryColor,
      height: double.infinity,
      width: double.infinity,
      child: Padding(
        padding: const EdgeInsets.only(left: 18.0, right: 18.0),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            const SizedBox(height: 10.0),
            header(context),
            const SizedBox(height: 10.0),
            Expanded(
              child: ScrollConfiguration(
                behavior: ScrollSettings(),
                child: SingleChildScrollView(
                  child: Column(
                    children: [
                      const SizedBox(height: 20.0),
                      info(),
                      const SizedBox(height: 20.0),
                      menu(),
                      const SizedBox(height: 20.0),
                      berita(),
                      const SizedBox(height: 90.0),
                    ],
                  ),
                ),
              ),
            )
          ],
        ),
      ),
    );
  }
}

Widget header(BuildContext context) {
  debugPrint("OK: HOME HEADER");
  final HomeController controller = Get.find<HomeController>();

  return Row(
    children: [
      ClipOval(
        child: CachedNetworkImage(
          imageUrl: "$fotoUrl/${g.userId}",
          httpHeaders: g.httpHeaders,
          height: 40.0,
          width: 40.0,
          progressIndicatorBuilder: (context, url, downloadProgress) => CircularProgressIndicator(value: downloadProgress.progress),
          errorWidget: (context, url, error) => const Icon(Icons.error),
          fit: BoxFit.cover,
        ),
      ),
      const SizedBox(width: 10),
      Expanded(
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            Row(
              children: const [
                Text(
                  "SI-TIMOU",
                  style: TextStyle(
                    color: Colors.red,
                    fontSize: 18.0,
                    fontWeight: FontWeight.w600,
                  ),
                ),
                Text(
                  "119",
                  style: TextStyle(
                    color: Colors.black54,
                    fontSize: 18.0,
                    fontWeight: FontWeight.w600,
                  ),
                ),
              ],
            ),
            const Text(
              "MINAHASA",
              style: TextStyle(
                color: Colors.black54,
                fontSize: 14.0,
                fontWeight: FontWeight.w400,
              ),
            ),
          ],
        ),
      ),
      InkWell(
        child: const Icon(
          FluentIcons.arrow_exit_20_regular,
          color: Colors.black54,
        ),
        onTap: () {
          controller.gantiPengguna(context);
          debugPrint("LOGOFF");
        },
      )
    ],
  );
}

Widget info() {
  final HomeController controller = Get.find<HomeController>();
  return SizedBox(
    width: double.infinity,
    height: 170,
    child: Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        const Text(
          "Hi,",
          style: TextStyle(
            color: Colors.grey,
            fontSize: 27.0,
            fontWeight: FontWeight.w500,
          ),
        ),

        // TODO: Obx()
        Obx(() {
          return Text(
            controller.detailPengguna.value.namaLengkap.toString().toUpperCase(),
            style: const TextStyle(
              color: Colors.black,
              fontSize: 20.0,
              fontWeight: FontWeight.w500,
            ),
            maxLines: 1,
          );
        }),

        const SizedBox(height: 15.0),
        //
        // Pengumuman
        //
        InkWell(
          onTap: () {},
          child: Container(
            height: 95.0,
            width: double.infinity,
            decoration: const BoxDecoration(
              gradient: LinearGradient(
                begin: Alignment.topRight,
                end: Alignment.bottomLeft,
                colors: [
                  Color.fromARGB(255, 108, 132, 245),
                  Color.fromARGB(255, 81, 112, 241),
                ],
              ),
              borderRadius: BorderRadius.all(Radius.circular(10.0)),
            ),
            child: Padding(
              padding: const EdgeInsets.all(8.0),
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                children: [
                  Row(
                    crossAxisAlignment: CrossAxisAlignment.center,
                    mainAxisAlignment: MainAxisAlignment.start,
                    children: const [
                      Icon(
                        FluentIcons.megaphone_loud_20_regular,
                        size: 22,
                        color: Colors.white,
                      ),
                      SizedBox(width: 10.0),
                      Text(
                        "Pengumuman",
                        style: TextStyle(
                          color: Colors.white,
                          fontSize: 14.0,
                          fontWeight: FontWeight.w500,
                        ),
                      )
                    ],
                  ),
                  const SizedBox(height: 10.0),
                  Obx(() {
                    return Text(
                      controller.infoPengumuman.value,
                      style: const TextStyle(
                        color: Colors.white,
                        fontSize: 13.0,
                      ),
                      maxLines: 3,
                    );
                  }),
                ],
              ),
            ),
          ),
        )
      ],
    ),
  );
}

Widget menu() {
  final HomeController controller = Get.find<HomeController>();
  return SizedBox(
    width: double.infinity,
    child: Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        const Text(
          "Menu",
          style: TextStyle(
            fontSize: 16.0,
            color: Colors.black,
            fontWeight: FontWeight.w500,
          ),
          maxLines: 1,
          overflow: TextOverflow.ellipsis,
        ),
        const SizedBox(height: 15.0),
        Wrap(
          runSpacing: 12.0,
          children: [
            MainMenuButton(
              hero: "hr_lapor",
              text: "Lapor",
              color: Colors.white,
              borderColor: Colors.orange,
              icon: Icons.campaign_outlined,
              iconSize: 32.0,
              width: 55.0,
              height: 55.0,
              onTap: () {
                // Cek jika sudah lengkap,
                if (controller.detailPengguna.value.tanggalLahir == '-' ||
                    controller.detailPengguna.value.jenisKelamin == '-' ||
                    controller.detailPengguna.value.noTelp == '-' ||
                    controller.detailPengguna.value.alamat == '-' ||
                    controller.detailPengguna.value.desa == '-' ||
                    controller.detailPengguna.value.kecamatan == '-') {
                  toastPesan("Profil", "Mohon lengkapi dahulu data pribadi anda sebelum membuat laporan.");
                  controller.editProfil();
                  return;
                }

                Get.to(() => LaporanPage(), binding: LaporanBinding(), arguments: ['LaporanById']);
              },
            ),
            const SizedBox(width: 10.0),
            MainMenuButton(
              hero: "hr_lokasi",
              text: "Lokasi Penting",
              color: Colors.white,
              borderColor: Colors.blue,
              icon: Icons.add_location_outlined,
              iconSize: 32.0,
              width: 55.0,
              height: 55.0,
              onTap: () {
                Get.to(() => LokasiPage(), binding: LokasiBinding());
              },
            ),
            const SizedBox(width: 10.0),
            /*
            const SizedBox(width: 10.0),
            MainMenuButton(
              hero: "OK",
              text: "Daftar Laporan",
              color: Colors.white,
              borderColor: Colors.green,
              icon: Icons.checklist_rtl_outlined,
              iconSize: 32.0,
              width: 55.0,
              height: 55.0,
              onTap: () {
                Get.to(() => DaftarLaporanPage(), binding: LaporanBinding(), arguments: ['LaporanAll']);
              },
            ),
            */
            MainMenuButton(
              hero: "hr_aturan",
              text: "Informasi Aturan",
              color: Colors.white,
              borderColor: Colors.purple,
              icon: Icons.local_library_outlined, // Icons.book_outlined,
              iconSize: 32.0,
              width: 55.0,
              height: 55.0,
              onTap: () {
                Get.to(() => AturanPage(), binding: InfoBinding());
                //toastPesan("SI-TIMOU 119", "Fasilitas belum tersedia.");
              },
            ),
            const SizedBox(width: 10.0),
            MainMenuButton(
              hero: "hr_berita",
              text: "Berita",
              color: Colors.white,
              borderColor: Colors.red,
              icon: Icons.newspaper_outlined,
              iconSize: 32.0,
              width: 55.0,
              height: 55.0,
              onTap: () {
                toastPesan("SI-TIMOU 119", "Fasilitas belum tersedia.");
              },
            ),
          ],
        ),
      ],
    ),
  );
}

Widget berita() {
  return SizedBox(
    width: double.infinity,
    child: Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        Row(
          crossAxisAlignment: CrossAxisAlignment.center,
          mainAxisAlignment: MainAxisAlignment.spaceBetween,
          children: [
            const Text(
              "Berita",
              style: TextStyle(
                fontSize: 16.0,
                color: Colors.black,
                fontWeight: FontWeight.w500,
              ),
              maxLines: 1,
              overflow: TextOverflow.ellipsis,
            ),
            InkWell(
              onTap: () {},
              child: const Icon(
                Icons.refresh,
                size: 24.0,
                color: Colors.grey,
              ),
            )
          ],
        ),
        const SizedBox(height: 15.0),
      ],
    ),
  );
}
