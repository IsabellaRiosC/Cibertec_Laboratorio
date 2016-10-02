
// restrict : 'E' -> Element
// restrict : 'A' -> Attribute
(function () {

    'use strict';
    angular.module('app')
    .directive('personCard', personCard);
    // dentro de una directiva la mayuscula se convierte en guion y letra en minuscula
    function personCard() {

        return {
            templateUrl: 'app/private/person/directives/person-card/person-card.html',
            restrict: 'E',
            transclude: true,
            scope: {

                personId: '@',  
                firstName: '@',                
                lastName: '@',
                modifiedDate: '@'


            }
        };

    }

})();

// scope tipo @ ->  One Way Binding
//   si pones el signo = ->  Two Way Binding