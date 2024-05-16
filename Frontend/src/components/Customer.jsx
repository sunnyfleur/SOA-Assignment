import React from 'react'
import { RiDeleteBin6Fill } from "react-icons/ri";

export default function Customer({ data }) {
  return (
    <div className='customer info'>
      <span className='name'>`{data.firstName} {data.lastName}`</span>
      <div className="customer-pay">
        <span className='price'>14.84</span>
        <button className='delete'><RiDeleteBin6Fill className='del-icon' /></button>
      </div>
    </div>
  )
}
