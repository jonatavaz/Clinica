class Helper {
    constructor() {
        this.headers = { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() };
    }

    /**
     * POST que retorna um JSON
     * @param {string} url - controler/action
     * @param {Object} data - corpo
     * @returns {Promise<Object>} resposta
     */
    async postFormData(url, data) {
        if (typeof data === 'object' && !(data instanceof FormData)) {
            const formData = new FormData();
            for (const key in data) {
                if (Array.isArray(data[key])) {
                    data[key].forEach((item, index) => {
                        for (const subKey in item) {
                            formData.append(`${key}[${index}][${subKey}]`, item[subKey]);
                        }
                    });
                } else {
                    formData.append(key, data[key]);
                }
            }
            data = formData;
        }
        try {
            const response = await fetch(url, {
                method: 'POST',
                headers: {
                    ...this.headers,
                },
                body: data
            });
            return await response.json();
        } catch (error) {
            console.error('Erro na solicitação:', error);
            throw error;
        }
    }

    /**
     * POST que retorna uma partial
     * @param {string} url - controler/action
     * @param {Object} data - corpo
     * @returns {Promise<Object>} resposta
     */
    async postPartial(url, data) {
        if (typeof data === 'object' && !(data instanceof FormData)) {
            const formData = new FormData();
            for (const key in data) {
                if (Array.isArray(data[key])) {
                    data[key].forEach((item, index) => {
                        for (const subKey in item) {
                            formData.append(`${key}[${index}][${subKey}]`, item[subKey]);
                        }
                    });
                } else {
                    formData.append(key, data[key]);
                }
            }
            data = formData;
        }
        try {
            const response = await fetch(url, {
                method: 'POST',
                headers: {
                    ...this.headers,
                },
                body: data
            });
            return await response.text();
        } catch (error) {
            console.error('Erro na solicitação:', error);
            throw error;
        }
    }

    async getJson(url, data) {
        try {
            const response = await fetch(`${url}?${data}`, {
                method: 'GET',
                headers: this.headers
            });
            return await response.json();
        } catch (error) {
            console.error('Erro na solicitação:', error);
            throw error;
        }
    }

    async postJson(url, data) {
        try {
            const response = await fetch(url, {
                method: 'POST',
                headers: {
                    ...this.headers,
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(data)
            });
            return await response.json();
        } catch (error) {
            console.error('Erro na solicitação:', error);
            throw error;
        }
    }

}

class Alerts {
    
    Mensagem(msg) {
        Swal.fire(
            'Atenção',
            msg,
            'info'
        )
    }

    Success(msg) {
        Swal.fire(
            'Sucesso',
            msg,
            'success'
        )
    }

    Erro(msg) {
        Swal.fire(
            'Erro',
            msg,
            'error'
        )
    }

    ModalConfirm(title, msg, callback) {
    Swal.fire({
        title: title,
        text: msg,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Confirmar',
        cancelButtonText: 'Cancelar'
    }).then((result) => {
        if (result.isConfirmed) {
            callback()
        }
    })
}
}

export {
    Helper, Alerts
};
