# InventarioApp

## Descripci�n General

InventarioApp es una aplicaci�n de gesti�n de productos y categor�as que permite realizar operaciones CRUD (Crear, Leer, Actualizar y Eliminar) tanto en productos como en categor�as. El frontend est� desarrollado en **Angular**, mientras que el backend est� construido con **.NET 8** y utiliza servicios REST para gestionar los datos.

---

## Backend (API en .NET 8)

### Requisitos

- .NET 8 SDK
- SQL Server o cualquier base de datos compatible
- Visual Studio o cualquier editor compatible con .NET

### Instalaci�n y Ejecuci�n del Backend

1. Clonar el repositorio.
2. Restaurar los paquetes NuGet.
3. Ejecutar el proyecto.

Una vez que los paquetes NuGet est�n restaurados y la cadena de conexi�n est� configurada, se puede iniciar la aplicaci�n. Esto iniciar� el servidor backend en la m�quina local.

---

## Frontend (Angular)

### Instalaci�n y Ejecuci�n del Frontend

1. Clonar el repositorio del frontend.
2. Instalar las dependencias de Node.js.
3. Iniciar la aplicaci�n de desarrollo.

---

## Descripci�n de la Aplicaci�n

### Backend (.NET 8)

El backend de la aplicaci�n est� construido en .NET 8, utilizando un modelo de API RESTful con controladores para gestionar **productos** y **categor�as**. Los puntos clave de la aplicaci�n backend son:

- **Categor�as**:
  - Obtener todas las categor�as.
  - Obtener una categor�a espec�fica.
  - Crear una nueva categor�a.
  - Actualizar una categor�a existente.
  - Eliminar una categor�a.

- **Productos**:
  - Obtener todos los productos.
  - Obtener un producto espec�fico.
  - Crear un nuevo producto.
  - Actualizar un producto existente.
  - Eliminar un producto.

Se incluye una funci�n de **Seed** (precarga) de datos para que cada vez que se ejecute la API se carguen datos predeterminados. Las categor�as y productos que se crean inicialmente son:

- **Categor�as**:
  - Electr�nica
  - Libros
  - Muebles

- **Productos**:
  - Laptop 
  - Smartphone
  - Auriculares 
  - Estanter�a 
  - Libro de Programaci�n 

Los productos se relacionan con las categor�as a trav�s de la tabla de asociaci�n **ProductCategory**.

---

### Frontend (Angular)

El frontend est� construido con **Angular** y **Angular Material** para el dise�o visual, proporcionando una interfaz para que los usuarios puedan interactuar con el sistema de inventario. Las funcionalidades principales incluyen:

- **Gesti�n de Categor�as**:
  - Crear, editar y eliminar categor�as.
  
- **Gesti�n de Productos**:
  - Crear, editar y eliminar productos.
  
El frontend se comunica con el backend a trav�s de llamadas HTTP a las APIs REST creadas en .NET 8. 

---

## Otros detalles t�cnicos

- La aplicaci�n est� utilizando **SQL In-Memory** para prop�sitos de demostraci�n r�pida (PoC).
- Se implementaron **tests unitarios** para los controladores y servicios en el backend.
- Se utiliza **Angular Material** para darle estilo a los componentes visuales en el frontend.
