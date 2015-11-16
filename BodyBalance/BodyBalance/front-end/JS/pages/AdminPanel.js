(function ($) {
    $("#userBtn").click(function () {
        window.app.sendRestRequest('/users', 'GET', null, function (data) {
            $.each(data, function (key, val) {
                var $tr = $(document.createElement("tr")).appendTo(".table tbody");
                $tr.append("<td>" + val.UserId + "</td><td>" + val.FirstName + "</td><td>" + val.LastName + "</td><td><button class='btn btn-primary btn-sm member'>Member</button> <button class='btn btn-primary btn-sm contrib'>Contributor</button> <button class='btn btn-primary btn-sm manager'>Manager</button> <button class='btn btn-primary btn-sm admin'>Admin</button></td>");
                if (val.UserRoles.IsMember)
                    $tr.find(".member").attr("disabled", true);
                if (val.UserRoles.IsContributor)
                    $tr.find(".contrib").attr("disabled", true);
                if (val.UserRoles.IsManager)
                    $tr.find(".manager").attr("disabled", true);
                if (val.UserRoles.IsAdmin)
                    $tr.find(".admin").attr("disabled", true);
            });
        });
        $("#userList").toggle("fast");
    });
})(jQuery);