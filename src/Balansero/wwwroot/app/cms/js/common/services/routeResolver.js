(function () {
	'use strict';

	angular.module('common').provider('routeResolver', [routeResolver]);

	function routeResolver() {
		function getApiUrl(relativePath) {
			return relativePath;
		}

		//this.getView = getView;
		this.getApiUrl = getApiUrl;

		this.$get = function () {
			return this;
		};
	}
})();