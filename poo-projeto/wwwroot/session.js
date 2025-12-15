// ---------------------------------------------------------
// session.js
// Responsável por gerir a sessão do utilizador no browser.Guarda token e dados do utilizador no localStorage. Fornece uma função de fetch autenticado.
// ---------------------------------------------------------

// Chaves usadas no localStorage
const SESSION_STORAGE_TOKEN_KEY = "gf_token";
const SESSION_STORAGE_USER_KEY = "gf_user";

// ---------------------------------------------------------
// Guarda o token de autenticação
// ---------------------------------------------------------
export function setToken(token) {
  localStorage.setItem(SESSION_STORAGE_TOKEN_KEY, token);
}

// ---------------------------------------------------------
// Obtém o token guardado (ou null se não existir)
// ---------------------------------------------------------
export function getToken() {
  return localStorage.getItem(SESSION_STORAGE_TOKEN_KEY);
}

// ---------------------------------------------------------
// Remove o token e os dados do utilizador (logout)
// ---------------------------------------------------------
export function clearToken() {
  localStorage.removeItem(SESSION_STORAGE_TOKEN_KEY);
  localStorage.removeItem(SESSION_STORAGE_USER_KEY);
}

// ---------------------------------------------------------
// Guarda os dados do utilizador autenticado
// ---------------------------------------------------------
export function setUser(user) {
  localStorage.setItem(
    SESSION_STORAGE_USER_KEY,
    JSON.stringify(user)
  );
}

// ---------------------------------------------------------
// Obtém os dados do utilizador autenticado
// ---------------------------------------------------------
export function getUser() {
  const v = localStorage.getItem(SESSION_STORAGE_USER_KEY);
  return v ? JSON.parse(v) : null;
}

/**
 * ---------------------------------------------------------
 * authFetch
 * Função auxiliar que faz pedidos fetch autenticados.
 * Adiciona automaticamente o cabeçalho:
 * Authorization: Bearer <token>
 * Caso o token exista.
 * ---------------------------------------------------------
 */
export async function authFetch(input, init = {}) {

  // Obtém o token da sessão
  const token = getToken();

  // Cria/atualiza os headers do pedido
  const headers = new Headers(init.headers || {});
  headers.set("Accept", "application/json");

  // Se existir token, adiciona ao cabeçalho
  if (token) {
    headers.set("Authorization", "Bearer " + token);
  }

  init.headers = headers;

  // Executa o fetch normal
  return fetch(input, init);
}

// ---------------------------------------------------------
// Exposição global para scripts não modulares
// (ex: login.js)
// ---------------------------------------------------------
window.Session = {
  setToken,
  getToken,
  clearToken,
  setUser,
  getUser,
  authFetch
};
