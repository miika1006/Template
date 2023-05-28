export type languages = "fi" | "en";
export interface textresources {
	FRONTPAGE: string;
	SECONDPAGE: string;
	YOU_CLICKED: string;
}
export interface localizedResources {
	fi: textresources;
	en: textresources;
	// format: (val:string)
}
const resources: localizedResources = {
	fi: {
		FRONTPAGE: "Etusivu",
		SECONDPAGE: "Toinen sivu",
		YOU_CLICKED: "Klikkasit {0} kertaa",
	},
	en: {
		FRONTPAGE: "Frontpage",
		SECONDPAGE: "Second page",
		YOU_CLICKED: "You clicked {0} times",
	},
	/*format = function () {
        // store arguments in an array
        var args = arguments;
        // use replace to iterate over the string
        // select the match and check if the related argument is present
        // if yes, replace the match with the argument
        return this.replace(/{([0-9]+)}/g, function (match, index) {
          // check if the argument is present
          return typeof args[index] == 'undefined' ? match : args[index];
        });
      };*/
};

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
