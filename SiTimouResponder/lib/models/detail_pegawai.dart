// To parse this JSON data, do
//
//     final detailPegawai = detailPegawaiFromJson(jsonString);

import 'dart:convert';

DetailPegawai detailPegawaiFromJson(String str) => DetailPegawai.fromJson(json.decode(str));

String detailPegawaiToJson(DetailPegawai data) => json.encode(data.toJson());

class DetailPegawai {
  DetailPegawai({
    this.userId,
    this.login,
    this.namaLengkap,
    this.grup,
    this.jabatan,
    this.opdSingkat,
    this.opdLengkap,
    this.opdId,
  });

  int? userId;
  String? login;
  String? namaLengkap;
  String? grup;
  String? jabatan;
  String? opdSingkat;
  String? opdLengkap;
  int? opdId;

  factory DetailPegawai.fromJson(Map<String, dynamic> json) => DetailPegawai(
        userId: json["user_id"],
        login: json["login"],
        namaLengkap: json["nama_lengkap"],
        grup: json["grup"],
        jabatan: json["jabatan"],
        opdSingkat: json["opd_singkat"],
        opdLengkap: json["opd_lengkap"],
        opdId: json["opd_id"],
      );

  Map<String, dynamic> toJson() => {
        "user_id": userId,
        "login": login,
        "nama_lengkap": namaLengkap,
        "grup": grup,
        "jabatan": jabatan,
        "opd_singkat": opdSingkat,
        "opd_lengkap": opdLengkap,
        "opd_id": opdId,
      };
}
