import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:get/get.dart';
import 'package:fluentui_system_icons/fluentui_system_icons.dart';

import 'package:sitimou/controllers/auth_controller.dart';
import 'package:sitimou/helper/colors.dart';
import 'package:sitimou/helper/scroll_settings.dart';
import 'package:sitimou/helper/text_helper.dart';
import 'package:sitimou/widgets/textfield/label_textfield.dart';
import 'package:sleek_button/sleek_button.dart';

class DaftarPenggunaPage extends StatelessWidget {
  DaftarPenggunaPage({Key? key}) : super(key: key);

  final AuthController _controller = Get.find<AuthController>();

  @override
  Widget build(BuildContext context) {
    return MediaQuery(
      data: MediaQuery.of(context).copyWith(textScaleFactor: 1.0),
      child: GestureDetector(
        onTap: () => FocusScope.of(context).unfocus(),
        child: Scaffold(
          backgroundColor: primaryColor,
          body: SafeArea(
            child: Padding(
              padding: const EdgeInsets.only(top: 0, left: 18.0, right: 18.0),
              child: Center(
                child: SizedBox(
                  child: ScrollConfiguration(
                    behavior: ScrollSettings(),
                    child: SingleChildScrollView(
                      child: Column(
                        crossAxisAlignment: CrossAxisAlignment.start,
                        children: [
                          const Center(
                            child: Icon(
                              FluentIcons.document_person_20_regular,
                              size: 64.0,
                              color: Colors.black87,
                            ),
                          ),
                          const SizedBox(height: 20.0),
                          const Center(
                            child: Text(
                              "DAFTAR PENGGUNA",
                              style: TextStyle(
                                color: Colors.black87,
                                fontSize: 20.0,
                                fontWeight: FontWeight.w500,
                              ),
                            ),
                          ),
                          const SizedBox(height: 70.0),
                          TextFieldWithLabel(
                            controller: _controller.textRegNik,
                            labelText: "NIK Pengguna",
                            hintText: "NIK PENGGUNA",
                            inputFormatters: [FilteringTextInputFormatter.digitsOnly],
                            inputType: TextInputType.number,
                            maxLenght: 50,
                          ),
                          const SizedBox(height: 20.0),
                          TextFieldWithLabel(
                            controller: _controller.textRegNama,
                            labelText: "Nama Pengguna",
                            hintText: "NAMA PENGGUNA",
                            inputFormatters: [UpperCaseTextFormatter()],
                            maxLenght: 150,
                          ),
                          const SizedBox(height: 20.0),
                          TextFieldWithLabel(
                            controller: _controller.textRegPwd,
                            labelText: "Password",
                            hintText: "PASSWORD",
                            inputFormatters: const [],
                            maxLenght: 50,
                            obscureText: true,
                          ),
                          const SizedBox(height: 20.0),
                          TextFieldWithLabel(
                            controller: _controller.textRegConfirm,
                            labelText: "Konfirmasi Password",
                            hintText: "KONFIRMASI PASSWORD",
                            inputFormatters: const [],
                            maxLenght: 50,
                            obscureText: true,
                          ),
                          const SizedBox(height: 40.0),
                          Row(
                            mainAxisAlignment: MainAxisAlignment.center,
                            children: [
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
                                      Text('DAFTAR'),
                                    ],
                                  ),
                                  onTap: () {
                                    // debugPrint(tipe);
                                    // _controller.updateProfile(context, tipe);
                                    _controller.saveRegistrasi(context);
                                  },
                                ),
                              ),
                              const SizedBox(width: 35.0),
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
                                    Get.back();
                                  },
                                ),
                              ),
                            ],
                          ),
                          const SizedBox(height: 20.0),
                        ],
                      ),
                    ),
                  ),
                ),
              ),
            ),
          ),
        ),
      ),
    );
  }
}
