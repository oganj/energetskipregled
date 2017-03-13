(function () {
	'use strict';
	angular
	.module('content')
	.controller('menuController', menuController);

	menuController.$inject = ['$scope', '$rootScope', 'dataservice', '$mdDialog', '$mdSidenav'];
	function menuController($scope, $rootScope, dataservice, $mdDialog, $mdSidenav) {
		var vm = this;
		$rootScope.user = {
			username: "User",
			id: ""
		};

		vm.username = "User";

		var init = function () {
			vm.getUsername();
			vm.getUserId();
		};

		vm.getUsername = function () {
			dataservice.user.getUsername().then(function (data) {
				vm.username = data.list[0];
				$rootScope.user.username = vm.username;
			});
		};

		vm.getUserId = function () {
			dataservice.user.getId().then(function (data) {
				$rootScope.user.id = data.list[0];
			});
		};

		vm.toggleSideNav = function () {
			$mdSidenav('left').toggle();
		};

		vm.query = {
			pageSize: 10,
			page: 1,
			order: 'name',
			filter: ''
		};

		init();
	}
})();