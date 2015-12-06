(function ()
{
    var boot = new MdoBoot();

    function config($stateProvider, $locationProvider, $urlRouterProvider, appInfoProvider)
    {
        appInfoProvider.initializeWith({
            loggedIn: false
        });
        $locationProvider.html5Mode(true);
        $urlRouterProvider.otherwise("/");

        $stateProvider
            .state('main', {
                url: '/',
                templateUrl: 'app/view/main/main.template.html'
            })
            .state('user', {
                url: '/user',
                templateUrl: 'app/view/user/user.template.html'
            })
            .state('userRegistration', {
                url: '/user/register',
                templateUrl: 'app/view/registration/registration.template.html'
            });
    }

    function run() {

    }

    angular.module('mdo', ['ui.router', 'ui.bootstrap', 'ngResource'])
        .constant('mdoConst', boot.getData())
        .config(config);
})();    
