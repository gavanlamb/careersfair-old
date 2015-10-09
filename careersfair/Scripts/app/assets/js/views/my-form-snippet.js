define(["jquery", "underscore", "backbone", "views/snippet", "views/temp-snippet", "helper/pubsub"],
    function($, _, Backbone, SnippetView, TempSnippetView, PubSub) {
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
                $("#formbuilder #target .component").on("mouseup", function(e) {
                    $(".popover").remove();
                    that.$el.popover("show");
                    $(".popover #save").on("click", that.saveHandler(that));
                    $(".popover #cancel").on("click", that.cancelHandler(that));
                    $("body").off("mousemove");
                    $("html, body").scrollTop($(".popover").offset().top - ($(this).height() + 10));
                });
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
                                boundContext.model.setField(name, _.escape($e.val()));
                                break;
                            case "textarea":
                                boundContext.model.setField(name, _.escape($e.val()));
                                break;
                            case "textarea-split":
                                boundContext.model.setField(name, _.chain(_.escape($e.val()).split("\n")).map(function(t) {
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
