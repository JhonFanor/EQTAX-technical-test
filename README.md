# EQTAX-technical-test
# Instrucciones para el repositorio

## 📋 Requisitos previos

- Docker y Docker Compose instalados en tu sistema
- Crear la carpeta local `C:\PruebaEQ` (o modificar la variable de entorno si prefieres otra ubicación)

## ⚙️ Configuración inicial

### Variables de entorno

El proyecto requiere un archivo `.env` en la carpeta principal con la siguiente variable:

```env
UPLOAD_FOLDER_HOST="C:\PruebaEQ"
```
Esta ruta será utilizada por la aplicación para subir y procesar archivos. Si la ubicación no existe, la aplicación no podrá iniciar.

## Iniciar la aplicación
Ejecuta el siguiente comando en la raíz del proyecto:
```
docker compose up --build
```
Este comando:
- Construirá las imágenes de Docker necesarias

- Levantará todos los servicios definidos en el compose

- Inicializará la aplicación completa

## Servicios disponibles
### Backend
- URL: http://localhost:8080/

- Swagger (Documentación API): Disponible en la ruta principal del backend

- Funcionalidades:

  - Procesamiento de archivos

  - API para interactuar con el sistema

### Frontend
- URL: http://localhost:5173/

- Aplicación web para interactuar con el backend
### Procesamiento de documentos
- Se encarga de revisar que los archivos se ejecuta en segundo plano.

## Notas importantes
- Asegúrate de que la carpeta especificada en UPLOAD_FOLDER_HOST exista antes de iniciar la aplicación

- Todos los servicios se inician automáticamente con el comando docker compose up --build

- Para detener la aplicación, usa Ctrl+C en la terminal o docker compose down en otra terminal
- El usuario y contraseña para iniciar sesion es admin y la contraseña 123456
