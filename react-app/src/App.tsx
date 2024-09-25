import CategoryCreatePage from "./components/categories/create";
import CategoryEditPage from "./components/categories/edit";
import MainLayout from "./components/containers/default";
import HomePage from "./components/home/index";
import {Route, Routes} from "react-router-dom";

export default function App() {
    return (
        <>
            <Routes>
                <Route path="/" element={<MainLayout />}>
                    <Route index element={<HomePage />} />
                    <Route path="/create" index element={<CategoryCreatePage />} />
                    <Route path="/edit/:id" element={<CategoryEditPage />} />
                </Route>
            </Routes>
        </>
    )
}