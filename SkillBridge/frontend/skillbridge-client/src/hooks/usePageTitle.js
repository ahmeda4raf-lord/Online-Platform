import { useEffect } from "react";

function usePageTitle(title) {
  useEffect(() => {
    document.title = title ? `${title} | SkillBridge` : "SkillBridge";
  }, [title]);
}

export default usePageTitle;
