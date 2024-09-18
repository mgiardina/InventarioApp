# InventarioApp

## Descripción General

InventarioApp es una aplicación de gestión de productos y categorías que permite realizar operaciones CRUD (Crear, Leer, Actualizar y Eliminar) tanto en productos como en categorías. El frontend está desarrollado en **Angular**, mientras que el backend está construido con **.NET 8** y utiliza servicios REST para gestionar los datos.

---

## Backend (API en .NET 8)

### Requisitos

- .NET 8 SDK
- SQL Server o cualquier base de datos compatible
- Visual Studio o cualquier editor compatible con .NET

### Instalación y Ejecución del Backend

1. Clonar el repositorio.
2. Restaurar los paquetes NuGet.
3. Ejecutar el proyecto.

Una vez que los paquetes NuGet estén restaurados y la cadena de conexión esté configurada, se puede iniciar la aplicación. Esto iniciará el servidor backend en la máquina local.

---

## Frontend (Angular)

### Instalación y Ejecución del Frontend

1. Clonar el repositorio del frontend.
2. Instalar las dependencias de Node.js.
3. Iniciar la aplicación de desarrollo.

---

## Descripción de la Aplicación

### Backend (.NET 8)

El backend de la aplicación está construido en .NET 8, utilizando un modelo de API RESTful con controladores para gestionar **productos** y **categorías**. Los puntos clave de la aplicación backend son:

- **Categorías**:
  - Obtener todas las categorías.
  - Obtener una categoría específica.
  - Crear una nueva categoría.
  - Actualizar una categoría existente.
  - Eliminar una categoría.

- **Productos**:
  - Obtener todos los productos.
  - Obtener un producto específico.
  - Crear un nuevo producto.
  - Actualizar un producto existente.
  - Eliminar un producto.

Se incluye una función de **Seed** (precarga) de datos para que cada vez que se ejecute la API se carguen datos predeterminados. Las categorías y productos que se crean inicialmente son:

- **Categorías**:
  - Electrónica
  - Libros
  - Muebles

- **Productos**:
  - Laptop 
  - Smartphone
  - Auriculares 
  - Estantería 
  - Libro de Programación 

Los productos se relacionan con las categorías a través de la tabla de asociación **ProductCategory**.

---

### Frontend (Angular)

El frontend está construido con **Angular** y **Angular Material** para el diseño visual, proporcionando una interfaz para que los usuarios puedan interactuar con el sistema de inventario. Las funcionalidades principales incluyen:

- **Gestión de Categorías**:
  - Crear, editar y eliminar categorías.
  
- **Gestión de Productos**:
  - Crear, editar y eliminar productos.
  
El frontend se comunica con el backend a través de llamadas HTTP a las APIs REST creadas en .NET 8. 

---

## Otros detalles técnicos

- La aplicación está utilizando **SQL In-Memory** para propósitos de demostración rápida (PoC).
- Se implementaron **tests unitarios** para los controladores y servicios en el backend.
- Se utiliza **Angular Material** para darle estilo a los componentes visuales en el frontend.
