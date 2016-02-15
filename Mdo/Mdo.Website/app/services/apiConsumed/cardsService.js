(function () {
    angular.module('mdo').service('CardsService', ['$resource', 'mdoConst', 'appInfo', 'toastr', function ($resource, mdoConst, appInfo, toastr) {

        var apiBase = mdoConst.apiUrl;
        var cardApiUrl = apiBase + '/cards';
        var card = $resource(cardApiUrl, {},
            {
                getCards: {
                    method: 'GET',
                    isArray: true
                },
                getExpansions: {
                    url: cardApiUrl + '/expansions',
                    method: 'GET',
                    isArray: true
                }
//                register: {
//                    method: 'POST',
//                    url: userApiUrl + '/register',
//                    withCredentials: true
//                },
//                login: {
//                    method: 'POST',
//                    url: userApiUrl + '/login'
//                },
//                getEmail: {
//                    method: 'GET',
//                    url: userApiUrl + '/email/:email'
//                }
            });

        return {
            resource: card
        }
        //TODO: move methods from this service to Resource to reduce number of files
//        function handleError(errorData) {
//            toastr.error(errorData.data.message, errorData.statusText);
//        }
//
//        function handleSuccessMessage(successData) {
//            var message = successData.message;
//            toastr.success(message);
//        }
//
//        function fireRequest(resource, parameters, onSuccess, onFail) {
//            resource(parameters, onSuccess, onFail);
//        }
//
//        function fireRequestDefaultError(resource, parameters, onSuccess, onFail) {
//            return resource(parameters, onSuccess, function (rejectData) {
//                onFail(rejectData);
//                handleError(rejectData);
//            });
//        }
//
//        this.login = function (username, password) {
//            var loginData = {
//                UsernameOrEmail: username,
//                Password: password
//            };
//
//            return fireRequestDefaultError(UserResource.login, loginData,
//                function (data) {
//                    appInfo.saveSession(LoginData.fromResponse(data));
//
//                    handleSuccessMessage(data);
//                },
//                function () {
//                    appInfo.endSession();
//                });
//        }
//
//        this.register = function (userData) {
//            var registrationData = {
//                Username: userData.username,
//                Email: userData.email,
//                Password: userData.password
//            };
//
//            return UserResource.register(registrationData);
//        }
//
//        this.get = function (username) {
//            return UserResource.get({ username: username });
//        }
//
//        this.getEmail = function (email) {
//            return UserResource.getEmail({ email: email });
//        }

    }]);
}());