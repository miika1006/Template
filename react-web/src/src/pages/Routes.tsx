import { Route } from "wouter";
import { FrontPage } from "./front";
import { SecondPage } from "./second";
import { Menu } from "../common/components/Menu";
import { useState } from "react";
import {
	getLocalizedResources,
	languages,
	setCurrentLanguage,
	textresources,
} from "../common/resources/resources";

function Routes() {
	const [resources, setResources] = useState<textresources>(
		getLocalizedResources()
	);
	const changeLanguage = (lang: languages) => {
		setCurrentLanguage(lang);
		setResources(getLocalizedResources(lang));
	};
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
