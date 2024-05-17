import React, { useState } from 'react';
import axios from 'axios';
import "../css/Register.css";
import { useNavigate } from 'react-router-dom';

function Register() {
  const nav = useNavigate();
  const [formData, setFormData] = useState({
    firstName: '',
    lastName: '',
    email: '',
    phone: '',
    address: '',
    userName: '',
    password: ''
  });

  const [errors, setErrors] = useState({});
  const [message, setMessage] = useState('');

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData({
      ...formData,
      [name]: value
    });
  };

  const validate = () => {
    const newErrors = {};
    
    // Check if all fields are filled
    if (!formData.firstName) newErrors.firstName = 'First Name is required';
    if (!formData.lastName) newErrors.lastName = 'Last Name is required';
    if (!formData.email) newErrors.email = 'Email is required';
    if (!formData.userName) newErrors.userName = 'UserName is required';
    if (!formData.phone) newErrors.phone = 'Phone number is required';
    if (!formData.address) newErrors.address = 'Address is required';
    if (!formData.password) newErrors.password = 'Password is required';
    
    // Check if phone number is valid
    const phonePattern = /^[0-9]{10}$/;
    if (formData.phone && !phonePattern.test(formData.phone)) {
      newErrors.phone = 'Phone number is invalid';
    }

    setErrors(newErrors);
    
    // If there are no errors, return true
    return Object.keys(newErrors).length === 0;
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (validate()) {
      try {
        const response = await axios.post('https://localhost:20560/api/Account/register', formData);
        if (response.status === 200) {
          setMessage('Registration successful!');
          nav("/login");
        }
      } catch (error) {
        setMessage('Registration failed. Please try again.');
      }
    }
  };

  return (
    <div className="register-container">
      <div className="register-content">
        <div className="banner">
          <h1 className='banner-content'>Register</h1>
        </div>
        <form className='register' onSubmit={handleSubmit}>
          <input 
            type="text" 
            className="info" 
            placeholder='First Name' 
            name="firstName" 
            value={formData.firstName} 
            onChange={handleChange} 
          />
          {errors.firstName && <span className="error">{errors.firstName}</span>}

          <input 
            type="text" 
            className="info" 
            placeholder='Last Name' 
            name="lastName" 
            value={formData.lastName} 
            onChange={handleChange} 
          />
          {errors.lastName && <span className="error">{errors.lastName}</span>}

          <input 
            type="text" 
            className="info" 
            placeholder='Email' 
            name="email" 
            value={formData.email} 
            onChange={handleChange} 
          />
          {errors.email && <span className="error">{errors.email}</span>}
          <input 
            type="text" 
            className="info" 
            placeholder='UserName' 
            name="userName" 
            value={formData.userName} 
            onChange={handleChange} 
          />
          {errors.userName && <span className="error">{errors.userName}</span>}

          <input 
            type="text" 
            className="info" 
            placeholder='Phone' 
            name="phone" 
            value={formData.phone} 
            onChange={handleChange} 
          />
          {errors.phone && <span className="error">{errors.phone}</span>}

          <input 
            type="text" 
            className="info" 
            placeholder='Address' 
            name="address" 
            value={formData.address} 
            onChange={handleChange} 
          />
          {errors.address && <span className="error">{errors.address}</span>}

          <input 
            type="password" 
            className="info" 
            placeholder='Password' 
            name="password" 
            value={formData.password} 
            onChange={handleChange} 
          />
          {errors.password && <span className="error">{errors.password}</span>}

          <div className="submit">
            <button type="submit" className='btn-submit'>Register</button>
          </div>
          {message && <span className="message">{message}</span>}
        </form>
      </div>
    </div>
  );
}

export default Register;
