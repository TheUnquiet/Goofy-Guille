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

  async function testJwtAuthentication() {
    if (!login) {
      setMessage("Please sign in first.");
      return;
    }

    try {
      const response = await fetch(
        "https://localhost:7115/api/TestAuth",
        {
          method: "GET",
          headers: {
            Authorization: `Bearer ${login.accessToken}`,
          },
        },
      );

      const data = await response.json();
      if (!response.ok) {
        setMessage(`JWT test failed: ${response.status}`);
        return;
      }

      //setMessage(`JWT works! Authenticated user ID: ${data.userId}`);
      setMessage(JSON.stringify(data, null, 2));
    } catch {
      setMessage("Could not reach protected API endpoint.");
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