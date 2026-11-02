import { useEffect, useState } from "react";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { clubService } from "@/services/ClubService";
import type { ClubDto } from "@/services/ClubService";
import { Dialog, DialogContent, DialogDescription, DialogFooter, DialogHeader, DialogTitle } from "../ui/dialog";

interface ClubDialogProps {
  open: boolean;
  club: ClubDto | null;
  onOpenChange: (open: boolean) => void;
  onSaved: () => void;
}

export default function ClubDialog({
  open,
  club,
  onOpenChange,
  onSaved,
}: ClubDialogProps) {
  const [name, setName] = useState("");
  const [error, setError] = useState<string | null>(null);
  const [isSaving, setIsSaving] = useState(false);

  const isEditing = club !== null;

  useEffect(() => {
    if (open) {
      setName(club?.name ?? "");
      setError(null);
    }
  }, [open, club]);

  async function handleSubmit(e: React.FormEvent<HTMLFormElement>) {
    e.preventDefault();

    const trimmedName = name.trim();

    if (!trimmedName) {
      setError("Club name is required.");
      return;
    }

    setIsSaving(true);
    setError(null);

    try {
      if (club) {
        await clubService.update({
          id: club.id,
          name: trimmedName,
        });
      } else {
        await clubService.create({
          name: trimmedName,
        });
      }

      onOpenChange(false);
      onSaved();
    } catch (error) {
      setError(
        error instanceof Error ? error.message : "Failed to save club."
      );
    } finally {
      setIsSaving(false);
    }
  }

  return (
    <Dialog open={open} onOpenChange={onOpenChange}>
      <DialogContent>
        <form onSubmit={handleSubmit}>
          <DialogHeader>
            <DialogTitle>
              {isEditing ? "Edit club" : "Create club"}
            </DialogTitle>

            <DialogDescription>
              {isEditing
                ? "Update the club's information."
                : "Add a new basketball club to the system."}
            </DialogDescription>
          </DialogHeader>

          <div className="space-y-2 py-6">
            <Label htmlFor="club-name">Club name</Label>

            <Input
              id="club-name"
              value={name}
              onChange={(e) => setName(e.target.value)}
              placeholder="e.g. Brussels Tigers"
              maxLength={100}
              disabled={isSaving}
              autoFocus
            />

            {error && (
              <p className="text-sm text-destructive">
                {error}
              </p>
            )}
          </div>

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
                  : "Create club"}
            </Button>
          </DialogFooter>
        </form>
      </DialogContent>
    </Dialog>
  );
}
