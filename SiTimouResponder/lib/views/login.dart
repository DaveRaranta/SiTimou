import 'package:get/get.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:double_back_to_close_app/double_back_to_close_app.dart';
import 'package:sitimoufr/controllers/auth_controller.dart';
import 'package:sitimoufr/helper/colors.dart';
import 'package:sitimoufr/helper/scroll_settings.dart';

class LoginPage extends StatelessWidget {
  LoginPage({Key? key}) : super(key: key);

  final AuthController _controller = Get.put(AuthController());

  @override
  Widget build(BuildContext context) {
    Size size = MediaQuery.of(context).size;

    return MediaQuery(
      data: MediaQuery.of(context).copyWith(textScaleFactor: 1.0),
      child: GestureDetector(
        onTap: () => FocusScope.of(context).unfocus(),
        child: Scaffold(
          backgroundColor: primaryColor,
          body: DoubleBackToCloseApp(
            snackBar: const SnackBar(
              content: Text("Tap tombol Kembali sekali lagi untuk keluar"),
            ),
            child: SafeArea(
              child: ScrollConfiguration(
                behavior: ScrollSettings(),
                child: SingleChildScrollView(
                  child: Padding(
                    padding: const EdgeInsets.only(left: 25.0, right: 25.0, top: 0, bottom: 0),
                    child: SizedBox(
                      width: double.infinity,
                      height: size.height,
                      child: Stack(
                        alignment: Alignment.center,
                        children: [
                          Positioned(
                            top: 10,
                            left: 0,
                            child: Row(
                              children: [
                                Image.asset(
                                  "assets/images/logo/logo_gov_512px.png",
                                  height: 50.0,
                                  filterQuality: FilterQuality.high,
                                  fit: BoxFit.fill,
                                ),
                                const SizedBox(width: 10.0),
                                Image.asset(
                                  "assets/images/logo/kementrian.png",
                                  height: 50.0,
                                  filterQuality: FilterQuality.high,
                                  fit: BoxFit.fill,
                                ),
                                const SizedBox(width: 10.0),
                              ],
                            ),
                          ),
                          Column(
                            mainAxisAlignment: MainAxisAlignment.center,
                            crossAxisAlignment: CrossAxisAlignment.start,
                            children: [
                              const SizedBox(height: 25.0),
                              Center(
                                child: Hero(
                                  tag: "login_app_logo",
                                  child: Image.asset(
                                    "assets/images/logo/app_logo_512px.png",
                                    height: 110.0,
                                    filterQuality: FilterQuality.high,
                                    fit: BoxFit.fill,
                                  ),
                                ),
                              ),
                              const SizedBox(height: 20.0),
                              const Center(
                                child: Text(
                                  "LOGIN",
                                  style: TextStyle(
                                    color: Colors.black87,
                                    fontSize: 25.0,
                                    fontWeight: FontWeight.w500,
                                  ),
                                ),
                              ),
                              const SizedBox(height: 35.0),
                              InputFields(),
                              const SizedBox(height: 50.0),
                              SizedBox(
                                width: double.infinity,
                                height: 50.0,
                                child: RawMaterialButton(
                                  onPressed: () {
                                    FocusScope.of(context).unfocus();
                                    _controller.masuk(context);
                                  },
                                  fillColor: const Color.fromARGB(255, 255, 59, 60),
                                  shape: RoundedRectangleBorder(
                                    borderRadius: BorderRadius.circular(12), // <-- Radius
                                  ),
                                  child: const Text(
                                    "MASUK",
                                    style: TextStyle(
                                      color: Colors.white,
                                      fontSize: 20.0,
                                      fontWeight: FontWeight.w600,
                                    ),
                                  ),
                                ),
                              ),
                              const SizedBox(height: 35.0),
                              const Center(
                                child: Text(
                                  "© 2022 Dinas Kesehatan Kab. Minahasa",
                                  style: TextStyle(
                                    color: Colors.black54,
                                    fontSize: 14.0,
                                    fontWeight: FontWeight.w500,
                                  ),
                                ),
                              ),
                            ],
                          ),
                        ],
                      ),
                    ),
                  ),
                ),
              ),
            ),
          ),
        ),
      ),
    );
  }
}

// INPUT
class InputFields extends StatelessWidget {
  InputFields({Key? key}) : super(key: key);

  final AuthController _controller = Get.find<AuthController>();

  @override
  Widget build(BuildContext context) {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.stretch,
      children: [
        TextField(
          controller: _controller.textLogin,
          maxLength: 50,
          style: const TextStyle(color: Colors.black87),
          decoration: const InputDecoration(
            hintText: "NIK",
            hintStyle: TextStyle(color: Colors.grey),
            enabledBorder: UnderlineInputBorder(
              borderSide: BorderSide(color: Colors.grey),
            ),
            focusedBorder: UnderlineInputBorder(
              borderSide: BorderSide(color: Colors.red),
            ),
            counterText: "",
          ),
          inputFormatters: [FilteringTextInputFormatter.digitsOnly],
          keyboardType: TextInputType.number,
        ),
        const SizedBox(height: 25.0),
        TextField(
          controller: _controller.textPwd,
          obscureText: true,
          maxLength: 50,
          style: const TextStyle(color: Colors.black87),
          decoration: const InputDecoration(
            hintText: "Password",
            hintStyle: TextStyle(color: Colors.grey),
            enabledBorder: UnderlineInputBorder(
              borderSide: BorderSide(color: Colors.grey),
            ),
            focusedBorder: UnderlineInputBorder(
              borderSide: BorderSide(color: Colors.red),
            ),
            counterText: "",
          ),
        )
      ],
    );
  }
}
