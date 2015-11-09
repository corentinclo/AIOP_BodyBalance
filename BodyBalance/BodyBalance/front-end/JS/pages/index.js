window.app.mappers['*'] = function (sender) { //Toujours appelé
    $(sender).tooltip({ title: "Clické !", trigger: 'manual' , placement: 'bottom'}).tooltip('show');
    setTimeout(function () {
        $(sender).tooltip('hide').tooltip('destroy');
    }, 1000);
    return true;
}

window.app.mappers['#'] = function () {
    alert("Retour à l'accueil");
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
    alert('S\'enregistrer');
    return false;
}

window.app.ajaxifyForm('#login_form', function (result) {
    $.get('pages/MainPage.html', null, function (data) {
        $('#main').html(data);
    });
}, function (result) {
    $.get('pages/MainPage.html', null, function (data) {
        $('#main').html(data);
    });
}, 'application/json');

$(function () {
    window.app.hrefToFunction('body');
});