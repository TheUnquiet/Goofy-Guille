import { useCallback, useEffect, useMemo, useState } from "react";
import { MapPin, Plus, RefreshCw, Search, Pencil, Trash2,} from "lucide-react";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Card, CardContent,} from "@/components/ui/card";
import LocationDialog from "@/components/locations/LocationDialog";
import { locationService } from "@/services/LocationService";
import type { LocationDto } from "@/services/LocationService";

export default function LocationsPage() {
  const [locations, setLocations] = useState<LocationDto[]>([]);
  const [search, setSearch] = useState("");
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  const [dialogOpen, setDialogOpen] = useState(false);
  const [selectedLocation, setSelectedLocation] =
    useState<LocationDto | null>(null);

  const loadLocations = useCallback(async () => {
    setLoading(true);
    setError(null);

    try {
      const result = await locationService.getAll();
      setLocations(result);
    } catch (error) {
      setError(
        error instanceof Error
          ? error.message
          : "Failed to load locations."
      );
    } finally {
      setLoading(false);
    }
  }, []);

  useEffect(() => {
    void loadLocations();
  }, [loadLocations]);

  const filteredLocations = useMemo(() => {
    const query = search.trim().toLowerCase();

    if (!query) return locations;

    return locations.filter((location) =>
      [
        location.name,
        location.street,
        location.streetNumber,
        location.postalCode,
        location.city,
      ]
        .join(" ")
        .toLowerCase()
        .includes(query)
    );
  }, [locations, search]);

  function handleCreate() {
    setSelectedLocation(null);
    setDialogOpen(true);
  }

  function handleEdit(location: LocationDto) {
    setSelectedLocation(location);
    setDialogOpen(true);
  }

  async function handleDelete(location: LocationDto) {
    const confirmed = window.confirm(
      `Are you sure you want to delete "${location.name}"?`
    );

    if (!confirmed) return;

    try {
      await locationService.delete(location.id);
      await loadLocations();
    } catch (error) {
      setError(
        error instanceof Error
          ? error.message
          : "Failed to delete location."
      );
    }
  }

  const totalLocations = locations.length;
  const displayedLocations = filteredLocations.length;

  return (
    <div className="space-y-6">
      {/* Page heading */}
      <div className="flex flex-col gap-4 sm:flex-row sm:items-center sm:justify-between">
        <div>
          <p className="text-sm text-muted-foreground">
            Administration
          </p>
          <h1 className="text-3xl font-bold tracking-tight">
            Locations
          </h1>
          <p className="text-muted-foreground">
            Manage sports halls, fields and other event locations.
          </p>
        </div>

        <Button onClick={handleCreate}>
          <Plus className="mr-2 h-4 w-4" />
          Add location
        </Button>
      </div>

      {/* Overview */}
      <div className="grid gap-4 sm:grid-cols-2">
        <Card>
          <CardContent className="flex items-center gap-4 p-6">
            <div className="rounded-lg bg-primary/10 p-3">
              <MapPin className="h-6 w-6 text-primary" />
            </div>

            <div>
              <p className="text-sm text-muted-foreground">
                Total locations
              </p>
              <p className="text-2xl font-bold">
                {totalLocations}
              </p>
            </div>
          </CardContent>
        </Card>

        <Card>
          <CardContent className="flex items-center gap-4 p-6">
            <div className="rounded-lg bg-primary/10 p-3">
              <Search className="h-6 w-6 text-primary" />
            </div>

            <div>
              <p className="text-sm text-muted-foreground">
                Displayed locations
              </p>
              <p className="text-2xl font-bold">
                {displayedLocations}
              </p>
            </div>
          </CardContent>
        </Card>
      </div>

      {/* Search and refresh */}
      <div className="flex flex-col gap-3 sm:flex-row">
        <div className="relative flex-1">
          <Search className="absolute left-3 top-1/2 h-4 w-4 -translate-y-1/2 text-muted-foreground" />

          <Input
            value={search}
            onChange={(e) => setSearch(e.target.value)}
            placeholder="Search locations..."
            className="pl-9"
          />
        </div>

        <Button
          variant="outline"
          onClick={() => void loadLocations()}
          disabled={loading}
        >
          <RefreshCw
            className={`mr-2 h-4 w-4 ${loading ? "animate-spin" : ""}`}
          />
          Refresh
        </Button>
      </div>

      {/* Error */}
      {error && (
        <div className="rounded-lg border border-destructive/30 bg-destructive/5 p-4 text-sm text-destructive">
          {error}
        </div>
      )}

      {/* Location list */}
      {loading ? (
        <div className="py-12 text-center text-muted-foreground">
          Loading locations...
        </div>
      ) : filteredLocations.length === 0 ? (
        <Card>
          <CardContent className="flex flex-col items-center gap-3 py-12 text-center">
            <MapPin className="h-10 w-10 text-muted-foreground" />

            <div>
              <h3 className="font-semibold">
                {search ? "No locations found" : "No locations yet"}
              </h3>

              <p className="text-sm text-muted-foreground">
                {search
                  ? "Try another search term."
                  : "Add your first location to get started."}
              </p>
            </div>

            {!search && (
              <Button onClick={handleCreate}>
                <Plus className="mr-2 h-4 w-4" />
                Add location
              </Button>
            )}
          </CardContent>
        </Card>
      ) : (
        <div className="grid gap-4 md:grid-cols-2 xl:grid-cols-3">
          {filteredLocations.map((location) => (
            <Card key={location.id}>
              <CardContent className="space-y-4 p-5">
                <div className="flex items-start justify-between gap-3">
                  <div className="flex min-w-0 items-center gap-3">
                    <div className="rounded-lg bg-primary/10 p-3">
                      <MapPin className="h-5 w-5 text-primary" />
                    </div>

                    <div className="min-w-0">
                      <h3 className="truncate font-semibold">
                        {location.name}
                      </h3>
                      <p className="text-xs text-muted-foreground">
                        {location.city || "No city specified"}
                      </p>
                    </div>
                  </div>
                </div>

                <div className="text-sm text-muted-foreground">
                  <p>
                    {location.street} {location.streetNumber}
                  </p>
                  <p>
                    {location.postalCode} {location.city}
                  </p>
                </div>

                <div className="flex justify-end gap-2 border-t pt-3">
                  <Button
                    size="sm"
                    variant="outline"
                    onClick={() => handleEdit(location)}
                  >
                    <Pencil className="mr-2 h-4 w-4" />
                    Edit
                  </Button>

                  <Button
                    size="sm"
                    variant="destructive"
                    onClick={() => void handleDelete(location)}
                  >
                    <Trash2 className="mr-2 h-4 w-4" />
                    Delete
                  </Button>
                </div>
              </CardContent>
            </Card>
          ))}
        </div>
      )}

      <LocationDialog
        open={dialogOpen}
        location={selectedLocation}
        onOpenChange={setDialogOpen}
        onSaved={() => void loadLocations()}
      />
    </div>
  );
}