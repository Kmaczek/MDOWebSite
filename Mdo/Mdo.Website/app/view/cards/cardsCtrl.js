(function () {
    angular.module('mdo').controller('cardsCtrl', ['$scope', 'appInfo', function ($scope, appInfo) {

        // executed code
        function initialize() {
            $scope.hello = 'In user!';

            console.log('in user ctrl');
        }
        
        initialize();
    }]);
}())
