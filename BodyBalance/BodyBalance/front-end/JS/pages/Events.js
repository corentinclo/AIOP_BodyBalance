var typeList = new Array();
var actList = new Array();
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

        var $panelHeading = $(document.createElement("div")).addClass("panel-heading").html("<h4 class='text-muted'>" + val.name + "</h4>").appendTo($panel);

        var $panelBody = $(document.createElement("div")).addClass("panel-body").appendTo($panel);
        $panelBody.append("<div class='act text-muted'>" + val.ActivityId + "</div>");
        $panelBody.append("<div class='type text-muted'>" + val.Type + "</div>");
        $panelBody.append("<p>" + val.MaxNb + " places available</p>");
        $panelBody.append("<p>Room n°" + val.RoomId + "</p>");
        $panelBody.append("<p>With " + val.ContributorId + "</p>");


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
        $panel.append("<div class='panel-footer text-muted'><p> <b>" + time + "</b></p>" + val.Price + "€</div>");

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
    

// function to convert date in ISO8601 format to a date for all browsers
function dateFromISO8601(iso8601Date) {
    var parts = iso8601Date.match(/\d+/g);
    var isoTime = Date.UTC(parts[0], parts[1] - 1, parts[2], parts[3], parts[4], parts[5]);
    var isoDate = new Date(isoTime);
    return isoDate;
}

// flatten object by concatting values
function concatValues(obj) {
    var value = '';
    for (var prop in obj) {
        value += obj[prop];
    }
    return value;
}