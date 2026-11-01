import { useEffect, useRef, useState } from "react";
import FullCalendar from "@fullcalendar/react";
import dayGridPlugin from "@fullcalendar/daygrid";
import timeGridPlugin from "@fullcalendar/timegrid";
import interactionPlugin from "@fullcalendar/interaction";
import type { CalendarApi, EventInput } from "@fullcalendar/core";
import { Card, CardContent } from "@/components/ui/card";
import { Button } from "@/components/ui/button";
import { useAuth } from "@/auth/AuthContext";
import { eventService } from "@/services/EventService";

export default function CalendarPage() {
  const calendarRef = useRef<FullCalendar>(null);
  const { login } = useAuth();
  const isAdmin = login?.user.isAdmin ?? false;
  const [events, setEvents] = useState<EventInput[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    let isActive = true;

    async function loadEvents() {
      setIsLoading(true);
      setError(null);

      try {
        const result = await eventService.getAllEvents(isAdmin);

        if (isActive) {
          setEvents(result);
        }
      } catch (error) {
        if (isActive) {
          setError(
            error instanceof Error
              ? error.message
              : "Failed to load events."
          );
        }
      } finally {
        if (isActive) {
          setIsLoading(false);
        }
      }
    }

    loadEvents();

    return () => {
      isActive = false;
    };
  }, [isAdmin]);

  function getCalendar(): CalendarApi | undefined {
    return calendarRef.current?.getApi();
  }

  return (
    <div className="space-y-8">
      <div>
        <h1 className="text-2xl font-bold tracking-tight">
          Calendar
        </h1>
        <p className="text-muted-foreground">
          View and manage your club's matches and training sessions.
        </p>
      </div>

      <Card>
        <CardContent className="space-y-4 pt-6">
          <div className="flex flex-wrap items-center justify-between gap-3">
            <div className="flex gap-2">
              <Button variant="outline" onClick={() => getCalendar()?.today()}>
                Today
              </Button>
              <Button variant="outline" onClick={() => getCalendar()?.prev()}>
                Previous
              </Button>
              <Button variant="outline" onClick={() => getCalendar()?.next()}>
                Next
              </Button>
            </div>

            <div className="flex gap-2">
              <Button variant="outline" size="sm" onClick={() => getCalendar()?.changeView("dayGridMonth")}>
                Month
              </Button>
              <Button variant="outline" size="sm" onClick={() => getCalendar()?.changeView("timeGridWeek")}>
                Week
              </Button>
              <Button variant="outline" size="sm" onClick={() => getCalendar()?.changeView("timeGridDay")}>
                Day
              </Button>
            </div>
          </div>

          {isLoading && (
            <p className="py-8 text-center text-sm text-muted-foreground">
              Loading events...
            </p>
          )}

          {error && (
            <p className="py-8 text-center text-sm text-destructive">
              {error}
            </p>
          )}

          {!isLoading && !error && (
            <FullCalendar
              ref={calendarRef}
              plugins={[
                dayGridPlugin,
                timeGridPlugin,
                interactionPlugin,
              ]}
              initialView="dayGridMonth"
              headerToolbar={false}
              height="auto"
              events={events}
              eventDisplay="block"
              dayMaxEvents={false}
            />
          )}
        </CardContent>
      </Card>
    </div>
  );
}