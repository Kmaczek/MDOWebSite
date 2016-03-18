(function () {
    angular.module('mdo').controller('cardsCtrl', ['$scope', 'appInfo', 'CardsService', function ($scope, appInfo, CardsService) {

        $scope.fetch = function() {
            var exp = CardsService.resource.getExpansions().$promise.then(function (data) {
                $scope.expansions = data;
            });
        }

        // executed code
        function initialize() {

            var car = CardsService.resource.getCards().$promise.then(function (data) {
                $scope.cards = data;
            });
        }
        
        initialize();
    }]);
}())
