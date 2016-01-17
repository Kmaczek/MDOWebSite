(function() {
    angular.module('mdo')
        .constant('MDO_EV', {
            loginSuccess: 'login-success',
            loginFailed: 'login-failed',
            sessionTimeout: 'session-timeout',
            notAuthorized: 'not-authorized'
        });
}())