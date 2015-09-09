$(document).ready(function () {
    var navListLength;
    var index;
    $("body").init(
        function () {
            navListLength = $("#navList li").length;
            index = 0;
            sectionManager();
            buttonManager();
            linkManager();
        }
    );
    $("#navList li a").click(
        function (e) {
            var tempIndex = $("#navList li a").index(this);
            e.preventDefault();
            if ($("#navList li a:eq(" + tempIndex + ")").hasClass("enabled")) {
                index = tempIndex;
                sectionManager();
                buttonManager();
                linkManager();
            }
        }
    );
    $("#next").click(
        function () {
            if (index >= 0 && index < navListLength) {
                ++index;
                sectionManager();
                buttonManager();
                linkManager();
            } 
        }
    );
    $("#previous").click(
        function () {
            if (index >= 0 && index < navListLength) {
                --index;
                sectionManager();
                buttonManager();
                linkManager();
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
                if (buttonVerb == "disable") {
                    disableTemp();
                } else if (buttonVerb == "enable") {
                    undoDisableTemp();
                    enableNextMove();
                    $("#formNameBuild").text($("#formNameInput").val());
                }
                this.defaultShowErrors();
            };
        }
    ());
    //Hides and shows the corresponding divs
    function sectionManager() {
        for (i = 0; i <= navListLength; i++) {
            var myDiv = $(".content:eq(" + i + ")");
            if (i == index) {
                myDiv.show();
            } else {
                myDiv.hide();
            }
        }
    }
    //Disables links for all unexplored options
    function linkManager() {
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
    //Enables and disables buttons 
    function buttonManager() {
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
    function enableNextMove() {
        if (index == (navListLength - 1)) {
            $("#finish").prop("disabled", false);
        } else if (index < (navListLength - 1)) {
            $("#next").prop("disabled", false);
        }
        $("#navList li a:eq(" + (index+1) + ")").removeClass("disabled");
        $("#navList li a:eq(" + (index+1) + ")").addClass("enabled");
    }
});