(function () {
    var MessageModel = function (title, content) {
        this.title = title;
        this.content = content;
    };
    angular.module('mdo').constant('MSG', {
        Unspecified: new MessageModel('Content not specified', ''),
        UserCreated: new MessageModel('User created', 'Please log in'),
        Unauthorized: new MessageModel('Unauthorized', 'Please log in to access page')
    });
}());

