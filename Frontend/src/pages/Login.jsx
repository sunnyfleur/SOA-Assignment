import React from 'react';
import "../css/Login.css";
import { LuUser2 } from "react-icons/lu";
import { Link } from 'react-router-dom';

function Login() {
  return (
    <div className="login-container">
      <div className="login-content">
        <div className="logo">
          <LuUser2 className='user-icon' />
        </div>
        <form className='login'>
          <input type="text" className="info" placeholder='Username' />
          <input type="text" className="info" placeholder='Password' />
          <div className="submit">
            <button type="submit" className='btn-submit'>LOGIN</button>
          </div>
        </form>
        <span>Don't have an account? <Link to={"/register"}>Register</Link></span>
      </div>
    </div>
  )
}

export default Login
