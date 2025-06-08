import React, { createContext, useState, useEffect, useContext, useCallback } from 'react';
import axios from 'axios';

// Boş bir default değer oluştur
const defaultCartContext = {
  cart: { items: [], totalPrice: 0, totalItems: 0 },
  loading: false,
  error: null,
  addToCart: () => {},
  updateCartItemQuantity: () => {},
  removeFromCart: () => {},
  clearCart: () => {},
  fetchCart: () => {}
};

const CartContext = createContext(defaultCartContext);

export const useCart = () => useContext(CartContext);

export const CartProvider = ({ children }) => {
  const [cart, setCart] = useState({ items: [], totalPrice: 0, totalItems: 0 });
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);

  // API URL'i doğru port ile ayarla
  const API_BASE_URL = 'http://localhost:5029';

  // Axios instance oluştur
  const api = axios.create({
    baseURL: API_BASE_URL,
    timeout: 10000,
    headers: {
      'Content-Type': 'application/json'
    }
  });

  // Sepeti getir - useCallback ile sarmalayalım
  const fetchCart = useCallback(async () => {
    setLoading(true);
    setError(null);
    try {
      console.log('Sepet verisi çekiliyor:', `${API_BASE_URL}/api/cart`);
      const response = await api.get('/api/cart');
      console.log('Sepet verileri:', response.data);
      setCart(response.data);
      return response.data;
    } catch (err) {
      console.error('Error fetching cart:', err);
      setError('Sepet yüklenirken bir hata oluştu');
      // Hata durumunda boş sepet döndür
      return { items: [], totalPrice: 0, totalItems: 0 };
    } finally {
      setLoading(false);
    }
  }, [api, API_BASE_URL]);

  // Sepete ürün ekle
  const addToCart = async (avocadoId, quantity) => {
    setLoading(true);
    setError(null);
    try {
      console.log(`Sepete ekleniyor: avocadoId=${avocadoId}, quantity=${quantity}`);
      const response = await api.post('/api/cart', { avocadoId, quantity });
      console.log('Sepete eklendi:', response.data);
      setCart(response.data);
      return true;
    } catch (err) {
      console.error('Error adding to cart:', err);
      setError('Ürün sepete eklenirken bir hata oluştu');
      return false;
    } finally {
      setLoading(false);
    }
  };

  // Sepetteki ürün miktarını güncelle
  const updateCartItemQuantity = async (itemId, quantity) => {
    setLoading(true);
    setError(null);
    try {
      console.log(`Sepet ürünü güncelleniyor: itemId=${itemId}, quantity=${quantity}`);
      const response = await api.put(`/api/cart/items/${itemId}`, { avocadoId: 0, quantity });
      console.log('Ürün miktarı güncellendi:', response.data);
      setCart(response.data);
      return true;
    } catch (err) {
      console.error('Error updating cart item:', err);
      setError('Ürün miktarı güncellenirken bir hata oluştu');
      return false;
    } finally {
      setLoading(false);
    }
  };

  // Sepetten ürün kaldır
  const removeFromCart = async (itemId) => {
    setLoading(true);
    setError(null);
    try {
      console.log(`Sepetten ürün kaldırılıyor: itemId=${itemId}`);
      const response = await api.delete(`/api/cart/items/${itemId}`);
      console.log('Ürün sepetten kaldırıldı:', response.data);
      setCart(response.data);
      return true;
    } catch (err) {
      console.error('Error removing from cart:', err);
      setError('Ürün sepetten kaldırılırken bir hata oluştu');
      return false;
    } finally {
      setLoading(false);
    }
  };

  // Sepeti temizle
  const clearCart = async () => {
    setLoading(true);
    setError(null);
    try {
      console.log('Sepet temizleniyor');
      const response = await api.delete('/api/cart');
      console.log('Sepet temizlendi:', response.data);
      setCart(response.data);
      return true;
    } catch (err) {
      console.error('Error clearing cart:', err);
      setError('Sepet temizlenirken bir hata oluştu');
      return false;
    } finally {
      setLoading(false);
    }
  };

  // Sayfa yüklendiğinde sepeti getir
  useEffect(() => {
    console.log('CartContext yükleniyor, sepet verisi çekiliyor...');
    fetchCart();

    // 30 saniyede bir sepeti yenile
    const intervalId = setInterval(() => {
      fetchCart();
    }, 30000);

    return () => clearInterval(intervalId);
  }, [fetchCart]);

  const value = {
    cart,
    loading,
    error,
    addToCart,
    updateCartItemQuantity,
    removeFromCart,
    clearCart,
    fetchCart
  };

  return <CartContext.Provider value={value}>{children}</CartContext.Provider>;
};

export default CartContext; 