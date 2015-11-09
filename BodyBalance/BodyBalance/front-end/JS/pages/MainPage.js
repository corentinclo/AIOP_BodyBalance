window.app.mappers['#logout'] = function () {
    window.app.sendRestRequest('/Account/logout', 'POST', null, function () {
        window.app.clearLoginParameters();
        $('[data-visible="loggedIn"]').hide();
        location.reload();
    });
    return false;
}
$('[data-visible="loggedIn"]').show();
window.app.hrefToFunction('#main');