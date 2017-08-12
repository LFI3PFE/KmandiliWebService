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