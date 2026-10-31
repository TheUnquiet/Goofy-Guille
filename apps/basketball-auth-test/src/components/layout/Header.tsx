import { LogOut, UserCircle } from "lucide-react";
import { useAuth } from "@/auth/AuthContext";

export default function Header() {
  const { login, logout } = useAuth();
  if (!login) return null;
  const { displayName, profileImageUrl, isAdmin } = login.user;
  console.log("Profile image URL:", profileImageUrl);

  return (
    <header className="flex h-16 items-center justify-between border-b bg-background px-6">
      <div>
        <h2 className="text-sm font-semibold">
          Welcome, {displayName}
        </h2>
        <p className="text-xs text-muted-foreground">
          {isAdmin ? "Administrator" : "Club Member"}
        </p>
      </div>

      <div className="flex items-center gap-4">
        <div className="flex items-center gap-2">
          {profileImageUrl ? (
            <img
              src={profileImageUrl}
              alt={`${displayName}'s profile`}
              referrerPolicy="no-referrer"
              className="size-9 rounded-full object-cover"
              onError={(event) => {
                event.currentTarget.style.display = "none";
              }}
            />
          ) : (
            <UserCircle className="size-9 text-muted-foreground" />
          )}

          <span className="hidden text-sm font-medium sm:block">
            {displayName}
          </span>
        </div>

        <button
          onClick={logout}
          className="flex items-center gap-2 rounded-md border px-3 py-2 text-sm font-medium transition-colors hover:bg-accent"
        >
          <LogOut className="size-4" />
          <span className="hidden sm:inline">Sign out</span>
        </button>
      </div>
    </header>
  );
}