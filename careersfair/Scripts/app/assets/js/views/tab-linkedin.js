define(["jquery", "underscore", "backbone", "text!templates/app/tab-nav-actions.html", "views/select-group", "helper/pubsub"],
function($, _, Backbone, _tabNavTemplate, SelectGroup, PubSub) {
    return Backbone.View.extend({
        tagName: "div",
        className: "tab-pane col-xs-12 col-sm-12 col-md-12 col-lg-12",
        events: {
            "change": "changeSelected"
        },
        initialize: function() {
            PubSub.on("changeSelected", this.changeSelected, this);
            this.id = this.options.id.toLowerCase().replace(/\W/g, '');
            this.idSelectGroup = this.id;
            this.id = this.id + (new Date().getTime() - 2);
            this.tabNavTemplate = _.template(_tabNavTemplate);
            this.selectGroupList = [];
            this.selectGroupList.push(new SelectGroup({
                id: this.idSelectGroup,
                collection: this.collection
            }));
            this.render();
        },
        render: function() {
            this.$el.append("<div class='actionsheadingrow col-xs-12 col-sm-12 col-md-12 col-lg-12'><div class='col-xs-12 col-sm-5 col-md-5 col-lg-5 col-sm-offset-1 col-md-offset-1 col-lg-offset-1'><h2 class='actionsheading'>Form Fields</h2></div><div class='col-xs-12 col-sm-5 col-md-5 col-lg-5 col-sm-offset-1 col-md-offset-1 col-lg-offset-1'><h2 class='actionsheading'>Linkedin Fields</h2></div></div>");
            $("#actionstabs").append(this.tabNavTemplate({
                title: this.options.title,
                id: this.id
            }));
            this.$el.attr("id", this.id);
            for (var i = 0; i < this.selectGroupList.length; i++) {
                this.$el.append(this.selectGroupList[i].render().$el);
            }
            this.$el.appendTo("#actionscontent");
            this.delegateEvents();
        },
        changeSelected: function() {
            for (var i = 0; i < this.selectGroupList.length; i++) {
                this.selectGroupList[i].removedUnused();
                this.selectGroupList[i].addEnd();
                this.selectGroupList[i].showFields();
                var selectLinkedInListIndex = (this.selectGroupList[i].selectLinkedInList.length == 1 ? this.selectGroupList[i].selectLinkedInList.length - 1 : this.selectGroupList[i].selectLinkedInList.length - 2);
                if (this.selectGroupList.length > 1 && ($(this.selectGroupList[i].selectForm.$el).val() == "" &&
                $(this.selectGroupList[i].selectLinkedInList[0].$el).val() == "") ||
                ((this.selectGroupList[0].selectForm.collection.length < this.selectGroupList.length  &&
                this.selectGroupList.length > 1) && $(this.selectGroupList[i].selectForm.$el).val() == "" &&
                $(this.selectGroupList[i].selectLinkedInList[0].$el).val() != "") ) {
                    this.selectGroupList[i].$el.remove();
                    this.selectGroupList.splice(i, 1);
                }
            }
            var selectLinkedInListIndex = (this.selectGroupList[this.selectGroupList.length - 1].selectLinkedInList.length == 1 ? this.selectGroupList[this.selectGroupList.length - 1].selectLinkedInList.length - 1 : this.selectGroupList[this.selectGroupList.length - 1].selectLinkedInList.length - 2);
            if ($(this.selectGroupList[this.selectGroupList.length - 1].selectForm.$el).val() != "" &&
            $(this.selectGroupList[this.selectGroupList.length - 1].selectLinkedInList[selectLinkedInListIndex].$el).val() != "" &&
            this.selectGroupList.length < this.selectGroupList[0].countLinkedinFields()) {
                this.selectGroupList.push(new SelectGroup({
                    id: this.idSelectGroup,
                    collection: this.collection
                }));
                this.$el.append(this.selectGroupList[this.selectGroupList.length - 1].render().$el);
            }
            var linkedInConnections = [];
            for (var i = 0; i < this.selectGroupList.length; i++) {
                this.selectGroupList[i].hideLinkedInFields();
                var JSONLinkedIn = this.selectGroupList[i].getJson();
                if (typeof JSONLinkedIn !== "undefined") {
                    linkedInConnections.push({
                        linkedInConnections: JSONLinkedIn
                    });
                }
            }
            if (linkedInConnections.length > 0) {
                $("#actions #actionslinkedin").val(JSON.stringify(linkedInConnections));
            }else{
                $("#actions #actionslinkedin").val("");
            }
        }
    });
});
