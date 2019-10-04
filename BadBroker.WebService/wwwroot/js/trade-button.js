$(function () {
    $('#startDate').datetimepicker({ pickTime: false, language: 'ru' });
    $('#endDate').datetimepicker({ pickTime: false, useCurrent: false, language: 'ru' });

    $('#startDate').data("DateTimePicker").setMinDate(new Date(1998, 12, 1));
    $('#startDate').data("DateTimePicker").setMaxDate(new Date(Date()));
    $('#startDate').on("dp.change", function (e) {
        $('#endDate').data("DateTimePicker").setMinDate(e.date);
    });

    $('#endDate').data("DateTimePicker").setMinDate(new Date(1998, 12, 1));
    $('#endDate').data("DateTimePicker").setMaxDate(new Date(Date()));
    $('#endDate').on("dp.change", function (e) {
        $('#startDate').data("DateTimePicker").setMaxDate(e.date);
    });


    $('#trade').click(function (e) {
        var startDate = $('#startDate input').val();
        var endDate = $('#endDate input').val();
        var score = $('#score').val();

        if (startDate === "" || endDate === "") {
            $('#modal').modal('show');
        } else {
            var inputData = {
            "StartDate": startDate,
            "EndDate": endDate,
            "Score": score
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
                if (response.success) {
                    $('#currency').html("");
                    $('#buyDate').html("");
                    $('#sellDate').html("");
                    $('#revenueUSD').html("");
                    $('#currency').append(response.result.currency);
                    $('#buyDate').append(moment(response.result.buyDate).format('DD[-]MM[-]YYYY'));
                    $('#sellDate').append(moment(response.result.sellDate).format('DD[-]MM[-]YYYY'));
                    $('#revenueUSD').append(response.result.revenue.toFixed(3));
                    $('#results').css("visibility", "initial");
                } else {
                    location.href = response.redirectUrl;
                }
            }
        });
        }
    });
});