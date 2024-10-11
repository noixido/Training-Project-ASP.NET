$(document).ready(function () {
    $('#roleTable').DataTable({
        "paging": true,
        "responsive": true,
        "lengthChange": true,
        "searching": true,
        "ordering": true,
        "info": true,
        "autoWidth": false,
        "processing": true,
        "ajax": {
            url: "https://localhost:7294/api/Role",
            type: "GET",
            headers: {
                "Authorization": "Bearer " + localStorage.getItem("jwtToken")
            },
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
            { "data": "roleId" },
            { "data": "roleName" },
            {
                "render": function (data, type, row) {
                    return '<div style="display:flex; justify-content:center; gap:5px;"><button type="button" id="editRole" class="btn btn-warning" data-tooltip="tooltip" data-placement="top" title="Edit Data" onclick="editRole(\'' + row.roleId + '\')" data-toggle="modal" data-target="#modal-default"> <i class="fas fa-pencil-alt"></i></button > ' +
                        ' | <button type="button" id="deleteRole" class="btn btn-danger" data-tooltip="tooltip" data-placement="top" title="Delete Data" onclick="deleteRole(\'' + row.roleId + '\')"><i class="fas fa-trash"></i></button></div > ';
                }
            }
        ]
    });
});

function addRole() {
    //debugger;
    var role = new Object();
    role.roleName = $('#roleName').val();
    $.ajax({
        url: "https://localhost:7294/api/Role",
        type: "POST",
        headers: {
            "Authorization": "Bearer " + localStorage.getItem("jwtToken")
        },
        data: JSON.stringify(role),
        contentType: "application/json; charset=utf-8",
    })
        .then((result) => {
            if (result.status == 200) {
                //alert(result.message);
                $(document).Toasts('create', {
                    class: 'bg-success',
                    title: 'Success',
                    autohide: true,
                    delay: 3000,
                    body: result.message
                });
                $('#roleTable').DataTable().ajax.reload();
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

function editRole(id) {
    document.getElementById('modalEdit').style.display = "block";
    document.getElementById('modalAdd').style.display = "none";
    $('#roleForm').find('.is-invalid').removeClass('is-invalid');

    $.ajax({
        url: "https://localhost:7294/api/Role/" + id,
        type: "GET",
        headers: {
            "Authorization": "Bearer " + localStorage.getItem("jwtToken")
        },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
    }).then((result) => {
        var obj = result.data;
        $('#roleName').val(obj.roleName);

        var btn = $('#modalEdit');
        btn.attr('onclick', 'edit(\'' + id + '\')');

        $('#modal-default').modal('show');
    });
}

function edit(id) {
    var role = new Object();
    role.roleName = $('#roleName').val();
    $.ajax({
        url: "https://localhost:7294/api/Role/" + id,
        type: "PUT",
        headers: {
            "Authorization": "Bearer " + localStorage.getItem("jwtToken")
        },
        data: JSON.stringify(role),
        contentType: "application/json; charset=utf-8",
    }).then((result) => {
        if (result.status == 200) {
            //alert(result.message);
            $(document).Toasts('create', {
                class: 'bg-success',
                title: 'Success',
                autohide: true,
                delay: 3000,
                body: result.message
            });
            $('#roleTable').DataTable().ajax.reload();
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

function deleteRole(id) {

    Swal.fire({
        title: "Are you sure?",
        text: "This Role will be deleted forever!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: "https://localhost:7294/api/Role/" + id,
                type: "DELETE",
                headers: {
                    "Authorization": "Bearer " + localStorage.getItem("jwtToken")
                },
                contentType: "application/json; charset=utf-8",
            }).then((result) => {
                if (result.status == 200) {
                    //alert(result.message);
                    $(document).Toasts('create', {
                        class: 'bg-success',
                        title: 'Success',
                        autohide: true,
                        delay: 3000,
                        body: result.message
                    });
                    $('#roleTable').DataTable().ajax.reload();
                } else {
                    alert(result.message);
                }
            });
        }
    });
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
    $('#roleForm').find('.is-invalid').removeClass('is-invalid');
    $("#roleForm").trigger('reset');
});

$(function () {
    $.validator.setDefaults({
        submitHandler: function (e) {
            $("#roleForm").submit(function (e) {
                e.preventDefault();
            });
        }
    });
    $('#roleForm').validate({
        rules: {
            roleName: {
                required: true,

            },
        },
        messages: {
            roleName: {
                required: "Please enter a role name"
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
});