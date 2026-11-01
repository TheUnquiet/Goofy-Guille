import { Navigate, Route, Routes } from "react-router-dom";
import { useAuth } from "@/auth/AuthContext";
import LoginPage from "@/pages/LoginPage"
import ProtectedRoute from "./ProtectedRoute";
import DashboardLayout from "@/components/layout/DashboardLayout";
import DashboardOverview from "@/components/dashboard/DashboardOverview";
import CalendarPage from "@/pages/CalendarPage";

export default function AppRoutes() {
  const { isAuthenticated, login } = useAuth();

  return (
    <Routes>
      <Route
        path="/login"
        element={
          isAuthenticated
            ? <Navigate to="/dashboard" replace />
            : <LoginPage />
        }
      />

      <Route element={<ProtectedRoute />}>
        <Route element={<DashboardLayout />}>
          <Route
            path="/dashboard"
            element={<DashboardOverview />}
          />
          <Route
            path="/calendar"
            element={<CalendarPage />}
          />
        </Route>
      </Route>

      <Route
        path="/"
        element={
          <Navigate
            to={isAuthenticated && login ? "/dashboard" : "/login"}
            replace
          />
        }
      />

      <Route
        path="*"
        element={
          <Navigate
            to={isAuthenticated && login ? "/dashboard" : "/login"}
            replace
          />
        }
      />
    </Routes>
  );
}