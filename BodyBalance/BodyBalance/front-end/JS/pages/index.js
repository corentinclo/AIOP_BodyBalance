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

window.app.mappers['#contact'] = function () {
    alert('Nous contacter');
    return false;
}

window.app.mappers['#register'] = function () {
    $.get('pages/Register.html', null, function (data) {
        $('#main').html(data);
    });
    return false;
}

window.app.ajaxifyForm('#login_form', function (result) {
    window.app.token = result.access_token;
    if ($('#cookie_input').is(':checked')) {
        document.cookie = "access_token=" + result.access_token + ";expires=Thu, 3 Dec 2015 12:00:00 UTC; path=/";
    }
    $.get('pages/MainPage.html', null, function (data) {
        $('#main').html(data);
    });
}, function (result) {
    alert('Mauvais login et/ou mot de passe');
}, 'application/json');

$(function () {
    window.app.hrefToFunction('body');
    if (document.cookie.indexOf('access_token') >= 0) {
        window.app.token = /.*access_token=([a-zA-Z0-9]+)/.exec(document.cookie)[0];
        $.get('pages/MainPage.html', null, function (data) {
            $('#main').html(data);
        });
    }
});