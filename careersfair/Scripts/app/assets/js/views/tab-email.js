define(["jquery", "underscore", "backbone", "text!templates/app/tab-nav-actions.html", "text!templates/app/textbox.html", "views/select", "helper/pubsub"],
function($, _, Backbone, _tabNavTemplate, _textboxTemplate, Select, PubSub) {
    return Backbone.View.extend({
        tagName: "div",
        className: "tab-pane col-xs-12 col-sm-12 col-md-12 col-lg-12",
        initialize: function() {
            this.id = this.options.id.toLowerCase().replace(/\W/g, '');
            this.id = this.id + (new Date().getTime() - 2);
            this.tabNavTemplate = _.template(_tabNavTemplate);
            this.textboxTemplate = _.template(_textboxTemplate);
            this.select = new Select({
                id: this.id,
                collection: this.collection,
                className: "col-xs-6 col-sm-6 col-md-5 col-lg-4 col-md-offset-1 col-lg-offset-1"
            });
            this.render();
        },
        render: function() {
            $("#actionstabs").append(this.tabNavTemplate({
                title: this.options.title,
                id: this.id
            }));
            this.$el.attr("id", this.id);
            this.$el.append("<div class='actionsheadingrow col-xs-12 col-sm-12 col-md-12 col-lg-12'><h2>TO</h2></div>");
            this.$el.append(this.select.render().$el);
            this.$el.append("<div class='col-xs-12 col-sm-12 col-md-12 col-lg-12'><h2>Subject</h2></div>");
            this.$el.append(this.textboxTemplate({
                subjectclasses: "col-xs-6 col-sm-6 col-md-5 col-lg-4 col-md-offset-1 col-lg-offset-1",
                id: this.id + "subject"
            }));
            this.$el.appendTo("#actionscontent");
            this.delegateEvents();
        }
    });
});
