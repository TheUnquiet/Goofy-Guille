export default interface NavigationItem {
  title: string,
  href: string,
  icon: React.ComponentType<{ className?: string}>;
}