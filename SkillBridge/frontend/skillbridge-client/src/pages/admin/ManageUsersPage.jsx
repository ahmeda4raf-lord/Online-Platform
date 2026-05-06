import PageHero from "../../components/common/PageHero";
import usePageTitle from "../../hooks/usePageTitle";

function ManageUsersPage() {
  usePageTitle("Manage Users");

  return (
    <div className="page-stack">
      <PageHero
        eyebrow="User moderation"
        title="View users, roles, and block status."
        description="The backend starter already exposes user listing and block or unblock endpoints for this page."
      />
      <section className="content-panel">
        <p>This is the right place for search, role filters, and moderation actions once admin tooling is implemented.</p>
      </section>
    </div>
  );
}

export default ManageUsersPage;
