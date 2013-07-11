angular.module('ravelog', ['ravelog.controllers'])
.config(['$routeProvider', function ($routeProvider) {
    $routeProvider.when('/log/information', { templateUrl: 'partials/information.html', controller: 'InformationController' });

    $routeProvider.when('/test/submit',
        {
            templateUrl: 'partials/test.html',
            controller: 'TestController'
        });

    $routeProvider.when('/view2', { templateUrl: 'partials/partial2.html', controller: 'MyCtrl2' });
    $routeProvider.otherwise({ redirectTo: '/test/submit' });
}]);