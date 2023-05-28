import { useCallback, useState } from "react";
import { getLocalizedResources, setCurrentLanguage } from "./functions";
import { textresources } from "./resources";
import { languages } from "./types";
/**
 * use Text Resources
 * @returns [resources[] dictionary, changeLanguage('en') -method]
 */
export const useTextResources = (): [
	textresources,
	(lang: languages) => void
] => {
	const [resources, setResources] = useState<textresources>(
		getLocalizedResources()
	);
	const changeLanguage = useCallback((lang: languages) => {
		setCurrentLanguage(lang);
		setResources(getLocalizedResources(lang));
	}, []);
	return [resources, changeLanguage];
};
