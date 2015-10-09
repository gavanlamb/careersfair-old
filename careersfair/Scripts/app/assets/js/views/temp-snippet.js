define(["jquery", "views/snippet", "text!templates/app/temp.html", "helper/pubsub"],
    function($, SnippetView, _tempTemplate, PubSub) {
        return SnippetView.extend({
            initialize: function() {
                PubSub.on("newTempPostRender", this.postRender, this);
                this.constructor.__super__.initialize.call(this);
                this.tempTemplate = _.template(_tempTemplate);
            },
            className: "temp",
            render: function() {
                return this.$el.html(this.tempTemplate({
                    text: this.constructor.__super__.render.call(this).html()
                }));
            },
            postRender: function(pointerEvent) {
                this.tempForm = this.$el.find("form")[0];
                this.halfHeight = Math.floor(this.tempForm.clientHeight / 2);
                this.halfWidth = Math.floor(this.tempForm.clientWidth / 2);
                this.centerOnEvent(pointerEvent);
            },
            events: {
                "mousemove": "pointerMoveHandler",
                "mouseup": "pointerUpHandler"
            },
            centerOnEvent: function(pointerEvent) {
                this.tempForm.style.top = (pointerEvent.pageY - this.halfHeight) + "px";
                this.tempForm.style.left = (pointerEvent.pageX - this.halfWidth) + "px";
                PubSub.trigger("tempMove", pointerEvent);
            },
            pointerMoveHandler: function(pointerEvent) {
                pointerEvent.preventDefault();
                this.centerOnEvent(pointerEvent);
            },
            pointerUpHandler: function(pointerEvent) {
                pointerEvent.preventDefault();
                PubSub.trigger("tempDrop", pointerEvent, this.model);
                this.remove();
            }
        });
    });
