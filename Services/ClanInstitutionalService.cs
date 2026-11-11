using System;
using System.Collections.Generic;
using System.Linq;
using ClanExiliados_Web.Models;

namespace ClanExiliados_Web.Services;

public class ClanInstitutionalService
{
    private readonly object _lock = new();

    public string PerfilVisualActual { get; private set; } = "Dirección"; // Dirección, Mando, Responsables, Integrantes
    public ConfiguracionInstitucional Configuracion { get; private set; } = new();

    public List<RangoOficial> Rangos { get; private set; } = new();
    public List<DepartamentoInstitucional> Departamentos { get; private set; } = new();
    public List<UnidadTactica> Unidades { get; private set; } = new();
    public List<IntegranteCE> Integrantes { get; private set; } = new();
    public List<SolicitudReclutamiento> SolicitudesReclutamiento { get; private set; } = new();
    public List<ServidorPlataforma> Servidores { get; private set; } = new();
    public List<OperacionInstitucional> Operaciones { get; private set; } = new();
    public List<EntrenamientoInstitucional> Entrenamientos { get; private set; } = new();
    public List<ReconocimientoInstitucional> Reconocimientos { get; private set; } = new();
    public List<InsigniaEspecialidadAdmin> InsigniasAdmin { get; private set; } = new();
    public List<PropuestaAscenso> Ascensos { get; private set; } = new();
    public List<RegistroDisciplinario> Disciplina { get; private set; } = new();
    public List<EntradaArchivoInstitucional> Archivo { get; private set; } = new();
    public List<RegistroAuditoria> Auditoria { get; private set; } = new();
    public List<ComunicadoInstitucional> Comunicaciones { get; private set; } = new();
    public List<DocumentoInstitucional> Documentos { get; private set; } = new();
    public List<NotificacionSistema> Notificaciones { get; private set; } = new();
    public List<AlertaDinamica> Alertas { get; private set; } = new();
    public List<EventoActivityFeed> ActivityFeed { get; private set; } = new();
    public List<ReporteDefinicion> Reportes { get; private set; } = new();

    public ClanInstitutionalService()
    {
        InicializarDatosDemostrativos();
    }

    public void CambiarPerfilVisual(string nuevoPerfil)
    {
        var permitidos = new[] { "Dirección", "Mando", "Responsables", "Integrantes" };
        if (permitidos.Contains(nuevoPerfil))
        {
            lock (_lock)
            {
                PerfilVisualActual = nuevoPerfil;
            }
        }
    }

