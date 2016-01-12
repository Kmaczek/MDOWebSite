(function() {
    angular.module('mdo').directive('email', function () {
        var pattern = /^[_a-z0-9]+(\.[_a-z0-9]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,4})$/;

        function linkFunc(scope, element, attributes, ngModel) {
            ngModel.$validators.email = function (modelValue) {
                if (ngModel.$dirty) {
                    return pattern.test(modelValue);
                }
                return true;
            }
        }

        return {
            restrict: 'A',
            require: '?ngModel',
            link: linkFunc
        }
    });
}())