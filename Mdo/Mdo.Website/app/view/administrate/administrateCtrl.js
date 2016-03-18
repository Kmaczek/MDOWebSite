(function () {
    angular.module('mdo').controller('administrateCtrl', ['$scope', 'appInfo', 'CardsService', 'AdminService', 'toastr', function ($scope, appInfo, CardsService, AdminService, toastr) {

        $scope.scrap = function () {
            var exp = CardsService.resource.scrap().$promise.then(function (result) {
                toastr.success(result.message);
            }, function (result) {
                toastr.error(result.message);
            });
        }

        $scope.createLabel = function () {
            AdminService.fixLabel($scope.card.name, $scope.inputs.offset).then(function (result) {
                toastr.success(result.message);
                refreshLabel();
            }, function (result) {
                toastr.error(result.message);
            });
        }

        $scope.downloadImage = function() {
            AdminService.fetchCardImage($scope.card.name, $scope.inputs.cardImageUrl).then(function (result) {
                toastr.success(result.message);
            }, function (result) {
                toastr.error(result.message);
            });
        }

        $scope.getCard = function () {
            CardsService.getCard($scope.inputs.cardName).then(function (result) {
                $scope.card = result;
                setImagesUrls();
                toastr.success(result.name + ' fetched successfuly');
            }, function (result) {
                toastr.error(result.message);
            });
        }

        // executed code
        function initialize() {
        }

        function setImagesUrls() {
            $scope.labelUrl = $scope.card.labelPath;
            $scope.imageUrl = $scope.card.imagePath;
        }

        function refreshLabel() {
            var random = Math.random();
            $scope.labelUrl = $scope.card.labelPath + '?dc=' + random;
        }

        initialize();
    }]);
}())
