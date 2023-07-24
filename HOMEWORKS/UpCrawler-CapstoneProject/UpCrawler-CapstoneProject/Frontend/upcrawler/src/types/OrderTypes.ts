export enum ProductCrawlType {
    All = 1,
    OnDiscount = 2,
    NonDiscount = 3,
}

export enum ProductRequestedAmount {
    All = 1,
    SpecificAmount = 2,
}

export type ProductDto={
    id: string;
    orderId: string;
    name: string;
    picture: string;
    isOnSale: boolean;
    price: number;
    salePrice?: number;
    isDeleted: boolean;
};

export type ProductGetAllQuery={
    isDeleted:boolean;
};

export type OrderDto={
id: string;
requestedAmount:number;
totalFoundAmount:number;
productCrawlType:string;
isDeleted:boolean;
};

export type OrderGetAllQuery={
    isDeleted:boolean;
};


export interface OrderEventsDto {
    orderId: string;
    status: string;
    isDeleted: boolean;
}

export type OrderEventGetAllQuery={
    isDeleted:boolean;
};
