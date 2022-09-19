import 'package:animated_bottom_navigation_bar/animated_bottom_navigation_bar.dart';
import 'package:double_back_to_close_app/double_back_to_close_app.dart';
import 'package:fluentui_system_icons/fluentui_system_icons.dart';
import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:sitimou/controllers/bindings/bindings.dart';
import 'package:sitimou/controllers/home_controller.dart';
import 'package:sitimou/helper/colors.dart';
import 'package:sitimou/views/home/components/home_front.dart';
import 'package:sitimou/views/home/components/home_profil.dart';
import 'package:sitimou/views/laporan/panic.dart';

class HomePage extends StatelessWidget {
  HomePage({super.key});

  final HomeController _controller = Get.find<HomeController>();

  @override
  Widget build(BuildContext context) {
    return MediaQuery(
      data: MediaQuery.of(context).copyWith(textScaleFactor: 1.0),
      child: Scaffold(
        backgroundColor: primaryColor,
        body: DoubleBackToCloseApp(
          snackBar: const SnackBar(
            content: Text("Tap tombol Kembali sekali lagi untuk keluar"),
          ),
          child: SafeArea(
            bottom: false,
            child: SizedBox(
              height: double.infinity,
              child: PageView(
                controller: _controller.pageController,
                onPageChanged: _controller.onPageChanged,
                physics: const NeverScrollableScrollPhysics(),
                children: const [
                  HomeFrontTab(),
                  HomeProfilTab(),
                ],
              ),
            ),
          ),
        ),
        extendBody: true,
        bottomNavigationBar: Obx(() {
          return AnimatedBottomNavigationBar.builder(
            itemCount: 2,
            tabBuilder: (int index, bool isActive) {
              final iconColor = isActive ? Colors.red : Colors.grey;
              final textColor = isActive ? Colors.black : Colors.grey;
              final textWeight = isActive ? FontWeight.w500 : FontWeight.normal;
              if (index == 0) {
                return Column(
                  mainAxisSize: MainAxisSize.min,
                  mainAxisAlignment: MainAxisAlignment.center,
                  children: [
                    Icon(
                      _controller.iconList[index],
                      color: iconColor,
                    ),
                    const SizedBox(height: 4),
                    Text(
                      "Beranda",
                      style: TextStyle(
                        color: textColor,
                        fontSize: 14.0,
                        fontWeight: textWeight,
                      ),
                    )
                  ],
                );
              } else {
                return Column(
                  mainAxisSize: MainAxisSize.min,
                  mainAxisAlignment: MainAxisAlignment.center,
                  children: [
                    Icon(
                      _controller.iconList[index],
                      color: iconColor,
                    ),
                    const SizedBox(height: 4),
                    Text(
                      "Profil",
                      style: TextStyle(
                        color: textColor,
                        fontSize: 14.0,
                        fontWeight: textWeight,
                      ),
                    )
                  ],
                );
              }
            },
            gapLocation: GapLocation.center,
            notchSmoothness: NotchSmoothness.softEdge,
            activeIndex: _controller.pageIndex.value,
            onTap: (index) {
              debugPrint(index.toString());
              _controller.pageIndex.value = index;
              _controller.pageController?.jumpToPage(_controller.pageIndex.value);
            },
            shadow: const BoxShadow(
              offset: Offset(0, 1),
              blurRadius: 12,
              spreadRadius: 0.5,
              color: Colors.grey,
            ),
          );
        }),
        floatingActionButton: FloatingActionButton(
          onPressed: () {
            Get.to(() => PanicPage(), binding: LaporanBinding(), arguments: ["panic"]);
          },
          backgroundColor: Colors.red,
          child: const Icon(
            // Icons.emergency,
            // ic_fluent_heart_pulse_32_regular
            // important_24_regular
            FluentIcons.heart_pulse_24_regular,
            color: Colors.white,
            size: 35.0,
          ),
        ),
        floatingActionButtonLocation: FloatingActionButtonLocation.centerDocked,
      ),
    );
  }
}
