import 'package:get/get.dart';
import 'package:flutter/material.dart';
import 'package:cached_network_image/cached_network_image.dart';
import 'package:sitimou/controllers/bindings/bindings.dart';
import 'package:sitimou/controllers/laporan_controller.dart';
import 'package:sitimou/helper/colors.dart';
import 'package:sitimou/helper/ui_toast.dart';
import 'package:sitimou/views/common/image_viewer.dart';
import 'package:sitimou/views/laporan/components/lokasi_laporan.dart';
import 'package:sitimou/widgets/appbar/app_bar.dart';
import 'package:sitimou/helper/globals.dart' as g;

class DetailLaporanPage extends StatelessWidget {
  DetailLaporanPage({super.key});

  final LaporanController controller = Get.find<LaporanController>();

  @override
  Widget build(BuildContext context) {
    return MediaQuery(
      data: MediaQuery.of(context).copyWith(textScaleFactor: 1.0),
      child: GestureDetector(
        onTap: () => FocusScope.of(context).unfocus(),
        child: Scaffold(
          backgroundColor: primaryColor,
          appBar: appBar(context,
              titleText: "DETAIL LAPORAN",
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
              showTailing: controller.detailLaporan.value.flg == 'N' && controller.detailLaporan.value.userId == g.userId,
              tailingIcons: const Icon(
                Icons.delete_rounded,
                color: Colors.black54,
              ),
              tailingFunction: () {
                debugPrint("HAPUS");
                controller.hapusDetailLaporan(context, "1");
              }),
          body: SafeArea(
            child: SizedBox(
              height: double.infinity,
              width: double.infinity,
              child: Padding(
                padding: const EdgeInsets.only(top: 20.0, left: 20.0, right: 20.0),
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    Text(
                      controller.detailLaporan.value.tanggalLong.toString(),
                      maxLines: 1,
                      softWrap: false,
                      overflow: TextOverflow.ellipsis,
                      style: const TextStyle(
                        fontSize: 12.0,
                        fontWeight: FontWeight.w600,
                        color: Colors.black54,
                      ),
                    ),
                    const SizedBox(height: 1),
                    GestureDetector(
                      child: Text(
                        controller.detailLaporan.value.tentang.toString(),
                        maxLines: 2,
                        softWrap: false,
                        overflow: TextOverflow.ellipsis,
                        style: const TextStyle(
                          fontSize: 20.0,
                          fontWeight: FontWeight.w500,
                        ),
                      ),
                      onTap: () {
                        toastPesan(
                          "TENTANG",
                          controller.detailLaporan.value.tentang.toString(),
                        );
                      },
                    ),
                    const SizedBox(height: 10),
                    statusLaporan(),
                    const SizedBox(height: 15),
                    Expanded(
                      child: Container(
                        width: double.infinity,
                        margin: const EdgeInsets.symmetric(vertical: 0),
                        padding: const EdgeInsets.symmetric(horizontal: 10, vertical: 10),
                        decoration: BoxDecoration(
                          color: Colors.white,
                          borderRadius: BorderRadius.circular(10),
                          boxShadow: [
                            BoxShadow(
                              color: Colors.grey.withOpacity(0.5),
                              spreadRadius: 1,
                              blurRadius: 4,
                              offset: const Offset(0, 3), // changes position of shadow
                            ),
                          ],
                        ),
                        child: Text(
                          controller.detailLaporan.value.isiLaporan.toString(),
                          overflow: TextOverflow.ellipsis,
                          style: const TextStyle(
                            fontSize: 14.0,
                            fontWeight: FontWeight.w500,
                            color: Colors.black54,
                          ),
                        ),
                      ),
                    ),
                    const SizedBox(height: 30.0),
                  ],
                ),
              ),
            ),
          ),
        ),
      ),
    );
  }
}

Widget statusLaporan() {
  final LaporanController controller = Get.find<LaporanController>();
  const imgUrl = "${g.apiUrl}/lapor/foto_lokasi";
  debugPrint("$imgUrl/${controller.detailLaporan.value.laporanId}");
  return Container(
    height: 90.0,
    width: double.infinity,
    margin: const EdgeInsets.symmetric(vertical: 0),
    padding: const EdgeInsets.symmetric(horizontal: 10, vertical: 10),
    decoration: BoxDecoration(
      color: Colors.white,
      borderRadius: BorderRadius.circular(10),
      boxShadow: [
        BoxShadow(
          color: Colors.grey.withOpacity(0.5),
          spreadRadius: 1,
          blurRadius: 4,
          offset: const Offset(0, 3), // changes position of shadow
        ),
      ],
    ),
    child: Column(
      children: [
        Row(
          children: [
            Icon(
              controller.iconStatusLaporan(controller.detailLaporan.value.flg.toString()),
              color: controller.colorStatusLaporan(controller.detailLaporan.value.flg.toString()),
              size: 20,
            ),
            const SizedBox(width: 5.0),
            Text(
              controller.statusLaporan(controller.detailLaporan.value.flg.toString()),
              maxLines: 1,
              softWrap: false,
              overflow: TextOverflow.ellipsis,
              style: TextStyle(
                fontSize: 13.0,
                fontWeight: FontWeight.w600,
                //color: Colors.black54,
                color: controller.colorStatusLaporan(controller.detailLaporan.value.flg.toString()),
              ),
            ),
          ],
        ),
        const SizedBox(height: 4),
        GestureDetector(
          onTap: () {
            Get.to(
              () => ImageViewer(
                provider: CachedNetworkImageProvider(
                  "$imgUrl/${controller.detailLaporan.value.laporanId}",
                  headers: g.httpHeaders,
                ),
              ),
            );
          },
          child: Row(
            children: const [
              Icon(
                Icons.attach_file,
                color: Colors.indigo,
                size: 20,
              ),
              SizedBox(width: 5.0),
              Text(
                "Foto Tautan",
                maxLines: 1,
                softWrap: false,
                overflow: TextOverflow.ellipsis,
                style: TextStyle(
                  fontSize: 13.0,
                  fontWeight: FontWeight.w600,
                  color: Colors.blue,
                  //decoration: TextDecoration.underline,
                ),
              ),
            ],
          ),
        ),
        const SizedBox(height: 4),
        GestureDetector(
          onTap: () {
            Get.to(() => const LokasiLaporan(), binding: LaporanBinding());
          },
          child: Row(
            children: const [
              Icon(
                Icons.location_pin,
                color: Colors.red,
                size: 20,
              ),
              SizedBox(width: 5.0),
              Text(
                "Lokasi Laporan",
                maxLines: 1,
                softWrap: false,
                overflow: TextOverflow.ellipsis,
                style: TextStyle(
                  fontSize: 13.0,
                  fontWeight: FontWeight.w600,
                  color: Colors.blue,
                  //decoration: TextDecoration.underline,
                ),
              ),
            ],
          ),
        )
      ],
    ),
  );
}
