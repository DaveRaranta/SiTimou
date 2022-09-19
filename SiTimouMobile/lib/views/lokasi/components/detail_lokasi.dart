import 'package:fluentui_system_icons/fluentui_system_icons.dart';
import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:google_maps_flutter/google_maps_flutter.dart';
import 'package:sitimou/controllers/lokasi_controller.dart';
import 'package:sitimou/helper/colors.dart';
import 'package:sitimou/widgets/appbar/app_bar.dart';

class DetailLokasiPage extends StatelessWidget {
  const DetailLokasiPage({
    super.key,
  });

  //final LokasiController controller = Get.find<LokasiController>();

  @override
  Widget build(BuildContext context) {
    return Material(
      child: MediaQuery(
        data: MediaQuery.of(context).copyWith(textScaleFactor: 1.0),
        child: Scaffold(
          appBar: appBar(
            context,
            titleText: "DETAIL LOKASI",
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
      ),
    );
  }
}

Widget body(BuildContext context) {
  final LokasiController controller = Get.find<LokasiController>();
  final Set<Marker> markers = {};
  // GoogleMapController _mapController;

  // Map data
  LatLng location = LatLng(
    double.parse(controller.detailLokasi.value.gpsLat.toString()),
    double.parse(controller.detailLokasi.value.gpsLng.toString()),
  );

  markers.add(
    Marker(
      markerId: const MarkerId("1"),
      position: location,
      infoWindow: InfoWindow(
        title: "Nama Lokasi",
        snippet: controller.detailLokasi.value.namaLokasi,
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
        zoomControlsEnabled: false,
      ),
      Padding(
        padding: const EdgeInsets.only(top: 15, left: 16.0, right: 16.0),
        child: Container(
          height: 125.0,
          width: double.infinity,
          margin: const EdgeInsets.symmetric(vertical: 0),
          padding: const EdgeInsets.symmetric(horizontal: 10, vertical: 1),
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
              const SizedBox(height: 7.0),
              GestureDetector(
                child: Text(
                  controller.detailLokasi.value.namaLokasi.toString(),
                  maxLines: 1,
                  softWrap: false,
                  overflow: TextOverflow.ellipsis,
                  style: const TextStyle(
                    fontSize: 18.0,
                    fontWeight: FontWeight.w600,
                    color: Colors.black54,
                  ),
                ),
                onTap: () {
                  // toastPesan(context, _controller.detailEvent.value.namaEvent.toString());
                },
              ),
              const SizedBox(height: 1),
              Text(
                "${controller.detailLokasi.value.alamat.toString().toUpperCase()}, ${controller.detailLokasi.value.desa.toString().toUpperCase()} - ${controller.detailLokasi.value.kecamatan.toString().toUpperCase()}",
                maxLines: 1,
                softWrap: false,
                overflow: TextOverflow.ellipsis,
                style: const TextStyle(
                  fontSize: 13.0,
                  fontWeight: FontWeight.w500,
                  color: Colors.black54,
                ),
              ),
              const SizedBox(height: 1),
              Text(
                "Telp: ${controller.detailLokasi.value.noTelp}",
                maxLines: 1,
                softWrap: false,
                overflow: TextOverflow.ellipsis,
                style: const TextStyle(
                  fontSize: 13.0,
                  fontWeight: FontWeight.w500,
                  color: Colors.black54,
                  fontStyle: FontStyle.italic,
                ),
              ),
              const SizedBox(height: 7),
              Text(
                "* ${controller.detailLokasi.value.keterangan}",
                maxLines: 1,
                softWrap: false,
                overflow: TextOverflow.ellipsis,
                style: const TextStyle(
                  fontSize: 13.0,
                  fontWeight: FontWeight.w500,
                  color: Colors.black54,
                  fontStyle: FontStyle.italic,
                ),
              ),
              const SizedBox(height: 10),
              GestureDetector(
                onTap: () {
                  controller.callPhone();
                },
                child: SizedBox(
                  child: Row(
                    //mainAxisAlignment: MainAxisAlignment.center,
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: const [
                      SizedBox(width: 10.0),
                      Icon(
                        Icons.phone,
                        color: Colors.green,
                        size: 21.0,
                      ),
                      SizedBox(width: 7.0),
                      Text(
                        "PANGGIL",
                        style: TextStyle(
                          color: Colors.green,
                          fontWeight: FontWeight.w600,
                          fontSize: 14.0,
                        ),
                      )
                    ],
                  ),
                ),
              )
            ],
          ),
        ),
      ),
    ],
  );
}
