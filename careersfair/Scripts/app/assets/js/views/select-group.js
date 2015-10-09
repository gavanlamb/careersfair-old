define(["jquery", "underscore", "backbone", "views/select", "collections/collection", "text!data/linkedin.json"], function($, _, Backbone, Select, Collection, linkedinJSON) {
    return Backbone.View.extend({
        tagName: "div",
        className: "actionslinkedinselectgroup col-xs-12 col-sm-12 col-md-12 col-lg-12",
        initialize: function() {
            this.idSelect = this.id;
            this.id = this.id + new Date().getTime();
            this.selectForm = new Select({
                collection: this.collection,
                id: this.idSelect + "sf" + new Date().getTime(),
                className: "actionslinkedinformitems  col-xs-6 col-sm-6 col-md-5 col-lg-4 col-md-offset-1 col-lg-offset-1"
            });
            this.selectLinkedInList = [];
            this.linkedinClassName = "actionslinkedinlinkedinitems" + new Date().getTime();
            this.linkedInCollection = new Collection(JSON.parse(linkedinJSON));
            this.selectLinkedInList.push(new Select({
                collection: this.linkedInCollection,
                id: this.idSelect + "sl" + new Date().getTime(),
                className: this.linkedinClassName + " col-xs-6 col-sm-6 col-md-5 col-lg-4 col-md-offset-1 col-lg-offset-1"
            }));
        },
        render: function() {
            this.$el.attr("id", this.id);
            this.$el.append(this.selectForm.addAll().el);
            this.$el.append("</div>");
            for (var i = 0; i < this.selectLinkedInList.length; i++) {
                this.$el.append(this.selectLinkedInList[i].addAll().el);
            }
            this.delegateEvents();
            return this;
        },
        updateFormCollection:function(collection){
            this.selectForm.options.collection = collection;
        },
        removedUnused: function() {
            for (var i = 0; i < this.selectLinkedInList.length; i++) {
                if ($(this.selectLinkedInList[i].el).val() == "" && i > 0) {
                    this.selectLinkedInList[i].el.remove();
                    this.selectLinkedInList.splice(i, 1);
                }
            }
        },
        addEnd: function() {
            if ($(this.selectLinkedInList[this.selectLinkedInList.length - 1].el).val() != "" && this.selectLinkedInList.length < this.selectLinkedInList[0].collection.length) {
                this.selectLinkedInList.push(new Select({
                    collection: this.linkedInCollection,
                    id: this.idSelect + "sl" + new Date().getTime(),
                    className: this.linkedinClassName + " col-xs-6 col-sm-6 col-md-5 col-lg-4 col-xs-offset-6 col-sm-offset-6 col-md-offset-7 col-lg-offset-6"
                }));
                this.$el.append(this.selectLinkedInList[this.selectLinkedInList.length - 1].addAll().el);
            }
        },
        getJson: function() {
            if ($(this.selectForm.el).val() != "" && $(this.selectLinkedInList[0].el).val() != "") {
                var linkedInConnections = [];
                var value = [];
                for (var i = 0; i < this.selectLinkedInList.length; i++) {
                    if ($(this.selectLinkedInList[i].el).val() != "") {
                        value.push({
                            linkedInField: $(this.selectLinkedInList[i].el).val()
                        });
                    }
                }
                linkedInConnections.push({
                    field: $(this.selectForm.el).val(),
                    value: value
                });
                return linkedInConnections;
            }
        },
        showFields: function() {
            $(this.selectForm.$el).find('option').show();
            for (var i = 0; i < this.selectLinkedInList.length; i++) {
                $(this.selectLinkedInList[i].$el).find('option').show();
            }
        },
        hideLinkedInFields: function() {
            var value = $(this.selectForm.$el).val();
            var $otherOptions = $(".actionslinkedinformitems").not($(this.selectForm.$el)).find('option');
            if (value != "") {
                $otherOptions.filter(function() {
                    return $(this).val() != "" && $(this).val() == value;
                }).hide();
            }
            for (var i = 0; i < this.selectLinkedInList.length; i++) {
                var value = $(this.selectLinkedInList[i].$el).val();
                var $otherOptions = $("." + this.linkedinClassName).not($(this.selectLinkedInList[i].$el)).find('option');
                if (value != "") {
                    $otherOptions.filter(function() {
                        return $(this).val() != "" && $(this).val() == value;
                    }).hide();
                }
            }
        }
    });
});
