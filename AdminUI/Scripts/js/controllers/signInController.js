"use strict";
angular.module("yapp").controller("signInController", ["$scope", "$location", "authService",  function ($scope, $location, authService) {   

    $scope.loginData = {};

    $scope.message = "";

    $scope.login = function () {
        authService.login($scope.loginData);
    };

}]);