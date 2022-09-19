import 'package:fluentui_system_icons/fluentui_system_icons.dart';
import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:liquid_pull_to_refresh/liquid_pull_to_refresh.dart';
import 'package:sitimou/controllers/lokasi_controller.dart';
import 'package:sitimou/helper/colors.dart';
import 'package:sitimou/helper/scroll_settings.dart';
import 'package:sitimou/widgets/appbar/app_bar.dart';
import 'package:sitimou/widgets/cards/card_lokasi.dart';
import 'package:sitimou/widgets/textfield/default_textfield.dart';

class LokasiPage extends StatelessWidget {
  LokasiPage({super.key});

  final LokasiController controller = Get.find<LokasiController>();

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
            titleText: "LOKASI PENTING",
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
                  onChanged: (f) => controller.filterListLokasi(f),
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

                  if (controller.listLokasiFilter.isEmpty) {
                    return const Center(
                      child: Text("TIDAK ADA DATA"),
                    );
                  }

                  return ScrollConfiguration(
                    behavior: ScrollSettings(),
                    child: LiquidPullToRefresh(
                      color: primaryColor,
                      backgroundColor: Colors.red,
                      onRefresh: controller.getListLokasi,
                      child: ListView.separated(
                        itemCount: controller.listLokasiFilter.length,
                        separatorBuilder: (context, index) {
                          return const SizedBox(height: 5);
                        },
                        itemBuilder: (context, index) {
                          return GestureDetector(
                            onTap: () {
                              controller.getDetailLokasi(context, int.parse(controller.listLokasiFilter[index]!.lokasiId.toString()));
                            },
                            child: Padding(
                              padding: const EdgeInsets.only(top: 8.0),
                              child: CardLokasi(
                                textContent: controller.listLokasiFilter[index]!.namaLokasi
                                    .toString(), //controller.listLokasiFilter[index]!.namaLokasi.toString(),
                                textFooter:
                                    "${controller.listLokasiFilter[index]!.alamat.toString()}, ${controller.listLokasiFilter[index]!.desa.toString()} - ${controller.listLokasiFilter[index]!.kecamatan.toString()}",
                                idData: controller.listLokasiFilter[index]!.lokasiId.toString(),
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
