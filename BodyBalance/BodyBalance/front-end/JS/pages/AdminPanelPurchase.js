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
                    window.app.sendRestRequest('/Purchases/' + val.PurchaseId, "GET", null, function (dt) {
                        var lines = dt.PurchaseLine;
                        var $table = $('<table class="table table-striped"><thead><tr><th>Product</th><th>Description</th><th>Price</th><th>Quantity</th><th>Total</th></tr></thead></table>');
                        var $tbody = $('<tbody></tbody>').appendTo($table);
                        $.each(lines, function (__, line) {
                            window.app.sendRestRequest('/Products/' + line.ProductId, "GET", null, function (product) {
                                $tbody.append(
                                    $('<tr>' +
                                        '<td>' +
                                            product.Name +
                                        '</td>' +
                                        '<td>' +
                                            product.Description +
                                        '</td>' +
                                        '<td>' +
                                            product.Price +
                                        '€</td>' +
                                        '<td>' +
                                            line.Quantity +
                                        '</td>' +
                                        '<td><b>' +
                                            product.Price * line.Quantity +
                                        '€</b></td>' +
                                    '</tr>')
                                );
                            }, $.noop);
                        });
                        bootbox.dialog({
                            title: "Purchase details (purchase from " + val.UserId + ")",
                            message: '<div id="purchaseDetails"></div>'
                        });
                        $('#purchaseDetails').append($table).append('<p class="text-info text-right">Total : ' + val.TotalPrice + '€</p>');
                    }, function () {
                        bootbox.alert('An error occured');
                    });
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