(function ()
{
    var boot = new MdoBoot();

    function config($stateProvider, $locationProvider, $urlRouterProvider, appInfoProvider, toastrConfig)
    {
        appInfoProvider.initializeWith({
            loggedIn: false,
            username: ''
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

        angular.extend(toastrConfig, {
            preventOpenDuplicates: true,
            progressBar: true,
            timeOut: 200000
        });
    }

    function run() {

    }

    angular.module('mdo', ['ui.router', 'ui.bootstrap', 'ngResource', 'ngAnimate', 'toastr'])
        .constant('mdoConst', boot.getData())
        .config(config);
})();    
