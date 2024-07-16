import axios from 'axios';

const API_URL = 'http://localhost:5251/api/immobilier/authentification';

export const login = async (email, mdp, contact) => {
    const response = await axios.post(`${API_URL}/loginAdmin`, {
      email,
      mdp,
      contact
    });
    return response.data;
};

export const loginProprietaire= async (email, mdp, contact) => {
  const response = await axios.post(`${API_URL}/loginProprietaire`, {
    email,
    mdp,
    contact
  });
  return response.data;
};

export const loginClient= async (email, mdp, contact) => {
  const response = await axios.post(`${API_URL}/loginClient`, {
    email,
    mdp,
    contact
  });
  return response.data;
};
