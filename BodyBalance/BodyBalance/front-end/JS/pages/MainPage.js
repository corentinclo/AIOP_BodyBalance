window.app.mappers['#logout'] = function () {
    window.app.sendRestRequest('/Account/logout', 'POST', null, function () {
        window.app.clearLoginParameters();
        $('[data-visible="loggedIn"]').hide();
        location.reload();
    });
    return false;
}
window.app.mappers['#myaccount'] = function () {
    $.get('pages/MyAccount.html', null, function (result) {
        $('body').append(result);
    });
    return false;
}


$('[data-visible="loggedIn"]').show();
window.app.hrefToFunction('#main');