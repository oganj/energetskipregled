(function () {
	'use strict';

	angular
		.module('content')
		.controller('editNontransparentConstructionController', editNontransparentConstructionController)
		.filter('filterByMaterialCategory', filterByCategoryId);
	editNontransparentConstructionController.$inject = ['$location', '$mdDialog', 'dataservice', 'materialsUsed', 'materialThickness', 'categories', 'materials'];

	function editNontransparentConstructionController($location, $mdDialog, dataservice, materialsUsed, materialThickness, categories, materials) {
		/* jshint validthis:true */
		var vm = this;
		vm.title = '';
		vm.showDeleteButton = false;
		vm.materialThickness = {};
		vm.categories = categories;
		vm.materials = materials;

		init();

		function init() {
			if (materialThickness) {
				vm.title = 'Izmeni';
				vm.showDeleteButton = true;
				vm.materialThickness = _.find(materialsUsed, function (mu) { return mu.id == materialThickness.id });
				//vm.formScope.category.$setValidity("required", true);
				//vm.myForm.$setPristine();
				
			}
			else {
				vm.title = 'Dodaj';
				vm.showDeleteButton = false;
				vm.materialThickness = {
					id: 0,
					material: {
						categoryId: null,
					},
					materialId: null,
					thickness: null
				};
			}
		}

		vm.setFormScope = function (scope) {
			vm.formScope = scope;
		}

		vm.cancel = function () {
			$mdDialog.cancel();
		};

		vm.delete = function () {
			var index = materialsUsed.indexOf(vm.materialThickness)
			if (index > -1) {
				materialsUsed.splice(index, 1);
			}
			$mdDialog.hide(true);
		};

		vm.save = function () {
			if (vm.materialThickness.id === 0) {
				vm.materialThickness.material = _.find(vm.materials, function (elem) { return elem.id == vm.materialThickness.materialId });
				materialsUsed.push(vm.materialThickness);
			}
			$mdDialog.hide(true);
			//if (vm.player.id) {
			//	dataservice.player.update(vm.player).then(function (data) {
			//		$mdDialog.hide(true);
			//	}, function (error) {
			//		$mdDialog.hide(false);
			//	});
			//} else {
			//	dataservice.player.create(vm.player).then(function (data) {
			//		$mdDialog.hide(true);
			//	}, function (error) {
			//		$mdDialog.hide(false);
			//	});
			//}
		};
	}
})();

	function filterByCategoryId() {
		return function (input, materialCategoryId) {
			input = input || [];

			var out = '';

			out = _.filter(input,
				function (m) { return m.categoryId == materialCategoryId }
			);


			return out;
		};
	};
