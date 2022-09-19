import 'package:cached_network_image/cached_network_image.dart';
import 'package:fluentui_system_icons/fluentui_system_icons.dart';
import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:sitimou/controllers/bindings/bindings.dart';
import 'package:sitimou/controllers/home_controller.dart';
import 'package:sitimou/helper/colors.dart';
import 'package:sitimou/helper/scroll_settings.dart';

import 'package:sitimou/helper/globals.dart' as g;
import 'package:sitimou/views/home/components/ubah_foto.dart';
import 'package:sitimou/views/home/components/ubah_profil.dart';
import 'package:sitimou/widgets/textfield/fake_label_textfield.dart';

String fotoUrl = "${g.apiUrl}/home/foto_profil_pengguna";

class HomeProfilTab extends StatelessWidget {
  const HomeProfilTab({super.key});

  @override
  Widget build(BuildContext context) {
    return Container(
      color: primaryColor,
      height: double.infinity,
      width: double.infinity,
      child: Padding(
        padding: const EdgeInsets.only(top: 10.0, left: 18.0, right: 18.0),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            profilHeader(),
            const SizedBox(height: 15.0),
            Expanded(
              child: ScrollConfiguration(
                behavior: ScrollSettings(),
                child: SingleChildScrollView(
                  child: infoUser(),
                ),
              ),
            ),
          ],
        ),
      ),
    );
  }
}

Widget profilHeader() {
  final controller = Get.find<HomeController>();

  return Column(
    children: [
      Row(
        mainAxisAlignment: MainAxisAlignment.end,
        children: [
          GestureDetector(
            child: const Icon(
              FluentIcons.edit_24_regular,
              color: Colors.black54,
            ),
            onTap: () {
              controller.editProfil();
            },
          ),
          const SizedBox(width: 15.0),
          GestureDetector(
            child: const Icon(
              FluentIcons.password_24_regular,
              color: Colors.black54,
            ),
            onTap: () {
              //Get.to(() => UbahPassword());
              debugPrint("OK");
            },
          ),
        ],
      ),
      const SizedBox(height: 25.0),
      SizedBox(
        width: 96.0,
        height: 96.0,
        child: Hero(
          tag: "h_user_profile",
          child: GestureDetector(
            child: CircleAvatar(
              backgroundImage: CachedNetworkImageProvider("$fotoUrl/${g.userId}", headers: g.httpHeaders),
            ),
            onTap: () {
              Get.to(() => UbahFotoProfil(), binding: HomeBinding());
            },
          ),
        ),
      ),
      const SizedBox(height: 15.0),
      const Text(
        "Profil Pengguna",
        maxLines: 2,
        overflow: TextOverflow.ellipsis,
        style: TextStyle(
          color: Colors.blue,
          fontSize: 20.0,
          fontWeight: FontWeight.w600,
        ),
      ),
      const SizedBox(height: 10.0),
    ],
  );
}

Widget infoUser() {
  final controller = Get.find<HomeController>();
  return Column(
    crossAxisAlignment: CrossAxisAlignment.start,
    children: [
      const SizedBox(height: 15.0),
      Row(
        children: const [
          Icon(
            FluentIcons.contact_card_20_regular,
            color: Colors.black,
          ),
          SizedBox(
            width: 7.0,
          ),
          Text(
            "Info Pengguna",
            overflow: TextOverflow.ellipsis,
            style: TextStyle(
              color: Colors.black,
              fontSize: 15.0,
              fontWeight: FontWeight.w500,
            ),
          ),
        ],
      ),
      const SizedBox(height: 15.0),
      Obx(
        () => FakeTextFieldWithLabel(
          labelFontSize: 14.0,
          labelText: "Nama Pengguna",
          fakeText: controller.detailPengguna.value.namaLengkap.toString(),
        ),
      ),
      const SizedBox(height: 15.0),
      Obx(
        () => FakeTextFieldWithLabel(
          labelFontSize: 14.0,
          labelText: "Nik Pengguna",
          fakeText: controller.detailPengguna.value.nik.toString(),
        ),
      ),
      const SizedBox(height: 15.0),
      Obx(() {
        var tgl = "";

        if (controller.detailPengguna.value.tanggalLahir.toString() == '-') {
          tgl = controller.detailPengguna.value.tanggalLahir.toString();
        } else {
          tgl = "${controller.detailPengguna.value.tanggalLahir} (${controller.detailPengguna.value.umur} tahun)";
        }
        return FakeTextFieldWithLabel(
          labelFontSize: 14.0,
          labelText: "Tanggal Lahir",
          fakeText: tgl,
        );
      }),
      const SizedBox(height: 15.0),
      Obx(
        () => FakeTextFieldWithLabel(
          labelFontSize: 14.0,
          labelText: "Jenis Kelamin",
          fakeText: controller.detailPengguna.value.jenisKelamin.toString(),
        ),
      ),
      const SizedBox(height: 15.0),
      Obx(
        () => FakeTextFieldWithLabel(
          labelFontSize: 14.0,
          labelText: "Nomor Telp",
          fakeText: controller.detailPengguna.value.noTelp.toString(),
        ),
      ),
      const SizedBox(height: 15.0),
      Row(
        children: const [
          Icon(
            FluentIcons.contact_card_20_regular,
            color: Colors.black,
          ),
          SizedBox(
            width: 7.0,
          ),
          Text(
            "Domisisi",
            overflow: TextOverflow.ellipsis,
            style: TextStyle(
              color: Colors.black,
              fontSize: 15.0,
              fontWeight: FontWeight.w500,
            ),
          ),
        ],
      ),
      const SizedBox(height: 15.0),
      Obx(
        () => FakeTextFieldWithLabel(
          labelFontSize: 14.0,
          labelText: "Alamat",
          fakeText: controller.detailPengguna.value.alamat.toString(),
        ),
      ),
      const SizedBox(height: 15.0),
      Obx(
        () => FakeTextFieldWithLabel(
          labelFontSize: 14.0,
          labelText: "Desa",
          fakeText: controller.detailPengguna.value.desa.toString(),
        ),
      ),
      const SizedBox(height: 15.0),
      Obx(
        () => FakeTextFieldWithLabel(
          labelFontSize: 14.0,
          labelText: "Kecamatan",
          fakeText: controller.detailPengguna.value.alamat.toString(),
        ),
      ),
      const SizedBox(height: 100.0),
    ],
  );
}
