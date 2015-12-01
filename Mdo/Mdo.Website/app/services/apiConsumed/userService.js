(function () {
    angular.module('mdo').service('UserService', ['UserResource', function (UserResource) {

        function handleError(errorData) {
            var error = errorData.status + ': ' + errorData.data.message;
            console.log(error);
        }

//        this.getUser = function (name) {
//            var userGet = UserResource.get(
//                { username: name },
//                function (data) {
//                    console.log(data);
//                },
//                function (err) {
//                    handleError(err);
//                });
//        }

        this.login = function (username, password) {
            var loginData = {
                Username: username,
                Password: password
            };
            var userSave = UserResource.login(
                loginData,
                function (data) {
                    console.log(data);
                }, function (err) {
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