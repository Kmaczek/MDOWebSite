(function () {
    angular.module('mdo')
        .constant('USER_ROLES', {
            all: '*',
            admin: 'admin',
            standard: 'standard',
            guest: 'guest'
        });
}())