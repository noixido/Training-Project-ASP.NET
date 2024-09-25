$('[data-tooltip="tooltip"]').tooltip({ trigger: "hover" });

document.getElementById('modalButton').addEventListener('click', function () {
    $('#empForm').find('.is-invalid').removeClass('is-invalid');
    $("#empForm").trigger('reset');
    $('#empForm').find('.select2bs4').val(null).trigger('change');
});

$(function () {
    $.validator.setDefaults({
        submitHandler: function (e) {
            $("#empForm").submit(function (e) {
                e.preventDefault();
            });
        }
    });
    $('#empForm').validate({
        rules: {
            firstName: {
                required: true,
            },
            lastName: {
                required: true,
            },
            phone: {
                required: true,
                range: [3, 15]
            },
            birthDate: {
                required: true,
            },
            email: {
                required: true,
                email: true
            },
            degree: {
                required: true,
            },
            gpa: {
                required: true,
                number: true,
                range: [0, 4]
            },
            university: {
                required: true,
            },
        },
        messages: {
            firstName: {
                required: "First Name is required!",
            },
            lastName: {
                required: "Last Name is required!",
            },
            phone: {
                required: "Phone Number is required!",
                range: "Please enter a Phone number between 3 digits and 15 digits!"
            },
            birthDate: {
                required: "Birth Date is required!",
            },
            email: {
                required: "Email is required!",
                email: "Please enter a valid Email!"
            },
            degree: {
                required: "Degree is required!",
            },
            gpa: {
                required: "GPA is required!",
                number: "GPA must be a number!",
                range: "GPA must be from 0 to 4! Comma number is allowed"
            },
            university: {
                required: "University is required!",
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

function addEmp(){
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
        $('#university').append('<option value="" selected disabled>== Select University ==</option>');
        $.each(result.data, function (index, univ) {
            $('#university').append('<option value="' + univ.univ_Id + '">' + univ.univ_Name + '</option>');
        });
    });
});