(function () {
	'use strict';

	var serviceId = 'dataservice.materialCategory';

	angular.module('common').factory(serviceId, ['$resource', 'routeResolver', MaterialCategoryService]);

	function MaterialCategoryService($resource, routeResolver) {
		var getList = $resource(routeResolver.getApiUrl('MaterialCategory/ListAll/'));
		//var get = $resource(routeResolver.getApiUrl('Project/Get/'));
		//var create = $resource(routeResolver.getApiUrl('Project/Create/'));
		//var update = $resource(routeResolver.getApiUrl('Project/Update/'));
		//var remove = $resource(routeResolver.getApiUrl('Project/Delete/'));

		var service = {
			getList: getListFunc,
			//get: getFunc,
			//create: createFunc,
			//update: updateFunc,
			//remove: removeFunc
		};

		return service;

		function getListFunc() {
			return getList.query().$promise;
		}

		//function getFunc(id, projectId) {
		//	return get.get({ id: id, projectId: projectId }).$promise;
		//}

		//function createFunc(project) {
		//	return create.save(project).$promise;
		//}

		//function updateFunc(project) {
		//	return update.save(project).$promise;
		//}

		//function removeFunc(ids) {
		//	return remove.delete({ ids: ids }).$promise;
		//}
	}
})();