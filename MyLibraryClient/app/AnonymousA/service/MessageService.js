app.service("MessageService", function ($http) {
    this.Save = function (message) {
        var req = $http({
            method: 'Post',
            url: "http://localhost:19679/odata/Messages",
            data: message
        })
        return req;
    };
    

});