import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:sitimoufr/helper/ui_dialogs.dart';
import 'package:sitimoufr/helper/ui_toast.dart';
import 'package:sitimoufr/models/daftar_aturan.dart';
import 'package:sitimoufr/models/detail_aturan.dart';
import 'package:sitimoufr/services/info_services.dart';
import 'package:sitimoufr/views/aturan/components/detail_aturan.dart';
import 'package:sitimoufr/widgets/dialog/progress_dialog.dart';

class InfoController extends GetxController {
  var isLoading = true.obs;
  var isListLoading = false.obs;
  var listAturan = <DaftarAturan?>[].obs;
  var detailAturan = DetailAturan().obs;

  // Filter
  TextEditingController textCari = TextEditingController();
  var listAturanFilter = <DaftarAturan?>[];

  @override
  void onInit() {
    // TODO: implement onInit
    super.onInit();

    getListAturan();
  }

  @override
  void onClose() {
    // TODO: implement onClose
    super.onClose();
  }

  //
  // Daftar Aturan
  //

  Future<void> getListAturan() async {
    try {
      isListLoading(true);
      var result = await InfoServices.getListAturan();
      if (listAturan.isNotEmpty) listAturan.clear();
      listAturan.assignAll(result);
      listAturanFilter = listAturan;
    } finally {
      isListLoading(false);
    }
  }

  void filterListAturan(String textFilter) {
    // var result = <DaftarLokasi?>[];
    if (textFilter == "") {
      listAturanFilter = listAturan;
    } else {
      listAturanFilter = listAturan.where((p) => p!.namaAturan.toString().toLowerCase().contains(textFilter.toLowerCase())).toList();
    }
  }

  //
  // Detail Aturan
  //

  void getDetailAturan(BuildContext context, int aturanId) async {
    // Load data detail surat masuk
    // Show Progrress

    ProgressDialog pd = progressDialog(context, "Harap Menunggu...");
    await pd.show();

    var result = await InfoServices.getDetailLokasi(aturanId);

    if (pd.isShowing()) pd.hide();

    if (result == null) {
      toastPesan("LOKASI", "Gagal ambil data Lokasi.");
      return;
    }

    detailAturan.value = result;

    Get.to(() => DetailAturanPage());
  }
}
