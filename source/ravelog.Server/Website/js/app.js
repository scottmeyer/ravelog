angular.module('ravelog', ['ravelog.controllers','ravelog.services', 'ui.state', 'ngGrid'])
.config(['$routeProvider', '$stateProvider', function ($routeProvider, $stateProvider) {

    $routeProvider
        .when('/log/information',
            {
                templateUrl: 'partials/log-grid.html',
                controller: 'InformationController'
            })
        .when('/log/warning',
            {
                templateUrl: 'partials/log-grid.html',
                controller: 'WarningController'
            })
        .when('/log/error',
            {
                templateUrl: 'partials/log-grid.html',
                controller: 'ErrorController'
            })
        .when('/log/trace',
            {
                templateUrl: 'partials/log-grid.html',
                controller: 'TraceController'
            })
        .when('/test/submit',
            {
                templateUrl: 'partials/test.html',
                controller: 'TestController'
            })
        .otherwise({ redirectTo: '/log/information' });

    $stateProvider
        .state('information', {
            url: "/log/information",
            templateUrl: "partials/log-grid.html"
        })
        .state('warning', {
            url: "/log/warning",
            templateUrl: "partials/log-grid.html"
        })
        .state('error', {
            url: "/log/error",
            templateUrl: "partials/log-grid.html"
        })
        .state('trace', {
            url: "/log/trace",
            templateUrl: "partials/log-grid.html"
        });
}]);