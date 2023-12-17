var canvas = document.getElementById("chart_area").getContext('2d');
$(function ()
{
    //$("#ID").click(function () {
    $.ajax(
        {
            type: "GET",
            url: "/Chart/GetTaskPerCategory",
            data: "",
            contextType: "application/json; charset-utf-8",
            dataType: "json",
            success: OnSuccessResult,
            error: OnError
        });
    function OnSuccessResult(data)
    {
        var _data = data;
        var _chartLabels = _data[0];
        var _chartValues = _data[1];
        /*var colors = ['red', 'green', 'blue', 'pink'];*/
        new Chart("chart_area",
            {
                type: "bar",
                data:
                {
                    labels: _chartLabels,
                    datasets:
                        [{
                            label: '',
                            backgroundColor: ['rgb(215, 199, 132)', 'rgb(0, 199, 132)', 'rgb(25, 99, 132)', 'rgb(255, 99, 13)'],
                            data: _chartValues,
                            borderWidth: 1,
                            borderColor: '#777',
                            hoverBorderWidth: 2,
                            hoverBorderColor: '#000',
                            barThickness: 30
                        }],
                    options:
                    {
                        title: {
                            display: true,
                            text: 'To-Do',
                            fontSize: 23,
                        },
                        maintainAspectRatio: false,
                        legend: {
                            display: true,

                        },


                    }


                }
            });
    }
    function OnError(data) {
        alert("No Data");
    }
});
//});
var canvas = document.getElementById("myChart").getContext('2d');
$(function ()
{
    //$("#ID").click(function () {
    $.ajax(
        {
            type: "GET",
            url: "/Chart/GetTaskPerPriority",
            data: "",
            contextType: "application/json; charset-utf-8",
            dataType: "json",
            success: OnSuccessResult,
            error: OnError
        });
    function OnSuccessResult(data)
    {
        var _data = data;
        var _chartLabels = _data[0];
        var _chartValues = _data[1];
        /*var colors = ['red', 'green', 'blue', 'pink'];*/
        new Chart("myChart",
            {
                type: "pie",
                data:
                {
                    labels: _chartLabels,
                    datasets:
                        [{
                            label: 'Task List per Category',
                            backgroundColor: ['rgb(215, 199, 132)', 'rgb(0, 199, 132)', 'rgb(25, 99, 132)'],
                            data: _chartValues,
                            borderWidth: 1,
                            borderColor: '#777',
                            hoverBorderWidth: 2,
                            hoverBorderColor: '#000',
                            barThickness: 30
                        }],
                    options:
                    {
                        title: {
                            display: true,
                            text: 'Task per Priority',
                            fontSize: 23,
                        },
                        maintainAspectRatio: false,
                        legend: {
                            display: true,

                        },


                    }


                }
            });
    }
    function OnError(data) {
        alert("No Data");
    }
});
    //});


