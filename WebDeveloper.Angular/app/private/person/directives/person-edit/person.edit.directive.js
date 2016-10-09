(function () {
	'use strict';
	angular.module('app')
    .directive('personEdit', personEdit)

	function personEdit() {
		return {
			templateUrl: 'app/private/person/directives/person-edit/person-edit.html',
			restrict: 'E',
			scope: {
				person: '=',
				readOnly: '='
			}
		};
	}

})();