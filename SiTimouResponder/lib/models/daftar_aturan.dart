// To parse this JSON data, do
//
//     final daftarAturan = daftarAturanFromJson(jsonString);

import 'dart:convert';

List<DaftarAturan> daftarAturanFromJson(String str) => List<DaftarAturan>.from(json.decode(str).map((x) => DaftarAturan.fromJson(x)));

String daftarAturanToJson(List<DaftarAturan> data) => json.encode(List<dynamic>.from(data.map((x) => x.toJson())));

class DaftarAturan {
  DaftarAturan({
    this.tanggalUpdate,
    this.namaOpd,
    this.namaAturan,
    this.namaPegawai,
    this.aturanId,
  });

  String? tanggalUpdate;
  String? namaOpd;
  String? namaAturan;
  String? namaPegawai;
  int? aturanId;

  factory DaftarAturan.fromJson(Map<String, dynamic> json) => DaftarAturan(
        tanggalUpdate: json["Tanggal Update"],
        namaOpd: json["Nama Unit"],
        namaAturan: json["Nama Aturan"],
        namaPegawai: json["Nama Pegawai"],
        aturanId: json["aturan_id"],
      );

  Map<String, dynamic> toJson() => {
        "Tanggal Update": tanggalUpdate,
        "Nama Unit": namaOpd,
        "Nama Aturan": namaAturan,
        "Nama Pegawai": namaPegawai,
        "aturan_id": aturanId,
      };
}
