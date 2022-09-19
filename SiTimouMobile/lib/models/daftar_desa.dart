// To parse this JSON data, do
//
//     final daftarDesa = daftarDesaFromJson(jsonString);

import 'dart:convert';

List<DaftarDesa> daftarDesaFromJson(String str) => List<DaftarDesa>.from(json.decode(str).map((x) => DaftarDesa.fromJson(x)));

String daftarDesaToJson(List<DaftarDesa> data) => json.encode(List<dynamic>.from(data.map((x) => x.toJson())));

class DaftarDesa {
  DaftarDesa({
    this.desaId,
    this.namaDesa,
  });

  int? desaId;
  String? namaDesa;

  factory DaftarDesa.fromJson(Map<String, dynamic> json) => DaftarDesa(
        desaId: json["desa_id"],
        namaDesa: json["nama_desa"],
      );

  Map<String, dynamic> toJson() => {
        "desa_id": desaId,
        "nama_desa": namaDesa,
      };
}
