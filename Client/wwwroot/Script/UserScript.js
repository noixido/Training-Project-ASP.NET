var data = localStorage.getItem('data');
var parsedData = JSON.parse(data);
var userEmail = parsedData.username;

//console.log(email);

$('[data-tooltip="tooltip"]').tooltip({ trigger: "hover" });

document.getElementById('modalButton').addEventListener('click', function () {
    $('#empForm').find('.is-invalid').removeClass('is-invalid');
});
document.getElementById('modalChangePassButton').addEventListener('click', function () {
    $('#changePassForm').trigger('reset');
    $('#changePassForm').find('.is-invalid').removeClass('is-invalid');
});

$(document).ready(function () {
    $.ajax({
        url: "https://localhost:7294/api/Register/getByEmail/" + userEmail,
        type: "GET",
        dataType: "json",
        success: function (result) {
            //console.log(result)
            document.getElementById('nik').textContent = result.data.nik;
            document.getElementById('fullName').textContent = result.data.fullName;
            document.getElementById('phone').textContent = result.data.phone;
            document.getElementById('birthDate').textContent = result.data.birthDate;
            document.getElementById('email').textContent = result.data.email;
            document.getElementById('degree').textContent = result.data.degree;
            document.getElementById('gpa').textContent = result.data.gpa;
            document.getElementById('univName').textContent = result.data.univ_Name;
            document.getElementById('roleName').textContent = result.data.roleName;
        },
        error: function (err) {
            console.log(err.responseJSON.message);
        }
    });
});

$(document).ready(function () {
    $.ajax({
        url: "https://localhost:7294/api/University",
        type: "GET",
        headers: {
            "Authorization": "Bearer " + localStorage.getItem("jwtToken")
        },
        dataType: "json",
    }).then((result) => {
        $('#universityForm').empty();
        $('#universityForm').append('<option value="" selected disabled>== Select University ==</option>');
        $.each(result.data, function (index, univ) {
            $('#universityForm').append('<option value="' + univ.univ_Id + '">' + univ.univ_Name + '</option>');
        });

        $.ajax({
            url: "https://localhost:7294/api/Register/getEmployeeByEmail/" + userEmail, // This is your other API that returns user info
            type: "GET",
            dataType: "json",
        }).then((userResult) => {
            // Set the selected university based on the user's data
            $('#universityForm').val(userResult.data.univ_Id).trigger('change');
        });
    });
});


$(document).ready(function () {
    $.ajax({
        url: "https://localhost:7294/api/Register/getEmployeeByEmail/" + userEmail,
        type: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            //console.log(result.data);
            //debugger;
            $('#firstNameForm').val(result.data.firstName);
            $('#lastNameForm').val(result.data.lastName);
            $('#phoneForm').val(result.data.phone);
            $('#birthDateForm').val(result.data.birthDate.split('T')[0]);
            $('#emailForm').val(result.data.email);
            $('#degreeForm').val(result.data.degree).trigger('change');
            $('#gpaForm').val(result.data.gpa);
        },
        error: function (err) {
            console.log(err.responseJSON.message);
        }
    });
});

function updateProfile() {
    //debugger;
    if ($("#empForm").valid()){
        var emp = {
            firstName: $("#firstNameForm").val(),
            lastName: $("#lastNameForm").val(),
            phone: $("#phoneForm").val(),
            birthDate: $("#birthDateForm").val(),
            email: $("#emailForm").val(),
            degree: $("#degreeForm").val(),
            gpa: $("#gpaForm").val(),
            univ_Id: $("#universityForm").val(),
        }
        //debugger;
        $.ajax({
            url: "https://localhost:7294/api/Register/editProfile/" + userEmail,
            type: "PUT",
            data: JSON.stringify(emp),
            contentType: "application/json; charset=utf-8",
            header: {
                "Authorization": "Bearer " + localStorage.getItem('jwtToken'),
            },
            success: function (result) {
                //debugger;
                //alert(result.message);

                $(document).Toasts('create', {
                    class: 'bg-success',
                    title: 'Success',
                    autohide: true,
                    delay: 3000,
                    body: result.message
                });
                window.location.href = "/employee"
            },
            error: function (err) {
                //debugger;
                console.log(err.responseJSON.message);
            }
        });
    }
}

function ChangeThePassword() {
    //debugger;

    //alert('change pass');

    //if ($("#changePassForm").valid()) {


        if ($("#newPass").val() !== $("#retypeNewPass").val()) {
            Swal.fire({
                icon: "Error",
                title: "The New Password that you retype is not match",
                showConfirmButton: false,
                timer: 1500
            });
        }

        var pass = {
            email: userEmail,
            oldPassword: $("#oldPass").val(),
            newPassword: $("#newPass").val(),
        }
        //debugger;
        $.ajax({
            url: "https://localhost:7294/api/Register/changePassword/",
            type: "PUT",
            data: JSON.stringify(pass),
            contentType: "application/json; charset=utf-8",
            header: {
                "Authorization": "Bearer " + localStorage.getItem('jwtToken'),
            },
            success:async function (result) {
                //debugger;
                //alert(result.message);

                await Swal.fire({
                    icon: "success",
                    title: result.message,
                    showConfirmButton: false,
                    timer: 1500
                });

                window.location.href = "/employee"
            },
            error:async function (xhr,status,error) {
                //debugger;
                //console.log(xhr.responseJSON.message);
               await Swal.fire({
                    icon: "Error",
                    title: xhr.responseJSON.message,
                    showConfirmButton: false,
                    timer: 1500
                });
            }
        });
    //}
}