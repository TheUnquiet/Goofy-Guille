import type { EventInput } from "@fullcalendar/core";
import { apiClient } from "@/api/ApiClient";
import type EventDto from "@/interfaces/events/EventDto";
import { EventType } from "@/interfaces/events/EventDto";

export class EventService {
  async getAllEvents(isAdmin: boolean): Promise<EventInput[]> {
    const endpoint = isAdmin ? "/Event/all" : "/Event/my-club";
    const events = await apiClient.get<EventDto[]>(endpoint);
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