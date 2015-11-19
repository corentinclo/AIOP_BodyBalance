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
    bootbox.dialog({
        message: "ZenLounge is a great gym or association or shop, well nobody knows... But it's great! We are great! And YOU are great!",
        title: "About ZenLounge",
        buttons: {
            main: {
                label: "Thanks for these information",
                className: "btn-primary",
            }
        }
    });
    return false;
}

window.app.mappers['#register'] = function () {
    $.get('pages/Register.html', null, function (data) {
        $('#main').html(data);
    });
    return false;
}

window.app.mappers['#activities'] = function () {
    $.get('pages/Activities.html', null, function (data) {
        $("li.active").removeClass("active");
        $("#activities").toggleClass("active"),
        $('#main').html(data);
    });
    return false;
}

window.app.mappers['#events'] = function () {
    $.get('pages/Events.html', null, function (data) {
        $("li.active").removeClass("active");
        $("#events").toggleClass("active"),
        $('#main').html(data);
    });
    return false;
}

window.app.mappers['#adminPanel'] = function () {
    $.get('pages/AdminPanel.html', null, function (data) {
        $("li.active").removeClass("active");
        $("#adminPanel").toggleClass("active"),
        $('#main').html(data);
    });
    return false;
}

window.app.ajaxifyForm('#login_form', function (result) {
    window.app.storeLoginParameters(result.userName, result.access_token, $('#cookie_input').is(':checked'));
    window.app.mappers['#'] = function () {
        $.get('pages/MainPage.html', null, function (data) {
            $('#main').html(data);
        });
        return false;
    }
    window.app.mappers['#']();
}, function (result) {
    $('#login_form input[type=submit]').attr('disabled', false);
    bootbox.alert('Bad username or password');
}, 'application/json',
function () {
    $('#login_form input[type=submit]').attr('disabled', true);
});

$(function () {
    window.app.hrefToFunction('body');
    if (window.app.processCookies()) {
        //Vérification de la validité du token
        window.app.sendRestRequest('/Account/IsValidToken', 'GET', null, function () {
            //Good credentials
            $.get('pages/MainPage.html', null, function (data) {
                $('#main').html(data);
            });
            window.app.mappers['#'] = function () {
                $.get('pages/MainPage.html', null, function (data) {
                    $('#main').html(data);
                });
                return false;
            }
        }, function () {
            //Bad credentials
            window.app.clearLoginParameters();
            bootbox.alert('Your authorization has ended. Please log in again.');
        });
    }
});