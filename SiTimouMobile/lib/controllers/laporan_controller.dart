import 'dart:io';

import 'package:flutter/material.dart';
import 'package:geolocator/geolocator.dart';
import 'package:get/get.dart';
import 'package:image_picker/image_picker.dart';
import 'package:image_cropper/image_cropper.dart';

import 'package:sitimou/controllers/bindings/bindings.dart';
import 'package:sitimou/helper/colors.dart';
import 'package:sitimou/helper/ui_dialogs.dart';
import 'package:sitimou/helper/ui_toast.dart';
import 'package:sitimou/models/daftar_laporan.dart';
import 'package:sitimou/models/detail_laporan.dart';
import 'package:sitimou/models/detail_pengguna.dart';
import 'package:sitimou/services/home_services.dart';
import 'package:sitimou/services/laporan_services.dart';
import 'package:sitimou/views/laporan/components/detail_laporan.dart';
import 'package:sitimou/widgets/dialog/progress_dialog.dart';

class LaporanController extends GetxController {
  var isLoading = true.obs;
  var isListLoading = false.obs;
  var listLaporan = <DaftarLaporan?>[].obs;
  var detailLaporan = DetailLaporan().obs;
  var detailPengguna = DetailPengguna().obs;
  // var detailLokasi = DetailLokasi().obs;

  // Get Arguments
  var args = Get.arguments;

  // Filter
  TextEditingController textCari = TextEditingController();
  var listLaporanFilter = <DaftarLaporan?>[];

  // Buat Laporan
  TextEditingController textTentang = TextEditingController();
  TextEditingController textIsiLaporan = TextEditingController();

  // Image Picker
  ImagePicker imagePicker = ImagePicker();
  var imageFile = "".obs;

  // GPS
  Position? position;

  // Panic
  var isPanicPressed = false.obs;

  @override
  void onInit() {
    // TODO: implement onInit
    super.onInit();

    debugPrint("ARGS: ${args[0]}");
    if (args[0] == "LaporanById") {
      getListLaporanById();
    } else if (args[0] == "LaporanAll") {
      getListLaporanAll();
    }
  }

  @override
  void onClose() {
    // TODO: implement onClose

    textCari.dispose();
    textTentang.dispose();
    textIsiLaporan.dispose();

    super.onClose();
  }

  //
  // Helper
  //

  void cleanUp() {
    textCari.text = "";
    textTentang.text = "";
    textIsiLaporan.text = "";
    imageFile.value = "";
  }

  String statusLaporan(String status) {
    switch (status) {
      case "N":
        return "Belum Diproses";
      case "P":
        return "Sedang Diproses";
      case "S":
        return "Selesai Diproses";
      default:
        return "Tidak diketahui";
    }
  }

  IconData iconStatusLaporan(String status) {
    switch (status) {
      case "N":
        return Icons.priority_high_rounded;
      case "P":
        return Icons.hourglass_top_rounded;
      case "S":
        return Icons.check_rounded;
      default:
        return Icons.question_mark_rounded;
    }
  }

  Color colorStatusLaporan(String status) {
    switch (status) {
      case "N":
        return const Color.fromARGB(255, 234, 71, 111);
      case "P":
        return const Color.fromARGB(255, 249, 181, 54);
      case "S":
        return const Color.fromARGB(255, 45, 188, 164); //const Color.fromARGB(255, 117, 212, 143);
      default:
        return Colors.red;
    }
  }

  Future<Position> getGeoLocationPosition() async {
    bool serviceEnabled;
    LocationPermission permission;
    // Test if location services are enabled.
    serviceEnabled = await Geolocator.isLocationServiceEnabled();
    if (!serviceEnabled) {
      // Location services are not enabled don't continue
      // accessing the position and request users of the
      // App to enable the location services.
      await Geolocator.openLocationSettings();
      // return Future.error('Location services are disabled.');
      return Future.error('p0');
    }
    permission = await Geolocator.checkPermission();
    if (permission == LocationPermission.denied) {
      permission = await Geolocator.requestPermission();
      if (permission == LocationPermission.denied) {
        // Permissions are denied, next time you could try
        // requesting permissions again (this is also where
        // Android's shouldShowRequestPermissionRationale
        // returned true. According to Android guidelines
        // your App should show an explanatory UI now.
        return Future.error('p1');
      }
    }
    if (permission == LocationPermission.deniedForever) {
      // Permissions are denied forever, handle appropriately.
      // return Future.error('Location permissions are permanently denied, we cannot request permissions.');
      return Future.error('p2');
    }
    // When we reach here, permissions are granted and we can
    // continue accessing the position of the device.
    return await Geolocator.getCurrentPosition(desiredAccuracy: LocationAccuracy.best);
  }

  //
  // Daftar Laporan
  //

  Future<void> getListLaporanById() async {
    try {
      isListLoading(true);
      var result = await LaporanServices.getListLaporanId();
      if (listLaporan.isNotEmpty) listLaporan.clear();
      listLaporan.assignAll(result);
      listLaporanFilter = listLaporan;
    } finally {
      isListLoading(false);
    }
  }

  Future<void> getListLaporanAll() async {
    try {
      isListLoading(true);
      var result = await LaporanServices.getListLaporanAll();
      if (listLaporan.isNotEmpty) listLaporan.clear();
      listLaporan.assignAll(result);
      listLaporanFilter = listLaporan;
    } finally {
      isListLoading(false);
    }
  }

