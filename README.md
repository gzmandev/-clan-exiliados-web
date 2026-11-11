# Clan Exiliados — Plataforma Institucional MILSIM & RP (PA3)

**Curso:** Desarrollo de Aplicaciones Básico  
**Tecnología:** ASP.NET Core 10.0, C#, Razor Views, Bootstrap 5, SQL Server

## Descripción

Plataforma web para Clan Exiliados (comunidad MILSIM / Roleplay). Gestión de solicitudes de ingreso, dashboard táctico, perfiles visuales, departamentos, unidades, operaciones, alertas dinámicas, búsqueda global.

## Tecnologías

| Componente | Versión |
|------------|---------|
| .NET SDK | 10.0 |
| ASP.NET Core | MVC + Razor |
| Base de datos | SQL Server |
| CSS | Bootstrap 5 |

## Ejecución

1. Abre `ClanExiliados.sln` en Visual Studio 2022+.
2. Presiona `F5` para ejecutar.
3. URL: `https://localhost:puerto/`

## Estructura

```
ClanExiliados_Web/
├── Controllers/   # Home, Solicitudes, Analitica, Consultas, Estructura, etc.
├── Services/      # ClanInstitutionalService (datos en memoria)
├── Views/         # Razor Views (.cshtml)
├── wwwroot/       # CSS, JS, lib (Bootstrap, jQuery, Lucide)
└── Program.cs
```

## Notas

- Carpetas `.vs/`, `bin/`, `obj/` excluidas por `.gitignore`.
- Datos en memoria (no conectado a SQL Server en tiempo de ejecución; `base.sql` corresponde al alcance PA2).
- Proyecto apunta a .NET 10 (muy reciente; requiere SDK instalado).