angular.module("yapp").controller("conditionController", ["$scope", "conditionService", function ($scope, conditionService) {

    $scope.hotels = [];
    $scope.gethotelsUrl = "";
    $scope.setHotelId = "";
    $scope.currentHotel = {};
    $scope.currentCondition = {};


    $scope.seeImages = function (condition) {
        $scope.currentCondition = angular.copy(condition);
        $("#imagesModal").modal("show");
    };

    $scope.editCondition = function (condition) {
        $scope.currentCondition = angular.copy(condition);
        $("#editModal").modal("show");  
    };


    $scope.saveEditCondition = function(url) {
        conditionService.saveEditCondition(url, $scope.currentCondition)    
            .success(function (data) {
                $("#editModal").modal("hide");  
                $scope.getHotels();
            })
            .error(function (data) {
                $("#editModal").modal("hide");  
                $("#ConditionExist").show();
                window.setTimeout(function () {
                    $("#ConditionExist").fadeTo(500, 0).slideUp(500, function () {
                        $(this).remove();
                    });
                }, 3000);   
            });
    };

    $scope.init = function (url) {
        $scope.gethotelsUrl = url;
        $scope.getHotels();
    };

    $scope.deleteCondition = function (url, condition) {
        conditionService.deleteCondition(url, condition.Id)
          .success(function (data) {
              $scope.currentHotel.Conditions.splice($scope.currentHotel.Conditions.indexOf(condition), 1);
          }).error(function () {

          });
    };

    $scope.removeImage = function (url, image) {
        conditionService.removeImage(url, $scope.currentCondition.Id, image)
            .success(function (data) {
                $scope.currentCondition.Images.splice($scope.currentCondition.Images.indexOf(image), 1);
                $scope.getHotels();
            })
            .error(function () {
            });
    };

    $scope.getHotels = function () {
        conditionService.getHotels($scope.gethotelsUrl)
          .success(function (data) {
              $scope.hotels = data;
              $scope.setHotelId = $scope.hotels[0].Id;
              $scope.currentHotel = $scope.hotels[0];
          }).error(function () {

          });
    };

}]);