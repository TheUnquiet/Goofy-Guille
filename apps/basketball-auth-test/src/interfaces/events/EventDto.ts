export const EventType = {
  Training: 0,
  Match: 1,
} as const;

export type EventType =
  (typeof EventType)[keyof typeof EventType];

export default interface EventDto {
  id: string;
  clubId: string;
  createdByUserId: string;
  type: EventType;
  description: string | null;
  locationId: string;
  startTime: string;
  endTime: string;
  opponentClubId: string | null;
}