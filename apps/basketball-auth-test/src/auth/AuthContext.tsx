import { createContext, useContext, useState } from "react";
import type { AuthContextValue } from "../interfaces/AuthContextValue";
import type { AuthProviderProps } from "../interfaces/AuthProviderProps";
import type { LoginResponse } from "../interfaces/LoginResponse";
import { apiClient } from "../api/ApiClient";

const AuthContext = createContext<AuthContextValue | undefined>(undefined,);

export function AuthProvider({ children }: AuthProviderProps) {
  const [login, setLogin] = useState<LoginResponse | null>(null);
  async function loginWithGoogle(identityToken: string): Promise<void> {
    apiClient.setAccessToken(null);
    setLogin(null);

    const response = await apiClient.post<{ provider: string; identityToken: string }, LoginResponse>("/Authentication/login", {
            provider: "Google",
            identityToken,
        });

    apiClient.setAccessToken(response.accessToken);
    setLogin(response);
  }

  function logout(): void {
    apiClient.setAccessToken(null);
    setLogin(null);
  }

  return (
    <AuthContext.Provider value={{login, isAuthenticated: login !== null, loginWithGoogle, logout}}>
      {children}
    </AuthContext.Provider>
  );
}

export function useAuth(): AuthContextValue {
  const context = useContext(AuthContext);
  if (!context) {
    throw new Error("useAuth must be used inside an AuthProvider.");
  }
  return context;
}