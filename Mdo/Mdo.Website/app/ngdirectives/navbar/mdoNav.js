(function () {

    var link = function(scope, element, attrs) {
        scope.login = login;

        function login() {
            console.log(scope.username);
            console.log(scope.password);
        }
    }

    var mdoNav = function() {
        return {
            templateUrl: 'app/ngdirectives/navbar/mdoNav.template.html',
            scope: {
                login: '='
            },
            link: link
        }
    }

    angular.module('mdo').directive('mdoNav', mdoNav);
}());

