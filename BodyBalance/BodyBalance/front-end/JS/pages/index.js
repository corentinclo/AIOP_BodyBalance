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
    $.get('pages/MainPage.html', null, function (data) {
        $('#main').html(data);
    });
}, function (result) {
    $.get('pages/MainPage.html', null, function (data) {
        alert('Mauvais login et/ou mot de passe');
    });
}, 'application/json');

$(function () {
    window.app.hrefToFunction('body');
});