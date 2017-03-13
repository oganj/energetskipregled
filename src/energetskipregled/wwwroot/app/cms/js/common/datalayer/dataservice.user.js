(function () {
	'use strict';

	var serviceId = 'dataservice.user';

	angular.module('common').factory(serviceId, ['$resource', 'routeResolver', userService]);

	function userService($resource, routeResolver) {
		var getUsername = $resource(routeResolver.getApiUrl('Account/GetUsername'));
		var getId = $resource(routeResolver.getApiUrl('Account/GetId'));

		var service = {
			getUsername: getFunc,
			getId: getIdFunc,
		};

		return service;

		function getFunc() {
			return getUsername.get().$promise;
		}

		function getIdFunc() {
			return getId.get().$promise;
		}
	}
})();