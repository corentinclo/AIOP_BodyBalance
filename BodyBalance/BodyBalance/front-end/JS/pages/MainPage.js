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
window.app.sendRestRequest('/Users/' + window.app.username, 'GET', null, function (data) {
    //if logged in user is an admin
    if (data.UserRoles.IsAdmin)
        $('[data-visible="admin"]').show();
});

window.app.hrefToFunction('#main');