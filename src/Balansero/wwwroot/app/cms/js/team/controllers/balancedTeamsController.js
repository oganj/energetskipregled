(function () {
	'use strict';

	angular
		.module('content')
		.controller('balancedTeamsController', balancedTeamsController);

	balancedTeamsController.$inject = ['$location', '$mdDialog', 'dataservice', 'players'];

	function balancedTeamsController($location, $mdDialog, dataservice, players) {
		/* jshint validthis:true */
		var vm = this;

		vm.cancel = function () {
			$mdDialog.cancel();
		};

		vm.rebalanceTeams = function () {
			var rebalancedPlayers = angular.copy(players);
			rebalancedPlayers = _.sortBy(players, 'skill');

			vm.team1 = [];
			vm.team2 = [];
			vm.team1OverallSkill = 0;
			vm.team2OverallSkill = 0;

			for (var i = 0; i < players.length; i = i + 2) {
				if (Math.random() >= 0.5) {
					vm.team1.push(rebalancedPlayers[i]);
					vm.team1OverallSkill += rebalancedPlayers[i].skill;

					vm.team2.push(rebalancedPlayers[i + 1]);
					vm.team2OverallSkill += rebalancedPlayers[i + 1].skill;
				} else {
					vm.team2.push(rebalancedPlayers[i]);
					vm.team2OverallSkill += rebalancedPlayers[i].skill;

					vm.team1.push(rebalancedPlayers[i + 1]);
					vm.team1OverallSkill += rebalancedPlayers[i + 1].skill;
				}
			}
		};

		function init() {
			vm.rebalanceTeams();
		}

		init();
	}
})();
