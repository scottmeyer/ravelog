angular.module('ravelog', ['ravelog.controllers','ravelog.services', 'ui.state', 'ngGrid'])
.config(['$routeProvider', '$stateProvider', function ($routeProvider, $stateProvider) {

    $routeProvider.when('/log/information',
        {
            templateUrl: 'partials/log-grid.html',
            controller: 'InformationController'
        });

    $routeProvider.when('/test/submit',
        {
            templateUrl: 'partials/test.html',
            controller: 'TestController'
        });

    $routeProvider.otherwise({ redirectTo: '/test/submit' });

    $stateProvider
        .state('information', {
            url: "/log/information",
            templateUrl: "partials/log-grid.html"
        })
}]);