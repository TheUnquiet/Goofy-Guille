
import { useAuth } from "@/auth/AuthContext";
import { GoogleLogin } from "@react-oauth/google";
import { useState } from "react";

export default function LoginPage() {
  const { loginWithGoogle } = useAuth();
  const [message, setMessage] = useState("");

  async function handleGoogleLogin(identityToken: string) {
    setMessage("Signing in...");

    try {
      await loginWithGoogle(identityToken);
    } catch {
      setMessage("Sign in failed. Please try again.");
    }
  }

  return (
    <main className="flex min-h-screen items-center justify-center bg-background p-6">
      <div className="w-full max-w-md space-y-6 rounded-xl border bg-card p-8 shadow-sm">
        <div className="space-y-2 text-center">
          <h1 className="text-3xl font-bold tracking-tight">
            Basketball Club
          </h1>
          <p className="text-muted-foreground">
            Sign in to manage your club activities.
          </p>
        </div>

        <div className="flex justify-center">
          <GoogleLogin
            onSuccess={(response) => {
              if (!response.credential) {
                setMessage("Google did not return a token.");
                return;
              }

              void handleGoogleLogin(response.credential);
            }}
            onError={() => setMessage("Google sign in failed.")}
          />
        </div>

        {message && (
          <p className="text-center text-sm text-destructive">
            {message}
          </p>
        )}
      </div>
    </main>
  );
}