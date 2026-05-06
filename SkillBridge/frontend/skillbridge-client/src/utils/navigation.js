export const publicLinks = [
  { label: "Home", to: "/" },
  { label: "Courses", to: "/courses" },
  { label: "Login", to: "/login" },
  { label: "Register", to: "/register" }
];

export const dashboardLinks = {
  Student: [
    { label: "Dashboard", to: "/student" },
    { label: "My Learning", to: "/student/my-learning" }
  ],
  Instructor: [
    { label: "Dashboard", to: "/instructor" },
    { label: "My Courses", to: "/instructor/courses" },
    { label: "Create Course", to: "/instructor/courses/create" }
  ],
  Admin: [
    { label: "Dashboard", to: "/admin" },
    { label: "Pending Courses", to: "/admin/pending-courses" },
    { label: "Manage Users", to: "/admin/users" },
    { label: "Categories", to: "/admin/categories" }
  ]
};
