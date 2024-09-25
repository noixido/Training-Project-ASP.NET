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
            success: function (result) {
                if (result.message == "Login Success!") {
                    localStorage.setItem('isLoggedIn', true);

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
                    //console.log(err.responseJSON.message);
                    toastr.error(err.responseJSON.message);
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
    localStorage.removeItem('isLoggedIn');
    window.location.href = '/login';
}