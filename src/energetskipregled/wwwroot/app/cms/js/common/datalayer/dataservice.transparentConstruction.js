(function () {
	'use strict';

	var serviceId = 'dataservice.transparentConstruction';

	angular.module('common').factory(serviceId, ['$resource', 'routeResolver', ConstructionService]);

	function ConstructionService($resource, routeResolver) {
		var getList = $resource(routeResolver.getApiUrl('NonTrasparentBuildingElemet/ListAll/'));
		var get = $resource(routeResolver.getApiUrl('NonTrasparentBuildingElemet/Get/'));
		var create = $resource(routeResolver.getApiUrl('NonTrasparentBuildingElemet/Create/'));
		var update = $resource(routeResolver.getApiUrl('NonTrasparentBuildingElemet/Update/'));
		var remove = $resource(routeResolver.getApiUrl('NonTrasparentBuildingElemet/Delete/'));

		var service = {
			getList: getListFunc,
			get: getFunc,
			create: createFunc,
			update: updateFunc,
			remove: removeFunc
		};

		return service;

		function getListFunc(projectId) {
			return get.get({ projectId: projectId }).$promise;
		}

		function getFunc(id, projectId) {
			return get.get({ id: id, projectId: projectId }).$promise;
		}

		function createFunc(Construction) {
			return create.save(Construction).$promise;
		}

		function updateFunc(Construction) {
			return update.save(Construction).$promise;
		}

		function removeFunc(ids) {
			return remove.delete({ ids: ids }).$promise;
		}
	}
})();