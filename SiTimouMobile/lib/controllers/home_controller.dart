import 'dart:io';
import 'package:flutter/services.dart';
import 'package:get/get.dart';
import 'package:flutter/material.dart';
import 'package:image_cropper/image_cropper.dart';
import 'package:image_picker/image_picker.dart';
import 'package:fluentui_system_icons/fluentui_system_icons.dart';
import 'package:intl/date_symbol_data_local.dart';
import 'package:intl/intl.dart';

import 'package:sitimou/controllers/bindings/bindings.dart';
import 'package:sitimou/helper/apphelper.dart';
import 'package:sitimou/helper/colors.dart';
import 'package:sitimou/helper/ui_dialogs.dart';
import 'package:sitimou/helper/ui_toast.dart';
import 'package:sitimou/models/daftar_desa.dart';
import 'package:sitimou/models/daftar_kecamatan.dart';
import 'package:sitimou/models/detail_pengguna.dart';
import 'package:sitimou/services/auth_services.dart';
import 'package:sitimou/services/home_services.dart';
import 'package:sitimou/views/auth/login.dart';

import 'package:sitimou/helper/globals.dart' as g;
import 'package:sitimou/views/home/components/ubah_profil.dart';
import 'package:sitimou/widgets/dialog/progress_dialog.dart';
import 'package:mobile_number/mobile_number.dart';

class HomeController extends GetxController {
  var isLoading = true.obs;
  var isListLoading = false.obs;
  var detailPengguna = DetailPengguna().obs;
  var infoPengumuman = "".obs;
  var listKecamatan = <DaftarKecamatan?>[].obs;
  var listDesa = <DaftarDesa?>[].obs;

  // Page Controller
  PageController? pageController = PageController();
  var pageIndex = 0.obs;

  // Image Picker
  ImagePicker imagePicker = ImagePicker();
  var imageFile = "".obs;

  // Text Controler
  TextEditingController textEditNik = TextEditingController();
  TextEditingController textEditNama = TextEditingController();
  TextEditingController textEditAlamat = TextEditingController();
  TextEditingController textEditNoTelp = TextEditingController();

  // Profil
  List jkItems = [
    'LAKI-LAKI',
    'PEREMPUAN',
  ];
  var jkValue = "".obs;
  var idDesa = 0.obs;
  var idKecamatan = 0.obs;
  var tanggalLahir = "".obs;
  var noTelp = "".obs;

  @override
  void onInit() {
    // TODO: implement onInit
    super.onInit();

    getDetailPengguna();
    getInfoPengumuman();
  }

  //
  // PageView
  //
  final iconList = <IconData>[
    FluentIcons.home_12_regular,
    FluentIcons.person_12_regular,
  ];

  onPageChanged(int index) {
    switch (pageIndex.value) {
      case 0:
        break;
      case 1:
        break;
      default:
    }
    pageIndex.value = index;
  }

  //
  // Info
  //

