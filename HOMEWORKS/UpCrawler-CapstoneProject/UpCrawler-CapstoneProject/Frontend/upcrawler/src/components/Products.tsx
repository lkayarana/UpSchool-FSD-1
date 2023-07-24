import axios from 'axios';
import { useEffect, useState } from 'react';
import {ProductDto, ProductGetAllQuery} from "../types/OrderTypes.ts";
import api from "../utils/axiosInstance.ts";
import ExcelJS from "exceljs";

interface Product {
    id: string;
    orderId: string;
    name: string;
    picture: string;
    isOnSale: boolean;
    price: number;
    salePrice?: number;
    isDeleted: boolean;
}

const Products = () => {
    const [products, setProducts] = useState<Product[]>([]);
    const [selectedOrderId, setSelectedOrderId] = useState<string | null>(null);

    const [ProductGetAllQuery] =
        useState<ProductGetAllQuery>({
            isDeleted: false,
        });

    useEffect(() => {
        const fetchProducts = async () => {
            try {
                const backendUrl = 'https://localhost:7243';
                const response = await axios.post<ProductDto[]>(`${backendUrl}/api/Products/GetAll`,ProductGetAllQuery);

                if (response.status === 200) {
                    setProducts(response.data);
                }
            } catch (error) {
                console.error('Error fetching products:', error);
            }
        };
        void fetchProducts();

    }, [products]);

    const handleHardDeleteClick = (orderId: string) => {
        const HardDelete = async () => {
            const responseDelete = await api.delete("Products/Delete", {
                data : {
                    orderId:selectedOrderId,
                },
            });
        };
        void HardDelete();
    };

    const handleFetchProducts = () => {
        fetchProducts();
    };

    const handleProductSelect = (orderId: string) => {
        setSelectedOrderId((prevSelectedOrderId) =>
            prevSelectedOrderId === orderId ? null : orderId
        );
    };



    const handleDownloadExcel = () => {
        if (!selectedOrderId) {
            alert("Please select an order to download the Excel file.");
            return;
        }

        // Filter selected products based on selectedOrderId
        const selectedProducts = products.filter(
            (product) => product.orderId === selectedOrderId
        );

        // Create a new Excel workbook and worksheet
        const workbook = new ExcelJS.Workbook();
        const worksheet = workbook.addWorksheet("Selected Products");

        // Define headers for the Excel table
        worksheet.columns = [
            { header: "Product ID", key: "id", width: 40 },
            { header: "Order ID", key: "orderId", width: 40 },
            { header: "Name", key: "name", width: 40 },
            { header: "Is On Sale", key: "isOnSale", width: 15 },
            { header: "Price", key: "price", width: 15 },
            { header: "Sale Price", key: "salePrice", width: 15 },
            { header: "Image Path", key: "picture", width: 40 },
        ];


        // Add data to the worksheet
        selectedProducts.forEach((product) => {
            worksheet.addRow({
                id: product.id,
                orderId: product.orderId,
                name: product.name,
                isOnSale: product.isOnSale ? "Yes" : "No",
                price: product.price,
                salePrice: product.salePrice || "-",
                picture: product.picture,
            });
        });

        // Set the desired style for the entire table
        worksheet.eachRow((row, rowNumber) => {
            row.eachCell((cell) => {
                cell.font = { color: { argb: "00000000" } }; // Black font color
                cell.fill = {
                    type: "pattern",
                    pattern: "solid",
                    fgColor: { argb: "033b8a" }, // Blue background color (You can modify this color as needed)
                };
                cell.alignment = { horizontal: "center", vertical: "center" }; // Center text in the cell
            });
        });

        // Set style for the header row
        worksheet.getRow(1).eachCell((cell) => {
            cell.fill = {
                type: "pattern",
                pattern: "solid",
                fgColor: { argb: "99badd" }, // Light blue color for header background
            };
            cell.font = { color: { argb: "1b1b1b" } }; // White font color for header (You can modify this color as needed)
            cell.alignment = { horizontal: "center", vertical: "center" }; // Center text in the cell
        });

        // Set alternating row colors
        worksheet.eachRow((row, rowNumber) => {
            if (rowNumber > 1) {
                const fillColor = rowNumber % 2 === 0 ? "FFFFFF" : "ccdcec"; // Alternating white and light gray colors
                row.eachCell((cell) => {
                    cell.fill = {
                        type: "pattern",
                        pattern: "solid",
                        fgColor: { argb: fillColor },
                    };
                });
            }
        });

        // Format rows based on Is On Sale? value (excluding header)
        worksheet.eachRow((row, rowNumber) => {
            if (rowNumber !== 1) { // Exclude the header row
                const isOnSaleCell = row.getCell("isOnSale");
                if (isOnSaleCell.value === "Yes") {
                    isOnSaleCell.fill = {
                        type: "pattern",
                        pattern: "solid",
                        fgColor: { argb: "03c03c" }, // Green color
                    };
                } else {
                    isOnSaleCell.fill = {
                        type: "pattern",
                        pattern: "solid",
                        fgColor: { argb: "ff0000" }, // Red color
                    };
                }
            }
        });

        // Set row height to auto
        worksheet.properties.rowHeight = -1;

        // Adjust column widths based on content for "Order ID," "Name," and "Product ID" columns
        ["A", "B", "C"].forEach((columnKey) => {
            const column = worksheet.getColumn(columnKey);
            let maxContentLength = column.header.length;
            selectedProducts.forEach((product) => {
                const cellValue = product[columnKey.toLowerCase()];
                if (cellValue && cellValue.length > maxContentLength) {
                    maxContentLength = cellValue.length;
                }
            });
            column.width = Math.min(Math.max(maxContentLength + 40, 40), 40);
        });

        // Enable autofilter on headers
        worksheet.autoFilter = "A1:G1";


        // Generate and save the Excel file
        workbook.xlsx.writeBuffer().then((buffer) => {
            const blob = new Blob(
                [buffer],
                { type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" }
            );
            const url = URL.createObjectURL(blob);
            const a = document.createElement("a");
            a.href = url;
            a.download = "products.xlsx";
            a.click();
            URL.revokeObjectURL(url);
        });
    };

    const handleSendEmail = async () => {
        try {
            if (!selectedOrderId) {
                alert("Please select an order to send the email.");
                return;
            }

            // Filter selected products based on selectedOrderId
            const selectedProducts = products.filter(
                (product) => product.orderId === selectedOrderId
            );


            const backendUrl = 'https://localhost:7243';
            const response = await axios.post(`${backendUrl}/api/Email/SendEmail`, {
                firstName: "Rana", // Replace with the appropriate first name
                lastName: "Kaya", // Replace with the appropriate last name
                email: "", // Replace with the appropriate email address
                excelFile: selectedProducts, // Send the array of product IDs
            });

            if (response.status === 200) {
                alert('Email sent successfully!');
            }
        } catch (error) {
            console.error('Error sending email:', error);
        }
    };



    return (
        <div className="flex flex-col items-center justify-center bg-white dark:bg-gray-800">
            <div
                className="p-2 bg-white text-white border border-gray-200 rounded-lg shadow dark:bg-gray-800 dark:border-gray-700 "
                style={{ width: '100vh' }}
            >
                <div className="container mx-auto px-4 text-white py-8">
                    <div className="mx-auto justify-items-center text-white text-center">
                        <button
                            className="bg-green-500 text-white font-bold py-2 px-4 rounded mb-4 mr-3"
                            onClick={handleFetchProducts}
                        >
                            Fetch Products
                        </button>
                        <button
                            className="bg-red-500 text-white font-bold py-2 px-4 rounded mb-4 mr-3"
                            onClick={handleHardDeleteClick}
                        >
                            Delete Products
                        </button>
                        <button
                            className="bg-blue-500 text-white font-bold py-2 px-4 rounded mb-4 mr-3"
                            onClick={handleDownloadExcel}
                        >
                            Download Excel
                        </button>
                        <button
                            className="bg-purple-500 text-white font-bold py-2 px-4 rounded mb-4 mr-3"
                            onClick={handleSendEmail}
                        >
                            Send Email
                        </button>
                    </div>
                    <div className="overflow-x-auto">
                        <table className="table-auto w-full">
                            <thead>
                            <tr>
                                <th className="px-4 py-2 text-white font-bold py-2 px-4 rounded mb-4">Picture</th>
                                <th className="px-4 py-2 text-white font-bold py-2 px-4 rounded mb-4">Order ID</th>
                                <th className="px-4 py-2 text-white font-bold py-2 px-4 rounded mb-4">Name</th>
                                <th className="px-4 py-2 text-white font-bold py-2 px-4 rounded mb-4">Is On Sale</th>
                                <th className="px-4 py-2 text-white font-bold py-2 px-4 rounded mb-4">Price</th>
                                <th className="px-4 py-2 text-white font-bold py-2 px-4 rounded mb-4">Sale Price</th>
                                <th className="px-4 py-2 text-white font-bold py-2 px-4 rounded mb-4">Is Deleted</th>
                            </tr>
                            </thead>
                            <tbody>
                            {products.map((product) => (
                                <tr key={product.orderId}>
                                    <td className="border px-4 py-2 text-white font-bold py-2 px-4 rounded mb-4">
                                        <img
                                            src={product.picture}
                                            alt={product.name}
                                            className="w-24 h-24 object-cover"
                                        />
                                    </td>
                                    <td className="border px-4 py-2 text-white font-bold py-2 px-4 rounded mb-4">{product.orderId}</td>
                                    <td className="border px-4 py-2 text-white font-bold py-2 px-4 rounded mb-4">{product.name}</td>
                                    <td className="border px-4 py-2 text-white font-bold py-2 px-4 rounded mb-4">
                                        {product.isOnSale ? 'Yes' : 'No'}
                                    </td>
                                    <td className="border px-4 py-2 text-white font-bold py-2 px-4 rounded mb-4">{product.price}</td>
                                    <td className="border px-4 py-2 text-white font-bold py-2 px-4 rounded mb-4">
                                        {product.salePrice || '-'}
                                    </td>
                                    <td className="border px-4 py-2 text-white font-bold py-2 px-4 rounded mb-4">
                                        {product.isDeleted ? 'Yes' : 'No'}
                                    </td>
                                    <td className="border px-4 py-2 text-white font-bold py-2 px-4 rounded mb-4">
                                        <input
                                            type="checkbox"
                                            checked={product.orderId === selectedOrderId}
                                            onChange={() => handleProductSelect(product.orderId)}
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

export default Products;
