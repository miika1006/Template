export type languages = "fi" | "en";
export interface localizedResources {
	fi: textresources;
	en: textresources;
}
export interface textresources {
	format: (val: string, ...args: string[] | number[]) => string;
	FRONTPAGE: string;
	SECONDPAGE: string;
	YOU_CLICKED: string;
	CLICK: string;
	//TODO: add more
}
