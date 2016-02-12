(function () {
    angular.module('mdo').controller('userProfileCtrl', ['$scope', function ($scope) {
        $scope.hello = "In user!";

        console.log('in user ctrl');
    }]);
}())
