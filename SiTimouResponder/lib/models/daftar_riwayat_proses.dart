// To parse this JSON data, do
//
//     final daftarRiwayatProses = daftarRiwayatProsesFromJson(jsonString);

import 'dart:convert';

List<DaftarRiwayatProses> daftarRiwayatProsesFromJson(String str) =>
    List<DaftarRiwayatProses>.from(json.decode(str).map((x) => DaftarRiwayatProses.fromJson(x)));

String daftarRiwayatProsesToJson(List<DaftarRiwayatProses> data) => json.encode(List<dynamic>.from(data.map((x) => x.toJson())));

class DaftarRiwayatProses {
  DaftarRiwayatProses({
    this.namaPelapor,
    this.tentang,
    this.tanggalMasuk,
    this.hasilProses,
    this.tanggalProses,
    this.durasiProses,
    this.laporanId,
    this.disposisiId,
  });

  String? namaPelapor;
  String? tentang;
  String? tanggalMasuk;
  String? hasilProses;
  String? tanggalProses;
  String? durasiProses;
  int? laporanId;
  int? disposisiId;

  factory DaftarRiwayatProses.fromJson(Map<String, dynamic> json) => DaftarRiwayatProses(
        namaPelapor: json["Nama Pelapor"],
        tentang: json["Tentang"],
        tanggalMasuk: json["Tanggal Masuk"],
        hasilProses: json["Hasil Proses"],
        tanggalProses: json["Tanggal Proses"],
        durasiProses: json["Durasi Proses"],
        laporanId: json["laporan_id"],
        disposisiId: json["disposisi_id"],
      );

  Map<String, dynamic> toJson() => {
        "Nama Pelapor": namaPelapor,
        "Tentang": tentang,
        "Tanggal Masuk": tanggalMasuk,
        "Hasil Proses": hasilProses,
        "Tanggal Proses": tanggalProses,
        "Durasi Proses": durasiProses,
        "laporan_id": laporanId,
        "disposisi_id": disposisiId,
      };
}
