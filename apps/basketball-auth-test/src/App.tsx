import { useAuth } from "./auth/AuthContext";
import LoginPage from "./pages/LoginPage";

function App() {
  const { login, isAuthenticated, logout } = useAuth();

  if (!isAuthenticated || !login) {
    return <LoginPage />;
  }

  return (
    <main className="min-h-screen bg-background p-8">
      <div className="mx-auto max-w-5xl space-y-6">
        <header className="flex items-center justify-between">
          <div>
            <h1 className="text-3xl font-bold">Welcome, {login.user.displayName}!</h1>
            <p className="text-muted-foreground">{login.user.isAdmin ? "Administrator" : "Club member"}</p>
          </div>
          <button onClick={logout} className="rounded-md border px-4 py-2 text-sm hover:bg-muted">Sign out</button>
        </header>
        <section className="rounded-xl font-semibold">
          <h2 className="text-xl font-semibold">{login.user.isAdmin ? "Admin Dashboard" : "Member Dashboard"}</h2>
          <p className="mt-2 text-muted-foreground">Your Dashboard will be built here...</p>
        </section>
      </div>
    </main>
  );
}

export default App;