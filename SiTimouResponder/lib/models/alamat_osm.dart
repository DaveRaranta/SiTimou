// To parse this JSON data, do
//
//     final alamatOpenSreetMap = alamatOpenSreetMapFromJson(jsonString);

import 'dart:convert';

AlamatOpenSreetMap alamatOpenSreetMapFromJson(String str) => AlamatOpenSreetMap.fromJson(json.decode(str));

String alamatOpenSreetMapToJson(AlamatOpenSreetMap data) => json.encode(data.toJson());

class AlamatOpenSreetMap {
  AlamatOpenSreetMap({
    this.placeId,
    this.licence,
    this.osmType,
    this.osmId,
    this.lat,
    this.lon,
    this.displayName,
    this.address,
    this.boundingbox,
  });

  int? placeId;
  String? licence;
  String? osmType;
  int? osmId;
  String? lat;
  String? lon;
  String? displayName;
  Address? address;
  List<String>? boundingbox;

  factory AlamatOpenSreetMap.fromJson(Map<String, dynamic> json) => AlamatOpenSreetMap(
        placeId: json["place_id"],
        licence: json["licence"],
        osmType: json["osm_type"],
        osmId: json["osm_id"],
        lat: json["lat"],
        lon: json["lon"],
        displayName: json["display_name"],
        address: Address.fromJson(json["address"]),
        boundingbox: List<String>.from(json["boundingbox"].map((x) => x)),
      );

  Map<String, dynamic> toJson() => {
        "place_id": placeId,
        "licence": licence,
        "osm_type": osmType,
        "osm_id": osmId,
        "lat": lat,
        "lon": lon,
        "display_name": displayName,
        "address": address!.toJson(),
        "boundingbox": List<dynamic>.from(boundingbox!.map((x) => x)),
      };
}

class Address {
  Address({
    this.road,
    this.town,
    this.state,
    this.iso31662Lvl4,
    this.postcode,
    this.country,
    this.countryCode,
  });

  String? road;
  String? town;
  String? state;
  String? iso31662Lvl4;
  String? postcode;
  String? country;
  String? countryCode;

  factory Address.fromJson(Map<String, dynamic> json) => Address(
        road: json["road"],
        town: json["town"],
        state: json["state"],
        iso31662Lvl4: json["ISO3166-2-lvl4"],
        postcode: json["postcode"],
        country: json["country"],
        countryCode: json["country_code"],
      );

  Map<String, dynamic> toJson() => {
        "road": road,
        "town": town,
        "state": state,
        "ISO3166-2-lvl4": iso31662Lvl4,
        "postcode": postcode,
        "country": country,
        "country_code": countryCode,
      };
}
