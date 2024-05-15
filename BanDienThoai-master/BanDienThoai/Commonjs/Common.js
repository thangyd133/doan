function NotiSuccess(message, callback) {
    Toastify({
        text: message,
        duration: 5000,
        close: true,
        gravity: "top", // `top` or `bottom`
        position: "right", // `left`, `center` or `right`
        style: {
            background: '#53D975'
        }
    }).showToast();
}

function NotiError(message, callback) {
    Toastify({
        text: message,
        duration: 5000,
        close: true,
        gravity: "top", // `top` or `bottom`
        position: "left", // `left`, `center` or `right`
        style: {
            background: '#F34747'
        }
    }).showToast();
}
