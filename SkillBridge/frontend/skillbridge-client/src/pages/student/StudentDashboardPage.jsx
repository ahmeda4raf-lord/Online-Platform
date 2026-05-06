import PageHero from "../../components/common/PageHero";
import StatGrid from "../../components/common/StatGrid";
import usePageTitle from "../../hooks/usePageTitle";
import { dashboardStats } from "../../utils/mockData";

function StudentDashboardPage() {
  usePageTitle("Student Dashboard");

  return (
    <div className="page-stack">
      <PageHero
        eyebrow="Student dashboard"
        title="Keep momentum with a simple learning overview."
        description="This dashboard is ready for enrolled courses, progress summaries, and recent lesson activity."
      />
      <StatGrid items={dashboardStats.Student} />
    </div>
  );
}

export default StudentDashboardPage;
