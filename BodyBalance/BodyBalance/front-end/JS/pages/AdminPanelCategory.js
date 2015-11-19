(function ($) {
    window.app.sendRestRequest('/categories', 'GET', null, function (data) {
        $.each(data, function (key, val) {
            var $tr = $(document.createElement("tr")).appendTo("#catTable tbody");
            $tr.append("<td>" + val.Name + "</td><td>" + val.Description + "<td><button class='btn btn-danger delBtnCat'><i class='fa fa-trash-o'></i></button></td><td><button class='btn btn-primary upBtnCat'><i class='fa fa-edit'></i></button></td>");
            // DELETE
            $tr.find(".delBtnCat").click(function () {
                bootbox.confirm({
                    message: "Do you want to delete this category ?",
                    callback: function (result) {
                        if (result) {
                            window.app.sendRestRequest('/categories/' + val.CategoryId, 'DELETE', null, function (data) {
                                bootbox.alert('Category deleted', function () {
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
            $tr.find(".upBtnCat").click(function () {
                var url = $('#edit_cat_form').attr('action');
                $('#edit_cat_form').attr('action', url + '/' + val.CategoryId);
                var tempObj = val;
                window.app.FillFormWithObject('#edit_cat_form', tempObj);
                $('#editCatModal').modal('show');
                window.app.ajaxifyFormJson('#edit_cat_form', function () {
                    $('#editCatModal').on('hide.bs.modal', function () {
                        bootbox.alert('The category has been updated', function () {
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
        $('#catId_input').val($('#name_input').val());
        var d = new Date();
        var month = d.getMonth() + 1;
        var day = d.getDate();
        var date = d.getFullYear() + '-' +
                    (('' + month).length < 2 ? '0' : '') + month + '-' +
                    (('' + day).length < 2 ? '0' : '') + day;
        $('#valdate_input').val(date);
        $('#parentid_input').val(null);
        window.app.ajaxifyFormJson('#create_cat_form', function () {
            bootbox.alert('Your category has been created', function () {
                $('#createCatModal').on('hidden.bs.modal', function () {
                    reloadAdminPanelPage();
                }).modal('hide');
            });
            return false;
        }, function () {
            bootbox.alert('A problem occured');
            return false;
        },
        'application/json', function () {
            $('#create_cat_form button').attr('disabled', true);
        }, true);

        $('#create_cat_form').submit();
    });
})(jQuery);

function reloadAdminPanelPage() {
    window.app.mappers['#adminPanelCategory']();
}