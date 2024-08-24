

let datatable;

$(document).ready(function () {
    loadDataTable();
});

var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'))
var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
    return new bootstrap.Tooltip(tooltipTriggerEl)
})
function loadDataTable() {
    datatable = $('#tblDatos_').DataTable({

        "language": {
            "lengthMenu": "Mostrar _MENU_ Registros Por Pagina",
            "zeroRecords": "Ningun Registro",
            "info": "Mostrar page _PAGE_ de _PAGES_",
            "infoEmpty": "no hay registros",
            "infoFiltered": "(filtered from _MAX_ total registros)",
            "search": "Buscar",
            "paginate": {
                "first": "Primero",
                "last": "Último",
                "next": "Siguiente",
                "previous": "Anterior"
            }
        },

        "columnDefs": [
            {
                "targets": "_all", // Aplica a todas las columnas
                "className": "text-center"//para alinear 
            }
        ],

        "ajax": {
            "url": "/Capturista/Actividad/ObtenerTodos"
        },
        "columns": [
            { "data": "clave"},
            { "data": "nombreActividad" },
            { "data": "objetivoActividad" },
            {
                "data": "fecha1",  // Nombre del campo que contiene la fecha
                "render": function (data) {
                    if (data) {
                        var fecha = new Date(data);
                        return fecha.toLocaleDateString('es-ES', {
                            day: '2-digit',
                            month: '2-digit',
                            year: 'numeric'
                        });
                    } else {
                        return "-";
                    }
                }
            },
            {
                "data": "fecha2",  // Nombre del campo que contiene la fecha
                "render": function (data) {
                    if (data) {
                        var fecha = new Date(data);
                        return fecha.toLocaleDateString('es-ES', {
                            day: '2-digit',
                            month: '2-digit',
                            year: 'numeric'
                        });
                    } else {
                        return "-";
                    }
                }
            },
            {
                "data": "fecha3",  // Nombre del campo que contiene la fecha
                "render": function (data) {
                    if (data) {
                        var fecha = new Date(data);
                        return fecha.toLocaleDateString('es-ES', {
                            day: '2-digit',
                            month: '2-digit',
                            year: 'numeric'
                        });
                    } else {
                        return "-";
                    }
                }
            },
            {
                "data": "fecha4",  // Nombre del campo que contiene la fecha
                "render": function (data) {
                    if (data) {
                        var fecha = new Date(data);
                        return fecha.toLocaleDateString('es-ES', {
                            day: '2-digit',
                            month: '2-digit',
                            year: 'numeric'
                        });
                    } else {
                        return "-";
                    }
                }
            },
           
            {
                "data": "aplicaIndicador",
                "render": function (data) {
                    if (data == true) {
                        return "Si";
                    } else {
                        return "No";
                    }

                }
            },
            {
                "data": "estado",
                "render": function (data) {
                    if (data == true) {
                        return "Activo";
                    } else {
                        return "Inactivo";
                    }

                }
            },


            {
                "data": "id",
                "render": function (data, type, row) {
                    // Solo muestra el botón "Cargar Indicadores" si el estado es Activo (true)
                    const indicadoresButton = row.aplicaIndicador == true ? `
                <div class="row">
                   <a href="/Capturista/Actividad/Upsert/${data}" class="ov-btn-grow-box" style="cursor:pointer" data-bs-toggle="tooltip" data-bs-placement="top" title="Cargar Indicadores">
                  <i class="bi bi-bar-chart-line"></i>
                   </a>
                </div>
             
        ` : '';

                    return `
        <div class="text-center">
            <div class="row">
                <a href="/Capturista/Actividad/Upsert/${data}" class="ov-btn-grow-box" style="cursor:pointer" data-bs-toggle="tooltip" data-bs-placement="top" title="Cargar Atribuciones">
                    <i class="bi bi-journal-text"></i>
                </a>
            </div>

              
                ${indicadoresButton}
          
            <div class="row">
                <a href="/Capturista/Actividad/Upsert/${data}" class="ov-btn-grow-box" style="cursor:pointer" data-bs-toggle="tooltip" data-bs-placement="top" title="Modificar">
                    <i class="bi bi-pencil-square"></i>
                </a>
            </div>
         
            <div class="row">
                <a onclick=Delete("/Capturista/Actividad/Delete/${data}") class="ov-btn-grow-box" style="cursor:pointer" data-bs-toggle="tooltip" data-bs-placement="top" title="Eliminar">
                    <i class="bi bi-trash3-fill"></i>
                </a>
            </div>
        </div>
        `;
                }
            }
        ]

       
    });

}

function Delete(url) {

    Swal.fire({
        title: '¿Estás seguro de eliminar la Actividad?',
        text: 'Este registro no podrá ser recuperado',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Eliminar',
        cancelButtonText: 'Cancelar',
        customClass: {
            confirmButton: 'btn btn-danger',
            cancelButton: 'btn btn-secondary'
        },
        dangerMode: true
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: "POST",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message); // el toast es para enviar notificaciones
                        datatable.ajax.reload();
                    } else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}
