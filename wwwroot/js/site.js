// ============================================================================
// CLAN EXILIADOS — TACTICAL ADMINISTRATIVE INTERFACE (ASP.NET CORE MVC)
// Script centralizado sin emojis, con integración de Lucide Icons
// ============================================================================

document.addEventListener("DOMContentLoaded", function () {
    // Inicializar Lucide Icons en el DOM
    if (window.lucide && typeof window.lucide.createIcons === "function") {
        window.lucide.createIcons();
    }
});

// 1. Limpiar formulario de alta (reproduce btnLimpiar_Click)
function limpiarFormularioSolicitud() {
    var txtNombreJugador = document.getElementById("txtNombreJugador");
    var cboRango = document.getElementById("cboRango");
    var cboEspecialidad = document.getElementById("cboEspecialidad");
    var nudHoras = document.getElementById("nudHoras");
    var rbMilitares = document.getElementById("rbMilitares");
    var txtDiscord = document.getElementById("txtDiscord");
    var cboDisponibilidad = document.getElementById("cboDisponibilidad");
    var dtpFechaSolicitud = document.getElementById("dtpFechaSolicitud");
    var chkTerminos = document.getElementById("chkTerminos");

    if (txtNombreJugador) txtNombreJugador.value = "";
    if (cboRango) cboRango.selectedIndex = 0;
    if (cboEspecialidad) cboEspecialidad.selectedIndex = 0;
    if (nudHoras) nudHoras.value = 100;
    if (rbMilitares) rbMilitares.checked = true;
    if (txtDiscord) txtDiscord.value = "";
    if (cboDisponibilidad) cboDisponibilidad.selectedIndex = 0;
    if (dtpFechaSolicitud) {
        var hoy = new Date().toISOString().split("T")[0];
        dtpFechaSolicitud.value = hoy;
    }
    if (chkTerminos) chkTerminos.checked = true;
    if (txtNombreJugador) txtNombreJugador.focus();

    mostrarToastHUD("Controles del formulario restablecidos a sus valores iniciales (100 horas, Militares).", "info");
}

// 2. Acción visual del botón Guardar solicitud / Grabar en Solicitudes/Nuevo
function mostrarAvisoGuardado() {
    var txtNombre = document.getElementById("txtNombreJugador");
    var nombre = txtNombre && txtNombre.value.trim() !== "" ? txtNombre.value.trim() : "Postulante";
    mostrarToastHUD("Solicitud de «" + nombre + "» validada correctamente en la plataforma institucional.", "success");
}

// 3. Filtro visual en Solicitudes/Index
function filtrarTablaMantenimientoPA2() {
    var input = document.getElementById("txtBuscar");
    if (!input) return;
    var filtro = input.value.trim().toLowerCase();
    var filas = document.querySelectorAll("#tablaSolicitudesPA2 tbody tr");
    var visibles = 0;

    filas.forEach(function (fila) {
        var texto = fila.innerText.toLowerCase();
        if (filtro === "" || texto.indexOf(filtro) !== -1) {
            fila.style.display = "";
            visibles++;
        } else {
            fila.style.display = "none";
        }
    });

    var lblTotal = document.getElementById("lblTotalSolicitudesPA2");
    if (lblTotal) {
        lblTotal.textContent = "Total de solicitudes: " + visibles;
    }
    mostrarToastHUD("Filtro visual aplicado: " + visibles + " solicitud(es) coincidente(s).", "info");
}

function restablecerTablaMantenimientoPA2() {
    var input = document.getElementById("txtBuscar");
    if (input) input.value = "";
    var filas = document.querySelectorAll("#tablaSolicitudesPA2 tbody tr");
    filas.forEach(function (fila) {
        fila.style.display = "";
    });
    var lblTotal = document.getElementById("lblTotalSolicitudesPA2");
    if (lblTotal) {
        lblTotal.textContent = "Total de solicitudes: " + filas.length;
    }
    mostrarToastHUD("Mostrando todas las solicitudes (" + filas.length + " registros).", "info");
}

