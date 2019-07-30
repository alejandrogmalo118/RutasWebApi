$(function () {
    $("table").dataTable({
        stateSave: true,
        "language": {
            "search": "Buscar:",
            "lengthMenu": "Mostrar _MENU_ registros",
            "emptyTable": "No hay registros en esta tabla",
            "info": "Mostrando _START_ de _END_ de _TOTAL_ entradas",
            "infoEmpty": "Mostrando 0 entradas",
            "infoFiltered": "(filtrado a partir de _MAX_ entradas en total)",
            "zeroRecords": "No se han encontrado registros que coincidan con su búsqueda",
            "paginate": {
                "first": "Primero",
                "previous": "Anterior",
                "next": "Siguiente",
                "last": "Último"
            },
            "loadingRecords": "Cargando...",
            "processing": "Procesando..."
        }
    });
});