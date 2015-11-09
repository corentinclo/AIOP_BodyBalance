window.app.mappers['#logout'] = function () {
    window.app.sendRestRequest('/Account/logout', 'POST', null, function () {
        window.app.clearLoginParameters();
        location.reload();
    });
    return false;
}
window.app.hrefToFunction('#main');