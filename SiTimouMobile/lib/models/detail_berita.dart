// To parse this JSON data, do
//
//     final detailBerita = detailBeritaFromJson(jsonString);

import 'dart:convert';

DetailBerita detailBeritaFromJson(String str) => DetailBerita.fromJson(json.decode(str));

String detailBeritaToJson(DetailBerita data) => json.encode(data.toJson());

class DetailBerita {
  DetailBerita({
    this.tanggal,
    this.jenis,
    this.judulBerita,
    this.ringkasan,
    this.isiBerita,
    this.namaPenulis,
    this.opdPenulis,
    this.fotoCover,
  });

  String? tanggal;
  String? jenis;
  String? judulBerita;
  String? ringkasan;
  String? isiBerita;
  String? namaPenulis;
  String? opdPenulis;
  String? fotoCover;

  factory DetailBerita.fromJson(Map<String, dynamic> json) => DetailBerita(
        tanggal: json["tanggal"],
        jenis: json["jenis"],
        judulBerita: json["judul_berita"],
        ringkasan: json["ringkasan"],
        isiBerita: json["isi_berita"],
        namaPenulis: json["nama_penulis"],
        opdPenulis: json["opd_penulis"],
        fotoCover: json["foto_cover"],
      );

  Map<String, dynamic> toJson() => {
        "tanggal": tanggal,
        "jenis": jenis,
        "judul_berita": judulBerita,
        "ringkasan": ringkasan,
        "isi_berita": isiBerita,
        "nama_penulis": namaPenulis,
        "opd_penulis": opdPenulis,
        "foto_cover": fotoCover,
      };
}
