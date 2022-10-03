import 'package:firebase_core/firebase_core.dart';
import 'package:firebase_messaging/firebase_messaging.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:get/get.dart';
import 'package:sitimoufr/services/fb_messaging_services.dart';
import 'package:sitimoufr/services/local_notification_services.dart';
import 'package:sitimoufr/views/splash_screen.dart';

// FCM
MessagingService _msgService = MessagingService();
LocalNotificationService _notificationService = LocalNotificationService();

void main() async {
  WidgetsFlutterBinding.ensureInitialized();

  // Firebase
  await Firebase.initializeApp();
  FirebaseMessaging.onBackgroundMessage(_firebaseMessagingBackgroundHandler);
  await _msgService.init();
  // AwesomeNotificationService().init();
  LocalNotificationService().init();
  runApp(const SITIMOU());
}

/// Top level function to handle incoming messages when the app is in the background
Future<void> _firebaseMessagingBackgroundHandler(RemoteMessage message) async {
  print(" --- background message received ---");
  print(message.data['title']);
  print(message.data['body']);
  print(message.data['userid']);

  _notificationService.notificationCustomSound(message.data['title'], message.data['body']);
  // _awesomeNotificationService.showNotification(message.data['title'], message.data['body']);
}

class SITIMOU extends StatelessWidget {
  const SITIMOU({Key? key}) : super(key: key);

  // This widget is the root of your application.
  @override
  Widget build(BuildContext context) {
    SystemChrome.setPreferredOrientations([
      DeviceOrientation.portraitUp,
      DeviceOrientation.portraitDown,
    ]);

    return GetMaterialApp(
      debugShowCheckedModeBanner: false,
      title: 'SI-TIMOU Responder',
      theme: ThemeData(
        // primarySwatch: Colors.blue,
        visualDensity: VisualDensity.adaptivePlatformDensity,
      ),
      home: const SplashScreen(),
    );
  }
}
