import 'package:fluentui_system_icons/fluentui_system_icons.dart';
import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:google_maps_flutter/google_maps_flutter.dart';
import 'package:sitimoufr/controllers/proses_controller.dart';
import 'package:sitimoufr/helper/colors.dart';
import 'package:sitimoufr/widgets/appbar/app_bar.dart';

class LokasiLaporan extends StatelessWidget {
  const LokasiLaporan({super.key});

  @override
  Widget build(BuildContext context) {
    return MediaQuery(
      data: MediaQuery.of(context).copyWith(textScaleFactor: 1.0),
      child: Scaffold(
        appBar: appBar(
          context,
          titleText: "LOKASI LAPORAN",
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
        ),
        body: SafeArea(
          child: body(context),
        ),
      ),
    );
  }
}

Widget body(BuildContext context) {
  final controller = Get.find<ProsesController>();
  final Set<Marker> markers = {};
  // GoogleMapController _mapController;

  // Map data
  LatLng location = LatLng(controller.gpsLat, controller.gpsLng);
  //double.parse(controller.gpsLat.toString()),
  //double.parse(controller.gpsLng.toString()),

  markers.add(
    Marker(
      markerId: const MarkerId("1"),
      position: location,
      infoWindow: const InfoWindow(
        title: "Lokasi Laporan",
      ),
      //icon: _controller.markerIcon!,
    ),
  );

  return Stack(
    children: [
      GoogleMap(
        initialCameraPosition: CameraPosition(
          target: location,
          zoom: 15.0,
        ),
        onMapCreated: (c) {
          //_mapController = c;
        },
        markers: markers,
        zoomControlsEnabled: true,
      ),
    ],
  );
}
