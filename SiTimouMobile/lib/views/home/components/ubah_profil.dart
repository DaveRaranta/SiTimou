import 'package:dropdown_button2/dropdown_button2.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:flutter_datetime_picker/flutter_datetime_picker.dart';
import 'package:get/get.dart';
import 'package:intl/intl.dart';
import 'package:sitimou/controllers/home_controller.dart';
import 'package:sitimou/helper/colors.dart';
import 'package:sitimou/helper/scroll_settings.dart';
import 'package:sitimou/helper/text_helper.dart';
import 'package:sitimou/widgets/appbar/app_bar.dart';
import 'package:sitimou/widgets/panel/round_container.dart';
import 'package:sitimou/widgets/textfield/fake_label_textfield.dart';
import 'package:sitimou/widgets/textfield/label_textfield.dart';

class UbahProfilPage extends StatelessWidget {
  UbahProfilPage({super.key});

  final controller = Get.find<HomeController>();

  @override
  Widget build(BuildContext context) {
    return MediaQuery(
      data: MediaQuery.of(context).copyWith(textScaleFactor: 1.0),
      child: GestureDetector(
        onTap: () => FocusScope.of(context).unfocus(),
        child: Scaffold(
          backgroundColor: primaryColor,
          appBar: appBar(context,
              titleText: "UBAH PROFIL",
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
                Icons.check_rounded,
                color: Colors.black54,
              ),
              tailingFunction: () {
                debugPrint("SIMPAN");
                controller.updateProfil(context);
                //controller.hapusDetailLaporan(context, "1");
              }),
          body: SafeArea(
            child: SizedBox(
              height: double.infinity,
              width: double.infinity,
              child: Padding(
                padding: const EdgeInsets.only(top: 0, left: 18.0, right: 18.0),
                child: ScrollConfiguration(
                    behavior: ScrollSettings(),
                    child: SingleChildScrollView(
                      child: Column(
                        crossAxisAlignment: CrossAxisAlignment.start,
                        children: [
                          const SizedBox(height: 25),
                          TextFieldWithLabel(
                            controller: controller.textEditNik,
                            labelText: "NIK Pengguna",
                            labelFontSize: 14.0,
                            hintText: "NIK PENGGUNA",
                            inputFormatters: [FilteringTextInputFormatter.digitsOnly],
                            inputType: TextInputType.number,
                            maxLenght: 50,
                          ),
                          const SizedBox(height: 20.0),
                          TextFieldWithLabel(
                            controller: controller.textEditNama,
                            labelText: "Nama Pengguna",
                            labelFontSize: 14.0,
                            hintText: "NAMA PENGGUNA",
                            inputFormatters: [UpperCaseTextFormatter()],
                            maxLenght: 50,
                          ),
                          const SizedBox(height: 20.0),
                          //
                          // Jenis Kelamin
                          //
                          const Text(
                            "Jenis Kelamin",
                            style: TextStyle(
                              fontSize: 14,
                              fontWeight: FontWeight.w600,
                              color: Colors.black54,
                            ),
                          ),
                          const SizedBox(height: 7.0),
                          RoundedSmallContainer(
                            child: DropdownButtonHideUnderline(
                              child: Obx(
                                () => DropdownButton2(
                                  dropdownDecoration: BoxDecoration(
                                    borderRadius: BorderRadius.circular(10),
                                    color: Colors.white,
                                  ),
                                  dropdownElevation: 8,
                                  hint: const Text(
                                    "JENIS KELAMIN",
                                    style: TextStyle(color: Colors.grey),
                                  ),
                                  onChanged: (value) {
                                    debugPrint("VALUE OPD: ${value.toString()}");
                                    controller.jkSelected(value.toString());
                                  },
                                  isExpanded: true,
                                  isDense: true,
                                  value: controller.jkValue.value == "" ? null : controller.jkValue.value,
                                  items: controller.jkItems.map((items) {
                                    return DropdownMenuItem(
                                      value: items,
                                      child: Text(
                                        items.toString(),
                                        style: const TextStyle(color: Colors.black, fontSize: 15.0),
                                        overflow: TextOverflow.ellipsis,
                                      ),
                                    );
                                  }).toList(),
                                ),
                              ),
                            ),
                          ),
                          const SizedBox(height: 20.0),
                          //
                          // Tanggal Lahir
                          //
                          Obx(
                            () => GestureDetector(
                              child: FakeTextFieldWithLabel(
                                labelFontSize: 14.0,
                                labelText: "Tanggal Lahir",
                                fakeText: controller.formatTanggalLahir(),
                              ),
                              onTap: () {
                                DatePicker.showDatePicker(
                                  context,
                                  showTitleActions: true,
                                  minTime: DateTime(1920),
                                  maxTime: DateTime.now(),
                                  onChanged: (d) {
                                    debugPrint("DATEPICKER ONCHANGE: $d");
                                  },
                                  onConfirm: (d) {
                                    final f = DateFormat('yyyy-MM-dd');
                                    controller.tanggalLahir.value = f.format(d);
                                  },
                                  currentTime: controller.detailPengguna.value.tanggalLahir == "-"
                                      ? DateTime.now()
                                      : DateTime.parse(controller.detailPengguna.value.rawTanggalLahir.toString()),
                                  locale: LocaleType.id,
                                );
                              },
                            ),
                          ),
                          const SizedBox(height: 20.0),
                          TextFieldWithLabel(
                            controller: controller.textEditAlamat,
                            labelText: "Alamat",
                            labelFontSize: 14.0,
                            hintText: "ALAMAT",
                            inputFormatters: [UpperCaseTextFormatter()],
                            maxLenght: 150,
                          ),
                          const SizedBox(height: 20.0),
                          //
                          // Kecamatan
                          //
                          const Text(
                            "Kecamatan",
                            style: TextStyle(
                              fontSize: 14,
                              fontWeight: FontWeight.w600,
                              color: Colors.black54,
                            ),
                          ),
                          const SizedBox(height: 7.0),
                          RoundedSmallContainer(
                            child: DropdownButtonHideUnderline(
                              child: Obx(
                                () => DropdownButton2(
                                  dropdownDecoration: BoxDecoration(
                                    borderRadius: BorderRadius.circular(10),
                                    color: Colors.white,
                                  ),
                                  dropdownElevation: 8,
                                  hint: const Text(
                                    "KECAMATAN",
                                    style: TextStyle(color: Colors.grey),
                                  ),
                                  onChanged: (value) {
                                    debugPrint("ID KECAMATAN: $value");
                                    controller.idKecamatan.value = int.parse(value.toString());
                                    // Load Desa..
                                    controller.getListDesa(int.parse(value.toString()));
                                  },
                                  isExpanded: true,
                                  isDense: true,
                                  value: controller.idKecamatan.value != 0 ? controller.idKecamatan.value : null,
                                  items: controller.listKecamatan.map((items) {
                                    return DropdownMenuItem(
                                      value: items!.kecamatanId,
                                      child: Text(
                                        items.namaKecamatan.toString(),
                                        style: const TextStyle(color: Colors.black, fontSize: 15.0),
                                        overflow: TextOverflow.ellipsis,
                                      ),
                                    );
                                  }).toList(),
                                ),
                              ),
                            ),
                          ),
                          const SizedBox(height: 20.0),
                          //
                          // Desa
                          //
                          const Text(
                            "Desa/Kelurahan",
                            style: TextStyle(
                              fontSize: 14,
                              fontWeight: FontWeight.w600,
                              color: Colors.black54,
                            ),
                          ),
                          const SizedBox(height: 7.0),
                          RoundedSmallContainer(
                            child: DropdownButtonHideUnderline(
                              child: Obx(
                                () => DropdownButton2(
                                  dropdownDecoration: BoxDecoration(
                                    borderRadius: BorderRadius.circular(10),
                                    color: Colors.white,
                                  ),
                                  dropdownElevation: 8,
                                  hint: const Text(
                                    "DESA/KELURAHAN",
                                    style: TextStyle(color: Colors.grey),
                                  ),
                                  onChanged: (value) {
                                    debugPrint("ID KECAMATAN: $value");
                                    controller.idDesa.value = int.parse(value.toString());
                                  },
                                  isExpanded: true,
                                  isDense: true,
                                  value: controller.idDesa.value != 0 ? controller.idDesa.value : null,
                                  items: controller.listDesa.map((items) {
                                    return DropdownMenuItem(
                                      value: items!.desaId,
                                      child: Text(
                                        items.namaDesa.toString(),
                                        style: const TextStyle(color: Colors.black, fontSize: 15.0),
                                        overflow: TextOverflow.ellipsis,
                                      ),
                                    );
                                  }).toList(),
                                ),
                              ),
                            ),
                          ),
                          const SizedBox(height: 20.0),
                          TextFieldWithLabel(
                            controller: controller.textEditNoTelp,
                            labelText: "No. Telp",
                            labelFontSize: 14.0,
                            hintText: "NOMOR TELP",
                            inputFormatters: [FilteringTextInputFormatter.digitsOnly],
                            inputType: TextInputType.number,
                            maxLenght: 14,
                          ),
                          const SizedBox(height: 40.0),
                        ],
                      ),
                    )),
              ),
            ),
          ),
        ),
      ),
    );
  }
}
