import { Link, NavLink } from "react-router-dom";
import { useAuth } from "../../context/AuthContext";
import { dashboardLinks, publicLinks } from "../../utils/navigation";

function Navbar() {
  const { isAuthenticated, logout, role, user } = useAuth();
  const roleLinks = dashboardLinks[role] || [];

  return (
    <header className="navbar">
      <div className="container navbar-inner">
        <Link className="brand" to="/">
          SkillBridge
        </Link>

        <nav className="nav-links">
          {publicLinks.slice(0, 2).map((link) => (
            <NavLink key={link.to} to={link.to}>
              {link.label}
            </NavLink>
          ))}

          {isAuthenticated &&
            roleLinks.map((link) => (
              <NavLink key={link.to} to={link.to}>
                {link.label}
              </NavLink>
            ))}
        </nav>

        <div className="nav-actions">
          {isAuthenticated ? (
            <>
              <span className="nav-user">{user?.fullName}</span>
              <button className="button button-secondary" onClick={logout} type="button">
                Logout
              </button>
            </>
          ) : (
            <>
              <NavLink className="button button-secondary" to="/login">
                Login
              </NavLink>
              <NavLink className="button" to="/register">
                Join free
              </NavLink>
            </>
          )}
        </div>
      </div>
    </header>
  );
}

export default Navbar;
