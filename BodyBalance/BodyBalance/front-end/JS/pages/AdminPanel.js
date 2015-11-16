(function ($) {
    var usersLoaded = false;
    var roomsLoaded = false;
    /* USERS */
    $("#userBtn").click(function () {
        if (!usersLoaded){
            window.app.sendRestRequest('/users', 'GET', null, function (data) {
                $.each(data, function (key, val) {
                    var $tr = $("<tr></tr>").appendTo("#userTable tbody");
                    $tr.append("<td>" + val.UserId + "</td><td>" + val.FirstName + "</td><td>" + val.LastName + "</td><td><button class='btn btn-primary btn-sm member'>Member</button> <button class='btn btn-primary btn-sm contrib'>Contributor</button> <button class='btn btn-primary btn-sm manager'>Manager</button> <button class='btn btn-primary btn-sm admin'>Admin</button></td><td><button class='btn btn-danger delBtn'><i class='fa fa-trash-o'></i></button></td>");
                    if (val.UserRoles.IsMember)
                        $tr.find(".member").attr("disabled", true);
                    if (val.UserRoles.IsContributor)
                        $tr.find(".contrib").attr("disabled", true);
                    if (val.UserRoles.IsManager)
                        $tr.find(".manager").attr("disabled", true);
                    if (val.UserRoles.IsAdmin)
                        $tr.find(".admin").attr("disabled", true);

                    $("#userInfoModal").modal({ show: false });
                    $tr.click(function () {
                        var $modal = $("#userInfoModal");
                        $modal.find(".modal-body").empty();
                        $("#modalLabel").empty();
                        $("#modalLabel").text(val.UserId);
                        $modal.find(".modal-body").append("<h4></h4>")
                        $modal.find(".modal-body h4").append(val.FirstName + " " + val.LastName + "</br>" + val.Mail + "</br>" + val.Adress1 );
                        if (val.Adress2 != null)
                            $modal.find(".modal-body h4").append("</br>"+val.Adress2 );
                        $modal.find(".modal-body h4").append("</br>" + val.PC + " " + val.Town + "</br>" + val.Phone );
                        $modal.find(".modal-body h4").css('text-align','center');
                        $modal.modal('show');
                    });

                    $tr.find(".delBtn").click(function () {
                        bootbox.confirm({
                            message: "Do you want to delete this user ?",
                            callback: function (result) {
                                if (result) {
                                    window.app.sendRestRequest('/Users/' + val.UserId, 'DELETE', null, function (data) {
                                        bootbox.alert('User deleted', function () {
                                            reloadAdminPanelPage();
                                        });
                                    }, function () {
                                        bootbox.alert('An error occured, try again later.');
                                    });
                                }
                            }
                        });
                    });

                    $tr.find(".member").click(function () {
                        bootbox.confirm({
                            message: "Do you want to upgrade this user ?",
                            callback: function (result) {
                                if (result) {
                                    var data = { MemberId: val.UserId };
                                    window.app.sendRestRequest('/members', 'POST', data, function (data) {
                                        bootbox.alert("This user is now a member");
                                        reloadAdminPanelPage();
                                    },function(){
                                        bootbox.alert("An error has occured");
                                    });
                                }
                            }
                        });
                                
                    });
                    $tr.find(".contrib").click(function () {
                        bootbox.confirm({
                            message: "Do you want to upgrade this user ?",
                            callback: function (result) {
                                if (result) {
                                    window.app.sendRestRequest('/contributors', 'POST', null, function (data) {
                                        bootbox.alert("This user is now a contributor");
                                        reloadAdminPanelPage();
                                    }, function () {
                                        bootbox.alert("An error has occured");
                                    });
                                }
                            }
                        });
                    });
                    $tr.find(".manager").click(function () {
                        bootbox.confirm({
                            message: "Do you want to upgrade this user ?",
                            callback: function (result) {
                                if (result) {
                                    window.app.sendRestRequest('/managers', 'POST', null, function (data) {
                                        bootbox.alert("This user is now a manager");
                                        reloadAdminPanelPage();
                                    }, function () {
                                        bootbox.alert("An error has occured");
                                    });
                                }
                            }
                        });
                    });
                    $tr.find(".admin").click(function () {
                        bootbox.confirm({
                            message: "Do you want to upgrade this user ?",
                            callback: function (result) {
                                if (result) {
                                    window.app.sendRestRequest('/admins', 'POST', null, function (data) {
                                        bootbox.alert("This user is now an admin");
                                        reloadAdminPanelPage();
                                    }, function () {
                                        bootbox.alert("An error has occured");
                                    });
                                }
                            }
                        });
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
                    $tr.append("<td>" + val.RoomId + "</td><td>" + val.Name + "</td><td>" + val.MaxNb + "</td><td>" + val.Superficy + "</td><td><button class='btn btn-danger delBtnRoom'><i class='fa fa-trash-o'></i></button></td>");
                    $tr.find(".delBtnRoom").click(function () {
                        bootbox.confirm({
                            message: "Do you want to delete this room ?",
                            callback: function (result) {
                                if (result) {
                                    window.app.sendRestRequest('/Rooms/' + val.RoomId, 'DELETE', null, function (data) {
                                        bootbox.alert('Room deleted', function () {
                                            reloadAdminPanelPage();
                                        });
                                    }, function () {
                                        bootbox.alert('An error occured, try again later.');
                                    });
                                }
                            }
                        });
                    });
                });
            });
            roomsLoaded = true;
        }

        $("#roomList").toggle("fast");
    });
})(jQuery);

function reloadAdminPanelPage() {
    window.app.mappers['#adminPanel']();
}