import CourseCard from "../../components/courses/CourseCard";
import PageHero from "../../components/common/PageHero";
import { featuredCourses } from "../../utils/mockData";
import usePageTitle from "../../hooks/usePageTitle";

function CoursesPage() {
  usePageTitle("Courses");

  return (
    <div className="page-stack">
      <PageHero
        eyebrow="Course catalog"
        title="Browse courses with room for filters, categories, and search."
        description="This page is ready for real API integration through the shared axios client and course DTO structure."
      />
      <section className="content-panel">
        <div className="course-grid">
          {featuredCourses.map((course) => (
            <CourseCard course={course} key={course.id} />
          ))}
        </div>
      </section>
    </div>
  );
}

export default CoursesPage;
