// To parse this JSON data, do
//
//     final daftarLaporan = daftarLaporanFromJson(jsonString);

import 'dart:convert';

List<DaftarLaporan> daftarLaporanFromJson(String str) => List<DaftarLaporan>.from(json.decode(str).map((x) => DaftarLaporan.fromJson(x)));

String daftarLaporanToJson(List<DaftarLaporan> data) => json.encode(List<dynamic>.from(data.map((x) => x.toJson())));

class DaftarLaporan {
  DaftarLaporan({
    this.tanggal,
    this.durasiDisposisi,
    this.namaPelapor,
    this.nomorTelp,
    this.alamatPelapor,
    this.desaKelurahan,
    this.kecamatan,
    this.jenisLaporan,
    this.statusLaporan,
    this.laporanId,
    this.pelaporId,
    this.nik,
    this.disposisiId,
    this.idJenisLaporan,
    this.flg,
  });

  String? tanggal;
  String? durasiDisposisi;
  String? namaPelapor;
  String? nomorTelp;
  String? alamatPelapor;
  String? desaKelurahan;
  String? kecamatan;
  String? jenisLaporan;
  String? statusLaporan;
  int? laporanId;
  int? pelaporId;
  String? nik;
  int? disposisiId;
  String? idJenisLaporan;
  String? flg;

  factory DaftarLaporan.fromJson(Map<String, dynamic> json) => DaftarLaporan(
        tanggal: json["Tanggal"],
        durasiDisposisi: json["Durasi Disposisi"],
        namaPelapor: json["Nama Pelapor"],
        nomorTelp: json["Nomor Telp"],
        alamatPelapor: json["Alamat Pelapor"],
        desaKelurahan: json["Desa/Kelurahan"],
        kecamatan: json["Kecamatan"],
        jenisLaporan: json["Jenis Laporan"],
        statusLaporan: json["Status Laporan"],
        laporanId: json["laporan_id"],
        pelaporId: json["pelapor_id"],
        nik: json["nik"],
        disposisiId: json["disposisi_id"],
        idJenisLaporan: json["jenis_laporan"],
        flg: json["flg"],
      );

  Map<String, dynamic> toJson() => {
        "Tanggal": tanggal,
        "Durasi Disposisi": durasiDisposisi,
        "Nama Pelapor": namaPelapor,
        "Nomor Telp": nomorTelp,
        "Alamat Pelapor": alamatPelapor,
        "Desa/Kelurahan": desaKelurahan,
        "Kecamatan": kecamatan,
        "Jenis Laporan": jenisLaporan,
        "Status Laporan": statusLaporan,
        "laporan_id": laporanId,
        "pelapor_id": pelaporId,
        "nik": nik,
        "disposisi_id": disposisiId,
        "jenis_laporan": idJenisLaporan,
        "flg": flg,
      };
}
