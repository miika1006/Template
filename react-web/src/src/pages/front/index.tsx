import { FC, useState } from "react"; // we need this to make JSX compile
import { textresources } from "../../common/resources";
import { useDelayedLoading } from "use-delayed-loading";

type FrontPageProps = {
	resources: textresources;
};

export const FrontPage: FC<FrontPageProps> = ({ resources }) => {
	const [count, setCount] = useState(0);
	const [loading, setLoading] = useDelayedLoading(false);

	const loadData = async () => {
		setLoading(true);
		const result = await fetch("https://dummyjson.com/products").then((res) =>
			res.json()
		);
		setLoading(false);
	};
	return (
		<div>
			<h1>{resources.FRONTPAGE}</h1>
			<p>{resources.format(resources.YOU_CLICKED, count)}</p>
			<button onClick={() => setCount(count + 1)}>{resources.CLICK}</button>
			<button onClick={() => loadData()}>Lataa</button>
			{loading && <div>Ladataan...</div>}
		</div>
	);
};
