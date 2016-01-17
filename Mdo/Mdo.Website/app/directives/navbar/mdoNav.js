(function () {
    angular.module('mdo').directive('mdoNav', function () {

        var controller = ['$scope', 'appInfo', 'UserService', function ($scope, appInfo, UserService) {

            function init() {
                $scope.shared = appInfo.container;
                $scope.login = login;
                $scope.logout = logout;
            }

            function login() {
                UserService.login($scope.username, $scope.password);
            }

            function logout() {
                appInfo.endSession();
            }

            init();
        }];

        return {
            templateUrl: 'app/directives/navbar/mdoNav.template.html',
            scope: {
                login: '='
            },
            controller: controller
        }
    });
}());

