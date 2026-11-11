using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ClanExiliados_Web.Services;

namespace ClanExiliados_Web.Controllers;

// SECCIONES 10, 11, 12 Y 13: CENTRO DE OPERACIONES, CALENDARIO, ENTRENAMIENTOS Y SERVIDORES RP
public class OperacionesController : Controller
{
    private readonly ClanInstitutionalService _clan;

    public OperacionesController(ClanInstitutionalService clan)
    {
        _clan = clan;
    }

    // Centro de Operaciones: /Operaciones
    public IActionResult Index(string? estado, string? plataforma)
    {
        ViewData["FiltroEstado"] = estado ?? "";
        ViewData["FiltroPlataforma"] = plataforma ?? "";
        return View(_clan);
    }

    [HttpPost]
    public IActionResult CrearOperacion(
        string nombre, string plataforma, DateTime fecha, string horario,
        string departamento, List<string>? unidades, string responsable,
        string descripcion, string contexto)
    {
        if (string.IsNullOrWhiteSpace(nombre))
        {
            TempData["ToastError"] = "Debe indicar el nombre de la operación institucional.";
            return RedirectToAction(nameof(Index));
        }

        var op = _clan.CrearOperacion(
            nombre, plataforma, fecha == default ? DateTime.Today.AddDays(3) : fecha,
            string.IsNullOrWhiteSpace(horario) ? "20:00 - 22:00" : horario,
            departamento, unidades ?? new List<string> { "UE-002" },
            string.IsNullOrWhiteSpace(responsable) ? "CE-001 · Paquito (Capitán)" : responsable,
            descripcion, contexto);

        TempData["ToastMensaje"] = $"Operación {op.Id} «{op.Nombre}» creada y sincronizada con el Calendario Institucional.";
        return RedirectToAction(nameof(Index));
    }

    // Calendario Institucional (Mensual, Semanal, Diaria + Simultaneidad): /Operaciones/Calendario
    public IActionResult Calendario(string? vista, string? plataforma, string? departamento, string? unidad, string? estado)
    {
        ViewData["VistaCalendario"] = string.IsNullOrWhiteSpace(vista) ? "mensual" : vista.ToLowerInvariant();
        ViewData["FiltroPlataforma"] = plataforma ?? "";
        ViewData["FiltroDepartamento"] = departamento ?? "";
        ViewData["FiltroUnidad"] = unidad ?? "";
        ViewData["FiltroEstado"] = estado ?? "";
        return View(_clan);
    }

    // Entrenamientos y Evaluaciones: /Operaciones/Entrenamientos
    public IActionResult Entrenamientos(string? categoria)
    {
        ViewData["FiltroCategoria"] = categoria ?? "";
        return View(_clan);
    }

    [HttpPost]
    public IActionResult CrearEntrenamiento(
        string nombre, string categoria, string plataforma, DateTime fecha,
        string horario, string instructor, List<string>? unidades, string descripcion)
    {
        if (string.IsNullOrWhiteSpace(nombre))
        {
            TempData["ToastError"] = "Debe indicar el nombre del entrenamiento.";
            return RedirectToAction(nameof(Entrenamientos));
        }

        var ent = _clan.CrearEntrenamiento(
            nombre, categoria, plataforma, fecha == default ? DateTime.Today.AddDays(2) : fecha,
            string.IsNullOrWhiteSpace(horario) ? "19:00 - 20:30" : horario,
            string.IsNullOrWhiteSpace(instructor) ? "CE-002 · Wolf_Alpha (Sargento)" : instructor,
            unidades ?? new List<string> { "UE-001" }, descripcion);

        TempData["ToastMensaje"] = $"Entrenamiento {ent.Id} «{ent.Nombre}» programado correctamente.";
        return RedirectToAction(nameof(Entrenamientos));
    }

    // Directorio de Plataformas, Servidores y Antecedentes RP: /Operaciones/Servidores
    public IActionResult Servidores(string? plataforma, string? categoriaAntecedente)
    {
        ViewData["FiltroPlataforma"] = plataforma ?? "";
        ViewData["FiltroCategoriaAntecedente"] = categoriaAntecedente ?? "";
        return View(_clan);
    }
}
