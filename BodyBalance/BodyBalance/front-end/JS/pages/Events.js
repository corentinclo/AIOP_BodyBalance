
var typeList = new Array();
var actList = new Array();
var isManager = undefined;
window.app.sendRestRequest('/Users/' + window.app.username, 'GET', null, function (data) {
    //if logged in user is a manager, with get role
    if (data.UserRoles.IsManager) {
        isManager = true;
    }
    else {
        isManager = false
    }

    window.app.sendRestRequest('/events', 'GET', null, function (data) {
        $.each(data, function (key, val) {
            if (jQuery.inArray(val.Type, typeList) == -1) {
                $("#selType").append("<option value='." + val.Type + "'> " + val.Type + "</a>");
                typeList.push(val.Type);

            }
            if (jQuery.inArray(val.ActivityId, actList) == -1) {
                $("#selAct").append("<option value='." + val.ActivityId + "'> " + val.ActivityId + "</a>");
                actList.push(val.ActivityId);
            }

            var $item = $(document.createElement("div")).addClass("grid-item " + val.ActivityId + " " + val.Type).appendTo(".grid");

            var $panel = $(document.createElement("div")).addClass("panel panel-default").appendTo($item);

            var $panelHeading = $(document.createElement("div")).addClass("panel-heading").html("<h4>" + val.name + "</h4>").appendTo($panel);

            var $panelBody = $(document.createElement("div")).addClass("panel-body").appendTo($panel);
            $panelBody.append("<div class='act'><b>" + val.ActivityId.toUpperCase() + "</b></div>");
            $panelBody.append("<div class='type'><b>" + val.Type.toUpperCase() + "</b></div>");
            $panelBody.append("<p><b>" + val.MaxNb + "</b> places available</p>");
            $panelBody.append("<p>Room <b>n°" + val.RoomId + "</b></p>");
            $panelBody.append("<p>With <b>" + val.ContributorId + "</b></p>");
            var $reg = $("<div class='register'><button class='btn btn-primarystyle'>Register</button></div>").appendTo($panelBody).find('button');
            var $del = null;
            if (isManager && val.ManagerId == window.app.username)
                $del = $("<div class='delete'><button class='btn btn-primarystyle'>Delete</button></div>").appendTo($panelBody).find('button');

            // The date is in ISO8601 format
            var date = dateFromISO8601(val.EventDate);
            var timestamp = Date.parse(date);
            var months = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];
            var year = date.getFullYear();
            var month = months[date.getMonth()];
            var day = date.getDate();
            var days = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];
            var weekDay = days[date.getDay()];
            var hour = "0" + (date.getHours() - 1);
            var min = "0" + date.getMinutes();
            var time = weekDay + ', ' + month + ' ' + day + ' ' + year //+ '. ' + hour.substr(hour.length - 2) + ':' + min.substr(min.length - 2);
            $panel.append("<div class='panel-footer'><p> <b>" + time + "</b></p>" + val.Price + "€</div>");

            // Hiden timestamp for isotope sorting
            $item.append("<div class='timestamp'>" + timestamp + "</div>");

            /* REGISTER TO AN EVENT */
            $reg.click(function () {
                bootbox.confirm({
                    message: "Do you want to register to this event ?",
                    callback: function (result) {
                        if (result) {
                            window.app.sendRestRequest('/Events/' + val.EventId + '/RegisterUser', 'POST', null, function (data) {
                                bootbox.alert('You are now register to ' + val.name);
                            }, function () {
                                bootbox.alert('An error occured, try again later.');
                            });
                        }
                    }
                });
            });
            if (isManager) {
                /* DELETE AN EVENT */
                $del.click(function () {
                    bootbox.confirm({
                        message: "Are you sure ?",
                        callback: function (result) {
                            if (result) {
                                window.app.sendRestRequest('/Events/' + val.EventId, 'DELETE', null, function (data) {
                                    bootbox.alert('This event doesn\'t exist anymore...');
                                }, function () {
                                    bootbox.alert('An error occured. Try again later.');
                                });
                            }
                        }
                    });
                });
            }
        });

        var iso = new Isotope('.grid', {
            itemSelector: '.grid-item',
            layoutMode: 'masonry',
            getSortData: {
                timestamp: '.timestamp'
            },
            sortBy: ['timestamp']
        });

        // filter with selects and checkboxes
        var $selects = $('#selects select');

        $selects.change(function () {
            // map input values to an array
            var filters = [];
            var filterValue = '';
            // filters from selects
            $selects.each(function (i, elem) {
                if (elem.value) {
                    if (elem.value != ".*") {
                        filters.push(elem.value);
                        filterValue += elem.value;
                    }
                }
            });
            iso.arrange({ filter: filterValue });
        });
    });


    /* CREATE AN EVENT */
    $('#createEventModal').on('shown.bs.modal', function (e) {
        $.each(actList, function (key, val) {
            $("#selActivity").append("<option value=" + val + "> " + val + "</a>");
        });
        $.each(typeList, function (key, val) {
            $("#selTyp").append("<option value=" + val + "> " + val + "</a>");
        });
        window.app.sendRestRequest('/Rooms', 'GET', null, function (data) {
            $.each(data, function (key, val) {
                $("#selRoom").append("<option value=" + val.RoomId + "> N°" + val.RoomId + " - " + val.Name + "</a>");
            });
        });
        /* TEMPORARY
        window.app.sendRestRequest('/Contributors', 'GET', null, function (data) {
            $.each(data, function (key, val) {
                $("#selContrib").append("<option value=" + val.ContributorId + ">" + val.ContributorId + "</a>");
            });
        });*/
    });
    $('#submitCreateButton').click(function () {
        $('#eventId_input').val($('#name_input').val());
        $('#managerId_input').val(window.app.username);
        $('#duration_input').val(1);
        window.app.ajaxifyFormJson('#create_event_form', function () {
            bootbox.alert('Your event has been created');
            return false;
        }, function () {
            bootbox.alert('A problem occured');
            return false;
        },
        'application/json', function () {
            $('#create_event_form button').attr('disabled', true);
        }, true);

        $('#create_event_form').submit();
    });

});
// function to convert date in ISO8601 format to a date for all browsers
function dateFromISO8601(iso8601Date) {
    var parts = iso8601Date.match(/\d+/g);
    var isoTime = Date.UTC(parts[0], parts[1] - 1, parts[2], parts[3], parts[4], parts[5]);
    var isoDate = new Date(isoTime);
    return isoDate;
}
