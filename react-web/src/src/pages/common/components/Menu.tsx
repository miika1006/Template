import { FC } from "react"; // we need this to make JSX compile
import { Link } from "wouter";

export const Menu: FC = () => {
	return (
		<div>
			<Link href="/">Etusivu</Link>
			<Link href="/second">Toinen sivu</Link>
		</div>
	);
};
