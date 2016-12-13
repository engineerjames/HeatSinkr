var chartData = [{ x: 0, y: 0 }, { x: 1, y: 1 }, { x: 2, y: 3 }];
var chart = document.getElementById("myChart");
var myChart = new Chart(chart, {
    type: 'line',
    data: {
        datasets: [{
            label: 'Thermal Resistance',
            data: chartData
        }]
    },
    options: {
        scales: {
            xAxes: [{
                type: 'linear',
                position: 'bottom',
                scaleLabel: {
                    display: true,
                    labelString: 'Volumetric Flow Rate [CFM]'
                },
                ticks: {
                    min: 0.5,
                    stepSize: 0.5
                }
            }],
            yAxes: [{
                type: 'linear',
                position: 'left',
                ticks: {
                    beginAtZero: true
                },
                scaleLabel: {
                    display: true,
                    labelString: 'Thermal Resistance [K/W]'
                }
            }]
        },
        responsive: false
    }
});
