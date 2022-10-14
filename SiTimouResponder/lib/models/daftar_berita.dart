// To parse this JSON data, do
//
//     final daftarBerita = daftarBeritaFromJson(jsonString);

import 'dart:convert';

List<DaftarBerita> daftarBeritaFromJson(String str) => List<DaftarBerita>.from(json.decode(str).map((x) => DaftarBerita.fromJson(x)));

String daftarBeritaToJson(List<DaftarBerita> data) => json.encode(List<dynamic>.from(data.map((x) => x.toJson())));

class DaftarBerita {
  DaftarBerita({
    this.idBerita,
    this.judulBerita,
    this.tglBerita,
    this.isiBerita,
    this.coverBerita,
  });

  String? idBerita;
  String? judulBerita;
  DateTime? tglBerita;
  String? isiBerita;
  String? coverBerita;

  factory DaftarBerita.fromJson(Map<String, dynamic> json) => DaftarBerita(
        idBerita: json["id_berita"],
        judulBerita: json["judul_berita"],
        tglBerita: DateTime.parse(json["tgl_berita"]),
        isiBerita: json["isi_berita"],
        coverBerita: json["cover_berita"],
      );

  Map<String, dynamic> toJson() => {
        "id_berita": idBerita,
        "judul_berita": judulBerita,
        "tgl_berita": tglBerita,
        "isi_berita": isiBerita,
        "cover_berita": coverBerita,
      };
}
