import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import { useCart } from '../context/CartContext';
import './Navbar.css';

const Navbar = () => {
  const [scrolled, setScrolled] = useState(false);
  const { cart } = useCart();

  useEffect(() => {
    const handleScroll = () => {
      const isScrolled = window.scrollY > 10;
      if (isScrolled !== scrolled) {
        setScrolled(isScrolled);
      }
    };

    document.addEventListener('scroll', handleScroll);

    return () => {
      document.removeEventListener('scroll', handleScroll);
    };
  }, [scrolled]);

  return (
    <div className={`navbar ${scrolled ? 'scrolled' : ''}`}>
      <div className="navbar-content">
        <div className="navbar-left">
          <Link to="/" className="navbar-logo">
            <img src="/logo.png" alt="Avokadocu Sezgin Logo" />
            <span>Avokadocu Sezgin</span>
          </Link>
        </div>

        <div className="navbar-center">
          <Link to="/" className="navbar-link">Ana Sayfa</Link>
          <Link to="/shop" className="navbar-link">Ürünlerimiz</Link>
          <Link to="/hakkimizda" className="navbar-link">Hakkımızda</Link>
          <Link to="/iletisim" className="navbar-link">İletişim</Link>
        </div>

        <div className="navbar-right">
          <Link to="/cart" className="sepet-button">
            <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round">
              <circle cx="9" cy="21" r="1"></circle>
              <circle cx="20" cy="21" r="1"></circle>
              <path d="M1 1h4l2.68 13.39a2 2 0 0 0 2 1.61h9.72a2 2 0 0 0 2-1.61L23 6H6"></path>
            </svg>
            <span>Sepetim</span>
            {cart && cart.totalItems > 0 && (
              <span className="sepet-badge">{cart.totalItems}</span>
            )}
          </Link>
        </div>
      </div>
    </div>
  );
};

export default Navbar; 