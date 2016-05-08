"use strict";
angular.module("yapp").factory("authService", ["$http", function ($http) {
   
    var authServiceFactory = {};

    var _login = function (loginData) {
        var deferred = $q.defer();
        $http({
            url: "/account/login",
            method: "POST",
            params: {
                username: loginData.userName,
                password: loginData.password
            }
        }).success(function (response) {
            if (response === "error") {
                $("#errorAlert").modal("show");
                deferred.reject("error");   
            } 
        }).error(function (err, status) {
            deferred.reject(err);
        });
        return deferred.promise;

    };

    authServiceFactory.login = _login;

    return authServiceFactory;
}]);