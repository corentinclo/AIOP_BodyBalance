var categories = {};
$('input[name="UserId"]').each(function () {
    $(this).val(window.app.username);
});
window.app.sendRestRequest('/Categories', 'GET', null, function (cat) {
    $.each(cat, function (i, e) {
        categories[e.CategoryId] = e;
        $('select[name="CategoryId"]').each(function () {
            $(this).append('<option value="' + e.CategoryId + '">' + e.Name + '</option>');
        });
    });
    window.app.sendRestRequest('/Products', 'GET', null, function (data) {
        var $tbody = $('#ProductsTable tbody');
        $.each(data, function (i, e) {
            var inBasket = window.app.getProductInBasket(e.ProductId);
            inBasket = (typeof inBasket == 'undefined' ? 0 : inBasket.Quantity);
            if (e.AvailableQuantity == 0) {
                return true;
            }
            var $tr = $('<tr data-pid="' + e.ProductId + '"/>').appendTo($tbody);
            $tr.append('<td>' + e.Name + '</td><td>' + e.Description + '</td><td class="available-qty">' + e.AvailableQuantity + '</td><td>' + categories[e.CategoryId].Name + '</td><td>' + e.Price + '€</td><td>' + e.MemberReduction + '€</td>');
            var $td = $('<td class="counter"/>').appendTo($tr);
            var $minus = $('<a href="#addProduct" style="color:#777; font-size:12px;"><i class="fa fa-minus"></i></a>').click(function () { addProductToCart(e.ProductId, 'minus'); return false; });
            var $plus = $('<a href="#addProduct" style="color:#777; font-size:12px;"><i class="fa fa-plus"></i></a>').click(function () { addProductToCart(e.ProductId, 'plus'); return false; });
            $td.append($minus).append('&nbsp;<span>' + inBasket + '</span>&nbsp;').append($plus);
            if (inBasket == 0) {
                $minus.addClass('disabled');
            }
            if (inBasket == e.AvailableQuantity) {
                $plus.addClass('disabled');
            }
            var $td2 = $('<td/>').appendTo($tr);
            if (e.UserId == window.app.username) {
                var $edit = $('<button class="btn btn-primary btn-xs" style="margin-right:5px"><i class="fa fa-edit fa-fw"></i></button>').appendTo($td2).click(function () {
                    window.app.FillFormWithObject('#editProductForm', e);
                    $('#editProductForm').attr('action', '/Products/' + e.ProductId);
                    $('#editProductModal').modal('show');
                });
                var $delete = $('<button class="btn btn-danger btn-xs"><i class="fa fa-trash fa-fw"></i></button>').appendTo($td2).click(function () {
                    bootbox.confirm('Are you sure you want to delete this product ?', function (result) {
                        if (result) {
                            window.app.sendRestRequest('/Products/' + e.ProductId, 'DELETE', null, function () {
                                reloadProductsPage();
                            }, function () {
                                bootbox.alert('An error occured while deleting this product.');
                            });
                        }
                    })
                });
            }
        });
        $('#ProductsTable').dataTable();
    }, function () {
        bootbox.alert('An error occured. Please try again later');
    },
    'application/json');
}, function () {
    bootbox.alert('An error occured. Please try again later');
});
function addProductToCart(id, fct) {
    var was = parseInt($('tr[data-pid="' + id + '"] .counter span').text());
    var available = parseInt($('tr[data-pid="' + id + '"] .available-qty').text()) - was;
    if (fct == 'plus') {
        if (available == 0) {
            $('tr[data-pid="' + id + '"] .counter a').last().addClass('disabled');
            return false;
        }
        else if (available == 1) {
            $('tr[data-pid="' + id + '"] .counter span').text(was + 1);
            $('tr[data-pid="' + id + '"] .counter a').last().addClass('disabled');
        }
        else {
            $('tr[data-pid="' + id + '"] .counter span').text(was + 1);
        }
    }
    else if (fct == 'minus') {
        if (was == 0) {
            $('tr[data-pid="' + id + '"] .counter a').first().addClass('disabled');
            return false;
        }
        else if (was == 1) {
            $('tr[data-pid="' + id + '"] .counter span').text(0);
            $('tr[data-pid="' + id + '"] .counter a').first().addClass('disabled');
        }
        else {
            $('tr[data-pid="' + id + '"] .counter span').text(was - 1);
        }
    }
    window.app.setProductInBasket(id, parseInt($('tr[data-pid="' + id + '"] .counter span').text()));
}

window.app.mappers['#sellProduct'] = function () {
    $('#newProductModal').modal('show');
    return false;
}

function reloadProductsPage() {
    window.app.mappers['#products']();
}

window.app.hrefToFunction('#main');

window.app.ajaxifyFormJson('#newProductForm', function () {
    $('#newProductModal').on('hidden.bs.modal', reloadProductsPage).modal('hide');
}, function () {
    bootbox.alert('An error occured. Please verify the fields and try again');
}, 'application/json', $.noop, true);

window.app.ajaxifyFormJson('#editProductForm', function () {
    $('#editProductModal').on('hidden.bs.modal', reloadProductsPage).modal('hide');
}, function () {
    bootbox.alert('An error occured. Please verify the fields and try again');
}, 'application/json', $.noop, true)