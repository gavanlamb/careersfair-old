require.config({
    baseUrl: "assets/js/lib/",
    shim: {
        "*": {
            deps: ["module"],
            exports: "Backbone"
        },
        "backbone": {
            deps: ["underscore", "jquery"],
            exports: "Backbone"
        },
        "underscore": {
            exports: "_"
        },
        "bootstrap": {
            deps: ["jquery"],
            exports: "$.fn.popover"
        },
        "": {
            deps: ["jquery"],
            exports: "$.fn.popover"
        }
    },
    paths: {
        app: "..",
        collections: "../collections",
        data: "../data",
        models: "../models",
        helper: "../helper",
        templates: "../templates",
        router: "../router",
        views: "../views"
    }
});
require(["app/app"], function(app) {
    app.initialize();
});
