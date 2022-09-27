import 'package:get/get.dart';
import 'package:sitimoufr/controllers/auth_controller.dart';
import 'package:sitimoufr/controllers/home_controller.dart';
import 'package:sitimoufr/controllers/info_controller.dart';
import 'package:sitimoufr/controllers/proses_controller.dart';
import 'package:sitimoufr/controllers/lokasi_controller.dart';

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

class ProsesBinding implements Bindings {
  @override
  void dependencies() {
    //Get.put<AuthController>(AuthController());
    Get.lazyPut<ProsesController>(() => ProsesController());
  }
}

class InfoBinding implements Bindings {
  @override
  void dependencies() {
    //Get.put<AuthController>(AuthController());
    Get.lazyPut<InfoController>(() => InfoController());
  }
}
