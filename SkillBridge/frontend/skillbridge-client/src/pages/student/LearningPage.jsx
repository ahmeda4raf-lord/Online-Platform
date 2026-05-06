import { useParams } from "react-router-dom";
import PageHero from "../../components/common/PageHero";
import usePageTitle from "../../hooks/usePageTitle";

function LearningPage() {
  const { courseId } = useParams();
  usePageTitle("Learning");

  return (
    <div className="page-stack">
      <PageHero
        eyebrow="Learning experience"
        title={`Continue course #${courseId}`}
        description="This page is ready for lesson playback, lesson completion, and sidebar navigation through course content."
      />
      <section className="content-panel split-panel">
        <article>
          <h2>Lesson player area</h2>
          <p>Video, text content, and attachments can live here once lesson delivery is implemented.</p>
        </article>
        <article>
          <h2>Progress actions</h2>
          <p>Hook the complete button to `POST /api/enrollments/lessons/complete` when you build the lesson flow.</p>
        </article>
      </section>
    </div>
  );
}

export default LearningPage;
