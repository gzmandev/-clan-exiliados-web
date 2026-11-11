using System.Text;
using Microsoft.AspNetCore.Mvc;
using ClanExiliados_Web.Services;

namespace ClanExiliados_Web.Controllers;

// SECCIONES 25 Y 26: ANALÍTICA, REPORTES (REP-001 A REP-008) Y CONFIGURACIÓN INSTITUCIONAL
public class AnaliticaController : Controller
{
    private readonly ClanInstitutionalService _clan;

    public AnaliticaController(ClanInstitutionalService clan)
    {
        _clan = clan;
    }

    // Centro de Estadísticas y Reportes: /Analitica
    public IActionResult Index(string? periodo, string? departamento, string? plataforma)
    {
        ViewData["FiltroPeriodo"] = periodo ?? "Trimestre Actual";
        ViewData["FiltroDepartamento"] = departamento ?? "";
        ViewData["FiltroPlataforma"] = plataforma ?? "";
        _clan.ActualizarCatalogoReportes();
        return View(_clan);
    }

    // Exportación demostrativa funcional a CSV compatible con Excel (UTF-8 con BOM)
    public IActionResult ExportarExcel(string id)
    {
        var sb = new StringBuilder();
        var codigo = string.IsNullOrWhiteSpace(id) ? "REP-001" : id.ToUpperInvariant();

        switch (codigo)
        {
            case "REP-001":
                sb.AppendLine("CodigoCE;NumeroPA2;Alias;Nombre;Rango;FaccionPA2;Departamentos;Unidades;HorasMILSIM;EstadoInstitucional;Disponibilidad;Discord");
                foreach (var i in _clan.Integrantes)
                    sb.AppendLine($"{i.CodigoCE};{i.NumeroPA2};{i.Alias};{i.Nombre};{i.Rango};{i.FaccionPA2};{string.Join(",", i.Departamentos)};{string.Join(",", i.Unidades)};{i.HorasMilsim};{i.EstadoInstitucional};{i.Disponibilidad};{i.Discord}");
                break;

            case "REP-002":
                sb.AppendLine("IdOperacion;Nombre;Plataforma;Fecha;Horario;Departamento;Unidades;Responsable;Estado;TotalConvocados");
                foreach (var op in _clan.Operaciones)
                    sb.AppendLine($"{op.Id};{op.Nombre};{op.Plataforma};{op.Fecha:dd/MM/yyyy};{op.Horario};{op.DepartamentoResponsable};{string.Join(",", op.Unidades)};{op.Responsable};{op.Estado};{op.Convocados.Count}");
                break;

            case "REP-003":
                sb.AppendLine("IdEntrenamiento;Nombre;Categoria;Plataforma;Fecha;Horario;Instructor;Estado;TotalEvaluados");
                foreach (var e in _clan.Entrenamientos)
                    sb.AppendLine($"{e.Id};{e.Nombre};{e.Categoria};{e.Plataforma};{e.Fecha:dd/MM/yyyy};{e.Horario};{e.Instructor};{e.Estado};{e.Convocados.Count}");
                break;

            case "REP-004":
                sb.AppendLine("Tipo;Codigo;Nombre;Responsable_O_Lider;Estado;EfectivosAsignados");
                foreach (var d in _clan.Departamentos)
                    sb.AppendLine($"Departamento;{d.Id};{d.Nombre};{d.Responsable};{d.Estado};{d.IntegrantesCodigos.Count}");
                foreach (var u in _clan.Unidades)
                    sb.AppendLine($"Unidad;{u.Id};{u.Nombre};{u.Lider};{u.Estado};{u.IntegrantesCodigos.Count}");
                break;

            case "REP-005":
                sb.AppendLine("IdReconocimiento;Nombre;Categoria;BeneficiarioCE;Alias;Fecha;Autoridad;Estado");
                foreach (var r in _clan.Reconocimientos)
                    sb.AppendLine($"{r.Id};{r.Nombre};{r.Categoria};{r.BeneficiarioCodigoCE};{r.BeneficiarioAlias};{r.Fecha:dd/MM/yyyy};{r.AutoridadOtorgante};{r.Estado}");
                break;

            case "REP-006":
                sb.AppendLine("IdAscenso;IntegranteCE;Alias;RangoActual;RangoPropuesto;Proponente;Fecha;Estado");
                foreach (var a in _clan.Ascensos)
                    sb.AppendLine($"{a.Id};{a.IntegranteCodigoCE};{a.IntegranteAlias};{a.RangoActual};{a.RangoPropuesto};{a.Proponente};{a.Fecha:dd/MM/yyyy};{a.Estado}");
                break;

            case "REP-007":
                sb.AppendLine("IdArchivo;FechaHora;Tipo;IntegranteCE;Alias;Departamento;Unidad;Descripcion;Autoridad");
                foreach (var ar in _clan.Archivo)
                    sb.AppendLine($"{ar.Id};{ar.Fecha:dd/MM/yyyy HH:mm};{ar.Tipo};{ar.IntegranteCodigoCE};{ar.IntegranteAlias};{ar.Departamento};{ar.Unidad};{ar.Descripcion};{ar.Autoridad}");
                break;

            default:
                sb.AppendLine("IdServidor;Nombre;Plataforma;ComunidadRP;Modalidad;Responsable;Estado;Integrantes");
                foreach (var s in _clan.Servidores)
                    sb.AppendLine($"{s.Id};{s.Nombre};{s.Plataforma};{s.ComunidadUniversoRP};{s.Modalidad};{s.Responsable};{s.Estado};{s.IntegrantesCodigos.Count}");
                break;
        }

        var preamble = Encoding.UTF8.GetPreamble();
        var body = Encoding.UTF8.GetBytes(sb.ToString());
        var bytes = new byte[preamble.Length + body.Length];
        System.Buffer.BlockCopy(preamble, 0, bytes, 0, preamble.Length);
        System.Buffer.BlockCopy(body, 0, bytes, preamble.Length, body.Length);

        return File(bytes, "text/csv; charset=utf-8", $"ClanExiliados_{codigo}_{System.DateTime.Today:yyyyMMdd}.csv");
    }

    // Centro de Configuración Institucional: /Analitica/Configuracion
    public IActionResult Configuracion()
    {
        return View(_clan);
    }

    [HttpPost]
    public IActionResult GuardarConfiguracion(string lema, string canalDiscord, bool alertasActivas, bool simultaneidadActiva)
    {
        _clan.ActualizarConfiguracion(lema, canalDiscord, alertasActivas, simultaneidadActiva);
        TempData["ToastMensaje"] = "Configuración institucional actualizada y registrada en el módulo de Auditoría.";
        return RedirectToAction(nameof(Configuracion));
    }
}
