import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:sitimoufr/helper/ui_dialogs.dart';
import 'package:sitimoufr/helper/ui_toast.dart';
import 'package:sitimoufr/models/daftar_lokasi.dart';
import 'package:sitimoufr/models/detail_lokasi.dart';
import 'package:sitimoufr/services/lokasi_services.dart';
import 'package:sitimoufr/views/lokasi/components/detail_lokasi.dart';
import 'package:sitimoufr/widgets/dialog/progress_dialog.dart';
import 'package:url_launcher/url_launcher.dart';

class LokasiController extends GetxController {
  var isLoading = true.obs;
  var isListLoading = false.obs;
  var listLokasi = <DaftarLokasi?>[].obs;
  var detailLokasi = DetailLokasi().obs;

  // Filter
  TextEditingController textCari = TextEditingController();
  var listLokasiFilter = <DaftarLokasi?>[];

  // Map stuff

  @override
  void onInit() {
    // TODO: implement onInit
    super.onInit();

    getListLokasi();
  }

  @override
  void onClose() {
    // TODO: implement onClose

    textCari.dispose();

    super.onClose();
  }

  //
  // Daftar Lokasi
  //

  Future<void> getListLokasi() async {
    try {
      isListLoading(true);
      var result = await LokasiServices.getListLokasi();
      if (listLokasi.isNotEmpty) listLokasi.clear();
      listLokasi.assignAll(result);
      listLokasiFilter = listLokasi;
    } finally {
      isListLoading(false);
    }
  }

  void filterListLokasi(String textFilter) {
    // var result = <DaftarLokasi?>[];
    if (textFilter == "") {
      listLokasiFilter = listLokasi;
    } else {
      listLokasiFilter = listLokasi.where((p) => p!.namaLokasi.toString().toLowerCase().contains(textFilter.toLowerCase())).toList();
    }
  }

  //
  // Detail Lokasi
  //

  void getDetailLokasi(BuildContext context, int lokasiId) async {
    // Load data detail surat masuk
    // Show Progrress

    ProgressDialog pd = progressDialog(context, "Harap Menunggu...");
    await pd.show();

    var result = await LokasiServices.getDetailLokasi(lokasiId);

    if (pd.isShowing()) pd.hide();

    if (result == null) {
      toastPesan("LOKASI", "Gagal ambil data Lokasi.");
      return;
    }

    detailLokasi.value = result;

    Get.to(() => const DetailLokasiPage());
  }

  void callPhone() {
    // validasi nomor tlp
    var noTelp = detailLokasi.value.noTelp.toString();

    if (noTelp.length < 11) {
      toastPesan("PANGGIL", "Nomor telepon sepertinya belum lengkap.");
      return;
    }

    debugPrint(noTelp.substring(0, 2));

    if (noTelp.substring(0, 2) != "04" && noTelp.substring(0, 2) != "08") {
      toastPesan("PANGGIL", "Nomor telepon sepertinya belum benar.");
      return;
    }

    var uri = Uri.parse("tel://${detailLokasi.value.noTelp}");
    launchUrl(uri);
  }
}
