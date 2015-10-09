define(["jquery", "underscore", "backbone", "models/snippet"], function($, _, Backbone, SnippetModel) {
    return Backbone.Collection.extend({
        model: SnippetModel
    });
});
