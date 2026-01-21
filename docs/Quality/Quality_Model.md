# Quality Model - ISO/IEC 25010 (DonJulioSuper)

## Modelo seleccionado
Se utiliza **ISO/IEC 25010** para definir atributos de calidad y métricas verificables.

| Atributo | Definición (1 línea) | Métrica verificable | EC que lo soporta |
|---|---|---|---|
| Adecuación funcional | Cumple funciones esperadas | RF críticos (REQ-002..REQ-004) verificados con checklist | /docs/SRS, /tests, /src |
| Eficiencia de desempeño | Responde rápido y usa recursos bien | ≤ 2s en 95% de búsquedas/listados frecuentes (NFR-001) | /docs/SRS, /tests |
| Fiabilidad | Opera sin fallos frecuentes | 0 errores críticos al registrar venta 5 veces seguidas | /tests, /src |
| Seguridad | Protege datos y accesos | 0 contraseñas en texto plano (NFR-002) | /src, /config |
| Mantenibilidad | Fácil de modificar sin romper | Cambios pequeños requieren ≤ 3 archivos (meta de equipo) | /src |
| Usabilidad | Fácil de usar | Registrar una venta en ≤ 5 pasos (checklist UX) | /tests |

## Métricas “estrella”
1) Rendimiento: **≤ 2 segundos en el 95%** de búsquedas/listados frecuentes.  
2) Seguridad: **0 contraseñas en texto plano** (hash seguro).
