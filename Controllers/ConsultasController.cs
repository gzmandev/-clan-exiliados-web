using Microsoft.AspNetCore.Mvc;

namespace ClanExiliados_Web.Controllers;

// La sección de consultas del sistema
public class ConsultasController : Controller
{
    // Redirección o vista principal de consultas
    public IActionResult Index()
    {
        return RedirectToAction("Periodo");
    }

    // La consulta de solicitudes por periodo: /Consultas/Periodo
    public IActionResult Periodo(DateTime? desde, DateTime? hasta, string? faccion)
    {
        ViewBag.Desde = desde?.ToString("yyyy-MM-dd") ?? "2026-07-01";
        ViewBag.Hasta = hasta?.ToString("yyyy-MM-dd") ?? "2026-10-31";
        ViewBag.Faccion = string.IsNullOrWhiteSpace(faccion) ? "(Todos)" : faccion;
        return View();
    }
}
