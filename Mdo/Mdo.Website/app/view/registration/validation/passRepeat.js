(function () {
    angular.module('mdo').directive('passRepeat', function () {

        function linkFunc(scope, element, attributes, ngModel) {
            ngModel.$validators.passrepeat = function (modelValue) {
                if (ngModel.$dirty) {
                    var pass = ngModel.$$parentForm.uPass;
                    var passValid = pass.$valid;
                    return passValid && modelValue === pass.$viewValue;
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