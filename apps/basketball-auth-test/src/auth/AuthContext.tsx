import { createContext, useContext, useEffect, useState } from "react";
import { apiClient } from "@/api/ApiClient";
import type { AuthContextValue } from "@/interfaces/auth/AuthContextValue";
import type { LoginResponse } from "@/interfaces/auth/LoginResponse";
import type { AuthProviderProps } from "@/interfaces/auth/AuthProviderProps";

const STORAGE_KEY = "basketball-auth";

const AuthContext = createContext<AuthContextValue | undefined>(undefined);

function getStoredLogin(): LoginResponse | null {
  const storedLogin = sessionStorage.getItem(STORAGE_KEY);

  if (!storedLogin) return null;

  try {
    const login = JSON.parse(storedLogin) as LoginResponse;

    if (new Date(login.expiresAt).getTime() <= Date.now()) {
      sessionStorage.removeItem(STORAGE_KEY);
      return null;
    }

    return login;
  } catch {
    sessionStorage.removeItem(STORAGE_KEY);
    return null;
  }
}

export function AuthProvider({ children }: AuthProviderProps) {
  const [login, setLogin] = useState<LoginResponse | null>(
    getStoredLogin
  );

  useEffect(() => {
    apiClient.setAccessToken(login?.accessToken ?? null);
  }, [login]);

  async function loginWithGoogle(identityToken: string): Promise<void> {
    apiClient.setAccessToken(null);
    setLogin(null);

    const response = await apiClient.post<
      { provider: string; identityToken: string },
      LoginResponse
    >("/Authentication/login", {
      provider: "Google",
      identityToken,
    });

    sessionStorage.setItem(STORAGE_KEY, JSON.stringify(response));

    apiClient.setAccessToken(response.accessToken);
    setLogin(response);
  }

  function logout(): void {
    sessionStorage.removeItem(STORAGE_KEY);
    apiClient.setAccessToken(null);
    setLogin(null);
  }

  return (
    <AuthContext.Provider
      value={{
        login,
        isAuthenticated: login !== null,
        loginWithGoogle,
        logout,
      }}
    >
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