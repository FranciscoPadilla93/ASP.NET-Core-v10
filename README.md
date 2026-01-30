# 🚀 Sistema de Gestión de Usuarios - Web API .NET

Este proyecto es una **Web API robusta y escalable** construida con **.NET 8**, diseñada bajo una arquitectura de **N-Capas**. Implementa estándares de seguridad modernos y un manejo eficiente de datos orientado a alto rendimiento.

## 🛠️ Stack Tecnológico

* **Framework:** .NET 8 (Web API)
* **Acceso a Datos:** Dapper (Micro-ORM) para alto rendimiento con Stored Procedures.
* **Base de Datos:** SQL Server.
* **Seguridad:** JSON Web Tokens (JWT) con Roles y Refresh Tokens.
* **Validación:** FluentValidation para limpieza de datos de entrada.
* **Cifrado:** BCrypt.Net para el hasheo de contraseñas.

## 🏗️ Arquitectura del Proyecto

El sistema está dividido en 4 capas para asegurar la separación de responsabilidades:
1.  **Api:** Controladores y configuración de Middleware (CORS, Auth, Exception Handling).
2.  **Business:** Lógica de negocio, servicios y validadores (FluentValidation).
3.  **Data:** Repositorios que gestionan la persistencia mediante Dapper.
4.  **Entities:** Modelos de datos y objetos de transferencia (DTOs).

## 🔐 Características de Seguridad

* **Autenticación JWT:** Emisión de Access Tokens con tiempo de vida corto (15 min) para mitigar riesgos.
* **Refresh Tokens:** Implementación de persistencia de sesión mediante tokens de refresco almacenados en base de datos.
* **Control de Roles:** Autorización basada en roles para proteger endpoints específicos.
* **Middleware Global de Excepciones:** Captura de errores centralizada que devuelve respuestas estandarizadas mediante `ApiResponse<T>`.

## 📊 Base de Datos e Infraestructura

* **Logging:** Registro de auditoría en la tabla `App_Log` para transacciones de Create, Update y Delete.
* **Stored Procedures:** Toda la lógica de persistencia reside en procedimientos almacenados optimizados.
* **CORS:** Configurado para comunicación segura con aplicaciones Angular en entornos de desarrollo.

## 🚀 Próximos Pasos (Frontend)
* [ ] Implementación de Dashboard en Angular.
* [ ] Interceptores HTTP para gestión automática de JWT y Refresh Tokens.
* [ ] Guards de navegación basados en roles.
