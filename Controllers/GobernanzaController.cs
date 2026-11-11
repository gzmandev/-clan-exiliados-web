using Microsoft.AspNetCore.Mvc;
using ClanExiliados_Web.Services;

namespace ClanExiliados_Web.Controllers;

// SECCIONES 14, 15 Y 16: ASCENSOS, RECONOCIMIENTOS, INSIGNIAS, DISCIPLINA, ARCHIVO Y AUDITORÍA
public class GobernanzaController : Controller
{
    private readonly ClanInstitutionalService _clan;

    public GobernanzaController(ClanInstitutionalService clan)
    {
        _clan = clan;
    }

    // Ascensos, Reconocimientos e Insignias: /Gobernanza/Ascensos
    public IActionResult Ascensos()
    {
        return View(_clan);
    }

    public IActionResult Reconocimientos()
    {
        return RedirectToAction(nameof(Ascensos));
    }

    [HttpPost]
    public IActionResult ProponerAscenso(string codigoCE, string rangoPropuesto, string proponente, string justificacion, string evaluaciones)
    {
        if (string.IsNullOrWhiteSpace(justificacion))
        {
            TempData["ToastError"] = "Debe ingresar la justificación de la propuesta de ascenso.";
            return RedirectToAction(nameof(Ascensos));
        }

        var asc = _clan.CrearPropuestaAscenso(codigoCE, rangoPropuesto, proponente, justificacion, evaluaciones);
        TempData["ToastMensaje"] = $"Propuesta de ascenso {asc.Id} registrada en estado Pendiente.";
        return RedirectToAction(nameof(Ascensos));
    }

    [HttpPost]
    public IActionResult ResolverAscenso(string idAscenso, bool aprobar, string motivo)
    {
        var ok = _clan.ResolverPropuestaAscenso(idAscenso, aprobar, motivo);
        if (ok)
        {
            TempData["ToastMensaje"] = aprobar
                ? $"Ascenso {idAscenso} APROBADO: se actualizaron el rango del integrante, su credencial, el Archivo Institucional y Auditoría."
                : $"Propuesta de ascenso {idAscenso} marcada como RECHAZADA.";
        }
        return RedirectToAction(nameof(Ascensos));
    }

    [HttpPost]
    public IActionResult OtorgarReconocimiento(string codigoCE, string nombre, string categoria, string motivo, string descripcion)
    {
        if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(motivo))
        {
            TempData["ToastError"] = "Debe indicar el nombre del reconocimiento y su motivo.";
            return RedirectToAction(nameof(Ascensos));
        }

        var rec = _clan.OtorgarReconocimiento(codigoCE, nombre, categoria, motivo, descripcion);
        TempData["ToastMensaje"] = $"Reconocimiento {rec.Id} «{rec.Nombre}» otorgado y registrado en el expediente {rec.BeneficiarioCodigoCE}.";
        return RedirectToAction(nameof(Ascensos));
    }

    // Gestión Disciplinaria (restringida visualmente a Dirección y Mando): /Gobernanza/Disciplina
    public IActionResult Disciplina()
    {
        return View(_clan);
    }

    [HttpPost]
    public IActionResult RegistrarDisciplina(string codigoCE, string tipo, string motivo, string evidencia, string duracion, string observaciones)
    {
        if (string.IsNullOrWhiteSpace(motivo))
        {
            TempData["ToastError"] = "Debe ingresar el motivo de la medida disciplinaria.";
            return RedirectToAction(nameof(Disciplina));
        }

        var dis = _clan.RegistrarMedidaDisciplinaria(codigoCE, tipo, motivo, evidencia, duracion, observaciones);
        TempData["ToastMensaje"] = $"Registro disciplinario {dis.Id} ({dis.Tipo}) asentado en el Archivo Institucional.";
        return RedirectToAction(nameof(Disciplina));
    }

    // Archivo Institucional y Registro de Auditoría: /Gobernanza/Archivo
    public IActionResult Archivo(string? pestana, string? tipo, string? departamento, string? unidad, string? q)
    {
        ViewData["PestanaActiva"] = string.IsNullOrWhiteSpace(pestana) ? "archivo" : pestana.ToLowerInvariant();
        ViewData["FiltroTipo"] = tipo ?? "";
        ViewData["FiltroDepartamento"] = departamento ?? "";
        ViewData["FiltroUnidad"] = unidad ?? "";
        ViewData["FiltroQ"] = q ?? "";
        return View(_clan);
    }
}
