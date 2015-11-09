window.app.ajaxifyFormJson('#register_form', function () {
    location.reload();
    return false;
}, function () {
    alert('Erreur');
    return false;
},
'application/json');

window.app.hrefToFunction('#main');