$(function () {
    window.app.sendRestRequest('/activities', 'GET', null, function (data) {
        $.each(data, function (key, val) {
            var $tr = $(document.createElement("tr")).appendTo(".table tbody");
            $tr.append("<td>" + val.Name + "</td><td>" + val.ShortDesc + "</td><td>" + val.ManagerId + "</td>")
            $tr.click(function () {
                bootbox.dialog({
                    message: val.LongDesc,
                    title: val.Name,
                    buttons: {
                        success: {
                            label: "This activity events",
                            className: "btn-success",
                            callback: function () {
                                bootbox.alert("Soon...");
                            }
                        },
                        main: {
                            label: "Back",
                            className: "btn-primary",
                        }
                    }
                });
            });
        });
    });

});