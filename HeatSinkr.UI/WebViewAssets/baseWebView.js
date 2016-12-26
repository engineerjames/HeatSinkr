var chartData = [{ x: 0, y: 0 }, { x: 1, y: 1 }, { x: 2, y: 3 }];
var chartData2 = [{ x: 0, y: 0 }, { x: 1, y: 1 }, { x: 2, y: 3 }];

var chart1 = document.getElementById("TrChart");
var chart2 = document.getElementById("PressureChart")

var TrChart = new Chart(chart1, {
    type: 'line',
    data: {
        datasets: [{
            label: 'Thermal Resistance [K/W]',
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
var PressureChart = new Chart(chart2, {
    type: 'line',
    data: {
        datasets: [{
            label: 'Pressure Drop ["H2O]',
            data: chartData2
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
                    labelString: 'Pressure Drop ["H2O]'
                }
            }]
        },
        responsive: false
    }
});

