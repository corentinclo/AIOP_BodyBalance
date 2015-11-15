$(function () {
    window.app.sendRestRequest('/Users/' + window.app.username, 'GET', null, function (data) {
        //if logged in user is a manager, with get role
        if (data.UserRoles.IsManager) {
            $('[data-visible="manager"]').show();
        }
    });
    var myId = window.app.username;
    var myActivities = new Array();
    var otherActivities = new Array();
    window.app.sendRestRequest('/activities', 'GET', null, function (data) {
        $.each(data, function (key, val) {
            // check if this activity is manage by the logged in user
            if (val.ManagerId == myId) {
                myActivities.push(val);
            } else {
                otherActivities.push(val);
            }
            addOneActivity(val);
         
        });
    });

    $('.myActButton').click(function () {
        $(".grid").empty();
        $("#tbody").empty();
        $(this).attr("disabled", true);
        $(".allActButton").attr("disabled", false);
        $("#activitiesTitle").text("The activities that you manage");
        $.each(myActivities, function (key, val) {
            addOneActivity(val);
        });
    });
    $('.allActButton').click(function () {
        $(".grid").empty();
        $(this).attr("disabled", true);
        $(".myActButton").attr("disabled", false);
        $("#activitiesTitle").text("All the ZenLounge activities!");
        $.each(otherActivities, function (key, val) {
            addOneActivity(val);
        });
    });
    $('#submitCreateButton').click(function () {
        $('#activityId_input').val($('#name_input').val());
        $('#managerId_input').val(myId);
        window.app.ajaxifyFormJson('#create_act_form', function () {
            bootbox.alert('Your activity has been created');
            return false;
        }, function () {
            bootbox.alert('A problem occured');
            return false;
        },
        'application/json', function () {
            $('#create_act_form button').attr('disabled', true);
        }, true);

        $('#create_act_form').submit();
    });

    
});

function showActivityEvents(activityId) {
    window.app.sendRestRequest('/Activities/' + activityId + '/Events', 'GET', null, function (data) {
        if ($(".grid").length) {
            $(".grid").empty();
            var $grid = $(".grid");
        } else {
            $(document.createElement("hr")).appendTo("#main");
            var $grid = $(document.createElement("div")).addClass("grid").appendTo("#main");

        }
        if (data.length == 0) {
            $grid.append("<p>No event for this activity.</p>");
        } else {
            $.each(data, function (key, val) {
                var $item = $(document.createElement("div")).addClass("grid-item " + val.ActivityId + " " + val.Type).appendTo($grid);

                var $panel = $(document.createElement("div")).addClass("panel panel-default").appendTo($item);

                var $panelHeading = $(document.createElement("div")).addClass("panel-heading").html("<h4>" + val.name + "</h4>").appendTo($panel);

                var $panelBody = $(document.createElement("div")).addClass("panel-body").appendTo($panel);
                $panelBody.append("<div class='act'><b>" + val.ActivityId.toUpperCase() + "</b></div>");
                $panelBody.append("<div class='type'><b>" + val.Type.toUpperCase() + "</b></div>");
                $panelBody.append("<p><b>" + val.MaxNb + "</b> places available</p>");
                $panelBody.append("<p>Room <b>n°" + val.RoomId + "</b></p>");
                $panelBody.append("<p>With <b>" + val.ContributorId + "</b></p>");


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
                var time = weekDay + ', ' + month + ' ' + day + ' ' + year + '. ' + hour.substr(hour.length - 2) + ':' + min.substr(min.length - 2);
                $panel.append("<div class='panel-footer'><p> <b>" + time + "</b></p>" + val.Price + "€</div>");

                // Hiden timestamp for isotope sorting
                $item.append("<div class='timestamp'>" + timestamp + "</div>");
            });
            var iso = new Isotope('.grid', {
                itemSelector: '.grid-item',
                layoutMode: 'masonry',
                getSortData: {
                    timestamp: '.timestamp'
                },
                sortBy: ['timestamp']
            });
        }
    });
};

// function to convert date in ISO8601 format to a date for all browsers
function dateFromISO8601(iso8601Date) {
    var parts = iso8601Date.match(/\d+/g);
    var isoTime = Date.UTC(parts[0], parts[1] - 1, parts[2], parts[3], parts[4], parts[5]);
    var isoDate = new Date(isoTime);
    return isoDate;
}

// function to add one activity to the table body
function addOneActivity(val) {
    var $tr = $(document.createElement("tr")).appendTo(".table tbody");
    $tr.append("<td>" + val.Name + "</td><td>" + val.ShortDesc + "</td><td>" + val.ManagerId + "</td>")
    $tr.click(function () {
        if (val.ManagerId == window.app.username) {
            bootboxMyAct();
        } else {
            bootboxOtherAct();
        }
        
    });

    function bootboxMyAct() {
        bootbox.dialog({
            message: val.LongDesc,
            title: val.Name,
            buttons: {
                update: {
                    label: "Update",
                    className: "btn-warning",
                    callback: function () {
                        $('#updateActModal').modal('toggle');
                        //On remplit le formulaire avec le compte utilisateur
                        window.app.sendRestRequest('/Activities/' + val.ActivityId, 'GET', null, function (data) {
                            window.app.FillFormWithObject('#update_act_form', data);
                        },
                        function () {
                            bootbox.alert('An error has occured, we were unable to get this activity informations');
                        });
                        window.app.ajaxifyFormJson('#update_act_form', function () {
                            $('#updateActModal button').attr('disabled', false);
                            bootbox.alert('Your informations have been updated');
                            return false;
                        }, function () {
                            $('#updateActModal button').attr('disabled', false);
                            bootbox.alert('Wrong password');
                            return false;
                        },
                        'application/json', function () {
                            $('#updateActModal button').attr('disabled', true);
                        }, true);
                        $('#update_act_form').attr('action', '/Activities/' + val.ActivityId);
                    }
                },
                del: {
                    label: "Delete",
                    className: "btn-danger",
                    callback: function () {
                        var activityId = val.Name;
                        window.app.sendRestRequest("/Activities/" + activityId, "DELETE", null, function () {
                            bootbox.alert( activityId+" has been deleted");
                        },
                        function () {
                            bootbox.alert("An error has occured and the activity couldn't be deleted.");
                        });
                    }
                },
                success: {
                    label: "This activity events",
                    className: "btn-success",
                    callback: function () {
                        var activityId = val.Name;
                        showActivityEvents(activityId);
                    }
                },
                main: {
                    label: "Back",
                    className: "btn-primary",
                }
            }
        });
    }
    function bootboxOtherAct() {
        bootbox.dialog({
            message: val.LongDesc,
            title: val.Name,
            buttons: {
                success: {
                    label: "This activity events",
                    className: "btn-success",
                    callback: function () {
                        var activityId = val.Name;
                        showActivityEvents(activityId);
                    }
                },
                main: {
                    label: "Back",
                    className: "btn-primary",
                }
            }
        });
    }
}

