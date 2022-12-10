
export class SingleOrder
{
    id:string;
    address:string;
    createdDate : Date;
    orderNo : string;
    orderStatus:any;
    totalPrice : string;
    user : User;
    orderItems : OrderItem[];
}

export class User{
    email:string;
    nameSurname : string;
}

export class OrderItem{
    product : Product;
    quantity : number;
    discount : string;
    price : string;
}

export class Product{
    name : string;
}
