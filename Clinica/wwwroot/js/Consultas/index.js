$(document).ready(function () {
    ListConsulta();
});

function CreateConsultaModal(pacienteId) {

    let id = pacienteId;

    fetch(`/Consultas/CreateConsultaModal/${id}`, {
        method: 'GET'
    })
        .then((res) => res.text())
        .then((data) =>
            $('#createConsulta').html(data).ready(function () {
                $('#modalCreateConsulta').modal('show');
            }))
        .catch((err) => console.error(err));
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

function EditConsulta(consultaId) {

    var formData = new FormData;
    formData.append('consultaId', consultaId);
    formData.append('pacienteId', $('#PacienteId').val());
    formData.append('medicoId', $('#MedicoId').val());
    formData.append('dataHora', $('#DataHora').val());


    if (!formData.get('medicoId') || !formData.get('medicoId') || !formData.get('dataHora')) {
        alert("Por favor, preencha todos os campos obrigatórios.");
        return;
    }

    fetch('/Consultas/EditConsulta', {
        method: 'POST',
        body: formData
    })
        .then((res) => res.json())
        .then((data) => {
            if (data.status) {
                ListConsulta();
                $('#modalCreateConsulta').modal('hide');
                alert('Consulta editada com sucesso!');
            } else {
                alert('Erro: ' + (data.message || 'Ocorreu um erro ao editar a consulta.'));

            }
        }).catch((err) => console.error(err));
}

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
//}

function ListConsulta() {

    fetch(`/Consultas/ListConsulta`, {
        method: 'GET'
    }).then((res) => res.text())
        .then((data) => $('#gridConsulta').html(data))
        .catch((err) => console.error(err));
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

function SaveConsulta() {
    //let PacienteNome = $('#NomePaciente').val();
    //let MedicoId = $('#MedicoId').val();
    //let Email = $('#Email').val();
    //let DataNascimento = $('#DataNascimento').val();
    //let DataHora = $('#DataHora').val();
    //let consultaId = $('#ConsultaId').val() || 0;
    var formData = new FormData();

    formData.append('pacienteNome', $('#NomePaciente').val());
    formData.append('medicoId', $('#MedicoId').val());
    formData.append('email', $('#Email').val());
    formData.append('dataNascimento', $('#DataNascimento').val());
    formData.append('dataHora', $('#DataHora').val());
    formData.append('consultaId', $('#ConsultaId').val() || 0);

    if (formData.get('pacienteNome') && formData.get('medicoId') && formData.get('email') && formData.get('dataHora')) {

        if (confirm('Deseja salvar a consulta?')) {

            let url = formData.get('consultaId') > 0 ? '/Consultas/EditConsulta' : '/Consultas/CreateConsulta';

            fetch(url, {
                method: 'POST',
                body: formData
            }).then((res) => res.json())
                .then((data) => {
                    if (data.status) {
                        ListConsulta();
                        $('#modalCreateConsulta').modal('hide');
                        alert('Consulta salva com sucesso!');
                    } else {
                        alert('Ocorreu um erro: ' + (data.message || 'Erro ao salvar a consulta.'));
                    }
                }).catch((err) => console.error(err));


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

function DeleteConsulta(consultaId) {

    formData = new FormData();
    formData.append('consultaId', consultaId);

    if (formData.get('consultaId') && confirm('Deseja realmente excluir esta consulta?')) {

        fetch('/Consultas/DeleteConsulta', {
            method: 'POST',
            body: formData
        }).then((res) => res.json())
            .then((data) => {
                if (data.status) {
                    ListConsulta();
                    alert('Consulta excluída com sucesso!');
                } else {
                    alert('Erro: ' + data.message);
                }
            }).catch((err) => console.error(err));

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
