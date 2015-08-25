$(document).ready(function () {
    var navListLength;
    var index;
    $("body").init(
        function () {
            navListLength = $("#navList li").length;
            index = 0;
            hideShowDivs();
            buttonEnablerDisabler();
            disableEnableLinks();
        }
    );
    $("#navList li a").click(
        function (e) {
            var tempIndex = $("#navList li a").index(this);
            e.preventDefault();
            if ($("#navList li a:eq(" + tempIndex + ")").hasClass("enabled")) {
                index = tempIndex;
                hideShowDivs();
                buttonEnablerDisabler();
                disableEnableLinks();
            }
        }
    );
    $("#next").click(
        function () {
            if (index >= 0 && index < navListLength) {
                ++index;
                hideShowDivs();
                buttonEnablerDisabler();
                disableEnableLinks();
            } 
        }
    );
    $("#previous").click(
        function () {
            if (index >= 0 && index < navListLength) {
                --index;
                hideShowDivs();
                buttonEnablerDisabler();
                disableEnableLinks();
            } 
        }
    );
    $('#formNameInput').keyup(
        function () {
            var form = $('#formNameForm');
            var nextButton = $('#next');
            nextButton.button();
            var validator = form.data('validator');
            validator.settings.showErrors = function () {
                var nrOfInvalids = this.numberOfInvalids();
                var buttonVerb = (nrOfInvalids > 0) ? "disable" : "enable";
                nextButton.button(buttonVerb);
                if (buttonVerb == "disable") {
                    disableTemp();
                } else if (buttonVerb == "enable") {
                    undoDisableTemp();
                    $("#formNameBuild").text($("#formNameInput").val());
                }
                this.defaultShowErrors();
            };
        }
    ());
    //Disables links for all unexplored options
    function disableEnableLinks() {
        for (i = 0; i <= navListLength; i++) {
            var myAnchor = $("#navList li a:eq(" + i + ")");
            if (i < index) {
                if (myAnchor.hasClass("selected")) {
                    myAnchor.removeClass("selected");   
                }
                if (!myAnchor.hasClass("enabled")) {
                    myAnchor.addClass("enabled");
                }
            } else if (i == index) {
                if (myAnchor.hasClass("disabled")) {
                    myAnchor.removeClass("disabled");
                }
                if (myAnchor.hasClass("enabled")) {
                    myAnchor.removeClass("enabled");
                }
                myAnchor.addClass("selected");
            } else {
                if (myAnchor.hasClass("selected")) {
                    myAnchor.removeClass("selected");
                    myAnchor.addClass("enabled");
                } else if (!myAnchor.hasClass("enabled")) {
                    myAnchor.addClass("disabled");
                }
            }
        }
    }
    //Hides and shows the corresponding divs
    function hideShowDivs() {
        for (i = 0; i <= navListLength; i++) {
            var myDiv = $(".content:eq(" + i + ")");
            if (i == index) {
                myDiv.show();
            } else {
                myDiv.hide();
            }
        }
    }
    //Enables and disables buttons 
    function buttonEnablerDisabler() {
        if (index == 0) {
            $("#previous").hide();
        } else {
            $("#previous").show();
        }
        if (index == navListLength - 1) {
            $("#next").hide();
            $("#finish").show();
        } else {
            $("#next").show();
            $("#finish").hide();
        }
        if ($("#navList li a:eq(" + (index + 1) + ")").hasClass("enabled") || $("#navList li a:eq(" + (index + 1) + ")").hasClass("selected")) {
            $("#next").prop("disabled", false);
        } else {
            $("#next").prop("disabled", true);
        }
    }

    function disableTemp() {
        for (i = index + 1; i <= navListLength; i++) {
            if ($("#navList li a:eq(" + (i) + ")").hasClass("enabled")) {
                $("#navList li a:eq(" + (i) + ")").removeClass("enabled");
                $("#navList li a:eq(" + (i) + ")").addClass("disabledtemp");
            }
        }
    }
    function undoDisableTemp() {
        for (i = index + 1; i <= navListLength; i++) {
            if ($("#navList li a:eq(" + (i) + ")").hasClass("disabledtemp")) {
                $("#navList li a:eq(" + (i) + ")").removeClass("disabledtemp");
                $("#navList li a:eq(" + (i) + ")").addClass("enabled");
            }
        }
    }
});