(function ($) {
    /* USERS */
        window.app.sendRestRequest('/purchases', 'GET', null, function (data) {
            $.each(data, function (key, val) {

                // The date is in ISO8601 format
                var date = dateFromISO8601(val.PurchaseDate);
                var timestamp = Date.parse(date);
                var months = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];
                var year = date.getFullYear();
                var month = months[date.getMonth()];
                var day = date.getDate();
                var days = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];
                var weekDay = days[date.getDay()];
                var hour = "0" + (date.getHours() - 1);
                var min = "0" + date.getMinutes();
                var time = weekDay + ', ' + month + ' ' + day + ' ' + year

                var $tr = $("<tr></tr>").appendTo("#userTable tbody");
                $tr.append("<td><button class='btn btn-info infoBtn'><i class='fa fa-info-circle'></i></button></td>"+
                            "<td>" + val.PurchaseId + "</td><td>" + time + "</td><td>" + val.TotalPrice + "€</td><td>" + val.UserId + "</td>");
                
                $("#userInfoModal").modal({ show: false });

                $tr.find(".infoBtn").click(function () {
                    var $modal = $("#userInfoModal");
                    $modal.find(".modal-body").empty();
                    $("#modalLabel").empty();
                    $("#modalLabel").text("Purchase n°" + val.PurchaseId);
                    window.app.sendRestRequest('/purchases/' + val.PurchaseId, 'GET', null, function (data2) {
                        $.each(data2.PurchaseLine, function (key3, val3) {
                            var $tr = $("<tr></tr>").appendTo($('#basketTable tbody'));
                            window.app.sendRestRequest('/Products/' + val3.ProductId, 'GET', null, function (data3) {
                                $.each(data3, function (_, e) {
                                    $tr.html('<td>' + data.Name + '</td><td>' + data.Description + '</td><td>' + e.Quantity + '</td><td><span class="Price" data-qte="' + e.Quantity + '">' + data.Price + '</span>€</td><td></td>');
                                });
                            });
                        });
                    });
                    $modal.modal('show');
                });
            });
            $('#userTable').dataTable({ lengthChange: false });
        }, function () {
            bootbox.alert("An error has occured");
        });
})(jQuery);

function reloadAdminPanelPage() {
    window.app.mappers['#adminPanelPurchase']();
}
// function to convert date in ISO8601 format to a date for all browsers
function dateFromISO8601(iso8601Date) {
    var parts = iso8601Date.match(/\d+/g);
    var isoTime = Date.UTC(parts[0], parts[1] - 1, parts[2], parts[3], parts[4], parts[5]);
    var isoDate = new Date(isoTime);
    return isoDate;
}