// 4. Acción de filtrado y consulta en Consultas/Periodo
function ejecutarConsultaPeriodoPA2() {
    var dtpDesde = document.getElementById("dtpDesde");
    var dtpHasta = document.getElementById("dtpHasta");
    var cboFaccion = document.getElementById("cboFaccion");
    var tabla = document.getElementById("tablaConsultasPeriodo");
    var lblTotal = document.getElementById("lblTotalConsultasPeriodo");
    var btnExportar = document.getElementById("btnExportarExcelPeriodo");

    if (!dtpDesde || !dtpHasta || !cboFaccion) return;

    var desdeVal = dtpDesde.value;
    var hastaVal = dtpHasta.value;
    var faccionVal = cboFaccion.value;

    if (desdeVal && hastaVal && desdeVal > hastaVal) {
        mostrarToastHUD("La fecha inicial no puede ser posterior a la fecha final.", "error");
        return;
    }

    var lista = window.todasLasSolicitudesPeriodo || [];
    var tablaCuerpo = tabla ? tabla.querySelector("tbody") : null;
    if (!tablaCuerpo) return;

    var filtrados = lista.filter(function (s) {
        var cumpleFecha = true;
        if (desdeVal && s.fecha < desdeVal) cumpleFecha = false;
        if (hastaVal && s.fecha > hastaVal) cumpleFecha = false;

        var cumpleFaccion = (faccionVal === "(Todos)" || s.faccion.toLowerCase() === faccionVal.toLowerCase());
        return cumpleFecha && cumpleFaccion;
    });

    tablaCuerpo.innerHTML = "";

    if (filtrados.length === 0) {
        tablaCuerpo.innerHTML =
            '<tr>' +
            '  <td colspan="8" class="text-center text-muted py-5">' +
            '    <div class="d-flex flex-column align-items-center gap-2">' +
            '      <i data-lucide="filter-x" class="icon-lg text-warning" style="opacity: 0.8;"></i>' +
            '      <span class="text-light fw-semibold">No se encontraron solicitudes para el rango y facción seleccionados.</span>' +
            '      <small class="text-muted">Pruebe ampliando el periodo (ej. desde 2026-07-01 hasta 2026-10-31) o seleccionando "(Todos)".</small>' +
            '    </div>' +
            '  </td>' +
            '</tr>';
        if (lblTotal) lblTotal.textContent = "0";
        if (btnExportar) {
            btnExportar.disabled = true;
            btnExportar.style.opacity = "0.5";
            btnExportar.style.cursor = "not-allowed";
        }
        mostrarToastHUD("No se encontraron solicitudes con los filtros aplicados.", "warning");
    } else {
        var html = "";
        filtrados.forEach(function (s) {
            var badgeClass = s.faccion === "Militares" ? "badge-militares" :
                             (s.faccion === "Mercenarios" ? "badge-mercenarios" : "badge-terroristas");

            html += '<tr>' +
                    '  <td class="hud-mono">' + s.fechaFormato + '</td>' +
                    '  <td class="fw-bold text-light">' + s.jugador + '</td>' +
                    '  <td><span class="hud-badge-neutral">' + s.rango + '</span></td>' +
                    '  <td>' + s.especialidad + '</td>' +
                    '  <td class="text-end hud-mono text-warning">' + s.horasFormato + '</td>' +
                    '  <td><span class="badge ' + badgeClass + '">' + s.faccion + '</span></td>' +
                    '  <td><code class="hud-mono text-light">' + s.discord + '</code></td>' +
                    '  <td>' + s.disponibilidad + '</td>' +
                    '</tr>';
        });

        tablaCuerpo.innerHTML = html;
        if (lblTotal) lblTotal.textContent = filtrados.length;
        if (btnExportar) {
            btnExportar.disabled = false;
            btnExportar.style.opacity = "1";
            btnExportar.style.cursor = "pointer";
        }
        mostrarToastHUD("Consulta ejecutada: " + filtrados.length + " solicitud(es) encontrada(s).", "success");
    }

    if (window.lucide && typeof window.lucide.createIcons === "function") {
        window.lucide.createIcons();
    }
}

function establecerRangoPeriodo(desde, hasta) {
    var dtpDesde = document.getElementById("dtpDesde");
    var dtpHasta = document.getElementById("dtpHasta");
    if (dtpDesde) dtpDesde.value = desde;
    if (dtpHasta) dtpHasta.value = hasta;
    ejecutarConsultaPeriodoPA2();
}

