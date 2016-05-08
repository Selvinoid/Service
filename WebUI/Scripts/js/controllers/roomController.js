angular.module("yapp").controller("roomController", ["$scope", "roomService", function ($scope, roomService) {
    $scope.rooms = [];
    $scope.getAllUrl = "";
    $scope.currentRoom = {};

    $scope.init = function(getUrl) {
        $scope.getAllUrl = getUrl;
        $scope.getAllRooms();
    };
 $scope.range = function (count) {
        var ratings = [];
        for (var i = 0; i < count; i++) {
            ratings.push(i);
        }
        return ratings;
    }
    $scope.seeRoomImage = function (room) { 
        $scope.currentRoom = angular.copy(room);    
        $("#imagesModal").modal("show");
    };

     $scope.editRoom = function (room) { 
        $scope.currentRoom = angular.copy(room);    
        $("#editRoomModal").modal("show");  
    };
    
     $scope.saveEditRoom = function (url) {
         roomService.saveEditRoom(url, $scope.currentRoom)
           .success(function (data) {
               $("#editRoomModal").modal("hide");   
               $scope.getAllRooms();
           }).error(function () {
               $("#editRoomModal").modal("hide");   
               $("#roomExistError").show();
               window.setTimeout(function () {
                   $("#roomExistError").fadeTo(500, 0).slideUp(500, function () {
                       $(this).remove();
                   });
               }, 3000);
           });
     };

    $scope.uploadImage = function (room) {  
        $scope.currentRoom = angular.copy(room);
    };
    
    $scope.removeImage = function (url,imageUrl) {    
        roomService.removeImage(url, $scope.currentRoom.Id, imageUrl)   
            .success(function (data) {
                 $("#imagesModal").modal("hide");
                $scope.getAllRooms();
            }).error(function () {

            });
    };
    
    $scope.deleteRoom = function (url, room) {
        roomService.deleteRoom(url, room.Id)
            .success(function (data) {
                $scope.rooms.splice($scope.rooms.indexOf(room), 1);
            }).error(function () {

            });
    };
    

    $scope.getAllRooms = function () {   
        roomService.getAllRooms($scope.getAllUrl)       
            .success(function (data) {
                $scope.rooms = data;
            }).error(function () {

            });
    };
}]);