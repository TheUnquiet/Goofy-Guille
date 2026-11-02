import { useEffect, useState } from "react";
import { Dialog, DialogContent, DialogDescription, DialogFooter, DialogHeader, DialogTitle } from "@/components/ui/dialog";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { clubService } from "@/services/ClubService";
import type { ClubDto } from "@/services/ClubService";
import { locationService } from "@/services/LocationService";
import type { LocationDto } from "@/services/LocationService";
import { eventService } from "@/services/EventService";
import { EventType } from "@/interfaces/events/EventDto";

interface EventDialogProps {
  open: boolean;
  onOpenChange: (open: boolean) => void;
  onSaved: () => void;
}

export default function EventDialog({
  open,
  onOpenChange,
  onSaved,
}: EventDialogProps) {
  const [clubs, setClubs] = useState<ClubDto[]>([]);
  const [locations, setLocations] = useState<LocationDto[]>([]);
  const [clubId, setClubId] = useState("");
  const [type, setType] = useState<EventType>(EventType.Training);
  const [locationId, setLocationId] = useState("");
  const [opponentClubId, setOpponentClubId] = useState("");
  const [description, setDescription] = useState("");
  const [startTime, setStartTime] = useState("");
  const [endTime, setEndTime] = useState("");
  const [loading, setLoading] = useState(false);
  const [saving, setSaving] = useState(false);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    if (!open) return;

    let active = true;

    async function loadOptions() {
      setLoading(true);
      setError(null);

      try {
        const [clubResults, locationResults] = await Promise.all([
          clubService.getAll(),
          locationService.getAll(),
        ]);

        if (!active) return;

        setClubs(clubResults);
        setLocations(locationResults);
        setClubId(clubResults[0]?.id ?? "");
        setLocationId(locationResults[0]?.id ?? "");
      } catch (err) {
        if (active) {
          setError(err instanceof Error ? err.message : "Failed to load options.");
        }
      } finally {
        if (active) setLoading(false);
      }
    }

    void loadOptions();

    return () => {
      active = false;
    };
  }, [open]);

  async function handleSubmit(event: React.FormEvent<HTMLFormElement>) {
    event.preventDefault();
    setError(null);

    if (!clubId || !locationId) {
      setError("Please select a club and a location.");
      return;
    }

    if (type === EventType.Match && !opponentClubId) {
      setError("Please select an opponent for the match.");
      return;
    }

    if (type === EventType.Match && opponentClubId === clubId) {
      setError("A club cannot play against itself.");
      return;
    }

    if (new Date(endTime) <= new Date(startTime)) {
      setError("The end time must be after the start time.");
      return;
    }

    setSaving(true);

    try {
      await eventService.create({
        clubId,
        type,
        description: description.trim() || null,
        locationId,
        startTime: new Date(startTime).toISOString(),
        endTime: new Date(endTime).toISOString(),
        opponentClubId: type === EventType.Match ? opponentClubId : null,
      });

      onOpenChange(false);
      onSaved();
    } catch (err) {
      setError(err instanceof Error ? err.message : "Failed to create event.");
    } finally {
      setSaving(false);
    }
  }

  return (
    <Dialog open={open} onOpenChange={onOpenChange}>
      <DialogContent className="w-[95vw] max-w-3xl max-h-[90vh] overflow-y-auto">
        <DialogHeader>
          <DialogTitle>Create event</DialogTitle>
          <DialogDescription>
            Schedule a match or training session.
          </DialogDescription>
        </DialogHeader>

        <form onSubmit={handleSubmit} className="space-y-5">
          {loading ? (
            <p className="py-8 text-center text-muted-foreground">
              Loading clubs and locations...
            </p>
          ) : (
            <>
              <div className="grid gap-4 sm:grid-cols-2">
                <div className="space-y-2">
                  <Label htmlFor="event-club">Club</Label>
                  <select
                    id="event-club"
                    value={clubId}
                    onChange={(e) => setClubId(e.target.value)}
                    className="h-10 w-full rounded-md border bg-background px-3 text-sm"
                    required
                  >
                    <option value="">Select a club</option>
                    {clubs.map((club) => (
                      <option key={club.id} value={club.id}>
                        {club.name}
                      </option>
                    ))}
                  </select>
                </div>

                <div className="space-y-2">
                  <Label htmlFor="event-type">Event type</Label>
                  <select
                    id="event-type"
                    value={type}
                    onChange={(e) => setType(Number(e.target.value) as EventType)}
                    className="h-10 w-full rounded-md border bg-background px-3 text-sm"
                  >
                    <option value={EventType.Training}>Training</option>
                    <option value={EventType.Match}>Match</option>
                  </select>
                </div>
              </div>

              {type === EventType.Match && (
                <div className="space-y-2">
                  <Label htmlFor="event-opponent">Opponent club</Label>
                  <select
                    id="event-opponent"
                    value={opponentClubId}
                    onChange={(e) => setOpponentClubId(e.target.value)}
                    className="h-10 w-full rounded-md border bg-background px-3 text-sm"
                    required
                  >
                    <option value="">Select an opponent</option>
                    {clubs
                      .filter((club) => club.id !== clubId)
                      .map((club) => (
                        <option key={club.id} value={club.id}>
                          {club.name}
                        </option>
                      ))}
                  </select>
                </div>
              )}

              <div className="space-y-2">
                <Label htmlFor="event-location">Location</Label>
                <select
                  id="event-location"
                  value={locationId}
                  onChange={(e) => setLocationId(e.target.value)}
                  className="h-10 w-full rounded-md border bg-background px-3 text-sm"
                  required
                >
                  <option value="">Select a location</option>
                  {locations.map((location) => (
                    <option key={location.id} value={location.id}>
                      {location.name}
                    </option>
                  ))}
                </select>
              </div>

              <div className="space-y-2">
                <Label htmlFor="event-description">Description</Label>
                <Input
                  id="event-description"
                  value={description}
                  onChange={(e) => setDescription(e.target.value)}
                  placeholder="e.g. U18 home match"
                />
              </div>

              <div className="grid gap-4 sm:grid-cols-2">
                <div className="space-y-2">
                  <Label htmlFor="event-start">Start</Label>
                  <Input
                    id="event-start"
                    type="datetime-local"
                    value={startTime}
                    onChange={(e) => setStartTime(e.target.value)}
                    required
                  />
                </div>

                <div className="space-y-2">
                  <Label htmlFor="event-end">End</Label>
                  <Input
                    id="event-end"
                    type="datetime-local"
                    value={endTime}
                    onChange={(e) => setEndTime(e.target.value)}
                    required
                  />
                </div>
              </div>
            </>
          )}

          {error && (
            <p className="text-sm text-destructive">{error}</p>
          )}

          <DialogFooter>
            <Button
              type="button"
              variant="outline"
              onClick={() => onOpenChange(false)}
              disabled={saving}
            >
              Cancel
            </Button>
            <Button type="submit" disabled={loading || saving}>
              {saving ? "Creating..." : "Create event"}
            </Button>
          </DialogFooter>
        </form>
      </DialogContent>
    </Dialog>
  );
}