function exportarConsultaPeriodoExcel() {
    var dtpDesde = document.getElementById("dtpDesde")?.value || "2026-07-01";
    var dtpHasta = document.getElementById("dtpHasta")?.value || "2026-10-31";
    var faccion = document.getElementById("cboFaccion")?.value || "(Todos)";

    var lista = window.todasLasSolicitudesPeriodo || [];
    var filtrados = lista.filter(function (s) {
        var cumpleFecha = true;
        if (dtpDesde && s.fecha < dtpDesde) cumpleFecha = false;
        if (dtpHasta && s.fecha > dtpHasta) cumpleFecha = false;
        var cumpleFaccion = (faccion === "(Todos)" || s.faccion.toLowerCase() === faccion.toLowerCase());
        return cumpleFecha && cumpleFaccion;
    });

    if (filtrados.length === 0) {
        mostrarToastHUD("No hay registros para exportar con los filtros seleccionados.", "warning");
        return;
    }

    var totalHoras = 0;
    filtrados.forEach(function (s) { totalHoras += (s.horas || 0); });

    var fechaDesdeFmt = dtpDesde.split("-").reverse().join("/");
    var fechaHastaFmt = dtpHasta.split("-").reverse().join("/");

    // Documento estructurado para Excel con formato nativo de celdas, filas y columnas
    var excelDoc =
        '<html xmlns:o="urn:schemas-microsoft-com:office:office" ' +
        'xmlns:x="urn:schemas-microsoft-com:office:excel" ' +
        'xmlns="http://www.w3.org/TR/REC-html40">\r\n' +
        '<head>\r\n' +
        '<!--[if gte mso 9]>\r\n' +
        '<xml>\r\n' +
        ' <x:ExcelWorkbook>\r\n' +
        '  <x:ExcelWorksheets>\r\n' +
        '   <x:ExcelWorksheet>\r\n' +
        '    <x:Name>Solicitudes</x:Name>\r\n' +
        '    <x:WorksheetOptions>\r\n' +
        '     <x:DisplayGridlines/>\r\n' +
        '    </x:WorksheetOptions>\r\n' +
        '   </x:ExcelWorksheet>\r\n' +
        '  </x:ExcelWorksheets>\r\n' +
        ' </x:ExcelWorkbook>\r\n' +
        '</xml>\r\n' +
        '<![endif]-->\r\n' +
        '<meta http-equiv="Content-Type" content="text/html; charset=UTF-8"/>\r\n' +
        '<style>\r\n' +
        '  body { font-family: Calibri, "Segoe UI", Arial, sans-serif; font-size: 11pt; }\r\n' +
        '  table { border-collapse: collapse; }\r\n' +
        '  .title-main { font-size: 14pt; font-weight: bold; color: #1F6B35; height: 28px; }\r\n' +
        '  .subtitle-text { font-size: 10pt; color: #555555; height: 20px; font-style: italic; }\r\n' +
        '  .th-header { background-color: #1F6B35; color: #FFFFFF; font-weight: bold; font-size: 11pt; text-align: center; border: 1px solid #144923; padding: 6px 10px; }\r\n' +
        '  .td-date { border: 1px solid #CCCCCC; text-align: center; mso-number-format: "dd/mm/yyyy"; padding: 5px 8px; }\r\n' +
        '  .td-text { border: 1px solid #CCCCCC; text-align: left; padding: 5px 8px; }\r\n' +
        '  .td-num { border: 1px solid #CCCCCC; text-align: right; mso-number-format: "#,##0"; padding: 5px 8px; font-weight: bold; color: #9A6500; }\r\n' +
        '  .td-center { border: 1px solid #CCCCCC; text-align: center; padding: 5px 8px; }\r\n' +
        '  .td-fac-mil { border: 1px solid #CCCCCC; text-align: center; padding: 5px 8px; font-weight: bold; color: #1F6B35; background-color: #EBF8EE; }\r\n' +
        '  .td-fac-mer { border: 1px solid #CCCCCC; text-align: center; padding: 5px 8px; font-weight: bold; color: #9A6500; background-color: #FFF9E8; }\r\n' +
        '  .td-fac-ter { border: 1px solid #CCCCCC; text-align: center; padding: 5px 8px; font-weight: bold; color: #B31D14; background-color: #FDEEEE; }\r\n' +
        '  .tr-zebra { background-color: #F8FAF8; }\r\n' +
        '  .tr-total { background-color: #E6F2E8; font-weight: bold; border-top: 2px solid #1F6B35; border-bottom: 2px solid #1F6B35; }\r\n' +
        '</style>\r\n' +
        '</head>\r\n' +
        '<body>\r\n' +
        '<table>\r\n' +
        '  <tr><td colspan="8" class="title-main">CLAN EXILIADOS — SISTEMA DE ADMISIÓN MILITAR</td></tr>\r\n' +
        '  <tr><td colspan="8" class="subtitle-text">Reporte de Solicitudes por Periodo | Rango: ' + fechaDesdeFmt + ' al ' + fechaHastaFmt + ' | Facción: ' + faccion + '</td></tr>\r\n' +
        '  <tr><td colspan="8" style="height: 10px;"></td></tr>\r\n' +
        '  <tr>\r\n' +
        '    <th class="th-header" style="width: 100px;">Fecha</th>\r\n' +
        '    <th class="th-header" style="width: 140px;">Jugador</th>\r\n' +
        '    <th class="th-header" style="width: 110px;">Rango</th>\r\n' +
        '    <th class="th-header" style="width: 140px;">Especialidad</th>\r\n' +
        '    <th class="th-header" style="width: 110px;">Horas MILSIM</th>\r\n' +
        '    <th class="th-header" style="width: 120px;">Facción</th>\r\n' +
        '    <th class="th-header" style="width: 150px;">Discord</th>\r\n' +
        '    <th class="th-header" style="width: 130px;">Disponibilidad</th>\r\n' +
        '  </tr>\r\n';

    filtrados.forEach(function (s, index) {
        var rowClass = (index % 2 === 1) ? ' class="tr-zebra"' : '';
        var facClass = s.faccion === "Militares" ? "td-fac-mil" :
                       (s.faccion === "Mercenarios" ? "td-fac-mer" : "td-fac-ter");

        excelDoc +=
            '  <tr' + rowClass + '>\r\n' +
            '    <td class="td-date">' + s.fechaFormato + '</td>\r\n' +
            '    <td class="td-text" style="font-weight: bold;">' + s.jugador + '</td>\r\n' +
            '    <td class="td-text">' + s.rango + '</td>\r\n' +
            '    <td class="td-text">' + s.especialidad + '</td>\r\n' +
            '    <td class="td-num">' + s.horas + '</td>\r\n' +
            '    <td class="' + facClass + '">' + s.faccion + '</td>\r\n' +
            '    <td class="td-text" style="font-family: Consolas, monospace;">' + s.discord + '</td>\r\n' +
            '    <td class="td-center">' + s.disponibilidad + '</td>\r\n' +
            '  </tr>\r\n';
    });

    excelDoc +=
        '  <tr class="tr-total">\r\n' +
        '    <td colspan="4" class="td-text" style="font-weight: bold;">TOTAL: ' + filtrados.length + ' solicitud(es) encontrada(s)</td>\r\n' +
        '    <td class="td-num" style="color: #1F6B35;">' + totalHoras + '</td>\r\n' +
        '    <td colspan="3" class="td-text" style="font-size: 9pt; color: #555555;">Emitido por la Plataforma Institucional Clan Exiliados</td>\r\n' +
        '  </tr>\r\n' +
        '</table>\r\n' +
        '</body>\r\n' +
        '</html>';

    var blob = new Blob(["\uFEFF" + excelDoc], { type: "application/vnd.ms-excel;charset=utf-8" });
    var url = URL.createObjectURL(blob);
    var a = document.createElement("a");
    a.href = url;
    a.download = "Solicitudes_Periodo_" + dtpDesde.replace(/-/g, "") + "_" + dtpHasta.replace(/-/g, "") + ".xls";
    document.body.appendChild(a);
    a.click();
    document.body.removeChild(a);
    URL.revokeObjectURL(url);

    mostrarToastHUD("Reporte de Excel exportado exitosamente con formato de tabla (" + filtrados.length + " filas).", "success");
}

