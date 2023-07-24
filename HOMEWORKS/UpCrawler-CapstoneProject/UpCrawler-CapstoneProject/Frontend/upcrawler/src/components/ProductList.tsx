import React from "react";

interface Product {
   name:string;
   price:number;
   saleprice:number;
   isOnSale:boolean;
   pictureUrl:string;
}

interface ProductListProps {
    products: Product[];
}

const ProductList: React.FC<ProductListProps> = ({ products }) => {
    return (
        <div>
            {products.map((product, index) => (
                <p key={index}>{product.name}
                    | {product.price} | {product.saleprice}
                    | {product.isOnSale} | {product.pictureUrl} </p>
            ))}
        </div>
    );
};

export default ProductList;