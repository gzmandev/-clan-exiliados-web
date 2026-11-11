using System;
using System.Collections.Generic;

namespace ClanExiliados_Web.Models;

// ============================================================================
// 7.1 RANGOS OFICIALES (8 RANGOS)
// ============================================================================
public class RangoOficial
{
    public int Orden { get; set; }
    public string Codigo { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public string NivelJerarquico { get; set; } = string.Empty;
    public string EmblemaTipo { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public List<string> Responsabilidades { get; set; } = new();
    public List<string> Facultades { get; set; } = new();
    public string RangoSuperior { get; set; } = string.Empty;
    public string RangoInferior { get; set; } = string.Empty;
}

// ============================================================================
// 7.2 DEPARTAMENTOS (5 DEPARTAMENTOS)
// ============================================================================
public class DepartamentoInstitucional
{
    public string Id { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public string Emblema { get; set; } = string.Empty;
    public string ColorReferencia { get; set; } = string.Empty;
    public string ClaseCss { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public string Responsable { get; set; } = string.Empty;
    public List<string> IntegrantesCodigos { get; set; } = new();
    public string Estado { get; set; } = "Activo"; // Activo / Inactivo
    public List<string> Actividades { get; set; } = new();
}

// ============================================================================
// 7.3 UNIDADES INTERNAS (UE-001...)
// ============================================================================
public class UnidadTactica
{
    public string Id { get; set; } = string.Empty; // UE-001
    public string Nombre { get; set; } = string.Empty;
    public string Emblema { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public string DepartamentoAsociado { get; set; } = string.Empty;
    public string Lider { get; set; } = string.Empty;
    public List<string> IntegrantesCodigos { get; set; } = new();
    public DateTime FechaCreacion { get; set; }
    public string Estado { get; set; } = "Activo";
    public List<string> Historial { get; set; } = new();
    public List<string> Actividades { get; set; } = new();
}

// ============================================================================
// 9. PERSONAL, EXPEDIENTES Y CREDENCIALES (CE-###)
// ============================================================================
public class IntegranteCE
{
    public int NumeroPA2 { get; set; }
    public string CodigoCE { get; set; } = string.Empty; // CE-001
    public string Nombre { get; set; } = string.Empty;
    public string Alias { get; set; } = string.Empty; // NombreJugador en PA2
    public string Discord { get; set; } = string.Empty;
    public int Edad { get; set; }
    public string Rango { get; set; } = string.Empty;
    public string FaccionPA2 { get; set; } = string.Empty; // Militares, Mercenarios, Terroristas
    public string Faccion { get => FaccionPA2; set => FaccionPA2 = value; }
    public string TurnoDisponibilidadPA2 { get; set; } = string.Empty; // 24/7, Noches, Fines de semana
    public string TurnoDisponibilidad { get => TurnoDisponibilidadPA2; set => TurnoDisponibilidadPA2 = value; }
    public List<string> Departamentos { get; set; } = new();
    public List<string> Unidades { get; set; } = new();
    public DateTime FechaIngreso { get; set; }
    public List<string> Especialidades { get; set; } = new();
    public List<string> InsigniasAdministrativas { get; set; } = new();
    public int HorasMilsim { get; set; }
    public string ExperienciaMilsim { get; set; } = string.Empty;
    public string ExperienciaRP { get; set; } = string.Empty;
    public List<AntecedenteRP> AntecedentesRP { get; set; } = new();
    public string Observaciones { get; set; } = string.Empty;
    public string EstadoInstitucional { get; set; } = "Activo"; // Activo, En reserva, Suspendido, Retirado
    public string Disponibilidad { get; set; } = "Disponible"; // Disponible, Ocupado, Desconectado, No disponible
    public List<string> HistorialInstitucional { get; set; } = new();
    public string SupervisorCodigoCE { get; set; } = string.Empty;
}

// ============================================================================
// 10. ANTECEDENTES RP Y SERVIDORES (SRV-001...)
// ============================================================================
public class ServidorPlataforma
{
    public string Id { get; set; } = string.Empty; // SRV-001
    public string Nombre { get; set; } = string.Empty;
    public string Plataforma { get; set; } = string.Empty; // Arma 3, Arma Reforger, GTA V / FiveM
    public string ComunidadUniversoRP { get; set; } = string.Empty;
    public string Modalidad { get; set; } = string.Empty; // MILSIM, Roleplay
    public string Descripcion { get; set; } = string.Empty;
    public string Responsable { get; set; } = string.Empty;
    public List<string> IntegrantesCodigos { get; set; } = new();
    public List<string> Unidades { get; set; } = new();
    public DateTime InicioParticipacion { get; set; }
    public string Estado { get; set; } = "Activo"; // Activo, Inactivo, Histórico
    public string Enlace { get; set; } = string.Empty;
    public List<string> Historial { get; set; } = new();
}

public class AntecedenteRP
{
    public string Id { get; set; } = string.Empty;
    public string CodigoCE { get; set; } = string.Empty;
    public string AliasIntegrante { get; set; } = string.Empty;
    public string Plataforma { get; set; } = string.Empty;
    public string Servidor { get; set; } = string.Empty;
    public string Comunidad { get; set; } = string.Empty;
    public string PersonajeOAlias { get; set; } = string.Empty;
    public string CategoriaAntecedente { get; set; } = "Militares"; // Militares, Mercenarios, Terroristas (Ficticio RP)
    public string TipoRolInterpretado { get; set; } = string.Empty;
    public string Periodo { get; set; } = string.Empty;
    public string Experiencia { get; set; } = string.Empty;
    public string Responsabilidades { get; set; } = string.Empty;
    public string Actividades { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public string Observaciones { get; set; } = string.Empty;
}

// ============================================================================
// 8. RECLUTAMIENTO E INCORPORACIÓN
// ============================================================================
public class SolicitudReclutamiento
{
    public string Id { get; set; } = string.Empty; // ADM-001
    public int NumeroPA2 { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Alias { get; set; } = string.Empty;
    public string Discord { get; set; } = string.Empty;
    public int Edad { get; set; }
    public int HorasMilsim { get; set; }
    public string RangoDeseadoPA2 { get; set; } = string.Empty;
    public string FaccionPA2 { get; set; } = string.Empty;
    public string Faccion { get => FaccionPA2; set => FaccionPA2 = value; }
    public string DisponibilidadPA2 { get; set; } = string.Empty;
    public string Disponibilidad { get => DisponibilidadPA2; set => DisponibilidadPA2 = value; }
    public string ExperienciaMilsim { get; set; } = string.Empty;
    public string ExperienciaRoleplay { get; set; } = string.Empty;
    public List<string> Plataformas { get; set; } = new();
    public string ServidoresAnteriores { get; set; } = string.Empty;
    public List<string> Especialidades { get; set; } = new();
    public string DescripcionPersonal { get; set; } = string.Empty;
    public string AntecedentesRP { get; set; } = string.Empty;
    public DateTime FechaSolicitud { get; set; }
    public string EtapaFlujo { get; set; } = "Evaluación"; // Registro, Verificación de edad, Evaluación, Entrevista, Decisión
    public string EstadoAdmision { get; set; } = "Pendiente"; // Pendiente, En revisión, Aprobada, Rechazada
    public string CriteriosEvaluacion { get; set; } = string.Empty;
    public string ObservacionesEvaluacion { get; set; } = string.Empty;
    public string EntrevistaFicticia { get; set; } = string.Empty;
    public string EvaluacionExperiencia { get; set; } = string.Empty;
    public string DecisionInstitucional { get; set; } = string.Empty;
    public bool Incorporado { get; set; }
    public string? CodigoCEAsignado { get; set; }
}

// ============================================================================
// 11. CENTRO DE OPERACIONES (OP-001...)
// ============================================================================
public class ConvocatoriaParticipante
{
    public string CodigoCE { get; set; } = string.Empty;
    public string Alias { get; set; } = string.Empty;
    public string EstadoConvocatoria { get; set; } = "Convocado"; // Convocado, Confirmado, No participará, Pendiente de respuesta
}

public class OperacionInstitucional
{
    public string Id { get; set; } = string.Empty; // OP-001
    public string Nombre { get; set; } = string.Empty;
    public string Plataforma { get; set; } = string.Empty;
    public DateTime Fecha { get; set; }
    public string Horario { get; set; } = string.Empty;
    public string DepartamentoResponsable { get; set; } = string.Empty;
    public List<string> Unidades { get; set; } = new();
    public string Responsable { get; set; } = string.Empty;
    public List<ConvocatoriaParticipante> Convocados { get; set; } = new();
    public string Descripcion { get; set; } = string.Empty;
    public string ContextoFicticio { get; set; } = string.Empty;
    public string Estado { get; set; } = "Programada"; // Programada, En curso, Finalizada, Cancelada
}

// ============================================================================
// 13. ENTRENAMIENTOS Y EVALUACIONES (ENT-001...)
// ============================================================================
public class EvaluacionAsistenteEntrenamiento
{
    public string CodigoCE { get; set; } = string.Empty;
    public string Alias { get; set; } = string.Empty;
    public string Asistencia { get; set; } = "Asistió"; // Asistió, Ausente, Justificado, Llegada tardía
    public int RendimientoGeneral { get; set; } = 90;
    public int Comunicacion { get; set; } = 90;
    public int Coordinacion { get; set; } = 88;
    public int TrabajoEnEquipo { get; set; } = 92;
    public int CumplimientoObjetivos { get; set; } = 90;
    public string Observaciones { get; set; } = string.Empty;
    public string Resultado { get; set; } = "Aprobado"; // Aprobado, En proceso, No aprobado
}

public class EntrenamientoInstitucional
{
    public string Id { get; set; } = string.Empty; // ENT-001
    public string Nombre { get; set; } = string.Empty;
    public string Categoria { get; set; } = string.Empty; // MILSIM, Coordinación, Comunicación, Roleplay, Formación interna
    public string Plataforma { get; set; } = string.Empty;
    public DateTime Fecha { get; set; }
    public string Horario { get; set; } = string.Empty;
    public string Instructor { get; set; } = string.Empty;
    public List<string> Unidades { get; set; } = new();
    public List<EvaluacionAsistenteEntrenamiento> Convocados { get; set; } = new();
    public string Descripcion { get; set; } = string.Empty;
    public string Estado { get; set; } = "Programado"; // Programado, En curso, Finalizado
}

// ============================================================================
// 14. RECONOCIMIENTOS (REC-001...), INSIGNIAS Y ASCENSOS (ASC-001...)
// ============================================================================
public class ReconocimientoInstitucional
{
    public string Id { get; set; } = string.Empty; // REC-001
    public string Nombre { get; set; } = string.Empty;
    public string Categoria { get; set; } = string.Empty; // Mérito institucional, Servicio y permanencia, Liderazgo, Desempeño y formación, Distinción especial
    public string Emblema { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public string Motivo { get; set; } = string.Empty;
    public string BeneficiarioCodigoCE { get; set; } = string.Empty;
    public string BeneficiarioAlias { get; set; } = string.Empty;
    public DateTime Fecha { get; set; }
    public string AutoridadOtorgante { get; set; } = string.Empty;
    public string Estado { get; set; } = "Vigente"; // Vigente, Revocado
}

public class InsigniaEspecialidadAdmin
{
    public string Id { get; set; } = string.Empty; // INS-001
    public string Nombre { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty; // Especialidad / Insignia Táctica
    public string IntegranteCodigoCE { get; set; } = string.Empty;
    public string IntegranteAlias { get; set; } = string.Empty;
    public string Justificacion { get; set; } = string.Empty;
    public string AutoridadAsignadora { get; set; } = string.Empty;
    public DateTime Fecha { get; set; }
    public string Estado { get; set; } = "Activa"; // Activa, Revocada
}

public class PropuestaAscenso
{
    public string Id { get; set; } = string.Empty; // ASC-001
    public string IntegranteCodigoCE { get; set; } = string.Empty;
    public string IntegranteAlias { get; set; } = string.Empty;
    public string RangoActual { get; set; } = string.Empty;
    public string RangoPropuesto { get; set; } = string.Empty;
    public string Proponente { get; set; } = string.Empty;
    public string Justificacion { get; set; } = string.Empty;
    public string Evaluaciones { get; set; } = string.Empty;
    public DateTime Fecha { get; set; }
    public string AutoridadAprobadora { get; set; } = string.Empty;
    public string Estado { get; set; } = "Pendiente"; // Pendiente, Aprobada, Rechazada
}

// ============================================================================
// 15. GESTIÓN DISCIPLINARIA (DIS-001...)
// ============================================================================
public class RegistroDisciplinario
{
    public string Id { get; set; } = string.Empty; // DIS-001
    public string IntegranteCodigoCE { get; set; } = string.Empty;
    public string IntegranteAlias { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty; // Llamada de atención, Advertencia formal, Suspensión temporal, Resolución administrativa, Rehabilitación
    public string Motivo { get; set; } = string.Empty;
    public DateTime Fecha { get; set; }
    public string Responsable { get; set; } = string.Empty;
    public string EvidenciaDescriptiva { get; set; } = string.Empty;
    public string Duracion { get; set; } = string.Empty;
    public string Estado { get; set; } = "Vigente"; // Pendiente, Vigente, Cumplida, Revocada
    public string Observaciones { get; set; } = string.Empty;
}

// ============================================================================
// 16. ARCHIVO INSTITUCIONAL Y REGISTRO DE AUDITORÍA
// ============================================================================
public class EntradaArchivoInstitucional
{
    public string Id { get; set; } = string.Empty; // ARC-001
    public DateTime Fecha { get; set; }
    public string Tipo { get; set; } = string.Empty; // Incorporación, Alta, Ascenso, Cambio de rango, Reconocimiento, Disciplina, etc.
    public string IntegranteCodigoCE { get; set; } = string.Empty;
    public string IntegranteAlias { get; set; } = string.Empty;
    public string Departamento { get; set; } = string.Empty;
    public string Unidad { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public string Autoridad { get; set; } = string.Empty;
}

public class RegistroAuditoria
{
    public string Id { get; set; } = string.Empty; // AUD-001
    public string UsuarioEjecutor { get; set; } = string.Empty;
    public string RegistroModificado { get; set; } = string.Empty;
    public string ValorAnterior { get; set; } = string.Empty;
    public string ValorNuevo { get; set; } = string.Empty;
    public DateTime FechaHora { get; set; }
    public string Motivo { get; set; } = string.Empty;
    public string TipoOperacion { get; set; } = string.Empty;
}

// ============================================================================
// 17. COMUNICACIONES INSTITUCIONALES (COM-001...)
// ============================================================================
public class ComunicadoInstitucional
{
    public string Id { get; set; } = string.Empty; // COM-001
    public string Titulo { get; set; } = string.Empty;
    public string Contenido { get; set; } = string.Empty;
    public string Categoria { get; set; } = string.Empty;
    public string Prioridad { get; set; } = "Informativo"; // Informativo, Importante, Urgente
    public string Autor { get; set; } = string.Empty;
    public string Departamento { get; set; } = string.Empty;
    public DateTime FechaHora { get; set; }
    public string Destinatarios { get; set; } = "Toda la institución"; // Toda la institución, Departamentos, Unidades, Integrantes específicos
    public string Estado { get; set; } = "Publicado"; // Borrador, Publicado, Archivado
    public bool LeidoDemostrativo { get; set; }
}

// ============================================================================
// 18. CENTRO DOCUMENTAL (DOC-001...)
// ============================================================================
public class DocumentoInstitucional
{
    public string Id { get; set; } = string.Empty; // DOC-001
    public string Titulo { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public string Categoria { get; set; } = string.Empty; // Reglamentos, Rangos, Departamentos, Unidades, Reclutamiento, Incorporación, Entrenamientos, Archivo histórico
    public List<string> Etiquetas { get; set; } = new();
    public string Autor { get; set; } = string.Empty;
    public string Responsable { get; set; } = string.Empty;
    public DateTime FechaPublicacion { get; set; }
    public DateTime FechaActualizacion { get; set; }
    public string NivelAcceso { get; set; } = "General";
    public string Estado { get; set; } = "Publicado"; // Borrador, Publicado, Archivado
    public string VistaPreviaContenido { get; set; } = string.Empty;
}

// ============================================================================
// 22 & 23. NOTIFICACIONES, ALERTAS DINÁMICAS Y ACTIVITY FEED
// ============================================================================
public class NotificacionSistema
{
    public string Id { get; set; } = string.Empty; // NOT-001
    public string Categoria { get; set; } = string.Empty; // Solicitudes, Evaluaciones, Incorporaciones, Actividades, Ascensos, Comunicados, Convocatorias
    public string Prioridad { get; set; } = "Normal"; // Normal, Importante, Urgente
    public string Titulo { get; set; } = string.Empty;
    public string Mensaje { get; set; } = string.Empty;
    public DateTime FechaHora { get; set; }
    public bool Leida { get; set; }
    public string EnlaceUrl { get; set; } = "/";
}

public class AlertaDinamica
{
    public string Id { get; set; } = string.Empty; // ALT-001
    public string Nivel { get; set; } = "Importante"; // Crítica, Importante, Informativa
    public string Titulo { get; set; } = string.Empty;
    public string Detalle { get; set; } = string.Empty;
    public string ModuloOrigen { get; set; } = string.Empty;
    public string EnlaceAccion { get; set; } = "/";
    public bool Atendida { get; set; }
    public DateTime FechaGeneracion { get; set; }
}

public class EventoActivityFeed
{
    public string Id { get; set; } = string.Empty; // ACT-001
    public string Icono { get; set; } = "shield";
    public string Tipo { get; set; } = string.Empty; // Integración, Ascenso, Reconocimiento, Operación, Entrenamiento, Comunicado, Disponibilidad, Administración
    public string Descripcion { get; set; } = string.Empty;
    public string Responsable { get; set; } = string.Empty;
    public DateTime FechaHora { get; set; }
    public string EnlaceRegistro { get; set; } = "/";
}

// ============================================================================
// 25 & 26. REPORTES (REP-001..REP-008) Y CONFIGURACIÓN INSTITUCIONAL
// ============================================================================
public class ReporteDefinicion
{
    public string Id { get; set; } = string.Empty; // REP-001..REP-008
    public string Titulo { get; set; } = string.Empty;
    public string Categoria { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public string Responsable { get; set; } = string.Empty;
    public int TotalRegistros { get; set; }
}

public class ConfiguracionInstitucional
{
    public string NombreInstitucional { get; set; } = "Clan Exiliados";
    public string LemaInstitucional { get; set; } = "Disciplina, Coordinación Táctica y Honor Operativo";
    public string Monograma { get; set; } = "CE";
    public string PaletaActiva { get; set; } = "Tactical HUD — Carbón, Grafito y Verde Militar (#228B22)";
    public string TipografiaTitulos { get; set; } = "Rajdhani (Tactical HUD Display)";
    public string TipografiaTextos { get; set; } = "Inter / Segoe UI";
    public string CanalContactoDiscord { get; set; } = "discord.gg/clanexiliados-milsim";
    public bool AlertasTiempoRealActivas { get; set; } = true;
    public bool DeteccionSimultaneidadCalendario { get; set; } = true;
    public bool MostrarEmblemasRangos { get; set; } = true;
    public string VersionPlataforma { get; set; } = "v3.4 Tactical HUD (PA3 MVC)";
}
