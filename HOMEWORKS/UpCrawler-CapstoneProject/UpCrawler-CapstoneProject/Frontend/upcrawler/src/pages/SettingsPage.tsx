import axios from "axios";
import React, { useState } from "react";
import { toast } from "react-toastify";

const BASE_URL = import.meta.env.VITE_API_URL;

const SettingsPage = () => {
    const [selectedOption, setSelectedOption] = useState<string>("none");

    const handleOptionChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setSelectedOption(event.target.value);
    };

    const handleSaveSettings = () => {
        //Send the selected option to the backend API and save it in the database
        axios
            .post(`${BASE_URL}/Notifications/Notifications`, { option: selectedOption })
            .then((response) => {
                // Handle the response or show a success message
                toast.success("Settings saved successfully!");
            })
            .catch((error) => {
                // Handle errors
                toast.error("Error saving settings:", error);
            });
    };

    return (
        <div className="flex justify-center items-center h-screen">
            <div className="card bg-gray-800 rounded-lg shadow dark:bg-gray-800 dark:border-gray-800 w-96 p-6">
                <h1 className="mb-12 text-2xl font-bold text-white flex justify-center">Notification Settings</h1>
                <h3 className="mb-5 text-2x font-bold text-white">How you would like to get notifications?</h3>
                <label className="block mt-4 text-white">
                    <input
                        type="radio"
                        value="email"
                        checked={selectedOption === "email"}
                        onChange={handleOptionChange}
                    />
                    Email
                </label>
                <label className="block mt-2 text-white">
                    <input
                        type="radio"
                        value="inApp"
                        checked={selectedOption === "inApp"}
                        onChange={handleOptionChange}
                    />
                    In-App Notification
                </label>
                <label className="block mt-2 text-white">
                    <input
                        type="radio"
                        value="none"
                        checked={selectedOption === "none"}
                        onChange={handleOptionChange}
                    />
                    None
                </label>
                <button
                    className="bg-blue-500 hover:bg-blue-600 text-white font-bold py-2 px-4 mt-4 rounded"
                    onClick={handleSaveSettings}
                >
                    Save
                </button>
            </div>
        </div>
    );
};

export default SettingsPage;