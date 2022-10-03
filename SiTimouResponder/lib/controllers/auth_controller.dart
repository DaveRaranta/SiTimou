import 'package:get/get.dart';
import 'package:flutter/material.dart';

import 'package:sitimoufr/controllers/bindings/bindings.dart';
import 'package:sitimoufr/helper/apphelper.dart';
import 'package:sitimoufr/helper/ui_dialogs.dart';
import 'package:sitimoufr/helper/ui_toast.dart';
import 'package:sitimoufr/services/auth_services.dart';
import 'package:sitimoufr/views/home/home.dart';
import 'package:sitimoufr/views/login.dart';
import 'package:sitimoufr/views/no_internet.dart';
import 'package:sitimoufr/widgets/dialog/progress_dialog.dart';
import 'package:sitimoufr/helper/globals.dart' as g;
//import 'package:mobile_number/mobile_number.dart';

// import 'package:sitimou/helper/globals.dart' as g;

class AuthController extends GetxController {
  var isLoading = true.obs;
  var isListLoading = false.obs;
  var isDataValid = false.obs;
  var nomorTelp = "".obs;

  // Arguments,
  //var args = Get.arguments;

  // Text Controller
  TextEditingController textLogin = TextEditingController();
  TextEditingController textPwd = TextEditingController();

  @override
  void onClose() async {
    // TODO: implement onClose
    textLogin.dispose();
    textPwd.dispose();

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
            textLogin.text = "";
            textPwd.text = "";

            // TODO: Uncomment ini jika FCM sudah ada

            await AuthServices.updateFcmToken(g.userFcmToken);
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
    if (textLogin.text.isEmpty) {
      toastPesan("LOGIN", "Masukan NIP anda.");
      return;
    }

    if (textPwd.text.isEmpty) {
      toastPesan("LOGIN", "Masukan Password anda.");
      return;
    }

    ProgressDialog pd = progressDialog(context, "Harap Menunggu...");
    await pd.show();

    var result = await AuthServices.masuk(textLogin.text, textPwd.text);

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
        textLogin.text = "";
        textPwd.text = "";

        // TODO: Uncomment ini jika FCM sudah ada

        await AuthServices.updateFcmToken(g.userFcmToken);
        Get.off(() => HomePage(), binding: HomeBinding());
      }
    } else {
      Get.off(() => LoginPage(), binding: AuthBinding());
    }
  }

  //
  // === NOT USE ===
  //

}
