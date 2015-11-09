/*
//Méthode toujours appelée, exemple
window.app.mappers['*'] = function (sender) { //Toujours appelé
    $(sender).tooltip({ title: "Clické !", trigger: 'manual' , placement: 'bottom'}).tooltip('show');
    setTimeout(function () {
        $(sender).tooltip('hide').tooltip('destroy');
    }, 1000);
    return true;
}
*/

window.app.mappers['#'] = function () {
    location.reload();
    return false;
}

window.app.mappers['#about'] = function () {
    alert('A propos de');
    return false;
}

window.app.mappers['#register'] = function () {
    $.get('pages/Register.html', null, function (data) {
        $('#main').html(data);
    });
    return false;
}

window.app.ajaxifyForm('#login_form', function (result) {
    window.app.storeLoginParameters(result.userName, result.access_token, $('#cookie_input').is(':checked'));
    $.get('pages/MainPage.html', null, function (data) {
        $('#main').html(data);
    });
}, function (result) {
    bootbox.alert('Bad username or password');
}, 'application/json');

$(function () {
    window.app.hrefToFunction('body');
    if (window.app.processCookies()) {
        //Vérification de la validité du token
        window.app.sendRestRequest('/Account/IsValidToken', 'GET', null, function () {
            //Good credentials
            $.get('pages/MainPage.html', null, function (data) {
                $('#main').html(data);
            });
        }, function () {
            //Bad credentials
            window.app.clearLoginParameters();
            bootbox.alert('Your authorization has ended. Please log in again.');
        });
    }
});