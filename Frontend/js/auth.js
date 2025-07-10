document.getElementById('loginForm').addEventListener('submit', async (e) => {
  e.preventDefault();

  const username = document.getElementById('username').value.trim();
  const password = document.getElementById('password').value.trim();
  const message = document.getElementById('message');

  if (!username || !password) {
    message.textContent = "Todos los campos son obligatorios.";
    message.className = "text-danger";
    return;
  }

  try {
    const response = await fetch('http://localhost:8080/api/Auth/login', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify({ username, password })
    });

    if (!response.ok) {
      message.textContent = "Credenciales inválidas.";
      message.className = "text-danger";
      return;
    }

    const result = await response.json();

    localStorage.setItem('accessToken', result.token);
    window.location.href = 'dashboard.html';

  } catch (error) {
    console.error(error);
    message.textContent = "Error al conectar con el servidor.";
    message.className = "text-danger";
  }
});

function checkAuth() {
  const token = localStorage.getItem('accessToken');
  if (!token) {
    window.location.href = 'login.html';
  }
}
