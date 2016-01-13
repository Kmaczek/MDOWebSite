(function () {
    angular.module('mdo').directive('passRepeat', function () {

        function linkFunc(scope, element, attributes, ngModel) {
            scope.$watch(function() {
                return scope.register.uPass.$viewValue;
            }, function () {
                ngModel.$validate();
            });

            ngModel.$validators.passrepeat = function (modelValue) {
                if (ngModel.$dirty) {
                    return scope.register.uPass.$valid &&
                        modelValue === scope.register.uPass.$viewValue;
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