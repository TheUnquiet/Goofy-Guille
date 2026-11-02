import type CreateEventDto from "./CreateEventDto";

export default interface UpdateEventDto extends CreateEventDto {
  id: string;
}