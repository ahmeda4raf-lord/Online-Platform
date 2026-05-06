import PageHero from "../../components/common/PageHero";
import usePageTitle from "../../hooks/usePageTitle";

function CreateCoursePage() {
  usePageTitle("Create Course");

  return (
    <div className="page-stack">
      <PageHero
        eyebrow="Create course"
        title="Start a new draft with a simple authoring flow."
        description="This placeholder can grow into a form for title, description, price, level, category, and the first sections."
      />
      <section className="content-panel">
        <p>The backend `POST /api/courses` endpoint is already scaffolded for when you implement the real create form.</p>
      </section>
    </div>
  );
}

export default CreateCoursePage;
