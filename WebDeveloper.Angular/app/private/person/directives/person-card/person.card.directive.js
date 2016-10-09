//restrict: 'E' -> Element
//restrict: 'A' -> Attribute
(function () {
    
    'use strict';
    angular.module('app')
        .directive('personCard', personCard);
// dentro de una directiva la mayuscula se convierte en guion y letra en minuscula
    function personCard() {
        return {
            templateUrl: 'app/private/person/directives/person-card/person-card.html',
            restrict: 'E',
            transclude: true,//Para enviar HTML
            scope: {
                personId: '@',
                firstName: '@',
                lastName: '@',
                modifiedDate: '@'
            }
        };
    }

 

})();