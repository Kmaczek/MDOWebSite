(function () {
    angular.module('mdo').directive('mdoNav', function () {

        var controller = ['$scope', 'appInfo', 'UserService', function ($scope, appInfo, UserService) {

            function init() {
                $scope.shared = appInfo.container;
                $scope.login = login;
            }

            function login() {
                console.log($scope.username);
                console.log($scope.password);

                UserService.login($scope.username, $scope.password);
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