    private void InicializarDatosDemostrativos()
    {
        // ====================================================================
        // 1. LOS 8 RANGOS OFICIALES EXACTOS
        // ====================================================================
        Rangos = new List<RangoOficial>
        {
            new()
            {
                Orden = 1, Codigo = "RNG-01", Nombre = "Director/Fundador",
                NivelJerarquico = "Nivel 1 — Alta Dirección Institucional", EmblemaTipo = "estrella-triple-laurel",
                Descripcion = "Máxima autoridad institucional de Clan Exiliados. Define la doctrina general, estatutos y alianzas de simulación MILSIM y Roleplay.",
                Responsabilidades = new() { "Supervisión estratégica de los 5 departamentos", "Ratificación de directrices y reglamentos", "Aprobación final de altos mandos" },
                Facultades = new() { "Acceso total a Auditoría y Configuración", "Otorgamiento de Distinciones Especiales", "Resolución definitiva de expedientes" },
                RangoSuperior = "Ninguno (Cúpula Institucional)", RangoInferior = "Comandante"
            },
            new()
            {
                Orden = 2, Codigo = "RNG-02", Nombre = "Comandante",
                NivelJerarquico = "Nivel 2 — Estado Mayor y Comando", EmblemaTipo = "estrella-doble-escudo",
                Descripcion = "Jefe ejecutivo de operaciones conjuntas y coordinación interdepartamental.",
                Responsabilidades = new() { "Coordinación de los departamentos de Operaciones e Inteligencia", "Aprobación de ascensos de oficiales", "Supervisión disciplinaria institucional" },
                Facultades = new() { "Aprobar ascensos hasta Capitán", "Autorizar operaciones inter-servidor", "Asignar líderes de unidad" },
                RangoSuperior = "Director/Fundador", RangoInferior = "Capitán"
            },
            new()
            {
                Orden = 3, Codigo = "RNG-03", Nombre = "Capitán",
                NivelJerarquico = "Nivel 3 — Oficial Superior de Unidad", EmblemaTipo = "tres-barras-doradas",
                Descripcion = "Oficial al mando de una compañía táctica o dirección departamental.",
                Responsabilidades = new() { "Planificación de campañas MILSIM", "Dirección de unidades tácticas", "Evaluación de propuestas de ascenso" },
                Facultades = new() { "Convocar despliegues oficiales", "Proponer ascensos y reconocimientos", "Evaluar expedientes de reclutamiento" },
                RangoSuperior = "Comandante", RangoInferior = "Teniente"
            },
            new()
            {
                Orden = 4, Codigo = "RNG-04", Nombre = "Teniente",
                NivelJerarquico = "Nivel 4 — Oficial Táctico de Sección", EmblemaTipo = "dos-barras-doradas",
                Descripcion = "Oficial operativo encargado de la ejecución táctica en terreno y conducción de entrenamientos.",
                Responsabilidades = new() { "Liderazgo de sección en despliegues", "Instrucción avanzada de especialidades", "Supervisión de sargentos y cabos" },
                Facultades = new() { "Dirigir entrenamientos oficiales", "Emitir informes de misión", "Evaluar entrevistas de admisión" },
                RangoSuperior = "Capitán", RangoInferior = "Sargento"
            },
            new()
            {
                Orden = 5, Codigo = "RNG-05", Nombre = "Sargento",
                NivelJerarquico = "Nivel 5 — Suboficial Mayor de Escuadra", EmblemaTipo = "tres-galones",
                Descripcion = "Columna vertebral del mando táctico directo; coordina escuadras y cohesión de operadores.",
                Responsabilidades = new() { "Control directo de escuadra en misión", "Verificación de equipamiento y logística", "Tutoría de cabos y operadores" },
                Facultades = new() { "Liderar patrullas y escuadras", "Registrar asistencia en entrenamientos", "Solicitar soporte logístico" },
                RangoSuperior = "Teniente", RangoInferior = "Cabo"
            },
            new()
            {
                Orden = 6, Codigo = "RNG-06", Nombre = "Cabo",
                NivelJerarquico = "Nivel 6 — Suboficial de Equipo de Fuego", EmblemaTipo = "dos-galones",
                Descripcion = "Segundo al mando de escuadra y líder de binomio o equipo de fuego táctico.",
                Responsabilidades = new() { "Conducción de equipo de fuego (4 operadores)", "Apoyo directo al Sargento de escuadra", "Guía operativa de reclutas" },
                Facultades = new() { "Mando de escuadra en ausencia del Sargento", "Reporte táctico de campo" },
                RangoSuperior = "Sargento", RangoInferior = "Operador"
            },
            new()
            {
                Orden = 7, Codigo = "RNG-07", Nombre = "Operador",
                NivelJerarquico = "Nivel 7 — Especialista Operativo Certificado", EmblemaTipo = "un-galon-estrella",
                Descripcion = "Integrante de pleno derecho con instrucción básica superada y especialidad táctica activa (equivalente a Soldado operativo PA2).",
                Responsabilidades = new() { "Ejecución de roles especializados (Infantería, Piloto, Médico, Comunicaciones, Logística)", "Participación en despliegues y entrenamientos" },
                Facultades = new() { "Portar insignias de especialidad", "Integrar unidades tácticas permanentes" },
                RangoSuperior = "Cabo", RangoInferior = "Recluta"
            },
            new()
            {
                Orden = 8, Codigo = "RNG-08", Nombre = "Recluta",
                NivelJerarquico = "Nivel 8 — Aspirante Incorporado en Formación", EmblemaTipo = "escudo-base",
                Descripcion = "Integrante recién incorporado al clan que cursa su ciclo inicial de adaptación MILSIM y doctrina institucional.",
                Responsabilidades = new() { "Asistir a los entrenamientos de formación interna", "Cumplir el reglamento de comunicaciones y roleplay" },
                Facultades = new() { "Participar en operaciones bajo supervisión", "Acceso al Centro Documental de formación" },
                RangoSuperior = "Operador", RangoInferior = "Ninguno (Ingreso Inicial)"
            }
        };

        // ====================================================================
        // 2. LOS 5 DEPARTAMENTOS INSTITUCIONALES
        // ====================================================================
        Departamentos = new List<DepartamentoInstitucional>
        {
            new()
            {
                Id = "DEP-COM", Nombre = "Comando", Emblema = "crown", ColorReferencia = "Dorado (#D4A017)", ClaseCss = "dept-comando",
                Descripcion = "Dirección estratégica, jerarquía institucional, supervisión disciplinaria y coordinación general de Clan Exiliados.",
                Responsable = "CE-015 · Kodiak (Director/Fundador)",
                IntegrantesCodigos = new() { "CE-015", "CE-010", "CE-001" },
                Estado = "Activo",
                Actividades = new() { "Dirección y toma de decisiones institucionales", "Supervisión del escalafón jerárquico y ascensos", "Coordinación interdepartamental y auditoría" }
            },
            new()
            {
                Id = "DEP-OPE", Nombre = "Operaciones", Emblema = "crosshair", ColorReferencia = "Verde Militar (#2E5A36)", ClaseCss = "dept-operaciones",
                Descripcion = "Planificación, despliegue y conducción de operaciones ficticias MILSIM, eventos tácticos y coordinación de unidades de combate.",
                Responsable = "CE-001 · Paquito (Capitán)",
                IntegrantesCodigos = new() { "CE-001", "CE-002", "CE-003", "CE-007", "CE-009", "CE-013", "CE-014" },
                Estado = "Activo",
                Actividades = new() { "Diseño y conducción de operaciones ficticias", "Coordinación de eventos tácticos en servidores", "Dirección operativa de las unidades UE-001, UE-002 y UE-003" }
            },
            new()
            {
                Id = "DEP-INT", Nombre = "Inteligencia", Emblema = "eye", ColorReferencia = "Azul Petróleo (#1F6F8B)", ClaseCss = "dept-inteligencia",
                Descripcion = "Gestión de documentación estratégica, análisis de escenarios de misión, verificación de antecedentes RP e información institucional.",
                Responsable = "CE-008 · Luna_Roja (Teniente)",
                IntegrantesCodigos = new() { "CE-008", "CE-004", "CE-003" },
                Estado = "Activo",
                Actividades = new() { "Administración del Centro Documental", "Análisis táctico de escenarios y mapas", "Registro y clasificación de antecedentes RP ficticios" }
            },
            new()
            {
                Id = "DEP-LOG", Nombre = "Logística", Emblema = "package", ColorReferencia = "Gris Acero (#6E7681)", ClaseCss = "dept-logistica",
                Descripcion = "Control de recursos ficticios de campaña, equipamiento táctico estándar, parque aéreo/terrestre y materiales de despliegue.",
                Responsable = "CE-006 · Titan_Nova (Sargento)",
                IntegrantesCodigos = new() { "CE-006", "CE-005", "CE-011", "CE-012" },
                Estado = "Activo",
                Actividades = new() { "Asignación de recursos ficticios por misión", "Estandarización de equipamiento MILSIM", "Soporte médico y abastecimiento en teatro de operaciones" }
            },
            new()
            {
                Id = "DEP-REC", Nombre = "Reclutamiento", Emblema = "user-check", ColorReferencia = "Ámbar (#D9822B)", ClaseCss = "dept-reclutamiento",
                Descripcion = "Recepción de aspirantes, validación de mayoría de edad (+18), evaluaciones técnicas, entrevistas y ceremonias de incorporación.",
                Responsable = "CE-014 · Athena_V (Teniente)",
                IntegrantesCodigos = new() { "CE-014", "CE-002", "CE-010" },
                Estado = "Activo",
                Actividades = new() { "Gestión del Mantenimiento de Solicitudes (PA2)", "Evaluación y entrevistas de aspirantes (+18 años)", "Ejecución del protocolo de incorporación CE-###" }
            }
        };

        // ====================================================================
        // 3. LAS 5 UNIDADES TÁCTICAS INTERNAS (UE-001 A UE-005)
        // ====================================================================
        Unidades = new List<UnidadTactica>
        {
            new()
            {
                Id = "UE-001", Nombre = "Unidad de Reconocimiento Táctico «Sombra»", Emblema = "compass",
                Descripcion = "Infiltración avanzada, observación adelantada, designación de objetivos y patrullaje de largo alcance.",
                DepartamentoAsociado = "Operaciones", Lider = "CE-008 · Luna_Roja (Teniente)",
                IntegrantesCodigos = new() { "CE-008", "CE-004", "CE-007" },
                FechaCreacion = new DateTime(2026, 2, 14), Estado = "Activo",
                Historial = new() { "Despliegue inaugural en Altis (Arma 3)", "Reconocimiento nocturno en Everon (Arma Reforger)" },
                Actividades = new() { "Observación táctica adelantada", "Comunicaciones encriptadas de campo", "Cartografía de objetivos" }
            },
            new()
            {
                Id = "UE-002", Nombre = "Unidad de Asalto Terrestre «Vanguardia»", Emblema = "zap",
                Descripcion = "Infantería de choque aerotransportada, combate urbano CQB y toma de posiciones estratégicas.",
                DepartamentoAsociado = "Operaciones", Lider = "CE-002 · Wolf_Alpha (Sargento)",
                IntegrantesCodigos = new() { "CE-002", "CE-010", "CE-013" },
                FechaCreacion = new DateTime(2026, 3, 1), Estado = "Activo",
                Historial = new() { "Operación Bastión de Acero completada con 100% de efectividad", "Certificación CQB nivel avanzado" },
                Actividades = new() { "Asalto directo y despeje de estructuras", "Maniobras mecanizadas", "Respuesta táctica rápida" }
            },
            new()
            {
                Id = "UE-003", Nombre = "Unidad de Apoyo Aéreo Táctico «Halcón»", Emblema = "navigation",
                Descripcion = "Transporte táctico de tropas (MEDEVAC / inserción), apoyo aéreo cercano (CAS) y superioridad aérea.",
                DepartamentoAsociado = "Operaciones", Lider = "CE-001 · Paquito (Capitán)",
                IntegrantesCodigos = new() { "CE-001", "CE-003", "CE-009", "CE-014" },
                FechaCreacion = new DateTime(2026, 3, 18), Estado = "Activo",
                Historial = new() { "Más de 450 horas de vuelo conjunto en Arma 3 y Reforger", "Puente aéreo en Operación Tormenta Verde" },
                Actividades = new() { "Inserción y extracción helitransportada", "Escoltas de convoy desde el aire", "Instrucción de vuelo instrumental" }
            },
            new()
            {
                Id = "UE-004", Nombre = "Unidad de Seguridad y Contención «Aegis»", Emblema = "shield",
                Descripcion = "Protección de convoyes, seguridad de instalaciones, escolta VIP en servidores Roleplay y control perimetral.",
                DepartamentoAsociado = "Comando", Lider = "CE-010 · Halcon_Z (Comandante)",
                IntegrantesCodigos = new() { "CE-010", "CE-015", "CE-013" },
                FechaCreacion = new DateTime(2026, 4, 10), Estado = "Activo",
                Historial = new() { "Custodia táctica en servidor FiveM Distrito Exiliados", "Protocolo de defensa de base central" },
                Actividades = new() { "Seguridad de instalaciones y puestos de mando", "Escolta de alto riesgo en entornos RP", "Control disciplinario de despliegue" }
            },
            new()
            {
                Id = "UE-005", Nombre = "Unidad de Logística y Sanidad «Caduceo»", Emblema = "heart-pulse",
                Descripcion = "Abastecimiento de munición/suministros ficticios, mantenimiento de flota y evacuación médica avanzada en combate.",
                DepartamentoAsociado = "Logística", Lider = "CE-006 · Titan_Nova (Sargento)",
                IntegrantesCodigos = new() { "CE-006", "CE-005", "CE-011", "CE-012" },
                FechaCreacion = new DateTime(2026, 5, 5), Estado = "Activo",
                Historial = new() { "Establecimiento de hospital de campaña móvil", "Suministro continuo en 12 operaciones oficiales" },
                Actividades = new() { "Soporte médico traumatológico MILSIM (ACE3)", "Gestión de convoyes de suministros", "Control de inventario táctico" }
            }
        };

        // ====================================================================
        // 4. INTEGRANTES Y EXPEDIENTES (BASADOS EN LOS 15 REGISTROS DE base.sql DE PA2)
        // ====================================================================
        Integrantes = new List<IntegranteCE>
        {
            new()
            {
                NumeroPA2 = 1, CodigoCE = "CE-001", Nombre = "Marco Antonio Guzmán", Alias = "Paquito",
                Discord = "Paquito#7777", Edad = 27, Rango = "Capitán", FaccionPA2 = "Militares", TurnoDisponibilidadPA2 = "24/7",
                Departamentos = new() { "Operaciones", "Comando" }, Unidades = new() { "UE-003" },
                FechaIngreso = new DateTime(2026, 7, 4), Especialidades = new() { "Piloto", "Liderazgo Aéreo" },
                InsigniasAdministrativas = new() { "Alas de Oro MILSIM", "Comandante de Escuadrón" },
                HorasMilsim = 100, ExperienciaMilsim = "100 h registradas en admisión inicial + 320 h de mando aéreo en Arma 3.",
                ExperienciaRP = "Oficial de aviación privada y coordinador de tráfico aéreo en servidores tácticos.",
                Observaciones = "Primer expediente histórico de la base de datos PA2. Responsable del Departamento de Operaciones.",
                EstadoInstitucional = "Activo", Disponibilidad = "Disponible", SupervisorCodigoCE = "CE-010",
                HistorialInstitucional = new() { "04/07/2026 — Alta e incorporación oficial (Solicitud PA2 #1)", "15/08/2026 — Designado Jefe de Unidad UE-003", "01/10/2026 — Confirmado en jefatura del Departamento de Operaciones" },
                AntecedentesRP = new()
                {
                    new() { Id = "ANT-001", CodigoCE = "CE-001", AliasIntegrante = "Paquito", Plataforma = "Arma 3", Servidor = "SRV-001 · Task Force Exiliados Altis", Comunidad = "Coalición Táctica Latam", PersonajeOAlias = "Capt. M. Guzmán «Paquito»", CategoriaAntecedente = "Militares", TipoRolInterpretado = "Piloto de Helicóptero UH-60 / Mando de Escuadrilla", Periodo = "2025 - 2026", Experiencia = "Inserciones nocturnas con NVG y soporte CAS", Responsabilidades = "Coordinación aérea de 3 aeronaves", Actividades = "Más de 40 misiones de extracción", Descripcion = "Rol ficticio militar en servidor dedicado Arma 3.", Observaciones = "Historial impecable verificado por Inteligencia." }
                }
            },
            new()
            {
                NumeroPA2 = 2, CodigoCE = "CE-002", Nombre = "Alejandro Lobos", Alias = "Wolf_Alpha",
                Discord = "Wolf#2041", Edad = 29, Rango = "Sargento", FaccionPA2 = "Mercenarios", TurnoDisponibilidadPA2 = "Noches",
                Departamentos = new() { "Operaciones", "Reclutamiento" }, Unidades = new() { "UE-002" },
                FechaIngreso = new DateTime(2026, 7, 12), Especialidades = new() { "Infantería", "Brechero CQB" },
                InsigniasAdministrativas = new() { "Instructor de Asalto Urbano", "Vanguardia Táctica" },
                HorasMilsim = 850, ExperienciaMilsim = "850 horas en simulación de infantería mecanizada y despeje CQB.",
                ExperienciaRP = "Contratista táctico de seguridad privada en entornos de roleplay serio.",
                Observaciones = "Segundo registro de PA2. Líder táctico de la Unidad de Asalto UE-002.",
                EstadoInstitucional = "Activo", Disponibilidad = "Disponible", SupervisorCodigoCE = "CE-001",
                HistorialInstitucional = new() { "12/07/2026 — Incorporación oficial (Solicitud PA2 #2)", "20/08/2026 — Nombrado Líder de Unidad UE-002" },
                AntecedentesRP = new()
                {
                    new() { Id = "ANT-002", CodigoCE = "CE-002", AliasIntegrante = "Wolf_Alpha", Plataforma = "Arma Reforger", Servidor = "SRV-002 · Operación Everon 1989", Comunidad = "Brigada Norte RP", PersonajeOAlias = "Sgt. Viktor «Wolf»", CategoriaAntecedente = "Mercenarios", TipoRolInterpretado = "Jefe de Pelotón de Contratistas PMC (Ficticio)", Periodo = "2024 - 2026", Experiencia = "850 horas de combate táctico", Responsabilidades = "Instrucción de escuadra de 8 operadores", Actividades = "Patrullaje y defensa de convoyes", Descripcion = "Interpretación ficticia de contratista privado en campaña Reforger.", Observaciones = "Antecedente estrictamente de roleplay; no equivale a rango institucional automático." }
                }
            },
            new()
            {
                NumeroPA2 = 3, CodigoCE = "CE-003", Nombre = "Valeria Vexler", Alias = "Kira_Vex",
                Discord = "KiraVex#9012", Edad = 25, Rango = "Teniente", FaccionPA2 = "Terroristas", TurnoDisponibilidadPA2 = "Fines de semana",
                Departamentos = new() { "Operaciones", "Inteligencia" }, Unidades = new() { "UE-003" },
                FechaIngreso = new DateTime(2026, 7, 23), Especialidades = new() { "Piloto", "Reconocimiento Aéreo" },
                InsigniasAdministrativas = new() { "Piloto de Combate Nocturno" },
                HorasMilsim = 1200, ExperienciaMilsim = "1,200 horas certificadas en vuelo de ala rotatoria y fija en Arma 3.",
                ExperienciaRP = "Oficial de enlace e inteligencia aérea en campañas narrativas.",
                Observaciones = "Tercer registro de muestra de PA2. Instructora principal de vuelo táctico.",
                EstadoInstitucional = "Activo", Disponibilidad = "Ocupado", SupervisorCodigoCE = "CE-001",
                HistorialInstitucional = new() { "23/07/2026 — Incorporación oficial (Solicitud PA2 #3)", "10/09/2026 — Certificación como Instructora de Vuelo" },
                AntecedentesRP = new()
                {
                    new() { Id = "ANT-003", CodigoCE = "CE-003", AliasIntegrante = "Kira_Vex", Plataforma = "GTA V / FiveM", Servidor = "SRV-003 · San Andreas Tactical RP", Comunidad = "División Táctica RP", PersonajeOAlias = "Kira Vex", CategoriaAntecedente = "Militares", TipoRolInterpretado = "Piloto de Intercepción y Evacuación Aérea", Periodo = "2023 - 2026", Experiencia = "1,200 horas acumuladas", Responsabilidades = "Coordinación de espacio aéreo ficticio", Actividades = "Vuelos tácticos y vigilancia", Descripcion = "Rol ficticio de aviación táctica.", Observaciones = "Verificado sin observaciones." }
                }
            },
            new()
            {
                NumeroPA2 = 4, CodigoCE = "CE-004", Nombre = "Gabriel Mendoza", Alias = "Ghost_07",
                Discord = "Ghost#0007", Edad = 22, Rango = "Operador", FaccionPA2 = "Militares", TurnoDisponibilidadPA2 = "Noches",
                Departamentos = new() { "Inteligencia" }, Unidades = new() { "UE-001" },
                FechaIngreso = new DateTime(2026, 8, 2), Especialidades = new() { "Comunicaciones", "Guerra Electrónica" },
                InsigniasAdministrativas = new() { "Operador Radio RTO" },
                HorasMilsim = 40, ExperienciaMilsim = "40 horas iniciales + formación interna en radiofrecuencia ACRE2/TFAR.",
                ExperienciaRP = "Operador de centro de control y despacho táctico.",
                Observaciones = "Especialista en protocolo radial de la unidad UE-001.",
                EstadoInstitucional = "Activo", Disponibilidad = "Disponible", SupervisorCodigoCE = "CE-008",
                HistorialInstitucional = new() { "02/08/2026 — Incorporación oficial (Solicitud PA2 #4)" }
            },
            new()
            {
                NumeroPA2 = 5, CodigoCE = "CE-005", Nombre = "Sebastián Rivas", Alias = "Doc_Rivas",
                Discord = "DocRivas#3310", Edad = 28, Rango = "Cabo", FaccionPA2 = "Mercenarios", TurnoDisponibilidadPA2 = "24/7",
                Departamentos = new() { "Logística" }, Unidades = new() { "UE-005" },
                FechaIngreso = new DateTime(2026, 8, 9), Especialidades = new() { "Médico", "Evacuación MEDEVAC" },
                InsigniasAdministrativas = new() { "Cruz de Sanidad Táctica" },
                HorasMilsim = 300, ExperienciaMilsim = "300 horas como paramédico de combate avanzado (sistema médico ACE3).",
                ExperienciaRP = "Jefe de cirugía de campaña y rescate en servidores de simulación.",
                Observaciones = "Candidato propuesto para ascenso a Sargento por mérito médico.",
                EstadoInstitucional = "Activo", Disponibilidad = "Disponible", SupervisorCodigoCE = "CE-006",
                HistorialInstitucional = new() { "09/08/2026 — Incorporación oficial (Solicitud PA2 #5)" }
            },
            new()
            {
                NumeroPA2 = 6, CodigoCE = "CE-006", Nombre = "Héctor Novoa", Alias = "Titan_Nova",
                Discord = "Titan#5555", Edad = 31, Rango = "Sargento", FaccionPA2 = "Terroristas", TurnoDisponibilidadPA2 = "Fines de semana",
                Departamentos = new() { "Logística" }, Unidades = new() { "UE-005" },
                FechaIngreso = new DateTime(2026, 8, 17), Especialidades = new() { "Logística", "Blindados" },
                InsigniasAdministrativas = new() { "Intendente Mayor CE" },
                HorasMilsim = 640, ExperienciaMilsim = "640 horas en gestión de convoyes, artillería y suministros.",
                ExperienciaRP = "Coordinador de suministros e infraestructura táctica.",
                Observaciones = "Responsable del Departamento de Logística y líder de UE-005.",
                EstadoInstitucional = "Activo", Disponibilidad = "Disponible", SupervisorCodigoCE = "CE-015",
                HistorialInstitucional = new() { "17/08/2026 — Incorporación oficial (Solicitud PA2 #6)", "01/09/2026 — Designado Responsable de Logística" }
            },
            new()
            {
                NumeroPA2 = 7, CodigoCE = "CE-007", Nombre = "Diego Ramírez", Alias = "Zorro_X",
                Discord = "Zorro#1188", Edad = 20, Rango = "Recluta", FaccionPA2 = "Militares", TurnoDisponibilidadPA2 = "Noches",
                Departamentos = new() { "Operaciones" }, Unidades = new() { "UE-001" },
                FechaIngreso = new DateTime(2026, 8, 28), Especialidades = new() { "Infantería" },
                InsigniasAdministrativas = new(),
                HorasMilsim = 15, ExperienciaMilsim = "15 horas iniciales; en proceso de instrucción básica MILSIM.",
                ExperienciaRP = "Participante en rol policial y patrullaje táctico.",
                Observaciones = "En fase de evaluación de entrenamiento básico.",
                EstadoInstitucional = "Activo", Disponibilidad = "Desconectado", SupervisorCodigoCE = "CE-008",
                HistorialInstitucional = new() { "28/08/2026 — Incorporación como Recluta (Solicitud PA2 #7)" }
            },
            new()
            {
                NumeroPA2 = 8, CodigoCE = "CE-008", Nombre = "Lucía Montes", Alias = "Luna_Roja",
                Discord = "Luna#4477", Edad = 26, Rango = "Teniente", FaccionPA2 = "Mercenarios", TurnoDisponibilidadPA2 = "24/7",
                Departamentos = new() { "Inteligencia", "Operaciones" }, Unidades = new() { "UE-001" },
                FechaIngreso = new DateTime(2026, 9, 3), Especialidades = new() { "Comunicaciones", "Ciberinteligencia Táctica" },
                InsigniasAdministrativas = new() { "Criptógrafa Táctica", "Mando de Reconocimiento" },
                HorasMilsim = 920, ExperienciaMilsim = "920 horas en dirección de inteligencia, JTAC y control aéreo.",
                ExperienciaRP = "Analista de inteligencia e interrogadora en tramas geopolíticas ficticias.",
                Observaciones = "Responsable del Departamento de Inteligencia y líder de UE-001.",
                EstadoInstitucional = "Activo", Disponibilidad = "Disponible", SupervisorCodigoCE = "CE-010",
                HistorialInstitucional = new() { "03/09/2026 — Incorporación oficial (Solicitud PA2 #8)", "15/09/2026 — Jefatura de Inteligencia" },
                AntecedentesRP = new()
                {
                    new() { Id = "ANT-004", CodigoCE = "CE-008", AliasIntegrante = "Luna_Roja", Plataforma = "Arma 3", Servidor = "SRV-004 · Insurgencia Takistán RP", Comunidad = "Red Táctica Hispana", PersonajeOAlias = "Comandante «Zafiro» (Facción Insurgente OpFor)", CategoriaAntecedente = "Terroristas", TipoRolInterpretado = "Líder de Célula Insurgente Ficticia (OpFor de ambientación)", Periodo = "2024 - 2025", Experiencia = "Ambientación de bando contrario (OpFor) para misiones del clan", Responsabilidades = "Diseño de emboscadas ficticias para entrenamiento de BluFor", Actividades = "Roleplay de facción antagonista en servidor privado", Descripcion = "Interpretación estrictamente teatral/ficticia como fuerza opositora (OpFor) en campañas de Arma 3.", Observaciones = "Rol de ambientación autorizado para enriquecer escenarios MILSIM." }
                }
            },
            new()
            {
                NumeroPA2 = 9, CodigoCE = "CE-009", Nombre = "Matías Vargas", Alias = "Viper_21",
                Discord = "Viper#2121", Edad = 24, Rango = "Cabo", FaccionPA2 = "Terroristas", TurnoDisponibilidadPA2 = "Noches",
                Departamentos = new() { "Operaciones" }, Unidades = new() { "UE-003" },
                FechaIngreso = new DateTime(2026, 9, 10), Especialidades = new() { "Piloto", "Artillero de Puerta" },
                InsigniasAdministrativas = new() { "Copiloto Certificado" },
                HorasMilsim = 210, ExperienciaMilsim = "210 horas en navegación táctica a baja cota.",
                ExperienciaRP = "Piloto de rescate marítimo y montaña en FiveM.",
                Observaciones = "Actualmente en reserva temporal por estudios universitarios.",
                EstadoInstitucional = "En reserva", Disponibilidad = "No disponible", SupervisorCodigoCE = "CE-001",
                HistorialInstitucional = new() { "10/09/2026 — Incorporación oficial (Solicitud PA2 #9)", "25/10/2026 — Pase a estado En reserva autorizada" }
            },
            new()
            {
                NumeroPA2 = 10, CodigoCE = "CE-010", Nombre = "Fernando Alcázar", Alias = "Halcon_Z",
                Discord = "Halcon#6060", Edad = 33, Rango = "Comandante", FaccionPA2 = "Militares", TurnoDisponibilidadPA2 = "Fines de semana",
                Departamentos = new() { "Comando", "Operaciones", "Reclutamiento" }, Unidades = new() { "UE-004", "UE-002" },
                FechaIngreso = new DateTime(2026, 9, 19), Especialidades = new() { "Infantería", "Estrategia Conjunta" },
                InsigniasAdministrativas = new() { "Cruz de Mando Supremo", "Estratega Senior" },
                HorasMilsim = 1500, ExperienciaMilsim = "1,500 horas liderando operaciones inter-clanes y teatro combinado.",
                ExperienciaRP = "Comandante de fuerzas especiales ficticias y comisario general en RP.",
                Observaciones = "Segundo al mando institucional (Comandante) y líder de UE-004.",
                EstadoInstitucional = "Activo", Disponibilidad = "Disponible", SupervisorCodigoCE = "CE-015",
                HistorialInstitucional = new() { "19/09/2026 — Incorporación en Estado Mayor (Solicitud PA2 #10)", "01/10/2026 — Ratificado como Comandante Ejecutivo" }
            },
            new()
            {
                NumeroPA2 = 11, CodigoCE = "CE-011", Nombre = "Camila Salazar", Alias = "Nyx_Sombra",
                Discord = "Nyx#8123", Edad = 25, Rango = "Sargento", FaccionPA2 = "Mercenarios", TurnoDisponibilidadPA2 = "24/7",
                Departamentos = new() { "Logística" }, Unidades = new() { "UE-005" },
                FechaIngreso = new DateTime(2026, 9, 27), Especialidades = new() { "Médico", "Quirófano de Campaña" },
                InsigniasAdministrativas = new() { "Instructora Médica ACE3" },
                HorasMilsim = 480, ExperienciaMilsim = "480 horas en logística médica y triaje bajo fuego.",
                ExperienciaRP = "Directora de hospital de campaña en servidor Arma Reforger.",
                Observaciones = "Subjefa de la unidad médica UE-005.",
                EstadoInstitucional = "Activo", Disponibilidad = "Ocupado", SupervisorCodigoCE = "CE-006",
                HistorialInstitucional = new() { "27/09/2026 — Incorporación oficial (Solicitud PA2 #11)" }
            },
            new()
            {
                NumeroPA2 = 12, CodigoCE = "CE-012", Nombre = "Bruno Morales", Alias = "Bruno_Mx",
                Discord = "Bruno#3030", Edad = 21, Rango = "Operador", FaccionPA2 = "Terroristas", TurnoDisponibilidadPA2 = "Noches",
                Departamentos = new() { "Logística" }, Unidades = new() { "UE-005" },
                FechaIngreso = new DateTime(2026, 10, 1), Especialidades = new() { "Logística", "Transporte Pesado" },
                InsigniasAdministrativas = new(),
                HorasMilsim = 60, ExperienciaMilsim = "60 horas en conducción de camiones tácticos y escolta.",
                ExperienciaRP = "Transportista de blindados en servidores RP.",
                Observaciones = "Suspendido temporalmente por inasistencia injustificada a convoy oficial.",
                EstadoInstitucional = "Suspendido", Disponibilidad = "No disponible", SupervisorCodigoCE = "CE-006",
                HistorialInstitucional = new() { "01/10/2026 — Incorporación oficial (Solicitud PA2 #12)", "28/10/2026 — Suspensión temporal de 7 días (DIS-002)" }
            },
            new()
            {
                NumeroPA2 = 13, CodigoCE = "CE-013", Nombre = "Ricardo Delgado", Alias = "Rex_Delta",
                Discord = "Rex#1414", Edad = 23, Rango = "Cabo", FaccionPA2 = "Militares", TurnoDisponibilidadPA2 = "Fines de semana",
                Departamentos = new() { "Operaciones" }, Unidades = new() { "UE-002", "UE-004" },
                FechaIngreso = new DateTime(2026, 10, 5), Especialidades = new() { "Infantería", "Tirador Designado DMR" },
                InsigniasAdministrativas = new() { "Puntería de Precisión" },
                HorasMilsim = 175, ExperienciaMilsim = "175 horas como tirador selecto de escuadra.",
                ExperienciaRP = "Agente SWAT en servidor FiveM.",
                Observaciones = "Integrante de apoyo tanto en Asalto (UE-002) como en Seguridad (UE-004).",
                EstadoInstitucional = "Activo", Disponibilidad = "Disponible", SupervisorCodigoCE = "CE-002",
                HistorialInstitucional = new() { "05/10/2026 — Incorporación oficial (Solicitud PA2 #13)" }
            },
            new()
            {
                NumeroPA2 = 14, CodigoCE = "CE-014", Nombre = "Elena Valdivia", Alias = "Athena_V",
                Discord = "Athena#9090", Edad = 28, Rango = "Teniente", FaccionPA2 = "Mercenarios", TurnoDisponibilidadPA2 = "24/7",
                Departamentos = new() { "Reclutamiento", "Operaciones" }, Unidades = new() { "UE-003" },
                FechaIngreso = new DateTime(2026, 10, 9), Especialidades = new() { "Piloto", "Evaluación de Personal" },
                InsigniasAdministrativas = new() { "Directora de Admisión", "Alas de Plata" },
                HorasMilsim = 1050, ExperienciaMilsim = "1,050 horas en aviación táctica e instrucción de reclutas.",
                ExperienciaRP = "Instructora de academia táctica y examinadora de vuelo.",
                Observaciones = "Responsable del Departamento de Reclutamiento.",
                EstadoInstitucional = "Activo", Disponibilidad = "Disponible", SupervisorCodigoCE = "CE-010",
                HistorialInstitucional = new() { "09/10/2026 — Incorporación oficial (Solicitud PA2 #14)", "15/10/2026 — Designada Jefa de Reclutamiento" }
            },
            new()
            {
                NumeroPA2 = 15, CodigoCE = "CE-015", Nombre = "Esteban Kódiak", Alias = "Kodiak",
                Discord = "Kodiak#7001", Edad = 35, Rango = "Director/Fundador", FaccionPA2 = "Terroristas", TurnoDisponibilidadPA2 = "Noches",
                Departamentos = new() { "Comando", "Logística" }, Unidades = new() { "UE-004" },
                FechaIngreso = new DateTime(2026, 10, 12), Especialidades = new() { "Logística", "Dirección Estratégica MILSIM" },
                InsigniasAdministrativas = new() { "Fundador Clan Exiliados", "Gran Cruz Institucional" },
                HorasMilsim = 1800, ExperienciaMilsim = "1,800 horas en mando estratégico, edición de misiones Zeus y administración de servidores.",
                ExperienciaRP = "Fundador de comunidades de simulación militar y roleplay táctico desde 2021.",
                Observaciones = "Director/Fundador de Clan Exiliados (Registro #15 con mayor experiencia en base.sql).",
                EstadoInstitucional = "Activo", Disponibilidad = "Disponible", SupervisorCodigoCE = "",
                HistorialInstitucional = new() { "12/10/2026 — Registro maestro de Dirección (Solicitud PA2 #15)" }
            }
        };

        // ====================================================================
        // 5. SOLICITUDES DE RECLUTAMIENTO E INCORPORACIÓN (ADM-001 A ADM-004)
        // ====================================================================
        SolicitudesReclutamiento = new List<SolicitudReclutamiento>
        {
            new()
            {
                Id = "ADM-001", NumeroPA2 = 16, Nombre = "Javier Solís", Alias = "Falcon_Pe",
                Discord = "FalconPe#4512", Edad = 24, HorasMilsim = 420,
                RangoDeseadoPA2 = "Soldado", FaccionPA2 = "Militares", DisponibilidadPA2 = "Noches",
                ExperienciaMilsim = "420 horas en infantería ligera y operación de morteros en Arma 3.",
                ExperienciaRoleplay = "2 años en servidores de rol militar y emergencias en FiveM.",
                Plataformas = new() { "Arma 3", "Arma Reforger" },
                ServidoresAnteriores = "Task Force Latam, Brigada Andina MILSIM",
                Especialidades = new() { "Infantería", "Comunicaciones" },
                DescripcionPersonal = "Jugador disciplinado, con micrófono de calidad y disponibilidad nocturna estable.",
                AntecedentesRP = "Rol de operador de radio en campañas cooperativas (Antecedente Militar ficticio).",
                FechaSolicitud = DateTime.Today.AddDays(-3),
                EtapaFlujo = "Entrevista", EstadoAdmision = "En revisión",
                CriteriosEvaluacion = "Mayoría de edad verificada (24 años). Conocimiento sólido de radio TFAR.",
                ObservacionesEvaluacion = "Candidato con excelente actitud táctica.",
                EntrevistaFicticia = "Entrevista realizada por Teniente Athena_V: responde correctamente protocolos de cadena de mando.",
                EvaluacionExperiencia = "Nivel intermedio-alto comprobado (420 h).",
                DecisionInstitucional = "Pendiente de firma final para pasar a Aprobada e Incorporación."
            },
            new()
            {
                Id = "ADM-002", NumeroPA2 = 17, Nombre = "Adriana Vega", Alias = "Valkyria_09",
                Discord = "AdriVega#8890", Edad = 26, HorasMilsim = 680,
                RangoDeseadoPA2 = "Cabo", FaccionPA2 = "Mercenarios", DisponibilidadPA2 = "Fines de semana",
                ExperienciaMilsim = "680 horas como médica de combate y logística aérea en Arma Reforger.",
                ExperienciaRoleplay = "3 años interpretando personal sanitario de rescate táctico.",
                Plataformas = new() { "Arma Reforger", "GTA V / FiveM" },
                ServidoresAnteriores = "Cuerpo Médico Táctico, San Andreas Emergency RP",
                Especialidades = new() { "Médico", "Logística" },
                DescripcionPersonal = "Especialista en protocolo ACE3 médico y organización de suministros.",
                AntecedentesRP = "Jefa de paramédicos tácticos en servidor RP (Antecedente Militar ficticio).",
                FechaSolicitud = DateTime.Today.AddDays(-5),
                EtapaFlujo = "Decisión", EstadoAdmision = "Aprobada",
                CriteriosEvaluacion = "Edad verificada (26 años). Prueba práctica de triaje superada con 98/100.",
                ObservacionesEvaluacion = "Lista para asignación de código CE-### y ceremonia digital de incorporación.",
                EntrevistaFicticia = "Aprobada por unanimidad por el comité de Reclutamiento.",
                EvaluacionExperiencia = "Sobresaliente (680 h).",
                DecisionInstitucional = "APROBADA — Pendiente de ejecutar ficha de incorporación automática."
            },
            new()
            {
                Id = "ADM-003", NumeroPA2 = 18, Nombre = "Carlos Pineda", Alias = "Shadow_Kid",
                Discord = "ShadowK#1001", Edad = 16, HorasMilsim = 500,
                RangoDeseadoPA2 = "Sargento", FaccionPA2 = "Terroristas", DisponibilidadPA2 = "24/7",
                ExperienciaMilsim = "500 horas reportadas en partidas públicas.",
                ExperienciaRoleplay = "1 año en servidores públicos.",
                Plataformas = new() { "Arma 3" },
                ServidoresAnteriores = "Servidores públicos King of the Hill",
                Especialidades = new() { "Infantería" },
                DescripcionPersonal = "Solicita ingreso al clan.",
                AntecedentesRP = "Sin antecedentes verificables.",
                FechaSolicitud = DateTime.Today.AddDays(-2),
                EtapaFlujo = "Verificación de edad", EstadoAdmision = "Rechazada",
                CriteriosEvaluacion = "Incumplimiento del requisito obligatorio de mayoría de edad (16 años < 18 años mínimos).",
                ObservacionesEvaluacion = "El reglamento institucional prohíbe la admisión de menores de 18 años sin excepción.",
                EntrevistaFicticia = "No aplica por filtro de edad.",
                EvaluacionExperiencia = "Desestimada por requisito legal/institucional de edad.",
                DecisionInstitucional = "RECHAZADA AUTOMÁTICAMENTE — Menor de 18 años."
            },
            new()
            {
                Id = "ADM-004", NumeroPA2 = 19, Nombre = "Rodrigo Cáceres", Alias = "Nomad_Actual",
                Discord = "Nomad#7331", Edad = 29, HorasMilsim = 310,
                RangoDeseadoPA2 = "Recluta", FaccionPA2 = "Terroristas", DisponibilidadPA2 = "Noches",
                ExperienciaMilsim = "310 horas en logística de convoyes y artillería.",
                ExperienciaRoleplay = "Contratista de seguridad privada en GTA V / FiveM.",
                Plataformas = new() { "Arma 3", "GTA V / FiveM" },
                ServidoresAnteriores = "División Blindada Sur",
                Especialidades = new() { "Logística", "Infantería" },
                DescripcionPersonal = "Interesado en apoyar a la unidad logística y seguridad de convoyes.",
                AntecedentesRP = "Escolta de transporte blindado (Antecedente Mercenario ficticio).",
                FechaSolicitud = DateTime.Today.AddDays(-1),
                EtapaFlujo = "Evaluación", EstadoAdmision = "Pendiente",
                CriteriosEvaluacion = "Edad verificada (29 años). Pendiente de revisión de cuestionario táctico.",
                ObservacionesEvaluacion = "Solicitud recién ingresada en cola de evaluación.",
                EntrevistaFicticia = "Pendiente de programar fecha.",
                EvaluacionExperiencia = "En verificación.",
                DecisionInstitucional = "En espera de evaluación inicial."
            }
        };

        // ====================================================================
        // 6. SERVIDORES Y PLATAFORMAS (SRV-001 A SRV-005)
        // ====================================================================
        Servidores = new List<ServidorPlataforma>
        {
            new()
            {
                Id = "SRV-001", Nombre = "Task Force Exiliados — Teatro Altis", Plataforma = "Arma 3",
                ComunidadUniversoRP = "Universo Táctico Mediterráneo CE", Modalidad = "MILSIM",
                Descripcion = "Servidor dedicado principal para campañas oficiales de operaciones combinadas con ACE3, TFAR y RHS.",
                Responsable = "CE-001 · Paquito (Capitán)",
                IntegrantesCodigos = new() { "CE-001", "CE-002", "CE-003", "CE-004", "CE-005", "CE-006", "CE-008", "CE-010", "CE-015" },
                Unidades = new() { "UE-001", "UE-002", "UE-003", "UE-005" },
                InicioParticipacion = new DateTime(2025, 6, 1), Estado = "Activo",
                Enlace = "ts3://arma3.clanexiliados.net:2302",
                Historial = new() { "Campaña Operación Centinela (Finalizada)", "Campaña Tormenta Verde (En curso)" }
            },
            new()
            {
                Id = "SRV-002", Nombre = "Operación Everon — Guerra Fría & Moderno", Plataforma = "Arma Reforger",
                ComunidadUniversoRP = "Teatro Atlántico Enfusion CE", Modalidad = "MILSIM",
                Descripcion = "Servidor multiplataforma sobre motor Enfusion para maniobras de infantería mecanizada, logística y combate nocturno.",
                Responsable = "CE-002 · Wolf_Alpha (Sargento)",
                IntegrantesCodigos = new() { "CE-002", "CE-006", "CE-007", "CE-011", "CE-013", "CE-014" },
                Unidades = new() { "UE-001", "UE-002", "UE-005" },
                InicioParticipacion = new DateTime(2026, 1, 15), Estado = "Activo",
                Enlace = "reforger://everon.clanexiliados.net:2001",
                Historial = new() { "Despliegue de reconquista de Montignac", "Instrucción semanal de escuadras" }
            },
            new()
            {
                Id = "SRV-003", Nombre = "Distrito Exiliados — Seguridad Corporativa PMC", Plataforma = "GTA V / FiveM",
                ComunidadUniversoRP = "San Andreas Tactical Roleplay", Modalidad = "Roleplay",
                Descripcion = "Servidor de Roleplay táctico e institucional donde Clan Exiliados opera como corporación de seguridad privada, escolta VIP y respuesta SWAT.",
                Responsable = "CE-010 · Halcon_Z (Comandante)",
                IntegrantesCodigos = new() { "CE-003", "CE-009", "CE-010", "CE-012", "CE-013", "CE-015" },
                Unidades = new() { "UE-003", "UE-004" },
                InicioParticipacion = new DateTime(2025, 11, 10), Estado = "Activo",
                Enlace = "fivem://connect/rp.clanexiliados.net:30120",
                Historial = new() { "Operativo de custodia diplomática", "Certificación de división aérea policial/PMC" }
            },
            new()
            {
                Id = "SRV-004", Nombre = "Teatro Takistán — Operaciones Desérticas", Plataforma = "Arma 3",
                ComunidadUniversoRP = "Coalición Desierto Sur", Modalidad = "MILSIM",
                Descripcion = "Servidor secundario utilizado para campañas históricas en terreno árido y entrenamientos de guerra asimétrica.",
                Responsable = "CE-008 · Luna_Roja (Teniente)",
                IntegrantesCodigos = new() { "CE-004", "CE-008", "CE-011" },
                Unidades = new() { "UE-001", "UE-005" },
                InicioParticipacion = new DateTime(2024, 8, 20), Estado = "Histórico",
                Enlace = "archivo://takistan.clanexiliados.net",
                Historial = new() { "Campaña Escudo del Desierto 2024-2025 concluida exitosamente" }
            },
            new()
            {
                Id = "SRV-005", Nombre = "Polígono Conjunto CQB — Ready or Not / Squad", Plataforma = "Otras plataformas compatibles",
                ComunidadUniversoRP = "División de Intervención Rápida CE", Modalidad = "Táctico Combinado",
                Descripcion = "Entorno de entrenamiento táctico en espacios cerrados (CQB) y coordinación de escuadras entre semana.",
                Responsable = "CE-014 · Athena_V (Teniente)",
                IntegrantesCodigos = new() { "CE-002", "CE-013", "CE-014" },
                Unidades = new() { "UE-002", "UE-004" },
                InicioParticipacion = new DateTime(2026, 4, 1), Estado = "Activo",
                Enlace = "tactical://cqb.clanexiliados.net",
                Historial = new() { "Torneo interno de despeje de estructuras" }
            }
        };

        // ====================================================================
        // 7. OPERACIONES INSTITUCIONALES (OP-001 A OP-005)
        // ====================================================================
        var hoy = DateTime.Today;
        Operaciones = new List<OperacionInstitucional>
        {
            new()
            {
                Id = "OP-001", Nombre = "Operación Lanza de Obsidiana", Plataforma = "Arma 3",
                Fecha = hoy, Horario = "20:00 - 22:30", DepartamentoResponsable = "Operaciones",
                Unidades = new() { "UE-001", "UE-002", "UE-003" }, Responsable = "CE-001 · Paquito (Capitán)",
                Descripcion = "Despliegue nocturno helitransportado para asegurar una estación de radar y extraer información crítica.",
                ContextoFicticio = "Fuerzas convencionales ficticias han ocupado el sector norte de Altis; las facciones Militares y Mercenarios ejecutan inserción conjunta.",
                Estado = "En curso",
                Convocados = new()
                {
                    new() { CodigoCE = "CE-001", Alias = "Paquito", EstadoConvocatoria = "Confirmado" },
                    new() { CodigoCE = "CE-002", Alias = "Wolf_Alpha", EstadoConvocatoria = "Confirmado" },
                    new() { CodigoCE = "CE-003", Alias = "Kira_Vex", EstadoConvocatoria = "Confirmado" },
                    new() { CodigoCE = "CE-008", Alias = "Luna_Roja", EstadoConvocatoria = "Confirmado" },
                    new() { CodigoCE = "CE-007", Alias = "Zorro_X", EstadoConvocatoria = "Pendiente de respuesta" }
                }
            },
            new()
            {
                Id = "OP-002", Nombre = "Operación Escudo de Hierro (Convoy VIP)", Plataforma = "GTA V / FiveM",
                Fecha = hoy.AddDays(2), Horario = "21:00 - 23:00", DepartamentoResponsable = "Comando",
                Unidades = new() { "UE-004", "UE-005" }, Responsable = "CE-010 · Halcon_Z (Comandante)",
                Descripcion = "Escolta táctica de tres vehículos blindados desde el puerto hasta la base norte con cobertura médica.",
                ContextoFicticio = "Escenario de roleplay corporativo de alta tensión en San Andreas.",
                Estado = "Programada",
                Convocados = new()
                {
                    new() { CodigoCE = "CE-010", Alias = "Halcon_Z", EstadoConvocatoria = "Confirmado" },
                    new() { CodigoCE = "CE-006", Alias = "Titan_Nova", EstadoConvocatoria = "Confirmado" },
                    new() { CodigoCE = "CE-013", Alias = "Rex_Delta", EstadoConvocatoria = "Convocado" },
                    new() { CodigoCE = "CE-005", Alias = "Doc_Rivas", EstadoConvocatoria = "Confirmado" }
                }
            },
            new()
            {
                Id = "OP-003", Nombre = "Operación Relámpago de Everon", Plataforma = "Arma Reforger",
                Fecha = hoy.AddDays(2), Horario = "21:00 - 23:00", DepartamentoResponsable = "Operaciones",
                Unidades = new() { "UE-001", "UE-002" }, Responsable = "CE-002 · Wolf_Alpha (Sargento)",
                Descripcion = "Reconocimiento y toma de nudo de comunicaciones en Montignac (Actividad simultánea en la misma franja horaria que OP-002).",
                ContextoFicticio = "Maniobra de control territorial en servidor Reforger.",
                Estado = "Programada",
                Convocados = new()
                {
                    new() { CodigoCE = "CE-002", Alias = "Wolf_Alpha", EstadoConvocatoria = "Confirmado" },
                    new() { CodigoCE = "CE-004", Alias = "Ghost_07", EstadoConvocatoria = "Confirmado" },
                    new() { CodigoCE = "CE-014", Alias = "Athena_V", EstadoConvocatoria = "Convocado" }
                }
            },
            new()
            {
                Id = "OP-004", Nombre = "Operación Centinela del Alba", Plataforma = "Arma 3",
                Fecha = hoy.AddDays(-6), Horario = "19:30 - 22:00", DepartamentoResponsable = "Operaciones",
                Unidades = new() { "UE-001", "UE-003", "UE-005" }, Responsable = "CE-001 · Paquito (Capitán)",
                Descripcion = "Evacuación aeromédica bajo fuego y aseguramiento de suministro logístico.",
                ContextoFicticio = "Primera fase de la campaña trimestral de Clan Exiliados.",
                Estado = "Finalizada",
                Convocados = new()
                {
                    new() { CodigoCE = "CE-001", Alias = "Paquito", EstadoConvocatoria = "Confirmado" },
                    new() { CodigoCE = "CE-005", Alias = "Doc_Rivas", EstadoConvocatoria = "Confirmado" },
                    new() { CodigoCE = "CE-006", Alias = "Titan_Nova", EstadoConvocatoria = "Confirmado" },
                    new() { CodigoCE = "CE-011", Alias = "Nyx_Sombra", EstadoConvocatoria = "Confirmado" }
                }
            },
            new()
            {
                Id = "OP-005", Nombre = "Operación Niebla Ártica", Plataforma = "Arma Reforger",
                Fecha = hoy.AddDays(-12), Horario = "20:00 - 22:00", DepartamentoResponsable = "Inteligencia",
                Unidades = new() { "UE-001" }, Responsable = "CE-008 · Luna_Roja (Teniente)",
                Descripcion = "Infiltración sigilosa suspendida por mantenimiento técnico del servidor externo.",
                ContextoFicticio = "Reconocimiento de largo alcance.",
                Estado = "Cancelada",
                Convocados = new()
                {
                    new() { CodigoCE = "CE-008", Alias = "Luna_Roja", EstadoConvocatoria = "Confirmado" },
                    new() { CodigoCE = "CE-009", Alias = "Viper_21", EstadoConvocatoria = "No participará" }
                }
            }
        };

        // ====================================================================
        // 8. ENTRENAMIENTOS Y EVALUACIONES (ENT-001 A ENT-004)
        // ====================================================================
        Entrenamientos = new List<EntrenamientoInstitucional>
        {
            new()
            {
                Id = "ENT-001", Nombre = "Curso Básico de Doctrina MILSIM y Formaciones", Categoria = "MILSIM",
                Plataforma = "Arma 3", Fecha = hoy.AddDays(1), Horario = "19:00 - 20:30",
                Instructor = "CE-002 · Wolf_Alpha (Sargento)", Unidades = new() { "UE-001", "UE-002" },
                Descripcion = "Entrenamiento obligatorio para reclutas y operadores sobre sectores de tiro, formaciones en cuña/columna y embarque aéreo.",
                Estado = "Programado",
                Convocados = new()
                {
                    new() { CodigoCE = "CE-007", Alias = "Zorro_X", Asistencia = "Asistió", RendimientoGeneral = 84, Comunicacion = 85, Coordinacion = 80, TrabajoEnEquipo = 88, CumplimientoObjetivos = 85, Observaciones = "En progreso de certificación inicial.", Resultado = "En proceso" },
                    new() { CodigoCE = "CE-013", Alias = "Rex_Delta", Asistencia = "Asistió", RendimientoGeneral = 94, Comunicacion = 92, Coordinacion = 95, TrabajoEnEquipo = 96, CumplimientoObjetivos = 94, Observaciones = "Excelente desempeño como binomio DMR.", Resultado = "Aprobado" }
                }
            },
            new()
            {
                Id = "ENT-002", Nombre = "Protocolo Radial TFAR/ACRE2 y Brevity Codes", Categoria = "Comunicación",
                Plataforma = "Arma Reforger", Fecha = hoy.AddDays(-3), Horario = "20:00 - 21:30",
                Instructor = "CE-008 · Luna_Roja (Teniente)", Unidades = new() { "UE-001", "UE-003" },
                Descripcion = "Estandarización de malla de radio de escuadra, mando aéreo y reporte SITREP/MEDEVAC de 9 líneas.",
                Estado = "Finalizado",
                Convocados = new()
                {
                    new() { CodigoCE = "CE-004", Alias = "Ghost_07", Asistencia = "Asistió", RendimientoGeneral = 98, Comunicacion = 100, Coordinacion = 96, TrabajoEnEquipo = 97, CumplimientoObjetivos = 98, Observaciones = "Dominio sobresaliente de frecuencias.", Resultado = "Aprobado" },
                    new() { CodigoCE = "CE-003", Alias = "Kira_Vex", Asistencia = "Asistió", RendimientoGeneral = 96, Comunicacion = 95, Coordinacion = 98, TrabajoEnEquipo = 95, CumplimientoObjetivos = 96, Observaciones = "Coordinación aire-tierra impecable.", Resultado = "Aprobado" },
                    new() { CodigoCE = "CE-012", Alias = "Bruno_Mx", Asistencia = "Ausente", RendimientoGeneral = 0, Comunicacion = 0, Coordinacion = 0, TrabajoEnEquipo = 0, CumplimientoObjetivos = 0, Observaciones = "Ausencia sin aviso previo.", Resultado = "No aprobado" }
                }
            },
            new()
            {
                Id = "ENT-003", Nombre = "Maniobras Conjuntas Helitransportadas y MEDEVAC", Categoria = "Coordinación",
                Plataforma = "Arma 3", Fecha = hoy.AddDays(-8), Horario = "18:30 - 20:30",
                Instructor = "CE-001 · Paquito (Capitán)", Unidades = new() { "UE-003", "UE-005" },
                Descripcion = "Coordinación entre pilotos de inserción (UE-003) y escuadra médica de evacuación bajo fuego (UE-005).",
                Estado = "Finalizado",
                Convocados = new()
                {
                    new() { CodigoCE = "CE-005", Alias = "Doc_Rivas", Asistencia = "Asistió", RendimientoGeneral = 95, Comunicacion = 94, Coordinacion = 96, TrabajoEnEquipo = 96, CumplimientoObjetivos = 95, Observaciones = "Extracción médica en menos de 4 minutos.", Resultado = "Aprobado" },
                    new() { CodigoCE = "CE-011", Alias = "Nyx_Sombra", Asistencia = "Asistió", RendimientoGeneral = 93, Comunicacion = 92, Coordinacion = 94, TrabajoEnEquipo = 95, CumplimientoObjetivos = 92, Observaciones = "Excelente estabilización quirúrgica ACE3.", Resultado = "Aprobado" },
                    new() { CodigoCE = "CE-009", Alias = "Viper_21", Asistencia = "Justificado", RendimientoGeneral = 88, Comunicacion = 88, Coordinacion = 88, TrabajoEnEquipo = 90, CumplimientoObjetivos = 88, Observaciones = "Permiso académico registrado.", Resultado = "Aprobado" }
                }
            },
            new()
            {
                Id = "ENT-004", Nombre = "Interpretación Táctica, Negociación y Escolta VIP", Categoria = "Roleplay",
                Plataforma = "GTA V / FiveM", Fecha = hoy.AddDays(4), Horario = "21:00 - 22:30",
                Instructor = "CE-010 · Halcon_Z (Comandante)", Unidades = new() { "UE-004" },
                Descripcion = "Entrenamiento de actuación institucional, perímetros de seguridad y resolución de crisis sin romper rol.",
                Estado = "Programado",
                Convocados = new()
                {
                    new() { CodigoCE = "CE-010", Alias = "Halcon_Z", Asistencia = "Asistió", RendimientoGeneral = 97, Comunicacion = 98, Coordinacion = 96, TrabajoEnEquipo = 97, CumplimientoObjetivos = 98, Observaciones = "Instructor líder.", Resultado = "Aprobado" },
                    new() { CodigoCE = "CE-014", Alias = "Athena_V", Asistencia = "Llegada tardía", RendimientoGeneral = 91, Comunicacion = 94, Coordinacion = 90, TrabajoEnEquipo = 92, CumplimientoObjetivos = 90, Observaciones = "Incorporada 10 min después por entrevista de admisión.", Resultado = "Aprobado" }
                }
            }
        };

        // ====================================================================
        // 9. RECONOCIMIENTOS (REC-001 A REC-005), INSIGNIAS Y ASCENSOS (ASC-001 A ASC-003)
        // ====================================================================
        Reconocimientos = new List<ReconocimientoInstitucional>
        {
            new()
            {
                Id = "REC-001", Nombre = "Cruz de Honor Táctico Exiliados", Categoria = "Mérito institucional",
                Emblema = "award", Descripcion = "Otorgada por actuación sobresaliente en la dirección y éxito de operaciones conjuntas.",
                Motivo = "Liderazgo aéreo ininterrumpido en la campaña Altis y organización del Departamento de Operaciones.",
                BeneficiarioCodigoCE = "CE-001", BeneficiarioAlias = "Paquito", Fecha = new DateTime(2026, 9, 15),
                AutoridadOtorgante = "CE-015 · Kodiak (Director/Fundador)", Estado = "Vigente"
            },
            new()
            {
                Id = "REC-002", Nombre = "Medalla a la Constancia y Servicio Activo", Categoria = "Servicio y permanencia",
                Emblema = "shield-check", Descripcion = "Reconoce la asistencia perfecta y compromiso continuo con las unidades del clan.",
                Motivo = "Más de 850 horas de servicio táctico y liderazgo de la Unidad de Asalto UE-002.",
                BeneficiarioCodigoCE = "CE-002", BeneficiarioAlias = "Wolf_Alpha", Fecha = new DateTime(2026, 9, 30),
                AutoridadOtorgante = "CE-010 · Halcon_Z (Comandante)", Estado = "Vigente"
            },
            new()
            {
                Id = "REC-003", Nombre = "Estrella de Conducción Estratégica", Categoria = "Liderazgo",
                Emblema = "star", Descripcion = "Distinción reservada a líderes de departamento o unidad por cohesión de grupo.",
                Motivo = "Reestructuración del Centro de Inteligencia y estandarización de comunicaciones.",
                BeneficiarioCodigoCE = "CE-008", BeneficiarioAlias = "Luna_Roja", Fecha = new DateTime(2026, 10, 12),
                AutoridadOtorgante = "CE-015 · Kodiak (Director/Fundador)", Estado = "Vigente"
            },
            new()
            {
                Id = "REC-004", Nombre = "Insignia de Excelencia en Instrucción", Categoria = "Desempeño y formación",
                Emblema = "book-open", Descripcion = "Premia la labor docente en entrenamientos y formación de nuevos operadores.",
                Motivo = "Instrucción de vuelo instrumental y evaluación de aspirantes en Reclutamiento.",
                BeneficiarioCodigoCE = "CE-003", BeneficiarioAlias = "Kira_Vex", Fecha = new DateTime(2026, 10, 20),
                AutoridadOtorgante = "CE-001 · Paquito (Capitán)", Estado = "Vigente"
            },
            new()
            {
                Id = "REC-005", Nombre = "Orden del Caduceo de Combate", Categoria = "Distinción especial",
                Emblema = "heart", Descripcion = "Reconocimiento especial a maniobras de rescate médico de alta dificultad.",
                Motivo = "Rescate íntegro de dos escuadras bajo fuego en la Operación Centinela del Alba.",
                BeneficiarioCodigoCE = "CE-005", BeneficiarioAlias = "Doc_Rivas", Fecha = new DateTime(2026, 10, 28),
                AutoridadOtorgante = "CE-006 · Titan_Nova (Sargento)", Estado = "Vigente"
            }
        };

        InsigniasAdmin = new List<InsigniaEspecialidadAdmin>
        {
            new() { Id = "INS-001", Nombre = "Alas de Oro MILSIM", Tipo = "Insignia de Especialidad Aérea", IntegranteCodigoCE = "CE-001", IntegranteAlias = "Paquito", Justificacion = "Acreditación administrativa por dirección de escuadrón aéreo UE-003.", AutoridadAsignadora = "CE-015 · Kodiak", Fecha = new DateTime(2026, 8, 10), Estado = "Activa" },
            new() { Id = "INS-002", Nombre = "Instructor de Asalto Urbano CQB", Tipo = "Especialidad Táctica", IntegranteCodigoCE = "CE-002", IntegranteAlias = "Wolf_Alpha", Justificacion = "Habilitación administrativa de Mando para dictar cursos CQB.", AutoridadAsignadora = "CE-010 · Halcon_Z", Fecha = new DateTime(2026, 8, 22), Estado = "Activa" },
            new() { Id = "INS-003", Nombre = "Cruz de Sanidad Táctica ACE3", Tipo = "Especialidad Médica", IntegranteCodigoCE = "CE-005", IntegranteAlias = "Doc_Rivas", Justificacion = "Certificación administrativa de soporte vital avanzado en campaña.", AutoridadAsignadora = "CE-006 · Titan_Nova", Fecha = new DateTime(2026, 9, 5), Estado = "Activa" },
            new() { Id = "INS-004", Nombre = "Criptógrafa y Control JTAC", Tipo = "Especialidad de Inteligencia", IntegranteCodigoCE = "CE-008", IntegranteAlias = "Luna_Roja", Justificacion = "Designación administrativa para enlace aéreo y cifrado radial.", AutoridadAsignadora = "CE-015 · Kodiak", Fecha = new DateTime(2026, 9, 18), Estado = "Activa" }
        };

        Ascensos = new List<PropuestaAscenso>
        {
            new()
            {
                Id = "ASC-001", IntegranteCodigoCE = "CE-005", IntegranteAlias = "Doc_Rivas",
                RangoActual = "Cabo", RangoPropuesto = "Sargento",
                Proponente = "CE-006 · Titan_Nova (Sargento)",
                Justificacion = "Desempeño sobresaliente al mando del equipo médico en UE-005 y 300+ horas de servicio activo.",
                Evaluaciones = "ENT-003 Aprobado (95/100) · Condecorado con REC-005.",
                Fecha = DateTime.Today.AddDays(-2),
                AutoridadAprobadora = "Pendiente de firma (Dirección / Mando)",
                Estado = "Pendiente"
            },
            new()
            {
                Id = "ASC-002", IntegranteCodigoCE = "CE-004", IntegranteAlias = "Ghost_07",
                RangoActual = "Operador", RangoPropuesto = "Cabo",
                Proponente = "CE-008 · Luna_Roja (Teniente)",
                Justificacion = "Excelencia técnica como operador RTO en la unidad de reconocimiento UE-001.",
                Evaluaciones = "ENT-002 Aprobado con puntaje máximo en Comunicaciones (100/100).",
                Fecha = DateTime.Today.AddDays(-1),
                AutoridadAprobadora = "Pendiente de firma (Dirección / Mando)",
                Estado = "Pendiente"
            },
            new()
            {
                Id = "ASC-003", IntegranteCodigoCE = "CE-002", IntegranteAlias = "Wolf_Alpha",
                RangoActual = "Cabo", RangoPropuesto = "Sargento",
                Proponente = "CE-001 · Paquito (Capitán)",
                Justificacion = "Liderazgo demostrado en la consolidación de la Unidad de Asalto UE-002.",
                Evaluaciones = "Evaluación anual de mando: 96/100.",
                Fecha = new DateTime(2026, 8, 25),
                AutoridadAprobadora = "CE-010 · Halcon_Z (Comandante)",
                Estado = "Aprobada"
            }
        };

        // ====================================================================
        // 10. GESTIÓN DISCIPLINARIA (DIS-001 A DIS-003)
        // ====================================================================
        Disciplina = new List<RegistroDisciplinario>
        {
            new()
            {
                Id = "DIS-001", IntegranteCodigoCE = "CE-007", IntegranteAlias = "Zorro_X",
                Tipo = "Llamada de atención",
                Motivo = "Uso de canal de radio global sin protocolo breve durante maniobra de práctica.",
                Fecha = new DateTime(2026, 9, 14), Responsable = "CE-008 · Luna_Roja (Teniente)",
                EvidenciaDescriptiva = "Bitácora de radio del entrenamiento introductorio.",
                Duracion = "Amonestación formativa inmediata", Estado = "Cumplida",
                Observaciones = "El recluta corrigió su procedimiento y aprobó el módulo de radio posterior."
            },
            new()
            {
                Id = "DIS-002", IntegranteCodigoCE = "CE-012", IntegranteAlias = "Bruno_Mx",
                Tipo = "Suspensión temporal",
                Motivo = "Inasistencia sin aviso previo a operación logística y entrenamiento ENT-002 teniendo convocatoria confirmada.",
                Fecha = new DateTime(2026, 10, 28), Responsable = "CE-010 · Halcon_Z (Comandante)",
                EvidenciaDescriptiva = "Reporte de asistencia de UE-005 y registro de ENT-002.",
                Duracion = "7 días calendario", Estado = "Vigente",
                Observaciones = "El estado institucional del integrante pasó temporalmente a Suspendido hasta cumplir el plazo."
            },
            new()
            {
                Id = "DIS-003", IntegranteCodigoCE = "CE-013", IntegranteAlias = "Rex_Delta",
                Tipo = "Rehabilitación",
                Motivo = "Cierre y archivo definitivo de advertencia preventiva tras 30 días de conducta intachable.",
                Fecha = new DateTime(2026, 10, 30), Responsable = "CE-015 · Kodiak (Director/Fundador)",
                EvidenciaDescriptiva = "Informe favorable del líder de unidad UE-002.",
                Duracion = "Permanente", Estado = "Cumplida",
                Observaciones = "Expediente rehabilitado sin deméritos activos."
            }
        };

        // ====================================================================
        // 11. ARCHIVO INSTITUCIONAL (ARC-001..) Y AUDITORÍA (AUD-001..)
        // ====================================================================
        Archivo = new List<EntradaArchivoInstitucional>
        {
            new() { Id = "ARC-001", Fecha = new DateTime(2026, 7, 4, 10, 0, 0), Tipo = "Incorporación", IntegranteCodigoCE = "CE-001", IntegranteAlias = "Paquito", Departamento = "Operaciones", Unidad = "UE-003", Descripcion = "Alta fundacional del expediente #1 (Paquito — Capitán / Piloto / Militares).", Autoridad = "CE-015 · Kodiak" },
            new() { Id = "ARC-002", Fecha = new DateTime(2026, 7, 12, 11, 30, 0), Tipo = "Incorporación", IntegranteCodigoCE = "CE-002", IntegranteAlias = "Wolf_Alpha", Departamento = "Operaciones", Unidad = "UE-002", Descripcion = "Alta del expediente #2 (Wolf_Alpha — Sargento / Infantería / Mercenarios).", Autoridad = "CE-015 · Kodiak" },
            new() { Id = "ARC-003", Fecha = new DateTime(2026, 7, 23, 15, 45, 0), Tipo = "Incorporación", IntegranteCodigoCE = "CE-003", IntegranteAlias = "Kira_Vex", Departamento = "Operaciones", Unidad = "UE-003", Descripcion = "Alta del expediente #3 (Kira_Vex — Teniente / Piloto / Terroristas).", Autoridad = "CE-010 · Halcon_Z" },
            new() { Id = "ARC-004", Fecha = new DateTime(2026, 8, 25, 19, 0, 0), Tipo = "Ascenso", IntegranteCodigoCE = "CE-002", IntegranteAlias = "Wolf_Alpha", Departamento = "Operaciones", Unidad = "UE-002", Descripcion = "Ascenso aprobado ASC-003 de Cabo a Sargento y jefatura de UE-002.", Autoridad = "CE-010 · Halcon_Z" },
            new() { Id = "ARC-005", Fecha = new DateTime(2026, 9, 15, 20, 0, 0), Tipo = "Reconocimiento", IntegranteCodigoCE = "CE-001", IntegranteAlias = "Paquito", Departamento = "Operaciones", Unidad = "UE-003", Descripcion = "Otorgamiento de la Cruz de Honor Táctico Exiliados (REC-001).", Autoridad = "CE-015 · Kodiak" },
            new() { Id = "ARC-006", Fecha = new DateTime(2026, 10, 28, 21, 15, 0), Tipo = "Disciplina", IntegranteCodigoCE = "CE-012", IntegranteAlias = "Bruno_Mx", Departamento = "Logística", Unidad = "UE-005", Descripcion = "Aplicación de suspensión temporal de 7 días (DIS-002) por inasistencia.", Autoridad = "CE-010 · Halcon_Z" }
        };

        Auditoria = new List<RegistroAuditoria>
        {
            new() { Id = "AUD-001", UsuarioEjecutor = "CE-015 · Kodiak (Dirección)", RegistroModificado = "Integrante CE-002 (Wolf_Alpha)", ValorAnterior = "Rango: Cabo", ValorNuevo = "Rango: Sargento", FechaHora = new DateTime(2026, 8, 25, 19, 2, 0), Motivo = "Resolución aprobatoria de propuesta de ascenso ASC-003", TipoOperacion = "Cambio de Rango" },
            new() { Id = "AUD-002", UsuarioEjecutor = "CE-010 · Halcon_Z (Mando)", RegistroModificado = "Integrante CE-012 (Bruno_Mx)", ValorAnterior = "EstadoInstitucional: Activo", ValorNuevo = "EstadoInstitucional: Suspendido", FechaHora = new DateTime(2026, 10, 28, 21, 16, 0), Motivo = "Medida disciplinaria temporal DIS-002", TipoOperacion = "Cambio de Estado" },
            new() { Id = "AUD-003", UsuarioEjecutor = "CE-014 · Athena_V (Mando)", RegistroModificado = "Solicitud ADM-003 (Shadow_Kid)", ValorAnterior = "EstadoAdmision: Pendiente", ValorNuevo = "EstadoAdmision: Rechazada", FechaHora = DateTime.Today.AddDays(-2).AddHours(18), Motivo = "Incumplimiento de edad mínima obligatoria (16 < 18 años)", TipoOperacion = "Filtro de Admisión" },
            new() { Id = "AUD-004", UsuarioEjecutor = "CE-015 · Kodiak (Dirección)", RegistroModificado = "Configuración Institucional", ValorAnterior = "DeteccionSimultaneidadCalendario: Inactivo", ValorNuevo = "DeteccionSimultaneidadCalendario: Activo", FechaHora = DateTime.Today.AddDays(-1).AddHours(10), Motivo = "Activación de alerta de cruce de horarios en Calendario Institucional", TipoOperacion = "Configuración" }
        };

        // ====================================================================
        // 12. COMUNICACIONES INSTITUCIONALES (COM-001 A COM-004)
        // ====================================================================
        Comunicaciones = new List<ComunicadoInstitucional>
        {
            new()
            {
                Id = "COM-001", Titulo = "Orden de Despliegue General — Operación Lanza de Obsidiana (OP-001)",
                Contenido = "Se convoca a las unidades UE-001, UE-002 y UE-003 a presentarse en sala de briefing 20 minutos antes de las 20:00 hrs. Verificar sincronización de repositorios de Arma 3 y frecuencias TFAR asignadas.",
                Categoria = "Operaciones", Prioridad = "Urgente", Autor = "CE-001 · Paquito (Capitán)",
                Departamento = "Operaciones", FechaHora = DateTime.Now.AddHours(-3),
                Destinatarios = "Unidades UE-001, UE-002 y UE-003", Estado = "Publicado", LeidoDemostrativo = false
            },
            new()
            {
                Id = "COM-002", Titulo = "Actualización del Protocolo de Admisión y Mayoría de Edad (+18)",
                Contenido = "El Departamento de Reclutamiento recuerda a todos los evaluadores que la edad mínima obligatoria de 18 años cumplidos es un requisito excluyente. Asimismo, toda solicitud aprobada debe completar su Ceremonia Digital de Incorporación para emitir la credencial CE-###.",
                Categoria = "Reclutamiento", Prioridad = "Importante", Autor = "CE-014 · Athena_V (Teniente)",
                Departamento = "Reclutamiento", FechaHora = DateTime.Now.AddDays(-1),
                Destinatarios = "Toda la institución", Estado = "Publicado", LeidoDemostrativo = true
            },
            new()
            {
                Id = "COM-003", Titulo = "Aviso de Simultaneidad Horaria en Calendario (OP-002 y OP-003)",
                Contenido = "El Estado Mayor informa que las operaciones OP-002 (FiveM) y OP-003 (Arma Reforger) coinciden en la franja de las 21:00 hrs. Los líderes de unidad ya han distribuido las escuadras para cubrir ambos frentes sin solapamiento de personal.",
                Categoria = "Comando", Prioridad = "Importante", Autor = "CE-010 · Halcon_Z (Comandante)",
                Departamento = "Comando", FechaHora = DateTime.Now.AddDays(-2),
                Destinatarios = "Departamentos de Comando y Operaciones", Estado = "Publicado", LeidoDemostrativo = true
            },
            new()
            {
                Id = "COM-004", Titulo = "Borrador de Directiva Logística Trimestral",
                Contenido = "Propuesta de estandarización de kits médicos ACE3 y dotación de munición para tiradores designados.",
                Categoria = "Logística", Prioridad = "Informativo", Autor = "CE-006 · Titan_Nova (Sargento)",
                Departamento = "Logística", FechaHora = DateTime.Now.AddDays(-4),
                Destinatarios = "Departamento de Logística", Estado = "Borrador", LeidoDemostrativo = false
            }
        };

        // ====================================================================
        // 13. CENTRO DOCUMENTAL (DOC-001 A DOC-008 — 8 CATEGORÍAS)
        // ====================================================================
        Documentos = new List<DocumentoInstitucional>
        {
            new() { Id = "DOC-001", Titulo = "Estatuto General y Código de Conducta de Clan Exiliados", Categoria = "Reglamentos", Descripcion = "Normativa rectora de convivencia, respeto, cadena de mando y distinción entre ficción RP y realidad.", Etiquetas = new() { "Estatuto", "Disciplina", "Obligatorio" }, Autor = "CE-015 · Kodiak", Responsable = "Comando", FechaPublicacion = new DateTime(2026, 1, 10), FechaActualizacion = new DateTime(2026, 10, 1), NivelAcceso = "General", Estado = "Publicado", VistaPreviaContenido = "ARTÍCULO 1: Clan Exiliados es una institución ficticia dedicada a la simulación militar (MILSIM) y el Roleplay táctico. ARTÍCULO 2: Es requisito indispensable contar con 18 años cumplidos al momento de postular. ARTÍCULO 3: Los antecedentes RP son estrictamente narrativos dentro de videojuegos." },
            new() { Id = "DOC-002", Titulo = "Manual de Escalafón, Insignias y los 8 Rangos Oficiales", Categoria = "Rangos", Descripcion = "Definición de niveles jerárquicos desde Recluta hasta Director/Fundador, facultades y criterios de ascenso.", Etiquetas = new() { "Rangos", "Jerarquía", "Insignias" }, Autor = "CE-010 · Halcon_Z", Responsable = "Comando", FechaPublicacion = new DateTime(2026, 2, 5), FechaActualizacion = new DateTime(2026, 9, 20), NivelAcceso = "General", Estado = "Publicado", VistaPreviaContenido = "ESCALAFÓN OFICIAL: 1. Director/Fundador · 2. Comandante · 3. Capitán · 4. Teniente · 5. Sargento · 6. Cabo · 7. Operador · 8. Recluta. Ningún rango proveniente de otro servidor externo se homologa automáticamente." },
            new() { Id = "DOC-003", Titulo = "Estructura Orgánica de los Cinco Departamentos", Categoria = "Departamentos", Descripcion = "Funciones y coordinación entre Comando, Operaciones, Inteligencia, Logística y Reclutamiento.", Etiquetas = new() { "Departamentos", "Organización" }, Autor = "CE-015 · Kodiak", Responsable = "Comando", FechaPublicacion = new DateTime(2026, 2, 18), FechaActualizacion = new DateTime(2026, 8, 15), NivelAcceso = "General", Estado = "Publicado", VistaPreviaContenido = "La matriz institucional se divide en 5 departamentos especializados. Los oficiales de mando pueden integrar más de un departamento para garantizar la cohesión operativa." },
            new() { Id = "DOC-004", Titulo = "Doctrina de Empleo Táctico de Unidades (UE-001 a UE-005)", Categoria = "Unidades", Descripcion = "Procedimientos operativos estándar (SOP) para Reconocimiento, Asalto, Apoyo Aéreo, Seguridad y Logística.", Etiquetas = new() { "Unidades", "SOP", "Táctica" }, Autor = "CE-001 · Paquito", Responsable = "Operaciones", FechaPublicacion = new DateTime(2026, 3, 12), FechaActualizacion = new DateTime(2026, 10, 5), NivelAcceso = "Operativo", Estado = "Publicado", VistaPreviaContenido = "Cada unidad táctica (UE-001 a UE-005) responde a un departamento principal pero actúa de forma integrada en los despliegues de Arma 3, Arma Reforger y FiveM." },
            new() { Id = "DOC-005", Titulo = "Guía de Evaluación de Aspirantes y Entrevista Táctica", Categoria = "Reclutamiento", Descripcion = "Parámetros de revisión de solicitudes, filtro de edad (+18) y rúbrica de entrevista.", Etiquetas = new() { "Admisión", "Entrevista", "+18" }, Autor = "CE-014 · Athena_V", Responsable = "Reclutamiento", FechaPublicacion = new DateTime(2026, 4, 2), FechaActualizacion = new DateTime(2026, 10, 18), NivelAcceso = "Mando y Reclutadores", Estado = "Publicado", VistaPreviaContenido = "FLUJO DE ADMISIÓN: 1. Registro -> 2. Verificación de edad (>= 18) -> 3. Evaluación técnica -> 4. Entrevista -> 5. Decisión e Incorporación con código CE-###." },
            new() { Id = "DOC-006", Titulo = "Protocolo de Incorporación y Emisión de Credencial CE", Categoria = "Incorporación", Descripcion = "Pasos para la asignación de identificador CE-###, departamento, unidad y ceremonia de bienvenida.", Etiquetas = new() { "Credencial", "Ceremonia", "CE" }, Autor = "CE-014 · Athena_V", Responsable = "Reclutamiento", FechaPublicacion = new DateTime(2026, 5, 1), FechaActualizacion = new DateTime(2026, 10, 10), NivelAcceso = "General", Estado = "Publicado", VistaPreviaContenido = "Todo aspirante aprobado recibe su identificador único CE-### y su credencial digital institucional durante la Ceremonia Digital de Incorporación." },
            new() { Id = "DOC-007", Titulo = "Programa Curricular de Entrenamientos y Evaluaciones", Categoria = "Entrenamientos", Descripcion = "Módulos de instrucción en MILSIM, Coordinación, Comunicación, Roleplay y Formación interna.", Etiquetas = new() { "Instrucción", "ACE3", "TFAR" }, Autor = "CE-002 · Wolf_Alpha", Responsable = "Operaciones", FechaPublicacion = new DateTime(2026, 6, 14), FechaActualizacion = new DateTime(2026, 10, 22), NivelAcceso = "General", Estado = "Publicado", VistaPreviaContenido = "Los entrenamientos evalúan 5 ejes: Rendimiento general, Comunicación, Coordinación, Trabajo en equipo y Cumplimiento de objetivos. La aprobación no otorga insignias automáticas." },
            new() { Id = "DOC-008", Titulo = "Compendio de Campañas Históricas 2025-2026", Categoria = "Archivo histórico", Descripcion = "Memoria institucional de las operaciones fundacionales de las facciones Militares, Mercenarios y Terroristas.", Etiquetas = new() { "Historia", "Facciones", "PA2" }, Autor = "CE-008 · Luna_Roja", Responsable = "Inteligencia", FechaPublicacion = new DateTime(2026, 7, 1), FechaActualizacion = new DateTime(2026, 10, 30), NivelAcceso = "General", Estado = "Publicado", VistaPreviaContenido = "Registro histórico de la evolución desde el Sistema de Admisión Militar en Windows Forms (PA2) hacia la Plataforma Institucional Web en ASP.NET Core MVC (PA3)." }
        };

        // ====================================================================
        // 14. NOTIFICACIONES, ALERTAS DINÁMICAS Y ACTIVITY FEED
        // ====================================================================
        Notificaciones = new List<NotificacionSistema>
        {
            new() { Id = "NOT-001", Categoria = "Solicitudes", Prioridad = "Importante", Titulo = "Solicitud aprobada lista para incorporar (ADM-002)", Mensaje = "La aspirante Valkyria_09 ha superado la entrevista y espera su incorporación CE-###.", FechaHora = DateTime.Now.AddMinutes(-25), Leida = false, EnlaceUrl = "/Reclutamiento" },
            new() { Id = "NOT-002", Categoria = "Comunicados", Prioridad = "Urgente", Titulo = "Comunicado urgente publicado (COM-001)", Mensaje = "Orden de Despliegue General para la Operación Lanza de Obsidiana hoy a las 20:00 hrs.", FechaHora = DateTime.Now.AddHours(-2), Leida = false, EnlaceUrl = "/Comunicaciones" },
            new() { Id = "NOT-003", Categoria = "Ascensos", Prioridad = "Importante", Titulo = "2 propuestas de ascenso pendientes de firma", Mensaje = "ASC-001 (Doc_Rivas a Sargento) y ASC-002 (Ghost_07 a Cabo) aguardan resolución de Mando.", FechaHora = DateTime.Now.AddHours(-5), Leida = false, EnlaceUrl = "/Gobernanza/Ascensos" },
            new() { Id = "NOT-004", Categoria = "Actividades", Prioridad = "Normal", Titulo = "Entrenamiento ENT-001 programado para mañana", Mensaje = "Curso Básico de Doctrina MILSIM a cargo del Sargento Wolf_Alpha.", FechaHora = DateTime.Now.AddHours(-12), Leida = true, EnlaceUrl = "/Operaciones/Entrenamientos" }
        };

        Alertas = new List<AlertaDinamica>
        {
            new() { Id = "ALT-001", Nivel = "Crítica", Titulo = "Comunicado urgente activo: Despliegue OP-001 en curso", Detalle = "Operación Lanza de Obsidiana requiere verificación de convocados en el Centro de Operaciones.", ModuloOrigen = "Operaciones / Comunicaciones", EnlaceAccion = "/Operaciones", Atendida = false, FechaGeneracion = DateTime.Now.AddHours(-1) },
            new() { Id = "ALT-002", Nivel = "Importante", Titulo = "Simultaneidad detectada en Calendario Institucional", Detalle = "Las operaciones OP-002 (FiveM) y OP-003 (Arma Reforger) coinciden el mismo día de 21:00 a 23:00 hrs.", ModuloOrigen = "Calendario Institucional", EnlaceAccion = "/Operaciones/Calendario", Atendida = false, FechaGeneracion = DateTime.Now.AddHours(-3) },
            new() { Id = "ALT-003", Nivel = "Importante", Titulo = "Solicitudes pendientes de evaluación e incorporación", Detalle = "ADM-001 (en entrevista), ADM-002 (lista para incorporar) y ADM-004 (pendiente de evaluación).", ModuloOrigen = "Reclutamiento", EnlaceAccion = "/Reclutamiento", Atendida = false, FechaGeneracion = DateTime.Now.AddHours(-4) },
            new() { Id = "ALT-004", Nivel = "Importante", Titulo = "Propuestas de ascenso pendientes de decisión (ASC-001 y ASC-002)", Detalle = "Dos expedientes aguardan aprobación administrativa de Dirección o Mando.", ModuloOrigen = "Ascensos", EnlaceAccion = "/Gobernanza/Ascensos", Atendida = false, FechaGeneracion = DateTime.Now.AddHours(-6) },
            new() { Id = "ALT-005", Nivel = "Informativa", Titulo = "Entrenamiento próximo: ENT-001 Doctrina MILSIM", Detalle = "Programado para mañana a las 19:00 hrs en servidor Arma 3.", ModuloOrigen = "Entrenamientos", EnlaceAccion = "/Operaciones/Entrenamientos", Atendida = false, FechaGeneracion = DateTime.Now.AddHours(-8) }
        };

        ActivityFeed = new List<EventoActivityFeed>
        {
            new() { Id = "ACT-001", Icono = "radio", Tipo = "Comunicado", Descripcion = "Se publicó el comunicado urgente COM-001: Orden de Despliegue OP-001.", Responsable = "CE-001 · Paquito", FechaHora = DateTime.Now.AddHours(-2), EnlaceRegistro = "/Comunicaciones" },
            new() { Id = "ACT-002", Icono = "user-check", Tipo = "Reclutamiento", Descripcion = "La solicitud ADM-002 (Valkyria_09) fue calificada como Aprobada tras entrevista.", Responsable = "CE-014 · Athena_V", FechaHora = DateTime.Now.AddHours(-5), EnlaceRegistro = "/Reclutamiento" },
            new() { Id = "ACT-003", Icono = "trending-up", Tipo = "Ascenso", Descripcion = "Se registró la propuesta de ascenso ASC-002 para el operador Ghost_07 (CE-004).", Responsable = "CE-008 · Luna_Roja", FechaHora = DateTime.Now.AddDays(-1), EnlaceRegistro = "/Gobernanza/Ascensos" },
            new() { Id = "ACT-004", Icono = "award", Tipo = "Reconocimiento", Descripcion = "Se otorgó la Orden del Caduceo de Combate (REC-005) al Cabo Doc_Rivas (CE-005).", Responsable = "CE-006 · Titan_Nova", FechaHora = DateTime.Now.AddDays(-3), EnlaceRegistro = "/Gobernanza/Reconocimientos" },
            new() { Id = "ACT-005", Icono = "shield-alert", Tipo = "Administración", Descripcion = "Se registró la medida disciplinaria temporal DIS-002 en el expediente CE-012.", Responsable = "CE-010 · Halcon_Z", FechaHora = DateTime.Now.AddDays(-5), EnlaceRegistro = "/Gobernanza/Disciplina" },
            new() { Id = "ACT-006", Icono = "crosshair", Tipo = "Operación", Descripcion = "Finalizó con éxito la Operación Centinela del Alba (OP-004) en Arma 3.", Responsable = "CE-001 · Paquito", FechaHora = DateTime.Now.AddDays(-6), EnlaceRegistro = "/Operaciones" }
        };

        ActualizarCatalogoReportes();
    }

