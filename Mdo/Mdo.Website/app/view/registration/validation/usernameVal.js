(function () {
    angular.module('mdo').directive('usernameVal', function ($q, UserService) {

        function linkFunc(scope, element, attributes, ngModel) {
            ngModel.$asyncValidators.username = function (modelValue, viewValue) {
                if (viewValue.length < 3) {
                    return $q.resolve();
                }
                var user = UserService.get(viewValue);
                return user.$promise.then(
                    function(response) {
                        if (response.username) {
                            return $q.reject();
                        }
                        return true;
                    },
                    function(response) {
                        return true;
                    });
            }
        }

        return {
            restrict: 'A',
            require: '?ngModel',
            link: linkFunc
        }
    });
}())