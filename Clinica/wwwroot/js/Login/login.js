import { Helper } from '../Global/Helper.js';
const helper = new Helper();
$(document).ready(function () {
    $('.date').mask('00/00/0000');
    $('.time').mask('00:00:00');
    $('.date_time').mask('00/00/0000 00:00:00');
    $('.cep').mask('00000-000');
    $('.phone').mask('0000-0000');
    $('.phone_with_ddd').mask('(00) 0000-0000');
    $('.phone_us').mask('(000) 000-0000');
    $('.mixed').mask('AAA 000-S0S');
    $('.cpf').mask('000.000.000-00', { reverse: true });
    $('.cnpj').mask('00.000.000/0000-00', { reverse: true });
    $('.money').mask('000.000.000.000.000,00', { reverse: true });
    $('.money2').mask("#.##0,00", { reverse: true });
    $('.ip_address').mask('0ZZ.0ZZ.0ZZ.0ZZ', {
        translation: {
            'Z': {
                pattern: /[0-9]/, optional: true
            }
        }
    });
    $('.ip_address').mask('099.099.099.099');
    $('.percent').mask('##0,00%', { reverse: true });
    $('.clear-if-not-match').mask("00/00/0000", { clearIfNotMatch: true });
    $('.placeholder').mask("00/00/0000", { placeholder: "__/__/____" });
    $('.fallback').mask("00r00r0000", {
        translation: {
            'r': {
                pattern: /[\/]/,
                fallback: '/'
            },
            placeholder: "__/__/____"
        }
    });
    $('.selectonfocus').mask("00/00/0000", { selectOnFocus: true });
});

window.login = login;

async function login() {

    let Email = $('#Email').val();
    let Senha = $('#Senha').val();
        

     try {
         const response = await helper.postFormData('/Login/Authenticate', { "Email": Email, "Senha": Senha })
         if (response.success) {
             window.location.href = '/Consultas/Index';
         } else {
             alert(response.message);
         }

     } catch (e) {
         console.log(e);
     }

     //const formData = $('#loginForm').serialize();
    //$.ajax({
    //    type: "POST",
    //    url: '/Login/Authenticate',
    //    data: formData,
    //    success: function (response) {
    //        console.log("Resposta do servidor:", response);
    //        if (response.success) {
    //            window.location.href = '/Consultas/Index';
    //        } else {
    //            alert(response.message);
    //        }
    //    },
    //    error: function (jqXHR, textStatus, errorThrown) {
    //        console.error("Erro:", textStatus, errorThrown);
    //        alert("Ocorreu um erro ao tentar fazer login.");
    //    }
    //});
}
