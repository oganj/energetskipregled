(function () {
	'use strict';

	var serviceId = 'dataservice.player';

	angular.module('common').factory(serviceId, ['$resource', 'routeResolver', playerService]);

	function playerService($resource, routeResolver) {
		var getList = $resource(routeResolver.getApiUrl('Player/ListAll/'));
		var get = $resource(routeResolver.getApiUrl('Player/Get/'));
		var create = $resource(routeResolver.getApiUrl('Player/Create/'));
		var update = $resource(routeResolver.getApiUrl('Player/Update/'));
		var remove = $resource(routeResolver.getApiUrl('Player/Delete/'));

		var service = {
			getList: getListFunc,
			get: getFunc,
			create: createFunc,
			update: updateFunc,
			remove: removeFunc
		};

		return service;

		function getListFunc() {
			return getList.query().$promise;
		}

		function getFunc(id) {
			return get.get({ id: id }).$promise;
		}

		function createFunc(player) {
			return create.save(player).$promise;
		}

		function updateFunc(player) {
			return update.save(player).$promise;
		}

		function removeFunc(ids) {
			return remove.delete({ ids: ids }).$promise;
		}
	}
})();