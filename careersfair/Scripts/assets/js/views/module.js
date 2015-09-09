define(["jquery", "underscore", "backbone"],
    function($, _, Backbone) {
    var ViewOrig = Backbone.View;
    Backbone.View = function(options) {
        ViewOrig.apply(this, arguments);
        this.options = options;
    };
    Backbone.View.prototype = ViewOrig.prototype;
    Backbone.View.extend = ViewOrig.extend;
    return Backbone;
});