    public void ActualizarCatalogoReportes()
    {
        Reportes = new List<ReporteDefinicion>
        {
            new() { Id = "REP-001", Titulo = "Reporte General de Personal y Escalafón CE", Categoria = "Personal", Descripcion = "Estado de fuerza completo con códigos CE, rangos, departamentos, unidades, horas MILSIM y disponibilidad.", Responsable = "Comando / Personal", TotalRegistros = Integrantes.Count },
            new() { Id = "REP-002", Titulo = "Reporte de Operaciones y Convocatorias Tácticas", Categoria = "Operaciones", Descripcion = "Despliegues programados, en curso y finalizados por plataforma y departamento responsable.", Responsable = "Departamento de Operaciones", TotalRegistros = Operaciones.Count },
            new() { Id = "REP-003", Titulo = "Reporte de Entrenamientos, Asistencia y Evaluaciones", Categoria = "Entrenamientos", Descripcion = "Estadísticas de instrucción, promedios de rendimiento y resultados de certificación.", Responsable = "División de Instrucción", TotalRegistros = Entrenamientos.Count },
            new() { Id = "REP-004", Titulo = "Reporte de Estructura: Departamentos y Unidades", Categoria = "Departamentos y Unidades", Descripcion = "Distribución de efectivos en los 5 departamentos y las 5 unidades tácticas (UE-001 a UE-005).", Responsable = "Estado Mayor", TotalRegistros = Departamentos.Count + Unidades.Count },
            new() { Id = "REP-005", Titulo = "Reporte de Reconocimientos e Insignias Administrativas", Categoria = "Reconocimientos", Descripcion = "Condecoraciones vigentes (REC) e insignias otorgadas por Dirección y Mando.", Responsable = "Comando", TotalRegistros = Reconocimientos.Count + InsigniasAdmin.Count },
            new() { Id = "REP-006", Titulo = "Reporte de Propuestas y Resoluciones de Ascenso", Categoria = "Ascensos", Descripcion = "Seguimiento de ascensos pendientes, aprobados y rechazados en el escalafón.", Responsable = "Comando / Mando", TotalRegistros = Ascensos.Count },
            new() { Id = "REP-007", Titulo = "Reporte de Archivo Histórico y Auditoría Institucional", Categoria = "Archivo histórico", Descripcion = "Bitácora cronológica de movimientos institucionales y trazabilidad de auditoría.", Responsable = "Inteligencia / Comando", TotalRegistros = Archivo.Count + Auditoria.Count },
            new() { Id = "REP-008", Titulo = "Reporte de Plataformas, Servidores y Antecedentes RP", Categoria = "Plataformas y servidores", Descripcion = "Participación en Arma 3, Arma Reforger y GTA V / FiveM, junto al registro de antecedentes RP ficticios.", Responsable = "Departamento de Inteligencia", TotalRegistros = Servidores.Count + Integrantes.Sum(i => i.AntecedentesRP.Count) }
        };
    }