function exportarConsultaPeriodoCSV() {
    exportarConsultaPeriodoExcel();
}

// 5. Acción visual para Archivo -> Salir (reproduce frmPrincipal_FormClosing)
function confirmarSalidaSistema(event) {
    if (event) event.preventDefault();
    var modalEl = document.getElementById("modalSalirSistema");
    if (modalEl && window.bootstrap) {
        var modal = new bootstrap.Modal(modalEl);
        modal.show();
    } else {
        if (confirm("¿Desea cerrar la sesión de la plataforma institucional?")) {
            window.location.href = "/";
        }
    }
    return false;
}

// 6. Toggle Sidebar (Escritorio colapsable y Móvil adaptable)
function toggleSidebar() {
    var sb = document.getElementById("hudSidebar");
    if (sb) {
        sb.classList.toggle("collapsed");
    }
}

function toggleSidebarMovil() {
    var sb = document.getElementById("hudSidebar");
    if (sb) {
        sb.classList.toggle("show-mobile");
    }
}

// 7. Búsqueda Global agrupada por categorías
function filtrarBusquedaGlobal() {
    var input = document.getElementById("txtBusquedaGlobalInput");
    if (!input) return;
    var q = input.value.trim().toLowerCase();
    var items = document.querySelectorAll(".global-search-item");
    var grupos = document.querySelectorAll(".global-search-group");
    var totalVisibles = 0;

    items.forEach(function (item) {
        var texto = (item.getAttribute("data-search") || item.innerText).toLowerCase();
        if (q === "" || texto.indexOf(q) !== -1) {
            item.classList.remove("d-none");
            totalVisibles++;
        } else {
            item.classList.add("d-none");
        }
    });

    grupos.forEach(function (grupo) {
        var itemsVisiblesEnGrupo = grupo.querySelectorAll(".global-search-item:not(.d-none)").length;
        if (itemsVisiblesEnGrupo > 0) {
            grupo.classList.remove("d-none");
        } else {
            grupo.classList.add("d-none");
        }
    });

    var contador = document.getElementById("contadorBusquedaGlobal");
    if (contador) {
        contador.textContent = totalVisibles + " resultado(s) encontrado(s)";
    }
}

