function initDocKey() {
  const createForm = document.getElementById('createKeyForm');
  const editForm = document.getElementById('editKeyForm');
  const keyTableBody = document.getElementById('keyTableBody');
  const keyMessage = document.getElementById('keyMessage');
  const pagination = document.getElementById('pagination');

  if (!createForm || !editForm || !keyTableBody || !pagination || !keyMessage) {
    console.warn("Elementos de DocKey no encontrados");
    return;
  }

  const API_URL = 'http://localhost:8080/api/DocKeys';
  const token = localStorage.getItem('accessToken');

  const headers = {
    'Content-Type': 'application/json',
    'Authorization': `Bearer ${token}`
  };

  let currentPage = 1;

  async function loadKeys(page = 1) {
    keyTableBody.innerHTML = '';
    pagination.innerHTML = '';

    try {
      const res = await fetch(`${API_URL}?pageNumber=${page}&pageSize=10`, { headers });
      const data = await res.json();

      data.items.forEach(item => {
        const row = document.createElement('tr');
        row.innerHTML = `
          <td>${item.key}</td>
          <td>${item.docName}</td>
          <td>
            <button class="btn btn-sm btn-secondary" data-edit="${item.id}" data-key="${item.key}" data-doc="${item.docName}">Editar</button>
            <button class="btn btn-sm btn-danger" data-delete="${item.id}">Eliminar</button>
          </td>
        `;
        keyTableBody.appendChild(row);
      });

      renderPagination(data.pageNumber, data.totalPages);
    } catch (err) {
      showMsg("Error al cargar claves", 'danger');
      console.error(err);
    }
  }

  function renderPagination(current, total) {
    const createBtn = (label, page, disabled = false, active = false) => {
      const btn = document.createElement('button');
      btn.className = `btn btn-sm btn-outline-primary mx-1 ${active ? 'active' : ''}`;
      btn.textContent = label;
      btn.disabled = disabled;
      btn.addEventListener('click', () => {
        currentPage = page;
        loadKeys(page);
      });
      return btn;
    };

    if (total > 1) {
      pagination.appendChild(createBtn('«', current - 1, current === 1));

      for (let i = 1; i <= total; i++) {
        pagination.appendChild(createBtn(i, i, false, i === current));
      }

      pagination.appendChild(createBtn('»', current + 1, current === total));
    }
  }

  createForm.addEventListener('submit', async (e) => {
    e.preventDefault();

    const clave = document.getElementById('newClave').value.trim();
    const docName = document.getElementById('newDocName').value.trim();

    if (!clave || !docName) {
      showMsg("Todos los campos son obligatorios", 'danger');
      return;
    }

    try {
      const res = await fetch(API_URL, {
        method: 'POST',
        headers,
        body: JSON.stringify({ key: clave, docName })
      });

      if (res.ok) {
        showMsg("Clave creada correctamente", 'success');
        createForm.reset();
        loadKeys(currentPage);
      } else {
        const msg = await res.text();
        showMsg(`Error al crear clave: ${msg}`, 'danger');
      }
    } catch (err) {
      showMsg("Error de conexión", 'danger');
      console.error(err);
    }
  });

  editForm.addEventListener('submit', async (e) => {
    e.preventDefault();

    const id = document.getElementById('editKeyId').value;
    const clave = document.getElementById('editClave').value.trim();
    const docName = document.getElementById('editDocName').value.trim();

    if (!clave || !docName) {
      showMsg("Todos los campos son obligatorios", 'danger');
      return;
    }

    try {
      const res = await fetch(`${API_URL}/${id}`, {
        method: 'PUT',
        headers,
        body: JSON.stringify({ key: clave, docName })
      });

      if (res.ok) {
        showMsg("Clave actualizada correctamente", 'success');
        editForm.classList.add('d-none');
        createForm.classList.remove('d-none');
        editForm.reset();
        loadKeys(currentPage);
      } else {
        const msg = await res.text();
        showMsg(`Error al actualizar clave: ${msg}`, 'danger');
      }
    } catch (err) {
      showMsg("Error de conexión", 'danger');
      console.error(err);
    }
  });

  document.getElementById('cancelEdit').addEventListener('click', () => {
    editForm.classList.add('d-none');
    createForm.classList.remove('d-none');
    editForm.reset();
  });

  keyTableBody.addEventListener('click', async (e) => {
    const editBtn = e.target.closest('[data-edit]');
    const deleteBtn = e.target.closest('[data-delete]');

    if (editBtn) {
      document.getElementById('editKeyId').value = editBtn.dataset.edit;
      document.getElementById('editClave').value = editBtn.dataset.key;
      document.getElementById('editDocName').value = editBtn.dataset.doc;

      createForm.classList.add('d-none');
      editForm.classList.remove('d-none');
    }

    if (deleteBtn) {
      const id = deleteBtn.dataset.delete;
      if (!confirm("¿Estás seguro de eliminar esta clave?")) return;

      try {
        const res = await fetch(`${API_URL}/${id}`, {
          method: 'DELETE',
          headers
        });

        if (res.ok) {
          showMsg("Clave eliminada correctamente", 'success');
          loadKeys(currentPage);
        } else {
          const msg = await res.text();
          showMsg(`Error al eliminar clave: ${msg}`, 'danger');
        }
      } catch (err) {
        showMsg("Error de conexión", 'danger');
        console.error(err);
      }
    }
  });

  function showMsg(msg, type) {
    keyMessage.textContent = msg;
    keyMessage.className = `alert alert-${type}`;
  }

  loadKeys();
}
