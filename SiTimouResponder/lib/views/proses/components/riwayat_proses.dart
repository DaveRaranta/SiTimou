import 'package:fluentui_system_icons/fluentui_system_icons.dart';
import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:liquid_pull_to_refresh/liquid_pull_to_refresh.dart';
import 'package:sitimoufr/controllers/proses_controller.dart';
import 'package:sitimoufr/helper/colors.dart';
import 'package:sitimoufr/helper/scroll_settings.dart';
import 'package:sitimoufr/widgets/appbar/app_bar.dart';
import 'package:sitimoufr/widgets/cards/card_laporan.dart';
import 'package:sitimoufr/widgets/textfield/default_textfield.dart';

class RiwayatLaporanPage extends StatelessWidget {
  final String jenisLaporan;
  RiwayatLaporanPage({required this.jenisLaporan, super.key});

  final controller = Get.find<ProsesController>();

  @override
  Widget build(BuildContext context) {
    return MediaQuery(
      data: MediaQuery.of(context).copyWith(textScaleFactor: 1.0),
      child: GestureDetector(
        onTap: () => FocusScope.of(context).unfocus(),
        child: Scaffold(
          backgroundColor: primaryColor,
          appBar: appBar(
            context,
            titleText: jenisLaporan == "1" ? "RIWAYAT LAPORAN" : "RIWAYAT PANIK",
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
            showTailing: false,
            bottom: PreferredSize(
              preferredSize: const Size.fromHeight(60),
              child: Padding(
                padding: const EdgeInsets.only(bottom: 20, left: 20.0, right: 20.0),
                child: RoundedTextField(
                  hintText: "Cari Data",
                  icon: const Icon(FluentIcons.search_20_regular),
                  onChanged: (f) => controller.filterRiwayatProses(f),
                ),
              ),
            ),
          ),
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

                  if (controller.listRiwayatProsesFilter.isEmpty) {
                    return const Center(
                      child: Text("TIDAK ADA DATA"),
                    );
                  }

                  return ScrollConfiguration(
                    behavior: ScrollSettings(),
                    child: LiquidPullToRefresh(
                      color: primaryColor,
                      backgroundColor: Colors.red,
                      onRefresh: controller.getListRiwayatProses,
                      child: ListView.separated(
                        itemCount: controller.listRiwayatProsesFilter.length,
                        separatorBuilder: (context, index) {
                          return const SizedBox(height: 5);
                        },
                        itemBuilder: (context, index) {
                          return GestureDetector(
                            onTap: () {},
                            child: Padding(
                              padding: const EdgeInsets.only(top: 8.0),
                              child: CardLaporan(
                                textHeader: controller.listRiwayatProsesFilter[index]!.tanggalProses.toString(),
                                textContent: controller.listRiwayatProsesFilter[index]!.namaPelapor.toString(),
                                textFooter: "Durasi: ${controller.listRiwayatProsesFilter[index]!.durasiProses}",
                                iconStatus: controller.listRiwayatProsesFilter[index]!.tentang == "PANIK"
                                    ? Icons.assignment_late_outlined
                                    : Icons.assignment_outlined,
                                iconColor: controller.listRiwayatProsesFilter[index]!.tentang == "PANIK" ? Colors.red : Colors.deepOrange,
                                leadBoxColor: Colors.white,
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
