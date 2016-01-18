(function () {
    angular.module('mdo').directive('emailVal', function ($q, UserService) {

        function linkFunc(scope, element, attributes, ngModel) {
            ngModel.$asyncValidators.emailexists = function (modelValue, viewValue) {
                if (viewValue.length < 6) {
                    return $q.resolve();
                }
                var user = UserService.getEmail(viewValue);
                return user.$promise.then(
                    function (response) {
                        if (response.email) {
                            return $q.reject();
                        }
                        return true;
                    },
                    function (response) {
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