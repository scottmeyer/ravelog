angular.module('ravelog.controllers', [])
    .controller('NavigationController', ['$scope', '$location', function ($scope, $location) {

        $scope.active = function(route) {
            return route === $location.path();
        };

    }])
  .controller('InformationController', ['$scope','$http', function ($scope, $http) {
      $scope.logData = [];
      $scope.grid = {
          data: 'logData',
          multiSelect: false
      };

      $http({
          url: 'http://localhost:8088/log/information',
          method: "GET",
          headers: {
              "Content-Type": "application/json"
          }
      }).success(function (data) {
          $scope.logData = data;
      });
  }])
  .controller('WarningController', ['$scope', '$http', function ($scope, $http) {
      $scope.logData = [];
      $scope.grid = {
          data: 'logData',
          multiSelect: false
      };

      $http({
          url: 'http://localhost:8088/log/warning',
          method: "GET",
          headers: {
              "Content-Type": "application/json"
          }
      }).success(function (data) {
          $scope.logData = data;
      });
  }])
  .controller('ErrorController', ['$scope', '$http', function ($scope, $http) {
      $scope.logData = [];
      $scope.grid = {
          data: 'logData',
          multiSelect: false
      };

      $http({
          url: 'http://localhost:8088/log/error',
          method: "GET",
          headers: {
              "Content-Type": "application/json"
          }
      }).success(function (data) {
          $scope.logData = data;
      });
  }])
  .controller('TraceController', ['$scope', '$http', function ($scope, $http) {
      $scope.logData = [];
      $scope.grid = {
          data: 'logData',
          multiSelect: false
      };

      $http({
          url: 'http://localhost:8088/log/trace',
          method: "GET",
          headers: {
              "Content-Type": "application/json"
          }
      }).success(function (data) {
          $scope.logData = data;
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