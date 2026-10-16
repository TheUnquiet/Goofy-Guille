import { GoogleLogin } from "@react-oauth/google";
import { useState } from "react";
import type { LoginResponse } from "./interfaces/LoginResponse";

function App() {
  const [message, setMessage] = useState("");
  const [login, setLogin] = useState<LoginResponse | null>(null);

  async function handleGoogleLogin(identityToken: string) {
    setMessage("Signing in..");
    setLogin(null);

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

      setLogin(data as LoginResponse);
      setMessage("Sign in successfully");
    } catch {
      setMessage("Could not reach API Check that it is running and that its HTTPS certificate is trusted.")
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
        </section>
      )}
    </main>
  )
}

export default App;