import type { EventInput } from "@fullcalendar/core";
import { apiClient } from "@/api/ApiClient";
import type EventDto from "@/interfaces/events/EventDto";
import { EventType } from "@/interfaces/events/EventDto";
import type CreateEventDto from "@/interfaces/events/CreateEventDto";
import type UpdateEventDto from "@/interfaces/events/UpdateEventDto";

export class EventService {
  private readonly clubColors = [
    "#aeaffc", // Pastel blue
    "#c6adf0", // Pastel purple
    "#a6e3c3", // Pastel green
    "#f5ad98", // Pastel peach
    "#f5a6d7", // Pastel pink
    "#f7df99", // Pastel yellow
    "#97dee6", // Pastel cyan
  ];

  private getClubColor(clubId: string): string {
    const hash = [...clubId].reduce(
      (value, character) => value + character.charCodeAt(0),
      0
    );

    return this.clubColors[hash % this.clubColors.length];
  }

  async getAllEvents(isAdmin: boolean): Promise<EventInput[]> {
    const endpoint = isAdmin ? "/Event/all" : "/Event/my-club";
    const events = await apiClient.get<EventDto[]>(endpoint);
    return events.map(event => this.mapToCalendarEvent(event));
  }

  async create(dto: CreateEventDto): Promise<EventDto> {
    return apiClient.post<CreateEventDto, EventDto>("/Event", dto);
  }

  async update(dto: UpdateEventDto): Promise<EventDto> {
    return apiClient.put<UpdateEventDto, EventDto>("/Event", dto);
  }

  async delete(id: string): Promise<void> {
    return apiClient.delete(`/Event/${id}`);
  }

  private mapToCalendarEvent(event: EventDto): EventInput {
    const isMatch = event.type === EventType.Match;
    const clubColor = this.getClubColor(event.clubId);
    return {
      id: event.id,
      title: isMatch ? "Match" : "Training",
      start: `${event.startTime}Z`,
      end: `${event.endTime}Z`,
      backgroundColor: clubColor,
      borderColor: clubColor,
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