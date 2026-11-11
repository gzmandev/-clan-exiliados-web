using Microsoft.AspNetCore.Mvc;
using ClanExiliados_Web.Services;

namespace ClanExiliados_Web.Controllers;

// SECCIONES 17, 18 Y 23: COMUNICACIONES INSTITUCIONALES, CENTRO DOCUMENTAL Y ACTIVITY FEED
public class ComunicacionesController : Controller
{
    private readonly ClanInstitutionalService _clan;

    public ComunicacionesController(ClanInstitutionalService clan)
    {
        _clan = clan;
    }

    // Centro de Comunicaciones: /Comunicaciones
    public IActionResult Index(string? prioridad, string? estado)
    {
        ViewData["FiltroPrioridad"] = prioridad ?? "";
        ViewData["FiltroEstado"] = estado ?? "";
        return View(_clan);
    }

    [HttpPost]
    public IActionResult Publicar(string titulo, string contenido, string categoria, string prioridad, string departamento, string destinatarios)
    {
        if (string.IsNullOrWhiteSpace(titulo) || string.IsNullOrWhiteSpace(contenido))
        {
            TempData["ToastError"] = "Debe completar el título y el contenido del comunicado.";
            return RedirectToAction(nameof(Index));
        }

        var com = _clan.PublicarComunicado(titulo, contenido, categoria, prioridad, departamento, destinatarios);
        TempData["ToastMensaje"] = $"Comunicado {com.Id} publicado y notificado a {com.Destinatarios}.";
        return RedirectToAction(nameof(Index));
    }

    // Centro Documental: /Comunicaciones/Documentos
    public IActionResult Documentos(string? categoria)
    {
        ViewData["FiltroCategoria"] = categoria ?? "";
        return View(_clan);
    }

    // Activity Feed Independiente: /Comunicaciones/Actividad
    public IActionResult Actividad(string? tipo)
    {
        ViewData["FiltroTipo"] = tipo ?? "";
        return View(_clan);
    }
}
