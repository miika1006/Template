import { FC, useState } from "react"; // we need this to make JSX compile
import { textresources } from "../../common/resources/resources";

type FrontPageProps = {
	resources: textresources;
};

export const FrontPage: FC<FrontPageProps> = ({ resources }) => {
	const [count, setCount] = useState(0);

	return (
		<div>
			<h1>{resources.FRONTPAGE}</h1>
			<p>
				{resources.YOU_CLICKED} {count}
			</p>
			<button onClick={() => setCount(count + 1)}>Click me</button>
		</div>
	);
};
