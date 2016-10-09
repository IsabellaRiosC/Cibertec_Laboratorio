(function () {
    'use strict';
    angular.module('app')
    .directive('modalPopup', modalPopup);

    function modalPopup() {
        return {
            templateUrl: 'app/components/modal-popup/modal-popup.html',
            restrict: 'E',
            transclude: true,
            scope: {
                title: '@',
                buttonTitle: '@',
                saveFunction: '=',
                readOnly: '=',
                isDelete:'='
            }
        };
    }
})();