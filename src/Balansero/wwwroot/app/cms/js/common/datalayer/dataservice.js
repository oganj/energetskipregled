(function () {
	'use strict';

	var serviceId = 'dataservice';

	angular.module('common').factory(serviceId,
	[
		'dataservice.player',
		'dataservice.user',
		dataservice
	]);

	function dataservice(player, user) {
		// Define the functions and properties to reveal.
		var service = {
			player: player,
			user: user
		};

		return service;
	}
})();