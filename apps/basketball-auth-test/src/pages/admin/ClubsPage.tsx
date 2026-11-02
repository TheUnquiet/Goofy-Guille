import { useEffect, useMemo, useState } from "react";
import { Building2, Search, Plus, Pencil, Trash2, RefreshCw, Users, } from "lucide-react";
import { clubService } from "@/services/ClubService";
import type { ClubDto } from "@/services/ClubService";
import { Card, CardContent } from "@/components/ui/card";
import { Button } from "@/components/ui/button";
import ClubDialog from "@/components/admin/ClubDialog";

export default function ClubsPage() {
  const [clubs, setClubs] = useState<ClubDto[]>([]);
  const [search, setSearch] = useState("");
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [isDialogOpen, setIsDialogOpen] = useState(false);
  const [selectedClub, setSelectedClub] = useState<ClubDto | null>(null);

  async function loadClubs() {
    setIsLoading(true);
    setError(null);

    try {
      const result = await clubService.getAll();
      setClubs(result);
    } catch (error) {
      setError(
        error instanceof Error ? error.message : "Failed to load clubs."
      );
    } finally {
      setIsLoading(false);
    }
  }

  useEffect(() => {
    loadClubs();
  }, []);

  const filteredClubs = useMemo(() => {
    return clubs.filter((club) =>
      club.name.toLowerCase().includes(search.toLowerCase())
    );
  }, [clubs, search]);

  return (
    <div className="space-y-8">
      {/* Page header */}
      <div className="flex flex-col justify-between gap-4 sm:flex-row sm:items-center">
        <div className="space-y-1">
          <div className="flex items-center gap-2 text-sm text-muted-foreground">
            <Building2 className="h-4 w-4" />
            Administration
          </div>

          <h1 className="text-2xl font-bold tracking-tight">
            Clubs
          </h1>

          <p className="text-muted-foreground">
            Manage basketball clubs and their details.
          </p>
        </div>

        <Button
          className="gap-2"
          onClick={() => {
            setSelectedClub(null);
            setIsDialogOpen(true);
          }}>
          <Plus className="h-4 w-4" />
          Add club
        </Button>
      </div>

      {/* Overview */}
      <div className="grid gap-4 sm:grid-cols-2">
        <Card>
          <CardContent className="flex items-center gap-4 p-6">
            <div className="rounded-xl bg-primary/10 p-3 text-primary">
              <Building2 className="h-6 w-6" />
            </div>

            <div>
              <p className="text-sm text-muted-foreground">
                Total clubs
              </p>
              <p className="text-2xl font-bold">
                {isLoading ? "—" : clubs.length}
              </p>
            </div>
          </CardContent>
        </Card>

        <Card>
          <CardContent className="flex items-center gap-4 p-6">
            <div className="rounded-xl bg-muted p-3 text-muted-foreground">
              <Users className="h-6 w-6" />
            </div>

            <div>
              <p className="text-sm text-muted-foreground">
                Displayed clubs
              </p>
              <p className="text-2xl font-bold">
                {isLoading ? "—" : filteredClubs.length}
              </p>
            </div>
          </CardContent>
        </Card>
      </div>

      {/* Club list */}
      <Card>
        <div className="flex flex-col justify-between gap-4 border-b p-6 sm:flex-row sm:items-center">
          <div>
            <h2 className="font-semibold">All clubs</h2>
            <p className="text-sm text-muted-foreground">
              View and manage registered clubs.
            </p>
          </div>

          <div className="flex gap-2">
            <div className="relative flex-1 sm:w-64">
              <Search className="absolute left-3 top-1/2 h-4 w-4 -translate-y-1/2 text-muted-foreground" />
              <input
                value={search}
                onChange={(e) => setSearch(e.target.value)}
                placeholder="Search clubs..."
                className="h-9 w-full rounded-md border border-input bg-background pl-9 pr-3 text-sm outline-none transition-colors focus-visible:border-ring focus-visible:ring-2 focus-visible:ring-ring/30"
              />
            </div>

            <Button
              variant="outline"
              size="icon"
              onClick={loadClubs}
              disabled={isLoading}
              title="Refresh clubs"
            >
              <RefreshCw className={`h-4 w-4 ${isLoading ? "animate-spin" : ""}`} />
            </Button>
          </div>
        </div>

        <CardContent className="p-0">
          {isLoading && (
            <div className="py-16 text-center text-sm text-muted-foreground">
              Loading clubs...
            </div>
          )}

          {error && (
            <div className="space-y-3 py-16 text-center">
              <p className="text-sm text-destructive">{error}</p>
              <Button variant="outline" size="sm" onClick={loadClubs}>
                Try again
              </Button>
            </div>
          )}

          {!isLoading && !error && filteredClubs.length === 0 && (
            <div className="flex flex-col items-center gap-3 py-16 text-center">
              <div className="rounded-full bg-muted p-4">
                <Building2 className="h-7 w-7 text-muted-foreground" />
              </div>

              <div>
                <p className="font-medium">
                  {search ? "No clubs found" : "No clubs yet"}
                </p>
                <p className="text-sm text-muted-foreground">
                  {search
                    ? "Try a different search term."
                    : "Create your first club to get started."}
                </p>
              </div>
            </div>
          )}

          {!isLoading && !error && filteredClubs.length > 0 && (
            <div className="divide-y">
              {filteredClubs.map((club) => (
                <div
                  key={club.id}
                  className="group flex items-center justify-between gap-4 px-6 py-4 transition-colors hover:bg-muted/40"
                >
                  <div className="flex min-w-0 items-center gap-4">
                    <div className="flex h-11 w-11 shrink-0 items-center justify-center rounded-xl bg-primary/10 text-primary">
                      <Building2 className="h-5 w-5" />
                    </div>

                    <div className="min-w-0">
                      <p className="truncate font-semibold">
                        {club.name}
                      </p>
                      <p className="truncate text-xs text-muted-foreground">
                        ID: {club.id}
                      </p>
                    </div>
                  </div>

                  <div className="flex shrink-0 gap-1 opacity-100 transition-opacity sm:opacity-60 sm:group-hover:opacity-100">
                    <Button
                      variant="ghost"
                      size="icon"
                      title="Edit club"
                      onClick={() => {
                        setSelectedClub(club);
                        setIsDialogOpen(true);
                      }}>
                      <Pencil className="h-4 w-4" />
                    </Button>

                    <Button
                      variant="ghost"
                      size="icon"
                      title="Delete club"
                      className="text-destructive hover:bg-destructive/10 hover:text-destructive"
                    >
                      <Trash2 className="h-4 w-4" />
                    </Button>
                  </div>
                </div>
              ))}
            </div>
          )}

          {!isLoading && !error && filteredClubs.length > 0 && (
            <div className="border-t px-6 py-3">
              <p className="text-xs text-muted-foreground">
                Showing {filteredClubs.length} of {clubs.length} clubs
              </p>
            </div>
          )}
        </CardContent>
      </Card>
      <ClubDialog
        open={isDialogOpen}
        club={selectedClub}
        onOpenChange={setIsDialogOpen}
        onSaved={loadClubs}
      />
    </div>
  );
}