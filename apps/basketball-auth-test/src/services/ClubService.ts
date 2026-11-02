import { apiClient } from "@/api/ApiClient";

export interface ClubDto {
  id: string;
  name: string;
}

export interface CreateClubDto {
  name: string;
}

export interface UpdateClubDto {
  id: string;
  name: string;
}

export class ClubService {
  async getAll(): Promise<ClubDto[]> {
    return apiClient.get<ClubDto[]>("/Club");
  }

  async getById(id: string): Promise<ClubDto> {
    return apiClient.get<ClubDto>(`/Club/${id}`);
  }

  async create(dto: CreateClubDto): Promise<ClubDto> {
    return apiClient.post<CreateClubDto, ClubDto>("/Club", dto);
  }

  async update(dto: UpdateClubDto): Promise<ClubDto> {
    return apiClient.put<UpdateClubDto, ClubDto>("/Club", dto);
  }

  async delete(id: string): Promise<void> {
    return apiClient.delete(`/Club/${id}`);
  }
}

export const clubService = new ClubService();