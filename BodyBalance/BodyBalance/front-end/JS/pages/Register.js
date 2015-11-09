window.app.ajaxifyForm('#register_form', function () {
    location.reload();
    return false;
}, function () {
    alert('Erreur');
    return false;
});

window.app.hrefToFunction('#main');