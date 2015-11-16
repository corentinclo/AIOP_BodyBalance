(function ($) {
    var usersLoaded = false;
    var roomsLoaded = false;
    /* USERS */
    $("#userBtn").click(function () {
        if (!usersLoaded){
            window.app.sendRestRequest('/users', 'GET', null, function (data) {
                $.each(data, function (key, val) {
                    var $tr = $(document.createElement("tr")).appendTo("#userTable tbody");
                    $tr.append("<td>" + val.UserId + "</td><td>" + val.FirstName + "</td><td>" + val.LastName + "</td><td><button class='btn btn-primary btn-sm member'>Member</button> <button class='btn btn-primary btn-sm contrib'>Contributor</button> <button class='btn btn-primary btn-sm manager'>Manager</button> <button class='btn btn-primary btn-sm admin'>Admin</button></td><td><i class='fa fa-times'></i></td>");
                    if (val.UserRoles.IsMember)
                        $tr.find(".member").attr("disabled", true);
                    if (val.UserRoles.IsContributor)
                        $tr.find(".contrib").attr("disabled", true);
                    if (val.UserRoles.IsManager)
                        $tr.find(".manager").attr("disabled", true);
                    if (val.UserRoles.IsAdmin)
                        $tr.find(".admin").attr("disabled", true);

                    $tr.find(".member").click(function () {
                    
                    });
                });
            });
            usersLoaded = true;
        }
        $("#userList").toggle("fast");
    });

    /* ROOMS */
    $("#roomBtn").click(function () {
        if (!roomsLoaded) {
            window.app.sendRestRequest('/rooms', 'GET', null, function (data) {
                $.each(data, function (key, val) {
                    var $tr = $(document.createElement("tr")).appendTo("#roomTable tbody");
                    $tr.append("<td>" + val.RoomId + "</td><td>" + val.Name + "</td><td>" + val.MaxNb + "</td><td>" + val.Superficy + "</td><td><i class='fa fa-times'></i></td>");
                });
            });
            roomsLoaded = true;
        }
        $("#roomList").toggle("fast");
    });
})(jQuery);