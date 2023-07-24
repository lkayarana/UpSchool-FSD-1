import { Link } from "react-router-dom";

function HomePage() {
    return (
        <div className="flex items-center justify-center h-screen">
            <div className="max-w-md p-12 bg-white border border-gray-200 rounded-lg shadow dark:bg-gray-800 dark:border-gray-500 flex flex-col justify-center items-center">
                <img
                    width="200"
                    src="public/upstorage_logo_without_text_535_411.png"
                    alt="Logo"
                    className="mx-auto mt-4 mb-9"
                />
                <h5 className="flex justify-center mb-12 text-2xl font-bold tracking-tight text-gray-900 dark:text-white">
                    Welcome to UpCrawler!
                </h5>
                <p className="mb-12 text-center font-normal text-gray-700 dark:text-gray-400">
                    Click the button to start creating an order!
                </p>
                <Link
                    to="/orders"
                    className="inline-flex items-center px-10 py-3 text-sm font-medium text-center text-white bg-blue-700 rounded-lg hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800"
                    style={{ maxWidth: "250px", wordWrap: "break-word" }}
                >
                    Start Crawling!
                    <svg
                        className="w-3.5 h-3.5 ml-2"
                        aria-hidden="true"
                        xmlns="http://www.w3.org/2000/svg"
                        fill="none"
                        viewBox="0 0 14 10"
                    >
                        <path
                            stroke="currentColor"
                            strokeLinecap="round"
                            strokeLinejoin="round"
                            strokeWidth="2"
                            d="M1 5h12m0 0L9 1m4 4L9 9"
                        />
                    </svg>
                </Link>
            </div>
        </div>
    );
}

export default HomePage;