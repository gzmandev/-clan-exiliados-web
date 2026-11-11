using Microsoft.AspNetCore.Mvc;

namespace ClanExiliados_Web.Controllers;

// La sección del mantenimiento de solicitudes (el CRUD de la PA2)
public class SolicitudesController : Controller
{
    // El listado de solicitudes: /Solicitudes
    public IActionResult Index()
    {
        return View();
    }

    // El formulario de alta de solicitud: /Solicitudes/Nuevo
    public IActionResult Nuevo()
    {
        return View();
    }
}
