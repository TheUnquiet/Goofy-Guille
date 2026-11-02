import { useEffect, useState } from "react";
import { MapContainer, Marker, TileLayer, useMap, useMapEvents,} from "react-leaflet";
import type { GeocodingResult } from "@/services/GeocodingService";
import { geocodingService } from "@/services/GeocodingService";

interface Coordinates {
  latitude: number;
  longitude: number;
}

interface LocationMapProps {
  coordinates: Coordinates | null;
  onLocationSelected: (result: GeocodingResult) => void;
}

function MapClickHandler({
  onLocationSelected,
}: Pick<LocationMapProps, "onLocationSelected">) {
  useMapEvents({
    async click(event) {
      const { lat, lng } = event.latlng;

      onLocationSelected({
        latitude: lat,
        longitude: lng,
        displayName: `${lat.toFixed(5)}, ${lng.toFixed(5)}`,
        address: {
          street: "",
          streetNumber: "",
          postalCode: "",
          city: "",
        },
      });

      try {
        const result = await geocodingService.reverseGeocode(lat, lng);
        onLocationSelected(result);
      } catch (error) {
        console.error("Reverse geocoding failed:", error);
      }
    },
  });

  return null;
}

function MapViewUpdater({ coordinates }: { coordinates: Coordinates | null }) {
  const map = useMap();

  useEffect(() => {
    if (coordinates) {
      map.setView(
        [coordinates.latitude, coordinates.longitude],
        16
      );
    }
  }, [coordinates, map]);

  return null;
}

export default function LocationMap({
  coordinates,
  onLocationSelected,
}: LocationMapProps) {
  const [query, setQuery] = useState("");
  const [results, setResults] = useState<GeocodingResult[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const belgiumCenter: [number, number] = [50.8503, 4.3517];

  async function handleSearch() {
    if (!query.trim()) return;

    setLoading(true);
    setError(null);
    setResults([]);

    try {
      const matches = await geocodingService.searchAddress(query);

      if (matches.length === 0) {
        setError("No matching addresses found.");
      }

      setResults(matches);
    } catch {
      setError("Could not search for this address.");
    } finally {
      setLoading(false);
    }
  }

  function handleSelectResult(result: GeocodingResult) {
    onLocationSelected(result);
    setResults([]);
    setQuery(result.displayName);
  }

  return (
    <div className="space-y-3">
      <div className="flex gap-2">
        <input
          value={query}
          onChange={(event) => setQuery(event.target.value)}
          onKeyDown={(event) => {
            if (event.key === "Enter") {
              event.preventDefault();
              void handleSearch();
            }
          }}
          placeholder="Search for an address..."
          className="flex-1 rounded-md border px-3 py-2 text-sm"
        />

        <button
          type="button"
          onClick={() => void handleSearch()}
          disabled={loading || !query.trim()}
          className="rounded-md bg-primary px-4 py-2 text-sm text-primary-foreground disabled:opacity-50"
        >
          {loading ? "Searching..." : "Search"}
        </button>
      </div>

      {error && (
        <p className="text-sm text-destructive">{error}</p>
      )}

      {results.length > 0 && (
        <div className="rounded-md border divide-y">
          {results.map((result, index) => (
            <button
              key={`${result.latitude}-${result.longitude}-${index}`}
              type="button"
              onClick={() => handleSelectResult(result)}
              className="w-full p-3 text-left text-sm hover:bg-accent"
            >
              {result.displayName}
            </button>
          ))}
        </div>
      )}

      <MapContainer
        center={
          coordinates
            ? [coordinates.latitude, coordinates.longitude]
            : belgiumCenter
        }
        zoom={coordinates ? 16 : 8}
        scrollWheelZoom
        className="h-112.5 w-full rounded-lg"
      >
        <TileLayer
          attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a>'
          url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
          referrerPolicy="strict-origin-when-cross-origin"
        />

        <MapViewUpdater coordinates={coordinates} />

        <MapClickHandler
          onLocationSelected={onLocationSelected}
        />

        {coordinates && (
          <Marker
            position={[
              coordinates.latitude,
              coordinates.longitude,
            ]}
          />
        )}
      </MapContainer>

      <p className="text-xs text-muted-foreground">
        Search for an address or click on the map to select a location.
        Map data © OpenStreetMap contributors.
      </p>
    </div>
  );
}