    // ========================================================================
    // ACCIONES SINCRONIZADAS EN MEMORIA (RECLUTAMIENTO, ASCENSOS, OPERACIONES)
    // ========================================================================

    public (bool Exito, string Mensaje, SolicitudReclutamiento? Solicitud) RegistrarAspirante(
        string nombre, string alias, string discord, int edad, int horasMilsim,
        string faccionPA2, string disponibilidadPA2, string experienciaMilsim, string experienciaRP,
        List<string> plataformas, string servidoresAnteriores, List<string> especialidades,
        string descripcionPersonal, string antecedentesRP)
    {
        lock (_lock)
        {
            var nuevoId = $"ADM-{SolicitudesReclutamiento.Count + 1:D3}";
            var esMayorEdad = edad >= 18;

            var solicitud = new SolicitudReclutamiento
            {
                Id = nuevoId,
                NumeroPA2 = Integrantes.Count + SolicitudesReclutamiento.Count + 1,
                Nombre = string.IsNullOrWhiteSpace(nombre) ? alias : nombre.Trim(),
                Alias = alias.Trim(),
                Discord = discord.Trim(),
                Edad = edad,
                HorasMilsim = Math.Max(0, horasMilsim),
                RangoDeseadoPA2 = "Recluta",
                FaccionPA2 = string.IsNullOrWhiteSpace(faccionPA2) ? "Militares" : faccionPA2,
                DisponibilidadPA2 = string.IsNullOrWhiteSpace(disponibilidadPA2) ? "Noches" : disponibilidadPA2,
                ExperienciaMilsim = string.IsNullOrWhiteSpace(experienciaMilsim) ? $"{horasMilsim} horas en simulación táctica." : experienciaMilsim.Trim(),
                ExperienciaRoleplay = string.IsNullOrWhiteSpace(experienciaRP) ? "Experiencia en servidores tácticos." : experienciaRP.Trim(),
                Plataformas = plataformas.Any() ? plataformas : new List<string> { "Arma 3" },
                ServidoresAnteriores = string.IsNullOrWhiteSpace(servidoresAnteriores) ? "Comunidad externa" : servidoresAnteriores.Trim(),
                Especialidades = especialidades.Any() ? especialidades : new List<string> { "Infantería" },
                DescripcionPersonal = string.IsNullOrWhiteSpace(descripcionPersonal) ? "Aspirante registrado desde la plataforma web." : descripcionPersonal.Trim(),
                AntecedentesRP = string.IsNullOrWhiteSpace(antecedentesRP) ? "Rol táctico militar ficticio." : antecedentesRP.Trim(),
                FechaSolicitud = DateTime.Today,
                EtapaFlujo = esMayorEdad ? "Evaluación" : "Verificación de edad",
                EstadoAdmision = esMayorEdad ? "Pendiente" : "Rechazada",
                CriteriosEvaluacion = esMayorEdad
                    ? $"Mayoría de edad verificada ({edad} años >= 18). Apto para evaluación técnica."
                    : $"RECHAZO AUTOMÁTICO: Edad declarada ({edad} años) inferior al mínimo obligatorio de 18 años cumplidos.",
                ObservacionesEvaluacion = esMayorEdad
                    ? "Expediente en revisión por el Departamento de Reclutamiento."
                    : "Inadmisión inmediata conforme al Estatuto Institucional (requisito +18).",
                EntrevistaFicticia = esMayorEdad ? "Pendiente de programar" : "Cancelada por minoría de edad",
                EvaluacionExperiencia = $"{horasMilsim} horas declaradas",
                DecisionInstitucional = esMayorEdad ? "Pendiente de evaluación" : "Rechazada por requisito de edad (< 18 años)"
            };

            SolicitudesReclutamiento.Insert(0, solicitud);

            if (!esMayorEdad)
            {
                RegistrarAuditoriaInterna("Sistema de Admisión (+18)", $"Solicitud {nuevoId} ({alias})", "Registro inicial", "EstadoAdmision: Rechazada", $"Edad ({edad}) menor a 18 años obligatorios", "Rechazo por Edad");
                return (false, $"La solicitud {nuevoId} fue registrada con estado RECHAZADA: la edad mínima obligatoria en Clan Exiliados es de 18 años cumplidos (edad ingresada: {edad}).", solicitud);
            }

            Notificaciones.Insert(0, new NotificacionSistema
            {
                Id = $"NOT-{Notificaciones.Count + 1:D3}",
                Categoria = "Solicitudes",
                Prioridad = "Importante",
                Titulo = $"Nueva solicitud de admisión: {alias} ({nuevoId})",
                Mensaje = $"Edad verificada ({edad} años). Especialidades: {string.Join(", ", solicitud.Especialidades)}.",
                FechaHora = DateTime.Now,
                Leida = false,
                EnlaceUrl = "/Reclutamiento"
            });

            ActivityFeed.Insert(0, new EventoActivityFeed
            {
                Id = $"ACT-{ActivityFeed.Count + 1:D3}",
                Icono = "user-plus",
                Tipo = "Reclutamiento",
                Descripcion = $"Nuevo aspirante registrado: {alias} ({nuevoId}) — Facción {solicitud.FaccionPA2}.",
                Responsable = "Departamento de Reclutamiento",
                FechaHora = DateTime.Now,
                EnlaceRegistro = "/Reclutamiento"
            });

            return (true, $"Solicitud {nuevoId} de {alias} registrada correctamente. Mayoría de edad verificada ({edad} años).", solicitud);
        }
    }

