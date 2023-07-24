import Order from "../components/Order.tsx";
import OrderEvents from "../components/OrderEvents.tsx";
import {useState} from "react";
import Products from "../components/Products.tsx";
import ExcelJS from "exceljs";

const ProductsPage = () => {
    const [isOrderEventsOpen, setIsOrderEventsOpen] = useState(false);
    const [isProductsOpen, setIsProductsOpen] = useState(false);

    // Function to handle Order Events button click
    const handleOrderEventsClick = () => {
        setIsOrderEventsOpen((prev) => !prev); // Toggle the state
    };

    // Function to handle Products button click
    const handleProductsClick = () => {
        setIsProductsOpen((prev) => !prev); // Toggle the state
    };

    return (
        <div className="flex flex-col items-center justify-center"
             style={{marginLeft: "260px"}}>
            <div
                className="p-20 text-white border border-gray-200 rounded-lg shadow dark:bg-gray-800 dark:border-gray-800 items-center justify-center "
                style={{width: "140vh"}}
            >
                <div className="container mx-auto px-4 py-12">
                    <h1 className="text-2xl flex items-center justify-center font-bold mb-9">Order Informations</h1>
                    <div className="mb-12">
                        <Order/>
                    </div>

                    {/* Accordion */}
                    <div className="mb-12">
                        <h2 className="flex justify-center text-lg font-bold mb-5">Orders' Details</h2>
                        <div className="flex justify-center space-x-14">
                            {/* Products Button */}
                            <button
                                className="py-2 px-4 rounded text-white font-bold py-2 px-4 rounded mb-4 mr-3 bg-green-500 font-bold py-2 px-4 rounded mb-4 mr-3"
                                onClick={handleProductsClick}
                            >
                                Products
                            </button>
                            {/* Order Events Button */}
                            <button
                                className="py-2 px-4 rounded font-bold py-2 px-4 rounded mb-4 mr-3 bg-green-500 text-white font-bold py-2 px-4 rounded mb-4 mr-3"
                                onClick={handleOrderEventsClick}
                            >
                                Order Events
                            </button>
                        </div>
                    </div>

                    {/* Render Content based on the state */}
                    {isOrderEventsOpen && (
                        <div className="mb-12">
                            <h2 className="text-lg font-bold mb-4">Order Events</h2>
                            <OrderEvents/>
                        </div>
                    )}

                    {isProductsOpen && (
                        <div className="mb-4">
                            <h2 className="text-lg font-bold mb-2">Products</h2>
                            <Products/>
                        </div>
                    )}
                </div>
            </div>
        </div>
    );
};

export default ProductsPage;