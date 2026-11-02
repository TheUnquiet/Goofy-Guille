import { useState } from "react";
import LocationMap from "@/components/locations/LocationMap";
import type { GeocodingResult } from "@/services/GeocodingService";

export default function LocationMapTestPage() {
  const [selectedLocation, setSelectedLocation] =
    useState<GeocodingResult | null>(null);

  return (
    <div className="space-y-6 p-8">
      <div>
        <h1 className="text-2xl font-bold">Map Test</h1>
        <p className="text-muted-foreground">
          Test address searching and map selection.
        </p>
      </div>

      <LocationMap
        coordinates={
          selectedLocation
            ? {
                latitude: selectedLocation.latitude,
                longitude: selectedLocation.longitude,
              }
            : null
        }
        onLocationSelected={setSelectedLocation}
      />

      {selectedLocation && (
        <div className="rounded-lg border p-4 space-y-2">
          <h2 className="font-semibold">Selected location</h2>
          <p>{selectedLocation.displayName}</p>
          <p className="text-sm text-muted-foreground">
            Latitude: {selectedLocation.latitude}
            <br />
            Longitude: {selectedLocation.longitude}
          </p>
        </div>
      )}
    </div>
  );
}