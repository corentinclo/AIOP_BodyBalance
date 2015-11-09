window.app.mappers['#logout'] = function () {
    window.app.sendRestRequest('/Account/logout', 'POST', null, function () {
        document.cookie = "access_token=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/";
        location.reload();
    });
    return false;
}
window.app.hrefToFunction('#main');