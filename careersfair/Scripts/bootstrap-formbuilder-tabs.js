$(document).ready(function () {
    var navListLength;
    var index;
    $("body").init(
        function () {
            navListLength = $("#navList li").length;
            index = 0;
            buttonEnablerDisabler();
            hideShowDivs();
            disableEnableLinks();
        }
    );
    $("#navList li a").click(
        function () {
            index = $("#navList li a").index(this);
            if (inRange()) {
                hideShowDivs();
                buttonEnablerDisabler();
            } else {
                alert("Error");
            }
        }
    );
    $("#next").click(
        function () {
            if (inRange()) {
                increaseIndex();
                hideShowDivs();
                buttonEnablerDisabler();
                disableEnableLinks();
            } else {
                alert("Error");
            }
        }
    );
    $("#previous").click(
        function () {
            if (inRange()) {
                decreaseIndex();
                hideShowDivs();
                buttonEnablerDisabler();
            } else {
                alert("Error");
            }
        }
    );
    $("#finish").click(
        function () {
        }
    );

    //Disables links for all unexplored options
    function disableEnableLinks() {
        for (i = 0; i <= navListLength; i++) {
            var myAnchor = $("#navList li a:eq(" + i + ")");
            if (i <= index) {
                myAnchor.removeClass("disabled");
                myAnchor.addClass("done");
            } else if (i == index) {
                myAnchor.removeClass("disabled");
                myAnchor.addClass("selected");
            } else {
                myAnchor.addClass("disabled");
            }
        }
    }
    
    //Hides and shows the corresponding divs
    function hideShowDivs() {
        for (i = 0; i <= navListLength; i++) {
            var $myDiv = $(".content:eq(" + i + ")");
            if (i == index) {
                $myDiv.show();
            } else {
                $myDiv.hide();
            }
        }
    }

    //Enables and disables buttons 
    function buttonEnablerDisabler() {
        if (isLast()) {
            $("#next").hide();
            $("#finish").show();
        } else {
            $("#next").show();
            $("#finish").hide();
        }
        if (isFirst()) {
            $("#previous").hide();
        } else {
            $("#previous").show();
        }
    }

    //increase the index value by one
    function increaseIndex() {
        ++index;
    }

    //increase the index value by one
    function decreaseIndex() {
        --index;
    }

    //checks to see if the future tab is in range
    function inRange() {
        var ret = false;
        if (index >= 0 && index < navListLength) {
            ret = true;
        }
        return ret;
    }

    //checks to see if the future tab is the last tab
    function isLast() {
        var ret = false;
        if (index == navListLength - 1) {
            ret = true;
        }
        return ret;
    }

    //checks to see if future tab is the first tab
    function isFirst() {
        var ret = false;
        if (index == 0) {
            ret = true;
        }
        return ret;
    }
});