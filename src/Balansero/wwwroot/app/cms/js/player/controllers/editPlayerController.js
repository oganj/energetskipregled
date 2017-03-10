(function () {
	'use strict';

	angular
		.module('content')
		.controller('editPlayerController', editPlayerController);

	editPlayerController.$inject = ['$location', '$mdDialog', 'dataservice', 'playerId'];

	function editPlayerController($location, $mdDialog, dataservice, playerId) {
		/* jshint validthis:true */
		var vm = this;
		vm.id = playerId;
		vm.player = {
			name: '',
			skill: 1
		};

		vm.title = vm.id ? 'Edit' : 'Add';

		init();

		function init() {
			if (vm.id) {
				dataservice.player.get(vm.id)
					.then(function (player) {
						vm.player = player;
					});
			}
		}

		vm.cancel = function () {
			$mdDialog.cancel();
		};

		vm.save = function () {
			if (vm.player.id) {
				dataservice.player.update(vm.player).then(function (data) {
					$mdDialog.hide(true);
				}, function (error) {
					$mdDialog.hide(false);
				});
			} else {
				dataservice.player.create(vm.player).then(function (data) {
					$mdDialog.hide(true);
				}, function (error) {
					$mdDialog.hide(false);
				});
			}
		};
	}
})();
