import 'package:animated_widgets/animated_widgets.dart';
import 'package:flutter/material.dart';
import 'package:sitimoufr/controllers/auth_controller.dart';
import 'package:sitimoufr/helper/colors.dart';

import 'package:sitimoufr/widgets/helper/statefull_wrapper.dart';

class SplashScreen extends StatelessWidget {
  const SplashScreen({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return StatefulWrapper(
      onInit: () async {
        AuthController().validasiSplashScreen();
      },
      child: MediaQuery(
        data: MediaQuery.of(context).copyWith(textScaleFactor: 1.0),
        child: Scaffold(
          backgroundColor: primaryColor,
          body: Stack(
            fit: StackFit.expand,
            children: [
              // Jika tidak mau background, hapus Stack()
              Column(
                mainAxisAlignment: MainAxisAlignment.center,
                children: [
                  OpacityAnimatedWidget.tween(
                    duration: const Duration(milliseconds: 700),
                    child: ScaleAnimatedWidget.tween(
                      duration: const Duration(milliseconds: 500),
                      child: const Hero(
                        tag: "login_app_logo",
                        child: Image(
                          width: 172,
                          height: 172,
                          fit: BoxFit.cover,
                          image: AssetImage('assets/images/logo/app_logo_512px.png'),
                          //assets/images/logo/bdc.png
                        ),
                      ),
                    ),
                  ),
                  const SizedBox(height: 35),
                  OpacityAnimatedWidget.tween(
                    duration: const Duration(milliseconds: 1400),
                    child: ScaleAnimatedWidget.tween(
                      duration: const Duration(milliseconds: 1000),
                      child: const Text(
                        "SI-TIMOU 119",
                        style: TextStyle(
                          color: Colors.redAccent,
                          fontSize: 35.0,
                          fontWeight: FontWeight.w600,
                        ),
                      ),
                    ),
                  ),
                  OpacityAnimatedWidget.tween(
                    duration: const Duration(milliseconds: 1400),
                    child: ScaleAnimatedWidget.tween(
                      duration: const Duration(milliseconds: 1000),
                      child: const Text(
                        "RESPONDER",
                        style: TextStyle(
                          color: Colors.grey,
                          fontSize: 25.0,
                          fontWeight: FontWeight.normal,
                        ),
                      ),
                    ),
                  ),
                ],
              )
            ],
          ),
        ),
      ),
    );
  }
}
