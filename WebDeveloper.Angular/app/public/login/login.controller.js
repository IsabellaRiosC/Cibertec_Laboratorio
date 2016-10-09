(function () {
    'use strict';
    angular.module('app')
    .controller('loginController', loginController);

    loginController.$inject = [/*'$http', '$state',*/ 'authenticationService'];

    function loginController(/*$http, $state,*/ authenticationService) {
        var vm = this;
        vm.user = {};
        vm.title = 'Login';
        vm.login = login;

        function login() {
            authenticationService.login(vm.user);
        }
    }
})();