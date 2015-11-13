$(function () {
    //On remplit le formulaire avec le compte utilisateur
    window.app.sendRestRequest('/Users/' + window.app.username, 'GET', null, function (data) {
        window.app.FillFormWithObject('#my_account_form', data);
    },
    function () {
        bootbox.alert('An error has occured, we were unable to get your informations');
    });

    $('#change_pass_usrId_input').val(window.app.username);

    var changePass = false;
    //Lors du click sur changement de mot de passe
    window.app.mappers['#changePassword'] = function () {
        changePass = true;
        $('#myAccountModal').modal('hide');
        return false;
    }
    //Quand la modal de changement de mot de passe se coupe
    $('#changePassModal').on('hidden.bs.modal', function () {
        //On remet la fenêtre Mon Compte
        $('#myAccountModal').modal('show');
    });
    //Quand la modal de mon compte se coupe
    $('#myAccountModal').modal('show').on('hidden.bs.modal', function () {
        if (changePass) {
            //Si on veut changer de password, on passe à la fenêtre de changement du mot de passe
            changePass = false;
            $('#changePassModal').modal('show');
        }
        else {
            //Sinon, on vire le lien dans mappers et on supprime les modal
            window.app.mappers['#changePassword'] = null;
            $('#myAccountModal').remove();
            $('#changePassModal').remove();
        }
    });
    window.app.hrefToFunction('#myAccountModal');
    window.app.ajaxifyFormJson('#change_password_form', function () {
        bootbox.alert('Your password has been changed');
        $('#changePassModal').modal('hide');
        return false;
    }, function () {
        bootbox.alert('An error has occured');
        return false;
    },
    'application/json',
    function () {
        //Validation
        if ($('#change_password_input').val() != $('#c_password_input').val()) {
            $('#c_password_input').tooltip({ title: "Le mot de passe ne correspond pas", placement: "bottom", trigger: "manual" }).tooltip("show");
            $('#c_password_input').parent().parent().addClass('has-error');
            $('html, body').animate({
                scrollTop: $("#c_password_input").offset().top - 100
            }, 500);
            setTimeout(function () {
                $('#c_password_input').tooltip('hide').tooltip('destroy');
            }, 5000);
            return false;
        }
        return true;
    }, true);
    window.app.ajaxifyFormJson('#my_account_form', function () {
        $('#myAccountModal button').attr('disabled', false);
        bootbox.alert('Your informations have been updated');
        return false;
    }, function () {
        $('#myAccountModal button').attr('disabled', false);
        bootbox.alert('An error has occured');
        return false;
    },
    'application/json', function () {
        $('#myAccountModal button').attr('disabled', true);
    }, true);

    $('#my_account_form').attr('action', '/Users/' + window.app.username);
});