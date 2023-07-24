import axios from 'axios';
import {useEffect, useState} from "react";
import {OrderDto, OrderGetAllQuery} from "../types/OrderTypes.ts";
import api from "../utils/axiosInstance.ts";

interface Order{
    id: string;
    requestedAmount:number;
    totalFoundAmount:number;
    productCrawlType:string;
    isDeleted:boolean;
}

const orderCrawlTypeMapping = {
    0: 'All Products',
    1: 'Is Discount Products',
    2: 'Non Discount Products',
};

const Order = () => {
    const [orders, setOrders] = useState<Order[]>([]);
    const [selectedOrderId, setSelectedOrderId] = useState<string | null>(null);


    const [OrderGetAllQuery, setOrderGetAllQuery] =
        useState<OrderGetAllQuery>({
            isDeleted: false,
        });

    useEffect(() => {
        const fetchProducts = async () => {
            try {
                const backendUrl = 'https://localhost:7243';
                const response = await axios.post<OrderDto[]>(`${backendUrl}/api/Orders/GetAll`,OrderGetAllQuery);

                if (response.status === 200) {
                    setOrders(response.data);
                }
            } catch (error) {
                console.error('Error fetching orders:', error);
            }
        };
        void fetchProducts();

    }, [orders]);


    const handleFetchOrders = () => {
        fetchOrders();
    };

    const handleHardDeleteClick = (id: string) => {
        const HardDelete = async () => {
            const responseDelete = await api.delete("Orders/Delete", {
                data : {
                    id: selectedOrderId,
                },
            });
        };
        void HardDelete();
    };

    const handleOrderSelect = (orderId: string) => {
        setSelectedOrderId((prevSelectedOrderId) =>
            prevSelectedOrderId === orderId ? null : orderId
        );
    };


    // useEffect(() => {
    //     fetchProducts();
    // }, []);

    return (
        <div className="flex flex-col items-center justify-center w-full dark:bg-gray-800">
            <div
                className="p-12 bg-white border border-gray-200 rounded-lg shadow dark:bg-gray-800 dark:border-gray-700 "
                style={{ width: '100vh' }}
            >
                <div className="container mx-auto px-4 py-8">
                    <div className="mx-auto w-full justify-items-center text-center">
                        <button
                            className="bg-green-500 text-white font-bold py-2 px-4 rounded mb-4 mr-3"
                            onClick={handleFetchOrders}
                        >
                            Fetch Orders
                        </button>
                        <button
                            className="bg-red-500 text-white font-bold py-2 px-4 rounded mb-4 mr-3"
                            onClick={handleHardDeleteClick}
                        >
                            Delete Orders
                        </button>
                    </div>
                    <div className="overflow-x-auto">
                        <table className="table-auto w-full">
                            <thead>
                            <tr>
                                <th className="px-4 py-2 text-white font-bold py-2 px-4 rounded mb-4">Order ID</th>
                                <th className="px-4 py-2 text-white font-bold py-2 px-4 rounded mb-4">Requested Amount</th>
                                <th className="px-4 py-2 text-white font-bold py-2 px-4 rounded mb-4">Total Found Amount</th>
                                <th className="px-4 py-2 text-white font-bold py-2 px-4 rounded mb-4">Product Crawl Type</th>
                                <th className="px-4 py-2 text-white font-bold py-2 px-4 rounded mb-4">Is Deleted</th>
                            </tr>
                            </thead>
                            <tbody>
                            {orders.map((order) => (
                                <tr key={order.id}>
                                    <td className="border px-4 py-2 text-white font-bold py-2 px-4 rounded mb-4">{order.id}</td>
                                    <td className="border px-4 py-2 text-white font-bold py-2 px-4 rounded mb-4">{order.requestedAmount}</td>
                                    <td className="border px-4 py-2 text-white font-bold py-2 px-4 rounded mb-4">{order.totalFoundAmount}</td>
                                    <td className="border px-4 py-2 text-white font-bold py-2 px-4 rounded mb-4">{orderCrawlTypeMapping[order.productCrawlType]}</td>
                                    <td className="border px-4 py-2 text-white font-bold py-2 px-4 rounded mb-4">
                                        {order.isDeleted ? 'Yes' : 'No'}
                                    </td>
                                    <td className="border px-4 py-2">
                                        <input
                                            type="checkbox"
                                            checked={order.id === selectedOrderId}
                                            onChange={() => handleOrderSelect(order.id)}
                                        />
                                    </td>
                                </tr>
                            ))}
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default Order;
