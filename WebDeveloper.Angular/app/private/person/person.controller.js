(function () {

    'use strict';

     angular.module('app')
    .controller('personController', personController);

     personController.$inject = ['dataService'];

     function personController(dataService) {

         var vm = this;   // se declara la variable para no sobrecargar 
         vm.title = 'Person Controller';
         vm.personList = [];

         init();


         function init() {

             loadData();
         }


         function loadData() {

             vm.personList = [];
             var apiUrl = 'http://localhost/WebDeveloper.API/person/';
             var url = apiUrl + 'list/1/15';
             dataService.getData(url)
                 .then(function (result) {
                     vm.personList = result.data;


                 },

             function (error) {

                 console.log(error);

             });

         }
    }


})();