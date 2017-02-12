var chartData = [{ x: 0.5, y: 7.525 }, { x: 1.143, y: 5.453 }, { x: 1.786, y: 4.605 }, { x: 2.429, y: 4.107 }, { x: 3.071, y: 3.769 }, { x: 3.714, y: 3.521 }, { x: 4.357, y: 3.328 }, { x: 5, y: 3.173 }, { x: 5.643, y: 3.044 }, { x: 6.286, y: 2.936 }, { x: 6.929, y: 2.843 }, { x: 7.571, y: 2.761 }, { x: 8.214, y: 2.69 }, { x: 8.857, y: 2.626 }, { x: 9.5, y: 2.568 }];
var chartData2 = [{ x: 0.5, y: 7.525 }, { x: 1.143, y: 5.453 }, { x: 1.786, y: 4.605 }, { x: 2.429, y: 4.107 }, { x: 3.071, y: 3.769 }, { x: 3.714, y: 3.521 }, { x: 4.357, y: 3.328 }, { x: 5, y: 3.173 }, { x: 5.643, y: 3.044 }, { x: 6.286, y: 2.936 }, { x: 6.929, y: 2.843 }, { x: 7.571, y: 2.761 }, { x: 8.214, y: 2.69 }, { x: 8.857, y: 2.626 }, { x: 9.5, y: 2.568 }];
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
                    min: 0.0,
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
                    min: 0.0,
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

