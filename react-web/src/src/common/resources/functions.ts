import { resources } from "./resources";
import { languages } from "./types";

export const getCurrentLanguage = () => {
	let currentLang = window.localStorage.getItem("lang") as languages;
	if (currentLang == null) {
		currentLang = "fi"; //TODO: Maybe get browsers language
		setCurrentLanguage(currentLang);
	}
	return currentLang;
};
export const setCurrentLanguage = (lang: languages) =>
	window.localStorage.setItem("lang", lang);

export const getLocalizedResources = (lang?: languages) => {
	const currentLang = lang ?? getCurrentLanguage();
	return resources[currentLang];
};
