$(function () {
    $('#startDate').datetimepicker({ pickTime: false, language: 'ru' });

    $('#endDate').datetimepicker({ pickTime: false, language: 'ru' });

    $('#trade').click(function (e) {
        var startDate = $('#startDate input').val();
        var endDate = $('#endDate input').val();

        var inputData = {
            "StartDate": startDate,
            "EndDate": endDate
        };
        console.log(inputData);

        $.ajax({
            url: "/home/index",
            type: "POST",
            data: JSON.stringify(inputData),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                console.log(response);
                console.log(typeof(response.revenue));
                $('#currency').html("");
                $('#buyDate').html("");
                $('#sellDate').html("");
                $('#revenueUSD').html("");
                $('#currency').append(response.currency);
                $('#buyDate').append(moment(response.buyDate).format('DD[-]MM[-]YYYY'));
                $('#sellDate').append(moment(response.sellDate).format('DD[-]MM[-]YYYY'));
                $('#revenueUSD').append(response.revenue.toFixed(3));
                $('#results').css("visibility", "initial");
            }
        });
    });
});