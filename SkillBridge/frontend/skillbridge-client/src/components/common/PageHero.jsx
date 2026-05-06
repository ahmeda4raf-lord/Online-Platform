function PageHero({ eyebrow, title, description, actions }) {
  return (
    <section className="page-hero">
      <p className="eyebrow">{eyebrow}</p>
      <h1>{title}</h1>
      <p className="page-description">{description}</p>
      {actions ? <div className="hero-actions">{actions}</div> : null}
    </section>
  );
}

export default PageHero;