    public bool EvaluarSolicitud(string idSolicitud, string nuevoEstado, string etapa, string observaciones, string entrevista, string decision)
    {
        lock (_lock)
        {
            var sol = SolicitudesReclutamiento.FirstOrDefault(s => s.Id == idSolicitud);
            if (sol == null) return false;

            if (sol.Edad < 18 && nuevoEstado == "Aprobada")
            {
                return false; // Regla infranqueable: menores de 18 no pueden ser aprobados
            }

            var estadoAnterior = sol.EstadoAdmision;
            sol.EstadoAdmision = nuevoEstado;
            sol.EtapaFlujo = etapa;
            if (!string.IsNullOrWhiteSpace(observaciones)) sol.ObservacionesEvaluacion = observaciones;
            if (!string.IsNullOrWhiteSpace(entrevista)) sol.EntrevistaFicticia = entrevista;
            if (!string.IsNullOrWhiteSpace(decision)) sol.DecisionInstitucional = decision;

            RegistrarAuditoriaInterna($"Perfil {PerfilVisualActual}", $"Solicitud {sol.Id} ({sol.Alias})", $"Estado: {estadoAnterior}", $"Estado: {nuevoEstado} ({etapa})", "Evaluación de admisión", "Evaluación Reclutamiento");

            Notificaciones.Insert(0, new NotificacionSistema
            {
                Id = $"NOT-{Notificaciones.Count + 1:D3}",
                Categoria = "Evaluaciones",
                Prioridad = nuevoEstado == "Aprobada" ? "Importante" : "Normal",
                Titulo = $"Solicitud {sol.Id} ({sol.Alias}) actualizada a {nuevoEstado}",
                Mensaje = $"Etapa actual: {etapa}.",
                FechaHora = DateTime.Now,
                Leida = false,
                EnlaceUrl = "/Reclutamiento"
            });

            return true;
        }
    }

