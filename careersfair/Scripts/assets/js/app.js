define([
       "jquery" , "underscore" , "backbone"
       , "collections/snippets" , "collections/my-form-snippets"
       , "views/tab" , "views/my-form"
       , "text!data/input.json", "text!data/radio.json", "text!data/select.json", "text!data/buttons.json", "text!data/page.json"
       ,
], function(
  $, _, Backbone
  , SnippetsCollection, MyFormSnippetsCollection
  , TabView, MyFormView
  , inputJSON, radioJSON, selectJSON, buttonsJSON, pageJSON
){
  return {
    initialize: function(){

      //Bootstrap tabs from json.
      new TabView({
        title: "Input"
        , collection: new SnippetsCollection(JSON.parse(inputJSON))
      });
      new TabView({
        title: "Radios"
        , collection: new SnippetsCollection(JSON.parse(radioJSON))
      });
      new TabView({
        title: "Select"
        , collection: new SnippetsCollection(JSON.parse(selectJSON))
      });
      new TabView({
        title: "Buttons"
        , collection: new SnippetsCollection(JSON.parse(buttonsJSON))
      });
      new TabView({
        title: "Page"
        , collection: new SnippetsCollection(JSON.parse(pageJSON))
      });
      //Make the first tab active!
      $("#components .tab-pane").first().addClass("active");
      $("#formtabs li").first().addClass("active");

      $('#toggle_bs_style').on('click', function(e) {
        e.preventDefault();
        if ($('#bootstrap-classic-theme').attr('href') == '') {
          $('#bootstrap-classic-theme').attr('href', 'assets/css/lib/bootstrap-3.0.0/dist/css/bootstrap-theme.min.css');
        } else {
          $('#bootstrap-classic-theme').attr('href', '');
        }

      });

      // Bootstrap "My Form" with 'Form Name' snippet.
      new MyFormView({
        title: "Original"
        , collection: new MyFormSnippetsCollection([])
      });
    }
  }
});
