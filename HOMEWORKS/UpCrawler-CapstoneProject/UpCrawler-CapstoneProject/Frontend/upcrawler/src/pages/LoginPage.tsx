import "tailwindcss/tailwind.css";
import { AuthLoginCommand, LocalJwt} from "../types/AuthTypes";
import { useContext, useState } from "react";
import { getClaimsFromJwt } from "../utils/jwtHelper";
import { useNavigate } from "react-router-dom";
import { toast } from "react-toastify";
import { AppUserContext } from "../context/StateContext";
import logo from "/upstorage_logo_without_text_535_411.png";
import api from "../utils/axiosInstance.ts";

const BASE_URL = import.meta.env.VITE_API_URL;

// export type LoginPageProps = {
//
// }

function LoginPage() {

    const { setAppUser } = useContext(AppUserContext);

    const navigate = useNavigate();

    const [authLoginCommand, setAuthLoginCommand] = useState<AuthLoginCommand>({ email: "", password: "" });

    const handleSubmit = async (event: React.FormEvent) => {

        event.preventDefault();

        try {

            const response = await api.post("/Authentication/Login", authLoginCommand);

            if (response.status === 200) {

                const accessToken = response.data.accessToken;

                const { uid, email, given_name, family_name } = getClaimsFromJwt(accessToken);

                const expires:string = response.data.expires;

                setAppUser({ id: uid, email, firstName: given_name, lastName: family_name, expires, accessToken });

                const localJwt: LocalJwt = {
                    accessToken,
                    expires
                }

                localStorage.setItem("upcrawler_user", JSON.stringify(localJwt));

                navigate("/");

                toast.success("Welcome");

            }
        } catch (error) {
            toast.error("Something went wrong!");
        }
    };

    const handleInputChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setAuthLoginCommand({
            ...authLoginCommand,
            [event.target.name]: event.target.value
        });
    };

    const onGoogleLoginClick = (e: React.FormEvent) => {
        //Handle Google Login
        e.preventDefault();

        window.location.href = `${BASE_URL}/Authentication/GoogleSignInStart`;

    };

    const onRegisterClick = (e:React.FormEvent) => {
        //Handle Register
        e.preventDefault();
        navigate("/register")
    };

    return (
        <div className="flex items-center justify-center h-screen">
            <div className="max-w-md w-full">
                <img width="200" src={logo} alt="Logo" className="mx-auto mt-8" />
                <h2 className="text-teal-500 text-2xl text-center mt-4">
                    Log-in to your account
                </h2>
                <form className="mt-8" onSubmit={handleSubmit}>
                    <div className="bg-white rounded-lg shadow-lg p-8">
                        <div className="mb-4">
                            <label className="block text-gray-700">Email</label>
                            <input
                                type="email"
                                className="w-full border border-gray-300 rounded-lg px-4 py-2"
                                placeholder="Email"
                                name="email"
                                value={authLoginCommand.email}
                                onChange={handleInputChange}
                            />
                        </div>
                        <div className="mb-4">
                            <label className="block text-gray-700">Password</label>
                            <input
                                type="password"
                                className="w-full border border-gray-300 rounded-lg px-4 py-2"
                                placeholder="Password"
                                name="password"
                                value={authLoginCommand.password}
                                onChange={handleInputChange}
                            />
                        </div>
                        <button
                            type="submit"
                            className="w-full bg-teal-500 text-white rounded-lg py-2"
                        >
                            Login
                        </button>
                        <button
                            type="button"
                            className="w-full bg-blue-500 text-white rounded-lg py-2 mt-4 flex items-center justify-center"
                            onClick={onRegisterClick}
                        >
                            Register
                        </button>
                        <button
                            type="button"
                            className="w-full bg-red-500 text-white rounded-lg py-2 mt-4 flex items-center justify-center"
                            onClick={onGoogleLoginClick}
                        >
                            <svg
                                xmlns="http://www.w3.org/2000/svg"
                                viewBox="0 0 20 20"
                                fill="currentColor"
                                className="h-4 w-4 mr-1"
                            >
                                <path
                                    fillRule="evenodd"
                                    d="M10 2a8 8 0 100 16 8 8 0 000-16zm1.707 11.707A4.48 4.48 0 0015 10a4.5 4.5 0 10-4.5 4.5c1.318 0 2.5-.57 3.536-1.464l-1.414-1.414A2.5 2.5 0 119.5 12c0 .828.4 1.559 1.014 2.015l1.414-1.414z"
                                    clipRule="evenodd"
                                />
                            </svg>
                            Sign in with Google
                        </button>
                    </div>
                </form>
            </div>
        </div>
    );
}

export default LoginPage;