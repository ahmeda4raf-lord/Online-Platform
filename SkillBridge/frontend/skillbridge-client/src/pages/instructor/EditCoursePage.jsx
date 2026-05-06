import { useParams } from "react-router-dom";
import PageHero from "../../components/common/PageHero";
import usePageTitle from "../../hooks/usePageTitle";

function EditCoursePage() {
  const { courseId } = useParams();
  usePageTitle("Edit Course");

  return (
    <div className="page-stack">
      <PageHero
        eyebrow="Edit draft"
        title={`Update course #${courseId}`}
        description="Use this page for the core course form, validation, and save-draft flow."
      />
      <section className="content-panel">
        <p>Connect this route to `PUT /api/courses/{'{id}'}` once you build the editable authoring UI.</p>
      </section>
    </div>
  );
}

export default EditCoursePage;
