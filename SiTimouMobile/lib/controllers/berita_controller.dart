import 'package:flutter/material.dart';
import 'package:get/get.dart';

import 'package:sitimou/helper/ui_dialogs.dart';
import 'package:sitimou/helper/ui_toast.dart';
import 'package:sitimou/models/daftar_berita.dart';
import 'package:sitimou/models/detail_berita.dart';
import 'package:sitimou/widgets/dialog/progress_dialog.dart';
import 'package:sitimou/services/info_services.dart';

class BeritaController extends GetxController {
  var isLoading = true.obs;
  var isListLoading = false.obs;
  var listBerita = <DaftarBerita?>[].obs;
  var detailBerita = DetailBerita().obs;

  @override
  void onInit() {
    // TODO: implement onInit
    super.onInit();

    getBeritaKota();
  }

  @override
  void onClose() {
    // TODO: implement onClose
    super.onClose();
  }

  Future<void> getBeritaKota() async {
    try {
      isListLoading(true);
      var result = await InfoServices.getListBerita();
      if (listBerita.isNotEmpty) listBerita.clear();
      listBerita.assignAll(result);
    } finally {
      isListLoading(false);
    }
  }

  void getDetailBeritaKota(BuildContext context, int idBerita) async {
    // Load data detail surat masuk
    // Show Progrress

    ProgressDialog pd = progressDialog(context, "Harap Menunggu...");
    await pd.show();

    var result = await InfoServices.getDetailBeritaKota(idBerita);

    if (pd.isShowing()) pd.hide();

    if (result == null) {
      toastPesan("BERITA", "Gagal ambil data Berita Kota.");
      return;
    }

    detailBerita.value = result;
    //Get.to(() => DetailBerita());
  }
}
