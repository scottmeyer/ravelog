angular.module('ravelog.controllers', [])
    .controller('NavigationController', ['$scope', '$location', function ($scope, $location) {

        $scope.active = function (route) {
            return route === $location.path();
        }

    }])
  .controller('InformationController', ['$scope', '$http','log', function ($scope, $http, log) {

      log.getAll("http://localhost:8088/log/information", function (data) {
          $scope.logData = data;
      })

      $scope.grid = {
          data: 'logData',
          multiSelect: false
      };
  }])
  .controller('TestController', ['$scope', '$http', function ($scope, $http) {

      $scope.url = 'http://localhost:8088/log/information';
      $scope.payload = "{'dateCreated': '"+ moment().format() + "', 'application': 'ravelog test', 'message':'This is a test message.'}"

      $scope.send = function () {
          $http({
              url: $scope.url,
              data: $scope.payload,
              method: "POST",
              headers: {
                  "Content-Type": "application/json"
              }
          }).success(function (response) {
              $scope.response = response;
          });
      }
  }]);