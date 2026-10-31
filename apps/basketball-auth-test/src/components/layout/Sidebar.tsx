import { useAuth } from "@/auth/AuthContext";
import type NavigationItem from "@/interfaces/ui/NavigationItem";
import { CalendarDays, LayoutDashboard, MapPin, Shield, Trophy, Users } from "lucide-react";

export default function Sidebar() {
  const { login } = useAuth();
  const isAdmin = login?.user.isAdmin ?? false;
  const navigationItems: NavigationItem[] = [
    {
      title: "Overview",
      href: "/dashboard",
      icon: LayoutDashboard,
    },
    {
      title: "Calendar",
      href: "/calendar",
      icon: CalendarDays
    },
    ...(isAdmin
      ? [
        {
           title: "Clubs",
            href: "/clubs",
            icon: Trophy,
        },
         {
            title: "Teams",
            href: "/teams",
            icon: Shield,
          },
          {
            title: "Members",
            href: "/members",
            icon: Users,
          },
          {
            title: "Locations",
            href: "/locations",
            icon: MapPin,
          },
      ]
    : []),
  ];

  return (
    <aside className="flex h-screen w-64 flex-col border-r bg-card">
      <div className="flex h-16 items-center border-b px-6">
        <h1 className="text-lg font-bold tracking-tight">
          Basketball Club
        </h1>
      </div>

      <nav className="flex-1 space-y-1 p-4">
        <p className="mb-3 px-3 text-xs font-semibold uppercase tracking-wider text-muted-foreground">
          Menu
        </p>

        {navigationItems.map((item) => {
          const Icon = item.icon;

          return (
            <a
              key={item.href}
              href={item.href}
              className="flex items-center gap-3 rounded-md px-3 py-2 text-sm font-medium text-muted-foreground transition-colors hover:bg-accent hover:text-accent-foreground"
            >
              <Icon className="size-4" />
              {item.title}
            </a>
          );
        })}
      </nav>

      <div className="border-t p-4">
        <p className="px-3 text-xs text-muted-foreground">
          {isAdmin ? "Administrator" : "Club Member"}
        </p>
      </div>
    </aside>
  );
}