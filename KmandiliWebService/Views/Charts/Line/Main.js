var ctx = document.getElementById("lineChart");
var jsonDataString = document.getElementById("lineData").innerHTML.replace(/(\r\n|\n|\r|\t| )/gm, "");
var lineTitle = document.getElementById("lineTitle").innerHTML;
var data = JSON.parse(jsonDataString);
var maxValue = Math.max.apply(null, data.Values);
//var maxValue = 20;
var myBarChar = new Chart(ctx, {
    type: 'line',
    scaleIntegersOnly: true,
    data: {
        datasets: [
            {
                label: lineTitle + ' ('+data.Year+')',
                lineTension: 0.1,
                borderColor: "rgba(75, 192, 192, 1)",
                borderCupStyle: "butt",
                borderDash: [],
                borderDashOffset: 0.0,
                borderJoinStyle: "meter",
                pointBorderColor: "rgba(75, 192, 192, 1)",
                pointBackgroundColor: "#fff",
                pointBorderWidth: 1,
                pointHoverRadius: 5,
                pointHoverBackgroundColor: "rgba(75, 192, 192, 1)",
                pointHoverBorderColor: "rgba(220, 220, 220, 1)",
                pointHoverBorderWidth: 2,
                pointRadius: 1,
                pointHitRadius: 10,
                data: data.Values
            }
        ],
        labels: data.Labels
    },
    options: {
        responsive: true,
        scales: {
          yAxes: [{
                  ticks: {
                      beginAtZero: true,
                      callback: function (value) { if (Number.isInteger(value)) { return value; } },
                  }
              }]
        }
    }
});
