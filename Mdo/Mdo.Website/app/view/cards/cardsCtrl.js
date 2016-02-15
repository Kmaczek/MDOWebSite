(function () {
    angular.module('mdo').controller('cardsCtrl', ['$scope', 'appInfo', 'CardsService', function ($scope, appInfo, CardsService) {

        $scope.fetch = function() {
            var exp = CardsService.resource.getExpansions().$promise.then(function (data) {
                $scope.expansions = data;
            });
        }
        // executed code
        function initialize() {
            $scope.hello = 'In user!';

            console.log('in user ctrl');

            var exp = CardsService.resource.getExpansions().$promise.then(function(data) {
                var exps = data;
            });

            var car = CardsService.resource.getCards().$promise.then(function (data) {
                var cards = data;
            });
        }
        
        initialize();
    }]);
}())
