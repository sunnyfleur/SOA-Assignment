import React, { useState } from 'react';
import axios from 'axios';
import "../css/Login.css";
import { LuUser2 } from "react-icons/lu";
import { Link, Navigate, useNavigate } from 'react-router-dom';

function Login() {
  const nav = useNavigate();
  const [formData, setFormData] = useState({
    username: '',
    password: ''
  });

  const [error, setError] = useState('');
  const [message, setMessage] = useState('');

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData({
      ...formData,
      [name]: value
    });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      const response = await axios.post('https://localhost:20560/api/Account/login', formData);
      if (response.status === 200) {
        setMessage('Login successful!');
        nav("/");
        setError('');
        // Bạn có thể thêm logic để chuyển hướng người dùng tới trang khác khi đăng nhập thành công
      }
    } catch (error) {
      setError('Login failed. Please try again.');
      setMessage('');
    }
  };

  return (
    <div className="login-container">
      <div className="login-content">
        <div className="logo">
          <LuUser2 className='user-icon' />
        </div>
        <form className='login' onSubmit={handleSubmit}>
          <input
            type="text"
            className="info"
            placeholder='Username'
            name="username"
            value={formData.username}
            onChange={handleChange}
          />
          <input
            type="password"
            className="info"
            placeholder='Password'
            name="password"
            value={formData.password}
            onChange={handleChange}
          />
          <div className="submit">
            <button type="submit" className='btn-submit'>LOGIN</button>
          </div>
          {error && <span className="error">{error}</span>}
          {message && <span className="success">{message}</span>}
        </form>
        <span>Don't have an account? <Link to={"/register"}>Register</Link></span>
      </div>
    </div>
  );
}

export default Login;
