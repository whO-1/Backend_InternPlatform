(async function () {
    const Utils = {
        months: function (cfg) {
            const monthNames = ["January", "February", "March", "April", "May", "June", "July"];
            return monthNames.slice(0, cfg.count);
        },
        numbers: function (cfg) {
            return Array.from({ length: cfg.count }, () => Math.floor(Math.random() * (cfg.max - cfg.min + 1)) + cfg.min);
        },
        CHART_COLORS: {
            red: 'rgba(255, 99, 132, 0.5)',
            blue: 'rgba(54, 162, 235, 0.5)',
            green: 'rgba(75, 192, 192, 0.5)',
        }
    };
    const DATA_COUNT = 7;
    const NUMBER_CFG = { count: DATA_COUNT, min: -100, max: 100 };

    const labels = Utils.months({ count: 7 });
    const data = {
        labels: labels,
        datasets: [
            {
                label: 'Dataset 1',
                data: Utils.numbers(NUMBER_CFG),
                backgroundColor: Utils.CHART_COLORS.red,
            },
            {
                label: 'Dataset 2',
                data: Utils.numbers(NUMBER_CFG),
                backgroundColor: Utils.CHART_COLORS.blue,
            },
            {
                label: 'Dataset 3',
                data: Utils.numbers(NUMBER_CFG),
                backgroundColor: Utils.CHART_COLORS.green,
            },
        ]
    };

    const config = {
        type: 'bar',
        data: data,
        options: {
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        },
    };

    new Chart(
        document.getElementById('BarItem'),
        config,
    );

})();