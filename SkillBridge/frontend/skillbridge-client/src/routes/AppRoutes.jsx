import { Navigate, Route, Routes } from "react-router-dom";
import Footer from "../components/layout/Footer";
import Navbar from "../components/layout/Navbar";
import ProtectedRoute from "../components/layout/ProtectedRoute";
import RoleBasedRoute from "../components/layout/RoleBasedRoute";
import Sidebar from "../components/layout/Sidebar";
import AdminDashboardPage from "../pages/admin/AdminDashboardPage";
import ManageCategoriesPage from "../pages/admin/ManageCategoriesPage";
import ManageUsersPage from "../pages/admin/ManageUsersPage";
import PendingCoursesPage from "../pages/admin/PendingCoursesPage";
import CreateCoursePage from "../pages/instructor/CreateCoursePage";
import EditCoursePage from "../pages/instructor/EditCoursePage";
import InstructorDashboardPage from "../pages/instructor/InstructorDashboardPage";
import ManageCourseContentPage from "../pages/instructor/ManageCourseContentPage";
import MyCoursesPage from "../pages/instructor/MyCoursesPage";
import CourseDetailsPage from "../pages/public/CourseDetailsPage";
import CoursesPage from "../pages/public/CoursesPage";
import HomePage from "../pages/public/HomePage";
import LoginPage from "../pages/public/LoginPage";
import RegisterPage from "../pages/public/RegisterPage";
import LearningPage from "../pages/student/LearningPage";
import MyLearningPage from "../pages/student/MyLearningPage";
import StudentDashboardPage from "../pages/student/StudentDashboardPage";

function AppShell({ children, withSidebar = false }) {
  return (
    <div className="app-shell">
      <Navbar />
      <main className="container main-content">
        {withSidebar ? (
          <div className="dashboard-layout">
            <Sidebar />
            <div className="dashboard-content">{children}</div>
          </div>
        ) : (
          children
        )}
      </main>
      <Footer />
    </div>
  );
}

function AppRoutes() {
  return (
    <Routes>
      <Route
        path="/"
        element={
          <AppShell>
            <HomePage />
          </AppShell>
        }
      />
      <Route
        path="/courses"
        element={
          <AppShell>
            <CoursesPage />
          </AppShell>
        }
      />
      <Route
        path="/courses/:courseId"
        element={
          <AppShell>
            <CourseDetailsPage />
          </AppShell>
        }
      />
      <Route
        path="/login"
        element={
          <AppShell>
            <LoginPage />
          </AppShell>
        }
      />
      <Route
        path="/register"
        element={
          <AppShell>
            <RegisterPage />
          </AppShell>
        }
      />

      <Route element={<ProtectedRoute />}>
        <Route element={<RoleBasedRoute allowedRoles={["Student"]} />}>
          <Route
            path="/student"
            element={
              <AppShell withSidebar>
                <StudentDashboardPage />
              </AppShell>
            }
          />
          <Route
            path="/student/my-learning"
            element={
              <AppShell withSidebar>
                <MyLearningPage />
              </AppShell>
            }
          />
          <Route
            path="/student/learning/:courseId"
            element={
              <AppShell withSidebar>
                <LearningPage />
              </AppShell>
            }
          />
        </Route>

        <Route element={<RoleBasedRoute allowedRoles={["Instructor"]} />}>
          <Route
            path="/instructor"
            element={
              <AppShell withSidebar>
                <InstructorDashboardPage />
              </AppShell>
            }
          />
          <Route
            path="/instructor/courses"
            element={
              <AppShell withSidebar>
                <MyCoursesPage />
              </AppShell>
            }
          />
          <Route
            path="/instructor/courses/create"
            element={
              <AppShell withSidebar>
                <CreateCoursePage />
              </AppShell>
            }
          />
          <Route
            path="/instructor/courses/:courseId/edit"
            element={
              <AppShell withSidebar>
                <EditCoursePage />
              </AppShell>
            }
          />
          <Route
            path="/instructor/courses/:courseId/content"
            element={
              <AppShell withSidebar>
                <ManageCourseContentPage />
              </AppShell>
            }
          />
        </Route>

        <Route element={<RoleBasedRoute allowedRoles={["Admin"]} />}>
          <Route
            path="/admin"
            element={
              <AppShell withSidebar>
                <AdminDashboardPage />
              </AppShell>
            }
          />
          <Route
            path="/admin/pending-courses"
            element={
              <AppShell withSidebar>
                <PendingCoursesPage />
              </AppShell>
            }
          />
          <Route
            path="/admin/users"
            element={
              <AppShell withSidebar>
                <ManageUsersPage />
              </AppShell>
            }
          />
          <Route
            path="/admin/categories"
            element={
              <AppShell withSidebar>
                <ManageCategoriesPage />
              </AppShell>
            }
          />
        </Route>
      </Route>

      <Route path="*" element={<Navigate replace to="/" />} />
    </Routes>
  );
}

export default AppRoutes;
