import 'tailwindcss/tailwind.css';
import { Route, Routes } from "react-router-dom";
import HomePage from './pages/HomePage';
import SettingsPage from './pages/SettingsPage';
import LoginPage from './pages/LoginPage';
import NavBar from './components/NavBar';
import { useEffect, useState } from 'react';
import { LocalJwt, LocalUser } from './types/AuthTypes';
import { getClaimsFromJwt } from './utils/jwtHelper';
import { useNavigate } from 'react-router-dom';
import { ToastContainer } from 'react-toastify';
import RegisterPage from './pages/RegisterPage';
import { AppUserContext } from './context/StateContext';
import ProtectedRoute from './components/ProtectedRoute';
import SocialLogin from './pages/SocialLogin';
import UsersPage from "./pages/UsersPage.tsx";
import NotFoundPage from "./pages/NotFoundPage.tsx";
import FakeLogsScreenPage from "./pages/FakeLogsScreenPage.tsx";
import OrderPage from "./pages/OrderPage.tsx";
import ProductsPage from "./pages/ProductsPage.tsx";

function App() {

    const navigate = useNavigate();

    const [appUser, setAppUser] = useState<LocalUser | undefined>(undefined);

    useEffect(() => {

        const jwtJson = localStorage.getItem("upcrawler_user");

        if (!jwtJson) {

            navigate("/login");
            return;

        }

        const localJwt: LocalJwt = JSON.parse(jwtJson);

        const { uid, email, given_name, family_name } = getClaimsFromJwt(localJwt.accessToken);

        const expires: string = localJwt.expires;

        setAppUser({ id: uid, email, firstName: given_name, lastName: family_name, expires, accessToken: localJwt.accessToken });

    }, []);

    return (
        <>
            <AppUserContext.Provider value={{ appUser, setAppUser }}>
                <ToastContainer />
                <NavBar />
                <div className='App'>
                    <Routes>
                        <Route path="/" element={
                            <ProtectedRoute>
                                <HomePage />
                            </ProtectedRoute>
                        }/>
                        <Route path="/orders" element={
                            <ProtectedRoute>
                                <OrderPage />
                            </ProtectedRoute>
                        } />
                        <Route path="/users" element={
                            <ProtectedRoute>
                                <UsersPage />
                            </ProtectedRoute>
                        } />
                        <Route path="/settings" element={
                            <ProtectedRoute>
                                <SettingsPage />
                            </ProtectedRoute>

                        } />
                        <Route path="/fakelogsscreen" element={
                            <ProtectedRoute>
                                <FakeLogsScreenPage />
                            </ProtectedRoute>

                        } />
                        <Route path="/products" element={
                            <ProtectedRoute>
                                <ProductsPage />
                            </ProtectedRoute>

                        } />
                        <Route path="/login" element={<LoginPage />} />
                        <Route path="/social-login" element={<SocialLogin />} />
                        <Route path="/orders" element={<OrderPage />} />
                        <Route path="/register" element={<RegisterPage />} />
                        <Route path="*" element={<NotFoundPage />} />
                        <Route path="/fakelogsscreen" element={<FakeLogsScreenPage />} />
                        <Route path="/products" element={<ProductsPage />} />
                    </Routes>
                </div>
            </AppUserContext.Provider>
        </>
    )
}

export default App