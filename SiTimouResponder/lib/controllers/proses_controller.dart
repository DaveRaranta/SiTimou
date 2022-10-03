import 'dart:io';

import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:image_cropper/image_cropper.dart';
import 'package:image_picker/image_picker.dart';
import 'package:sitimoufr/controllers/bindings/bindings.dart';
import 'package:sitimoufr/helper/colors.dart';
import 'package:sitimoufr/helper/ui_dialogs.dart';
import 'package:sitimoufr/helper/ui_toast.dart';
import 'package:sitimoufr/models/alamat_osm.dart';
import 'package:sitimoufr/models/daftar_laporan.dart';
import 'package:sitimoufr/models/daftar_riwayat_proses.dart';
import 'package:sitimoufr/models/detail_laporan.dart';
import 'package:sitimoufr/models/detail_panik.dart';
import 'package:sitimoufr/services/lokasi_services.dart';
import 'package:sitimoufr/services/proses_services.dart';
import 'package:sitimoufr/views/proses/components/proses_laporan.dart';
import 'package:sitimoufr/views/proses/components/proses_panik.dart';
import 'package:sitimoufr/views/proses/components/riwayat_proses.dart';
import 'package:sitimoufr/widgets/dialog/progress_dialog.dart';

class ProsesController extends GetxController {
  var isLoading = true.obs;
  var isListLoading = false.obs;
  var listLaporan = <DaftarLaporan?>[].obs;
  var listRiwayatProses = <DaftarRiwayatProses?>[].obs;
  var detailLaporan = DetailLaporan().obs;
  var detailPanik = DetailPanik().obs;
  var alamatOsm = AlamatOpenSreetMap().obs;
  var statusTerima = false.obs;

  var jenisLaporan = "";
  var idRiwayat = "";
  int disposisiId = 0;

  // Textfield
  TextEditingController textJudulLaporan = TextEditingController();
  TextEditingController textUraianLaporan = TextEditingController();

  // Image Picker
  ImagePicker imagePicker = ImagePicker();
  var imageFile = "".obs;

  // Gps Stuff
  var alamatGps = "".obs;
  double gpsLat = 0;
  double gpsLng = 0;

  // Filter Stuff
  // TextEditingController textCari = TextEditingController();
  var listRiwayatProsesFilter = <DaftarRiwayatProses?>[];

  @override
  void onInit() {
    // TODO: implement onInit
    super.onInit();

    getListLaporan();
  }

  @override
  void onClose() {
    // TODO: implement onClose

    textJudulLaporan.dispose();
    textUraianLaporan.dispose();

    super.onClose();
  }

  //
  // Daftar Laporan Masuk
  //

  Future<void> getListLaporan() async {
    try {
      isListLoading(true);
      var result = await ProsesServices.getListLaporan();
      if (listLaporan.isNotEmpty) listLaporan.clear();
      listLaporan.assignAll(result);
    } finally {
      isListLoading(false);
    }
  }

  //
  // Daftar Riwayat Laporan
  //
  void pilihRiwayatLaporan(BuildContext context) {
    showModalBottomSheet(
      context: context,
      builder: (context) {
        return SafeArea(
          child: Column(
            mainAxisSize: MainAxisSize.min,
            children: [
              ListTile(
                leading: const Icon(
                  Icons.assignment_outlined,
                  size: 32.0,
                  color: Colors.blue,
                ),
                title: const Text("Riwayat Proses Laporan"),
                onTap: () async {
                  debugPrint("LOAD RIWAYAT LAPORAN");

                  Navigator.pop(context);

                  idRiwayat = "8";

                  // Load Data
                  ProgressDialog pd = progressDialog(context, "Harap Menunggu...");
                  await pd.show();

                  await getListRiwayatProses();

                  if (pd.isShowing()) pd.hide();

                  Get.to(() => RiwayatLaporanPage(jenisLaporan: "1"), binding: ProsesBinding());
                },
              ),
              ListTile(
                leading: const Icon(
                  Icons.assignment_late_outlined,
                  size: 32.0,
                  color: Colors.red,
                ),
                title: const Text("Riwayat Proses Panik"),
                onTap: () async {
                  debugPrint("LOAD RIWAYAT PANIK");

                  Navigator.pop(context);

                  idRiwayat = "9";

                  // Load Data
                  ProgressDialog pd = progressDialog(context, "Harap Menunggu...");
                  await pd.show();

                  await getListRiwayatProses();

                  if (pd.isShowing()) pd.hide();

                  Get.to(() => RiwayatLaporanPage(jenisLaporan: "2"), binding: ProsesBinding());
                },
              )
            ],
          ),
        );
      },
    );
  }

