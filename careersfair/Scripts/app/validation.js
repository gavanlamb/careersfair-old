$(document).ready(function () {
    var createAllErrors = function () {
        var form = $(this),
            errorList = $("ul.errorMessages");

        if (form.find("button.previous").length > 0) {
            var previousEventHandler = $._data($(".previous", form)[0], "events")["click"][0].handler;
        }

        if (form.find("button.next").length > 0) {
            var nextEventHandler = $._data($(".next", form)[0], "events")["click"][0].handler;
        }

        var showAllErrorMessages = function () {
            errorList.empty();
            var invalidFields = form.find(":invalid").each(function (index, node) {
                if (node.type == "radio") {
                    if (node.name + "-0" == node.id) {
                        var label = $("label[for=" + node.name + "] "),
                            message = node.validationMessage || 'Invalid value';
                        errorList.append("<li><span>" + label.html() + " ---</span> " + message + "</li>");
                    }
                } else if (node.type == "checkbox") {
                    if (node.name + "-0" == node.id) {
                        var label = $("label[for=" + node.name + "] "),
                            message = "Please select a checkbox ";
                        errorList.append("<li><span>" + label.html() + " ---</span> " + message + "</li>");
                        $('td[name=' + node.name + ']')
                    }
                } else {
                    var label = $("label[for=" + node.id + "] "),
                        message = node.validationMessage || 'Invalid value';
                    errorList.append("<li><span>" + label.html() + " ---</span> " + message + "</li>");
                }
            });
        };

        $("input[type=submit], button:not([type=button]), .previous, .next", form).on("mousedown", function (e) {
            if (!form[0].checkValidity()) {
                form.find(':submit').click();
                $(".previous, .next", form).off("click");
                $("html, body").animate({ scrollTop: 0 }, 0);
                e.preventDefault();
            } else {
                if (form.find("button.previous").length > 0) {
                    $(".previous", form).on("click", previousEventHandler);
                }
                if (form.find("button.next").length > 0) {
                    $(".next", form).on("click", nextEventHandler);
                }
            }
            $("input[type=submit], button:not([type=button]), .previous, .next", form).on("mouseup", function (e) {
                showAllErrorMessages();
            });
        });

        $('input, select').on("invalid", function (e) {
            e.preventDefault();
        });

        $('input[type=checkbox]').on("invalid", function (e) {
            
        });

        $("input", form).on("keypress", function (event) {
            var type = $(this).attr("type");
            if (/date|datetime-local|email|month|number|tel|text|time|week/.test(type) && event.keyCode == 13) {
                showAllErrorMessages();
            }
        });
    };

    $("#form fieldset").each(
        createAllErrors
    );
});