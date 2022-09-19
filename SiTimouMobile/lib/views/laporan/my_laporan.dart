import 'package:fluentui_system_icons/fluentui_system_icons.dart';
import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:liquid_pull_to_refresh/liquid_pull_to_refresh.dart';
import 'package:sitimou/controllers/bindings/bindings.dart';
import 'package:sitimou/controllers/laporan_controller.dart';
import 'package:sitimou/helper/colors.dart';
import 'package:sitimou/helper/scroll_settings.dart';
import 'package:sitimou/views/laporan/components/buat_laporan.dart';
import 'package:sitimou/widgets/appbar/app_bar.dart';
import 'package:sitimou/widgets/cards/card_laporan.dart';
import 'package:sitimou/widgets/textfield/default_textfield.dart';

class LaporanPage extends StatelessWidget {
  LaporanPage({super.key});

  final LaporanController controller = Get.find<LaporanController>();

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
            titleText: "LAPORAN",
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
            tailingIcons: const Icon(
              FluentIcons.history_24_filled,
              color: Colors.black54,
            ),
            bottom: PreferredSize(
              preferredSize: const Size.fromHeight(60),
              child: Padding(
                padding: const EdgeInsets.only(bottom: 20, left: 20.0, right: 20.0),
                child: RoundedTextField(
                  hintText: "Cari Data",
                  icon: const Icon(FluentIcons.search_20_regular),
                  onChanged: (f) => controller.filterListLaporan(f),
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

                  if (controller.listLaporanFilter.isEmpty) {
                    return const Center(
                      child: Text("TIDAK ADA DATA"),
                    );
                  }

                  return ScrollConfiguration(
                    behavior: ScrollSettings(),
                    child: LiquidPullToRefresh(
                      color: primaryColor,
                      backgroundColor: Colors.red,
                      onRefresh: controller.getListLaporanById,
                      child: ListView.separated(
                        itemCount: controller.listLaporanFilter.length,
                        separatorBuilder: (context, index) {
                          return const SizedBox(height: 5);
                        },
                        itemBuilder: (context, index) {
                          return GestureDetector(
                            onTap: () {
                              // controller.getDetailLokasi(context, int.parse(controller.listLokasiFilter[index]!.lokasiId.toString()));
                              debugPrint(controller.listLaporanFilter[index]!.laporanId.toString());
                              controller.getDetailLaporan(context, int.parse(controller.listLaporanFilter[index]!.laporanId.toString()));
                            },
                            child: Padding(
                              padding: const EdgeInsets.only(top: 8.0),
                              child: CardLaporan(
                                textHeader: controller.listLaporanFilter[index]!.tanggal.toString(),
                                textContent: controller.listLaporanFilter[index]!.tentang.toString(),
                                textFooter: controller.statusLaporan(controller.listLaporanFilter[index]!.flg.toString()).toUpperCase(),
                                iconStatus: controller.iconStatusLaporan(controller.listLaporanFilter[index]!.flg.toString().toUpperCase()),
                                leadBoxColor: controller.colorStatusLaporan(controller.listLaporanFilter[index]!.flg.toString().toUpperCase()),
                                idData: controller.listLaporanFilter[index]!.laporanId.toString(),
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
          floatingActionButton: FloatingActionButton(
            onPressed: () {
              Get.to(() => BuatLaporanPage(), binding: LaporanBinding());
            },
            backgroundColor: Colors.red,
            child: const Icon(
              // Icons.emergency,
              // ic_fluent_heart_pulse_32_regular
              // important_24_regular
              Icons.add,
              color: Colors.white,
              size: 36.0,
            ),
          ),
        ),
      ),
    );
  }
}
