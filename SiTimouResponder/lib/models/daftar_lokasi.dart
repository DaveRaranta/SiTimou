// To parse this JSON data, do
//
//     final daftarLokasi = daftarLokasiFromJson(jsonString);

import 'dart:convert';

List<DaftarLokasi> daftarLokasiFromJson(String str) => List<DaftarLokasi>.from(json.decode(str).map((x) => DaftarLokasi.fromJson(x)));

String daftarLokasiToJson(List<DaftarLokasi> data) => json.encode(List<dynamic>.from(data.map((x) => x.toJson())));

class DaftarLokasi {
  DaftarLokasi({
    this.namaLokasi,
    this.alamat,
    this.desa,
    this.kecamatan,
    this.noTelp,
    this.jenisLokasi,
    this.keterangan,
    this.lokasiId,
    this.daftarLokasiJenisLokasi,
    this.gpsLat,
    this.gpsLng,
  });

  String? namaLokasi;
  String? alamat;
  String? desa;
  String? kecamatan;
  String? noTelp;
  String? jenisLokasi;
  String? keterangan;
  int? lokasiId;
  String? daftarLokasiJenisLokasi;
  double? gpsLat;
  double? gpsLng;

  factory DaftarLokasi.fromJson(Map<String, dynamic> json) => DaftarLokasi(
        namaLokasi: json["Nama Lokasi"],
        alamat: json["Alamat"],
        desa: json["Desa"],
        kecamatan: json["Kecamatan"],
        noTelp: json["No. Telp"],
        jenisLokasi: json["Jenis Lokasi"],
        keterangan: json["Keterangan"],
        lokasiId: json["lokasi_id"],
        daftarLokasiJenisLokasi: json["jenis_lokasi"],
        gpsLat: json["gps_lat"].toDouble(),
        gpsLng: json["gps_lng"].toDouble(),
      );

  Map<String, dynamic> toJson() => {
        "Nama Lokasi": namaLokasi,
        "Alamat": alamat,
        "Desa": desa,
        "Kecamatan": kecamatan,
        "No. Telp": noTelp,
        "Jenis Lokasi": jenisLokasi,
        "Keterangan": keterangan,
        "lokasi_id": lokasiId,
        "jenis_lokasi": daftarLokasiJenisLokasi,
        "gps_lat": gpsLat,
        "gps_lng": gpsLng,
      };
}
