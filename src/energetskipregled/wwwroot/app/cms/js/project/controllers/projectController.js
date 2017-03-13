(function () {
	'use strict';
	angular
	.module('content')
	.controller('projectController', projectController)

	projectController.$inject = ['$scope', 'dataservice', '$log', '$location', '$mdDialog', '$mdSimpleToast', '$rootScope'];
	function projectController($scope, dataservice, $log, $location, $mdDialog, $mdSimpleToast, $rootScope) {
		var vm = this;
		vm.projects = [];
		vm.project = {}
		$rootScope.headerTitle = '';

		var init = function () {
			dataservice.project.getList().then(function (data) {

				if (data) {
					vm.porjects = data;
					vm.project = data[0];
				}
			});
		};

		init();

		vm.save = function (ev) {
			ev.stopPropagation();
			if (vm.project.name) {
				dataservice.project.create(vm.project).then(function (data) {
					$mdSimpleToast.show('Projekat je snimljen!');
					init();
				}, function (error) {
					$mdSimpleToast.show('Došlo je do greške!');
				});
			}
		}
	}
})();