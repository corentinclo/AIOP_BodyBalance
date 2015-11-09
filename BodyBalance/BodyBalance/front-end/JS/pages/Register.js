window.app.ajaxifyFormJson('#register_form', function () {
    location.reload();
    return false;
}, function () {
    bootbox.alert('An error has occured');
    return false;
},
'application/json',
function () {
    //Validation
    if ($('#password_input').val() != $('#c_password_input').val()) {
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
});

window.app.hrefToFunction('#main');