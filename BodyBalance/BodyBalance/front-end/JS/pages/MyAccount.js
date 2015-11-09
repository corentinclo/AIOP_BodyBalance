$(function () {
    var testAccount = {
        UserId: "Test",
        Phone: "0600000000"
    };
    //On remplit le formulaire avec le compte utilisateur
    window.app.FillFormWithObject('#my_account_form', testAccount);

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
            $('#myAccountModal').dispose();
            $('#changePassModal').dispose();
        }
    });
    window.app.hrefToFunction('#myAccountModal');
})