(function () {
	'use strict';
	angular
	.module('content')
	.controller('trnsparentConstructionController', trnsparentConstructionController)

	trnsparentConstructionController.$inject = ['$scope', 'dataservice', '$log', '$location', '$mdDialog', '$mdSimpleToast', '$rootScope'];
	function trnsparentConstructionController($scope, dataservice, $log, $location, $mdDialog, $mdSimpleToast, $rootScope) {
		var vm = this;
		vm.tbes = [];
		vm.tbeMaterials = [];
		vm.tbeHeatCorrectionFactors = [];
		vm.tbeFrames = [];

		var init = function () {
			dataservice.tbe.getList().then(function (data) {
				if (data) {
					vm.tbes = data;
					populateMaterials();
					populateHeatCorrectionFactor();
					populateFrames();
					//_.map(vm.constructions, function (construction) { tableValuesCalculate(construction) })
				}
			});

			dataservice.tbeMaterial.getList().then(function (data) {
				vm.tbeMaterials = data;
				populateMaterials()
			});

			dataservice.tbeFrame.getList().then(function (data) {
				vm.tbeFrames = data;
				populateFrames();
			});

			dataservice.tbeHeatCorrectionFactor.getList().then(function (data) {
				vm.tbeHeatCorrectionFactors = data;
				populateHeatCorrectionFactor();
			});			
		};

		var populateMaterials = function () {
			if (vm.tbes.length === 0 || vm.tbeMaterials.length === 0)
				return;

			_.each(vm.tbes, (tbe) => {
				tbe.tbeMaterial = _.find(vm.tbeMaterials, (material) => { return material.id === tbe.tbeMaterialId })
			});
		};

		var populateFrames = function () {
			if (vm.tbes.length === 0 || vm.tbeFrames.length === 0)
				return;

			_.each(vm.tbes, (tbe) => {
				tbe.tbeFrame = _.find(vm.tbeFrames, (tbeFrame) => { return tbeFrame.id === tbe.tbeFrameId })
			});
		};

		var populateHeatCorrectionFactor = function () {
			if (vm.tbes.length === 0 || vm.tbeHeatCorrectionFactors.length === 0)
				return;

			_.each(vm.tbes, (tbe) => {
				tbe.tbeHeatCorrectionFactor = _.find(vm.tbeHeatCorrectionFactors, (heatCorrectionFactor) => { return heatCorrectionFactor.id === tbe.tbeHeatCorrectionFactorId })
			});
		};

		init();

		vm.addOrUpdateMaterialThickness = function (ev, construction) {
			ev.stopPropagation();
			$mdDialog.show({
				controller: 'editNontransparentConstructionController as vm',
				templateUrl: 'app/cms/views/nonTransparentConstruction/edit.html?v=4',
				parent: angular.element(document.body),
				targetEvent: ev,
				locals: {
					materialsUsed: construction.materialsUsed,
					materialThickness: materialThickness,
					categories: vm.categories,
					materials: vm.materials,
				},
				clickOutsideToClose: true,
				fullscreen: true // Only for -xs, -sm breakpoints.
			})
			.then(function (answer) {
				if (answer) {
					$mdSimpleToast.show('Uspešno!');
					tableValuesCalculate(construction);
				} else {
					$mdSimpleToast.show('Došlo je do greške!');
				}
			}, function () {
			});
		}
	}
})();