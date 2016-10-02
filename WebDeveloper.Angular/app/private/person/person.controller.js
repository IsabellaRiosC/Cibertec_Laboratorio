(function () {

    'use strict';
    var apiUrl = 'http://localhost/WebDeveloper.API/person/';
     angular.module('app')
    .controller('personController', personController);

     personController.$inject = ['dataService'];

     function personController(dataService) {

         var vm = this;   // se declara la variable para no sobrecargar 
         vm.title = 'Person Controller';
         vm.personList = [];
         vm.person; // es un default

         vm.getPersonDetail = getPersonDetail;
        

         init();


         function init() {

             loadData();
         }


         function loadData() {

             vm.personList = [];
          
             var url = apiUrl + 'list/1/15';
             dataService.getData(url)
                 .then(function (result) {
                     vm.personList = result.data;


                 },

             function (error) {

                 console.log(error);

             });

         }

         function getPersonDetail(id) {

            
             dataService.getData(apiUrl + id)
                 .then(function (result) {
                     vm.person = result.data;


                 },

             function (error) {
                 vm.person = null;
                 console.log(error);

             });

         }

    }


})();