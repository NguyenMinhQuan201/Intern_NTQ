class login {
    constructor() {
        this.GetToken();
        this.HideText();
    }
    GetToken() {
        $('#lay_token').on('click', function () {
            var user = $('#user').val();
            var pass = $('#pass').val();
            console.log(user);
            console.log(pass);
            $.ajax({
                type: "POST",
                url: "https://localhost:7139/api/User/authenticate",
                data: JSON.stringify({
                    UserName: user,
                    Password: pass,
                    RememberMe: true
                }),
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                
                success: function (response) {
                    console.log(response.token);
                    if (response.token == "") {
                        $('#canhbao').html("Sai tài khoản hoặc mật khẩu")
                    }
                    else {
                        $.ajax({
                            url: "/Admin/Login/Index",
                            data:{
                                token: response.token
                            },
                            dataType: "json",
                            type: "POST",
                            success: function (response2) {
                                console.log(response2.status);
                                window.location.href = "/Admin/Admin/Index"
                            }
                        })
                    }
                }
            }).fail(function (jqXHR, textStatus, errorThrown) {
                $('#canhbao').html("Hong biet nua")
            });
        });
    }
    HideText() {
        $("#user").keypress(function () {
            console.log("Handler for .keypress() called.");
            $('#canhbao').html("")
        });
        $("#pass").keypress(function () {
            console.log("Handler for .keypress() called.");
            $('#canhbao').html("")
        });
    }
}
new login();
