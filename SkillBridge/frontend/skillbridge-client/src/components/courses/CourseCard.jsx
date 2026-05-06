import { Link } from "react-router-dom";

function CourseCard({ course }) {
  return (
    <article className="course-card">
      <div className="course-badge">{course.level}</div>
      <h3>{course.title}</h3>
      <p>{course.price}</p>
      <Link className="button button-secondary" to={`/courses/${course.id}`}>
        View details
      </Link>
    </article>
  );
}

export default CourseCard;
