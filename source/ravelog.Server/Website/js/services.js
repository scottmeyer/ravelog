angular.module('ravelog.services', [])
    .factory('log', ['$http', function($http){
        var log = {};

        log.load = function(url, callback) {
            $http({
                url: url,
                method: "GET",
                headers: {
                    "Content-Type": "application/json"
                }
            }).success(function(data) {
                callback(data);
            });
        };

        return log;
    }]);