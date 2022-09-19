import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:get/get.dart';
import 'package:sitimou/views/home/home.dart';
//import 'package:mobile_number/mobile_number.dart';

import 'package:sitimou/controllers/bindings/bindings.dart';
import 'package:sitimou/helper/apphelper.dart';
import 'package:sitimou/helper/ui_dialogs.dart';
import 'package:sitimou/helper/ui_toast.dart';
import 'package:sitimou/services/auth_services.dart';
import 'package:sitimou/views/auth/login.dart';
import 'package:sitimou/views/common/no_internet.dart';
import 'package:sitimou/widgets/dialog/progress_dialog.dart';
// import 'package:sitimou/helper/globals.dart' as g;

class AuthController extends GetxController {
  var isLoading = true.obs;
  var isListLoading = false.obs;
  var isDataValid = false.obs;
  var nomorTelp = "".obs;

  // Arguments,
  //var args = Get.arguments;

  // Text Controller
  TextEditingController textNik = TextEditingController();
  TextEditingController textPwd = TextEditingController();
  // Daftar
  TextEditingController textRegNik = TextEditingController();
  TextEditingController textRegNama = TextEditingController();
  TextEditingController textRegPwd = TextEditingController();
  TextEditingController textRegConfirm = TextEditingController();

  @override
  void onClose() async {
    // TODO: implement onClose
    textNik.dispose();
    textPwd.dispose();

    textRegNik.dispose();
    textRegNama.dispose();
    textRegPwd.dispose();
    textRegConfirm.dispose();

    super.onClose();
  }

  // === SPLASH SCREEN ===

  Future validasiSplashScreen() async {
    Future.delayed(const Duration(seconds: 7), () async {
      // Cek Koneksi
      var cekKoneksi = await AppHelper().connectionChecker();

      if (cekKoneksi) {
        // Validasi JWT Token
        var authToken = await AuthServices.validasiAuthToken();
        if (authToken) {
          var validasiUser = await AuthServices.validasiPengguna();

          if (!validasiUser) {
            Get.off(() => LoginPage(), binding: AuthBinding());
          } else {
            // Celan Up
            textNik.text = "";
            textPwd.text = "";

            // TODO: Uncomment ini jika FCM sudah ada

            // await AuthServices.updateFcmToken(g.userFcmToken);
            Get.off(() => HomePage(), binding: HomeBinding());
          }
        } else {
          Get.off(() => LoginPage());
        }
      } else {
        Get.off(() => const ErrorKoneksiPage());
      }
    });
  }

  //
  // === LOGIN ===
  //

  void masuk(BuildContext context) async {
    if (textNik.text.isEmpty) {
      toastPesan("LOGIN", "Masukan NIK anda.");
      return;
    }

    if (textPwd.text.isEmpty) {
      toastPesan("LOGIN", "Masukan Password anda.");
      return;
    }

    ProgressDialog pd = progressDialog(context, "Harap Menunggu...");
    await pd.show();

    var result = await AuthServices.masuk(textNik.text, textPwd.text);

    if (pd.isShowing()) pd.hide();

    if (!result) {
      if (pd.isShowing()) pd.hide();
      return;
    }

    await validasiLogin();

    if (pd.isShowing()) pd.hide();
  }

  Future validasiLogin() async {
    var authToken = await AuthServices.validasiAuthToken();
    if (authToken) {
      var validasiUser = await AuthServices.validasiPengguna();

      if (!validasiUser) {
        Get.off(() => LoginPage(), binding: AuthBinding());
      } else {
        // Celan Up
        textNik.text = "";
        textPwd.text = "";

        // TODO: Uncomment ini jika FCM sudah ada

        // await AuthServices.updateFcmToken(g.userFcmToken);
        Get.off(() => HomePage(), binding: HomeBinding());
      }
    } else {
      Get.off(() => LoginPage(), binding: AuthBinding());
    }
  }

  //
  // === DAFTAR ===
  //

  void saveRegistrasi(BuildContext context) async {
    try {
      if (textRegNik.text.isEmpty) throw ("Isi NIK sesuai dengan KTP anda.");
      if (textRegNama.text.isEmpty) throw ("Isi Nama Lengkap anda sesuai KTP.");
      if (textRegPwd.text.isEmpty) throw ("Isi Password anda.");
      if (textRegNik.text.length < 16) throw ("NIK anda belum lengkap.");
      if (textRegPwd.text.length < 5) throw ("Panjang password minimal 5 huruf/angka");
      if (textRegPwd.text != textRegConfirm.text) throw ("Password yang anda masukan belum sama.");

      // Simpan
      ProgressDialog pd = progressDialog(context, "Harap Menunggu...");
      await pd.show();

      var result = await AuthServices.saveRegistrasiPengguna(textRegNik.text.trim(), textRegNama.text.trim(), textRegPwd.text);

      if (pd.isShowing()) pd.hide();

      if (!result) throw ("Gagal dafter pengguna. Coba kembali atau hubungi dinas terkait.");

      // CelanUp
      textRegNik.text = "";
      textRegNama.text = "";
      textRegPwd.text = "";
      textRegConfirm.text = "";

      Get.off(() => LoginPage(), binding: AuthBinding());
    } catch (e) {
      toastPesan("DAFTAR PENGGUNA", e.toString());
    }
  }

  //
  // === NOT USE ===
  //

}
