/*
//Méthode toujours appelée, exemple
window.app.mappers['*'] = function (sender) { //Toujours appelé
    $(sender).tooltip({ title: "Clické !", trigger: 'manual' , placement: 'bottom'}).tooltip('show');
    setTimeout(function () {
        $(sender).tooltip('hide').tooltip('destroy');
    }, 1000);
    return true;
}
*/

window.app.mappers['#'] = function () {
    location.reload();
    return false;
}

window.app.mappers['#about'] = function () {
    bootbox.dialog({
        message: "ZenLounge is a great gym or association or shop, well nobody knows... But it's great! We are great! And YOU are great!",
        title: "About ZenLounge",
        buttons: {
            main: {
                label: "Thanks for these information",
                className: "btn-primary",
            }
        }
    });
    return false;
}

window.app.mappers['#register'] = function () {
    $.get('pages/Register.html', null, function (data) {
        $('#main').html(data);
    });
    return false;
}

window.app.mappers['#activities'] = function () {
    $.get('pages/Activities.html', null, function (data) {
        $("li.active").removeClass("active");
        $("#activities").toggleClass("active"),
        $('#main').html(data);
    });
    return false;
}

window.app.mappers['#events'] = function () {
    $.get('pages/Events.html', null, function (data) {
        $("li.active").removeClass("active");
        $("#events").toggleClass("active"),
        $('#main').html(data);
    });
    return false;
}

window.app.mappers['#adminPanel'] = function () {
    $.get('pages/AdminPanel.html', null, function (data) {
        $("li.active").removeClass("active");
        $("#adminPanel").toggleClass("active");
        $('#main').html(data);
    });
    return false;
}

window.app.ajaxifyForm('#login_form', function (result) {
    window.app.storeLoginParameters(result.userName, result.access_token, $('#cookie_input').is(':checked'));
    window.app.mappers['#'] = function () {
        $.get('pages/MainPage.html', null, function (data) {
            $('#main').html(data);
        });
        $("li.active").removeClass("active");
        $("#home").toggleClass("active");
        return false;
    }
    window.app.mappers['#']();
}, function (result) {
    $('#login_form input[type=submit]').attr('disabled', false);
    bootbox.alert('Bad username or password');
}, 'application/json',
function () {
    $('#login_form input[type=submit]').attr('disabled', true);
});

$(function () {
    window.app.hrefToFunction('body');
    if (window.app.processCookies()) {
        //Vérification de la validité du token
        window.app.sendRestRequest('/Account/IsValidToken', 'GET', null, function () {
            //Good credentials
            $.get('pages/MainPage.html', null, function (data) {
                $('#main').html(data);
            });
            window.app.mappers['#'] = function () {
                $.get('pages/MainPage.html', null, function (data) {
                    $('#main').html(data);
                });
                $("li.active").removeClass("active");
                $("#home").toggleClass("active");
                return false;
            }
        }, function () {
            //Bad credentials
            window.app.clearLoginParameters();
            bootbox.alert('Your authorization has ended. Please log in again.');
        });
    }
});


  function statusChangeCallback(response) {
    // The response object is returned with a status field that lets the
    // app know the current login status of the person.
    // Full docs on the response object can be found in the documentation
    // for FB.getLoginStatus().
    if (response.status === 'connected') {
      // Logged into your app and Facebook.
      userID_FB=response.authResponse.userID;
      testAPI(userID_FB);
    } else if (response.status === 'not_authorized') {
        // The person is logged into Facebook, but not your app.
      document.getElementById('status').innerHTML = 'Please log ' +
        'into this app.';
    } else {
      // The person is not logged into Facebook, so we're not sure if
      // they are logged into this app or not.
      document.getElementById('status').innerHTML = 'Please log ' +
        'into Facebook.';
    }
  }

  // This function is called when someone finishes with the Login
  // Button.  See the onlogin handler attached to it in the sample
  // code below.
  function checkLoginState() {
      FB.getLoginStatus(function (response) {
          window.app.mappers['#register']();
          statusChangeCallback(response);
    });
  }

  window.fbAsyncInit = function() {
  FB.init({
    appId      : '1630074017281291',
    cookie     : true,  // enable cookies to allow the server to access the session
    xfbml      : true,  // parse social plugins on this page
    version    : 'v2.5' // use version 2.2
  });
  };

  // Load the SDK asynchronously
  (function(d, s, id) {
    var js, fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) return;
    js = d.createElement(s); js.id = id;
    js.src = "//connect.facebook.net/en_US/sdk.js";
    fjs.parentNode.insertBefore(js, fjs);
  }(document, 'script', 'facebook-jssdk'));

  // Here we run a very simple test of the Graph API after login is
  // successful.  See statusChangeCallback() for when this call is made.
  function testAPI(userID) {
      console.log('Welcome!  Fetching your information.... ');
      FB.api("/me/", { fields: 'name ,first_name ,last_name, email' }, function (response) {
          $("#email_input").val(response.email);
          $("#first_name_input").val(response.first_name);
          $("#last_name_input").val(response.last_name);

          console.log('Successful login for: ' + response.name);
          document.getElementById('status').innerHTML =
            'Thanks for logging in, ' + response.email + '!';
      });
  }




