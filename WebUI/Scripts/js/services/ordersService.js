"use strict";
angular.module("yapp").factory("ordersService", ["$http", function ($http) {

    var ordersService = {};

    ordersService.getAllOrders = function (url) {
        return $http({
            url: url,
            method: "GET"
        });
    };

     ordersService.getHotels = function (url) {
        return $http({
            url: url,
            method: "GET"
        });
     };

     ordersService.addOrder = function (url, data) {
        return $http.post(url, { order: data });    
    };

      ordersService.editOrder = function (url, data) {
        return $http.post(url, { order: data });    
    };
     

     ordersService.getUsers = function (url) {
         return $http({
             url: url,
             method: "GET"
         });
     };

    ordersService.deleteOrder = function (url, id) {
        return $http({
            url: url,
            method: "GET",
            params: {
                id: id
            }
        });
    };
     
    return ordersService;
}]);