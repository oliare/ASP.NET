import CategoryCreatePage from "./components/categories/create";
import CategoryEditPage from "./components/categories/edit";
import MainLayout from "./components/containers/default";
import HomePage from "./components/home/index";
import {Route, Routes} from "react-router-dom";
import ProductListPage from "./components/products/list";
import ProductCreatePage from "./components/products/create";

export default function App() {
    return (
        <>
            <Routes>
                <Route path="/" element={<MainLayout />}>
                    <Route index element={<HomePage />} />
                    <Route path="/create" element={<CategoryCreatePage />} />
                    <Route path="/edit/:id" element={<CategoryEditPage />} />
                    
                    {/* PRODUCTS */}
                    <Route path="/products" element={<ProductListPage />} />
                    <Route path="/products/create" element={<ProductCreatePage />} />
                    </Route>
            </Routes>
        </>
    )
}