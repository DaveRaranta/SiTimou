import 'dart:typed_data';
import 'package:flutter/material.dart';
import 'package:flutter_local_notifications/flutter_local_notifications.dart';
import 'package:get/get.dart';
import 'package:sitimoufr/helper/apphelper.dart';
import 'package:timezone/data/latest.dart' as tz;
import 'package:timezone/timezone.dart' as tz;

class LocalNotificationService {
  //
  //NotificationService a singleton object
  //
  static final LocalNotificationService _notificationService = LocalNotificationService._internal();

  factory LocalNotificationService() {
    return _notificationService;
  }

  LocalNotificationService._internal();

  //static const channelId = '123';

  late AndroidNotificationChannel channel;

  //bool isFlutterLocalNotificationsInitialized = false;

  final FlutterLocalNotificationsPlugin flutterLocalNotificationsPlugin = FlutterLocalNotificationsPlugin();

  Future<void> init() async {
    const AndroidInitializationSettings initializationSettingsAndroid = AndroidInitializationSettings('/raw/app_normal');

    const IOSInitializationSettings initializationSettingsIOS = IOSInitializationSettings(
      requestSoundPermission: false,
      requestBadgePermission: false,
      requestAlertPermission: false,
    );

    const InitializationSettings initializationSettings =
        InitializationSettings(android: initializationSettingsAndroid, iOS: initializationSettingsIOS, macOS: null);

    tz.initializeTimeZones();

    await flutterLocalNotificationsPlugin.initialize(initializationSettings, onSelectNotification: selectNotification);
  }

  //
  // NOTIFICATION BUILDER
  //

  Future<void> notificationDefaultSound(String title, String body) async {
    final Int64List vibrationPattern = Int64List(4);
    vibrationPattern[0] = 0;
    vibrationPattern[1] = 1000;
    vibrationPattern[2] = 5000;
    vibrationPattern[3] = 2000;

    BigTextStyleInformation bigTextStyleInformation = BigTextStyleInformation(
      body,
      htmlFormatBigText: true,
      contentTitle: '<b>$title</b>',
      htmlFormatContentTitle: true,
      htmlFormatTitle: true,
      htmlFormatSummaryText: true,
    );

    var androidPlatformChannelSpecifics = AndroidNotificationDetails(
      'sitimou_default',
      'sitimou-fr',
      importance: Importance.max,
      priority: Priority.high,
      icon: '/raw/app_normal',
      color: const Color.fromARGB(255, 255, 0, 0),
      enableLights: true,
      ledColor: const Color.fromARGB(255, 255, 0, 0),
      ledOnMs: 1000,
      ledOffMs: 500,
      playSound: true,
      largeIcon: const DrawableResourceAndroidBitmap('raw/app_round'),
      vibrationPattern: vibrationPattern,
      colorized: true,
      styleInformation: bigTextStyleInformation,
    );

    var iOSPlatformChannelSpecifics = const IOSNotificationDetails();

    var platformChannelSpecifics = NotificationDetails(
      android: androidPlatformChannelSpecifics,
      iOS: iOSPlatformChannelSpecifics,
    );

    flutterLocalNotificationsPlugin.show(
      0,
      '<b>$title</b>',
      '',
      platformChannelSpecifics,
      payload: 'Default Sound',
    );
  }

  Future<void> notificationCustomSound(String title, String body, [int userId = 0]) async {
    // bool isUser = userId > 0;
    // Vibrator Settings
    final Int64List vibrationPattern = Int64List(4);
    vibrationPattern[0] = 0;
    vibrationPattern[1] = 1000;
    vibrationPattern[2] = 5000;
    vibrationPattern[3] = 2000;

    // Bigtext untuk Title and body
    BigTextStyleInformation bigTextStyleInformation = BigTextStyleInformation(
      body,
      htmlFormatBigText: true,
      contentTitle: '<b>$title</b>',
      htmlFormatContentTitle: true,
      htmlFormatTitle: true,
      htmlFormatSummaryText: true,
    );

    // Large icon
    final String largeIcon;
    largeIcon = await AppHelper.base64encodedImageFromAsset('assets/images/logo/app_logo_512px.png');

    var androidPlatformChannelSpecifics = AndroidNotificationDetails(
      'sitimou_default',
      'sitimou-fr',
      importance: Importance.max,
      priority: Priority.high,
      icon: '/raw/app_normal',
      color: const Color.fromARGB(255, 255, 0, 0),
      enableLights: true,
      ledColor: const Color.fromARGB(255, 255, 0, 0),
      ledOnMs: 1000,
      ledOffMs: 500,
      playSound: true,
      sound: const RawResourceAndroidNotificationSound('notif_sound'),
      largeIcon: ByteArrayAndroidBitmap.fromBase64String(largeIcon), // : DrawableResourceAndroidBitmap('raw/app_round'),
      vibrationPattern: vibrationPattern,
      colorized: true,
      styleInformation: bigTextStyleInformation, //DefaultStyleInformation(false, true),
    );

    var iOSPlatformChannelSpecifics = const IOSNotificationDetails(sound: 'notif_sound.m4a');

    var platformChannelSpecifics = NotificationDetails(
      android: androidPlatformChannelSpecifics,
      iOS: iOSPlatformChannelSpecifics,
    );

    flutterLocalNotificationsPlugin.show(
      1,
      '<b>$title</b>',
      body,
      platformChannelSpecifics,
      payload: 'Custom Sound',
    );
  }

  Future<void> getxNotification(String title, String body) async {
    Get.snackbar(
      title,
      body,
      backgroundColor: Colors.black.withOpacity(0.5),
      colorText: Colors.white,
      icon: Padding(
        padding: const EdgeInsets.all(8.0),
        child: Image.asset(
          "assets/images/logo/app_logo_512px.png",
          height: 28.0,
          filterQuality: FilterQuality.high,
          fit: BoxFit.fill,
        ),
      ),
      borderRadius: 10.0,
      duration: const Duration(seconds: 5),
      margin: const EdgeInsets.all(5),
    );
  }

  //
  // NOT USED
  //
  //

  AndroidNotificationDetails _androidNotificationDetails = AndroidNotificationDetails(
    'high_importance_channel',
    'MHDoc2',
    playSound: true,
    priority: Priority.high,
    importance: Importance.high,
  );

  Future<void> showNotifications() async {
    await flutterLocalNotificationsPlugin.show(
      0,
      "Notification Title",
      "This is the Notification Body!",
      NotificationDetails(android: _androidNotificationDetails),
    );
  }

  Future<void> scheduleNotifications() async {
    await flutterLocalNotificationsPlugin.zonedSchedule(0, "Notification Title", "This is the Notification Body!",
        tz.TZDateTime.now(tz.local).add(const Duration(seconds: 5)), NotificationDetails(android: _androidNotificationDetails),
        androidAllowWhileIdle: true, uiLocalNotificationDateInterpretation: UILocalNotificationDateInterpretation.absoluteTime);
  }

  Future<void> cancelNotifications(int id) async {
    await flutterLocalNotificationsPlugin.cancel(id);
  }

  Future<void> cancelAllNotifications() async {
    await flutterLocalNotificationsPlugin.cancelAll();
  }
}

Future selectNotification(String? payload) async {
  //handle your logic here
}
