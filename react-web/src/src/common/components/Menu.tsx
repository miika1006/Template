import { FC } from "react"; // we need this to make JSX compile
import { Link } from "wouter";
import { languages, textresources } from "../resources";
import "./Menu.css";
type MenuProps = {
	resources: textresources;
	changeLanguage: (lang: languages) => void;
};

export const Menu: FC<MenuProps> = ({ resources, changeLanguage }) => {
	return (
		<div className="menu">
			<Link href="/">{resources.FRONTPAGE}</Link>
			<Link href="/second">{resources.SECONDPAGE}</Link>
			<button onClick={() => changeLanguage("fi")}>fi</button>{" "}
			<button onClick={() => changeLanguage("en")}>en</button>
		</div>
	);
};
