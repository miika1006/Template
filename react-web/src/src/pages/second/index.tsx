import { FC } from "react"; // we need this to make JSX compile
import { textresources } from "../../common/resources";
import { useDelayedLoading } from "use-delayed-loading";

type SecondPageProps = {
	resources: textresources;
};

export const SecondPage: FC<SecondPageProps> = ({ resources }) => {
	const [loading, setLoading] = useDelayedLoading(false, 1000);

	const loadData = async () => {
		setLoading(true);
		const result = await fetch("https://dummyjson.com/products").then((res) =>
			res.json()
		);
		setLoading(false);
	};
	return (
		<div>
			<h1>{resources.SECONDPAGE}</h1>
			<button onClick={() => loadData()}>Lataa</button>
			{loading && <div>Ladataan...</div>}
		</div>
	);
};
