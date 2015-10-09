define(["jquery", "underscore", "backbone"], function($, _, Backbone) {
    return Backbone.View.extend({
        tagName: 'option',
        initialize: function() {
            _.bindAll(this, 'render');
            this.model.on('change', this.render, this);
            this.model.on('remove', this.remove, this);
        },
        render: function() {
            if (this.model.getField("label") != false) {
                this.$el.attr('value', this.model.getField("id")).html(this.model.getField("label"));
            } else if (this.model.getField("placeholder") != false) {
                this.$el.attr('value', this.model.getField("id")).html(this.model.getField("placeholder"));
            } else if (this.model.getField("type") != false) {
                this.$el.attr('value', this.model.getField("id")).html(this.model.getField("type"));
            }
            return this;
        }
    });
});
