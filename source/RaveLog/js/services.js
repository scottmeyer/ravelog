angular.module('ravelog.services', [])
    .factory('log', ['$http', function($http){
        var log = {};

        log.getAll = function (url, callback) {
            $http({
                url: url,
                method: "GET",
                headers: {
                    "Content-Type": "application/json"
                }
            }).success(function (response) {
                callback(response);
            });
        }

        return log;
    }]);