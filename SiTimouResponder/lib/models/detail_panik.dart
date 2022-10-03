// To parse this JSON data, do
//
//     final detailPanik = detailPanikFromJson(jsonString);

import 'dart:convert';

DetailPanik detailPanikFromJson(String str) => DetailPanik.fromJson(json.decode(str));

String detailPanikToJson(DetailPanik data) => json.encode(data.toJson());

class DetailPanik {
  DetailPanik({
    this.tanggal,
    this.durasi,
    this.gpsLat,
    this.gpsLng,
    this.userId,
    this.laporanId,
    this.nik,
    this.namaPelapor,
    this.umur,
    this.jenisKelamin,
    this.noTelp,
    this.alamat,
    this.namaDesa,
    this.namaKecamatan,
  });

  String? tanggal;
  String? durasi;
  double? gpsLat;
  double? gpsLng;
  int? userId;
  int? laporanId;
  String? nik;
  String? namaPelapor;
  String? umur;
  String? jenisKelamin;
  String? noTelp;
  String? alamat;
  String? namaDesa;
  String? namaKecamatan;

  factory DetailPanik.fromJson(Map<String, dynamic> json) => DetailPanik(
        tanggal: json["Tanggal"],
        durasi: json["Durasi"],
        gpsLat: json["gps_lat"].toDouble(),
        gpsLng: json["gps_lng"].toDouble(),
        userId: json["user_id"],
        laporanId: json["laporan_id"],
        nik: json["nik"],
        namaPelapor: json["nama_pelapor"],
        umur: json["umur"],
        jenisKelamin: json["jenis_kelamin"],
        noTelp: json["no_telp"],
        alamat: json["alamat"],
        namaDesa: json["nama_desa"],
        namaKecamatan: json["nama_kecamatan"],
      );

  Map<String, dynamic> toJson() => {
        "Tanggal": tanggal,
        "Durasi": durasi,
        "gps_lat": gpsLat,
        "gps_lng": gpsLng,
        "user_id": userId,
        "laporan_id": laporanId,
        "nik": nik,
        "nama_pelapor": namaPelapor,
        "umur": umur,
        "jenis_kelamin": jenisKelamin,
        "no_telp": noTelp,
        "alamat": alamat,
        "nama_desa": namaDesa,
        "nama_kecamatan": namaKecamatan,
      };
}
