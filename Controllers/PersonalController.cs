using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ClanExiliados_Web.Services;

namespace ClanExiliados_Web.Controllers;

// SECCIÓN 9: PERSONAL, EXPEDIENTES Y CREDENCIALES DIGITALES
public class PersonalController : Controller
{
    private readonly ClanInstitutionalService _clan;

    public PersonalController(ClanInstitutionalService clan)
    {
        _clan = clan;
    }

    // Vista dual: Tarjetas visuales HUD y Tabla administrativa PA2
    public IActionResult Index(string? vista, string? q, string? rango, string? departamento, string? unidad, string? estado, string? disponibilidad)
    {
        ViewData["VistaActiva"] = string.IsNullOrWhiteSpace(vista) ? "tarjetas" : vista.ToLowerInvariant();
        ViewData["FiltroQ"] = q ?? "";
        ViewData["FiltroRango"] = rango ?? "";
        ViewData["FiltroDepartamento"] = departamento ?? "";
        ViewData["FiltroUnidad"] = unidad ?? "";
        ViewData["FiltroEstado"] = estado ?? "";
        ViewData["FiltroDisponibilidad"] = disponibilidad ?? "";

        var lista = _clan.Integrantes.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(q))
        {
            var term = q.Trim();
            lista = lista.Where(i =>
                i.Nombre.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                i.Alias.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                i.CodigoCE.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                i.Discord.Contains(term, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(rango))
            lista = lista.Where(i => i.Rango.Equals(rango, StringComparison.OrdinalIgnoreCase));

        if (!string.IsNullOrWhiteSpace(departamento))
            lista = lista.Where(i => i.Departamentos.Any(d => d.Equals(departamento, StringComparison.OrdinalIgnoreCase)));

        if (!string.IsNullOrWhiteSpace(unidad))
            lista = lista.Where(i => i.Unidades.Any(u => u.Equals(unidad, StringComparison.OrdinalIgnoreCase)));

        if (!string.IsNullOrWhiteSpace(estado))
            lista = lista.Where(i => i.EstadoInstitucional.Equals(estado, StringComparison.OrdinalIgnoreCase));

        if (!string.IsNullOrWhiteSpace(disponibilidad))
            lista = lista.Where(i => i.Disponibilidad.Equals(disponibilidad, StringComparison.OrdinalIgnoreCase));

        return View(lista.ToList());
    }

    // Expediente individual completo: /Personal/Expediente/CE-001
    public IActionResult Expediente(string id)
    {
        var integrante = _clan.Integrantes.FirstOrDefault(i => i.CodigoCE.Equals(id, StringComparison.OrdinalIgnoreCase))
                         ?? _clan.Integrantes.First();
        return View(integrante);
    }

    // Credencial digital institucional (vista completa, compacta, impresión y exportación PNG): /Personal/Credencial/CE-001
    public IActionResult Credencial(string id, string? modo)
    {
        var integrante = _clan.Integrantes.FirstOrDefault(i => i.CodigoCE.Equals(id, StringComparison.OrdinalIgnoreCase))
                         ?? _clan.Integrantes.First();
        ViewData["ModoCredencial"] = string.IsNullOrWhiteSpace(modo) ? "completa" : modo.ToLowerInvariant();
        return View(integrante);
    }
}
