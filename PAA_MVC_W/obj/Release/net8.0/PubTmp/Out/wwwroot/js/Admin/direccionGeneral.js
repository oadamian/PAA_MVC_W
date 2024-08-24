let datatable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    datatable = $('#tblDatos').DataTable({

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

        "ajax": {
            "url": "/Admin/DireccionGeneral/ObtenerTodos"
        },
        "columns": [
            { "data": "cc", "width": "10%" },
            { "data": "siglas", "width": "10%" },
            { "data": "nombre_DG", "width": "40%" },
            { "data": "unidadAuditora.siglas", "width": "10%" },
            {
                "data": "estado",
                "render": function (data) {
                    if (data == true) {
                        return "Activo";
                    } else {
                        return "Inactivo";
                    }

                }, "width": "10%"
            },
            {
                "data": "id",
                "render": function (data) {
                    return `
                    <div class="text-center">
                       <a href="/Admin/DireccionGeneral/Upsert/${data}" class="btn btn-success text-white" style="cursor:pointer">
                           <i class="bi bi-pencil-square"></i>
                       </a>
                       <a onclick=Delete("/Admin/DireccionGeneral/Delete/${data}") class="btn btn-danger text-white style="cursos:pointer">
                       <i class="bi bi-trash3-fill"></i>
                          
                       </a>
                    </div>
                        `;
                }, "width": "20%"
            }

        ]





    });

}

function Delete(url) {
    //swal({
    //    title: "Esta seguro de Eliminar la Bodega?",
    //    text: "Este registro no podra ser recuperado",
    //    icon: "warning",
    //    showCancelButton: true,
    //    dangerMoode: true
    //}).then((borrar) => {
    //    if (borrar) {
    //        $.ajax({
    //            type: "POST",
    //            url: url,
    //            success: function (data) {
    //                if (data.success) {
    //                    toastr.success(data.message);//el toast es para enviar notificaciones
    //                    datatable.ajax.reload();

    //                }
    //                else {
    //                    toastr.error(data.message)
    //                }
    //            }
    //        });
    //    }
    //});
    Swal.fire({
        title: '¿Estás seguro de eliminar la Dirección General?',
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
