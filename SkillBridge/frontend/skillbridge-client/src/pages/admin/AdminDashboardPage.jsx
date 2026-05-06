import PageHero from "../../components/common/PageHero";
import StatGrid from "../../components/common/StatGrid";
import usePageTitle from "../../hooks/usePageTitle";
import { dashboardStats } from "../../utils/mockData";

function AdminDashboardPage() {
  usePageTitle("Admin Dashboard");

  return (
    <div className="page-stack">
      <PageHero
        eyebrow="Admin control room"
        title="Oversee quality, users, and platform readiness."
        description="This dashboard gives you a clean starting point for moderation, approvals, and platform operations."
      />
      <StatGrid items={dashboardStats.Admin} />
    </div>
  );
}

export default AdminDashboardPage;
