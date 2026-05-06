import { useState } from "react";
import { useNavigate } from "react-router-dom";
import axiosClient from "../../api/axiosClient";
import PageHero from "../../components/common/PageHero";
import { useAuth } from "../../context/AuthContext";
import usePageTitle from "../../hooks/usePageTitle";

function RegisterPage() {
  const navigate = useNavigate();
  const { login } = useAuth();
  const [form, setForm] = useState({
    fullName: "",
    email: "",
    password: "",
    role: "Student"
  });
  const [error, setError] = useState("");
  const [isSubmitting, setIsSubmitting] = useState(false);

  usePageTitle("Register");

  const handleChange = (event) => {
    setForm((current) => ({ ...current, [event.target.name]: event.target.value }));
  };

  const handleSubmit = async (event) => {
    event.preventDefault();
    setError("");
    setIsSubmitting(true);

    try {
      const { data } = await axiosClient.post("/auth/register", form);
      login(data);
      navigate("/");
    } catch (submitError) {
      setError(submitError.message);
    } finally {
      setIsSubmitting(false);
    }
  };

  return (
    <div className="page-stack">
      <PageHero
        eyebrow="Create account"
        title="Join SkillBridge as a student or instructor."
        description="Admins are seeded from the backend starter. Students and instructors can register from this shared entry point."
      />
      <section className="content-panel form-panel">
        <form className="form-grid" onSubmit={handleSubmit}>
          <label>
            Full name
            <input name="fullName" onChange={handleChange} required value={form.fullName} />
          </label>
          <label>
            Email
            <input name="email" onChange={handleChange} required type="email" value={form.email} />
          </label>
          <label>
            Password
            <input name="password" onChange={handleChange} required type="password" value={form.password} />
          </label>
          <label>
            Role
            <select name="role" onChange={handleChange} value={form.role}>
              <option value="Student">Student</option>
              <option value="Instructor">Instructor</option>
            </select>
          </label>
          {error ? <p className="form-error">{error}</p> : null}
          <button className="button" disabled={isSubmitting} type="submit">
            {isSubmitting ? "Creating account..." : "Register"}
          </button>
        </form>
      </section>
    </div>
  );
}

export default RegisterPage;
