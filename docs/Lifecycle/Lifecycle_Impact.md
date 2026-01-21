# Lifecycle Impact - Cambio simulado por fases (DonJulioSuper)

## Cambio simulado (Caso 2: Seguridad)
Implementar **2FA para administradores** (segundo factor al iniciar sesión con rol admin).

| Fase | ¿Qué cambia? | EC afectados | Riesgo si no se controla | Evidencia de validación |
|---|---|---|---|---|
| Requisitos | Agregar RNF/REQ de 2FA + criterios de aceptación | /docs/SRS/SRS_v1.md | Ambigüedad → retrabajo y falla de seguridad | Commit + revisión |
| Diseño | Definir flujo: login admin → OTP | /docs/Lifecycle/ | Implementación inconsistente | Checklist de flujo |
| Implementación | Lógica de 2FA para rol admin | /src/ | Admin vulnerable | Commit feat |
| Configuración | Flag para activar 2FA | /config/config.example | No se puede activar/desactivar | Config controlada |
| Pruebas | Caso: admin con 2FA ON/OFF | /tests/ | “funciona en mi PC” | Evidencia checklist |
| Despliegue/Mant. | Documentar activación/rollback | README/CHANGELOG | Caídas y regresión sin trazabilidad | Registro en changelog |

Nota: GCS reduce costo de cambios controlando EC y evidencias.
