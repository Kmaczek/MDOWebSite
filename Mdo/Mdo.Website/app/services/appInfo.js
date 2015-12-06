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

        this.$get = ['$q', function appInfoFactory($q)
        {
            return new AppInfo(initData);
        }];

        function AppInfo(initData)
        {
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
        }
    });
}());