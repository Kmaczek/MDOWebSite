(function () {
    angular.module('mdo').directive('mdoNav', ['User', function (User) {
        var link = function (scope, element, attrs) {
            scope.login = login;

            function login() {
                console.log(scope.username);
                console.log(scope.password);

                User.login(scope.username, scope.password);
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

