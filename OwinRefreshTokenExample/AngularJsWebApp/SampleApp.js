/// <reference path="_includes.ts" />
var SampleApp;
(function (SampleApp) {
    "use strict";
    var sampleApp = angular.module("sampleApp", ["ngRoute"]);
    sampleApp.controller("HomePageController", SampleApp.Controllers.HomePageController);
    sampleApp.config(function ($routeProvider) {
        $routeProvider.when("/", { templateUrl: "Views/homePage.html", controller: "HomePageController as ctrl" }).otherwise({ redirectTo: "Views/homePage.html" });
    });
})(SampleApp || (SampleApp = {}));
//# sourceMappingURL=SampleApp.js.map