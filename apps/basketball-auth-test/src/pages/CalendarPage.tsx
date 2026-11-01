import { useRef } from "react";

import FullCalendar from "@fullcalendar/react";
import dayGridPlugin from "@fullcalendar/daygrid";
import timeGridPlugin from "@fullcalendar/timegrid";
import interactionPlugin from "@fullcalendar/interaction";
import type { CalendarApi } from "@fullcalendar/core";

import {
  Card,
  CardContent,
} from "@/components/ui/card";

import { Button } from "@/components/ui/button";

export default function CalendarPage() {
  const calendarRef = useRef<FullCalendar>(null);

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

          <FullCalendar
            ref={calendarRef}
            plugins={[
              dayGridPlugin,
              timeGridPlugin,
              interactionPlugin,
            ]}
            initialView="dayGridMonth"
            headerToolbar={{
              left: "",
              center: "title",
              right: "",
            }}
            height="auto"
            events={[
              {
                title: "Training",
                start: "2026-11-03T18:00:00",
                end: "2026-11-03T20:00:00",
                backgroundColor: "#2563eb",
                borderColor: "#2563eb",
              },
              {
                title: "Match vs Tigers",
                start: "2026-11-05T19:00:00",
                end: "2026-11-05T21:00:00",
                backgroundColor: "#16a34a",
                borderColor: "#16a34a",
              },
            ]}
          />
        </CardContent>
      </Card>
    </div>
  );
}