import { useCallback, useState } from "react";

export type languages = "fi" | "en";
export interface textresources {
	format: (val: string, ...args: string[] | number[]) => string;
	FRONTPAGE: string;
	SECONDPAGE: string;
	YOU_CLICKED: string;
	CLICK: string;
}
export interface localizedResources {
	fi: textresources;
	en: textresources;
}
const format = (val: string, ...args: string[] | number[]) => {
	return val.replace(/{([0-9]+)}/g, (match, index) => {
		// check if the argument is present
		return typeof args[index] == "undefined" ? match : args[index].toString();
	});
};
const resources: localizedResources = {
	fi: {
		format,
		FRONTPAGE: "Etusivu",
		SECONDPAGE: "Toinen sivu",
		YOU_CLICKED: "Klikkasit {0} kertaa",
		CLICK: "Klikkaa",
	},
	en: {
		format,
		FRONTPAGE: "Frontpage",
		SECONDPAGE: "Second page",
		YOU_CLICKED: "You clicked {0} times",
		CLICK: "Click",
	},
};

const getCurrentLanguage = () => {
	let currentLang = window.localStorage.getItem("lang") as languages;
	if (currentLang == null) {
		currentLang = "fi"; //TODO: Maybe get browsers language
		setCurrentLanguage(currentLang);
	}
	return currentLang;
};
const setCurrentLanguage = (lang: languages) =>
	window.localStorage.setItem("lang", lang);

const getLocalizedResources = (lang?: languages) => {
	const currentLang = lang ?? getCurrentLanguage();
	return resources[currentLang];
};

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
