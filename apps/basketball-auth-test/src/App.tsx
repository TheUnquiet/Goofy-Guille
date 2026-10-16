import { GoogleLogin } from "@react-oauth/google";
import { useState } from "react";
import type { LoginResponse } from "./interfaces/LoginResponse";
import { apiClient } from "./api/ApiClient";

function App() {
  const [message, setMessage] = useState("");
  const [login, setLogin] = useState<LoginResponse | null>(null);

  async function handleGoogleLogin(identityToken: string) {
    setMessage("Signing in..");
    setLogin(null);
    apiClient.setAccessToken(null);

    try {
      const response = await fetch(
        "https://localhost:7115/api/Authentication/login",
        {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify({provider: "Google", identityToken}),
        }
      );

      const data = await response.json();
      if (!response.ok) {
        setMessage(data.message ?? "Sign in failed");
        return;
      }

      const loginResponse = data as LoginResponse;
      apiClient.setAccessToken(loginResponse.accessToken);
      setLogin(loginResponse);
      setMessage("Sign in successfully");
    } catch {
      setMessage("Could not reach API Check that it is running and that its HTTPS certificate is trusted.")
    }
  }

  async function testJwtAuthentication() {
    if (!login) {
      setMessage("Please sign in first.");
      return;
    }

    try {
      const data = await apiClient.get<{
        message: string,
        userId: string,
        name: string,
        role: string,
      }>("/TestAuth");
      setMessage(`JWT works! User: ${data.name} (${data.role}), ID: ${data.userId}`,);
    } catch (error) {
       setMessage(error instanceof Error ? error.message: "JWT test failed.",);
    }
  }

  return (
    <main>
      <h1>BasketBall Club</h1>
      <p>Sign in with Google</p>
      <GoogleLogin 
        onSuccess={(response) => {
          if (!response.credential) {
            setMessage("Google did not return a token");
            return;
          }
          void handleGoogleLogin(response.credential);
        }}
        onError={() => setMessage("Google sing in failed")}
      />
      {message && <p>{message}</p>}
      {login && (
        <section>
          <h2>Welcome, {login.user.displayName}!</h2>
          <p>Email: {login.user.email}</p>
          <p>Admin: {login.user.isAdmin ? "Yes" : "No"}</p>
          <p>
              JWT expires:{" "}
              {new Date(login.expiresAt).toLocaleString()}
          </p>
          <button onClick={testJwtAuthentication}>Test JWT Authentication</button>
        </section>
      )}
    </main>
  )
}

export default App;