"use strict";
angular.module("yapp").factory("conditionService", ["$http", function ($http) {

    var conditionService = {};

    conditionService.getHotels = function (url) {
        return $http({
            url: url,
            method: "GET"
        });
    };

     conditionService.deleteCondition = function (url, id) { 
        return $http({
            url: url,
            method: "GET",
            params: {
                id: id
            }
        });
    };
     conditionService.saveEditCondition = function (url, data) {    
        return $http.post(url, { conditionDto: data }); 
    };
     
     conditionService.removeImage = function (url, id, data) {
         return $http.post(url, { image: data, id: id });
     };
    return conditionService;
}]);