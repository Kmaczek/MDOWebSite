(function () {
    angular.module('mdo').service('User', ['$resource', function ($resource) {

        var _username = '';
        var authorized = false;

        var sth = $resource('http://localhost:15555/user/:username');

        this.setUsername = function (username) {
            _username = username;
        }

        this.getUsername = function () {
            if (!authorized || !_username) {
                console.warn('User not authorized');
                return 'unknown';
            }
            return _username;
        }

        this.getUser = function (name) {

            var user = sth.get({ username: name }, function () {
                console.log(user);
                return user;
            });
        }

        this.login = function(username, password) {
            var user = sth.get({username: 'kmakov'}, function() {
                console.log(user);
            });
        }

    }]);
}());