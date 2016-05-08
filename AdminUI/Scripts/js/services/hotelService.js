"use strict";
angular.module("yapp").factory("hotelService", ["$http", function ($http) {

    var hotelService = {};

    hotelService.getHotels = function (url) {
        return $http({
            url: url,
            method: "GET"
        });
    };
    hotelService.addHotel = function (url, data) {
        return $http({
            url: url,
            method: "POST",
            params: {
                hotelDto: data
            }
        });
    };

    hotelService.deleteHotel = function (url, id) {
        return $http({
            url: url,
            method: "DELETE",
            params: {
                id: id
            }
        });
    };


    return hotelService;
}]);