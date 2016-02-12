(function () {
    angular.module('mdo').factory('mdoNavigate', ['$location',
        function ($location) {
            var to = {
                main: goToMainPage,
                messagePage: goToMessagePage
            };

            var data = {};
            var target = {
                controller: '',
                path: ''
            };

            function goToMainPage() {
                var path = '/';
                target.path = path;
                $location.path(path);
            }

            function goToMessagePage(messageKey) {
                data = messageKey;
                var path = '/message';
                target.path = path;
                $location.path(path);
            }

            function toPage(path) {
                target.path = path;
                $location.path(path);
            }

            return {
                data: function() {
                    return data;
                },
                target: target,
                to: to,
                toPage: toPage
            }
        }]);

}())
