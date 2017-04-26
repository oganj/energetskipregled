(function () {
	'use strict';

	var serviceId = 'dataservice.nontransparentConstruction';

	angular.module('common').factory(serviceId, ['$resource', 'routeResolver', NonConstructionService]);

	function NonConstructionService($resource, routeResolver) {
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

		function getListFunc() {
			return getList.query().$promise;
		}

		function getFunc(id, projectId) {
			return get.get({ id: id, projectId: projectId }).$promise;
		}

		function createFunc(cons) {
			return create.save({
				id: cons.id,
				name: cons.name,
				code: cons.code,
				MaterialsUsed: cons.materialsUsed,
				createdAt: cons.createdAt,
				lastModifiedAt: cons.lastModifiedAt
			}).$promise;
		}

		function updateFunc(cons) {
			return update.save({
				id: cons.id,
				name: cons.name,
				code: cons.code,
				MaterialsUsed: cons.materialsUsed,
				createdAt: cons.createdAt,
				lastModifiedAt: cons.lastModifiedAt
			}).$promise;
		}

		function removeFunc(ids) {
			return remove.delete({ ids: ids }).$promise;
		}
	}
})();