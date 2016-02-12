(function () {
    angular.module('mdo').controller('messageCtrl', ['$scope', '$state', 'mdoNavigate', 'MSG', 'appInfo', function ($scope, $state, mdoNavigate, MSG, appInfo) {

        // executed code
        function initialize() {

            if (getMessage()) {
                $scope.messageTitle = getMessage().title;
                $scope.message = getMessage().content;
            } else if (getMessageFromState()) {
                $scope.messageTitle = getMessageFromState().title;
                $scope.message = getMessageFromState().content;
            } else {
                $scope.messageTitle = getUnspecified().title;
                $scope.message = getUnspecified().content;
            }

            $scope.$watch(function() {
                return appInfo.container.isloggedIn;
            }, function(val) {
                if (val === true) {
                    mdoNavigate.toPage(returnPage());
                }
            });
        }

        function returnPage() {
            return $state.current.data.backState();
        }

        function getMessage() {
            return MSG[mdoNavigate.data()];
        }

        function getMessageFromState() {
            return MSG[$state.current.data.messageFromState()];
        }

        function getUnspecified() {
            return MSG['Unspecified'];
        }

        initialize();
    }]);
}())
