import 'package:firebase_messaging/firebase_messaging.dart';
import 'package:flutter/material.dart';

import 'package:sitimoufr/services/local_notification_services.dart';
import 'package:sitimoufr/helper/globals.dart' as g;

class MessagingService {
  final FirebaseMessaging _firebaseMessaging = FirebaseMessaging.instance;
  final LocalNotificationService _notificationService = LocalNotificationService();

  String? _token;
  String? get token => _token;

  Future init() async {
    final settings = await _requestPermission();

    if (settings.authorizationStatus == AuthorizationStatus.authorized) {
      await _getToken();
      _registerForegroundMessageHandler();
    }
  }

  Future _getToken() async {
    _token = await _firebaseMessaging.getToken();

    debugPrint("FCM: $_token");

    _firebaseMessaging.onTokenRefresh.listen((token) {
      _token = token;
    });

    // Simpan/update FCM Token
    g.userFcmToken = _token;
    debugPrint("FCM2: ${g.userFcmToken}");
  }

  Future<NotificationSettings> _requestPermission() async {
    return await _firebaseMessaging.requestPermission(
        alert: true, badge: true, sound: true, carPlay: false, criticalAlert: false, provisional: false, announcement: false);
  }

  void _registerForegroundMessageHandler() {
    FirebaseMessaging.onMessage.listen((message) {
      print(" --- foreground message received ---");
      print(message.data['title']);
      print(message.data['body']);
      print(message.data['userid']);

      // TODO: NULL CHECK untuk message

      // _awesomeNotificationService.showNotification(remoteMessage.data['title'], remoteMessage.data['body']);
      _notificationService.notificationCustomSound(message.data['title'], message.data['body'], int.parse(message.data['userid']));
    });
  }

  //
  // Notification Builder
  //
}
