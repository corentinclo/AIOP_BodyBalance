var typeList = new Array();
var actList = new Array();
var isManager = undefined;
window.app.sendRestRequest('/Users/' + window.app.username, 'GET', null, function (data) {
    //if logged in user is a manager, with get role
    if (data.UserRoles.IsManager) {
        isManager = true;
        $('[data-visible="manager"]').show();
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
            var $del = null;
            var $edit = null;
            var $users = null;
            if (isManager && val.ManagerId == window.app.username) {
                $edit = $('<div class="pull-left"><button class="btn btn-default"><i class="fa fa-edit"></i></button></div>').prependTo($panelHeading).find('button').tooltip({ title: "Edit", placement: "top", trigger: "hover" });
                $del = $("<div class='pull-right'><button class='btn btn-danger'><i class='fa fa-times'></i></button></div>").prependTo($panelHeading).find('button').tooltip({ title: "Delete", placement: "top", trigger: "hover" });
            }
            if (val.ManagerId == window.app.username || val.ContributorId == window.app.username) {
                $users = $('<div class="pull-right"><button class="btn btn-primary"><i class="fa fa-users"></i></button></div>').appendTo($panelBody).find('button').tooltip({ title: "See the registered users", placement: "top", trigger: "hover" });
            }
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
            var $reg = $("<div class='register'><button class='btn btn-primarystyle'>Register</button></div>").appendTo($panelBody).find('button');
            $reg.click(function () {
                bootbox.confirm({
                    message: "Do you want to register to this event ?",
                    callback: function (result) {
                        if (result) {
                            window.app.sendRestRequest('/Events/' + val.EventId + '/RegisterUser', 'POST', window.app.username, function (data) {
                                bootbox.alert('You are now registered to ' + val.name, function () {
                                    reloadEventsPage();
                                });
                            }, function () {
                                bootbox.alert('An error occured, try again later.');
                            }, 'application/json', true);
                        }
                    }
                });
            });

            window.app.sendRestRequest("/Events/" + val.EventId + "/IsRegisteredUser/" + window.app.username, "GET", null, function (data) {
                if (data === true) {
                    $reg.off('click').html('Registered <i class="fa fa-check-circle"></i>').addClass('disabled');
                    if (!$item.hasClass('minetrue')) {
                        $item.addClass('minetrue');
                    }
                }
            })
           
            if (val.ManagerId == window.app.username || val.ContributorId == window.app.username) {
                //Hidden Boolean for filtering
                if (!$item.hasClass('minetrue')) {
                    $item.addClass('minetrue');
                }
                /* SEE THE REGISTERED USERS */
                $users.click(function () {
                    window.app.sendRestRequest('/Events/' + val.EventId + '/users', 'GET', null, function (data) {
                        var $table = $('<table class="table table-stripped"></table>');
                        var $thead = $('<thead></thead>').appendTo($table);
                        var $tbody = $('<tbody></tbody>').appendTo($table);
                        $thead.html('<tr>' +
                            '<th>Username</th>' +
                            '<th>First Name</th>' +
                            '<th>Last Name</th></tr>'
                        );
                        $.each(data, function (i, e) {
                            var $tr = $('<tr></tr>').appendTo($tbody);
                            $tr.append('<th>' + e.UserId + '</th>');
                            $tr.append('<td>' + e.FirstName + '</td>');
                            $tr.append('<td>' + e.LastName + '</td>');
                        });
                        bootbox.dialog({
                            title: 'Members in "' + val.EventId + '"',
                            message: "<div id='bootboxList'></div>",
                            buttons: {
                                success: {
                                    label: "OK",
                                    className: "btn-primary"
                                }
                            }
                        });
                        if ($tbody.html() == "") {
                            $table = "<p class='text-info'>There is no registered users yet.</p>"
                        }
                        $('#bootboxList').append($table);
                    }, function () {
                        bootbox.alert("An error has occured");
                    }, 'application/json');
                });
            }
            if (isManager && val.ManagerId == window.app.username) {
                /* DELETE AN EVENT */
                $del.click(function () {
                    bootbox.confirm({
                        message: "Are you sure ?",
                        callback: function (result) {
                            if (result) {
                                window.app.sendRestRequest('/Events/' + val.EventId, 'DELETE', null, function (data) {
                                    bootbox.alert('This event doesn\'t exist anymore...');
                                    reloadEventsPage();
                                }, function () {
                                    bootbox.alert('An error occured. Try again later.');
                                });
                            }
                        }
                    });
                });
                /* EDIT AN EVENT */
                $edit.click(function () {
                    var url = $('#edit_event_form').attr('action');
                    $('#edit_event_form').attr('action', url + '/' + val.EventId);
                    var tempObj = val;
                    tempObj.EventDate = dateFromISO8601(val.EventDate).toISOString().substring(0,10);
                    window.app.FillFormWithObject('#edit_event_form', tempObj);
                    $('#editEventModal').modal('show');
                    window.app.ajaxifyFormJson('#edit_event_form', function () {
                        $('#editEventModal').on('hide.bs.modal', function () {
                            bootbox.alert('The event has been updated', function () {
                                reloadEventsPage();
                            });
                        }).modal('hide');
                    }, function () {
                        bootbox.alert('An error occured');
                    },
                    'application/json', undefined, true);
                })
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
        var $filters = $('#selects select, #myOnly');

        $filters.change(function () {
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
            if ($("#myOnly").is(':checked')) {
                filters.push('.minetrue');
                filterValue += ".minetrue";
            }
            iso.arrange({ filter: filterValue });
        });

        $("#selActivity").html('');
        $("#edit_selActivity").html('');
        $("#selTyp").html('');
        $("#edit_selType").html('');
        $("#selRoom").html('');
        $("#edit_selRoom").html('');
        $("#selContrib").html('');
        $("#edit_selContrib").html('');
        $.each(actList, function (key, val) {
            $("#selActivity").append("<option value=" + val + "> " + val + "</a>");
            $("#edit_selActivity").append("<option value=" + val + "> " + val + "</a>");
        });

        $.each(typeList, function (key, val) {
            $("#selTyp").append("<option value=" + val + "> " + val + "</a>");
            $("#edit_selTyp").append("<option value=" + val + "> " + val + "</a>");
        });

        window.app.sendRestRequest('/Rooms', 'GET', null, function (data) {
            $.each(data, function (key, val) {
                $("#selRoom").append("<option value=" + val.RoomId + "> N°" + val.RoomId + " - " + val.Name + "</a>");
                $("#edit_selRoom").append("<option value=" + val.RoomId + "> N°" + val.RoomId + " - " + val.Name + "</a>");
            });
        });

        window.app.sendRestRequest('/Contributors', 'GET', null, function (data) {
            $.each(data, function (key, val) {
                $("#selContrib").append("<option value=" + val.UserId + ">" + val.UserId + "</a>");
                $("#edit_selContrib").append("<option value=" + val.UserId + ">" + val.UserId + "</a>");
            });
        });
    });


    /* CREATE AN EVENT */
    $('#submitCreateButton').click(function () {
        $('#eventId_input').val($('#name_input').val().replace(" ",""));
        $('#managerId_input').val(window.app.username);
        $('#duration_input').val(1);
        window.app.ajaxifyFormJson('#create_event_form', function () {
            bootbox.alert('Your event has been created', function () {
                $('#createEventModal').on('hidden.bs.modal', function () {
                    reloadEventsPage();
                }).modal('hide');
            });
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

function reloadEventsPage() {
    window.app.mappers['#events']();
}