  void filterListLaporan(String textFilter) {
    // var result = <DaftarLokasi?>[];
    if (textFilter == "") {
      listLaporanFilter = listLaporan;
    } else {
      listLaporanFilter = listLaporan.where((p) => p!.tentang.toString().toLowerCase().contains(textFilter.toLowerCase())).toList();
    }
  }

  //
  // Detail Laporan
  //

  void getDetailLaporan(BuildContext context, int idLaporan) async {
    // Get laporan

    ProgressDialog pd = progressDialog(context, "Harap Menunggu...");
    await pd.show();

    if (pd.isShowing()) pd.hide();

    var result = await LaporanServices.getDetailLaporan(idLaporan);
    if (result == null) {
      toastPesan("LAPORAN", "Gagal buka detail laporan.");
      return;
    }

    detailLaporan.value = result;

    Get.to(() => DetailLaporanPage(), binding: LaporanBinding());
  }

  void hapusDetailLaporan(BuildContext context, String jenis) {
    questionBox(
      context,
      "Hapus Laporan",
      "Hapus laporan ini?",
      () async {
        var result = await LaporanServices.hapusLaporan(int.parse(detailLaporan.value.laporanId.toString()));

        if (!result) {
          toastPesan("HAPUS", "Gagal hapus laporan.");
          return;
        }

        if (jenis == "1") {
          await getListLaporanById();
        }

        Navigator.pop(context);
        Get.back();
      },
    );
    return;
  }

  //
  // Buat Laporan
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

  void simpanLaporan(BuildContext context, [bool mounted = true]) async {
    // Validasi input
    if (textTentang.text.isEmpty) {
      toastPesan("LAPORAN", "Isi Perihal laporan anda.");
      return;
    }

    if (textIsiLaporan.text.isEmpty) {
      toastPesan("LAPORAN", "Ketik isi laporan anda.");
      return;
    }

    if (imageFile.isEmpty) {
      toastPesan("LAPORAN", "Lampirkan file foto untuk laporan anda.");
      return;
    }

    try {
      // Ambil Posisi GPS

      position = await getGeoLocationPosition();

      // Cek posisi
      if (position!.isMocked) {
        toastPesan("LAPORAN", "Tidak bisa mengirim laporan karena 'Mock Location' sedang aktif");
        return;
      }

      if (position == null) throw Exception("p3");

      debugPrint("Lat: ${position!.latitude}, Lng: ${position!.longitude}");

      // Simpan data
      if (!mounted) return;
      ProgressDialog pd = progressDialog(context, "Harap Menunggu...");
      await pd.show();

      var result = await LaporanServices.kirimLaporan(
          textTentang.text.trim(), textIsiLaporan.text.trim(), position!.latitude, position!.longitude, File.fromUri(Uri.parse(imageFile.value)));

      if (!result) {
        if (pd.isShowing()) pd.hide();
        toastPesan("LAPORAN", "Gagal kirim laporan.");
        return;
      }

      // Celanup
      cleanUp();
      await getListLaporanById();
      if (pd.isShowing()) pd.hide();
      Get.back();

      //
      // Done
      //
    } on Exception catch (e) {
      String pesan;

      switch (e.toString()) {
        case "p0":
          pesan = "Mohon aktifkan deteksi Lokasi.";
          break;
        case "p1":
          pesan = "Ijinkan aplikasi untuk akses lokasi.";
          break;
        case "p2":
          pesan = "Gagal kirim laporan. Akses lokasi telah anda tolak secara permanen.";
          break;
        case "Exception: p3":
          pesan = "Tidak bisa mengakses lokasi anda.";
          break;
        default:
          pesan = "Gagal kirim laporan";
      }
      toastPesan("LAPORAN", pesan);
    }
    // Check GPS

    debugPrint(imageFile.toString());
  }

  //
  // PANIC
  //

  void onPanicTimerInitiated() async {
    // Ambil Info Pengguna
    debugPrint("onPanicTimerInitiated");
    var result = await HomeServices.getDetailPengguna();
    detailPengguna.value = result!;
  }

  void onPanicTimeout() async {
    try {
      position = await getGeoLocationPosition();

      // Cek posisi
      if (position!.isMocked) {
        toastPesan("LAPORAN", "Tidak bisa mengirim laporan karena 'Mock Location' sedang aktif");
        return;
      }

      if (position == null) throw Exception("p3");

      // Simpan panic
      var result = await LaporanServices.kirimPanik(position!.latitude, position!.longitude);

      if (!result) {
        toastPesan("PANIK", "Gagal kirim pesan Panik. Mohon cek koneksi.");
        return;
      }

      // Set Panic = true
      isPanicPressed(true);
      //
    } on Exception catch (e) {
      String pesan;

      switch (e.toString()) {
        case "p0":
          pesan = "Mohon aktifkan deteksi Lokasi.";
          break;
        case "p1":
          pesan = "Ijinkan aplikasi untuk akses lokasi.";
          break;
        case "p2":
          pesan = "Gagal kirim pesan Panik. Akses lokasi telah anda tolak secara permanen.";
          break;
        case "Exception: p3":
          pesan = "Tidak bisa mengakses lokasi anda.";
          break;
        default:
          pesan = "Gagal kirim pesan Panik";
      }
      toastPesan("LAPORAN", pesan);
    }
  }
}
