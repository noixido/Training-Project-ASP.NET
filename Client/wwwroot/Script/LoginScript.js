$(document).ready(function () {
    $('#loginForm').submit(function (e) {
        e.preventDefault();

        var theLogin = new Object();
        theLogin.username = $('#email').val();
        theLogin.password = $('#password').val();

        $.ajax({
            url: "https://localhost:7294/api/Register/Login",
            method: "POST",
            data: JSON.stringify(theLogin),
            contentType: "application/json; charset=utf-8",
            beforeSend: function () {
                document.getElementById('loader').style.display = "block";
            },
            complete: function () {
                document.getElementById('loader').style.display = "none";
            },
            success: function (result) {
                localStorage.setItem("jwtToken", result.data);

                const token = localStorage.getItem("jwtToken");
                const decodedToken = jwt_decode(token);

                let userData = {
                    username: decodedToken.Username,
                    fullname: decodedToken.fullName,
                    role: decodedToken.role
                };

                localStorage.setItem('data', JSON.stringify(userData));
                //console.log(userData.role);

                if (userData.role == "Admin") {
                    //console.log("Admin!");
                    var redirectUrl = localStorage.getItem('redirectUrl');
                    if (redirectUrl) {
                        toastr.success(result.message);
                        window.location.href = redirectUrl;
                        localStorage.removeItem('redirectUrl');
                    } else {
                        toastr.success(result.message);
                        window.location.href = '/';
                    }
                } else {
                    //console.log("Employee");
                    var redirectUrl = localStorage.getItem('redirectUrl');
                    if (redirectUrl) {
                        toastr.success(result.message);
                        window.location.href = redirectUrl;
                        localStorage.removeItem('redirectUrl');
                    } else {
                        toastr.success(result.message);
                        window.location.href = '/employee';
                    }
                }
            },
            error: function (err) {
                //console.log(err.responseJSON.message);
                toastr.error(err.responseJSON.message);
            }
        });
    });
});

function logout() {
    //debugger;

    Swal.fire({
        title: "Logout",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes"
    }).then((result) => {
        if (result.isConfirmed) {
            //localStorage.removeItem('isLoggedIn');
            localStorage.removeItem('jwtToken');
            window.location.href = '/login';
        }
    });

    //alert("logout");
}