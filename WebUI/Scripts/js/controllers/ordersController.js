angular.module("yapp").controller("ordersController", ["$scope", "ordersService", function ($scope, ordersService) {

    $scope.orders = [];
    $scope.getAllOrdersUrl = "";
    $scope.hotels = [];
    $scope.users = [];
    $scope.addOrder = {};
    $scope.editOrder = {};
    $scope.range = function (count) {
        var ratings = [];
        for (var i = 0; i < count; i++) {
            ratings.push(i);
        }
        return ratings;
    }
    $scope.setEdit = function (order) {
        $scope.editOrder = angular.copy(order);
        $("#editModal").modal("show");
    };

    $scope.getAllOrders = function () {
        ordersService.getAllOrders($scope.getAllOrdersUrl)
            .success(function (data) {
                $scope.orders = data;
            })
            .error(function (data) {

            });
    };

    $scope.deleteOrder = function (url, order) {
        ordersService.deleteOrder(url, order.Id)
            .success(function (data) {
                $scope.orders.splice($scope.orders.indexOf(order), 1);
            })
            .error(function (data) {

            });
    };

  

    $scope.getAllHotels = function (url) {
        ordersService.getHotels(url)
            .success(function (data) {
                $scope.hotels = data;
                $scope.addOrder.Hotel = $scope.hotels[0];
                $scope.addOrder.Room =  $scope.addOrder.Hotel.Rooms[0];
            }).error(function () {

            });
    };

    $scope.addOrder = function (url) {
        $scope.order = {};
        $scope.order.UserName = $scope.addOrder.User.UserName;
        $scope.order.HotelName = $scope.addOrder.Hotel.Name;    
        $scope.order.RoomNumber = $scope.addOrder.Room.Number;
        $scope.order.ArrivalDate = $scope.addOrder.ArrivalDate;
        $scope.order.LeaveDate = $scope.addOrder.LeaveDate;
        $scope.order.TotalPrice = $scope.addOrder.TotalPrice;   
        ordersService.addOrder(url,$scope.order) 
            .success(function (data) {
                $("#addModal").modal("hide");
                $scope.orders = data;
            }).error(function () {
            $("#addModal").modal("hide");
                $("#Error").show();   
                window.setTimeout(function () {
                    $("#Error").fadeTo(500, 0).slideUp(500, function () {
                        $(this).remove();
                    });
                }, 3000);
            });
    }

    $scope.saveEditOrder = function (url) { 
     
        ordersService.editOrder(url, $scope.editOrder)
            .success(function (data) {
                $("#editModal").modal("hide");  
                $scope.orders = data;
            }).error(function () {
                $("#editModal").modal("hide");  
                $("#Error").show();
                window.setTimeout(function () {
                    $("#Error").fadeTo(500, 0).slideUp(500, function () {
                        $(this).remove();
                    });
                }, 3000);
            });
    }

    $scope.getUsers = function (url) {
        ordersService.getUsers(url)
            .success(function (data) {
                $scope.users = data;
                $scope.addOrder.User = $scope.users[0];    
            }).error(function () {

            });
    };

    $scope.init = function (url,hotelUrl,userUrl) { 
        $scope.getAllOrdersUrl = url;
        $scope.getAllOrders();
        $scope.getAllHotels(hotelUrl);
        $scope.getUsers(userUrl);
    };

}]);