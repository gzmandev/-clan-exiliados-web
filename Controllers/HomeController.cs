using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ClanExiliados_Web.Models;
using ClanExiliados_Web.Services;

namespace ClanExiliados_Web.Controllers;

public class HomeController : Controller
{
    private readonly ClanInstitutionalService _clan;

    public HomeController(ClanInstitutionalService clan)
    {
        _clan = clan;
    }

    // La página de inicio (PA3 Parte C + Dashboard Tactical HUD & Command Center): /
    public IActionResult Index()
    {
        return View(_clan);
    }

    // Acerca del sistema (PA3 Parte B/C): /Home/AcercaDe
    public IActionResult AcercaDe()
    {
        return View(_clan);
    }

    // La página de error personalizada (PA3 Parte E). La abre Program.cs cuando la dirección no existe.
    public IActionResult Estado(int? id)
    {
        ViewData["CodigoError"] = id ?? 404;
        return View();
    }

    // Cambio demostrativo entre los 4 perfiles visuales (Dirección, Mando, Responsables, Integrantes)
    [HttpPost]
    public IActionResult CambiarPerfil(string perfil, string? returnUrl)
    {
        _clan.CambiarPerfilVisual(perfil);
        TempData["ToastMensaje"] = $"Perfil visual conmutado a: {perfil}";
        if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
        {
            return Redirect(returnUrl);
        }
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public IActionResult AtenderAlerta(string id, string? returnUrl)
    {
        _clan.MarcarAlertaAtendida(id);
        TempData["ToastMensaje"] = $"Alerta {id} marcada como atendida.";
        if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
        {
            return Redirect(returnUrl);
        }
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public IActionResult LeerNotificaciones(string? returnUrl)
    {
        _clan.MarcarNotificacionesLeidas();
        TempData["ToastMensaje"] = "Todas las notificaciones fueron marcadas como leídas.";
        if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
        {
            return Redirect(returnUrl);
        }
        return RedirectToAction(nameof(Index));
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
