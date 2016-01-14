(function () {
    angular.module('mdo').controller('registrationCtrl', ['$scope', 'UserService', function ($scope, UserService) {

        var registerClicked = false;
        $scope.showValidAndDirtyMessage = showValidAndDirtyMessage;

        $scope.user = {
            username: '',
            email: '',
            password: '',
            passwordCheck: ''
        }

        $scope.registerUser = function () {
            registerClicked = true;
            if (areFieldsValidAndDirty()) {
                UserService.register($scope.user);
            }
        }

        function showValidAndDirtyMessage() {
            return registerClicked && !areFieldsValidAndDirty();
        }

        function areFieldsValidAndDirty() {
            return validAndDirty($scope.register.uName)
                && validAndDirty($scope.register.uEmail)
                && validAndDirty($scope.register.uPass)
                && validAndDirty($scope.register.uPassRepeat);
        }

        function validAndDirty(input) {
            return input && input.$valid && input.$dirty;
        }

    }]);

}())

