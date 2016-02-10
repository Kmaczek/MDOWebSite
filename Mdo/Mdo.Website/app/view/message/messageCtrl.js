(function () {
    angular.module('mdo').controller('messageCtrl', ['$scope', 'mdoNav', 'MSG', function ($scope, mdoNav, MSG) {
        if (getMessage()) {
            $scope.messageTitle = getMessage().title;
            $scope.message = getMessage().message;
        } else {
            $scope.messageTitle = getUnspecified().title;
            $scope.message = getUnspecified().message;
        }

        function getMessage() {
            return MSG[mdoNav.data()];
        }

        function getUnspecified() {
            return MSG['Unspecified'];
        }
    }]);
}())
