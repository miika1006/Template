import { FC, useState } from "react"; // we need this to make JSX compile

export const FrontPage: FC = () => {
	const [count, setCount] = useState(0);

	return (
		<div>
			<h1>Etusivu</h1>
			<p>Klikkasit {count} kertaa</p>
			<button onClick={() => setCount(count + 1)}>Click me</button>
		</div>
	);
};
