import React from 'react';
import { RiDeleteBin6Fill } from "react-icons/ri";

export default function Customer({ data }) {
  const totalAmount = data.orders.reduce((sum, order) => sum + order.totalAmount, 0);

  return (
    <div className='customer info'>
      <span className='name'>{data.firstName} {data.lastName}</span>
      <div className="customer-pay">
        <span className='price'>{totalAmount}</span>
        <button className='delete'><RiDeleteBin6Fill className='del-icon' /></button>
      </div>
    </div>
  );
}
