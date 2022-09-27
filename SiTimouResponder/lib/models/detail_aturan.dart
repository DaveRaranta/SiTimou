// To parse this JSON data, do
//
//     final detailAturan = detailAturanFromJson(jsonString);

import 'dart:convert';

DetailAturan detailAturanFromJson(String str) => DetailAturan.fromJson(json.decode(str));

String detailAturanToJson(DetailAturan data) => json.encode(data.toJson());

class DetailAturan {
  DetailAturan({
    this.tanggalUpdate,
    this.judul,
    this.isi,
    this.namaOpd,
    this.namaPegawai,
  });

  String? tanggalUpdate;
  String? judul;
  String? isi;
  String? namaOpd;
  String? namaPegawai;

  factory DetailAturan.fromJson(Map<String, dynamic> json) => DetailAturan(
        tanggalUpdate: json["Tanggal Update"],
        judul: json["Judul"],
        isi: json["Isi"],
        namaOpd: json["Nama Unit"],
        namaPegawai: json["Nama Pegawai"],
      );

  Map<String, dynamic> toJson() => {
        "Tanggal Update": tanggalUpdate,
        "Judul": judul,
        "Isi": isi,
        "Nama Unit": namaOpd,
        "Nama Pegawai": namaPegawai,
      };
}
