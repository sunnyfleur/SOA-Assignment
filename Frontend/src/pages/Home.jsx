import React, { useEffect, useState } from 'react';
import '../css/App.css';
import { IoSearch } from "react-icons/io5";
import axios from "axios";

function Home() {
  const nav = useNavigate();
  const [products, setProducts] = useState([]);
  const [searchTerm, setSearchTerm] = useState('');
  const [selectedProducts, setSelectedProducts] = useState([]);
  const [totalAmount, setTotalAmount] = useState(0);
  const [customerID, setCustomerID] = useState('');
  const [customer, setCustomer] = useState(null);
  const [productResult, setProductResult] = useState(null);

  useEffect(() => {
    const fetchProducts = async () => {
      try {
        const response = await axios.get('https://localhost:20560/api/Products');
        const productsArray = response.data.$values || [];
        setProducts(productsArray);
      } catch (error) {
        console.error('Error fetching products:', error);
      }
    };

    fetchProducts();
  }, []);

  const handleProductSearchByID = async (event) => {
    event.preventDefault();
    try {
      const response = await axios.get(`https://localhost:20560/api/Products/${searchTerm}`);
      setProductResult(response.data || null);
    } catch (error) {
      console.error('Error fetching product:', error);
      setProductResult(null);
    }
  };

  const handleProductSelect = (product) => {
    const existingProduct = selectedProducts.find(p => p.productID === product.productID);
    if (existingProduct) {
      const updatedProducts = selectedProducts.map(p => 
        p.productID === product.productID ? { ...p, quantity: p.quantity + 1 } : p
      );
      setSelectedProducts(updatedProducts);
      updateTotalAmount(updatedProducts);
    } else {
      const updatedProducts = [...selectedProducts, { ...product, quantity: 1 }];
      setSelectedProducts(updatedProducts);
      updateTotalAmount(updatedProducts);
    }
  };

  const handleQuantityChange = (productID, quantity) => {
    const updatedProducts = selectedProducts.map(product => 
      product.productID === productID ? { ...product, quantity: quantity } : product
    );
    setSelectedProducts(updatedProducts);
    updateTotalAmount(updatedProducts);
  };

  const handleProductRemove = (productID) => {
    const updatedProducts = selectedProducts.filter(product => product.productID !== productID);
    setSelectedProducts(updatedProducts);
    updateTotalAmount(updatedProducts);
  };

  const updateTotalAmount = (productsList) => {
    const total = productsList.reduce((acc, product) => acc + (product.price * product.quantity), 0);
    setTotalAmount(total);
  };

  const handleCustomerSearch = async (event) => {
    event.preventDefault();
    try {
      const response = await axios.get(`https://localhost:20560/api/Customers/${customerID}`);
      setCustomer(response.data || null);
    } catch (error) {
      console.error('Error fetching customer:', error);
      setCustomer(null);
    }
  };

  return (
    <div className='container'>
      <div className="content-left">
        <label htmlFor="search">Search product by ID</label>
        <div className="search">
          <IoSearch className='search-icon' />
          <input
            type="text"
            className="search-input"
            id='search'
            value={searchTerm}
            onChange={(e) => setSearchTerm(e.target.value)}
            placeholder='Enter product ID'
          />
          <button onClick={handleProductSearchByID}>Search</button>
        </div>
      </div>

      <div className="content-right">
        <form className='customer-search' onSubmit={handleCustomerSearch}>
          <label>Add a customer</label>
          <input
            type="text"
            value={customerID}
            onChange={(e) => setCustomerID(e.target.value)}
            placeholder="Customer ID"
          />
          <button type="submit">Search Customer</button>
        </form>

        {customer && (
          <div className="customer-details">
            <p>{customer.firstName} {customer.lastName}</p>
            <p>{customer.email}</p>
            <p>{customer.phone}</p>
          </div>
        )}

        {productResult && (
          <div className="product-details">
            <p>Product Name: {productResult.productName}</p>
            <p>Price: ${productResult.price}</p>
            <p>Stock: {productResult.stock}</p>
            <p>Description: {productResult.description}</p>
            <button onClick={() => handleProductSelect(productResult)}>Add to Cart</button>
          </div>
        )}

        <h3>Selected Products</h3>
        <div className="selected-products">
          {selectedProducts.map(product => (
            <div key={product.productID} className="product-item">
              <p>{product.productName}</p>
              <input
                type="number"
                value={product.quantity}
                onChange={(e) => handleQuantityChange(product.productID, parseInt(e.target.value))}
                min="1"
                max={product.stock}
              />
              <p>${(product.price * product.quantity).toFixed(2)}</p>
              <button onClick={() => handleProductRemove(product.productID)}>Remove</button>
            </div>
          ))}
        </div>
        <div className="summary">
          <p>Tax GST 10%: ${(totalAmount * 0.1).toFixed(2)}</p>
          <p>Total: ${(totalAmount * 1.1).toFixed(2)}</p>
        </div>
        <button className="pay-button">Pay</button>
      </div>
    </div>
  );
}

export default Home;
