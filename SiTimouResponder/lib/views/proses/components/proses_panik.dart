import 'package:get/get.dart';
import 'package:flutter/material.dart';
import 'package:sleek_button/sleek_button.dart';
import 'package:url_launcher/url_launcher.dart';
import 'package:fluentui_system_icons/fluentui_system_icons.dart';
import 'package:cached_network_image/cached_network_image.dart';

import 'package:sitimoufr/controllers/bindings/bindings.dart';
import 'package:sitimoufr/controllers/proses_controller.dart';
import 'package:sitimoufr/helper/colors.dart';
import 'package:sitimoufr/helper/scroll_settings.dart';
import 'package:sitimoufr/helper/text_helper.dart';
import 'package:sitimoufr/views/proses/components/peta_laporan.dart';
import 'package:sitimoufr/widgets/appbar/app_bar.dart';
import 'package:sitimoufr/helper/globals.dart' as g;

class ProsesPanikPage extends StatelessWidget {
  ProsesPanikPage({super.key});

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
            titleText: "PROSES PANIK",
            titleFontSize: 20.0,
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
              FluentIcons.map_24_regular,
              color: Colors.black54,
            ),
            tailingFunction: () async {
              Get.to(() => const LokasiLaporan(), binding: ProsesBinding());
            },
          ),
          body: SafeArea(
            child: Padding(
                padding: const EdgeInsets.only(top: 0.0, left: 20.0, right: 20.0),
                child: SizedBox(
                  width: double.infinity,
                  child: ScrollConfiguration(
                    behavior: ScrollSettings(),
                    child: SingleChildScrollView(
                      child: Column(
                        crossAxisAlignment: CrossAxisAlignment.start,
                        children: [
                          // Info Pelapor
                          const SizedBox(height: 20.0),
                          infoPelapor(),
                          infoLaporan(),
                          Obx(() {
                            if (controller.statusTerima.value == false) {
                              return terimaLaporan(context);
                            }
                            return prosesLapporan(context);
                          }),
                          const SizedBox(height: 35.0),
                        ],
                      ),
                    ),
                  ),
                )),
          ),
        ),
      ),
    );
  }
}

