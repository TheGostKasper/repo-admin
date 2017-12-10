(function () {
    var oDetails = new OrderDetails();
    var intv;
    var dateObj = {}
    var myChart;
    var _odcontainer = $('#order_details_id');
    var orderNumber = location.href.split('/');


    oDetails.getOrder("order/details/" + orderNumber[orderNumber.length - 1], {}).then(function (data) {
        var or = data.data;
        console.log(or);
        if (or != null) {
            oDetails.bindToHtml([{ key: '.or-m-name', value: or.merchantName },
                { key: '.or-acceptedBy', value: or.supervisorName },
                { key: '.or-handeledBy', value: or.courierName },
                { key: '.or-ordernumber', value: or.orderNumber },
                { key: '.or-m-phone', value: or.merchantPhone },
                { key: '.or-c-notes', value: or.customer.notes },
                { key: '.or-c-address', value: or.customer.address },
                { key: '.or-c-apt', value: or.customer.apt },
                { key: '.or-c-mobile', value: or.customer.mobile },
                { key: '.or-c-name', value: or.customer.name },
                { key: '.or-status', value: or.state },
                { key: '.or-grandTotal', value: or.grandTotal + ' ' + or.currency },
                { key: '.or-creationDate', value: moment(or.creationDate).format('DD/MM/YYYY hh:mm:ss A') }]);

            var _data = data.data.products;

            var html = '';
            for (var i = 0; i < _data.length; i++) {
                var name = (_data[i].nameEN == "") ? _data[i].nameAR : _data[i].nameEN
                html += '<tr><td>' + name + ' ' + _data[i].size + _data[i].unit + '</td><td>' + _data[i].price + '</td><td>' + _data[i].quantity + 'x</td><td> ' + _data[i].price * _data[i].quantity + ' </td></tr> '
            }
            html += '<tr><td colspan="3"></td><td>Total: ' + data.data.grandTotal + ' <br/>Tip: ' + data.data.tip + '.00 <br/>Discount: ' + data.data.discount + ' %<br/>Tax: ' + data.data.tax + ' %<br/>Service: ' + data.data.serviceFees + ' </td></tr>';

            $('#productsList').html(html);
            $('.products-search-Info').removeClass('hide');
            $('.pg-notfound').addClass('hide');
            $('.creationDate').html(moment.utc(data.data.creationDate).toDate().toLocaleDateString());
            getDate(data.data);
            $('#or-card').show();
        } else {
            $('#or-card').hide();
            $('.not-found').show();
        }

    }, function (err) {
        $('.not-found').show();
    });
    function drawChartToView(arr) {
        var chart = document.getElementById("bar-chart");
        $('#bar-chart').empty();
        if (myChart)
            myChart.destroy();
        myChart = new Chart(document.getElementById("bar-chart"), {
            type: 'bar',
            data: {
                labels: ["Pending", "Accepted", "Ready", "Delivered"],
                datasets: [{
                    label: "#Min ",
                    backgroundColor: [
                        'rgba(255, 99, 132, 0.2)',
                        'rgba(54, 162, 235, 0.2)',
                        'rgba(255, 206, 86, 0.2)',
                        'rgba(255, 159, 64, 0.2)'
                    ],
                    data: arr
                }]
            },
            options: {
                legend: { display: false },
                title: {
                    display: true,
                    text: 'Minutes taken'
                }
            }
        });
    }
    var hub = new SignalREntry();
    var notificationHub = hub.getConnectionHub();

    notificationHub.client.updateOrderStatus = function (state, id) {
        if (state == "Delivered" && getId(id) == id) {
            dateObj.deliveredDateTime = new Date().toString();
            updateChart(dateObj);
        }
    };
    notificationHub.client.updateReadyOrder = function (data) {
        if (data.state == "Ready" && getId(data.id) == data.id) {
            dateObj.assignedToDriverDateTime = new Date().toString();
            updateChart(dateObj);

        }
    }
    notificationHub.client.updateAcceptOrder = function (data) {
        if (data.state == "Accepted" && parseInt(getId(data.id)) == data.id) {
            dateObj.acceptedDateTime = new Date().toString();
            updateChart(dateObj);
        }
    }
    notificationHub.client.updateCancelOrder = function (orderId) {
        if (getId(orderId) == orderId) { }
    }
    function getId() {
        return parseInt($('#orderTextSearch').val())
    }
    function getDate(data) {
        dateObj.creationDate = data.creationDate;
        dateObj.acceptedDateTime = data.acceptedDateTime;
        dateObj.assignedToDriverDateTime = data.assignedToDriverDateTime;
        dateObj.deliveredDateTime = data.deliveredDateTime;
        var cMins;
        if (dateObj.acceptedDateTime == "") cMins = getMins(dateObj.creationDate, new Date().toString());
        else cMins = 0;
        drawChartToView([cMins,
            getMins(dateObj.creationDate, dateObj.acceptedDateTime),
            getMins(dateObj.acceptedDateTime, dateObj.assignedToDriverDateTime),
            getMins(dateObj.assignedToDriverDateTime, dateObj.deliveredDateTime)
        ]);
    }
    function updateChart(dateObj) {
        var cMins;
        if (dateObj.acceptedDateTime == "") cMins = getMins(dateObj.creationDate, new Date().toString());
        else cMins = 0;

        drawChartToView([cMins,
            getMins(dateObj.creationDate, dateObj.acceptedDateTime),
            getMins(dateObj.acceptedDateTime, dateObj.assignedToDriverDateTime),
            getMins(dateObj.assignedToDriverDateTime, dateObj.deliveredDateTime)
        ]);
    }
    function getMins(from, to) {
        var sec = (moment.utc(to).toDate().getTime() - moment.utc(from).toDate().getTime()) / 1000;
        var rd = (sec % (60 * 60));
        if (to != "") {
            if (from == "") return 0;
            return Math.floor(rd / 60) + '.' + Math.floor(rd % 60);
        } else
            return 0;
    }

   
}());