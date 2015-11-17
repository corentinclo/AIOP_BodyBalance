(function ($) {
    /**
     * Définition de la classe application
     */
    var Application = function () {
        //Tableau de fonctions contenant les triggers à déclencher par hrefToAjax
        this.mappers = [];
        this.token = "";
        this.username = "";
        this.basket = [];
    }



    /**
     * Transforme tous les liens en fonctions dans un containeur
     */
    Application.prototype.hrefToFunction = function (parent) {
        var t = this;
        $(parent + ' a[href]').off('click').click(function () {
            if (typeof t.mappers['*'] != 'undefined') {
                if (!t.mappers['*'](this)) {
                    return false;
                }
            }
            if (typeof t.mappers[$(this).attr('href')] == 'undefined')
                return true;
            return t.mappers[$(this).attr('href')](this);
        });
    }

    Application.prototype.ajaxifyForm = function (form_id, on_success, on_error, contentType, beforesend) {
        $(form_id).submit(function () {
            if (typeof beforesend != 'undefined') {
                var res = beforesend(form_id);
                if (res === false) {
                    return false;
                }
            }
            //var formData = new FormData($(form_id)[0]); //Les données du formulaire
            $.ajax({
                type: $(form_id).attr("method"), //La méthode (GET, POST, PUT, DELETE)
                url: $(form_id).attr("action"), //URL
                data: $(form_id).serialize(), //On met les données
                success: on_success, //Fonction en cas de succès
                error: on_error, //Fonction en cas d'erreur
                cache: false, //Pas de cache
                contentType: (typeof contentType == "undefined") ? false : contentType, //Pas de contenu de retour spécifique
                processData: false
            });
            return false;
        });
    }

    Application.prototype.FormToObject = function (form_id) {
        var res = {};
        $(form_id + ' input, ' + form_id + ' textarea, ' + form_id + ' select').each(function () {
            res[$(this).attr('name')] = $(this).val();
        });
        return res;
    }

    Application.prototype.FillFormWithObject = function (form_id, object) {
        $.each(object, function (i, e) {
            $(form_id + ' [name="' + i + '"]').val(e);
        });
    }

    Application.prototype.ajaxifyFormJson = function (form_id, on_success, on_error, contentType, beforesend, sendToken) {
        var t = this;
        var before = null;
        if (sendToken === true) {
            before = function (request) {
                request.setRequestHeader("Authorization", "Bearer " + t.token);
            };
        }
        $(form_id).submit(function () {
            if (typeof beforesend != 'undefined') {
                var res = beforesend(form_id);
                if (res === false) {
                    return false;
                }
            }
            $.ajax({
                type: $(form_id).attr("method"), //La méthode (GET, POST, PUT, DELETE)
                url: $(form_id).attr("action"), //URL
                data: JSON.stringify(t.FormToObject(form_id)), //On met les données
                success: on_success, //Fonction en cas de succès
                error: on_error, //Fonction en cas d'erreur
                cache: false, //Pas de cache
                contentType: (typeof contentType == "undefined") ? false : contentType, //Pas de contenu de retour spécifique
                processData: false,
                beforeSend: before
            });
            return false;
        });
    }

    Application.prototype.sendRestRequest = function (url, method, data, on_success, on_error, contentType, force_json) {
        if (typeof contentType == 'undefined') {
            contentType = "application/json";
        }
        if (typeof force_json == 'undefined') {
            force_json = false;
        }
        if (typeof on_error == 'undefined') {
            on_error = on_success;
        }
        if (force_json === true) {
            data = JSON.stringify(data);
        }

        var t = this;
        $.ajax({
            url: url,
            method: method,
            data: data,
            success: on_success,
            error: on_error,
            contentType: contentType,
            beforeSend: function (request) {
                request.setRequestHeader("Authorization", "Bearer " + t.token);
            }
        })
    }
    
    Application.prototype.storeLoginParameters = function (userId, token, cookie) {
        this.token = token;
        this.username = userId;
        if (cookie) {
            var expire = new Date();
            expire.setFullYear(expire.getFullYear + 1);
            document.cookie = "uid=" + userId + ";expires=" + expire.toUTCString() + "; path=/";
            document.cookie = "access_token=" + token + ";expires=" + expire.toUTCString() + "; path=/";
        }
        
    }

    Application.prototype.clearLoginParameters = function () {
        this.token = "";
        this.username = "";
        document.cookie = "access_token=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/";
        document.cookie = "uid=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/";
    }

    Application.prototype.processCookies = function () {
        var t = this;
        if (document.cookie.indexOf('access_token') >= 0 && document.cookie.indexOf('uid') >= 0) {
            t.token = $.cookie('access_token');
            t.username = $.cookie('uid');
            return true;
        }
        return false;
    }

    Application.prototype.reloadBasket = function () {
        var t = this;
        t.sendRestRequest("/Users/" + t.username + "/Baskets", "GET", null, function (data) {
            t.basket = data;
            if (data.length == 0) {
                $('.basket .badge').html('');
            }
            else {
                $('.basket .badge').html(data.length);
            }
        })
    }

    Application.prototype.setProductInBasket = function (product_id, quantity) {
        var t = this;
        if (t.basket.length == 0) {
            var data = { UserId: t.username, ProductId: product_id, Quantity: quantity };
            t.sendRestRequest('/Users/' + t.username + '/Baskets', 'POST', data, function () {
                t.reloadBasket();
            }, function () {
                bootbox.alert("An error occured while adding the product to your cart", function () { location.reload(); })
            }, 'application/json', true);
        }
        else {
            var found = false;
            $.each(t.basket, function (_, e) {
                if (e.ProductId == product_id) {
                    if (e.Quantity != quantity && quantity != 0) {
                        e.Quantity = quantity;
                        t.sendRestRequest('/Users/' + t.username + '/Baskets/' + product_id, 'PUT', e, $.noop, function () {
                            bootbox.alert("An error occured while adding the product to your cart", function () { location.reload(); })
                        }, 'application/json', true);
                    }
                    else if (quantity == 0) {
                        t.sendRestRequest('/Users/' + t.username + '/Baskets/' + product_id, 'DELETE', null, function () {
                            t.reloadBasket();
                        }, function () {
                            bootbox.alert("An error occured while adding the product to your cart", function () { location.reload(); })
                        });
                    }
                    found = true;
                    return false; //Sort du foreach
                }
            });
            if (!found) {
                var data = { UserId: t.username, ProductId: product_id, Quantity: quantity };
                t.sendRestRequest('/Users/' + t.username + '/Baskets', 'POST', data, function () {
                    t.reloadBasket();
                }, function () {
                    bootbox.alert("An error occured while adding the product to your cart", function () { location.reload(); })
                }, 'application/json', true);
            }
        }
    }

    Application.prototype.getProductInBasket = function (id) {
        var t = this;
        if (t.basket.length == 0) {
            return undefined;
        }
        var res = undefined;
        $.each(t.basket, function (_, e) {
            if (e.ProductId == id) {
                res = e;
                return false;
            }
        });
        return res;
    }

    window.app = new Application();
})(jQuery);