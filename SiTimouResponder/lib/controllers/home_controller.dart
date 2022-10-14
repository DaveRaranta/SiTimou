import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:sitimoufr/helper/apphelper.dart';
import 'package:sitimoufr/helper/ui_dialogs.dart';
import 'package:sitimoufr/helper/ui_toast.dart';
import 'package:sitimoufr/models/daftar_berita.dart';
import 'package:sitimoufr/models/detail_pegawai.dart';
import 'package:sitimoufr/services/auth_services.dart';
import 'package:sitimoufr/services/home_services.dart';
import 'package:sitimoufr/services/info_services.dart';
import 'package:sitimoufr/views/login.dart';
import 'package:sitimoufr/helper/globals.dart' as g;

class HomeController extends GetxController {
  var isLoading = true.obs;
  var isListLoading = false.obs;
  var infoPengumuman = "".obs;
  var detailPegawai = DetailPegawai().obs;
  var listBerita = <DaftarBerita?>[].obs;

  @override
  void onInit() async {
    // TODO: implement onInit
    super.onInit();

    await AppHelper.deleteImageFromCache("${g.apiUrl}/home/foto_profil_pegawai");
    getDetailPegawai();
    getInfoPengumuman();
    getRecentBerita();
  }

  @override
  void onClose() {
    // TODO: implement onClose
    super.onClose();
  }

  //
  // Info
  //

  void getDetailPegawai() async {
    try {
      isLoading(true);
      var result = await HomeServices.getDetailPegawai();
      if (result == null) return;
      detailPegawai.value = result;
    } on Exception catch (e) {
      debugPrint("[!] DebugInfo: [ERROR] HomeController.getDetailPegawai() => ${e.toString()}");
      return;
    } finally {
      isLoading(false);
    }
  }

  void getInfoPengumuman() async {
    try {
      isLoading(true);
      var result = await HomeServices.getInfoPengumuman();
      infoPengumuman.value = result;
    } finally {
      isLoading(false);
    }
  }

  void getRecentBerita() async {
    try {
      isListLoading(true);
      var result = await InfoServices.getListBerita();
      if (listBerita.isNotEmpty) listBerita.clear();
      listBerita.assignAll(result);
    } finally {
      isListLoading(false);
    }
  }

  //
  // Logout
  //

  void gantiPengguna(BuildContext context) async {
    questionBox(
      context,
      "Logoff",
      "Ganti Pengguna?",
      () async {
        var result = await AuthServices.keluar();

        if (!result) {
          toastPesan("LOGOUT", "Gagal ganti pengguna.");
          return;
        }

        Get.offAll(LoginPage());
      },
    );

    return;
  }
}
