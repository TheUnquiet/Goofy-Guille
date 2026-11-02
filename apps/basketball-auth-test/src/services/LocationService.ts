import { apiClient } from "@/api/ApiClient";

export interface LocationDto {
  id: string;
  name: string;
  street: string;
  streetNumber: string;
  postalCode: string;
  city: string;
  latitude: number | null;
  longitude: number | null;
}

export interface CreateLocationDto {
  name: string;
  street: string;
  streetNumber: string;
  postalCode: string;
  city: string;
  latitude: number | null;
  longitude: number | null;
}

export interface UpdateLocationDto extends CreateLocationDto {
  id: string;
}

export class LocationService {
  async getAll(): Promise<LocationDto[]> {
    return apiClient.get<LocationDto[]>("/Location");
  }

  async getById(id: string): Promise<LocationDto> {
    return apiClient.get<LocationDto>(`/Location/${id}`);
  }

  async create(dto: CreateLocationDto): Promise<LocationDto> {
    return apiClient.post<CreateLocationDto, LocationDto>(
      "/Location",
      dto
    );
  }

  async update(dto: UpdateLocationDto): Promise<LocationDto> {
    return apiClient.put<UpdateLocationDto, LocationDto>(
      "/Location",
      dto
    );
  }

  async delete(id: string): Promise<void> {
    return apiClient.delete(`/Location/${id}`);
  }
}

export const locationService = new LocationService();