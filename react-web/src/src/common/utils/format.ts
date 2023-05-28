export const format = (val: string, ...args: string[] | number[]) => {
	return val.replace(/{([0-9]+)}/g, (match, index) => {
		return typeof args[index] == "undefined" ? match : args[index].toString();
	});
};
