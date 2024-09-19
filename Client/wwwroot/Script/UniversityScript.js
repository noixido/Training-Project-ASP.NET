$(document).ready(function () {

    $('#univTable').DataTable({
        "paging": true,
        "responsive": true,
        "lengthChange": true,
        "searching": true,
        "ordering": true,
        "info": true,
        "autoWidth": false,
        "processing": true,
        "ajax": {
            url: "https://localhost:7294/api/University",
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
            { "data": "univ_Id" },
            { "data": "univ_Name" },
            {
                "render": function (data, type, row) {
                    return '<div style="display:flex; justify-content:center; gap:5px;"><button type="button" id="editUniv" class="btn btn-warning" data-tooltip="tooltip" data-placement="top" title="Edit Data" onclick="editUniv(\'' + row.univ_Id + '\')" data-toggle="modal" data-target="#modal-default"> <i class="fas fa-pencil-alt"></i></button > ' +
                        ' | <button type="button" id="deleteUniv" class="btn btn-danger" data-tooltip="tooltip" data-placement="top" title="Delete Data" onclick="deleteUniv(\'' + row.univ_Id + '\')"><i class="fas fa-trash"></i></button></div > ';
                }
            }
        ]
    });
});

function addUniv() {
    var univ = new Object();
    univ.univ_Name = $('#univName').val();
    $.ajax({
        url: "https://localhost:7294/api/University",
        type: "POST",
        data: JSON.stringify(univ),
        contentType: "application/json; charset=utf-8",
    })
        .then((result) => {
            if (result.status == 200) {
                //alert(result.message);
                $(document).Toasts('create', {
                    class: 'bg-success',
                    title: 'Success',
                    autohide: true,
                    delay: 750,
                    body: result.message
                });
                $('#univTable').DataTable().ajax.reload();
                $('#modal-default').modal('hide');
            } else {
                //alert(result.message);
                Swal.fire({
                    icon: "error",
                    title: result.message,
                    showConfirmButton: false,
                    timer: 1500
                });
            }
        })
}

function editUniv(id) {
    document.getElementById('modalEdit').style.display = "block";
    document.getElementById('modalAdd').style.display = "none";

    $.ajax({
        url: "https://localhost:7294/api/University/" + id,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
    }).then((result) => {
        var obj = result.data;
        $('#univName').val(obj.univ_Name);

        var btn = $('#modalEdit');
        btn.attr('onclick', 'edit(\'' + id + '\')');

        $('#modal-default').modal('show');
    });
}

function edit(id) {
    var univ = new Object();
    univ.univ_Name = $('#univName').val();
    $.ajax({
        url: "https://localhost:7294/api/University/" + id,
        type: "PUT",
        data: JSON.stringify(univ),
        contentType: "application/json; charset=utf-8",
    }).then((result) => {
        if (result.status == 200) {
            //alert(result.message);
            $(document).Toasts('create', {
                class: 'bg-success',
                title: 'Success',
                autohide: true,
                delay: 750,
                body: result.message
            });
            $('#univTable').DataTable().ajax.reload();
            $('#modal-default').modal('hide');
        } else {
            //alert(result.message);
            Swal.fire({
                icon: "error",
                title: result.message,
                showConfirmButton: false,
                timer: 1500
            });
        }
    });
}

function deleteUniv(id) {
    var confirmation = confirm("Are you sure want to delete?")
    if (confirmation) {
        $.ajax({
            url: "https://localhost:7294/api/University/" + id,
            type: "DELETE",
            contentType: "application/json; charset=utf-8",
        }).then((result) => {
            if (result.status == 200) {
                //alert(result.message);
                $(document).Toasts('create', {
                    class: 'bg-success',
                    title: 'Success',
                    autohide: true,
                    delay: 750,
                    body: result.message
                });
                $('#univTable').DataTable().ajax.reload();
            } else {
                alert(result.message);
            }
        });
    } else {
        alert("Deletion Abort!");
    }
}

$('[data-tooltip="tooltip"]').tooltip({ trigger: "hover" });

$(document).ajaxComplete(function () {
    $('[data-tooltip="tooltip"]').tooltip({
        trigger: "hover",
    });
});

document.getElementById('modalButton').addEventListener('click', function () {
    document.getElementById('modalEdit').style.display = "none";
    document.getElementById('modalAdd').style.display = "block";
})

$('#modal-default').on('hidden.bs.modal', function () {
    $("#univForm").trigger('reset');
});


$('#univForm').validate({
    rules: {
        univName: {
            required: true,
        },
    },
    messages: {
        univName: {
            required: "Please enter a University Name",
        },
    },
    errorElement: 'span',
    errorPlacement: function (error, element) {
        error.addClass('invalid-feedback');
        element.closest('.form-group').append(error);
    },
    highlight: function (element, errorClass, validClass) {
        $(element).addClass('is-invalid');
    },
    unhighlight: function (element, errorClass, validClass) {
        $(element).removeClass('is-invalid');
    }
});