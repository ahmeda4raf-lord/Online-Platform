import { NavLink } from "react-router-dom";
import { useAuth } from "../../context/AuthContext";
import { dashboardLinks } from "../../utils/navigation";

function Sidebar() {
  const { role } = useAuth();
  const links = dashboardLinks[role] || [];

  if (!links.length) {
    return null;
  }

  return (
    <aside className="sidebar">
      <p className="sidebar-title">{role} workspace</p>
      <nav className="sidebar-links">
        {links.map((link) => (
          <NavLink key={link.to} to={link.to}>
            {link.label}
          </NavLink>
        ))}
      </nav>
    </aside>
  );
}

export default Sidebar;
