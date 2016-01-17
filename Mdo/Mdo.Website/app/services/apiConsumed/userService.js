(function () {
    angular.module('mdo').service('UserService', ['UserResource', 'appInfo', 'toastr', '$cookieStore', function (UserResource, appInfo, toastr, $cookieStore) {

        function handleError(errorData)
        {
            toastr.error(errorData.data.message, errorData.statusText);
        }

        function handleSuccessMessage(successData) {
            var message = successData.message;
            toastr.success(message);
        }

        function fireRequest(resource, parameters, onSuccess, onFail) {
            resource(parameters, onSuccess, onFail);
        }

        function fireRequestDefaultError(resource, parameters, onSuccess, onFail) {
            return resource(parameters, onSuccess, function (rejectData) {
                onFail(rejectData);
                handleError(rejectData);
            });
        }

        this.login = function (username, password) {
            var loginData = {
                UsernameOrEmail: username,
                Password: password
            };

            return fireRequestDefaultError(UserResource.login, loginData,
                function (data) {
                    appInfo.saveSession(data.username);

                    handleSuccessMessage(data);
                },
                function () {
                    appInfo.endSession();
                });
        }

        this.register = function (userData) {
            var registrationData = {
                Username: userData.username,
                Email: userData.email,
                Password: userData.password
            };

            return fireRequestDefaultError(UserResource.register, registrationData,
                function(data)
                {
                    console.log(data);
                });
        }

    }]);
}());