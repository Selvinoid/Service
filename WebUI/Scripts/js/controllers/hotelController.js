angular.module("yapp").controller("hotelController", ["$scope", "hotelService", function ($scope, hotelService) {
    $scope.hotels = [];
    $scope.hotel = {};
    $scope.hotel.Id = 0;
    $scope.hotel.Stars = 1;
    $scope.gethotelsUrl = "";
    $scope.currentHotel = {};
    $scope.hotelRoom = {};
    $scope.hotelRoom.PersonCount = 1;

    $scope.range = function (count) {
        var ratings = [];
        for (var i = 0; i < count; i++) {
            ratings.push(i);
        }
        return ratings;
    }

    $scope.seeHotelImage = function (hotel) {
        $scope.currentHotel = null;
        $scope.currentHotel = angular.copy(hotel);
        $("#imagesModal").modal("show");
    };
    $scope.addCondition = function (url, hotel) {
        $scope.uploadImage(url, hotel.Id);
        $scope.currentHotel = angular.copy(hotel);
        $("#conditionModal").modal("show");
    };

    $scope.addRoom = function (url, hotel) {
        $scope.uploadImage(url, hotel.Id);
        $scope.currentHotel = angular.copy(hotel);
        $("#addRoomModal").modal("show");
    };
    $scope.editHotel = function (hotel) {
        $scope.currentHotel = angular.copy(hotel);
        $("#editModal").modal("show");
    };


    $scope.changeHotel = function (url) {
        hotelService.changeHotel(url, $scope.currentHotel)
            .success(function () {
                $scope.init($scope.gethotelsUrl);
                $("#editModal").modal("hide");
            }).error(function () {
                $("#editModal").modal("hide");
                $("#hotelExistError").show();   
                window.setTimeout(function () {
                    $("#hotelExistError").fadeTo(500, 0).slideUp(500, function () {
                        $(this).remove();
                    });
                }, 3000);
            });
    };
    $scope.saveRoom = function (url) {
        hotelService.saveRoom(url, $scope.hotelRoom)
            .success(function (data) {
                $("#addRoomModal").modal("hide");
                $scope.init($scope.gethotelsUrl);
            })
            .error(function (data) {
                $("#addRoomModal").modal("hide");
                $("#roomExistError").show();
                window.setTimeout(function () {
                    $("#roomExistError").fadeTo(500, 0).slideUp(500, function () {
                        $(this).remove();
                    });
                }, 3000);
            });
    };

    $scope.removeImage = function (url, image) {
        hotelService.removeImage(url, $scope.currentHotel.Id, image)
            .success(function (data) {
                $scope.currentHotel.Images.splice($scope.currentHotel.Images.indexOf(image), 1);
                $scope.init($scope.gethotelsUrl);
            })
            .error(function () {
            });
    };

    $scope.uploadImage = function (url, hotelId) {
        hotelService.setHotelId(url, hotelId)
            .success(function (data) {
            })
            .error(function () {
            });
    };
    $scope.init = function (url) {
        $scope.gethotelsUrl = url;
        hotelService.getHotels($scope.gethotelsUrl)
            .success(function (data) {
                $scope.hotels = data;
            }).error(function () {

            });
    };
    $scope.deleteHotel = function (url, hotel) {
        hotelService.deleteHotel(url, hotel.Id)
            .success(function (data) {
                $scope.hotels.splice($scope.hotels.indexOf(hotel), 1);
            }).error(function () {

            });
    };

    $scope.addHotel = function (url) {
        hotelService.addHotel(url, $scope.hotel)
            .success(function () {
                $scope.init($scope.gethotelsUrl);
                $("#myModal").modal("hide");
            }).error(function () {
                $("#myModal").modal("hide");
                $("#hotelExistError").show();
                window.setTimeout(function () {
                    $("#hotelExistError").fadeTo(500, 0).slideUp(500, function () {
                        $(this).remove();
                    });
                }, 3000);
            });
    };
}]);