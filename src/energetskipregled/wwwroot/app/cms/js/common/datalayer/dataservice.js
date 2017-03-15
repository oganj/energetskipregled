(function () {
	'use strict';

	var serviceId = 'dataservice';

	angular.module('common').factory(serviceId,
	[
		'dataservice.player',
		'dataservice.user',
		'dataservice.construction',
		'dataservice.project',
		'dataservice.transparentConstruction',
		'dataservice.nontransparentConstruction',
		'dataservice.materialCategory',
		'dataservice.material',
		dataservice
	]);

	function dataservice(player, user, construction, project, transparentConstruction, nontransparentConstruction, materialCategory, material) {
		// Define the functions and properties to reveal.
		var service = {
			player: player,
			user: user,
			construction: construction,
			project: project,
			transparentConstruction: transparentConstruction,
			nontransparentConstruction: nontransparentConstruction,
			materialCategory: materialCategory,
			material: material
		};

		return service;
	}
})();