﻿(function () {
    var boot = new MdoBoot();

    function config($stateProvider, $locationProvider, $urlRouterProvider, appInfoProvider, toastrConfig) {
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
            .state('cards', {
                url: '/cards',
                templateUrl: 'app/view/cards/cards.template.html',
                data: {
                    permissions: {
                        only: ['admin']
                    }
                }
            })
            .state('userRegistration', {
                url: '/user/register',
                templateUrl: 'app/view/registration/registration.template.html'
            })
            .state('message', {
                url: '/message',
                templateUrl: 'app/view/message/message.template.html'
            });

        angular.extend(toastrConfig, {
            preventOpenDuplicates: true,
            progressBar: true,
            timeOut: 1200,
            extendedTimeOut: 1200
        });
    }

    var run = ['appInfo', 'RoleStore', 'PermissionStore',
        function (appInfo, RoleStore, PermissionStore) {
            appInfo.restoreSession();

            PermissionStore.definePermission('authorized', function (stateParams) {
                if (appInfo.container.isloggedIn) {
                    return true;
                }
                return false;
            });

            PermissionStore.definePermission('isAdmin', function (stateParams) {
                var isAdmin = appInfo.container.sessionData &&
                    Enumerable
                    .From(appInfo.container.sessionData.roles)
                    .Contains('administrator');
                return isAdmin;
            });

            PermissionStore.definePermission('isMod', function (stateParams) {
                var isAdmin = appInfo.container.sessionData &&
                    Enumerable
                    .From(appInfo.container.sessionData.roles)
                    .Contains('moderator');
                return isAdmin;
            });

            PermissionStore.definePermission('isUser', function (stateParams) {
                var isAdmin = appInfo.container.sessionData &&
                    Enumerable
                    .From(appInfo.container.sessionData.roles)
                    .Contains('standard');
                return isAdmin;
            });

            RoleStore.defineRole('admin', ['authorized', 'isAdmin']);
            RoleStore.defineRole('moderator', ['authorized', 'isMod']);
            RoleStore.defineRole('user', ['authorized', 'isUser']);
        }
    ];

    angular.module('mdo', ['ui.router', 'ui.bootstrap', 'ngResource', 'ngAnimate', 'ngMessages', 'ngCookies', 'permission', 'toastr'])
        .constant('mdoConst', boot.getData())
        .config(config)
        .run(run);
})();
