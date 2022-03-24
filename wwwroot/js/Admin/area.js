// Area Chart
var ctx = document.getElementById('myAreaChart');
var ar = $('#myAreaChart').data('month');
var someDate = new Date();
function GetDates(x) {
  
    var dd = someDate.getDate()+x;
    var mm = someDate.getMonth()+1 ;
    var y = someDate.getFullYear();

    return dd + '/' + mm + '/' + y;
    
}
console.log(GetDates(-7));
console.log(GetDates(-6));
console.log(GetDates(-5));


var myLineChart = new Chart(ctx, {
    type: 'line',
    data: {
        labels: [GetDates(-6), GetDates(-5), GetDates(-4), GetDates(-3), GetDates(-2), GetDates(-1), "Today"],
        datasets: [{
            label: "TotalPrice",
            lineTension: 0.3,
            backgroundColor: "rgba(2,117,216,0.2)",
            borderColor: "rgba(2,117,216,1)",
            pointRadius: 5,
            pointBackgroundColor: "rgba(2,117,216,1)",
            pointBorderColor: "rgba(255,255,255,0.8)",
            pointHoverRadius: 5,
            pointHoverBackgroundColor: "rgba(2,117,216,1)",
            pointHitRadius: 50,
            pointBorderWidth: 2,
            data: ar,
        }],
    },
    options: {
        scales: {
            xAxes: [{
                time: {
                    unit: 'date'
                },
                gridLines: {
                    display: false
                },
                ticks: {
                    maxTicksLimit: 7
                }
            }],
            yAxes: [{
                ticks: {
                    min: 0,
                    //max: 4000,
                    //maxTicksLimit: 5
                },
                gridLines: {
                    color: "rgba(0, 0, 0, .125)",
                }
            }],
        },
        legend: {
            display: false
        }
    }
});
