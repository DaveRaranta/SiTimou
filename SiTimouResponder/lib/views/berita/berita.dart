import 'package:get/get.dart';
import 'package:flutter/material.dart';
import 'package:liquid_pull_to_refresh/liquid_pull_to_refresh.dart';

import 'package:sitimoufr/helper/colors.dart';
import 'package:sitimoufr/helper/extensions.dart';
import 'package:sitimoufr/helper/scroll_settings.dart';
import 'package:sitimoufr/controllers/berita_controller.dart';
import 'package:sitimoufr/widgets/appbar/app_bar.dart';
import 'package:sitimoufr/widgets/cards/card_berita_small.dart';
import 'package:sitimoufr/views/berita/components/detail_berita.dart';

class BeritaPage extends StatelessWidget {
  BeritaPage({Key? key}) : super(key: key);

  final BeritaController controller = Get.find<BeritaController>();

  @override
  Widget build(BuildContext context) {
    return MediaQuery(
      data: MediaQuery.of(context).copyWith(textScaleFactor: 1.0),
      child: Scaffold(
        backgroundColor: primaryColor,
        appBar: appBar(
          context,
          titleText: "BERITA MINAHASA",
          titleFontSize: 22.0,
          titleTextColor: Colors.black54,
          showLeading: true,
          leadingIcon: const Icon(
            Icons.arrow_back_ios_new,
            color: Colors.black54,
          ),
          leadingFunction: () {
            Get.back();
          },
        ),
        body: SafeArea(
          child: Hero(
            tag: "h_berita_kota",
            child: SizedBox(
              height: double.infinity,
              width: double.infinity,
              child: Obx(() {
                if (controller.isListLoading.value) {
                  return const Center(child: CircularProgressIndicator());
                }

                if (controller.listBerita.isEmpty) {
                  return const Center(
                    child: Text("NO DATA"),
                  );
                }
                return ScrollConfiguration(
                  behavior: ScrollSettings(),
                  child: LiquidPullToRefresh(
                    color: primaryColor,
                    backgroundColor: Colors.red,
                    onRefresh: controller.getBeritaKota,
                    child: ListView.separated(
                      separatorBuilder: (context, index) {
                        return SizedBox(
                          height: 12,
                          child: Center(
                            child: Container(
                              height: 1,
                              decoration: BoxDecoration(
                                gradient: LinearGradient(
                                  begin: Alignment.centerRight,
                                  end: Alignment.centerLeft,
                                  colors: [
                                    primaryColor,
                                    Colors.grey.shade300,
                                    primaryColor,
                                  ],
                                ),
                              ),
                            ),
                          ),
                        );
                      },
                      itemCount: controller.listBerita.length,
                      itemBuilder: (context, index) {
                        return GestureDetector(
                          child: Padding(
                            padding: const EdgeInsets.only(left: 15.0, right: 15.0),
                            child: CardBeritaSmall(
                              textJenis: "",
                              textHeader: DateTime.parse(controller.listBerita[index]!.tglBerita.toString()).format('EEEE, d MMMM y', 'id'),
                              textContent: controller.listBerita[index]!.judulBerita.toString(),
                              textFooter: controller.listBerita[index]!.coverBerita.toString(),
                              imageCover: controller.listBerita[index]!.coverBerita.toString(),
                              idData: controller.listBerita[index]!.idBerita.toString(),
                            ),
                          ),
                          onTap: () {
                            debugPrint("OPEN BERITA");
                            debugPrint(controller.listBerita[index]!.coverBerita.toString());
                            Get.to(
                              () => DetailBeritaPage(
                                imageCover: controller.listBerita[index]!.coverBerita.toString(),
                                textTanggal: DateTime.parse(controller.listBerita[index]!.tglBerita.toString()).format('EEEE, d MMMM y', 'id'),
                                textTitle: controller.listBerita[index]!.judulBerita.toString(),
                                textIsi: controller.listBerita[index]!.isiBerita.toString(),
                              ),
                            );
                          },
                        );
                      },
                    ),
                  ),
                );
              }),
            ),
          ),
        ),
      ),
    );
  }
}