  void getDetailPengguna() async {
    try {
      isLoading(true);
      var result = await HomeServices.getDetailPengguna();
      detailPengguna.value = result!;
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

  //
  // PROFIL
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
                  simpanFotoProfil(ImageSource.camera);
                },
              ),
              ListTile(
                leading: const Icon(Icons.photo_album),
                title: const Text("Galeri"),
                onTap: () {
                  Navigator.pop(context);
                  simpanFotoProfil(ImageSource.gallery);
                },
              )
            ],
          ),
        );
      },
    );
  }

  void simpanFotoProfil(ImageSource imageSource) async {
    XFile? file = await imagePicker.pickImage(source: imageSource);

    if (file == null) return;

    isLoading(true);

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

    // imageFile.value = cropFile.path.toString();

    // Simpan foto
    var fn = File.fromUri(Uri.parse(cropFile.path.toString()));
    var result = await HomeServices.gantiFotoProfil(fn);

    if (!result) {
      toastPesan("FOTO PROFIL", "Gagal perbaharui foto profil.");
      return;
    }

    // Reset
    await AppHelper.deleteImageFromCache("${g.apiUrl}/home/foto_profil_pengguna/${g.userId}");
    isLoading(false);
  }

  //
  // Edit Profil
  //

  Future getNomorTelp() async {
    // cek jika platform == android
    // == Implement nanti ==
    // Get Permission
    MobileNumber.listenPhonePermission((isGranted) {
      if (isGranted == false) {
        return;
      }
    });

    // Get nomor
    if (!await MobileNumber.hasPhonePermission) {
      await MobileNumber.requestPhonePermission;
      return;
    }

    try {
      noTelp.value = (await MobileNumber.mobileNumber)!;
      debugPrint("Nomor Telp: '${noTelp.value}'");
    } on PlatformException catch (e) {
      debugPrint("Gagal ambil nomor telp: '${e.message}'");
    }
  }

  void getListKecamatan() async {
    var result = await HomeServices.getListKecamatan();
    if (listKecamatan.isNotEmpty) listKecamatan.clear();
    listKecamatan.assignAll(result);
  }

  void getListDesa(int idKecamatan) async {
    var result = await HomeServices.getListDesa(idKecamatan);
    if (listDesa.isNotEmpty) listDesa.clear();
    listDesa.assignAll(result);
  }

  void editProfil() async {
    var jk = detailPengguna.value.jenisKelamin.toString().toUpperCase();
    var tgl = detailPengguna.value.rawTanggalLahir.toString().toUpperCase();
    // Prep Textbox
    textEditNik.text = detailPengguna.value.nik.toString();
    textEditNama.text = detailPengguna.value.namaLengkap.toString().toUpperCase();
    textEditAlamat.text = detailPengguna.value.alamat.toString().toUpperCase();
    jkValue.value = jk == "-" ? "" : jk;
    tanggalLahir.value = tgl == "-" ? "" : tgl;
    getListKecamatan();

    if (detailPengguna.value.noTelp.toString() == '-') {
      await getNomorTelp();
      textEditNoTelp.text = noTelp.value;
    } else {
      textEditNoTelp.text = detailPengguna.value.noTelp.toString();
    }

    // Navigate
    Get.to(() => UbahProfilPage(), binding: HomeBinding(), transition: Transition.fade);
  }

  void jkSelected(String value) {
    jkValue.value = value;
  }

  String formatTanggalLahir() {
    if (tanggalLahir.value == "") return "Pilih tanggal";
    initializeDateFormatting();
    var f = DateFormat.yMMMMEEEEd('id_ID');
    return f.format(DateTime.parse(tanggalLahir.toString()));
  }

  void updateProfil(BuildContext context) async {
    // Validasi
    if (textEditNik.text.isEmpty) {
      toastPesan("PROFIL", "NIK tidak boleh kosong.");
      return;
    }

    if (textEditNama.text.isEmpty) {
      toastPesan("PROFIL", "Nama Pengguna tidak boleh kosong.");
      return;
    }

    if (jkValue.value.isEmpty) {
      toastPesan("PROFIL", "Pilih jenis kelamin.");
      return;
    }

    if (tanggalLahir.value.isEmpty) {
      toastPesan("PROFIL", "Pilih tanggal lahir.");
      return;
    }

    if (textEditAlamat.text.isEmpty || textEditAlamat.text == '-') {
      toastPesan("PROFIL", "Alamat tidak boleh kosong.");
      return;
    }

    if (idDesa.value == 0) {
      toastPesan("PROFIL", "Pilih Desa.");
      return;
    }

    if (idKecamatan.value == 0) {
      toastPesan("PROFIL", "Pilih Kecamatan.");
      return;
    }

    if (textEditNoTelp.text.isEmpty) {
      toastPesan("PROFIL", "Nomor Telp tidak boleh kosong.");
      return;
    }

    if (textEditNoTelp.text.length < 11) {
      toastPesan("PROFIL", "Nomor Telp belum lengkap.");
      return;
    }

    debugPrint("ALL INPUT IS OK");

    //return;

    ProgressDialog pd = progressDialog(context, "Harap Menunggu...");
    await pd.show();

    var result = await HomeServices.updateProfil(textEditNik.text.trim(), textEditNama.text.trim(), tanggalLahir.value, jkValue.value,
        textEditAlamat.text.trim(), idDesa.value, idKecamatan.value, textEditNoTelp.text.trim());

    if (!result) {
      toastPesan("PROFIL", "Gagal update profil pengguna.");
      return;
    }

    getDetailPengguna();

    if (pd.isShowing()) pd.hide();

    Get.back();
  }
}
