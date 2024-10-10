import CategoryCreatePage from "./components/categories/create";
import CategoryEditPage from "./components/categories/edit";
import MainLayout from "./components/containers/default";
import HomePage from "./components/home/index";
import { Route, Routes } from "react-router-dom";
import ProductListPage from "./components/products/list";
import ProductCreatePage from "./components/products/create";
import ProductEditPage from "./components/products/edit";
// import ProductDescriptionPage from "./components/products/details";
import LoginPage from "./components/auth/login";
import RegisterPage from "./components/auth/register";

export default function App() {
    return (
        <>
            <Routes>
                <Route path="/" element={<MainLayout />}>
                    <Route index element={<HomePage />} />
                    <Route path="/create" element={<CategoryCreatePage />} />
                    <Route path="/edit/:id" element={<CategoryEditPage />} />

                    {/* PRODUCTS */}
                    <Route path={"products"}>
                        <Route index element={<ProductListPage />} />
                        <Route path="create" element={<ProductCreatePage />} />
                        <Route path="edit/:id" element={<ProductEditPage />} />
                        {/* <Route path="/products/:id/description" element={<ProductDescriptionPage />} /> */}
                    </Route>

                    <Route path="auth/login" element={<LoginPage />} />
                    <Route path="auth/register" element={<RegisterPage />} />

                </Route>
            </Routes>
        </>
    )
}