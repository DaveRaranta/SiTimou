import 'package:fluentui_system_icons/fluentui_system_icons.dart';
import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:liquid_pull_to_refresh/liquid_pull_to_refresh.dart';
import 'package:sitimoufr/controllers/proses_controller.dart';
import 'package:sitimoufr/helper/colors.dart';
import 'package:sitimoufr/helper/scroll_settings.dart';
import 'package:sitimoufr/widgets/appbar/app_bar.dart';
import 'package:sitimoufr/widgets/cards/card_laporan.dart';

class ProsesPage extends StatelessWidget {
  ProsesPage({super.key});

  final controller = Get.find<ProsesController>();

  @override
  Widget build(BuildContext context) {
    return MediaQuery(
      data: MediaQuery.of(context).copyWith(textScaleFactor: 1.0),
      child: GestureDetector(
        onTap: () => FocusScope.of(context).unfocus(),
        child: Scaffold(
          backgroundColor: primaryColor,
          appBar: appBar(context,
              titleText: "LAPORAN MASUK",
              backgroundColor: primaryColor,
              //titleFontSize: 20.0,
              titleTextColor: Colors.black54,
              showLeading: true,
              leadingIcon: const Icon(
                Icons.arrow_back_ios_new,
                color: Colors.black54,
              ),
              leadingFunction: () {
                Get.back();
              },
              showTailing: true,
              tailingIcons: const Icon(
                Icons.history_rounded,
                color: Colors.black54,
              ),
              tailingFunction: () {
                controller.pilihRiwayatLaporan(context);
              }),
          body: SafeArea(
            child: SizedBox(
              height: double.infinity,
              width: double.infinity,
              child: Padding(
                padding: const EdgeInsets.only(left: 20.0, right: 20.0),
                child: Obx(() {
                  if (controller.isListLoading.value) {
                    return const Center(child: CircularProgressIndicator());
                  }

                  if (controller.listLaporan.isEmpty) {
                    return const Center(
                      child: Text("TIDAK ADA DATA"),
                    );
                  }

                  return ScrollConfiguration(
                    behavior: ScrollSettings(),
                    child: LiquidPullToRefresh(
                      color: primaryColor,
                      backgroundColor: Colors.red,
                      onRefresh: controller.getListLaporan,
                      child: ListView.separated(
                        itemCount: controller.listLaporan.length,
                        separatorBuilder: (context, index) {
                          return const SizedBox(height: 5);
                        },
                        itemBuilder: (context, index) {
                          return GestureDetector(
                            onTap: () {
                              debugPrint(controller.listLaporan[index]!.laporanId.toString());
                              // Set "key" data
                              controller.statusTerima.value = controller.listLaporan[index]!.flg != "N";
                              //controller.jenisLaporan = controller.listLaporan[index]!.idJenisLaporan.toString();

                              //
                              // Muat laporan
                              //

                              // Set Id Disposisi
                              controller.disposisiId = int.parse(controller.listLaporan[index]!.disposisiId.toString());

                              // Load detail laporan
                              controller.getDetailLaporan(context, controller.listLaporan[index]!.idJenisLaporan.toString(),
                                  int.parse(controller.listLaporan[index]!.laporanId.toString()));
                            },
                            child: Padding(
                              padding: const EdgeInsets.only(top: 8.0),
                              child: CardLaporan(
                                textHeader: controller.listLaporan[index]!.tanggal.toString(),
                                textContent: controller.listLaporan[index]!.namaPelapor.toString(),
                                textFooter: controller.listLaporan[index]!.alamatPelapor.toString(),
                                iconStatus:
                                    controller.listLaporan[index]!.idJenisLaporan == "1" ? Icons.assignment_outlined : Icons.assignment_late_outlined,
                                leadBoxColor: controller.listLaporan[index]!.idJenisLaporan == "1" ? Colors.orange : Colors.red,
                              ),
                            ),
                          );
                          //Text(controller.listLokasiFilter[index]!.namaLokasi.toString());
                        },
                      ),
                    ),
                  );
                }),
              ),
            ),
          ),
        ),
      ),
    );
  }
}
