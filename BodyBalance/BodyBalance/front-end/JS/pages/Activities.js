$(function () {
    var testActivity = {
        ACTIVITY_NAME: "Yoga",
        ACTIVITY_SHORTDESC: "Faite du yoga",
        ACTIVITY_MANAGER: "Bob",
        ACTIVITY_LONGDESC: "Le yoga, c'est trop cool!"
    };

    $.getJSON("ajax/test.json", function (data) {
        var items = [];
        $.each(data, function (key, val) {
            items.push("<tr><td>"+ val + "</td></tr>");
        });
    });
});