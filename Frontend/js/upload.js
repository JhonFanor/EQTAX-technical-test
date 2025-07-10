function initUpload() {
  const form = document.getElementById('uploadForm');
  const fileInput = document.getElementById('pdfFile');
  const message = document.getElementById('uploadMessage');

  if (!form || !fileInput || !message) {
    console.warn("Elementos de carga no encontrados");
    return;
  }

  form.addEventListener('submit', async (e) => {
    e.preventDefault();
    message.innerHTML = '';

    const file = fileInput.files[0];
    if (!file) {
      return showMessage('Selecciona un archivo.', 'warning');
    }

    if (file.type !== 'application/pdf') {
      return showMessage('Solo se permiten archivos PDF.', 'danger');
    }

    if (file.size > 10 * 1024 * 1024) {
      return showMessage('El archivo no debe superar los 10MB.', 'danger');
    }

    const formData = new FormData();
    formData.append('file', file);

    try {
      const token = localStorage.getItem('accessToken');
      const response = await fetch('http://localhost:8080/api/File/upload', {
        method: 'POST',
        headers: {
          Authorization: `Bearer ${token}`
        },
        body: formData
      });

      const result = await response.text();

      if (response.ok) {
        message.innerHTML = `<div class="alert alert-success">Archivo subido correctamente</div>`;
        form.reset();
      } else {
        showMessage(`Error: ${result}`, 'danger');
      }
    } catch (err) {
      console.error(err);
      showMessage('Error de conexión con el servidor.', 'danger');
    }
  });

  function showMessage(msg, type) {
    message.innerHTML = `<div class="alert alert-${type}">${msg}</div>`;
  }
}
