$(document).ready(function () {
    $(".file-label").click(function () {
        $($(this).attr("for")).click();
    });
    $(".selfie-label").click(function () {
        $($(this).attr("for")).click();
    });
    $("input.file").change(function () {
        $($(this).next(".file-name")).html($(this).val().split("\\").pop());
    });
});