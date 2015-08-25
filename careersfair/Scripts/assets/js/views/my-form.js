define([
    "jquery", "underscore", "backbone", "views/temp-snippet", "helper/pubsub", "text!templates/app/renderform.html"
], function(
    $, _, Backbone, TempSnippetView, PubSub, _renderForm
) {
    return Backbone.View.extend({
        tagName: "fieldset",
        initialize: function() {
            this.collection.on("add", this.render, this);
            this.collection.on("remove", this.render, this);
            this.collection.on("change", this.render, this);
            PubSub.on("mySnippetDrag", this.handleSnippetDrag, this);
            PubSub.on("tempMove", this.handleTempMove, this);
            PubSub.on("tempDrop", this.handleTempDrop, this);
            PubSub.on("downAdd", this.handleDownAdd, this);
            this.$build = $("#build");
            this.renderForm = _.template(_renderForm);
            this.render();
        }

        ,
        render: function() {
            //Render Snippet Views
            this.$el.empty();
            var that = this;
            _.each(this.collection.renderAll(), function(snippet) {
                that.$el.append(snippet);
            });
            $("#render").val(that.renderForm({
                text: _.map(this.collection.renderAllClean(), function(e) {
                    return e.html()
                }).join("\n")
            }));
            this.$el.appendTo("#build form");
            this.delegateEvents();
        }

        ,
        handleSnippetDrag: function(pointerEvent, snippetModel) {
            $("body").append(new TempSnippetView({
                model: snippetModel
            }).render());
            this.collection.remove(snippetModel);
            PubSub.trigger("newTempPostRender", pointerEvent);
        }

        ,
        handleTempMove: function(pointerEvent) {
            $("div").remove(".target");
            if (pointerEvent.pageX >= this.$build.offset().left && pointerEvent.pageX < (this.$build.width() + this.$build.offset().left) &&
                pointerEvent.pageY >= this.$build.offset().top - 100 && pointerEvent.pageY < (this.$build.height() + this.$build.offset().top)) {
                var eventY = pointerEvent.pageY;
                var myFormBits = $(this.$el.find(".component"));
                for (var i = 0; i < $(myFormBits).length; i++) {
                    if (eventY > $(myFormBits[0]).offset().top - $(myFormBits[0]).height() && eventY < $(myFormBits[0]).offset().top) {
                        $("#build form fieldset .component").eq(i).before('<div class="target"></div>');
                        i = $(myFormBits).length;
                    } else if (eventY > $(myFormBits[i]).offset().top && eventY < $(myFormBits[i]).offset().top + $(myFormBits[i]).height()) {
                        $("#build form fieldset .component").eq(i).after('<div class="target"></div>');
                    } else if (eventY > $(myFormBits[$(myFormBits).length - 1]).offset().top) {
                        $("#build form fieldset .component").eq($(myFormBits).length - 1).after('<div class="target"></div>');
                        i = $(myFormBits).length;
                    }
                }
                if ($(myFormBits).length === 0) {;
                    $("#build form fieldset").prepend('<div class="target"></div>');
                }
                $("div .target").height($(".temp form").height());
            } else {
                $("div").remove(".target");
            }
        }

        ,
        handleTempDrop: function(pointerEvent, model, index) {
            if (pointerEvent.pageX >= this.$build.offset().left && pointerEvent.pageX < (this.$build.width() + this.$build.offset().left) &&
                pointerEvent.pageY >= this.$build.offset().top - 90 && pointerEvent.pageY < (this.$build.height() + this.$build.offset().top)) {
                var index = $(".target").index();
                $("div").remove(".target");
                this.collection.add(model, {
                    at: index
                });
                console.log(this.collection);
            } else {
                $("div").remove(".target");
            }
        }

        ,
        handleDownAdd: function(model) {
            var length = this.collection.length;
            this.collection.add(model, {
                at: length
            });
        }
    })
});
