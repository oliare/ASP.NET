import CategoryCreate from "./components/categories/create";
import MainLayout from "./components/containers/default";
import HomePage from "./components/home/index";
import {Route, Routes} from "react-router-dom";

export default function App() {
    return (
        <>
            <Routes>
                <Route path="/" element={<MainLayout />}>
                    <Route index element={<HomePage />} />
                    <Route path="/create" index element={<CategoryCreate />} />
                    {/* Using path="*"" means "match anything", so this route
                acts like a catch-all for URLs that we don't have explicit
                routes for. */}
                    {/*<Route path="*" element={<NoMatch />} />*/}
                </Route>
            </Routes>
        </>
    )
}