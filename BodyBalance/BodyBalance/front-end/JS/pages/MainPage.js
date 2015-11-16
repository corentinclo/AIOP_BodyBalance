﻿window.app.mappers['#logout'] = function () {
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

window.app.sendRestRequest("/Users/" + window.app.username + "/Events", "GET", null, function (data) {
    var events = Array();
    $.each(data, function (i, element) {
        var temp = {};
        temp.id = element.EventId;
        temp.title = element.name;
        temp.start = dateFromISO8601(element.EventDate);
        temp.allDay = true;
        temp.editable = false;
        events.push(temp);
    });
    $('#fullcalendar').html('').fullCalendar({
        header: {
            left: 'prev,next today',
            center: 'title',
            right: 'month'
        },
        buttonText: {
            today: "Today"
        },
        defaultView: 'month',
        lang: 'en',
        editable: false,
        droppable: false,
        eventOverlap: true,
        allDaySlot: true,
        events: events
    })

}, function () {
    bootbox.alert("An error occurred while loading your events.");
});

// function to convert date in ISO8601 format to a date for all browsers
function dateFromISO8601(iso8601Date) {
    var parts = iso8601Date.match(/\d+/g);
    var isoTime = Date.UTC(parts[0], parts[1] - 1, parts[2], parts[3], parts[4], parts[5]);
    var isoDate = new Date(isoTime);
    return isoDate;
}