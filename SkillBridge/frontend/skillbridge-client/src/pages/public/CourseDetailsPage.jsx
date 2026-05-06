import { useParams } from "react-router-dom";
import PageHero from "../../components/common/PageHero";
import usePageTitle from "../../hooks/usePageTitle";

function CourseDetailsPage() {
  const { courseId } = useParams();
  usePageTitle("Course Details");

  return (
    <div className="page-stack">
      <PageHero
        eyebrow="Course details"
        title={`Course #${courseId}`}
        description="This placeholder page is ready for published course details, reviews, lessons preview, and enrollment actions."
      />
      <section className="content-panel split-panel">
        <article>
          <h2>What students will see</h2>
          <p>Course summary, instructor info, category, level, pricing, and public lessons preview.</p>
        </article>
        <article>
          <h2>Next API hookup</h2>
          <p>Connect this route to `GET /api/courses/{'{id}'}` and render the section and lesson DTOs.</p>
        </article>
      </section>
    </div>
  );
}

export default CourseDetailsPage;
