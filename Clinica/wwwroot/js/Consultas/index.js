$(document).ready(function () {
    ListConsulta();
});

function CreateConsultaModal() {
    $.ajax({
        type: "GET",
        url: '/Consultas/CreateConsultaModal',
        dataType: "html",
        success: function (response) {
            $('#createConsulta').html(response).ready(function () {
                $('#modalCreateConsulta').modal('show');
            });
        },
        error: function () {
            alert("Ocorreu um erro ao agendar a consulta.");
        }
    });
}

function EditConsulta(consultaId) {
    $.ajax({
        type: "GET",
        url: `/Consultas/CreateConsultaModal`,
        data: { "id": consultaId },
        dataType: "html",
        success: function (response) {
            $('#createConsulta').html(response).ready(function () {
                $('#modalCreateConsulta').modal('show');
            });
        },
        error: function () {
            alert("Ocorreu um erro ao carregar os dados da consulta.");
        }
    });
}

function ListConsulta() {
    $.ajax({
        type: "GET",
        url: '/Consultas/ListConsulta',
        dataType: "html",
        success: function (response) {
            $('#gridConsulta').html(response);
        },
        error: function () {
            alert("Ocorreu um erro ao listar as consultas.");
        }
    });
}

function SaveConsulta() {
    let PacienteId = $('#PacienteId').val();
    let MedicoId = $('#MedicoId').val();
    let Email = $('#Email').val();
    let DataHora = $('#DataHora').val();
    let consultaId = $('#ConsultaId').val() || 0;

    if (PacienteId) {
        if (confirm('Deseja salvar a consulta?')) {
            let url = consultaId > 0 ? '/Consultas/UpdateConsulta' : '/Consultas/CreateConsulta';
            let data = consultaId > 0 ? { id: consultaId, PacienteId, MedicoId, Email, DataHora } : { PacienteId, MedicoId, Email, DataHora };

            $.ajax({
                type: "POST",
                url: url,
                dataType: "json",
                data: data,
                success: function (response) {
                    if (response.status) {
                        ListConsulta();
                        $('#modalCreateConsulta').modal('hide');
                        alert('Consulta salva com sucesso!');
                    } else {
                        alert('Ocorreu um erro');
                    }
                },
                error: function () {
                    alert("Ocorreu um erro ao salvar a consulta.");
                }
            });
        }
    } else {
        alert("Por favor, preencha todos os campos obrigatórios.");
    }
}

function DeleteConsulta(consultaId) {
    if (confirm('Deseja realmente excluir esta consulta?')) {
        $.ajax({
            type: "POST",
            url: '/Consultas/DeleteConsulta',
            dataType: "json",
            data: { id: consultaId },
            success: function (response) {
                if (response.status) {
                    alert('Consulta excluída com sucesso.');
                    ListConsulta();
                } else {
                    alert('Erro ao excluir a consulta.');
                }
            },
            error: function () {
                alert("Ocorreu um erro ao tentar excluir a consulta.");
            }
        });
    }
}
