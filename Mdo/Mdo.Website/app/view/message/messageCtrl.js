(function () {
    angular.module('mdo').controller('messageCtrl', ['$scope', 'mdoNav', function ($scope, mdoNav) {
        $scope.messageTitle = mdoNav.data().messageTitle;
        $scope.message = mdoNav.data().message;
    }]);
}())
