(function () {
	'use strict';

	angular
		.module('content')
		.controller('editNontransparentConstructionController', editNontransparentConstructionController);

	editNontransparentConstructionController.$inject = ['$location', '$mdDialog', 'dataservice', 'materialsUsed', 'materialThicknessId', 'categories', 'materials'];

	function editNontransparentConstructionController($location, $mdDialog, dataservice, materialsUsed, materialThicknessId, categories, materials) {
		/* jshint validthis:true */
		var vm = this;

		vm.title = materialThicknessId ? 'Izmeni' : 'Dodaj';
		vm.materialThickness = materialThicknessId ?
			_.find(materialsUsed, function (mu) { return mu.id == materialThicknessId }) :
			{
				material: {
					categoryId: '',
				},
				materialId: '',
				thickness:''
			};

		vm.categories = categories;
		vm.materials = materials;

		init();

		function init() {
			//if (vm.id) {
			//	dataservice.player.get(vm.id)
			//		.then(function (player) {
			//			vm.player = player;
			//		});
			//}
		}

		vm.cancel = function () {
			$mdDialog.cancel();
		};

		vm.save = function () {
			if (!vm.materialThicknessId) {
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
