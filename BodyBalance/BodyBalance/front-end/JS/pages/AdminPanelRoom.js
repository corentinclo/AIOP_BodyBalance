(function ($) {
        window.app.sendRestRequest('/rooms', 'GET', null, function (data) {
            $.each(data, function (key, val) {
                var $tr = $(document.createElement("tr")).appendTo("#roomTable tbody");
                $tr.append("<td>" + val.RoomId + "</td><td>" + val.Name + "</td><td>" + val.MaxNb + "</td><td>" + val.Superficy + "</td><td><button class='btn btn-danger delBtnRoom'><i class='fa fa-trash-o'></i></button></td><td><button class='btn btn-primary upBtnRoom'><i class='fa fa-edit'></i></button></td>");
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
                // UPDATE
                $tr.find(".upBtnRoom").click(function () {
                    var url = $('#update_room_form').attr('action');
                    $('#update_room_form').attr('action', url + '/' + val.RoomId);
                    var tempObj = val;
                    window.app.FillFormWithObject('#update_room_form', tempObj);
                    $('#updateRoomModal').modal('show');
                    window.app.ajaxifyFormJson('#update_room_form', function () {
                        $('#updateRoomModal').on('hide.bs.modal', function () {
                            bootbox.alert('The room has been updated', function () {
                                reloadAdminPanelPage();
                            });
                        }).modal('hide');
                    }, function () {
                        bootbox.alert('An error occured');
                    },
                    'application/json', undefined, true);
                });

            });
        });

    
    // CREATE
        $('#submitCreateButton').click(function () {
            $('#submitCreateButton').prop("disable",true);
            window.app.ajaxifyFormJson('#create_room_form', function () {
                bootbox.alert('Your room has been created', function () {
                    $('#createRoomModal').on('hidden.bs.modal', function () {
                        reloadAdminPanelPage();
                    }).modal('hide');
                });
                return false;
            }, function () {
                bootbox.alert('A problem occured');
                return false;
            },
            'application/json', function () {
                $('#create_room_form button').attr('disabled', true);
            }, true);

            $('#create_room_form').submit();
        });
})(jQuery);

function reloadAdminPanelPage() {
    window.app.mappers['#adminPanelRoom']();
}