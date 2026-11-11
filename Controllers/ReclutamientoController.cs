using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ClanExiliados_Web.Services;

namespace ClanExiliados_Web.Controllers;

// SECCIÓN 8: RECLUTAMIENTO, EVALUACIÓN (+18), INCORPORACIÓN Y CEREMONIA DIGITAL
public class ReclutamientoController : Controller
{
    private readonly ClanInstitutionalService _clan;

    public ReclutamientoController(ClanInstitutionalService clan)
    {
        _clan = clan;
    }

    public IActionResult Index(string? estado)
    {
        ViewData["FiltroEstado"] = estado ?? "";
        return View(_clan);
    }

    [HttpPost]
    public IActionResult Registrar(
        string nombre, string alias, string discord, int edad, int horasMilsim,
        string faccionPA2, string disponibilidadPA2, string experienciaMilsim, string experienciaRP,
        List<string>? plataformas, string servidoresAnteriores, List<string>? especialidades,
        string descripcionPersonal, string antecedentesRP)
    {
        if (string.IsNullOrWhiteSpace(alias) || string.IsNullOrWhiteSpace(discord))
        {
            TempData["ToastError"] = "Debe ingresar el alias del jugador y su usuario de Discord.";
            return RedirectToAction(nameof(Index));
        }

        var resultado = _clan.RegistrarAspirante(
            nombre, alias, discord, edad, horasMilsim, faccionPA2, disponibilidadPA2,
            experienciaMilsim, experienciaRP, plataformas ?? new List<string>(),
            servidoresAnteriores, especialidades ?? new List<string>(),
            descripcionPersonal, antecedentesRP);

        if (resultado.Exito)
            TempData["ToastMensaje"] = resultado.Mensaje;
        else
            TempData["ToastError"] = resultado.Mensaje;

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public IActionResult Evaluar(string idSolicitud, string nuevoEstado, string etapa, string observaciones, string entrevista, string decision)
    {
        var ok = _clan.EvaluarSolicitud(idSolicitud, nuevoEstado, etapa, observaciones, entrevista, decision);
        if (ok)
            TempData["ToastMensaje"] = $"Solicitud {idSolicitud} actualizada a estado: {nuevoEstado} ({etapa}).";
        else
            TempData["ToastError"] = $"No se pudo aprobar la solicitud {idSolicitud}: verifique el requisito obligatorio de mayoría de edad (+18).";

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public IActionResult Incorporar(
        string idSolicitud, string rangoInicial, List<string>? departamentos,
        List<string>? unidades, List<string>? especialidades)
    {
        var incorporado = _clan.IncorporarAspiranteAprobado(
            idSolicitud,
            rangoInicial,
            departamentos ?? new List<string> { "Operaciones" },
            unidades ?? new List<string> { "UE-001" },
            especialidades ?? new List<string> { "Infantería" });

        if (incorporado == null)
        {
            TempData["ToastError"] = "No se pudo completar la incorporación. Verifique que el aspirante tenga al menos 18 años cumplidos.";
            return RedirectToAction(nameof(Index));
        }

        TempData["ToastMensaje"] = $"¡Incorporación completada! Se emitió el expediente {incorporado.CodigoCE} para {incorporado.Alias}.";
        return RedirectToAction(nameof(Ceremonia), new { id = incorporado.CodigoCE });
    }

    // Ceremonia Digital de Incorporación: /Reclutamiento/Ceremonia/CE-016
    public IActionResult Ceremonia(string id)
    {
        var integrante = _clan.Integrantes.FirstOrDefault(i => i.CodigoCE == id)
                         ?? _clan.Integrantes.Last();
        return View(integrante);
    }
}
