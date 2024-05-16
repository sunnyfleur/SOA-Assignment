import React, { useEffect, useState } from 'react'
import '../css/App.css';
import Customer from '../components/Customer.jsx';
import { IoSearch } from "react-icons/io5";
import axios from "axios";

function Home() {
  const [listDetail, setListDetail] = useState([]);
  let [listResult, setListResult] = useState([]);
  const [inputSearch, setInputSearch] = useState('');
  useEffect(() => {
    const fetchData = async () => {
      const response = await axios.get('https://localhost:7168/api/Customers');
      setListDetail(response.data);
    }
    fetchData();
  }, [])
  const handleSearch = (event) => {
    event.preventDefault();
    listResult = listDetail.filter(item => item.customerID == inputSearch)
    setListResult([...listResult]);
  }
  return (
    <div className='container'>
      <div className="content">
        <div className="content-left">
          <label htmlFor="search"> Search for products</label>
          <div className="search">
            <IoSearch className='search-icon' />
            <input type="text" className="search-input" id='search' />
          </div>
        </div>
        <div className="content-right">
          <form className='search' onSubmit={handleSearch}>
            <input className='add' placeholder='Add a customer' value={inputSearch} onChange={(e) => setInputSearch(e.target.value)}></input>
            {listResult.length > 0 ? (listResult.map(item => {
              return <Customer data={item} key={item.customerID} />
            })) : (listDetail.map(item => {
              return <Customer data={item} key={item.customerID} />
            }))}
          </form>
          <div className="tax info">
            <span className='rate'>Tax GST 10%</span>
            <span className='rate-money'>$11.2</span>
          </div>
          <div className="payment info">
            <span>Pay</span>
            <span className="money">$123.24</span>
          </div>
        </div>
      </div>

    </div>
  )
}

export default Home
