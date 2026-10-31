import { useAuth } from "@/auth/AuthContext";
import type ProtectedRouteProps from "@/interfaces/routing/ProtectedRouteProps ";
import { Navigate, Outlet } from "react-router-dom";

export default function ProtectedRoute({requiredRole}: ProtectedRouteProps) {
  const { login, isAuthenticated } = useAuth();
  if (!isAuthenticated || !login) return <Navigate to="/login" replace />
  const isAdmin = login.user.isAdmin;
  if (requiredRole === "Admin" && !isAdmin) return <Navigate to="/dashboard" replace />;
  if (requiredRole === "Member" && isAdmin) return <Navigate to="/dashboard" replace />;
  return <Outlet />;
}