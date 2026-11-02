
import { useEffect, useState } from "react";

import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogFooter,
  DialogHeader,
  DialogTitle,
} from "@/components/ui/dialog";

import LocationMap from "@/components/locations/LocationMap";
import { locationService } from "@/services/LocationService";
import type { LocationDto } from "@/services/LocationService";
import type { GeocodingResult } from "@/services/GeocodingService";

interface LocationDialogProps {
  open: boolean;
  location: LocationDto | null;
  onOpenChange: (open: boolean) => void;
  onSaved: () => void;
}

export default function LocationDialog({
  open,
  location,
  onOpenChange,
  onSaved,
}: LocationDialogProps) {
  const [name, setName] = useState("");
  const [street, setStreet] = useState("");
  const [streetNumber, setStreetNumber] = useState("");
  const [postalCode, setPostalCode] = useState("");
  const [city, setCity] = useState("");

  const [latitude, setLatitude] = useState<number | null>(null);
  const [longitude, setLongitude] = useState<number | null>(null);

  const [error, setError] = useState<string | null>(null);
  const [isSaving, setIsSaving] = useState(false);

  const isEditing = location !== null;

  useEffect(() => {
    if (open) {
      setName(location?.name ?? "");
      setStreet(location?.street ?? "");
      setStreetNumber(location?.streetNumber ?? "");
      setPostalCode(location?.postalCode ?? "");
      setCity(location?.city ?? "");
      setLatitude(location?.latitude ?? null);
      setLongitude(location?.longitude ?? null);
      setError(null);
    }
  }, [open, location]);

  function handleLocationSelected(result: GeocodingResult) {
    setLatitude(result.latitude);
    setLongitude(result.longitude);

    setStreet(result.address.street);
    setStreetNumber(result.address.streetNumber);
    setPostalCode(result.address.postalCode);
    setCity(result.address.city);
  }

  async function handleSubmit(e: React.FormEvent<HTMLFormElement>) {
    e.preventDefault();

    if (!name.trim()) {
      setError("Location name is required.");
      return;
    }

    if (!street.trim() || !city.trim()) {
      setError("Please select a valid address on the map.");
      return;
    }

    if (latitude === null || longitude === null) {
      setError("Please select a position on the map.");
      return;
    }

    setIsSaving(true);
    setError(null);

    try {
      const dto = {
        name: name.trim(),
        street: street.trim(),
        streetNumber: streetNumber.trim(),
        postalCode: postalCode.trim(),
        city: city.trim(),
        latitude,
        longitude,
      };

      if (location) {
        await locationService.update({
          id: location.id,
          ...dto,
        });
      } else {
        await locationService.create(dto);
      }

      onOpenChange(false);
      onSaved();
    } catch (error) {
      setError(
        error instanceof Error
          ? error.message
          : "Failed to save location."
      );
    } finally {
      setIsSaving(false);
    }
  }

  return (
    <Dialog open={open} onOpenChange={onOpenChange}>
      <DialogContent className="w-[95vw] max-w-5xl max-h-[90vh] overflow-y-auto">
        <form onSubmit={handleSubmit}>
          <DialogHeader>
            <DialogTitle>
              {isEditing ? "Edit location" : "Create location"}
            </DialogTitle>

            <DialogDescription>
              {isEditing
                ? "Update the location's information and position."
                : "Add a new location to the system."}
            </DialogDescription>
          </DialogHeader>

          <div className="grid grid-cols-1 gap-6 py-6 md:grid-cols-2">

            {/* Location details */}
            <div className="space-y-4">
              <h3 className="font-semibold">Location details</h3>

              <div className="space-y-2">
                <Label htmlFor="location-name">Name</Label>
                <Input
                  id="location-name"
                  value={name}
                  onChange={(e) => setName(e.target.value)}
                  placeholder="e.g. Brussels Sports Hall"
                  maxLength={100}
                  disabled={isSaving}
                  autoFocus
                />
              </div>

              <div className="space-y-2">
                <Label htmlFor="location-street">Street</Label>
                <Input
                  id="location-street"
                  value={street}
                  onChange={(e) => setStreet(e.target.value)}
                  disabled={isSaving}
                />
              </div>

              <div className="grid grid-cols-2 gap-3">
                <div className="space-y-2">
                  <Label htmlFor="location-number">House number</Label>
                  <Input
                    id="location-number"
                    value={streetNumber}
                    onChange={(e) => setStreetNumber(e.target.value)}
                    disabled={isSaving}
                  />
                </div>

                <div className="space-y-2">
                  <Label htmlFor="location-postal">Postal code</Label>
                  <Input
                    id="location-postal"
                    value={postalCode}
                    onChange={(e) => setPostalCode(e.target.value)}
                    disabled={isSaving}
                  />
                </div>
              </div>

              <div className="space-y-2">
                <Label htmlFor="location-city">City</Label>
                <Input
                  id="location-city"
                  value={city}
                  onChange={(e) => setCity(e.target.value)}
                  disabled={isSaving}
                />
              </div>

              {latitude !== null && longitude !== null && (
                <p className="text-xs text-muted-foreground">
                  Coordinates: {latitude.toFixed(6)},{" "}
                  {longitude.toFixed(6)}
                </p>
              )}
            </div>

            {/* Map */}
            <div className="space-y-4">
              <h3 className="font-semibold">Select position</h3>

              <LocationMap
                coordinates={
                  latitude !== null && longitude !== null
                    ? { latitude, longitude }
                    : null
                }
                onLocationSelected={handleLocationSelected}
              />

              <p className="text-xs text-muted-foreground">
                Search for an address or click on the map to place a pin.
              </p>
            </div>
          </div>

          {error && (
            <p className="mb-4 text-sm text-destructive">{error}</p>
          )}

          <DialogFooter>
            <Button
              type="button"
              variant="outline"
              disabled={isSaving}
              onClick={() => onOpenChange(false)}
            >
              Cancel
            </Button>

            <Button type="submit" disabled={isSaving}>
              {isSaving
                ? "Saving..."
                : isEditing
                  ? "Save changes"
                  : "Create location"}
            </Button>
          </DialogFooter>
        </form>
      </DialogContent>
    </Dialog>
  );
}