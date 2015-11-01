(function () {
    function config($routeProvider) {
        $routeProvider
            .when('/', {
                controller: 'mainController',
                templateUrl: 'view/main/main.html'
            });
    }

    config.$inject = ['$routeProvider'];

    function run() {

    }

    angular
        .module('mdo', ['ngRoute'])
        .config(config)
        .run(run);
}());