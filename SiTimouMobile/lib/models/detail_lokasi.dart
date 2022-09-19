// To parse this JSON data, do
//
//     final detailLokasi = detailLokasiFromJson(jsonString);

import 'dart:convert';

DetailLokasi detailLokasiFromJson(String str) => DetailLokasi.fromJson(json.decode(str));

String detailLokasiToJson(DetailLokasi data) => json.encode(data.toJson());

class DetailLokasi {
  DetailLokasi({
    this.namaLokasi,
    this.alamat,
    this.desa,
    this.kecamatan,
    this.noTelp,
    this.jenisLokasi,
    this.keterangan,
    this.lokasiId,
    this.detailLokasiJenisLokasi,
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
  String? detailLokasiJenisLokasi;
  double? gpsLat;
  double? gpsLng;

  factory DetailLokasi.fromJson(Map<String, dynamic> json) => DetailLokasi(
        namaLokasi: json["Nama Lokasi"],
        alamat: json["Alamat"],
        desa: json["Desa"],
        kecamatan: json["Kecamatan"],
        noTelp: json["No. Telp"],
        jenisLokasi: json["Jenis Lokasi"],
        keterangan: json["Keterangan"],
        lokasiId: json["lokasi_id"],
        detailLokasiJenisLokasi: json["jenis_lokasi"],
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
        "jenis_lokasi": detailLokasiJenisLokasi,
        "gps_lat": gpsLat,
        "gps_lng": gpsLng,
      };
}
