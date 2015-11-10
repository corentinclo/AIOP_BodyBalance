$(function () {
    var testActivity = {
        ACTIVITY_NAME: "Yoga",
        ACTIVITY_SHORTDESC: "Faite du yoga",
        ACTIVITY_MANAGER: "Bob",
        ACTIVITY_LONGDESC: "Le yoga, c'est trop cool!"
    };

    $('#activitiesTable > tbody:last-child').append('<tr><td>' + testActivity.ACTIVITY_NAME + '</td><td>' + testActivity.ACTIVITY_SHORTDESC + '</td><td>' + testActivity.ACTIVITY_MANAGER + '</td></tr>');

});