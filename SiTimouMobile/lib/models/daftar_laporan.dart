// To parse this JSON data, do
//
//     final daftarLaporan = daftarLaporanFromJson(jsonString);

import 'dart:convert';

List<DaftarLaporan> daftarLaporanFromJson(String str) => List<DaftarLaporan>.from(json.decode(str).map((x) => DaftarLaporan.fromJson(x)));

String daftarLaporanToJson(List<DaftarLaporan> data) => json.encode(List<dynamic>.from(data.map((x) => x.toJson())));

class DaftarLaporan {
  DaftarLaporan({
    this.tanggal,
    this.namaPelapor,
    this.tentang,
    this.isiLaporan,
    this.tanggalShort,
    this.gpsLat,
    this.gpsLng,
    this.flg,
    this.laporanId,
  });

  String? tanggal;
  String? namaPelapor;
  String? tentang;
  String? isiLaporan;
  DateTime? tanggalShort;
  double? gpsLat;
  double? gpsLng;
  String? flg;
  int? laporanId;

  factory DaftarLaporan.fromJson(Map<String, dynamic> json) => DaftarLaporan(
        tanggal: json["Tanggal"],
        namaPelapor: json["Nama Pelapor"],
        tentang: json["Tentang"],
        isiLaporan: json["Isi Laporan"],
        tanggalShort: DateTime.parse(json["tanggal_short"]),
        gpsLat: json["gps_lat"].toDouble(),
        gpsLng: json["gps_lng"].toDouble(),
        flg: json["flg"],
        laporanId: json["laporan_id"],
      );

  Map<String, dynamic> toJson() => {
        "Tanggal": tanggal,
        "Nama Pelapor": namaPelapor,
        "Tentang": tentang,
        "Isi Laporan": isiLaporan,
        "tanggal_short": tanggalShort,
        "gps_lat": gpsLat,
        "gps_lng": gpsLng,
        "flg": flg,
        "laporan_id": laporanId,
      };
}
