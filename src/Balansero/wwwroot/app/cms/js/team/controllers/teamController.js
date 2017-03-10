(function () {
	'use strict';
	angular
	.module('content')
	.controller('teamController', teamController)

	teamController.$inject = ['$scope', 'dataservice', '$log', '$location', '$rootScope', '$interpolate', '$mdDialog'];
	function teamController($scope, dataservice, $log, $location, $rootScope, $interpolate, $mdDialog) {
		var vm = this;
		vm.playersSelected = 0;
		vm.headerTitle = 'Players selected: {{playersSelected}}';
		vm.players = [];
		$rootScope.headerTitle = $interpolate(vm.headerTitle)(this);

		var init = function () {
			dataservice.player.getList().then(function (data) {
				vm.players = data;
			});
		};
		init();

		vm.select = function (player) {
			player.selected = !player.selected;

			player.selected ? vm.playersSelected++ : vm.playersSelected--;
			$rootScope.headerTitle = $interpolate(vm.headerTitle)(this);
		};

		vm.toggle = function (ev, player) {
			ev.stopPropagation();

			player.selected ? vm.playersSelected-- : vm.playersSelected++;
			$rootScope.headerTitle = $interpolate(vm.headerTitle)(this);
		};

		vm.showTeams = function (ev) {
			if (vm.playersSelected % 2 == 0 && vm.playersSelected != 0) {
				var players = _.filter(vm.players, function (pl) {
					return pl.selected;
				});

				$mdDialog.show({
					controller: 'balancedTeamsController as vm',
					templateUrl: 'app/cms/views/team/balancedTeams.html?v=3',
					parent: angular.element(document.body),
					targetEvent: ev,
					locals: {
						players: players
					},
					clickOutsideToClose: true,
					fullscreen: true // Only for -xs, -sm breakpoints.
				})
				.then(function () {
					
				}, function () {
				});
			} else if (vm.playersSelected != 0) {
				$mdDialog.show(
					$mdDialog.alert()
						.parent(angular.element(document.body))
						.clickOutsideToClose(true)
						.title('Even number')
						.textContent('Please select even number of players.')
						.ok('Got it!')
						.targetEvent(ev)
				);
			} else {
				$mdDialog.show(
					$mdDialog.alert()
						.parent(angular.element(document.body))
						.clickOutsideToClose(true)
						.title('Select players')
						.textContent('Please select even number of players.')
						.ok('Got it!')
						.targetEvent(ev)
				);
			}
		};
	}
})();