(function () {
    angular.module('mdo').controller('registrationCtrl', ['$scope', 'UserService', function ($scope, UserService) {

        $scope.user = {
            username: '',
            email: '',
            password: '',
            passwordCheck: ''
        }
        
        $scope.register = function()
        {
            if (fieldsValid())
            {
                UserService.register($scope.user);
            }
        }

        function fieldsValid()
        {
            return true;
        }
    }]);

}())

