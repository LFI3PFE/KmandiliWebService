var ctx = document.getElementById("productChart");
var quantityJsonDataString = document.getElementById("productQunatityData").innerHTML.replace(/(\r\n|\n|\r|\t| )/gm, "");
var ordersJsonDataString = document.getElementById("productOrdersData").innerHTML.replace(/(\r\n|\n|\r|\t| )/gm, "");
var quantityData = JSON.parse(quantityJsonDataString);
var ordersData = JSON.parse(ordersJsonDataString);
new Chart(ctx, {
    type: 'bar',
    data: {
        labels: ordersData.Labels,
        datasets: [
        {
            type: 'line',
            label: 'Commandes',
            fill: false,
            lineTension: 0.1,
            borderColor: "#db5c9b",
            borderCupStyle: "butt",
            borderDash: [],
            borderDashOffset: 0.0,
            borderJoinStyle: "meter",
            pointBorderColor: "rgba(75, 192, 192, 1)",
            pointBackgroundColor: "#fff",
            pointBorderWidth: 1,
            pointHoverRadius: 5,
            pointHoverBackgroundColor: "#db5c9b",
            pointHoverBorderColor: "rgba(220, 220, 220, 1)",
            pointHoverBorderWidth: 2,
            pointRadius: 1,
            pointHitRadius: 10,
            yAxisID: 'A',
            data: ordersData.Values
        },
        {
            label: 'Quantit\351 (Unit\351 de vente)',
            yAxisID: 'A',
            backgroundColor: 'rgba(75, 192, 192, 1)',
            data: quantityData.Values
        }]
    },
    options: {
        scales: {
            yAxes: [{
                id: 'A',
                type: 'linear',
                position: 'left',
                ticks: {
                    beginAtZero: true,
                    callback: function (value) { if (Number.isInteger(value)) { return value; } },
                }
            }]
        }
    }
});
//var jsonDataString = document.getElementById("lineData").innerHTML.replace(/(\r\n|\n|\r|\t| )/gm, "");
//var data = JSON.parse(jsonDataString);
//var maxValue = Math.max.apply(null, data.Values);
////var maxValue = 20;
//var myBarChar = new Chart(ctx, {
//    //scaleIntegersOnly: true,
//    data: {
//        type: 'bar',
//        datasets: [
//            {
//                type: 'line',
//                label: 'sample-line',
//                data: [29, 62, 117, 105, 42, 100],
//                borderColor: "rgba(254,97,132,0.8)",
//                backgroundColor: "rgba(254,97,132,0.5)",
//                yAxisID: "y-axis-1"
//            },
//            {
//                label: 'sample-bar',
//                data: [40, 32, 14, 78, 114, 78],
//                borderColor: "rgba(54,164,235,0.8)",
//                backgroundColor: "rgba(54,164,235,0.5)",
//                yAxisID: "y-axis-2"
//            }
//        ],
//        labels: ["2/10", "2/11", "2/12", "2/13", "2/14", "2/15"]
//    },
//    //options: {
//    //    responsive: true,
//    //    scales: {
//    //      yAxes: [{
//    //              ticks: {
//    //                  beginAtZero: true,
//    //                  callback: function (value) { if (Number.isInteger(value)) { return value; } },
//    //              }
//    //          }]
//    //    }
//    //}
//});
