import { Route } from "wouter";
import { FrontPage } from "./front";
import { SecondPage } from "./second";
import { Menu } from "../common/components/Menu";
import { useTextResources } from "../common/resources";

function Routes() {
	const [resources, changeLanguage] = useTextResources();
	return (
		<div>
			<Menu resources={resources} changeLanguage={changeLanguage} />
			<Route path="/second">
				<SecondPage resources={resources} />
			</Route>
			<Route path="/">
				<FrontPage resources={resources} />
			</Route>
			{/* 
			<Route path="/about">About Us</Route>
			<Route path="/users/:name">
				{(params) => <div>Hello, {params.name}!</div>}
			</Route> */}
		</div>
	);
}

export default Routes;
