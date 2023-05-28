import React, { FC } from "react"; // we need this to make JSX compile
import { textresources } from "../../common/resources";

type SecondPageProps = {
	resources: textresources;
};

export const SecondPage: FC<SecondPageProps> = ({ resources }) => {
	return (
		<div>
			<h1>{resources.SECONDPAGE}</h1>
		</div>
	);
};