// 8. Panel lateral (Offcanvas) de vista previa rápida de integrante
function abrirVistaPreviaIntegrante(codigo, alias, nombre, rango, faccion, depto, unidad, horas, estado, disp, discord) {
    var elCodigo = document.getElementById("prevCodigoCE");
    if (!elCodigo) return;
    elCodigo.textContent = codigo;
    document.getElementById("prevAlias").textContent = alias;
    document.getElementById("prevNombre").textContent = nombre;
    document.getElementById("prevRango").textContent = rango;
    document.getElementById("prevFaccion").textContent = faccion;
    document.getElementById("prevDepto").textContent = depto;
    document.getElementById("prevUnidad").textContent = unidad;
    document.getElementById("prevHoras").textContent = horas + " h MILSIM";
    document.getElementById("prevEstado").textContent = estado;
    document.getElementById("prevDisp").textContent = disp;
    document.getElementById("prevDiscord").textContent = discord;
    document.getElementById("prevLinkExpediente").setAttribute("href", "/Personal/Expediente/" + codigo);
    document.getElementById("prevLinkCredencial").setAttribute("href", "/Personal/Credencial/" + codigo);

    var offcanvasEl = document.getElementById("offcanvasVistaPreviaIntegrante");
    if (offcanvasEl && window.bootstrap) {
        var oc = bootstrap.Offcanvas.getOrCreateInstance(offcanvasEl);
        oc.show();
    }
}

