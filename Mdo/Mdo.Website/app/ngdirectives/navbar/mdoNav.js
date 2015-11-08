(function () {
    angular.module('mdo').directive('mdoNav', ['User', function (User) {
        var link = function (scope, element, attrs) {
            scope.login = login;

            function login() {
                console.log(scope.username);
                console.log(scope.password);

                User.getUser(scope.username);
            }
        }

        return {
            templateUrl: 'app/ngdirectives/navbar/mdoNav.template.html',
            scope: {
                login: '='
            },
            link: link
        }

    }]);
}());

