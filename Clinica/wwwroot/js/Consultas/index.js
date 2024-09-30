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

function InserirConsulta() {
    let PacienteId = document.getElementById('PacienteId').value;
    let MedicoId = document.getElementById('MedicoId').value;
    let Email = document.getElementById('Email').value;
    let DataHora = document.getElementById('DataHora').value;

    if (PacienteId != 0) {
        if (confirm('Deseja Agendar a consulta?')) {
            $.ajax({
                type: "POST",
                url: '/Consultas/CreateConsulta',
                dataType: "json",
                data: {
                    "PacienteId": PacienteId,
                    "MedicoId": MedicoId,
                    "Email": Email,
                    "DataHora": DataHora,
                },
                success: function (response) {
                    if (response.status) {
                         
                        ListConsulta()

                        $('#modalCreateConsulta').modal('hide');

                        alert(`Sua Próxima Consulta Será o Paciente: ${response.nome}` )
                    }
                    else {
                        alert('Ocorreu um erro')
                    }
                    
                },
                error: function () {
                    alert("Ocorreu um erro ao agendar a consulta.");
                }
            });
        }
    } else {
        alert("Insira os dados abaixo");
    }
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

function EditConsulta(consultaId) {
    $.ajax({
        type: "GET",
        url: `/Consultas/CreateConsultaModal`,
        data: {
            "id": consultaId
        },
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

function SaveConsulta() {
    let PacienteId = document.getElementById('PacienteId').value;
    let MedicoId = document.getElementById('MedicoId').value;
    let Email = document.getElementById('Email').value;
    let DataHora = document.getElementById('DataHora').value;
    let consultaId = document.getElementById('ConsultaId') ? document.getElementById('ConsultaId').value : 0;

    if (PacienteId != 0) {
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
        alert("Insira os dados abaixo");
    }
}

function DeletConsulta(consultaId) {
    if (confirm('Deseja realmente excluir esta consulta?')) {
        $.ajax({
            type: "POST",
            url: '/Consultas/Delete',
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

function RetornarTela() {
    $('#createConsulta').hide()
    $('#gridConsulta').show();
}

function CreateConsulta() {

    $.ajax({
        type: "GET",
        url: '/Consultas/CreateConsulta',
        dataType: "html",
        success: function (response) {            
            $('#createConsulta').html(response).ready(function () {
                $('#gridConsulta').hide();
                $('#createConsulta').show();
            });
        },
        error: function () {
            alert("Ocorreu um erro ao agendar a consulta.");
        }
    });
}