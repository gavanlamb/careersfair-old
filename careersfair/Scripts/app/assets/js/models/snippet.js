define(['jquery', 'underscore', 'backbone'],
    function($, _, Backbone) {
        return Backbone.Model.extend({
            getValues: function() {
                return _.reduce(this.get("fields"), function(o, v, k) {
                    if (v["type"] == "select") {
                        o[k] = _.find(v["value"], function(o) {
                            return o.selected
                        })["value"];
                    } else {
                        o[k] = v["value"];
                    }
                    return o;
                }, {});
            },
            idFriendlyTitle: function() {
                return this.get("title").replace(/\W/g, '').toLowerCase();
            },
            setField: function(name, value) {
                var fields = this.get("fields");
                fields[name]["value"] = value;
                this.set("fields", fields);
            },
            getField: function(name) {
                var fields = this.get("fields");
                if (typeof fields !== "undefined" && typeof fields[name] !== "undefined") {
                    if (typeof fields[name]["type"] !== "undefined" && fields[name]["type"] == "select") {
                        var field = _.find(fields[name]["value"], function(o) {
                            return o.selected
                        })["value"];
                    } else {
                        var field = fields[name]["value"];
                    }
                    if (typeof field !== "undefined") {
                        return field;
                    }
                }
                return false;
            }
        });
    });
