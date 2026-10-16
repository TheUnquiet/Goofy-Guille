export interface LoginResponse {
  accessToken: string;
    expiresAt: string;
    user: {
        id: string;
        email: string;
        displayName: string;
        profileImageUrl: string | null;
        clubId: string | null;
        teamId: string | null;
        isAdmin: boolean;
    };
}