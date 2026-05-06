import PageHero from "../../components/common/PageHero";
import usePageTitle from "../../hooks/usePageTitle";

function PendingCoursesPage() {
  usePageTitle("Pending Courses");

  return (
    <div className="page-stack">
      <PageHero
        eyebrow="Course approval"
        title="Review submitted courses before publishing."
        description="This page is ready for approve and reject actions against the admin API endpoints."
      />
      <section className="content-panel">
        <p>Wire this page to `GET /api/admin/pending-courses`, then add approve and reject buttons for each course row.</p>
      </section>
    </div>
  );
}

export default PendingCoursesPage;
