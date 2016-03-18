(function () {
    angular.module('mdo').service('AdminService', ['$resource', 'mdoConst', 'appInfo', 'toastr', function ($resource, mdoConst, appInfo, toastr) {

        var apiBase = mdoConst.apiUrl;
        var adminApiUrl = apiBase + '/admin';
        var admin = $resource(adminApiUrl, {},
            {
                fixCardLabel: {
                    url: adminApiUrl + '/fixlabel',
                    method: 'POST'
                },
                fetchCardImage: {
                    url: adminApiUrl + '/downloadimage',
                    method: 'POST'
                }
            });

        function fixLabel(cardName, yOffset) {
            return admin.fixCardLabel({}, { cardName: cardName, yoffset: yOffset }).$promise;
        }

        function fetchCardImage(cardName, url) {
            return admin.fetchCardImage({}, {cardName: cardName, url: url}).$promise;
        }

        return {
            resource: admin,
            fixLabel: fixLabel,
            fetchCardImage: fetchCardImage
        }
    }]);
}());