// 9. Exportación de Credencial Digital CE a imagen PNG mediante HTML5 Canvas (sin emojis)
function descargarCredencialPNG(codigoCE, alias, nombre, rango, departamento, unidad, fechaIngreso, estado) {
    var canvas = document.createElement("canvas");
    canvas.width = 720;
    canvas.height = 420;
    var ctx = canvas.getContext("2d");

    // Fondo Tactical Administrative Interface (#070D08 y #0F1A11)
    var grad = ctx.createLinearGradient(0, 0, 720, 420);
    grad.addColorStop(0, "#070D08");
    grad.addColorStop(0.5, "#0F1A11");
    grad.addColorStop(1, "#18251B");
    ctx.fillStyle = grad;
    ctx.fillRect(0, 0, 720, 420);

    // Borde verde oscuro #1F6B35
    ctx.strokeStyle = "#1F6B35";
    ctx.lineWidth = 4;
    ctx.strokeRect(10, 10, 700, 400);

    // Encabezado
    ctx.fillStyle = "#18251B";
    ctx.fillRect(12, 12, 696, 68);
    ctx.fillStyle = "#E2EAE3";
    ctx.font = "bold 24px Rajdhani, Segoe UI, sans-serif";
    ctx.fillText("CLAN EXILIADOS — CREDENCIAL DIGITAL INSTITUCIONAL", 28, 46);
    ctx.fillStyle = "#39FF6A";
    ctx.font = "12px Share Tech Mono, monospace";
    ctx.fillText("IDENTIFICACIÓN OFICIAL DE SIMULACIÓN MILSIM Y ROLEPLAY (SIN VALIDEZ LEGAL)", 28, 68);

    // Datos principales
    ctx.fillStyle = "#FFB000";
    ctx.font = "bold 28px Share Tech Mono, monospace";
    ctx.fillText(codigoCE + "  ·  " + alias, 32, 130);

    ctx.fillStyle = "#E2EAE3";
    ctx.font = "17px Inter, Segoe UI, sans-serif";
    ctx.fillText("Nombre de Expediente: " + nombre, 32, 170);
    ctx.fillText("Rango Institucional: " + rango, 32, 205);
    ctx.fillText("Departamento Asignado: " + departamento, 32, 240);
    ctx.fillText("Unidad Táctica: " + unidad, 32, 275);
    ctx.fillText("Fecha de Incorporación: " + fechaIngreso, 32, 310);
    ctx.fillStyle = "#39FF6A";
    ctx.font = "bold 17px Share Tech Mono, monospace";
    ctx.fillText("Estado Institucional: " + estado.toUpperCase(), 32, 348);

    // Pie
    ctx.fillStyle = "#91A397";
    ctx.font = "12px Inter, Segoe UI, sans-serif";
    ctx.fillText("Emitido por la Plataforma Institucional ClanExiliados_Web (ASP.NET Core MVC) — Uso exclusivo en simulación ficticia.", 32, 390);

    var link = document.createElement("a");
    link.download = "Credencial_" + codigoCE + "_" + alias + ".png";
    link.href = canvas.toDataURL("image/png");
    link.click();

    mostrarToastHUD("Credencial digital " + codigoCE + " exportada en formato PNG.", "success");
}

// 10. Sistema de notificaciones Toast Tactical (sin emojis)
function mostrarToastHUD(mensaje, tipo) {
    var contenedor = document.getElementById("hudToastContainer");
    if (!contenedor) return;

    var bordeColor = tipo === "error" ? "#FF3B30" : (tipo === "success" ? "#39FF6A" : "#FFB000");
    var etiquetaTipo = tipo === "error" ? "CRÍTICO" : (tipo === "success" ? "CONFIRMACIÓN" : "SISTEMA");

    var toastEl = document.createElement("div");
    toastEl.className = "toast align-items-center show mb-2";
    toastEl.setAttribute("role", "alert");
    toastEl.style.backgroundColor = "#0F1A11";
    toastEl.style.color = "#E2EAE3";
    toastEl.style.border = "1px solid #1F6B35";
    toastEl.style.borderLeft = "4px solid " + bordeColor;
    toastEl.style.borderRadius = "6px";
    toastEl.style.boxShadow = "0 8px 24px rgba(0, 0, 0, 0.5)";

    toastEl.innerHTML =
        '<div class="d-flex p-2">' +
        '  <div class="toast-body p-1" style="font-size: 0.88rem;">' +
        '    <span class="hud-mono" style="color:' + bordeColor + '; font-weight: 700; margin-right: 6px;">[' + etiquetaTipo + ']</span>' +
        '    <strong class="me-1">Clan Exiliados:</strong> ' + mensaje +
        '  </div>' +
        '  <button type="button" class="btn-close btn-close-white me-2 m-auto" onclick="this.closest(\'.toast\').remove();"></button>' +
        '</div>';

    contenedor.appendChild(toastEl);
    setTimeout(function () {
        if (toastEl && toastEl.parentNode) {
            toastEl.remove();
        }
    }, 4500);
}
