(function () {
	'use strict';

	var serviceId = 'dataservice';

	angular.module('common').factory(serviceId,
	[
		'dataservice.player',
		'dataservice.user',
		'dataservice.construction',
		dataservice
	]);

	function dataservice(player, user, construction) {
		// Define the functions and properties to reveal.
		var service = {
			player: player,
			user: user,
			construction: construction
		};

		return service;
	}
})();