(function () {
    angular.module('mdo').factory('mdoNav', ['$location',
        function ($location) {
            var to = {
                messagePage: goToMessagePage
            };

            var data = {};
            var target = {
                controller: '',
                path: ''
            };

            function goToMessagePage(messageKey) {
                var path = '/message';
                data = messageKey;
                target.path = path;
                $location.path(path);
            }

            return {
                data: function() {
                    return data;
                },
                target: target,
                to: to
            }
        }]);

}())
