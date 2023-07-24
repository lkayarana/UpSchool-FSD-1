import axios from 'axios';
import { useEffect, useState } from "react";
import {OrderEventGetAllQuery, OrderEventsDto} from "../types/OrderTypes.ts";
import api from "../utils/axiosInstance.ts";

const orderStatusMapping = {
    1: 'BotStarted',
    2: 'CrawlingStarted',
    3: 'CrawlingCompleted',
    4: 'CrawlingFailed',
    5: 'OrderCompleted',
};
const OrderEvents = () => {
    const [orderEvents, setOrderEvents] = useState<OrderEventsDto[]>([]);
    const [selectedOrderId, setSelectedOrderId] = useState<string | null>(null);

    const [OrderEventGetAllQuery, setOrderEventGetAllQuery] =
        useState<OrderEventGetAllQuery>({
            isDeleted: false,
        });

    useEffect(() => {
        const fetchOrderEvents = async () => {
            try {
                const backendUrl = 'https://localhost:7243';
                const response = await axios.post<OrderEventsDto[]>(`${backendUrl}/api/OrderEvents/GetAll`,OrderEventGetAllQuery);

                if (response.status === 200) {
                    setOrderEvents(response.data);
                }
            } catch (error) {
                console.error('Error fetching order events:', error);
            }
        };
        void fetchOrderEvents();

    }, [orderEvents]);

    const handleHardDeleteClick = (orderId: string) => {
        const HardDelete = async () => {
            const responseDelete = await api.delete("OrderEvents/Delete", {
                data : {
                    orderId: selectedOrderId,
                },
            });
        };
        void HardDelete();
    };

    const handleFetchOrderEvents = () => {
        fetchOrderEvents();
    };

    const handleOrderEventSelect = (orderId: string) => {
        setSelectedOrderId((prevSelectedOrderId) =>
            prevSelectedOrderId === orderId ? null : orderId
        );
    };

    return (
        <div className="flex flex-col items-center justify-center w-full dark:bg-gray-800">
            <div
                className="p-1 bg-white border border-gray-200 rounded-lg shadow dark:bg-gray-800 dark:border-gray-700 "
                style={{ width: '100vh' }}
            >
                <div className="container mx-auto px-4 py-2">
                    <div className="mx-auto w-full justify-items-center text-center">
                        <button
                            className="bg-green-500 text-white font-bold py-2 px-4 rounded mb-4 mr-3"
                            onClick={handleFetchOrderEvents}
                        >
                            Fetch Order Events
                        </button>
                        <button
                            className="bg-red-500 text-white font-bold py-2 px-4 rounded mb-4 mr-3"
                            onClick={handleHardDeleteClick}
                        >
                            Delete Order Events
                        </button>

                    </div>
                    <div className="overflow-x-auto">
                        <table className="table-auto w-full">
                            <thead>
                            <tr>
                                <th className="px-4 py-2 text-white font-bold py-2 px-4 rounded mb-4">Order ID</th>
                                <th className="px-4 py-2 text-white font-bold py-2 px-4 rounded mb-4">Status</th>
                                <th className="px-4 py-2 text-white font-bold py-2 px-4 rounded mb-4">Is Deleted</th>
                            </tr>
                            </thead>

                            <tbody>
                            {orderEvents.map((orderEvent) => (
                                <tr key={orderEvent.orderId}>
                                    <td className="border px-4 py-2 text-white font-bold py-2 px-4 rounded mb-4">{orderEvent.orderId}</td>
                                    <td className="border px-4 py-2 text-white font-bold py-2 px-4 rounded mb-4">{orderStatusMapping[orderEvent.status]}</td>
                                    <td className="border px-4 py-2 text-white font-bold py-2 px-4 rounded mb-4">
                                        {orderEvent.isDeleted ? 'Yes' : 'No'}
                                    </td>
                                    <td className="border px-4 py-2">
                                        <input
                                            type="checkbox"
                                            checked={orderEvent.orderId === selectedOrderId}
                                            onChange={() => handleOrderEventSelect(orderEvent.orderId)}
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

export default OrderEvents;
