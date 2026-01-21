# SRS v1 - DonJulioSuper (Inventario/Supermercado)

## Alcance
Sistema para gestionar productos, stock y ventas/pedidos con reportes básicos.

## Requisitos Funcionales (RF)
- REQ-001: El sistema debe permitir registro e inicio de sesión de usuarios.
- REQ-002: El sistema debe permitir crear/editar/eliminar productos.
- REQ-003: El sistema debe permitir registrar entradas y salidas de inventario.
- REQ-004: El sistema debe permitir registrar ventas/pedidos y generar un comprobante/número de transacción.
- REQ-005: El sistema debe permitir consultar stock actual por producto.
- REQ-006: El sistema debe permitir generar reportes básicos (ventas por fecha / productos con bajo stock).

## Requisitos No Funcionales (RNF)
- NFR-001 (Rendimiento): La búsqueda/listado de productos debe responder en ≤ 2 segundos en el 95% de consultas frecuentes.
- NFR-002 (Seguridad): No se deben almacenar contraseñas en texto plano (usar hash seguro).

## Criterios mínimos de aceptación
- Documentación versionada en /docs (SRS, Quality, Lifecycle)
- Configuración ejemplo controlada en /config
- Evidencia mínima de pruebas en /tests