Widget infoPelapor() {
  final controller = Get.find<ProsesController>();
  var fotoPelapor = "${g.apiUrl}/home/foto_profil_pengguna/${controller.detailPanik.value.userId}";
  // var imgUrl = "${g.apiUrl}/lapor/foto_lokasi/${controller.detailPanik.value.laporanId}";

  return Column(
    crossAxisAlignment: CrossAxisAlignment.start,
    children: [
      const Text(
        "Info Pelapor",
        overflow: TextOverflow.ellipsis,
        style: TextStyle(
          color: Colors.black,
          fontSize: 15.0,
          fontWeight: FontWeight.w500,
        ),
      ),
      const SizedBox(height: 5.0),
      Container(
        //height: 155.0,
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
        child: Row(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            SizedBox(
              height: 64.0,
              width: 64.0,
              child: ClipRRect(
                borderRadius: BorderRadius.circular(10),
                child: CachedNetworkImage(
                  imageUrl: fotoPelapor,
                  httpHeaders: g.httpHeaders,
                  //height: 40.0,
                  //width: 40.0,
                  progressIndicatorBuilder: (context, url, downloadProgress) => CircularProgressIndicator(value: downloadProgress.progress),
                  errorWidget: (context, url, error) => const Icon(Icons.error),
                  fit: BoxFit.cover,
                ),
              ),
            ),
            const SizedBox(width: 10.0),
            Expanded(
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                children: [
                  Text(
                    controller.detailPanik.value.namaPelapor.toString(),
                    maxLines: 1,
                    softWrap: false,
                    overflow: TextOverflow.ellipsis,
                    style: const TextStyle(
                      color: Colors.black54,
                      fontSize: 18.0,
                      fontWeight: FontWeight.w500,
                    ),
                  ),
                  const SizedBox(height: 5.0),
                  Row(
                    //crossAxisAlignment: Crossaxis,
                    children: [
                      const Icon(
                        Icons.credit_card,
                        size: 17.0,
                        color: Colors.blue,
                      ),
                      const SizedBox(width: 7.0),
                      Text(
                        controller.detailPanik.value.nik.toString(),
                        maxLines: 1,
                        softWrap: false,
                        overflow: TextOverflow.ellipsis,
                        style: const TextStyle(
                          color: Colors.black54,
                          fontSize: 13.0,
                          fontWeight: FontWeight.w500,
                        ),
                      ),
                    ],
                  ),
                  const SizedBox(height: 2.0),
                  Row(
                    //crossAxisAlignment: Crossaxis,
                    children: [
                      Icon(
                        controller.detailPanik.value.jenisKelamin == "LAKI-LAKI" ? Icons.male_sharp : Icons.female_sharp,
                        size: 17.0,
                        color: Colors.purple,
                      ),
                      const SizedBox(width: 7.0),
                      Text(
                        controller.detailPanik.value.jenisKelamin.toString(),
                        maxLines: 1,
                        softWrap: false,
                        overflow: TextOverflow.ellipsis,
                        style: const TextStyle(
                          color: Colors.black54,
                          fontSize: 13.0,
                          fontWeight: FontWeight.w500,
                        ),
                      ),
                    ],
                  ),
                  const SizedBox(height: 2.0),
                  Row(
                    //crossAxisAlignment: Crossaxis,
                    children: [
                      const Icon(
                        Icons.calendar_month_rounded,
                        size: 17.0,
                        color: Colors.orange,
                      ),
                      const SizedBox(width: 7.0),
                      Text(
                        controller.detailPanik.value.umur != null ? "${controller.detailPanik.value.umur} Tahun" : "-",
                        maxLines: 1,
                        softWrap: false,
                        overflow: TextOverflow.ellipsis,
                        style: const TextStyle(
                          color: Colors.black54,
                          fontSize: 13.0,
                          fontWeight: FontWeight.w500,
                        ),
                      ),
                    ],
                  ),
                  const SizedBox(height: 2.0),
                  GestureDetector(
                    onTap: () {
                      if (controller.detailPanik.value.noTelp == null) return;

                      var uri = Uri.parse("tel://${controller.detailPanik.value.noTelp}");
                      launchUrl(uri);
                    },
                    child: Row(
                      //crossAxisAlignment: Crossaxis,
                      children: [
                        const Icon(
                          Icons.phone,
                          size: 17.0,
                          color: Colors.green,
                        ),
                        const SizedBox(width: 7.0),
                        Text(
                          controller.detailPanik.value.noTelp != null ? "${controller.detailPanik.value.noTelp}" : "-",
                          maxLines: 1,
                          softWrap: false,
                          overflow: TextOverflow.ellipsis,
                          style: TextStyle(
                            color: controller.detailPanik.value.noTelp == null ? Colors.black54 : const Color.fromARGB(255, 0, 180, 6),
                            fontSize: 13.0,
                            fontWeight: FontWeight.w500,
                          ),
                        ),
                      ],
                    ),
                  ),
                  const SizedBox(height: 2.0),
                  Row(
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: [
                      const Icon(
                        Icons.location_on_outlined,
                        size: 17.0,
                        color: Colors.red,
                      ),
                      const SizedBox(width: 7.0),
                      Expanded(
                        child: Text(
                          "${controller.detailPanik.value.alamat}, Desa/Kel. ${controller.detailPanik.value.namaDesa}, Kec. ${controller.detailPanik.value.namaKecamatan}",
                          maxLines: 2,
                          softWrap: true,
                          overflow: TextOverflow.ellipsis,
                          style: const TextStyle(
                            color: Colors.black54,
                            fontSize: 13.0,
                            fontWeight: FontWeight.w500,
                          ),
                        ),
                      ),
                    ],
                  ),
                ],
              ),
            )
          ],
        ),
      ),
      const SizedBox(height: 25.0),
    ],
  );
}

Widget infoLaporan() {
  final controller = Get.find<ProsesController>();

  return Column(
    crossAxisAlignment: CrossAxisAlignment.start,
    children: [
      const Text(
        "Alamat",
        overflow: TextOverflow.ellipsis,
        style: TextStyle(
          color: Colors.black,
          fontSize: 15.0,
          fontWeight: FontWeight.w500,
        ),
      ),
      const SizedBox(height: 5.0),
      Container(
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
            Obx(
              (() {
                var text = "";
                if (controller.alamatOsm.value.displayName.toString() == "") {
                  text = "Alamat tidak tersedia.";
                } else {
                  text = controller.alamatOsm.value.displayName.toString();
                }

                return Text(
                  text,
                  maxLines: 100,
                  softWrap: false,
                  overflow: TextOverflow.ellipsis,
                  style: const TextStyle(
                    color: Colors.black54,
                    fontSize: 14.0,
                    fontWeight: FontWeight.w500,
                  ),
                );
              }),
            ),
            const SizedBox(height: 5.0),
            const Divider(),
            const SizedBox(height: 5.0),
            const Text(
              "*) Alamat Laporan diambil menggunakan 'Reverse Geocoding' sehingga alamat yang tertera bisa saja kurang tepat.",
              maxLines: 100,
              softWrap: false,
              overflow: TextOverflow.ellipsis,
              style: TextStyle(
                color: Colors.deepOrangeAccent,
                fontSize: 12.0,
                fontWeight: FontWeight.w500,
                fontStyle: FontStyle.italic,
              ),
            ),
          ],
        ),
      ),
      const SizedBox(height: 25.0),
    ],
  );
}

