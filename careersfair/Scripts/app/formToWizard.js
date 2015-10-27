$(document).ready(function () {
    var pageBreak = $("div.pageBreak");
    if (pageBreak.length > 0) {
        $("div.pageBreak").each(function () {
            var boundary = $(this);
            $("<fieldset>").insertAfter(boundary.parent()).append(boundary.nextAll().andSelf());
            boundary.remove();
        });
        $("#form").formToWizard({ submitButton: 'submit' });
        $("#submit").css("margin-top", "-65px");
    }
});
(function ($) {
    $.fn.formToWizard = function (options) {
        options = $.extend({
            submitButton: ""
        }, options);
        var element = this;
        var steps = $(element).find("fieldset");
        var count = steps.size();
        var submmitButtonName = "#" + options.submitButton;
        $(submmitButtonName).hide();
        steps.each(function(i) {
            $(this).wrap("<div id='step" + i + "'></div>");
            $(this).append("<p id='step" + i + "commands'></p>");
            if (i == 0) {
                createNextButton(i);
                selectStep(i);
            }
            else if (i == count - 1) {
                $("#step" + i).hide();
                createPrevButton(i);
            }
            else {
                $("#step" + i).hide();
                createPrevButton(i);
                createNextButton(i);
            }
        });
        function createPrevButton(i) {
            var stepName = "step" + i;
            $("#" + stepName + "commands").append("<button type='button' href='#' id='" + stepName + "Prev' class='previous btn pull-left'><span class='ion-ios-arrow-left'></span></button>");
            $("#" + stepName + "Prev").bind("click", function(e) {
                $("#" + stepName).hide();
                $("#step" + (i - 1)).show();
                $(submmitButtonName).hide();
                selectStep(i - 1);
            });
        }
        function createNextButton(i) {
            var stepName = "step" + i;
            $("#" + stepName + "commands").append("<button type='button' href='#' id='" + stepName + "Next' class='next btn pull-right'><span class='ion-ios-arrow-right'></span></button>");
            $("#" + stepName + "Next").bind("click", function(e) {
                $("#" + stepName).hide();
                $("#step" + (i + 1)).show();
                if (i + 2 == count)
                    $(submmitButtonName).show();
                selectStep(i + 1);
            });
        }
        function selectStep(i) {
            $("#steps li").removeClass("current");
            $("#stepDesc" + i).addClass("current");
        }
    }
})(jQuery);
