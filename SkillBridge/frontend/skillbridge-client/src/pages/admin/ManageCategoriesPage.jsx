import PageHero from "../../components/common/PageHero";
import usePageTitle from "../../hooks/usePageTitle";

function ManageCategoriesPage() {
  usePageTitle("Manage Categories");

  return (
    <div className="page-stack">
      <PageHero
        eyebrow="Category management"
        title="Create and maintain the course catalog taxonomy."
        description="This page is prepared for category CRUD through the shared API client."
      />
      <section className="content-panel">
        <p>Start with a simple category list and modal form, then connect it to the categories controller.</p>
      </section>
    </div>
  );
}

export default ManageCategoriesPage;
