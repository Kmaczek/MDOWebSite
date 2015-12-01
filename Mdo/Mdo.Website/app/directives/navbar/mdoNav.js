(function () {
    angular.module('mdo').directive('mdoNav', ['UserService', function (UserService) {
        var link = function (scope, element, attrs) {
            scope.login = login;

            function login() {
                console.log(scope.username);
                console.log(scope.password);

                UserService.login(scope.username, scope.password);
            }

            function register()
            {
                
            }
        }

        return {
            templateUrl: 'app/directives/navbar/mdoNav.template.html',
            scope: {
                login: '='
            },
            link: link
        }

    }]);
}());

