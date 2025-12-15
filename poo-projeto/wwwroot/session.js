// session.js
// utilitários para guardar token e fazer fetch autenticado

const SESSION_STORAGE_TOKEN_KEY = "gf_token";
const SESSION_STORAGE_USER_KEY = "gf_user";

export function setToken(token) {
  localStorage.setItem(SESSION_STORAGE_TOKEN_KEY, token);
}

export function getToken() {
  return localStorage.getItem(SESSION_STORAGE_TOKEN_KEY);
}

export function clearToken() {
  localStorage.removeItem(SESSION_STORAGE_TOKEN_KEY);
  localStorage.removeItem(SESSION_STORAGE_USER_KEY);
}

export function setUser(user) {
  localStorage.setItem(SESSION_STORAGE_USER_KEY, JSON.stringify(user));
}

export function getUser() {
  const v = localStorage.getItem(SESSION_STORAGE_USER_KEY);
  return v ? JSON.parse(v) : null;
}

/**
 * Wrapper fetch que adiciona Authorization: Bearer <token> quando existe token.
 * Retorna a Promise do fetch.
 */
export async function authFetch(input, init = {}) {
  const token = getToken();
  const headers = new Headers(init.headers || {});
  headers.set("Accept", "application/json");
  if (token) headers.set("Authorization", "Bearer " + token);
  init.headers = headers;
  return fetch(input, init);
}

// Para compatibilidade com ficheiros não modulares (login.js), expõe globalmente:
window.Session = {
  setToken, getToken, clearToken, setUser, getUser, authFetch
};
