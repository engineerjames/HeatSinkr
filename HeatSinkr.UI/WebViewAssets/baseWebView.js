var chartData = [{ x: 0, y: 0 }, { x: 1, y: 1 }, { x: 2, y: 3 }];
var chart = document.getElementById("myChart");
var myChart = new Chart(chart, {
    type: 'line',
    data: {
        datasets: [{
            label: 'Heatsink Thermal Resistance',
            data: chartData
        }]
    },
    options: {
        scales: {
            xAxes: [{
                type: 'linear',
                position: 'bottom'
            }]
        },
        responsive: false
    }
});