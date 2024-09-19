$(document).ready(function () {
    $('#empTable').DataTable({
        "paging": true,
        "responsive": true,
        "lengthChange": true,
        "searching": true,
        "ordering": true,
        "info": true,
        "autoWidth": false,
        "processing": true,
        "ajax": {
            url: "https://localhost:7294/api/Register",
            type: "GET",
            dataType: "json",
            "datasrc": "data",
        },

        columnDefs: [{
            "defaultContent": "-",
            "targets": "_all"
        }],
        "columns": [
            {
                "render": function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
                }
            },
            { "data": "nik" },
            { "data": "fullName" },
            { "data": "phone" },
            { "data": "birthDate" },
            { "data": "email" },
            { "data": "degree" },
            { "data": "gpa" },
            { "data": "univ_Name" },
        ]
    });
});

function addUniv(){
    var reg = new Object();
    reg.firstName = $('#firstName').val();
    reg.lastName = $('#lastName').val();
    reg.phone = $('#phone').val();
    reg.birthDate = $('#birtDate').val();
    reg.email = $('#email').val();
    reg.degree = $('#degree').val();
    reg.gpa = $('#gpa').val();
    reg.univ_Id = $('#university').val();
    $.ajax({
        url: "https://localhost:7294/api/Register",
        type: "POST",
        data: JSON.stringify(reg),
        contentType: "application/json; charset=utf-8",
    })
        .then((result) => {
            if (result.status == 200) {
                $(document).Toasts('create', {
                    class: 'bg-success',
                    title: 'Success',
                    autohide: true,
                    delay: 1000,
                    body: result.message
                });
                $('#empTable').DataTable().ajax.reload();
                $('#modal-default').modal('hide');
            } else {
                alert(result.message);
            }
        })
}

$(document).ready(function () {
    $.ajax({
        url: "https://localhost:7294/api/University",
        type: "GET",
        dataType: "json",
    }).then((result) => {
        $('#university').empty();
        $('#university').append('<option>== Select University ==</option>');
        $.each(result.data, function (index, univ) {
            $('#university').append('<option value="' + univ.univ_Id + '">' + univ.univ_Name + '</option>');
        });
    });
});