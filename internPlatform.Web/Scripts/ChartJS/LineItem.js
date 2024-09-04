
(async function () {
    const data = {
        labels: ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November','December'],
        datasets: [{
            label: 'Looping tension',
            data: [65, 59, 80, 81, 26, 55, 40],
            fill: false,
            borderColor: '#f04853',
        }]
    };

    const config = {
        type: 'line',
        data: data,
        options: {
            animations: {
                tension: {
                    duration: 5000,
                    easing: 'linear',
                    from: 1,
                    to: 0,
                    loop: true
                }
            },
            
            elements: {
                point: {
                    pointStyle: 'circle',
                },
                line: {
                    backgroundColor: '#f04853',
                    borderWidth: 3,

                },
            },
            interaction: {
                mode: 'nearest'
            },
            plugins: {
                legend: {
                    display: false,
                },
                title: {
                    align: 'start',
                    padding: 17,
                    color: '#373647',
                    display: true,
                    position: 'bottom',
                    font: {
                        family:'Verdana, Geneva, Tahoma, sans-serif',
                        weight: 'bolder',
                        size: 12,
                    },
                    text: 'Exception statistics',
                },
            },
            responsive: true,

            aspectRatio: 4


        }
    };


    new Chart(
        document.getElementById('LineItem'),
        config,
    );
})();