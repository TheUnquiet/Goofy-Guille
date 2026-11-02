import type { EventType } from "./EventDto";

export default interface CreateEventDto {
  clubId: string;
  type: EventType;
  description: string | null;
  locationId: string;
  startTime: string;
  endTime: string;
  opponentClubId: string | null;
}