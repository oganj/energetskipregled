(function () {
	'use strict';

	config.$inject = ['$locationProvider', '$mdThemingProvider', '$httpProvider'];

	var app = angular.module('login', ['common']).config(config);

	function config($locationProvider, $mdThemingProvider, $httpProvider) {
		$mdThemingProvider.theme('default')
			//.primaryPalette('teal')
			//.accentPalette('green')
			//.warnPalette('deep-orange')
			//.dark();
			.primaryPalette('light-green')
			.accentPalette('green')
			.warnPalette('red');

		// Enable browser color
		$mdThemingProvider.enableBrowserColor({
			theme: 'default', // Default is 'default'
			palette: 'accent' // Default is 'primary', any basic material palette and extended palettes are available
		});

		$locationProvider
			.html5Mode(false);
	}

	app.run(['$q', '$rootScope',
		function ($q, $rootScope) {

		}]);
})();