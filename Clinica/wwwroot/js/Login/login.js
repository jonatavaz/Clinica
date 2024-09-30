function login() {
    console.log("Tentando fazer login...");

    const formData = $('#loginForm').serialize();
    console.log("Dados do formulário:", formData);

    $.ajax({
        type: "POST",
        url: '/Login/Authenticate',
        data: formData,
        success: function (response) {
            console.log("Resposta do servidor:", response);
            if (response.success) {
                window.location.href = '/Consultas/ListConsulta';
            } else {
                alert(response.message);
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.error("Erro:", textStatus, errorThrown);
            alert("Ocorreu um erro ao tentar fazer login.");
        }
    });
}
