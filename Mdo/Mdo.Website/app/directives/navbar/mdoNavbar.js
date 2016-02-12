(function () {
    angular.module('mdo').directive('mdoNavbar', function () {

        var controller = ['$scope', 'appInfo', 'mdoNavigate', 'UserService', function ($scope, appInfo, mdoNavigate, UserService) {

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
                mdoNavigate.to.main();
                clearLoginInput();
            }

            function clearLoginInput() {
                $scope.username = '';
                $scope.password = '';
            }

            init();
        }];

        return {
            templateUrl: 'app/directives/navbar/mdoNavbar.template.html',
            scope: {
                login: '='
            },
            controller: controller
        }
    });
}());

