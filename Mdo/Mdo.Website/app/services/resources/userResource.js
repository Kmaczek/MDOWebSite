(function () {
    angular.module('mdo').factory('UserResource', ['$resource', 'mdoConst', function ($resource, mdoConst)
    {
        var apiBase = mdoConst.apiUrl;
        var userApiUrl = apiBase + '/user';
        var user = $resource(userApiUrl + '/:username', {},
            {
                get: {
                    method: 'GET'
                },
                register: {
                    method: 'POST',
                    url: userApiUrl + '/register',
                    withCredentials: true
                },
                login: {
                    method: 'POST',
                    url: userApiUrl + '/login'
                }
            });

        return user;

    }]);
}());