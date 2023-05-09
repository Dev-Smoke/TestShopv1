var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            url: '/product/getall',            
        },
        "processing": true,
        "serverSide": true,
        "columns": [
            { data: 'Name', "width": "15%" },
            { data: 'unitPriceNetto', "width": "10%" },
            { data: 'imagePath', "width": "15%" },
            { data: 'description', "width": "20%" },
            { data: 'category.name', "width": "10%" },
            { data: 'manufacturer.name', "width": "20%" },
        ]
    });
}

