"use strict";
angular.module("yapp").factory("roomService", ["$http", function ($http) {

    var roomService = {};

    roomService.getAllRooms = function (url) {
        return $http({
            url: url,
            method: "GET"
        });
    };

    roomService.removeImage = function (url, id, imageUrl) {
        return $http({
            url: url,
            method: "POST",
            params: {   
                id: id,
                url: imageUrl
            }
        });
    };
   
     roomService.saveEditRoom = function (url,data) {
         return $http.post(url, { room: data });
    };
    
    roomService.deleteRoom = function (url, id) {   
        return $http({
            url: url,
            method: "GET",
            params: {
                id: id
            }
        });
    };

    return roomService;
}]);