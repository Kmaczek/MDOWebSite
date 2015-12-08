(function () {
    angular.module('mdo').service('UserService', ['UserResource', 'appInfo', function (UserResource, appInfo) {

        function handleError(errorData) {
            var error = errorData.status + ': ' + errorData.data.message;
            console.log(error);
        }

        this.login = function (username, password) {
            var loginData = {
                UsernameOrEmail: username,
                Password: password
            };
            var userLogin = UserResource.login(
                loginData,
                function (data)
                {
                    console.log(data);
                    appInfo.container.loggedIn = true;
                    appInfo.container.username = data.username;
                }, function (err) {
                    appInfo.container.loggedIn = false;
                    appInfo.container.username = '';
                    handleError(err);
                });
        }

        this.register = function (userData)
        {
            var registrationData = {
                Username: userData.username,
                Email: userData.email,
                Password: userData.password
            };
            var userRegister = UserResource.register(
                registrationData,
                function (data) {
                    console.log(data);
                },
                function (err) {
                    handleError(err);
                });
        }

    }]);
}());