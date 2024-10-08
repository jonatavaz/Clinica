$(document).ready(function () {
    ListConsulta();
});

function CreateConsultaModal(pacienteId) {

    let id = pacienteId;

    $.ajax({
        type: "GET",
        url: '/Consultas/CreateConsultaModal',
        dataType: "html",
        data: {
            id: id            
        },
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
    let pacienteId = $('#PacienteId').val();
    let medicoId = $('#MedicoId').val();
    let dataHora = $('#DataHora').val();

    if (!pacienteId || !medicoId || !dataHora) {
        alert("Por favor, preencha todos os campos obrigatórios.");
        return;
    }

    $.ajax({
        type: "POST",
        url: `/Consultas/EditConsulta`,
        data: {
            ConsultaId: consultaId,
            PacienteId: pacienteId, 
            MedicoId: medicoId,
            DataHora: dataHora
        },
        dataType: "json",
        success: function (response) {
            if (response.status) {
                ListConsulta();
                $('#modalCreateConsulta').modal('hide');
                alert('Consulta editada com sucesso!');
            } else {
                alert('Erro: ' + (response.message || 'Ocorreu um erro ao editar a consulta.'));
            }
        },
        error: function () {
            alert("Ocorreu um erro ao editar a consulta.");
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
    let PacienteNome = $('#NomePaciente').val();
    let MedicoId = $('#MedicoId').val();
    let Email = $('#Email').val();
    let DataNascimento = $('#DataNascimento').val();
    let DataHora = $('#DataHora').val();
    let consultaId = $('#ConsultaId').val() || 0;

    
    if (PacienteNome && MedicoId && Email && DataHora) {
        
        if (confirm('Deseja salvar a consulta?')) {
            
            let url = consultaId > 0 ? '/Consultas/EditConsulta' : '/Consultas/CreateConsulta';
            let data = {
                PacienteNome: PacienteNome,
                MedicoId: MedicoId,
                Email: Email,
                DataNascimento: DataNascimento,
                DataHora: DataHora,
                ConsultaId: consultaId 
            };

            
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
                        alert('Ocorreu um erro: ' + (response.message || 'Erro ao salvar a consulta.'));
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
    if (consultaId && confirm('Deseja realmente excluir esta consulta?')) {
        $.ajax({
            type: "POST",
            url: '/Consultas/DeleteConsulta',
            dataType: "json",
            data: { consultaId: consultaId },
            success: function (response) {
                if (response.status) {
                    ListConsulta();
                    alert('Consulta excluída com sucesso!');
                } else {
                    alert('Erro: ' + response.message);
                }
            },
            error: function () {
                alert("Ocorreu um erro ao excluir a consulta.");
            }
        });
    } else {
        alert("ID da consulta inválido.");
    }
}

