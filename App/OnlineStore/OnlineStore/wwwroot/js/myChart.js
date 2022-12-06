let charDates = [];
let charPrices = [];
let charAmounts = [];

for (var item of document.getElementsByName("charDates")) {
    charDates.push(item.value);
}
for (var item of document.getElementsByName("charPrices")) {
    charPrices.push(parseFloat(item.value));
}
for (var item of document.getElementsByName("charAmounts")) {
    charAmounts.push(parseInt(item.value));
}

const ctx = document.getElementById('myChart').getContext('2d');
const myChart = new Chart(ctx, {
    type: 'line',
    data: {
        labels: charDates,
        datasets: [{
            label: 'Выручка от продажи за месяц',
            data: charPrices,
            borderColor: 'rgb(234, 28, 38)'
        }]
    },
    options: {
        scales: {
            x: {
                display: true,
                title: {
                    display: true,
                    text: 'Месяц и год продажи'
                }
            },
            y: {
                display: true,
                title: {
                    display: true,
                    text: 'Выручка от продажи'
                }
            }
        }
    }
    
});

const ctx2 = document.getElementById('myChart2').getContext('2d');
const myChart2 = new Chart(ctx2, {
    type: 'line',
    data: {
        labels: charDates,
        datasets: [{
            label: 'Количество проданных товаров за месяц',
            data: charAmounts,
            borderColor: 'rgb(75, 192, 192)'
        }]
    },
    options: {
        scales: {
            x: {
                display: true,
                title: {
                    display: true,
                    text: 'Месяц и год продажи'
                }
            },
            y: {
                display: true,
                title: {
                    display: true,
                    text: 'Количество проданных товаров, шт'
                }
            }
        }
    }

});