(function () {
	'use strict';

	config.$inject = ['$routeProvider', '$locationProvider', '$mdThemingProvider', '$mdIconProvider','$httpProvider'];

	var app = angular.module('content', ['common']).config(config);

	function config($routeProvider, $locationProvider, $mdThemingProvider, $mdIconProvider, $httpProvider) {
		$routeProvider
			.when('/', { redirectTo: '/players' })
			.when('/players', {
				templateUrl: 'app/cms/views/player/index.html',
				controller: 'playerController as vm',
				pageTrack: '/players'
			})
			.when('/konstrukcije', {
				templateUrl: 'app/cms/views/construction/index.html',
				controller: 'constructionController as vm',
				pageTrack: '/konstrukcije'
			})
			.when('/teams', {
				templateUrl: 'app/cms/views/team/index.html?v=3',
				controller: 'teamController as vm',
				pageTrack: '/teams'
			})
			.otherwise({ redirectTo: '/' });

		$mdThemingProvider.theme('default')
			.primaryPalette('light-green')
			.accentPalette('green')
			.warnPalette('red');
			//.backgroundPalette('light-blue')
			//.dark();

		// Enable browser color
		$mdThemingProvider.enableBrowserColor({
			theme: 'default', // Default is 'default'
			palette: 'primary', // Default is 'primary', any basic material palette and extended palettes are available
			hue: '800' // Default is '800'
		});

		$mdIconProvider
			.icon('person', 'app/cms/css/material-icons/ic_person_black_24px.svg')
			.icon('add', 'app/cms/css/material-icons/ic_add_black_24px.svg');

		$locationProvider
			.html5Mode(false);
	}

	app.run(['$q', '$rootScope',
		function ($q, $rootScope) {

		}]);
})();