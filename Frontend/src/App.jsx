import './App.css';
import Customer from './Customer.jsx';
import { BsPersonPlusFill } from "react-icons/bs";
import { IoSearch } from "react-icons/io5";

function App() {

  return (
    <div className='container'>
      <div className="content">
        <div className="content-left">
          <label htmlFor="search"> Search for products</label>
          <div className="search">
            <IoSearch className='search-icon'/>
            <input type="text" className="search-input" id='search' />
          </div>
        </div>
      </div>
      <div className="content">
        <div className="content-right">
          <button className='add'><BsPersonPlusFill className='add-icon' /> Add a customer</button>
          <Customer />
          <Customer />
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

export default App
