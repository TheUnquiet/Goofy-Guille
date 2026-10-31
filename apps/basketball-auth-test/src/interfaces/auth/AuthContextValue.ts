import type { LoginResponse } from "./LoginResponse";

export interface AuthContextValue {
  login: LoginResponse | null;
  isAuthenticated: boolean;
  loginWithGoogle: (identityToken: string) => Promise<void>;
  logout: () => void;
}