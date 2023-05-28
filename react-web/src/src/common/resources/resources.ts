import { format } from "../utils/format";
import { localizedResources } from "./types";

export interface textresources {
	format: (val: string, ...args: string[] | number[]) => string;
	FRONTPAGE: string;
	SECONDPAGE: string;
	YOU_CLICKED: string;
	CLICK: string;
	//TODO: add more
}
export const resources: localizedResources = {
	fi: {
		format,
		FRONTPAGE: "Etusivu",
		SECONDPAGE: "Toinen sivu",
		YOU_CLICKED: "Klikkasit {0} kertaa",
		CLICK: "Klikkaa",
		//TODO: add more
	},
	en: {
		format,
		FRONTPAGE: "Frontpage",
		SECONDPAGE: "Second page",
		YOU_CLICKED: "You clicked {0} times",
		CLICK: "Click",
		//TODO: add more
	},
};