Widget terimaLaporan(BuildContext context) {
  final controller = Get.find<ProsesController>();

  return Column(
    crossAxisAlignment: CrossAxisAlignment.start,
    children: [
      const SizedBox(height: 25.0),
      SizedBox(
        width: double.infinity,
        child: Row(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            SizedBox(
              height: 40.0,
              width: 110.0,
              child: SleekButton(
                style: SleekButtonStyle.flat(
                  color: Colors.red,
                  context: context,
                ),
                child: Row(
                  mainAxisAlignment: MainAxisAlignment.center,
                  children: const [
                    Text('TOLAK'),
                  ],
                ),
                onTap: () {
                  controller.tolakLaporan(context);
                },
              ),
            ),
            const SizedBox(width: 35.0),
            SizedBox(
              height: 40.0,
              width: 110.0,
              child: SleekButton(
                style: SleekButtonStyle.flat(
                  color: Colors.blue,
                  context: context,
                ),
                child: Row(
                  mainAxisAlignment: MainAxisAlignment.center,
                  children: const [
                    Text('TERIMA'),
                  ],
                ),
                onTap: () {
                  controller.terimaLaporan(context);
                },
              ),
            ),
          ],
        ),
      ),
    ],
  );
}

Widget prosesLapporan(BuildContext context) {
  final controller = Get.find<ProsesController>();

  return Column(
    crossAxisAlignment: CrossAxisAlignment.start,
    children: [
      const Text(
        "Proses Laporan",
        overflow: TextOverflow.ellipsis,
        style: TextStyle(
          color: Colors.black,
          fontSize: 15.0,
          fontWeight: FontWeight.w500,
        ),
      ),
      const SizedBox(height: 5.0),
      Container(
        //width: double.infinity,
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
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            const SizedBox(height: 5.0),
            TextField(
              //enableInteractiveSelection: true,
              decoration: const InputDecoration(
                border: OutlineInputBorder(
                  // width: 0.0 produces a thin "hairline" border
                  borderRadius: BorderRadius.all(Radius.circular(10.0)),
                  borderSide: BorderSide.none,
                ),
                filled: true,
                fillColor: primaryColor,
                hintText: 'Judul Laporan',
              ),
              cursorColor: Colors.grey,
              autofocus: false,
              maxLines: null,
              maxLength: 100,

              controller: controller.textJudulLaporan,
              keyboardType: TextInputType.multiline,
              inputFormatters: [UpperCaseTextFormatter()],
            ),
            const SizedBox(height: 10.0),
            TextField(
              //enableInteractiveSelection: true,
              decoration: const InputDecoration(
                border: OutlineInputBorder(
                  // width: 0.0 produces a thin "hairline" border
                  borderRadius: BorderRadius.all(Radius.circular(10.0)),
                  borderSide: BorderSide.none,
                ),
                filled: true,
                fillColor: primaryColor,
                hintText: 'Uraian Laporan',
              ),
              cursorColor: Colors.grey,
              autofocus: false,
              maxLines: null,
              maxLength: 5000,
              controller: controller.textUraianLaporan,
              keyboardType: TextInputType.multiline,
              inputFormatters: [UpperCaseTextFormatter()],
            ),
            const SizedBox(height: 5.0),
            const Divider(),
            const SizedBox(height: 5.0),
            GestureDetector(
              onTap: () {
                controller.pickImage(context);
              },
              onLongPress: () {
                debugPrint("OK, LONG PRESSED");
              },
              child: Row(
                crossAxisAlignment: CrossAxisAlignment.start,
                children: const [
                  Icon(
                    Icons.attach_file_outlined,
                    size: 17.0,
                    color: Colors.blue,
                  ),
                  SizedBox(width: 7.0),
                  Text(
                    "Lampirkan Foto",
                    maxLines: 1,
                    softWrap: false,
                    overflow: TextOverflow.ellipsis,
                    style: TextStyle(
                      color: Colors.blue,
                      fontSize: 13.0,
                      fontWeight: FontWeight.w500,
                    ),
                  ),
                ],
              ),
            ),
            const SizedBox(height: 5.0),
            const Divider(),
            const SizedBox(height: 20.0),
            Row(
              mainAxisAlignment: MainAxisAlignment.center,
              children: [
                SizedBox(
                  height: 40.0,
                  width: 110.0,
                  child: SleekButton(
                    style: SleekButtonStyle.flat(
                      color: Colors.red,
                      context: context,
                    ),
                    child: Row(
                      mainAxisAlignment: MainAxisAlignment.center,
                      children: const [
                        Text('BATAL'),
                      ],
                    ),
                    onTap: () {
                      //Get.back();
                      controller.prosesLaporan(context, "2", "B");
                    },
                  ),
                ),
                const SizedBox(width: 35.0),
                SizedBox(
                  height: 40.0,
                  width: 110.0,
                  child: SleekButton(
                    style: SleekButtonStyle.flat(
                      color: Colors.blue,
                      context: context,
                    ),
                    child: Row(
                      mainAxisAlignment: MainAxisAlignment.center,
                      children: const [
                        Text('SIMPAN'),
                      ],
                    ),
                    onTap: () {
                      controller.prosesLaporan(context, "2", "S");
                    },
                  ),
                ),
              ],
            ),
            const SizedBox(height: 20.0),
          ],
        ),
      ),
    ],
  );
}
