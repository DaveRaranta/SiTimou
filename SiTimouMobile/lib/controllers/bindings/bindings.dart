import 'package:get/get.dart';
import 'package:sitimou/controllers/auth_controller.dart';
import 'package:sitimou/controllers/home_controller.dart';
import 'package:sitimou/controllers/info_controller.dart';
import 'package:sitimou/controllers/laporan_controller.dart';
import 'package:sitimou/controllers/lokasi_controller.dart';

class AuthBinding implements Bindings {
  @override
  void dependencies() {
    //Get.put<AuthController>(AuthController());
    Get.lazyPut<AuthController>(() => AuthController());
  }
}

class HomeBinding implements Bindings {
  @override
  void dependencies() {
    //Get.put<AuthController>(AuthController());
    Get.lazyPut<HomeController>(() => HomeController());
  }
}

class LokasiBinding implements Bindings {
  @override
  void dependencies() {
    //Get.put<AuthController>(AuthController());
    Get.lazyPut<LokasiController>(() => LokasiController());
  }
}

class LaporanBinding implements Bindings {
  @override
  void dependencies() {
    //Get.put<AuthController>(AuthController());
    Get.lazyPut<LaporanController>(() => LaporanController());
  }
}

class InfoBinding implements Bindings {
  @override
  void dependencies() {
    //Get.put<AuthController>(AuthController());
    Get.lazyPut<InfoController>(() => InfoController());
  }
}
