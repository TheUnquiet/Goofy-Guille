const API_BASE_URL = "https://localhost:7115/api";

export class ApiClient {
  private accessToken: string | null = null;
  setAccessToken(token: string | null): void {
    this.accessToken = token;
  }

  async get<T>(endpoint: string): Promise<T> {
    return this.request<T>(endpoint, {
      method: "GET",
    });
  }

  async post<TRequest, TResponse>(endpoint: string, body: TRequest,): Promise<TResponse> {
    return this.request<TResponse>(endpoint, {
      method: "POST",
      body: JSON.stringify(body),
    });
  }

  async put<TRequest, TResponse>(endpoint: string, body: TRequest): Promise<TResponse> {
    return this.request<TResponse>(endpoint, {
      method: "PUT",
      body: JSON.stringify(body),
    });
  }

  async delete(endpoint: string): Promise<void> {
    await this.request<void>(endpoint, {
      method: "DELETE",
    });
  }

  private async request<T>(endpoint: string, options: RequestInit,): Promise<T> {
    const headers = new Headers(options.headers);
    headers.set("Content-Type", "application/json");
    if (this.accessToken) {
      headers.set("Authorization", `Bearer ${this.accessToken}`);
    }
    const response = await fetch(
      `${API_BASE_URL}${endpoint}`,
      {
        ...options,
        headers,
      },
    );
    const data = await response.json().catch(() => null);
    if (!response.ok) {
      throw new Error(data?.message ?? `API request failed (${response.status}).`);
    }
    return data as T;
  }
}

export const apiClient = new ApiClient();