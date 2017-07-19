$(function () {
    $('#trade').click(function (e) {
        $.getJSON("http://api.fixer.io/latest", function (data) {
            $.ajax({
                url: "/Trade/Trade",
                type: "POST",
                data: JSON.stringify(data),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                error: function (response) {
                    alert(response.responseText);
                },
                success: function (response) {
                    alert(response);
                }
            });
        });
    });
});