    public IntegranteCE? IncorporarAspiranteAprobado(
        string idSolicitud, string rangoInicial, List<string> departamentosAsignados,
        List<string> unidadesAsignadas, List<string> especialidadesAsignadas)
    {
        lock (_lock)
        {
            var sol = SolicitudesReclutamiento.FirstOrDefault(s => s.Id == idSolicitud);
            if (sol == null || sol.Edad < 18) return null;

            if (sol.Incorporado && !string.IsNullOrEmpty(sol.CodigoCEAsignado))
            {
                return Integrantes.FirstOrDefault(i => i.CodigoCE == sol.CodigoCEAsignado);
            }

            var siguienteNumero = Integrantes.Count + 1;
            var codigoCE = $"CE-{siguienteNumero:D3}";

            var depts = departamentosAsignados != null && departamentosAsignados.Any()
                ? departamentosAsignados
                : new List<string> { "Operaciones" };

            var unids = unidadesAsignadas != null && unidadesAsignadas.Any()
                ? unidadesAsignadas
                : new List<string> { "UE-001" };

            var esps = especialidadesAsignadas != null && especialidadesAsignadas.Any()
                ? especialidadesAsignadas
                : (sol.Especialidades.Any() ? sol.Especialidades : new List<string> { "Infantería" });

            var nuevoIntegrante = new IntegranteCE
            {
                NumeroPA2 = siguienteNumero,
                CodigoCE = codigoCE,
                Nombre = sol.Nombre,
                Alias = sol.Alias,
                Discord = sol.Discord,
                Edad = sol.Edad,
                Rango = string.IsNullOrWhiteSpace(rangoInicial) ? "Recluta" : rangoInicial,
                FaccionPA2 = string.IsNullOrWhiteSpace(sol.FaccionPA2) ? "Militares" : sol.FaccionPA2,
                TurnoDisponibilidadPA2 = string.IsNullOrWhiteSpace(sol.DisponibilidadPA2) ? "Noches" : sol.DisponibilidadPA2,
                Departamentos = depts,
                Unidades = unids,
                FechaIngreso = DateTime.Today,
                Especialidades = esps,
                InsigniasAdministrativas = new List<string> { "Credencial CE Emitida" },
                HorasMilsim = sol.HorasMilsim,
                ExperienciaMilsim = sol.ExperienciaMilsim,
                ExperienciaRP = sol.ExperienciaRoleplay,
                Observaciones = $"Incorporado mediante flujo institucional desde la solicitud {sol.Id}.",
                EstadoInstitucional = "Activo",
                Disponibilidad = "Disponible",
                SupervisorCodigoCE = "CE-002",
                HistorialInstitucional = new List<string>
                {
                    $"{DateTime.Today:dd/MM/yyyy} — Incorporación oficial con código {codigoCE} (Solicitud {sol.Id})",
                    $"{DateTime.Today:dd/MM/yyyy} — Asignación inicial: Rango {rangoInicial}, Depto. {string.Join(", ", depts)}, Unidad {string.Join(", ", unids)}"
                },
                AntecedentesRP = new List<AntecedenteRP>
                {
                    new()
                    {
                        Id = $"ANT-{Integrantes.Sum(x => x.AntecedentesRP.Count) + 1:D3}",
                        CodigoCE = codigoCE,
                        AliasIntegrante = sol.Alias,
                        Plataforma = sol.Plataformas.FirstOrDefault() ?? "Arma 3",
                        Servidor = sol.ServidoresAnteriores,
                        Comunidad = "Declarado en Admisión",
                        PersonajeOAlias = sol.Alias,
                        CategoriaAntecedente = "Militares",
                        TipoRolInterpretado = sol.AntecedentesRP,
                        Periodo = "Previo al ingreso",
                        Experiencia = $"{sol.HorasMilsim} h MILSIM",
                        Responsabilidades = "Operador táctico",
                        Actividades = "Simulación y roleplay",
                        Descripcion = sol.AntecedentesRP,
                        Observaciones = "Antecedente registrado durante la incorporación."
                    }
                }
            };

            Integrantes.Add(nuevoIntegrante);

            foreach (var dName in depts)
            {
                var d = Departamentos.FirstOrDefault(x => x.Nombre.Equals(dName, StringComparison.OrdinalIgnoreCase));
                if (d != null && !d.IntegrantesCodigos.Contains(codigoCE))
                    d.IntegrantesCodigos.Add(codigoCE);
            }

            foreach (var uId in unids)
            {
                var u = Unidades.FirstOrDefault(x => x.Id.Equals(uId, StringComparison.OrdinalIgnoreCase));
                if (u != null && !u.IntegrantesCodigos.Contains(codigoCE))
                {
                    u.IntegrantesCodigos.Add(codigoCE);
                    u.Historial.Insert(0, $"{DateTime.Today:dd/MM/yyyy} — Incorporación del integrante {codigoCE} ({sol.Alias}).");
                }
            }

            sol.EstadoAdmision = "Aprobada";
            sol.EtapaFlujo = "Decisión";
            sol.Incorporado = true;
            sol.CodigoCEAsignado = codigoCE;
            sol.DecisionInstitucional = $"INCORPORADO OFICIALMENTE CON CÓDIGO {codigoCE}";

            Archivo.Insert(0, new EntradaArchivoInstitucional
            {
                Id = $"ARC-{Archivo.Count + 1:D3}",
                Fecha = DateTime.Now,
                Tipo = "Incorporación",
                IntegranteCodigoCE = codigoCE,
                IntegranteAlias = sol.Alias,
                Departamento = depts.FirstOrDefault() ?? "Operaciones",
                Unidad = unids.FirstOrDefault() ?? "UE-001",
                Descripcion = $"Ceremonia digital de incorporación completada. Código asignado: {codigoCE} · Rango: {nuevoIntegrante.Rango}.",
                Autoridad = $"Perfil {PerfilVisualActual} · Reclutamiento"
            });

            RegistrarAuditoriaInterna($"Perfil {PerfilVisualActual}", $"Expediente {codigoCE} ({sol.Alias})", $"Solicitud {sol.Id} (Aprobada)", $"Integrante Activo {codigoCE}", "Incorporación automática demostrativa", "Alta de Personal");

            Notificaciones.Insert(0, new NotificacionSistema
            {
                Id = $"NOT-{Notificaciones.Count + 1:D3}",
                Categoria = "Incorporaciones",
                Prioridad = "Importante",
                Titulo = $"Nuevo integrante incorporado: {codigoCE} · {sol.Alias}",
                Mensaje = $"Asignado a {string.Join(", ", depts)} ({string.Join(", ", unids)}) con rango {nuevoIntegrante.Rango}.",
                FechaHora = DateTime.Now,
                Leida = false,
                EnlaceUrl = $"/Personal/Expediente/{codigoCE}"
            });

            ActivityFeed.Insert(0, new EventoActivityFeed
            {
                Id = $"ACT-{ActivityFeed.Count + 1:D3}",
                Icono = "shield-check",
                Tipo = "Integración",
                Descripcion = $"Ceremonia de incorporación: {sol.Alias} recibió la credencial {codigoCE} ({nuevoIntegrante.Rango}).",
                Responsable = "Departamento de Reclutamiento",
                FechaHora = DateTime.Now,
                EnlaceRegistro = $"/Personal/Expediente/{codigoCE}"
            });

            var alertaRec = Alertas.FirstOrDefault(a => a.Id == "ALT-003");
            if (alertaRec != null && !SolicitudesReclutamiento.Any(s => !s.Incorporado && s.EstadoAdmision != "Rechazada"))
            {
                alertaRec.Atendida = true;
            }

            ActualizarCatalogoReportes();
            return nuevoIntegrante;
        }
    }

