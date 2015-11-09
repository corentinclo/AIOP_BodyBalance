(function ($) {
    /**
     * Définition de la classe application
     */
    var Application = function () {
        //Tableau de fonctions contenant les triggers à déclencher par hrefToAjax
        this.mappers = [];
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
            return t.mappers[$(this).attr('href')](this);
        });
    }

    Application.prototype.ajaxifyForm = function (form_id, on_success, on_error, contentType) {
        $(form_id).submit(function () {
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
        $(form_id + ' input, ' + form_id + ' textarea').each(function() {
            res[$(this).attr('name')] = $(this).val();
        });
        return res;
    }

    Application.prototype.ajaxifyFormJson = function (form_id, on_success, on_error, contentType) {
        var t = this;
        $(form_id).submit(function () {
            $.ajax({
                type: $(form_id).attr("method"), //La méthode (GET, POST, PUT, DELETE)
                url: $(form_id).attr("action"), //URL
                data: JSON.stringify(t.FormToObject(form_id)), //On met les données
                success: on_success, //Fonction en cas de succès
                error: on_error, //Fonction en cas d'erreur
                cache: false, //Pas de cache
                contentType: (typeof contentType == "undefined") ? false : contentType, //Pas de contenu de retour spécifique
                processData: false
            });
            return false;
        });
    }

    window.app = new Application();
})(jQuery);