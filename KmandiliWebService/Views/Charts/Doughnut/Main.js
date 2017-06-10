var ctx = document.getElementById("doughnutChart");
var jsonDataString = document.getElementById("doughnutData").innerHTML.replace(/(\r\n|\n|\r|\t| )/gm, "");
var data = JSON.parse(jsonDataString);
console.log(data);
var myPieChart = new Chart(ctx, {
    type: 'doughnut',
    data: {
        datasets: [
            {
                label: 'Points',
                backgroundColor: ['#77BEF5', '#db5c9b', '#797db0', '#4a82bc', '#54b8d7', '#c9d5de'],
                data: data.Values
            }
        ],
        labels: data.Labels
    },
    options: {
        percentageInnerCutout: 40,
        tooltips: {
            callbacks: {
                label: function(tooltipItem, currentData) {
                    var dataset = currentData.datasets[tooltipItem.datasetIndex];
                    var label = currentData.labels[tooltipItem.index];
                    var total = data.Total;
                    var currentValue = dataset.data[tooltipItem.index];
                    var precentage = (currentValue / total) * 100;         
                    return label +" "+ precentage + "%";
                }
            }
        },
        responsive: true,
        scaleBeginAtZero: true,
        legend:
            {
            display: true,
            position: 'bottom',
            labels: {
                generateLabels: function (chart) {
                    var data = chart.data;
                    if (data.labels.length && data.datasets.length) {
                        return data.labels.map(function (label, i) {
                            var meta = chart.getDatasetMeta(0);
                            var ds = data.datasets[0];
                            var arc = meta.data[i];
                            var custom = arc && arc.custom || {};
                            var getValueAtIndexOrDefault = Chart.helpers.getValueAtIndexOrDefault;
                            var arcOpts = chart.options.elements.arc;
                            var fill = custom.backgroundColor ? custom.backgroundColor : getValueAtIndexOrDefault(ds.backgroundColor, i, arcOpts.backgroundColor);
                            var stroke = custom.borderColor ? custom.borderColor : getValueAtIndexOrDefault(ds.borderColor, i, arcOpts.borderColor);
                            var bw = custom.borderWidth ? custom.borderWidth : getValueAtIndexOrDefault(ds.borderWidth, i, arcOpts.borderWidth);

                            // We get the value of the current label
                            var value = chart.config.data.datasets[arc._datasetIndex].data[arc._index];

                            return {
                                // Instead of `text: label,`
                                // We add the value to the string
                                text: label + " : " + value,
                                fillStyle: fill,
                                strokeStyle: stroke,
                                lineWidth: bw,
                                hidden: isNaN(ds.data[i]) || meta.data[i].hidden,
                                index: i
                            };
                        });
                    } else {
                        return [];
                    }
                }
            }
        }
    }
});
