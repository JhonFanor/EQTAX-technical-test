function loadNavbar() {
  fetch('components/navbar.html')
    .then(response => response.text())
    .then(html => {
      document.getElementById('navbar').innerHTML = html;
    });
}

function logout() {
  localStorage.removeItem('accessToken');
  localStorage.removeItem('refreshToken');
  window.location.href = 'login.html';
}

function goToSection(section) {
  alert(`Ir a la sección: ${section}`);
}
