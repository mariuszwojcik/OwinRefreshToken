/// <reference path="_includes.ts" />

module SampleApp {
    "use strict";

    var sampleApp = angular.module("sampleApp", ["ngRoute"]);

    sampleApp.controller("HomePageController", SampleApp.Controllers.HomePageController);


    sampleApp.config(($routeProvider: ng.route.IRouteProvider) => {
        $routeProvider
            .when("/", { templateUrl: "Views/homePage.html", controller: "HomePageController as ctrl"})
            .otherwise({ redirectTo: "Views/homePage.html" });
    });
}