define([
    "jquery", "underscore", "backbone",
    "views/snippet", "views/temp-snippet",
    "helper/pubsub"
], function(
    $, _, Backbone,
    SnippetView, TempSnippetView,
    PubSub
) {
    return SnippetView.extend({
        events: {
            "click": "preventPropagation",
            "mousedown": "mouseDownHandler",
            "mouseup": "mouseUpHandler"
        },
        mouseDownHandler: function(pointerEvent) {
            pointerEvent.stopPropagation();
            pointerEvent.preventDefault();
            var that = this;
            //popover
            $(".popover").remove();
            this.$el.popover("show");
            $(".popover #save").on("click", this.saveHandler(that));
            $(".popover #cancel").on("click", this.cancelHandler(that));
            $("body").on("mousemove", function(pointerMoveEvent) {
                if (Math.abs(pointerEvent.pageX - pointerMoveEvent.pageX) > 10 || Math.abs(pointerEvent.pageY - pointerMoveEvent.pageY) > 10) {
                    that.$el.popover('destroy');
                    PubSub.trigger("mySnippetDrag", pointerEvent, that.model);
                    that.mouseUpHandler();
                };
            });
        },
        mouseUpHandler: function(pointerEvent) {
            $("body").off("mousemove");
        },
        preventPropagation: function(e) {
            e.stopPropagation();
            e.preventDefault();
        },
        saveHandler: function(boundContext) {
            return function(pointerEvent) {
                pointerEvent.preventDefault();
                var fields = $(".popover .field");
                _.each(fields, function(e) {

                    var $e = $(e),
                        type = $e.attr("data-type"),
                        name = $e.attr("id");

                    switch (type) {
                        case "checkbox":
                            boundContext.model.setField(name, $e.is(":checked"));
                            break;
                        case "input":
                            boundContext.model.setField(name, $e.val());
                            if (name == "label") { //added to take the label value and output it to the id and name value
                                boundContext.model.setField("id", idFromForm($e.val()));
                            }
                            break;
                        case "textarea":
                            boundContext.model.setField(name, $e.val());
                            break;
                        case "textarea-split":
                            boundContext.model.setField(name, _.chain($e.val().split("\n")).map(function(t) {
                                return $.trim(t)
                            }).filter(function(t) {
                                return t.length > 0
                            }).value());
                            break;
                        case "select":
                            var valarr = _.map($e.find("option"), function(e) {
                                return {
                                    value: e.value,
                                    selected: e.selected,
                                    label: $(e).text()
                                };
                            });
                            boundContext.model.setField(name, valarr);
                            break;
                    }
                });
                boundContext.model.trigger("change");
                $(".popover").remove();
            }
        },
        cancelHandler: function(boundContext) {
            return function(pointerEvent) {
                pointerEvent.preventDefault();
                $(".popover").remove();
                boundContext.model.trigger("change");
            }
        }
    });
});

function idFromForm(e) {
    var a = e;
    a = a.replace(/^([^a-zA-Z])*/g, "");
    a = a.replace(/([^a-zA-Z0-9-:._])/g, "");
    if (a.length < 8) {
        var possible = "";
        if (a.length > 0) {
            a += "-";
        }
        for (var i = a.length; i < 8; i++) {
            if (i < 1) {
                possible = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            } else {
                possible = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            }
            a += possible.charAt(Math.floor(Math.random() * possible.length));
        }
    }
    return a;
}
