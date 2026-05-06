import PageHero from "../../components/common/PageHero";
import usePageTitle from "../../hooks/usePageTitle";

function MyCoursesPage() {
  usePageTitle("My Courses");

  return (
    <div className="page-stack">
      <PageHero
        eyebrow="Instructor catalog"
        title="Review drafts, pending submissions, and published courses."
        description="This page is prepared for `GET /api/instructor/my-courses` and action buttons like edit, delete, and submit for review."
      />
      <section className="content-panel">
        <p>Use this area for course tables or cards, status chips, and quick edit links.</p>
      </section>
    </div>
  );
}

export default MyCoursesPage;
