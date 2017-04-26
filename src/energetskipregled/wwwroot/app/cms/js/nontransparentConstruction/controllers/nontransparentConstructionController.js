(function () {
	'use strict';
	angular
	.module('content')
	.controller('nontrnsparentConstructionController', nontrnsparentConstructionController)

	nontrnsparentConstructionController.$inject = ['$scope', 'dataservice', '$log', '$location', '$mdDialog', '$mdSimpleToast', '$rootScope'];
	function nontrnsparentConstructionController($scope, dataservice, $log, $location, $mdDialog, $mdSimpleToast, $rootScope) {
		var vm = this;
		vm.constructions = [];
		vm.categories = [];
		vm.materials = [];
		vm.constructionFixValues = {
			tetaj1Max: 20.00,
			tetaj1Min: -5.00,
			tetaj2Max: 20.00,
			tetaj2Min: -15.00,
			rjMax: 0.13,
			rjMin: 0.4,
			rmMax: 0.13,
			fii: 0.55,//Vlažnost  ϕi
		}

		var init = function () {
			dataservice.nontransparentConstruction.getList().then(function (data) {
				if (data) {
					vm.constructions = data;
					_.map(vm.constructions, function (construction) { tableValuesCalculate(construction) })
				}
			});

			dataservice.materialCategory.getList().then(function (data) {
				vm.categories = data;
			});

			dataservice.material.getList().then(function (data) {
				vm.materials = data;
			});
		};

		var calculate = function (paramName, construction, index){
			switch (paramName) {
				
				case 'U':
					return 1 / construction.rm[index];
				case 'tetaj1': //O5-((O5-$O$12)/$L$12)*L6
					return construction.tetaj1[index - 1] - ((construction.tetaj1[index - 1] - vm.constructionFixValues.tetaj1Min) / construction.rj[3 + construction.materialsUsed.length]) * construction.rj[index];
				case 'tetaj2': //P5-((P5-$P$12)/$L$12)*L6
					return construction.tetaj2[index - 1] - ((construction.tetaj2[index - 1] - vm.constructionFixValues.tetaj2Min) / construction.rj[3 + construction.materialsUsed.length]) * construction.rj[index];
				case 'p1':
					if(construction.tetaj2[index] > 0) {
						return 0.6107 * Math.pow((1 + construction.tetaj1[index] / 109.8), 8.02);
					}
					else {
						return 0.6107 * Math.pow((1 + construction.tetaj1[index] / 149), 12.03);
					}
					
				case 'p2'://=Q6*C15(fii)
					return construction.p1[index] * vm.constructionFixValues.fii;
				default:
					return 0;
			}
		}

		var tableValuesCalculate = function (construction) {
			//row 0 
			construction.tetaj1 = []; construction.tetaj1[0] = vm.constructionFixValues.tetaj1Max;
			construction.tetaj2 = []; construction.tetaj2[0] = vm.constructionFixValues.tetaj2Max;
			construction.p1 = []; construction.p1[0] = calculate('p1', construction, 0);
			construction.p2 = [];
			construction.sd = [];

			//row 1
			construction.rj = []; construction.rj[1] = vm.constructionFixValues.rjMax;
			construction.rm = []; construction.rm[1] = vm.constructionFixValues.rmMax;
			construction.u = []; construction.u[1] = calculate('U', construction, 1);
			
			//First Calculate rj,rm, u
			for (var i = 0; i < construction.materialsUsed.length; i++) {
				construction.rj[2 + i] = construction.materialsUsed[i].thickness / (construction.materialsUsed[i].material.heatConduction * 100);
				construction.rm[2 + i] = construction.rj[2 + i] + construction.rm[1 + i];
				construction.u[2 + i] = calculate('U', construction, 2 + i);
				construction.sd[2 + i] = construction.materialsUsed[i].thickness * construction.materialsUsed[i].material.relativeWaterVaporDiffusionCoefficient / 100;
			}
			//end rows
			construction.rj[2 + construction.materialsUsed.length] = vm.constructionFixValues.rjMin;
			construction.rj[3 + construction.materialsUsed.length] = construction.rj.reduce((a, b) => a + b, 0);//sum
			
			// row 1 and on
			construction.tetaj1[1] = calculate('tetaj1', construction, 1);//dependent on rj-end
			construction.tetaj2[1] = calculate('tetaj2', construction, 1);//dependent on rj-end
			construction.p1[1] = calculate('p1', construction, 1);//dependent on tetaj1
			construction.p2[1] = calculate('p2', construction, 1);//dependent on p1
			for (var i = 0; i < construction.materialsUsed.length; i++) {
				construction.tetaj1[2 + i] = calculate('tetaj1', construction, 2 + i);
				construction.tetaj2[2 + i] = calculate('tetaj2', construction, 2 + i);
				construction.p1[2 + i] = calculate('p1', construction, 2 + i);
				construction.p2[2 + i] = calculate('p2', construction, 2 + i);
			}
			var index = 2 + construction.materialsUsed.length;
			construction.tetaj1[index] = calculate('tetaj1', construction, index);
			construction.tetaj2[index] = calculate('tetaj2', construction, index);
			construction.p1[index] = calculate('p1', construction, index);
			construction.p2[index] = calculate('p2', construction, index);

			//end rows
			index = 3 + construction.materialsUsed.length;
			construction.tetaj1[index] = vm.constructionFixValues.tetaj1Min;
			construction.tetaj2[index] = vm.constructionFixValues.tetaj2Min;
			construction.p1[index] = calculate('p1', construction, index);
			construction.sd[index] = construction.sd.reduce((a, b) => a + b, 0);//sum
		}



		

		init();

		vm.addNewConstruction = function (ev) {
			ev.stopPropagation();
			var construction = {
				id:0,
				name: 'Spoljni zid',
				code: 'SZ01',
				materialsUsed: [],

				//InsideTempCondensationCheck: 0,
				//InsideTempTemperatureCheck: 0,
				//Rj: 0,
				//Rm: 0,
			};
			tableValuesCalculate(construction);
			vm.constructions.push(construction);
		};

		vm.addNewMaterial = function (ev, construction) {
			ev.stopPropagation();
			$mdDialog.show({
				controller: 'editNontransparentConstructionController as vm',
				templateUrl: 'app/cms/views/nonTransparentConstruction/edit.html?v=4',
				parent: angular.element(document.body),
				targetEvent: ev,
				locals: {
					materialsUsed: construction.materialsUsed,
					materialThicknessId: null,
					categories: vm.categories,
					materials: vm.materials,
				},
				clickOutsideToClose: true,
				fullscreen: true // Only for -xs, -sm breakpoints.
			})
			.then(function (answer) {
				if (answer) {
					$mdSimpleToast.show('Uspešno dodat!');
					tableValuesCalculate(construction);
				} else {
					$mdSimpleToast.show('Došlo je do greške!');
				}
			}, function () {
			});
		}

		vm.saveConstruction = function (ev, construction) {
			ev.stopPropagation();
			if (construction.Id != 0) {
				dataservice.nontransparentConstruction.update(construction).then(function (data) {
					$mdSimpleToast.show('Uspešno snimljena konstrukcija!');
				});
			}
			else {
				dataservice.nontransparentConstruction.create(construction).then(function (data) {
					$mdSimpleToast.show('Uspešno snimljena konstrukcija!');
				});
			}
		};

		vm.removeConsruction = function (ev, construction) {
			ev.stopPropagation();
			// Appending dialog to document.body to cover sidenav in docs app
			var confirm = $mdDialog.confirm()
					.title('Obriši konstrukciju')
					.textContent('Da li si sugran da želiš da obrišeš konstrukciju?')
					.targetEvent(ev)
					.ok('Da')
					.cancel('Ne');

			$mdDialog.show(confirm).then(function (construction) {
				var index = vm.constructions.indexOf(construction);
				vm.constructions.splice(index, 1);
				var ids = []; ids.push(construction.id);

				dataservice.nontransparentConstruction.remove(ids).then(function (data) {
					$mdSimpleToast.show('Uspešno obrisana konstrukcija!');
				});
				//var ids = [];
				//ids.push(playerId);
				//dataservice.player.remove(ids).then(function (data) {
				//	$mdSimpleToast.show('Player removed!');
				//	init();
				//}, function (err) {
				//	$mdSimpleToast.show('Error occurred!');
				//});
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

		//var tableValuesInit = function (construction) {
		//	//Colons 
		//	construction.thicknes = [];
		//	construction.rj = [];
		//	construction.rm = [];
		//	construction.u = [];
		//	construction.tetaj1 = [];
		//	construction.tetaj2 = [];
		//	construction.p1 = [];
		//	construction.p2 = [];
		//	construction.sd = [];

		//	//FIX
		//	construction.fii = 0.55;//Vlažnost  ϕi

		//	//First calculations
		//	//row: 0
		//	construction.tetaj1[0] = 20.00;
		//	construction.tetaj2[0] = 20.00;
		//	construction.p1[0] = calculate('p1',construction,0);

		//	//REACALCULATE
		//	//row: 1
		//	construction.rj[1] = 0.13;
		//	construction.rm[1] = 0.13;
		//	construction.u[1] = calculate('U',construction, 1);
		//	//moved after other calculations
		//	construction.tetaj2[1] = 17.65;


		//	//row: end - 1
		//	construction.rj[2] = 0.04;

		//	//row: end
		//	construction.rj[3] = 1.94;//construction.rj.reduce((a, b) => a + b, 0);//sum
		//	construction.tetaj1[3] = -5.00;
		//	construction.tetaj2[3] = -15.00;

		//	//row: 1 AGAIN - because it's dependent on row-end
		//	construction.tetaj1[1] = calculate('tetaj1', construction, 1);//dependent on rj-end
		//	construction.tetaj2[1] = calculate('tetaj2', construction, 1);//dependent on rj-end
		//	construction.p1[1] = calculate('p1', construction, 1);//dependent on tetaj1
		//	construction.p2[1] = calculate('p2', construction, 1);//dependent on p1
		//};
	}
})();