    public bool ResolverPropuestaAscenso(string idAscenso, bool aprobar, string motivoResolucion)
    {
        lock (_lock)
        {
            var asc = Ascensos.FirstOrDefault(a => a.Id == idAscenso);
            if (asc == null || asc.Estado != "Pendiente") return false;

            asc.Estado = aprobar ? "Aprobada" : "Rechazada";
            asc.AutoridadAprobadora = $"CE-015 · Kodiak ({PerfilVisualActual})";

            var integrante = Integrantes.FirstOrDefault(i => i.CodigoCE == asc.IntegranteCodigoCE);
            if (aprobar && integrante != null)
            {
                var rangoAnterior = integrante.Rango;
                integrante.Rango = asc.RangoPropuesto;
                integrante.HistorialInstitucional.Insert(0, $"{DateTime.Today:dd/MM/yyyy} — Ascenso oficial de {rangoAnterior} a {asc.RangoPropuesto} ({asc.Id}).");

                Archivo.Insert(0, new EntradaArchivoInstitucional
                {
                    Id = $"ARC-{Archivo.Count + 1:D3}",
                    Fecha = DateTime.Now,
                    Tipo = "Ascenso",
                    IntegranteCodigoCE = integrante.CodigoCE,
                    IntegranteAlias = integrante.Alias,
                    Departamento = integrante.Departamentos.FirstOrDefault() ?? "Operaciones",
                    Unidad = integrante.Unidades.FirstOrDefault() ?? "UE-001",
                    Descripcion = $"Ascenso aprobado ({asc.Id}): {integrante.Alias} promovido de {rangoAnterior} a {asc.RangoPropuesto}.",
                    Autoridad = asc.AutoridadAprobadora
                });

                RegistrarAuditoriaInterna(asc.AutoridadAprobadora, $"Integrante {integrante.CodigoCE} ({integrante.Alias})", $"Rango: {rangoAnterior}", $"Rango: {asc.RangoPropuesto}", string.IsNullOrWhiteSpace(motivoResolucion) ? asc.Justificacion : motivoResolucion, "Ascenso de Rango");
            }

            Notificaciones.Insert(0, new NotificacionSistema
            {
                Id = $"NOT-{Notificaciones.Count + 1:D3}",
                Categoria = "Ascensos",
                Prioridad = "Importante",
                Titulo = $"Propuesta {asc.Id} ({asc.IntegranteAlias}): {asc.Estado}",
                Mensaje = aprobar
                    ? $"{asc.IntegranteAlias} ha sido ascendido al rango de {asc.RangoPropuesto}."
                    : $"La propuesta de ascenso de {asc.IntegranteAlias} fue desestimada.",
                FechaHora = DateTime.Now,
                Leida = false,
                EnlaceUrl = "/Gobernanza/Ascensos"
            });

            ActivityFeed.Insert(0, new EventoActivityFeed
            {
                Id = $"ACT-{ActivityFeed.Count + 1:D3}",
                Icono = "trending-up",
                Tipo = "Ascenso",
                Descripcion = aprobar
                    ? $"Ascenso oficial: {asc.IntegranteAlias} ({asc.IntegranteCodigoCE}) ahora ostenta el rango de {asc.RangoPropuesto}."
                    : $"Propuesta de ascenso {asc.Id} ({asc.IntegranteAlias}) marcada como Rechazada.",
                Responsable = asc.AutoridadAprobadora,
                FechaHora = DateTime.Now,
                EnlaceRegistro = "/Gobernanza/Ascensos"
            });

            if (!Ascensos.Any(a => a.Estado == "Pendiente"))
            {
                var alertaAsc = Alertas.FirstOrDefault(a => a.Id == "ALT-004");
                if (alertaAsc != null) alertaAsc.Atendida = true;
            }

            ActualizarCatalogoReportes();
            return true;
        }
    }

