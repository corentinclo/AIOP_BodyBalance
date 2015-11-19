$(function () {
    var star = window.app.mappers['*'];
    window.app.onBasket = true;

    window.app.mappers['*'] = function(send) {
        window.app.onBasket = false;
        window.app.mappers['*'] = star;
        if (typeof star == 'undefined') {
            return true;
        }
        return star(send);
    }

    /** BASKET **/
    var i = window.app.basket.length;
    if (i == 0) {
        $('#collapseOne').html('<p class="text-info text-center">Your cart is empty</p>');
    }
    else {
        $.each(window.app.basket, function (_, e) {
            var $tr = $('<tr/>').appendTo($('#basketTable tbody'));
            window.app.sendRestRequest('/Products/' + e.ProductId, 'GET', null, function (data) {
                $tr.html('<td>' + data.Name + '</td><td>' + data.Description + '</td><td>' + e.Quantity + '</td><td><span class="Price" data-qte="' + e.Quantity + '">' + data.Price + '</span>€</td><td></td>');
                $('<button class="btn btn-danger"><i class="fa fa-times-circle"></i></button>').appendTo($tr.find('td').last()).click(function () {
                    window.app.setProductInBasket(e.ProductId, 0);
                });
                var total = 0;
                i--;
                if (i == 0) {
                    $('#basketTable .Price').each(function () {
                        total += parseFloat($(this).text()) * parseFloat($(this).data('qte'));
                    });
                    $('[data-bind="total"]').html(total + '€');
                    $('#basketTable').dataTable();
                }
            }, $.noop)
        });

        $('#confirmBasketBtn').click(function () {
            bootbox.confirm('Are you sure you want to purchase these articles ? If you answer yes, please ship a check to the following adress : "Polytech Montpellier salle de projet 216, Place Eugène Bataillon, 34000 Montpellier". The order will be send after the check reception.', function (result) {
                if (result) {
                    window.app.sendRestRequest('/Users/' + window.app.username + '/Baskets/Validate', 'POST', null, function () {
                        bootbox.alert('Your purchase have been saved', function () {
                            location.reload();
                        });
                    }, function () {
                        bootbox.alert('An error occured, please try again later. It can be because an article is not available anymore.');
                    });
                }
            })
        });
    }
    /** PURCHASES **/
    window.app.sendRestRequest('/Users/' + window.app.username + '/Purchases', 'GET', null, function (data) {
        if (data.length == 0) {
            $('#collapseTwo').html('<p class="text-info text-center">There isn\'t any purchase yet.</p>');
        }
        else {
            var i = data.length;
            $.each(data, function (_, e) {
                $('#purchaseTable tbody').append($('<tr style="cursor: pointer"><td>' + dateFromISO8601(e.PurchaseDate).toLocaleDateString() + '</td><td>' + e.TotalPrice + '€</td></tr>').click(function () {
                    window.app.sendRestRequest('/Purchases/' + e.PurchaseId, "GET", null, function (dt) {
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
                            title: "Purchase details",
                            message: '<div id="purchaseDetails"></div>'
                        });
                        $('#purchaseDetails').append($table).append('<p class="text-info text-right">Total : ' + e.TotalPrice +'€</p>');
                    }, function () {
                        bootbox.alert('An error occured');
                    })
                }));
                i--;
                if (i == 0) {
                    $('#purchaseTable').dataTable();
                }
            });
        }
    }, function () {
        bootbox.alert('An error occured while loading your previous purchases');
    });
});

// function to convert date in ISO8601 format to a date for all browsers
function dateFromISO8601(iso8601Date) {
    var parts = iso8601Date.match(/\d+/g);
    var isoTime = Date.UTC(parts[0], parts[1] - 1, parts[2], parts[3], parts[4], parts[5]);
    var isoDate = new Date(isoTime);
    return isoDate;
}
