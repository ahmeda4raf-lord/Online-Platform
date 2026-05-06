import PageHero from "../../components/common/PageHero";
import usePageTitle from "../../hooks/usePageTitle";

function MyLearningPage() {
  usePageTitle("My Learning");

  return (
    <div className="page-stack">
      <PageHero
        eyebrow="My courses"
        title="Track enrolled courses and progress."
        description="Connect this page to `GET /api/enrollments/my-courses` to show active learning items and completion status."
      />
      <section className="content-panel">
        <p>Placeholder cards can later become progress rows, continue-learning buttons, and completion badges.</p>
      </section>
    </div>
  );
}

export default MyLearningPage;
