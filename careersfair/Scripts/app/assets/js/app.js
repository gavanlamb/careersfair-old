define(["jquery", "underscore", "backbone", "collections/snippets", "collections/my-form-snippets", "router/snippet", "views/tab", "views/my-form", "text!data/input.json", "text!data/radio.json", "text!data/select.json", "text!data/buttons.json", "text!data/page.json", "views/tab-linkedin", "bootstrap"],
    function($, _, Backbone, SnippetsCollection, MyFormSnippetsCollection, SnippetRouter, TabView, MyFormView, inputJSON, radioJSON, selectJSON, buttonsJSON, pageJSON, TabLinkedInView) {
        return {
            initialize: function() {
                var router = new SnippetRouter();
                router.formelements = new MyFormSnippetsCollection([]);
                //Bootstrap tabs from json.
                new TabView({
                    title: "Input",
                    collection: new SnippetsCollection(JSON.parse(inputJSON))
                });
                new TabView({
                    title: "Radios",
                    collection: new SnippetsCollection(JSON.parse(radioJSON))
                });
                new TabView({
                    title: "Select",
                    collection: new SnippetsCollection(JSON.parse(selectJSON))
                });
                new TabView({
                    title: "Buttons",
                    collection: new SnippetsCollection(JSON.parse(buttonsJSON))
                });
                new TabView({
                    title: "Page",
                    collection: new SnippetsCollection(JSON.parse(pageJSON))
                });
                //Make the first tab active!
                $("#formbuilder #components .tab-pane").first().addClass("active");
                $("#formbuildertabs li").first().addClass("active");
                // Bootstrap "My Form" with 'Form Name' snippet.
                new MyFormView({
                    title: "Original",
                    collection: router.formelements
                });
                // new TabLinkedInView({
                //     title: "ion-ios-email",
                //     id: "Email",
                //     collection: router.formelements
                // });
                // new TabLinkedInView({
                //     title: "ion-ios-help",
                //     id: "Incentives",
                //     collection: router.formelements
                // });
                new TabLinkedInView({
                    title: "ion-social-linkedin",
                    id: "actionslinkedIn",
                    collection: router.formelements
                });
                // new TabLinkedInView({
                //     title: "ion-ios-help",
                //     id: "Popup",
                //     collection: router.formelements
                // });
                $("#actions .tab-content .tab-pane").first().addClass("active");
                $("#actionstabs li").first().addClass("active");
            }
        }
    });
