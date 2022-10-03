// To parse this JSON data, do
//
//     final detailLaporan = detailLaporanFromJson(jsonString);

import 'dart:convert';

DetailLaporan detailLaporanFromJson(String str) => DetailLaporan.fromJson(json.decode(str));

String detailLaporanToJson(DetailLaporan data) => json.encode(data.toJson());

class DetailLaporan {
  DetailLaporan({
    this.tanggal,
    this.durasi,
    this.tentang,
    this.isiLaporan,
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
  String? tentang;
  String? isiLaporan;
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

  factory DetailLaporan.fromJson(Map<String, dynamic> json) => DetailLaporan(
        tanggal: json["Tanggal"],
        durasi: json["Durasi"],
        tentang: json["tentang"],
        isiLaporan: json["isi_laporan"],
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
        "tentang": tentang,
        "isi_laporan": isiLaporan,
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
