import React, { useEffect, useState } from 'react';
import { HubConnectionBuilder } from '@microsoft/signalr';
import './OrderPage.css';
import { useNavigate } from 'react-router-dom';

const OrderPage: React.FC = () => {
    const [connection, setConnection] = useState<signalR.HubConnection | null>(null);
    const [requestedAmount, setRequestedAmount] = useState('All');
    const [isSpecificAmountSelected, setIsSpecificAmountSelected] = useState(false);
    const [productCrawlType, setProductCrawlType] = useState('');
    const navigate = useNavigate();

    useEffect(() => {
        const newConnection = new HubConnectionBuilder()
            .withUrl('https://localhost:7243/Hubs/OrderHub')
            .build();

        setConnection(newConnection);

        newConnection
            .start()
            .then(() => console.log('SignalR connected.'))
            .catch((error) => console.error('SignalR connection error: ', error));

        return () => {
            newConnection.stop();
        };
    }, []);

    const startCrawler = async () => {
        try {
            await connection?.invoke('SendUserData', requestedAmount, productCrawlType);
        } catch (error) {
            console.error('Error invoking SendUserData:', error);
        }
        navigate('/fakelogsscreen');
    };

    const handleRequestedAmountChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setRequestedAmount(event.target.value);
    };

    const handleSpecificAmountSelected = () => {
        setIsSpecificAmountSelected(true);
        setRequestedAmount('');
    };

    const handleAllSelected = () => {
        setIsSpecificAmountSelected(false);
        setRequestedAmount('All');
    };

    const handleProductCrawlTypeChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
        setProductCrawlType(event.target.value);
    };

    return (
        <div className="flex items-center justify-center h-screen">
            <div className="flex items-center justify-center">
                <div className="order-card bg-gray-800 text-white p-12 shadow-lg rounded-lg w-96 flex flex-col items-center">
                    <h2 className="text-2xl font-bold mb-12">Product Order</h2>
                    <div className="mb-9">
                        <label className="block font-bold mb-5 flex items-center justify-center">Number of Products:</label>
                        <div className="flex">
                            <button
                                onClick={handleAllSelected}
                                className={`mr-2 py-2 px-4 rounded focus:outline-none focus:ring ${
                                    !isSpecificAmountSelected ? 'bg-blue-500 text-white' : 'bg-gray-700 text-white'
                                }`}
                            >
                                All
                            </button>
                            <button
                                onClick={handleSpecificAmountSelected}
                                className={`py-2 px-4 rounded focus:outline-none focus:ring ${
                                    isSpecificAmountSelected ? 'bg-blue-500 text-white' : 'bg-gray-700 text-white'
                                }`}
                            >
                                Specific Amount
                            </button>
                        </div>
                    </div>
                    {isSpecificAmountSelected && (
                        <div className="mb-4">
                            <input
                                type="number"
                                value={requestedAmount}
                                onChange={handleRequestedAmountChange}
                                className="w-full border border-gray-700 rounded-md py-2 px-3 focus:outline-none focus:ring focus:border-blue-300 bg-gray-900 text-white"
                            />
                        </div>
                    )}
                    <div className="mb-4">
                        <label htmlFor="productCrawlType" className="block font-bold mb-2 flex items-center justify-center">
                            Product Crawling Type:
                        </label>
                        <select
                            id="productCrawlType"
                            value={productCrawlType}
                            onChange={handleProductCrawlTypeChange}
                            className="w-full border border-gray-700 rounded-md py-2 px-3 focus:outline-none focus:ring focus:border-blue-300 bg-gray-900 text-white flex items-center justify-center"
                        >
                            <option value="">Select</option>
                            <option value="A">All</option>
                            <option value="B">Is Discount</option>
                            <option value="C">Non Discount</option>
                        </select>
                    </div>
                    <button
                        onClick={startCrawler}
                        className="bg-blue-500 hover:bg-blue-600 text-white py-2 px-4 rounded focus:outline-none focus:ring"
                    >
                        Start Crawler
                    </button>
                </div>
            </div>
        </div>
    );
};

export default OrderPage;
