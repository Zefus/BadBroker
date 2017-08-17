$(function () {

    $('#startDate').datetimepicker({ pickTime: false, language: 'ru' });

    $('#endDate').datetimepicker({ pickTime: false, language: 'ru' });

    var enumerateDaysBetweenDates = function (startDate, endDate) {
        var dates = [];

        var currDate = startDate.clone().startOf('day');
        var lastDate = endDate.clone().startOf('day');

        dates.push(currDate.clone().toDate());
        while (currDate.add(1, 'days').diff(lastDate) <= 0) {
            console.log(currDate.toDate());
            dates.push(currDate.clone().toDate());
        }
        return dates;
    };

    $('#trade').click(function (e) {

        var startDate = $('#startDate input').val();
        var endDate = $('#endDate input').val();

        startDate = new Date(Number(startDate.substring(6, 10)),
            Number(startDate.substring(3, 5)) - 1, Number(startDate.substring(0, 2)));
        endDate = new Date(Number(endDate.substring(6, 10)),
            Number(endDate.substring(3, 5)) - 1, Number(endDate.substring(0, 2)));

        var inputData = {
            "StartDate": moment(startDate).format('YYYY[-]MM[-]DD'),
            "EndDate": moment(endDate).format('YYYY[-]MM[-]DD'),
            "AllRates": []
        };

        var dates = [];
        dates = enumerateDaysBetweenDates(moment(startDate), moment(endDate));


        for (var i = 0; i < dates.length; i++) {
            dates[i] = moment(dates[i]).format('YYYY[-]MM[-]DD');
        }

        var finished = 0;
        var dateCount = dates.length;
        var model;

        for (var i = 0; i < dateCount; i++) {
            $.getJSON("http://api.fixer.io/" + dates[i] + "?base=USD", function (data) {
                inputData.AllRates.push(data);
                if (++finished === dateCount) {
                    $.ajax({
                        url: "/Trade/Index",
                        type: "POST",
                        data: JSON.stringify(inputData),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            var i = 0;
                            console.log(response.Currency);
                            $('#currency').append(response.Currency);
                            $('#buyDate').append(moment(response.BuyDate).format('DD[-]MM[-]YYYY'));
                            $('#sellDate').append(moment(response.SellDate).format('DD[-]MM[-]YYYY'));
                            $('#revenueUSD').append(response.Score);
                            $('#results').css("visibility", "initial");
                        }                        
                    });
                }
            });
        }
    });
});