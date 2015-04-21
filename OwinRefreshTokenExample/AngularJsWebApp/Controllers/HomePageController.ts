/// <reference path="../_includes.ts" />

module SampleApp.Controllers {
    "use strict";

    export class HomePageController {
        static $inject = ["$scope"];

        constructor(private $scope: ng.IScope) {
            
        }

        info = "Hello World!";
    }
} 