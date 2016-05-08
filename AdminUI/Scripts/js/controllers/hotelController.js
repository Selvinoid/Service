angular.module("yapp").controller("hotelController", ["$scope", "hotelService", function ($scope, hotelService) {
    $scope.hotels = [];
    $scope.hotel = {};

    $scope.init = function () {
        hotelService.getHotels(url)
            .success(function (data) {
                $scope.hotels = data;
            }).error(function () {

            });
    };
    $scope.deleteHotel = function (url) {
        hotelService.deleteHotel(url, hotel.Id) 
            .success(function (data) {
                $scope.hotels.splice($scope.hotels.indexOf(hotel), 1);  
            }).error(function () {

            });
    };
    
    $scope.addHotel = function (url) {
        hotelService.addHotel(url, $scope.hotel)
            .success(function () {
                $scope.init();
            }).error(function () {

            });
    };
}]);