    public PropuestaAscenso CrearPropuestaAscenso(string codigoCE, string rangoPropuesto, string proponente, string justificacion, string evaluaciones)
    {
        lock (_lock)
        {
            var integrante = Integrantes.FirstOrDefault(i => i.CodigoCE == codigoCE) ?? Integrantes.First();
            var nueva = new PropuestaAscenso
            {
                Id = $"ASC-{Ascensos.Count + 1:D3}",
                IntegranteCodigoCE = integrante.CodigoCE,
                IntegranteAlias = integrante.Alias,
                RangoActual = integrante.Rango,
                RangoPropuesto = rangoPropuesto,
                Proponente = string.IsNullOrWhiteSpace(proponente) ? $"Perfil {PerfilVisualActual}" : proponente,
                Justificacion = justificacion,
                Evaluaciones = string.IsNullOrWhiteSpace(evaluaciones) ? "Expediente en regla" : evaluaciones,
                Fecha = DateTime.Today,
                AutoridadAprobadora = "Pendiente de firma (Dirección / Mando)",
                Estado = "Pendiente"
            };

            Ascensos.Insert(0, nueva);

            ActivityFeed.Insert(0, new EventoActivityFeed
            {
                Id = $"ACT-{ActivityFeed.Count + 1:D3}",
                Icono = "trending-up",
                Tipo = "Ascenso",
                Descripcion = $"Nueva propuesta de ascenso {nueva.Id}: {integrante.Alias} ({integrante.Rango} → {rangoPropuesto}).",
                Responsable = nueva.Proponente,
                FechaHora = DateTime.Now,
                EnlaceRegistro = "/Gobernanza/Ascensos"
            });

            ActualizarCatalogoReportes();
            return nueva;
        }
    }

    public OperacionInstitucional CrearOperacion(string nombre, string plataforma, DateTime fecha, string horario, string departamento, List<string> unidades, string responsable, string descripcion, string contexto)
    {
        lock (_lock)
        {
            var op = new OperacionInstitucional
            {
                Id = $"OP-{Operaciones.Count + 1:D3}",
                Nombre = nombre,
                Plataforma = plataforma,
                Fecha = fecha,
                Horario = horario,
                DepartamentoResponsable = departamento,
                Unidades = unidades.Any() ? unidades : new List<string> { "UE-002" },
                Responsable = responsable,
                Descripcion = descripcion,
                ContextoFicticio = contexto,
                Estado = "Programada",
                Convocados = Integrantes
                    .Where(i => i.EstadoInstitucional == "Activo" && i.Unidades.Any(u => unidades.Contains(u)))
                    .Take(5)
                    .Select(i => new ConvocatoriaParticipante { CodigoCE = i.CodigoCE, Alias = i.Alias, EstadoConvocatoria = "Convocado" })
                    .ToList()
            };

            Operaciones.Insert(0, op);

            Notificaciones.Insert(0, new NotificacionSistema
            {
                Id = $"NOT-{Notificaciones.Count + 1:D3}",
                Categoria = "Actividades",
                Prioridad = "Importante",
                Titulo = $"Nueva operación programada: {op.Id} · {op.Nombre}",
                Mensaje = $"Plataforma: {op.Plataforma} · Fecha: {op.Fecha:dd/MM/yyyy} ({op.Horario}).",
                FechaHora = DateTime.Now,
                Leida = false,
                EnlaceUrl = "/Operaciones"
            });

            ActivityFeed.Insert(0, new EventoActivityFeed
            {
                Id = $"ACT-{ActivityFeed.Count + 1:D3}",
                Icono = "crosshair",
                Tipo = "Operación",
                Descripcion = $"Se programó la operación {op.Id} «{op.Nombre}» en {op.Plataforma} para el {op.Fecha:dd/MM/yyyy}.",
                Responsable = op.Responsable,
                FechaHora = DateTime.Now,
                EnlaceRegistro = "/Operaciones"
            });

            Archivo.Insert(0, new EntradaArchivoInstitucional
            {
                Id = $"ARC-{Archivo.Count + 1:D3}",
                Fecha = DateTime.Now,
                Tipo = "Decisión administrativa",
                IntegranteCodigoCE = "CE-001",
                IntegranteAlias = op.Responsable,
                Departamento = op.DepartamentoResponsable,
                Unidad = op.Unidades.FirstOrDefault() ?? "UE-002",
                Descripcion = $"Apertura de orden de operaciones {op.Id}: {op.Nombre} ({op.Plataforma}).",
                Autoridad = op.Responsable
            });

            ActualizarCatalogoReportes();
            return op;
        }
    }

    public EntrenamientoInstitucional CrearEntrenamiento(string nombre, string categoria, string plataforma, DateTime fecha, string horario, string instructor, List<string> unidades, string descripcion)
    {
        lock (_lock)
        {
            var ent = new EntrenamientoInstitucional
            {
                Id = $"ENT-{Entrenamientos.Count + 1:D3}",
                Nombre = nombre,
                Categoria = categoria,
                Plataforma = plataforma,
                Fecha = fecha,
                Horario = horario,
                Instructor = instructor,
                Unidades = unidades.Any() ? unidades : new List<string> { "UE-001" },
                Descripcion = descripcion,
                Estado = "Programado",
                Convocados = Integrantes
                    .Where(i => i.EstadoInstitucional == "Activo")
                    .Take(4)
                    .Select(i => new EvaluacionAsistenteEntrenamiento { CodigoCE = i.CodigoCE, Alias = i.Alias, Asistencia = "Asistió", Resultado = "En proceso", Observaciones = "Convocado a sesión." })
                    .ToList()
            };

            Entrenamientos.Insert(0, ent);

            ActivityFeed.Insert(0, new EventoActivityFeed
            {
                Id = $"ACT-{ActivityFeed.Count + 1:D3}",
                Icono = "target",
                Tipo = "Entrenamiento",
                Descripcion = $"Nuevo entrenamiento {ent.Id} «{ent.Nombre}» ({ent.Categoria}) programado para el {ent.Fecha:dd/MM/yyyy}.",
                Responsable = ent.Instructor,
                FechaHora = DateTime.Now,
                EnlaceRegistro = "/Operaciones/Entrenamientos"
            });

            ActualizarCatalogoReportes();
            return ent;
        }
    }

    public ReconocimientoInstitucional OtorgarReconocimiento(string codigoCE, string nombre, string categoria, string motivo, string descripcion)
    {
        lock (_lock)
        {
            var integrante = Integrantes.FirstOrDefault(i => i.CodigoCE == codigoCE) ?? Integrantes.First();
            var rec = new ReconocimientoInstitucional
            {
                Id = $"REC-{Reconocimientos.Count + 1:D3}",
                Nombre = nombre,
                Categoria = categoria,
                Emblema = "award",
                Descripcion = descripcion,
                Motivo = motivo,
                BeneficiarioCodigoCE = integrante.CodigoCE,
                BeneficiarioAlias = integrante.Alias,
                Fecha = DateTime.Today,
                AutoridadOtorgante = $"CE-015 · Kodiak ({PerfilVisualActual})",
                Estado = "Vigente"
            };

            Reconocimientos.Insert(0, rec);
            integrante.HistorialInstitucional.Insert(0, $"{DateTime.Today:dd/MM/yyyy} — Condecorado con {rec.Nombre} ({rec.Id}).");

            Archivo.Insert(0, new EntradaArchivoInstitucional
            {
                Id = $"ARC-{Archivo.Count + 1:D3}",
                Fecha = DateTime.Now,
                Tipo = "Reconocimiento",
                IntegranteCodigoCE = integrante.CodigoCE,
                IntegranteAlias = integrante.Alias,
                Departamento = integrante.Departamentos.FirstOrDefault() ?? "Operaciones",
                Unidad = integrante.Unidades.FirstOrDefault() ?? "UE-001",
                Descripcion = $"Otorgamiento de reconocimiento {rec.Id} ({rec.Nombre}) a {integrante.Alias}.",
                Autoridad = rec.AutoridadOtorgante
            });

            ActivityFeed.Insert(0, new EventoActivityFeed
            {
                Id = $"ACT-{ActivityFeed.Count + 1:D3}",
                Icono = "award",
                Tipo = "Reconocimiento",
                Descripcion = $"{integrante.Alias} ({integrante.CodigoCE}) recibió la distinción «{rec.Nombre}» ({rec.Id}).",
                Responsable = rec.AutoridadOtorgante,
                FechaHora = DateTime.Now,
                EnlaceRegistro = "/Gobernanza/Ascensos"
            });

            ActualizarCatalogoReportes();
            return rec;
        }
    }

    public RegistroDisciplinario RegistrarMedidaDisciplinaria(string codigoCE, string tipo, string motivo, string evidencia, string duracion, string observaciones)
    {
        lock (_lock)
        {
            var integrante = Integrantes.FirstOrDefault(i => i.CodigoCE == codigoCE) ?? Integrantes.First();
            var dis = new RegistroDisciplinario
            {
                Id = $"DIS-{Disciplina.Count + 1:D3}",
                IntegranteCodigoCE = integrante.CodigoCE,
                IntegranteAlias = integrante.Alias,
                Tipo = tipo,
                Motivo = motivo,
                Fecha = DateTime.Today,
                Responsable = $"CE-010 · Halcon_Z ({PerfilVisualActual})",
                EvidenciaDescriptiva = evidencia,
                Duracion = duracion,
                Estado = "Vigente",
                Observaciones = observaciones
            };

            Disciplina.Insert(0, dis);
            integrante.HistorialInstitucional.Insert(0, $"{DateTime.Today:dd/MM/yyyy} — Registro disciplinario {dis.Id} ({dis.Tipo}).");

            if (tipo == "Suspensión temporal")
            {
                var ant = integrante.EstadoInstitucional;
                integrante.EstadoInstitucional = "Suspendido";
                RegistrarAuditoriaInterna(dis.Responsable, $"Integrante {integrante.CodigoCE}", $"Estado: {ant}", "Estado: Suspendido", motivo, "Disciplina");
            }
            else if (tipo == "Rehabilitación")
            {
                var ant = integrante.EstadoInstitucional;
                integrante.EstadoInstitucional = "Activo";
                RegistrarAuditoriaInterna(dis.Responsable, $"Integrante {integrante.CodigoCE}", $"Estado: {ant}", "Estado: Activo", motivo, "Rehabilitación");
            }

            Archivo.Insert(0, new EntradaArchivoInstitucional
            {
                Id = $"ARC-{Archivo.Count + 1:D3}",
                Fecha = DateTime.Now,
                Tipo = "Disciplina",
                IntegranteCodigoCE = integrante.CodigoCE,
                IntegranteAlias = integrante.Alias,
                Departamento = integrante.Departamentos.FirstOrDefault() ?? "Operaciones",
                Unidad = integrante.Unidades.FirstOrDefault() ?? "UE-001",
                Descripcion = $"Registro disciplinario {dis.Id} ({dis.Tipo}): {motivo}",
                Autoridad = dis.Responsable
            });

            return dis;
        }
    }

    public ComunicadoInstitucional PublicarComunicado(string titulo, string contenido, string categoria, string prioridad, string departamento, string destinatarios)
    {
        lock (_lock)
        {
            var com = new ComunicadoInstitucional
            {
                Id = $"COM-{Comunicaciones.Count + 1:D3}",
                Titulo = titulo,
                Contenido = contenido,
                Categoria = categoria,
                Prioridad = prioridad,
                Autor = $"CE-015 · Kodiak ({PerfilVisualActual})",
                Departamento = departamento,
                FechaHora = DateTime.Now,
                Destinatarios = destinatarios,
                Estado = "Publicado",
                LeidoDemostrativo = false
            };

            Comunicaciones.Insert(0, com);

            Notificaciones.Insert(0, new NotificacionSistema
            {
                Id = $"NOT-{Notificaciones.Count + 1:D3}",
                Categoria = "Comunicados",
                Prioridad = prioridad == "Urgente" ? "Urgente" : "Importante",
                Titulo = $"Nuevo comunicado [{prioridad}]: {titulo}",
                Mensaje = $"Destinatarios: {destinatarios} · Departamento: {departamento}.",
                FechaHora = DateTime.Now,
                Leida = false,
                EnlaceUrl = "/Comunicaciones"
            });

            ActivityFeed.Insert(0, new EventoActivityFeed
            {
                Id = $"ACT-{ActivityFeed.Count + 1:D3}",
                Icono = "radio",
                Tipo = "Comunicado",
                Descripcion = $"Comunicado {com.Id} publicado ({prioridad}): «{titulo}».",
                Responsable = com.Autor,
                FechaHora = DateTime.Now,
                EnlaceRegistro = "/Comunicaciones"
            });

            Archivo.Insert(0, new EntradaArchivoInstitucional
            {
                Id = $"ARC-{Archivo.Count + 1:D3}",
                Fecha = DateTime.Now,
                Tipo = "Decisión administrativa",
                IntegranteCodigoCE = "CE-015",
                IntegranteAlias = "Kodiak",
                Departamento = departamento,
                Unidad = "General",
                Descripcion = $"Publicación de comunicado oficial {com.Id}: {titulo}.",
                Autoridad = com.Autor
            });

            return com;
        }
    }

    public void ActualizarConfiguracion(string lema, string canalDiscord, bool alertasActivas, bool simultaneidadActiva)
    {
        lock (_lock)
        {
            var lemaAnterior = Configuracion.LemaInstitucional;
            Configuracion.LemaInstitucional = string.IsNullOrWhiteSpace(lema) ? Configuracion.LemaInstitucional : lema.Trim();
            Configuracion.CanalContactoDiscord = string.IsNullOrWhiteSpace(canalDiscord) ? Configuracion.CanalContactoDiscord : canalDiscord.Trim();
            Configuracion.AlertasTiempoRealActivas = alertasActivas;
            Configuracion.DeteccionSimultaneidadCalendario = simultaneidadActiva;

            RegistrarAuditoriaInterna(
                $"CE-015 · Kodiak ({PerfilVisualActual})",
                "Configuración Institucional",
                $"Lema: {lemaAnterior}",
                $"Lema: {Configuracion.LemaInstitucional} | Alertas: {alertasActivas}",
                "Actualización desde el Centro de Configuración Institucional",
                "Configuración");
        }
    }

    public void MarcarAlertaAtendida(string idAlerta)
    {
        lock (_lock)
        {
            var a = Alertas.FirstOrDefault(x => x.Id == idAlerta);
            if (a != null) a.Atendida = true;
        }
    }

    public void MarcarNotificacionesLeidas()
    {
        lock (_lock)
        {
            foreach (var n in Notificaciones) n.Leida = true;
        }
    }

    private void RegistrarAuditoriaInterna(string ejecutor, string registro, string anterior, string nuevo, string motivo, string tipo)
    {
        Auditoria.Insert(0, new RegistroAuditoria
        {
            Id = $"AUD-{Auditoria.Count + 1:D3}",
            UsuarioEjecutor = ejecutor,
            RegistroModificado = registro,
            ValorAnterior = anterior,
            ValorNuevo = nuevo,
            FechaHora = DateTime.Now,
            Motivo = motivo,
            TipoOperacion = tipo
        });
    }
}
