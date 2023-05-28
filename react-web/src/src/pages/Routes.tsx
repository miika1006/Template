import { Route } from "wouter";
import { FrontPage } from "./front";
import { SecondPage } from "./second";
import { Menu } from "./common/components/menu";

function Routes() {
	return (
		<div>
			<Menu />
			<Route path="/second" component={SecondPage} />
			<Route path="/" component={FrontPage} />
			{/* 
			<Route path="/about">About Us</Route>
			<Route path="/users/:name">
				{(params) => <div>Hello, {params.name}!</div>}
			</Route> */}
		</div>
	);
}

export default Routes;
