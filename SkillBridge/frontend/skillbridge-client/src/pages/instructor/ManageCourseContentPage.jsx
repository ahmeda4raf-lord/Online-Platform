import { useParams } from "react-router-dom";
import PageHero from "../../components/common/PageHero";
import usePageTitle from "../../hooks/usePageTitle";

function ManageCourseContentPage() {
  const { courseId } = useParams();
  usePageTitle("Manage Content");

  return (
    <div className="page-stack">
      <PageHero
        eyebrow="Course content"
        title={`Organize sections and lessons for course #${courseId}`}
        description="This is the natural home for section ordering, lesson editing, preview toggles, and content publishing rules."
      />
      <section className="content-panel split-panel">
        <article>
          <h2>Sections</h2>
          <p>Build drag-friendly section ordering here later without changing the overall route structure.</p>
        </article>
        <article>
          <h2>Lessons</h2>
          <p>Each section can expand into lesson forms, preview settings, and video links when you implement them.</p>
        </article>
      </section>
    </div>
  );
}

export default ManageCourseContentPage;
