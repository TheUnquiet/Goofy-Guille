import type { EventInput } from "@fullcalendar/core";

import { apiClient } from "@/api/ApiClient";
import type EventDto from "@/interfaces/events/EventDto";
import { EventType } from "@/interfaces/events/EventDto";

export class EventService {
  async getMyClubEvents(): Promise<EventInput[]> {
    const events = await apiClient.get<EventDto[]>("/Event/my-club");

    return events.map(this.mapToCalendarEvent);
  }

  private mapToCalendarEvent(event: EventDto): EventInput {
    const isMatch = event.type === EventType.Match;

    return {
      id: event.id,
      title: isMatch ? "Match" : "Training",
      start: event.startTime,
      end: event.endTime,
      backgroundColor: isMatch ? "#16a34a" : "#2563eb",
      borderColor: isMatch ? "#16a34a" : "#2563eb",
      extendedProps: {
        clubId: event.clubId,
        description: event.description,
        locationId: event.locationId,
        opponentClubId: event.opponentClubId,
      },
    };
  }
}

export const eventService = new EventService();