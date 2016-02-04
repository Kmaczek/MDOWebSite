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
            var mdoCookie = 'mdoCookie';
            var container = {
                sessionData: new SessionData(),
                isloggedIn: false
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

            function clearSession() {
                container.isloggedIn = false;
                container.sessionData.cleanData();
            }

            function fillSessionData() {
                container.sessionData.setFromCookie($cookieStore.get(mdoCookie));
                container.isloggedIn = true;
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

            this.saveSession = function (sessionData) {
                var cookieValue = SessionData.fromSessionData(sessionData);
                $cookieStore.put(mdoCookie, cookieValue);

                fillSessionData();
            }

            this.restoreSession = function () {
                var cookie = $cookieStore.get(mdoCookie);
                if (!cookie) {
                    return;
                }
                var sessionDate = cookie.sessionExpire;

                if (moment(sessionDate, moment.ISO_8601).isAfter(moment())) {
                    fillSessionData();
                } else {
                    clearSession();
                }
            }

            this.endSession = function() {
                clearSession();
                $cookieStore.remove(mdoCookie);
            }
        }
    });
}());

//TODO: can be moved into angular service
SessionData = function(username, secret, roles, sessionExpire) {
    
    this.username = username;
    this.secret = secret;
    this.roles = roles;

    if (sessionExpire) {
        this.sessionExpire = sessionExpire;
    } else {
        this.sessionExpire = moment().add(7, 'days');
    }

    this.setFromCookie = function(cookie) {
        this.username = cookie.username;
        this.secret = cookie.secret;
        this.roles = cookie.roles;
        this.sessionExpire = sessionExpire;
    }

    this.cleanData = function() {
        this.username = '';
        this.secret = '';
        this.roles = [];
        this.sessionExpire = null;
    }
}

SessionData.fromSessionData = function (sessionData) {
    return new SessionData(sessionData.username, sessionData.secret, sessionData.roles);
}

LoginData = function(username, secret, roles) {
    this.username = username;
    this.secret = secret;
    this.roles = roles;
}

LoginData.fromResponse = function(response) {
    return new LoginData(response.username, response.secret, response.roles);
}