(function () {
	'use strict';
	angular
	.module('login')
	.controller('registerController', registerController)

	registerController.$inject = ['$scope', 'dataservice', '$mdDialog', '$mdToast', '$log'];
	function registerController($scope, dataservice, $mdDialog, $mdToast, $log) {
		var vm = this;

		vm.cancel = function () {
			$mdDialog.cancel();
		};

		$scope.showLoginDialog = function (ev) {
			showLoginDialog(ev);
		};

		function showLoginDialog(ev) {
			$mdDialog.show({
				controller: 'registerController as vm',
				templateUrl: 'login',
				parent: angular.element(document.body),
				targetEvent: ev,
				clickOutsideToClose: true,
				fullscreen: true // Only for -xs, -sm breakpoints.
			})
			.then(function () {
				$mdToast.show(
					$mdToast.simple()
						.textContent('Successfully signed in!')
						.position('bottom right')
				);
			}
			, function () {
				// Dialog cancelled.
			});
		}
	}
})();