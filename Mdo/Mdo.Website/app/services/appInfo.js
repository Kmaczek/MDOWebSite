// example provider, can be configured in config, through initializeWith()
// then service can be used across app, ['appInfo', function(appInfo){}]
// <script type="text/javascript" src="app/ngservices/appInfo.js"></script> after app.js

(function () {
    angular.module('mdo').provider('appInfo', function AppInfoProvider()
    {
        var initData;

        this.initializeWith = function(initializationData)
        {
            initData = initializationData;
        };

        this.$get = ['$cookieStore', function appInfoFactory($cookieStore)
        {
            return new AppInfo(initData, $cookieStore);
        }];

        function AppInfo(initData, $cookieStore) {
            var storedSessionExpire = 'sessionExpire';
            var storedUsername = 'username';
            var container = {
                version: ''
            }

            this.container = container;

            function initialize(initData)
            {
                if (initData)
                {
                    Object.keys(initData).forEach(function(key) {
                        container[key] = initData[key];
                    });
                }
            }

            initialize(initData);

            this.get = function(property)
            {
                if (property in container)
                {
                    return container[property];
                }

                return null;
            };

            this.saveSession = function (username) {
                container.loggedIn = true;
                container.username = username;
                $cookieStore.put(storedSessionExpire, moment().add(7, 'days'));
                $cookieStore.put(storedUsername, username);
            }

            this.restoreSession = function() {
                var sessionDate = $cookieStore.get(storedSessionExpire);
                if (moment(sessionDate, moment.ISO_8601).isAfter(moment())) {
                    container.loggedIn = true;
                    container.username = $cookieStore.get(storedUsername);
                } else {
                    container.loggedIn = false;
                    container.username = '';
                }
            }

            this.endSession = function() {
                container.loggedIn = false;
                container.username = '';
                $cookieStore.remove(storedSessionExpire);
                $cookieStore.remove(storedUsername);
            }
        }
    });
}());