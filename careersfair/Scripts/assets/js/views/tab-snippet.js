define([
    "jquery", "underscore", "backbone", "models/snippet", "views/snippet", "helper/pubsub"
], function(
    $, _, Backbone, SnippetModel, SnippetView, PubSub
) {
    return SnippetView.extend({
        events: {
            "click": "preventPropagation",
            "mousedown": "pointerDownHandler"
        },
        pointerDownHandler: function(pointerEvent) {
            pointerEvent.stopPropagation();
            pointerEvent.preventDefault();
            //hide all popovers
            $(".popover").hide();
            PubSub.trigger("downAdd", new SnippetModel($.extend(true, {}, this.model.attributes)));
        },
        preventPropagation: function(pointerEvent) {
            pointerEvent.stopPropagation();
            pointerEvent.preventDefault();
        }
    });
});
