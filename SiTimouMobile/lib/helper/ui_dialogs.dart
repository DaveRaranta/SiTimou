import 'package:flutter/material.dart';
import 'package:cool_alert/cool_alert.dart';

import 'package:sitimou/widgets/dialog/progress_dialog.dart';

ProgressDialog? pr;

void messageBox(BuildContext context, CoolAlertType type, String title, String message) {
  CoolAlert.show(
    context: context,
    type: type,
    title: title,
    text: message,
    barrierDismissible: false,
  );
}

void messageBoxAction(BuildContext context, CoolAlertType type, String title, String message, VoidCallback? okAction) {
  CoolAlert.show(
    context: context,
    type: type,
    title: title,
    text: message,
    barrierDismissible: false,
    confirmBtnText: "OK",
    onConfirmBtnTap: okAction,
  );
}

void questionBox(BuildContext context, String title, String message, VoidCallback okAction,
    [VoidCallback? cancelAction, String okText = 'OK', String cancelText = 'Batal']) {
  CoolAlert.show(
    context: context,
    type: CoolAlertType.confirm,
    title: title,
    text: message,
    barrierDismissible: false,
    confirmBtnText: okText,
    cancelBtnText: cancelText,
    onConfirmBtnTap: okAction,
    onCancelBtnTap: cancelAction,
  );
}

ProgressDialog progressDialog(BuildContext context, [String pesan = "Harap Menunggu..."]) {
  pr = ProgressDialog(context, type: ProgressDialogType.Normal, isDismissible: false);
  pr!.style(
    message: pesan,
    messageTextStyle: const TextStyle(color: Colors.black, fontSize: 20.0, fontWeight: FontWeight.w400),
  );

  return pr!;
}
