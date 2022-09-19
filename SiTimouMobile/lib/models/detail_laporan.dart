// To parse this JSON data, do
//
//     final detailLaporan = detailLaporanFromJson(jsonString);

import 'dart:convert';

DetailLaporan detailLaporanFromJson(String str) => DetailLaporan.fromJson(json.decode(str));

String detailLaporanToJson(DetailLaporan data) => json.encode(data.toJson());

class DetailLaporan {
  DetailLaporan({
    this.tanggalLong,
    this.tanggalShort,
    this.tentang,
    this.isiLaporan,
    this.gpsLat,
    this.gpsLng,
    this.laporanId,
    this.userId,
    this.flg,
  });

  String? tanggalLong;
  DateTime? tanggalShort;
  String? tentang;
  String? isiLaporan;
  double? gpsLat;
  double? gpsLng;
  int? laporanId;
  int? userId;
  String? flg;

  factory DetailLaporan.fromJson(Map<String, dynamic> json) => DetailLaporan(
        tanggalLong: json["tanggal_long"],
        tanggalShort: DateTime.parse(json["tanggal_short"]),
        tentang: json["tentang"],
        isiLaporan: json["isi_laporan"],
        gpsLat: json["gps_lat"].toDouble(),
        gpsLng: json["gps_lng"].toDouble(),
        laporanId: json["laporan_id"],
        userId: json["user_id"],
        flg: json["flg"],
      );

  Map<String, dynamic> toJson() => {
        "tanggal_long": tanggalLong,
        "tanggal_short": tanggalShort,
        "tentang": tentang,
        "isi_laporan": isiLaporan,
        "gps_lat": gpsLat,
        "gps_lng": gpsLng,
        "laporan_id": laporanId,
        "user_id": userId,
        "flg": flg,
      };
}