  Future<void> getListRiwayatProses() async {
    try {
      isListLoading(true);
      var result = await ProsesServices.getListRiwayatProses(idRiwayat);
      if (listRiwayatProses.isNotEmpty) listRiwayatProses.clear();
      listRiwayatProses.assignAll(result);
      listRiwayatProsesFilter = listRiwayatProses;
    } finally {
      isListLoading(false);
    }
  }

  void filterRiwayatProses(String textFilter) {
    // var result = <DaftarLokasi?>[];
    if (textFilter == "") {
      listRiwayatProsesFilter = listRiwayatProses;
    } else {
      listRiwayatProsesFilter = listRiwayatProses.where((p) => p!.namaPelapor.toString().toLowerCase().contains(textFilter.toLowerCase())).toList();
    }
  }

  //
  // Detail Laporan / Panik
  //

  void getDetailLaporan(BuildContext context, String jenisLaporan, int laporanId) async {
    // Load data detail surat masuk
    // Show Progrress

    dynamic result;

    ProgressDialog pd = progressDialog(context, "Harap Menunggu...");
    await pd.show();

    if (jenisLaporan == "1") {
      result = await ProsesServices.detailLaporan(laporanId);

      if (result == null) {
        if (pd.isShowing()) pd.hide();
        toastPesan("LAPORAN", "Gagal ambil data Laporan.");
        return;
      }

      detailLaporan.value = result;
      gpsLat = double.parse(detailLaporan.value.gpsLat.toString());
      gpsLng = double.parse(detailLaporan.value.gpsLng.toString());

      if (pd.isShowing()) pd.hide();

      Get.to(() => ProsesLaporanPage());
      //
    } else if (jenisLaporan == "2") {
      //
      result = await ProsesServices.detailPanik(laporanId);

      if (result == null) {
        if (pd.isShowing()) pd.hide();
        toastPesan("PANIK", "Gagal ambil data Panik.");
        return;
      }

      detailPanik.value = result;
      gpsLat = double.parse(detailPanik.value.gpsLat.toString());
      gpsLng = double.parse(detailPanik.value.gpsLng.toString());

      await getAlamatFromGps();

      if (pd.isShowing()) pd.hide();

      Get.to(() => ProsesPanikPage());
    }
  }

  Future<void> getAlamatFromGps() async {
    var result = await LokasiServices.getAlamatOsm(detailPanik.value.gpsLat!, detailPanik.value.gpsLng!);

    alamatOsm.value = result!;

    debugPrint(alamatOsm.value.displayName);
  }

  //
  // TERIMA LAPORAN
  //
  void terimaLaporan(BuildContext context) async {
    debugPrint(disposisiId.toString());

    questionBox(
      context,
      "Terima Laporan",
      "Terima laporan ini?",
      () async {
        Navigator.pop(context);

        ProgressDialog pd = progressDialog(context, "Harap Menunggu...");
        await pd.show();

        var result = await ProsesServices.terimaLaporan(disposisiId);

        if (!result) {
          if (pd.isShowing()) pd.hide();
          toastPesan("HAPUS", "Gagal batal laporan.");
          statusTerima(false);
          return;
        }

        // Update status terima
        statusTerima(true);
        // Refresh list
        await getListLaporan();

        if (pd.isShowing()) pd.hide();
      },
    );
    return;
  }

