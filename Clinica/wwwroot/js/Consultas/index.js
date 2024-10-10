import { Helper } from '../Global/Helper.js';
const helper = new Helper();
$(document).ready(function () {
    ListConsulta();
});

window.CreateConsultaModal = CreateConsultaModal;
window.EditConsulta = EditConsulta;
window.SaveConsulta = SaveConsulta;
window.DeleteConsulta = DeleteConsulta;


async function CreateConsultaModal(pacienteId) {

    //let id = pacienteId;
    try {
        const response = await helper.postPartial('/Consultas/CreateConsultaModal', { "id": pacienteId });

        if (response != null) {
            $('#createConsulta').html(response).ready(function () {
                $('#modalCreateConsulta').modal('show');
            })
        }

    } catch (e) {
        console.log(e);
    }



    //fetch(`/Consultas/CreateConsultaModal/${id}`, {
    //    method: 'GET'
    //})
    //    .then((res) => res.text())
    //    .then((data) =>
    //        $('#createConsulta').html(data).ready(function () {
    //            $('#modalCreateConsulta').modal('show');
    //        }))
    //    .catch((err) => console.error(err));
}

//$.ajax({
//    type: "GET",
//    url: '/Consultas/CreateConsultaModal',
//    dataType: "html",
//    data: {
//        id: id
//    },
//    success: function (response) {
//        $('#createConsulta').html(response).ready(function () {
//            $('#modalCreateConsulta').modal('show');
//        });
//    },
//    error: function () {
//        alert("Ocorreu um erro ao agendar a consulta.");
//    }
//});

async function EditConsulta(consultaId) {
    let pacienteId = $('#PacienteId').val();
    let medicoId = $('#MedicoId').val();
    let dataHora = $('#DataHora').val();

    if (!pacienteId || !medicoId || !dataHora) {
        alert("Por favor, preencha todos os campos obrigatórios.");
        return;
    }

    try {
        const response = await helper.postFormData('/Consultas/EditConsulta', { "consultaId": consultaId, "pacienteId": pacienteId, "medicoId": medicoId, "dataHora": dataHora });

        if (response.status) {
            await ListConsulta();
            $('#modalCreateConsulta').modal('hide');
            alert('Consulta editada com sucesso!');
        } else {
            alert('Erro: ' + (response.message || 'Ocorreu um erro ao editar a consulta.'));
        }

    } catch (e) {
        console.log(e);
    }



    //var formData = new FormData;
    //formData.append('consultaId', consultaId);
    //formData.append('pacienteId', $('#PacienteId').val());
    //formData.append('medicoId', $('#MedicoId').val());
    //formData.append('dataHora', $('#DataHora').val());


//    fetch('/Consultas/EditConsulta', {
//        method: 'POST',
//        body: formData
//    })
//        .then((res) => res.json())
//        .then((data) => {
//            if (data.status) {
//                ListConsulta();
//                $('#modalCreateConsulta').modal('hide');
//                alert('Consulta editada com sucesso!');
//            } else {
//                alert('Erro: ' + (data.message || 'Ocorreu um erro ao editar a consulta.'));

//            }
//        }).catch((err) => console.error(err));
//}

//    $.ajax({
//        type: "POST",
//        url: `/Consultas/EditConsulta`,
//        data: {
//            ConsultaId: consultaId,
//            PacienteId: pacienteId,
//            MedicoId: medicoId,
//            DataHora: dataHora
//        },
//        dataType: "json",
//        success: function (response) {
//            if (response.status) {
//                ListConsulta();
//                $('#modalCreateConsulta').modal('hide');
//                alert('Consulta editada com sucesso!');
//            } else {
//                alert('Erro: ' + (response.message || 'Ocorreu um erro ao editar a consulta.'));
//            }
//        },
//        error: function () {
//            alert("Ocorreu um erro ao editar a consulta.");
//        }
//    });
}

async function ListConsulta() {

    const response = await helper.postPartial('/Consultas/ListConsulta', {});
    if (response != null) {
        $('#gridConsulta').html(response)
    }

    //fetch(`/Consultas/ListConsulta`, {
    //    method: 'GET'
    //}).then((res) => res.text())
    //    .then((data) => $('#gridConsulta').html(data))
    //    .catch((err) => console.error(err));
}

