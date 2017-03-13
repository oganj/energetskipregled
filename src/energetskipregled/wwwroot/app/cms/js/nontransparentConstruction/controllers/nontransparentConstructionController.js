(function () {
	'use strict';
	angular
	.module('content')
	.controller('nontrnsparentConstructionController', nontrnsparentConstructionController)

	nontrnsparentConstructionController.$inject = ['$scope', 'dataservice', '$log', '$location', '$mdDialog', '$mdSimpleToast', '$rootScope'];
	function nontrnsparentConstructionController($scope, dataservice, $log, $location, $mdDialog, $mdSimpleToast, $rootScope) {
		var vm = this;
		vm.players = [];
		$rootScope.headerTitle = '';

		var init = function () {
			dataservice.player.getList().then(function (data) {
				vm.players = data;
			});
		};

		init();

		vm.addNewPlayer = function (ev) {
			$mdDialog.show({
				controller: 'editPlayerController as vm',
				templateUrl: 'app/cms/views/player/edit.html?v=4',
				parent: angular.element(document.body),
				targetEvent: ev,
				locals: {
					playerId: null
				},
				clickOutsideToClose: true,
				fullscreen: true // Only for -xs, -sm breakpoints.
			})
			.then(function (answer) {
				if (answer) {
					$mdSimpleToast.show('Player added!');
					init();
				} else {
					$mdSimpleToast.show('Error occurred!');
				}
			}, function () {
			});
		};

		vm.removePlayer = function (ev, playerId) {
			ev.stopPropagation();
			// Appending dialog to document.body to cover sidenav in docs app
			var confirm = $mdDialog.confirm()
					.title('Remove')
					.textContent('Are you sure you want to remove player?')
					.targetEvent(ev)
					.ok('Yes')
					.cancel('No');

			$mdDialog.show(confirm).then(function () {
				var ids = [];
				ids.push(playerId);
				dataservice.player.remove(ids).then(function (data) {
					$mdSimpleToast.show('Player removed!');
					init();
				}, function (err) {
					$mdSimpleToast.show('Error occurred!');
				});
			}, function () {
			});
		};

		vm.editPlayer = function (ev, playerId) {
			$mdDialog.show({
				controller: 'editPlayerController as vm',
				templateUrl: 'app/cms/views/player/edit.html?v=4',
				parent: angular.element(document.body),
				targetEvent: ev,
				locals: {
					playerId: playerId
				},
				clickOutsideToClose: true,
				fullscreen: true // Only for -xs, -sm breakpoints.
			})
			.then(function (answer) {
				if (answer) {
					$mdSimpleToast.show('Player updated!');
					init();
				} else {
					$mdSimpleToast.show('Error occurred!');
				}
			}, function () {
			});
		};
	}
})();