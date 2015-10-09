define(["jquery", "underscore", "backbone", "text!templates/app/tab-nav.html"],
    function($, _, Backbone, _tabNavTemplate) {
        return Backbone.View.extend({
            tagName: "div",
            className: "tab-pane",
            initialize: function() {
                this.id = this.options.title.toLowerCase().replace(/\W/g, '');
                this.tabNavTemplate = _.template(_tabNavTemplate);
                this.render();
            },
            render: function() {
                var that = this;
                if (that.collection !== undefined) {
                    _.each(this.collection.renderAll(), function(snippet) {
                        that.$el.append(snippet);
                    });
                } else if (that.options.content) {
                    that.$el.append(that.options.content);
                }
                $("#formbuildertabs").append(this.tabNavTemplate({
                    title: this.options.title,
                    id: this.id
                }))
                this.$el.attr("id", this.id);
                this.$el.appendTo("#formbuildercontent");
                this.delegateEvents();
            }
        });
    });