//    $.ajax({
//        type: "GET",
//        url: '/Consultas/ListConsulta',
//        dataType: "html",
//        success: function (response) {
//            $('#gridConsulta').html(response);
//        },
//        error: function () {
//            alert("Ocorreu um erro ao listar as consultas.");
//        }
//    });
//}

async function SaveConsulta() {
    let pacienteNome = $('#NomePaciente').val();
    let medicoId = $('#MedicoId').val();
    let email = $('#Email').val();
    let dataNascimento = $('#DataNascimento').val();
    let dataHora = $('#DataHora').val();
    let consultaId = $('#ConsultaId').val() || 0;
    //var formData = new FormData();

    //formData.append('pacienteNome', $('#NomePaciente').val());
    //formData.append('medicoId', $('#MedicoId').val());
    //formData.append('email', $('#Email').val());
    //formData.append('dataNascimento', $('#DataNascimento').val());
    //formData.append('dataHora', $('#DataHora').val());
    //formData.append('consultaId', $('#ConsultaId').val() || 0);

    if (pacienteNome && medicoId && email && dataHora) {

        if (confirm('Deseja salvar a consulta?')) {

            let url = consultaId > 0 ? '/Consultas/EditConsulta' : '/Consultas/CreateConsulta';
            try {
                const response = await helper.postFormData(url, { "pacienteNome": pacienteNome, "medicoId": medicoId, "email": email, "dataNascimento": dataNascimento, "dataHora": dataHora, "consultaId": consultaId });

                if (response.status) {
                     await ListConsulta();
                    $('#modalCreateConsulta').modal('hide');
                    alert('Consulta salva com sucesso!');
                } else {
                    alert('Ocorreu um erro: ' + (response.message || 'Erro ao salvar a consulta.'));
                }
            } catch (e) {
                console.log(e);
            }
            //fetch(url, {
            //    method: 'POST',
            //    body: formData
            //}).then((res) => res.json())
            //    .then((data) => {
            //        if (data.status) {
            //            ListConsulta();
            //            $('#modalCreateConsulta').modal('hide');
            //            alert('Consulta salva com sucesso!');
            //        } else {
            //            alert('Ocorreu um erro: ' + (data.message || 'Erro ao salvar a consulta.'));
            //        }
            //    }).catch((err) => console.error(err));


            //        $.ajax({
            //            type: "POST",
            //            url: url,
            //            dataType: "json",
            //            data: data,
            //            success: function (response) {
            //                if (response.status) {
            //                    ListConsulta(); 
            //                    $('#modalCreateConsulta').modal('hide'); 
            //                    alert('Consulta salva com sucesso!'); 
            //                } else {
            //                    alert('Ocorreu um erro: ' + (response.message || 'Erro ao salvar a consulta.'));
            //                }
            //            },
            //            error: function () {
            //                alert("Ocorreu um erro ao salvar a consulta.");
            //            }
            //        });
        }
    } else {
        alert("Por favor, preencha todos os campos obrigatórios.");
    }
}

async function DeleteConsulta(consultaId) {

    //formData = new FormData();/
    //formData.append('consultaId', consultaId);

    if (consultaId && confirm('Deseja realmente excluir esta consulta?')) {

        try {
            const response = await helper.postFormData('/Consultas/DeleteConsulta', { "consultaId": consultaId });

            if (response.status) {
                await ListConsulta();
                alert('Consulta excluída com sucesso!');
            } else {
                alert('Erro: ' + response.message);
            }
        } catch (e) {
            console.log(e);
        }
        //fetch('/Consultas/DeleteConsulta', {
        //    method: 'POST',
        //    body: formData
        //}).then((res) => res.json())
        //    .then((data) => {
        //        if (data.status) {
        //            ListConsulta();
        //            alert('Consulta excluída com sucesso!');
        //        } else {
        //            alert('Erro: ' + data.message);
        //        }
        //    }).catch((err) => console.error(err));

        //$.ajax({
        //    type: "POST",
        //    url: '/Consultas/DeleteConsulta',
        //    dataType: "json",
        //    data: { consultaId: consultaId },
        //    success: function (response) {
        //        if (response.status) {
        //            ListConsulta();
        //            alert('Consulta excluída com sucesso!');
        //        } else {
        //            alert('Erro: ' + response.message);
        //        }
        //    },
        //    error: function () {
        //        alert("Ocorreu um erro ao excluir a consulta.");
        //    }
        //});
    } else {
        alert("ID da consulta inválido.");
    }
}
