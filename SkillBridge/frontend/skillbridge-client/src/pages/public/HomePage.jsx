import { Link } from "react-router-dom";
import CourseCard from "../../components/courses/CourseCard";
import PageHero from "../../components/common/PageHero";
import { featuredCourses } from "../../utils/mockData";
import usePageTitle from "../../hooks/usePageTitle";

function HomePage() {
  usePageTitle("Home");

  return (
    <div className="page-stack">
      <PageHero
        eyebrow="Online learning platform"
        title="Bridge skills, careers, and better course publishing."
        description="SkillBridge gives students a clear learning path, instructors a clean publishing workflow, and admins the tools to keep quality high."
        actions={
          <>
            <Link className="button" to="/courses">
              Explore courses
            </Link>
            <Link className="button button-secondary" to="/register">
              Start learning
            </Link>
          </>
        }
      />

      <section className="content-panel">
        <div className="section-heading">
          <h2>Featured starter courses</h2>
          <p>Clean placeholder content so the app never feels empty during early development.</p>
        </div>
        <div className="course-grid">
          {featuredCourses.map((course) => (
            <CourseCard course={course} key={course.id} />
          ))}
        </div>
      </section>
    </div>
  );
}

export default HomePage;
