using Microsoft.AspNetCore.Mvc;
using ClanExiliados_Web.Services;

namespace ClanExiliados_Web.Controllers;

// SECCIONES 7 Y 20: RANGOS OFICIALES, DEPARTAMENTOS, UNIDADES Y ORGANIGRAMA INTERACTIVO
public class EstructuraController : Controller
{
    private readonly ClanInstitutionalService _clan;

    public EstructuraController(ClanInstitutionalService clan)
    {
        _clan = clan;
    }

    public IActionResult Facciones()
    {
        return View(_clan);
    }

    public IActionResult Rangos()
    {
        return View(_clan);
    }

    public IActionResult Departamentos()
    {
        return View(_clan);
    }

    public IActionResult Unidades()
    {
        return View(_clan);
    }

    public IActionResult Organigrama(string? perspectiva, string? rango, string? departamento, string? unidad)
    {
        ViewData["Perspectiva"] = string.IsNullOrWhiteSpace(perspectiva) ? "jerarquica" : perspectiva.ToLowerInvariant();
        ViewData["FiltroRango"] = rango ?? "";
        ViewData["FiltroDepartamento"] = departamento ?? "";
        ViewData["FiltroUnidad"] = unidad ?? "";
        return View(_clan);
    }
}
