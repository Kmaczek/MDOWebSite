(function () {
    angular.module('mdo').controller('mainCtrl', ['$scope', function ($scope) {
        $scope.hello = "Yo man!";

        console.log('in main ctrl');
    }]);

}())

