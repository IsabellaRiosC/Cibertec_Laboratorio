//iife + tab + tab
//jasmine unit test
//https://github.com/johnpapa/angular-styleguide/blob/master/a1/README.md
(function () {
    'use strict';
    angular.module('app', ['ngRoute',
                           'ui.router',
                           'LocalStorageModule',
                           'ngAnimate',
                           'ngSanitize',
                            'ui.bootstrap'
                  ]);

})();