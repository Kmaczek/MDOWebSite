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
                getCard: {
                    url: cardApiUrl + '/:name',
                    method: 'GET'
                },
                getExpansions: {
                    url: cardApiUrl + '/expansions',
                    method: 'GET',
                    isArray: true
                },
                scrap: {
                    url: cardApiUrl + '/scrap',
                    method: 'GET'
                }
            });

        function getCard(cardName) {
            return card.getCard({name: cardName}).$promise;
        }

        return {
            resource: card,
            getCard: getCard
        }
    }]);
}());