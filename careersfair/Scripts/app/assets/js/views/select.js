define(["jquery", "underscore", "backbone", "views/select-item"],
    function($, _, Backbone, SelectItem) {
        return Backbone.View.extend({
            tagName: "select",
            initialize: function() {
                _.bindAll(this, 'addOne', 'addAll');
                this.collection.on('reset', this.addAll);
                this.collection.on('add', this.addOne);
                this.collection.on('change', this.render);
                this.$el.append("<option selected value=''></option>");
            },
            addOne: function(list) {
                if (list.getField("linkedin") || list.getField("use")) {
                    this.$el.append(
                        new SelectItem({
                            model: list
                        }).render().$el);
                }
            },
            addAll: function() {
                this.collection.each(this.addOne);
                return this;
            }
        });
    });
