import React, { useContext, useState } from 'react';
import { AuthRegisterCommand, LocalJwt } from '../types/AuthTypes';
import { toast } from 'react-toastify';
import { getClaimsFromJwt } from '../utils/jwtHelper';
import { useNavigate } from 'react-router-dom';
import { RegisterUserContext } from '../context/StateContext';
import api from "../utils/axiosInstance.ts";



export default function RegisterPage() {

    const { setRegisterUser } = useContext(RegisterUserContext);

    const navigate = useNavigate();

    const [registerCommand, setRegisterCommand] = useState<AuthRegisterCommand>({ firstName: '', lastName: '', email: '', password: ''});

    const handleSubmit = async (event: React.FormEvent) => {

        event.preventDefault();

        try {

            const response = await api.post("/Authentication/Register", registerCommand);

            if (response.status === 200) {

                const accessToken = response.data.accessToken;

                const { uid, email, given_name, family_name } = getClaimsFromJwt(accessToken);

                const expires:string = response.data.expires;

                setRegisterUser({ id: uid, email, firstName: given_name, lastName: family_name, expires, accessToken });

                const localJwt: LocalJwt = {
                    accessToken,
                    expires
                }

                localStorage.setItem("upcrawler_user", JSON.stringify(localJwt));

                navigate("/login");

                toast.success("Successful!")

            }
        } catch (error) {

            toast.success("Successful!")

            navigate("/login");
        }
    };

    const handleInputChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setRegisterCommand({
            ...registerCommand,
            [event.target.name]: event.target.value
        });
    };

    return (
        <div className="flex justify-center items-center h-screen">
            <form
                onSubmit={handleSubmit}
                className="bg-white shadow-md rounded px-8 pt-14 pb-8 mb-0 dark:bg-gray-800 max-w-md w-full" // Updated class name here
            >
                <h2 className="text-white text-2xl font-bold mb-12 flex justify-center items-center">Register here!</h2>
                <div className="mb-4">
                    <label htmlFor="firstName" className="block text-white text-sm font-bold mb-2">
                        First Name
                    </label>
                    <input
                        type="text"
                        id="firstName"
                        name="firstName"
                        value={registerCommand.firstName}
                        onChange={handleInputChange}
                        className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
                        placeholder="Enter your first name"
                    />
                </div>
                <div className="mb-4">
                    <label htmlFor="lastName" className="block text-white text-sm font-bold mb-2">
                        Last Name
                    </label>
                    <input
                        type="text"
                        id="lastName"
                        name="lastName"
                        value={registerCommand.lastName}
                        onChange={handleInputChange}
                        className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
                        placeholder="Enter your last name"
                    />
                </div>
                <div className="mb-4">
                    <label htmlFor="email" className="block text-white text-sm font-bold mb-2">
                        Email
                    </label>
                    <input
                        type="email"
                        id="email"
                        name="email"
                        value={registerCommand.email}
                        onChange={handleInputChange}
                        className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
                        placeholder="Enter your email"
                    />
                </div>
                <div className="mb-4">
                    <label htmlFor="password" className="block text-white text-sm font-bold mb-2">
                        Password
                    </label>
                    <input
                        type="password"
                        id="password"
                        name="password"
                        value={registerCommand.password}
                        onChange={handleInputChange}
                        className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
                        placeholder="Enter your password"
                    />
                </div>
                <div className="flex items-center justify-center"> {/* Center-align the button */}
                    <button
                        type="submit"
                        className="mt-9 bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-8 rounded focus:outline-none focus:shadow-outline"
                    >
                        Submit
                    </button>
                </div>
            </form>
        </div>
    );
}
