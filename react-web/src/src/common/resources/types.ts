import { textresources } from "./resources";

export type languages = "fi" | "en";
export interface localizedResources {
	fi: textresources;
	en: textresources;
}
