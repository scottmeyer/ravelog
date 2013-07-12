angular.module('ravelog.controllers', [])
    .controller('NavigationController', ['$scope', '$location', function ($scope, $location) {

        $scope.active = function(route) {
            return route === $location.path();
        };

    }])
    .controller('LogController', ['$scope', '$http','$state', function ($scope, $http, $state) {
        $scope.logData = [];
        $scope.grid = {
            data: 'logData',
            multiSelect: false
        };
        
        var refresh = function () {
            $http({
                url: $state.current.data.url,
                method: "GET",
                headers: {
                    "Content-Type": "application/json"
                }
            }).success(function(data) {
                $scope.logData = data;
            });
        };

        $scope.$on('$stateChangeSuccess', function(event, toState, toParams, fromState, fromParams) {
             refresh();
        });
    }])
  .controller('TestController', ['$scope', '$http', function ($scope, $http) {

          $scope.url = 'http://localhost:8088/log/information';
          $scope.payload = "{'dateCreated': '" + moment().format() + "', 'application': 'ravelog test', 'message':'This is a test message.'}";

          $scope.send = function() {
              $http({
                  url: $scope.url,
                  data: $scope.payload,
                  method: "POST",
                  headers: {
                      "Content-Type": "application/json"
                  }
              }).success(function(response) {
                  $scope.response = response;
              });
          };
  }]);