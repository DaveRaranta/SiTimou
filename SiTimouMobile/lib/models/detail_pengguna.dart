// To parse this JSON data, do
//
//     final detailPengguna = detailPenggunaFromJson(jsonString);

import 'dart:convert';

DetailPengguna detailPenggunaFromJson(String str) => DetailPengguna.fromJson(json.decode(str));

String detailPenggunaToJson(DetailPengguna data) => json.encode(data.toJson());

class DetailPengguna {
  DetailPengguna({
    this.nik,
    this.namaLengkap,
    this.tanggalLahir,
    this.jenisKelamin,
    this.umur,
    this.alamat,
    this.desa,
    this.kecamatan,
    this.noTelp,
    this.rawTanggalLahir,
    this.flg,
    this.userId,
  });

  String? nik;
  String? namaLengkap;
  String? tanggalLahir;
  String? jenisKelamin;
  String? umur;
  String? alamat;
  String? desa;
  String? kecamatan;
  String? noTelp;
  String? rawTanggalLahir;
  String? flg;
  int? userId;

  factory DetailPengguna.fromJson(Map<String, dynamic> json) => DetailPengguna(
        nik: json["NIK"],
        namaLengkap: json["Nama Lengkap"],
        tanggalLahir: json["Tanggal Lahir"],
        jenisKelamin: json["Jenis Kelamin"],
        umur: json["Umur"],
        alamat: json["Alamat"],
        desa: json["Desa"],
        kecamatan: json["Kecamatan"],
        noTelp: json["No. Telp"],
        rawTanggalLahir: json["raw_tanggal_lahir"],
        flg: json["flg"],
        userId: json["user_id"],
      );

  Map<String, dynamic> toJson() => {
        "NIK": nik,
        "Nama Lengkap": namaLengkap,
        "Tanggal Lahir": tanggalLahir,
        "Jenis Kelamin": jenisKelamin,
        "Umur": umur,
        "Alamat": alamat,
        "Desa": desa,
        "Kecamatan": kecamatan,
        "No. Telp": noTelp,
        "raw_tanggal_lahir": rawTanggalLahir,
        "flg": flg,
        "user_id": userId,
      };
}