  //
  // BATAL LAPORAN
  //
  void tolakLaporan(BuildContext context) async {
    questionBox(
      context,
      "Batal Laporan",
      "Batal laporan ini?",
      () async {
        Navigator.pop(context);

        ProgressDialog pd = progressDialog(context, "Harap Menunggu...");
        await pd.show();

        var result = await ProsesServices.tolakLaporan(int.parse(detailLaporan.value.laporanId.toString()));

        if (!result) {
          if (pd.isShowing()) pd.hide();
          toastPesan("HAPUS", "Gagal batal laporan.");
          return;
        }

        // Refresh list
        await getListLaporan();

        if (pd.isShowing()) pd.hide();

        Get.back();
      },
    );
    return;
  }

  //
  // SIMPAN LAPORAN
  //

  void prosesLaporan(BuildContext context, String jenisLaporan, String status) async {
    // Valiasi input

    if (disposisiId == 0) {
      toastPesan("PROSES", "Gagal ambil ID disposisi laporan.");
      return;
    }

    if (textUraianLaporan.text.isEmpty || textJudulLaporan.text.isEmpty) {
      toastPesan("PROSES", "Judul dan isi laporan tidak boleh kosong.");
      return;
    }

    if (imageFile.isEmpty) {
      toastPesan("PROSES", "Lampirkan foto untuk proses laporan anda.");
      return;
    }

    // Let's Save
    questionBox(
      context,
      status == "B" ? "Batal Laporan" : "Proses Laporan",
      status == "B" ? "Batal laporan ini?" : "Proses dan simpan laporan ini?",
      () async {
        Navigator.pop(context);

        ProgressDialog pd = progressDialog(context, "Harap Menunggu...");
        await pd.show();

        var result = await ProsesServices.prosesLapaporan(
          jenisLaporan,
          textJudulLaporan.text.trim(),
          textUraianLaporan.text.trim(),
          status,
          File.fromUri(Uri.parse(imageFile.value)),
          disposisiId,
          jenisLaporan == "1" ? int.parse(detailLaporan.value.laporanId.toString()) : int.parse(detailPanik.value.laporanId.toString()),
        );

        if (!result) {
          if (pd.isShowing()) pd.hide();
          toastPesan("PROSES", "Gagal proses laporan.");
          return;
        }

        // Cleanup
        textJudulLaporan.text = "";
        textUraianLaporan.text = "";
        disposisiId = 0;

        // Refresh and back
        await getListLaporan();
        Get.back();
        Get.back();
      },
    );
    return;
  }

  //
  // Image Picker
  //

  void pickImage(BuildContext context) {
    showModalBottomSheet(
      context: context,
      builder: (context) {
        return SafeArea(
          child: Column(
            mainAxisSize: MainAxisSize.min,
            children: [
              ListTile(
                leading: const Icon(Icons.camera),
                title: const Text("Kamera"),
                onTap: () {
                  Navigator.pop(context);
                  cropImage(ImageSource.camera);
                },
              ),
              ListTile(
                leading: const Icon(Icons.photo_album),
                title: const Text("Galeri"),
                onTap: () {
                  Navigator.pop(context);
                  cropImage(ImageSource.gallery);
                },
              )
            ],
          ),
        );
      },
    );
  }

  void cropImage(ImageSource imageSource) async {
    //CroppedFile croppedFile;
    XFile? file = await imagePicker.pickImage(source: imageSource);

    if (file == null) return;

    isLoading(true);

    // Crop image dan set 'imageFile'
    final cropFile = await ImageCropper().cropImage(sourcePath: file.path, compressFormat: ImageCompressFormat.jpg, compressQuality: 75, uiSettings: [
      AndroidUiSettings(
        toolbarTitle: 'Edit Foto',
        toolbarColor: primaryColor,
        toolbarWidgetColor: Colors.black,
        initAspectRatio: CropAspectRatioPreset.original,
        lockAspectRatio: false,
      ),
      IOSUiSettings(
        title: "Edit Foto",
      ),
    ]);

    if (cropFile == null) {
      isLoading(false);
      return;
    }

    imageFile.value = cropFile.path.toString();

    debugPrint("cropFile: ${cropFile.path.toString()}");
  }
}
