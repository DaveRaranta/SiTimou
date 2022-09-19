// To parse this JSON data, do
//
//     final daftarKecamatan = daftarKecamatanFromJson(jsonString);

import 'dart:convert';

List<DaftarKecamatan> daftarKecamatanFromJson(String str) => List<DaftarKecamatan>.from(json.decode(str).map((x) => DaftarKecamatan.fromJson(x)));

String daftarKecamatanToJson(List<DaftarKecamatan> data) => json.encode(List<dynamic>.from(data.map((x) => x.toJson())));

class DaftarKecamatan {
  DaftarKecamatan({
    this.kecamatanId,
    this.namaKecamatan,
  });

  int? kecamatanId;
  String? namaKecamatan;

  factory DaftarKecamatan.fromJson(Map<String, dynamic> json) => DaftarKecamatan(
        kecamatanId: json["kecamatan_id"],
        namaKecamatan: json["nama_kecamatan"],
      );

  Map<String, dynamic> toJson() => {
        "kecamatan_id": kecamatanId,
        "nama_kecamatan": namaKecamatan,
      };
}
