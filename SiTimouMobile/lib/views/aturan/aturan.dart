import 'package:fluentui_system_icons/fluentui_system_icons.dart';
import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:liquid_pull_to_refresh/liquid_pull_to_refresh.dart';
import 'package:sitimou/controllers/info_controller.dart';
import 'package:sitimou/helper/colors.dart';
import 'package:sitimou/helper/scroll_settings.dart';
import 'package:sitimou/widgets/appbar/app_bar.dart';
import 'package:sitimou/widgets/cards/card_info.dart';
import 'package:sitimou/widgets/textfield/default_textfield.dart';

class AturanPage extends StatelessWidget {
  AturanPage({super.key});

  final controller = Get.find<InfoController>();

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
            titleText: "INFO ATURAN",
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
                  onChanged: (f) => controller.filterListAturan(f),
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

                  if (controller.listAturanFilter.isEmpty) {
                    return const Center(
                      child: Text("TIDAK ADA DATA"),
                    );
                  }

                  return ScrollConfiguration(
                    behavior: ScrollSettings(),
                    child: LiquidPullToRefresh(
                      color: primaryColor,
                      backgroundColor: Colors.red,
                      onRefresh: controller.getListAturan,
                      child: ListView.separated(
                        itemCount: controller.listAturanFilter.length,
                        separatorBuilder: (context, index) {
                          return const SizedBox(height: 5);
                        },
                        itemBuilder: (context, index) {
                          return GestureDetector(
                            onTap: () {
                              debugPrint(controller.listAturanFilter[index]!.aturanId.toString());
                              controller.getDetailAturan(context, int.parse(controller.listAturanFilter[index]!.aturanId.toString()));
                            },
                            child: Padding(
                              padding: const EdgeInsets.only(top: 8.0),
                              child: CardInfo(
                                textContent: controller.listAturanFilter[index]!.namaAturan.toString(),
                                textFooter: controller.listAturanFilter[index]!.namaOpd.toString(),
                                iconStatus: Icons.local_library_outlined,
                                leadBoxColor: Colors.purple,
                                //idData: controller.listAturanFilter[index]!.aturanId.toString(),
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
