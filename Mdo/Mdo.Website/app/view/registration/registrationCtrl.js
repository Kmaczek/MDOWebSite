(function () {
    angular.module('mdo').controller('registrationCtrl', ['$scope', 'UserService', 'mdoNav', 'toastr',
        function ($scope, UserService, mdoNav, toastr) {

        var registerClicked = false;
        $scope.showValidAndDirtyMessage = showValidAndDirtyMessage;
        $scope.serverError = false;

        $scope.user = {
            username: '',
            email: '',
            password: '',
            passwordCheck: ''
        }

        $scope.registerUser = function () {
            registerClicked = true;
            $scope.serverError = false;
            if (areFieldsValidAndDirty()) {
                UserService.register($scope.user).$promise.then(
                    function (result) {
                        mdoNav.to.messagePage('UserCreated');
                    },
                    function(error) {
                        $scope.serverErrorMsg = error.data.message;
                        $scope.serverError = true;
                    });
            }
        }

        function resetPage() {
            clearModel();
            $scope.register.$setPristine();
            $scope.register.$setUntouched();
            registerClicked = false;
        }

        function clearModel() {
            $scope.user.username = "";
            $scope.user.email = "";
            $scope.user.password = "";
            $scope.user.passwordCheck = "";
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

