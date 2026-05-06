import PageHero from "../../components/common/PageHero";
import StatGrid from "../../components/common/StatGrid";
import usePageTitle from "../../hooks/usePageTitle";
import { dashboardStats } from "../../utils/mockData";

function InstructorDashboardPage() {
  usePageTitle("Instructor Dashboard");

  return (
    <div className="page-stack">
      <PageHero
        eyebrow="Instructor workspace"
        title="Manage your catalog and publish with confidence."
        description="The initial structure is ready for draft management, student lists, and course approval workflows."
      />
      <StatGrid items={dashboardStats.Instructor} />
    </div>
  );
}

export default InstructorDashboardPage;
