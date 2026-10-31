import type StatCardProps from "@/interfaces/ui/StatCardProps ";
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card";
import { useAuth } from "@/auth/AuthContext";
import { CalendarDays, Users, Trophy, MapPin,} from "lucide-react";

function StatCard({ title, value, description, icon: Icon, }: StatCardProps) {
  return (
    <Card>
      <CardHeader className="flex flex-row items-center justify-between space-y-0 pb-2">
        <CardTitle className="text-sm font-medium">
          {title}
        </CardTitle>
        <Icon className="size-4 text-muted-foreground" />
      </CardHeader>

      <CardContent>
        <div className="text-2xl font-bold">{value}</div>
        <p className="text-xs text-muted-foreground">
          {description}
        </p>
      </CardContent>
    </Card>
  );
}

export default function DashboardOverview() {
  const { login } = useAuth();
  const isAdmin = login?.user.isAdmin ?? false;
  const stats = isAdmin 
    ? [
       {
         title: "Clubs",
          value: "—",
          description: "Registered clubs",
          icon: Trophy,
       },
       {
          title: "Members",
          value: "—",
          description: "Registered members",
          icon: Users,
        },
        {
          title: "Locations",
          value: "—",
          description: "Available locations",
          icon: MapPin,
        },
    ] :
    [
        {
          title: "Upcoming Events",
          value: "—",
          description: "Your club's upcoming events",
          icon: CalendarDays,
        },
        {
          title: "Club Members",
          value: "—",
          description: "Members in your club",
          icon: Users,
        },
    ];
    return (
    <div className="space-y-8">
      <div>
        <h1 className="text-2xl font-bold tracking-tight">
          Overview
        </h1>
        <p className="text-muted-foreground">
          {isAdmin
            ? "Here's an overview of your basketball platform."
            : "Here's what's happening with your club."}
        </p>
      </div>

      <div className="grid gap-4 sm:grid-cols-2 xl:grid-cols-4">
        {stats.map((stat) => (
          <StatCard key={stat.title} {...stat} />
        ))}
      </div>

      <Card>
        <CardHeader>
          <CardTitle>Getting started</CardTitle>
        </CardHeader>
        <CardContent>
          <p className="text-sm text-muted-foreground">
            {isAdmin
              ? "Manage clubs, members, teams, locations and events from the navigation menu."
              : "Check your calendar to see upcoming matches and training sessions."}
          </p>
        </CardContent>
      </Card>
    </div>
  );
}