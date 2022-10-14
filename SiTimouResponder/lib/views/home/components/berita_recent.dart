import 'package:sitimoufr/controllers/home_controller.dart';
import 'package:sitimoufr/helper/colors.dart';
import 'package:sitimoufr/helper/extensions.dart';
import 'package:sitimoufr/views/berita/components/detail_berita.dart';
import 'package:sitimoufr/widgets/cards/card_berita_recent.dart';
import 'package:fluentui_system_icons/fluentui_system_icons.dart';
import 'package:flutter/material.dart';
import 'package:get/get.dart';

class HomeBeritaRecent extends StatelessWidget {
  HomeBeritaRecent({super.key});

  final controller = Get.find<HomeController>();

  @override
  Widget build(BuildContext context) {
    return LimitedBox(
      maxHeight: 500,
      // width: double.infinity,
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        mainAxisSize: MainAxisSize.min,
        children: [
          const SizedBox(height: 30.0),
          Row(
            mainAxisAlignment: MainAxisAlignment.spaceBetween,
            children: [
              const Text(
                "Berita Kota",
                style: TextStyle(
                  fontSize: 16.0,
                  color: Colors.black,
                  fontWeight: FontWeight.w500,
                ),
                maxLines: 1,
                overflow: TextOverflow.ellipsis,
              ),
              GestureDetector(
                child: const Icon(
                  FluentIcons.arrow_sync_24_regular,
                  // Iconsax.refresh,
                  color: Colors.black54,
                  size: 21.0,
                ),
                onTap: () {
                  controller.getRecentBerita();
                  // _controller.getCountDisposisi();
                  // _controller.getRecentBerita();
                },
              ),
            ],
          ),
          const SizedBox(height: 15.0),
          Flexible(
            child: Obx(() {
              if (controller.isListLoading.value) {
                return const Center(child: CircularProgressIndicator());
              }

              if (controller.listBerita.isEmpty) {
                return const Center(
                  child: Text("NO DATA"),
                );
              } else {
                return ListView.separated(
                  shrinkWrap: true,
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
                  itemCount: controller.listBerita.length < 5 ? controller.listBerita.length : 5,
                  itemBuilder: (context, index) {
                    debugPrint("LENGHT:${controller.listBerita.length}");
                    return GestureDetector(
                      child: CardRecentBeritaSmall(
                        textTitle: controller.listBerita[index]!.judulBerita.toString(),
                        textFooter: DateTime.parse(controller.listBerita[index]!.tglBerita.toString()).format('EEEE, d MMMM y', 'id'),
                        imageCover: controller.listBerita[index]!.coverBerita.toString(),
                      ),
                      onTap: () {
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
                );
              }
            }),
          )
        ],
      ),
    );
  }
}
