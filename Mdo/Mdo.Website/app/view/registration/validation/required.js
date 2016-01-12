(function () {
    angular.module('mdo').directive('mdorequired', function () {

        function linkFunc(scope, element, attributes, ngModel) {
            ngModel.$validators.required = function (modelValue) {
                if (ngModel.$dirty) {
                    return modelValue.length > 0;
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