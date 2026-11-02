export interface GeocodingAddress {
  street: string;
  streetNumber: string;
  postalCode: string;
  city: string;
}

export interface GeocodingResult {
  latitude: number;
  longitude: number;
  displayName: string;
  address: GeocodingAddress;
}

interface NominatimAddress {
  road?: string;
  pedestrian?: string;
  house_number?: string;
  postcode?: string;
  city?: string;
  town?: string;
  village?: string;
  municipality?: string;
  suburb?: string;
}

interface NominatimResult {
  lat: string;
  lon: string;
  display_name: string;
  address: NominatimAddress;
}

export class GeocodingService {
  private readonly baseUrl =
    "https://nominatim.openstreetmap.org";

  async searchAddress(query: string): Promise<GeocodingResult[]> {
    if (!query.trim()) {
      return [];
    }

    const params = new URLSearchParams({
      q: query,
      format: "jsonv2",
      addressdetails: "1",
      limit: "5",
      countrycodes: "be",
    });

    const response = await fetch(
      `${this.baseUrl}/search?${params}`
    );

    if (!response.ok) {
      throw new Error("Failed to search for address.");
    }

    const results: NominatimResult[] = await response.json();

    return results.map(this.mapResult);
  }

  async reverseGeocode(
    latitude: number,
    longitude: number
  ): Promise<GeocodingResult> {
    const params = new URLSearchParams({
      lat: latitude.toString(),
      lon: longitude.toString(),
      format: "jsonv2",
      addressdetails: "1",
    });

    const response = await fetch(
      `${this.baseUrl}/reverse?${params}`
    );

    if (!response.ok) {
      throw new Error("Failed to retrieve address.");
    }

    const result: NominatimResult = await response.json();

    return this.mapResult(result);
  }

  private mapResult(result: NominatimResult): GeocodingResult {
    const address = result.address;

    return {
      latitude: Number(result.lat),
      longitude: Number(result.lon),
      displayName: result.display_name,
      address: {
        street: address.road ?? address.pedestrian ?? "",
        streetNumber: address.house_number ?? "",
        postalCode: address.postcode ?? "",
        city:
          address.city ??
          address.town ??
          address.village ??
          address.municipality ??
          address.suburb ??
          "",
      },
    };
  }
}

export const geocodingService = new GeocodingService();
