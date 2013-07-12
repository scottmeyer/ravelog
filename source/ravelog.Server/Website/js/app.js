angular.module('ravelog', ['ravelog.controllers','ravelog.services', 'ui.state', 'ngGrid'])
.config(['$routeProvider', '$stateProvider', function ($routeProvider, $stateProvider) {

    $routeProvider.when('/', 
    {
         redirectTo: '/log/information'
    });

    var logs = {
        name: 'log',
        url: "/log",
        abstract: true,
        templateUrl: 'partials/log-grid.html',
        controller: 'LogController'
    };

    var information = {
        name: 'log.information',
        parent: logs,
        url: '/information',
        data: {
            url: "http://localhost:8088/log/information"
        },
        onEnter: ['$state', function($state) {
            if ($state.parent !== undefined) {
                $state.parent.controller.refresh();
            }
        }]
    };

    var error = {
        name: 'log.error',
        parent: logs,
        url: '/error',
        data: {
            url: "http://localhost:8088/log/error"
        }
        ,
        onEnter: ['$state', function ($state) {
            if ($state.parent !== undefined) {
                $state.parent.controller.refresh();
            }
        }]
    };
    
    var warning = {
        name: 'log.warning',
        parent: logs,
        url: '/warning',
        data: {
            url: "http://localhost:8088/log/warning"
        }
        ,
        onEnter: ['$state', function ($state) {
            if ($state.parent !== undefined) {
                $state.parent.controller.refresh();
            }
        }]
    };
    
    var trace = {
        name: 'log.trace',
        parent: logs,
        url: '/trace',
        data: {
            url: "http://localhost:8088/log/trace"
        }
        ,
        onEnter: ['$state', function ($state) {
            if ($state.parent !== undefined) {
                $state.parent.controller.refresh();
            }
        }]
    };

    var test = {
        name: 'test',
        url: '/test',
        templateUrl: 'partials/test.html',
        controller: 'TestController'
    };

    $stateProvider
        .state(logs)
        .state(information)
        .state(warning)
        .state(error)
        .state(trace)
        .state(test);
}]);