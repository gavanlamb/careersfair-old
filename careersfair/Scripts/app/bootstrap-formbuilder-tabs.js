$(document).ready(function () {
    var navListLength;
    var index;
    var form;
    var formDetails;
    var typingTimer;
    var doneTypingInterval;
    $("body").init(function () {
        //var setup
        navListLength = $("#navList li").length;
        index = 0;
        form = $("#builderForm");
        doneTypingInterval = 300;
        //function calls
        sectionManager();
        buttonManager();
        linkManager();
        structureCallValidator();
    });
    $("#navList li a").click(function (e) {
        var tempIndex = $("#navList li a").index(this);
        e.preventDefault();
        if ($("#navList li a:eq(" + tempIndex + ")").hasClass("enabled")) {
            index = tempIndex;
            sectionManager();
            buttonManager();
            linkManager();
        }
    });
    $("#next").click(function () {
        if (index >= 0 && index < navListLength) {
            ++index;
            sectionManager();
            buttonManager();
            linkManager();
        }
    });
    $("#previous").click(function () {
        if (index >= 0 && index < navListLength) {
            --index;
            sectionManager();
            buttonManager();
            linkManager();
        }
    });
    $("#builderFormName").keyup(function () {
        if (form.validate().element(this)) {
            undoDisableTemp();
            enableNextMove();
            $("#builderFormNameH1").text($("#builderFormName").val());
        } else {
            disableTemp();
        }
    });
    function structureCallValidator() {
        var tempVal = $("#builderFormStructure").val();
        setInterval(
            function () {
                if (index == 1 && tempVal != $("#builderFormStructure").val()) {
                    if (form.validate().element("#builderFormStructure")) {
                        undoDisableTemp();
                        enableNextMove();
                    } else {
                        disableTemp();
                    }
                    tempVal = $("#builderFormStructure").val();
                }
            }, doneTypingInterval);
    }
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
    function disableTemp() {
        for (i = index + 1; i <= navListLength; i++) {
            if ($("#navList li a:eq(" + (i) + ")").hasClass("enabled")) {
                $("#navList li a:eq(" + (i) + ")").removeClass("enabled");
                $("#navList li a:eq(" + (i) + ")").addClass("disabledtemp");
            }
        }
        if (index == (navListLength - 1)) {
            $("#finish").prop("disabled", true);
        } else if (index < (navListLength - 1)) {
            $("#next").prop("disabled", true);
        }
    }
    function undoDisableTemp() {
        for (i = index + 1; i <= navListLength; i++) {
            if ($("#navList li a:eq(" + (i) + ")").hasClass("disabledtemp")) {
                $("#navList li a:eq(" + (i) + ")").removeClass("disabledtemp");
                $("#navList li a:eq(" + (i) + ")").addClass("enabled");
            }
        }
        if (index == (navListLength - 1)) {
            $("#finish").prop("disabled", false);
        } else if (index < (navListLength - 1)) {
            $("#next").prop("disabled", false);
        }
    }
    function enableNextMove() {
        if (index == (navListLength - 1)) {
            $("#finish").prop("disabled", false);
        } else if (index < (navListLength - 1)) {
            $("#next").prop("disabled", false);
        }
        $("#navList li a:eq(" + (index + 1) + ")").removeClass("disabled");
        $("#navList li a:eq(" + (index + 1) + ")").addClass("enabled");
    }
});