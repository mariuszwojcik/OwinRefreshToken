/// <reference path="../_includes.ts" />
var SampleApp;
(function (SampleApp) {
    var Controllers;
    (function (Controllers) {
        "use strict";
        var HomePageController = (function () {
            function HomePageController($scope) {
                this.$scope = $scope;
                this.info = "Hello World!";
            }
            HomePageController.$inject = ["$scope"];
            return HomePageController;
        })();
        Controllers.HomePageController = HomePageController;
    })(Controllers = SampleApp.Controllers || (SampleApp.Controllers = {}));
})(SampleApp || (SampleApp = {}));
//# sourceMappingURL=HomePageController.js.map