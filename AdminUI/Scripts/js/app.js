"use strict";
angular.module("yapp", [ "snap", "ngAnimate", "LocalStorageModule", "angular-loading-bar"])
 .directive('loading', ['$http', function ($http) {
     return {
         restrict: 'A',
         link: function (scope, elm, attrs) {
             scope.isLoading = function () {
                 return $http.pendingRequests.length > 0;
             };

             scope.$watch(scope.isLoading, function (v) {
                 if (v) {
                     elm.show();
                 } else {
                     elm.hide();
                 }
             });
         }
     };

 }]);
angular.module("yapp").config([
    '$httpProvider', function($httpProvider) {
        $httpProvider.defaults.useXDomain = true;
        delete $httpProvider.defaults.headers.common['X-Requested-With'];
    }
]);
angular.module("yapp").config(function ($httpProvider) {
    $httpProvider.interceptors.push("authInterceptorService");
});

angular.module("yapp").value(
    "$sanitize",
    function (html) {
        return (html);
    }
);
