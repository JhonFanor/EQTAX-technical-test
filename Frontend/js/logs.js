function initLogs() {
  const tbody = document.getElementById('logsTableBody');
  const message = document.getElementById('logMessage');
  const pagination = document.getElementById('logsPagination');

  if (!tbody || !message || !pagination) {
    console.warn("Elementos de logs no encontrados.");
    return;
  }

  const API_URL = 'http://localhost:8080/api/LogProcess';
  const token = localStorage.getItem('accessToken');
  const headers = { Authorization: `Bearer ${token}` };

  let currentPage = 1;

  async function loadLogs(page = 1) {
    tbody.innerHTML = '';
    message.innerHTML = '';
    pagination.innerHTML = '';
    currentPage = page;

    try {
      const res = await fetch(`${API_URL}?pageNumber=${page}&pageSize=10`, { headers });

      if (!res.ok) {
        const error = await res.text();
        showMsg(`Error al obtener logs: ${error}`, 'danger');
        return;
      }

      const data = await res.json();

      if (data.items.length === 0) {
        tbody.innerHTML = '<tr><td colspan="4" class="text-center">No hay registros.</td></tr>';
        return;
      }

      data.items.forEach(log => {
        const row = document.createElement('tr');
        row.innerHTML = `
          <td>${log.originalFileName}</td>
          <td>${log.status}</td>
          <td>${log.newFileName || '-'}</td>
          <td>${new Date(log.dateProcess).toLocaleString()}</td>
        `;
        tbody.appendChild(row);
      });

      renderPagination(data.pageNumber, data.totalPages);

    } catch (err) {
      console.error(err);
      showMsg("Error de conexión con el servidor.", 'danger');
    }
  }

  function renderPagination(current, total) {
    const createBtn = (label, page, disabled = false, active = false) => {
      const btn = document.createElement('button');
      btn.className = `btn btn-sm btn-outline-primary mx-1 ${active ? 'active' : ''}`;
      btn.textContent = label;
      btn.disabled = disabled;
      btn.addEventListener('click', () => loadLogs(page));
      return btn;
    };

    pagination.appendChild(createBtn('«', current - 1, current === 1));

    for (let i = 1; i <= total; i++) {
      pagination.appendChild(createBtn(i, i, false, i === current));
    }

    pagination.appendChild(createBtn('»', current + 1, current === total));
  }

  function showMsg(msg, type) {
    message.textContent = msg;
    message.className = `alert alert-${type}`;
  }

  // Carga inicial
  loadLogs();
}
