(function () {
    google.charts.load('current', {
        'packages': ['corechart', 'table', 'bar', 'geochart'],
        'mapsApiKey': 'AIzaSyD-9tSrke72PouQMnMX-a7eZSW0jkFMBWY'
    });
    var mrch = new Merchant();
    var oDetails = new OrderDetails();
    var orderObj = {
        pending: 0,
        accepted: 0,
        ready: 0,
        canceled: 0,
        delivered: 0
    }
    var total = 0, totalOrder = 0, totalusers = 0, ios = 0, android = 0, arlang = 0, enlang = 0,
        myChart, usersArr = [], regionArr = [], glob_activity = [], orderslastCheck = moment(new Date()), timer;
    var map, marker, infoWindow;
    mrch.getData('order/state/list', 'GET', {}).then(function (data) {
        var _data = data.data;
        console.log(_data)
        getArrayOfOrderStatus(data)
        setTimeout(function () {
            //applyOrderChanges(orderObj, percent);
            totalOrder = _data.orders;
            totalusers = _data.users;
            ios = _data.ios;
            android = _data.android;
            arlang = _data.arLang;
            enlang = _data.enLang;
            $('#totalUsers').html(totalusers);
            $('#totalMerchant').html(_data.merchants);
            $('#totalOrders').html(totalOrder);
            $('#iosUsers').text(_data.ios);
            $('#androidUsers').text(_data.android);
            $('#sales').text(_data.sales.replace('$', '') + ' EGP');

        }, 500);
        gtTopOrd()
    }, function (err) { });

    function gtTopOrd() {
        mrch.getData('order/list/top', 'GET', {}).then(function (response) {
            if (response.data != null) {
                response = JSON.parse(JSON.stringify(response));
                addOrderToactivity(response.data.reverse());
            }
        }, function (err) { });
    }
    //$('div.x_panel').toggle( "highlight" );


    var hub = new SignalREntry();
    var notificationHub = hub.getConnectionHub();
    notificationHub.client.orderAdminNotify = function (data) {
        orderObj.pending++;
        //applyOrderChanges(orderObj, percent);
        totalOrder++;
        $('#totalOrders').html(totalOrder);
        glob_activity.splice(0, 1);
        glob_activity.push(data);
        //drawActivity(data);
        applyChangesOnActivity(glob_activity);
        google.charts.setOnLoadCallback(drawBarsChart);
    };

    notificationHub.client.updateOrderStatus = function (state, id) {
        var data = {};
        data.id = id;
        if (state == "Delivered") {
            orderObj.delivered++;
            orderObj.ready = (orderObj.ready > 0) ? orderObj.ready - 1 : 0;
            //var percent = drawChart(orderObj);
            //applyOrderChanges(orderObj, percent);
            data.state = "Delivered";
        } else {
            orderObj.canceled++;
            orderObj.accepted = (orderObj.accepted > 0) ? orderObj.accepted - 1 : 0;
            //var percent = drawChart(orderObj);
            //applyOrderChanges(orderObj, percent);
            data.state = "Canceled";
        }
        addOrderStateToActivity(data);
        google.charts.setOnLoadCallback(drawBarsChart);
        //console.log('updateOrderStatus');
    };
    notificationHub.client.updateReadyOrder = function (data) {
        orderObj.ready++;
        orderObj.accepted = (orderObj.accepted > 0) ? orderObj.accepted - 1 : 0;
        //var percent = drawChart(orderObj);
        //applyOrderChanges(orderObj, percent);
        addOrderStateToActivity(data);
        google.charts.setOnLoadCallback(drawBarsChart);
    }
    notificationHub.client.updateAcceptOrder = function (data) {
        orderObj.accepted++;
        orderObj.pending = (orderObj.pending > 0) ? orderObj.pending - 1 : 0;
        // //console.log(orderObj.pending);
        //var percent = drawChart(orderObj);
        // applyOrderChanges(orderObj, percent);
        addOrderStateToActivity(data);
        google.charts.setOnLoadCallback(drawBarsChart);
    }
    notificationHub.client.updateCancelOrder = function (orderId) {
        var data = {}
        orderObj.canceled++;
        data = getActivity(orderId);
        //if (data && data.state == 'Pending')
        //    orderObj.accepted = (orderObj.accepted > 0) ? orderObj.accepted - 1 : 0;
        //else
        orderObj.pending = (orderObj.pending > 0) ? orderObj.pending - 1 : 0;
        //var percent = drawChart(orderObj);
        //applyOrderChanges(orderObj, percent);
        if (data)
            data.state = 'Canceled';
        addOrderStateToActivity(data);
        google.charts.setOnLoadCallback(drawBarsChart);

    }
    notificationHub.client.userCreated = function (data) {
        totalusers++;
        $('#totalUsers').html(totalusers);

    }
    notificationHub.client.broadcastMessage = function (name, msg) {
        alert(name);
    }
    hub.getHubStarted().done(function () {
        $('.x_title').on('click', function () {

        });

        //notificationHub.server.sendToEhab("Ehab", "Welcome in Weelo");
    });


    function getArrayOfOrderStatus(_data) {
        orderObj = {
            pending: _data.data.pending,
            accepted: _data.data.accepted,
            ready: _data.data.ready,
            canceled: _data.data.canceled,
            delivered: _data.data.delivered
        }
        return [orderObj.pending, orderObj.accepted, orderObj.ready, orderObj.canceled, orderObj.delivered]
    }
    function getPercentage(dataArray) {
        var total = 0;
        for (var i = 0; i < dataArray.length; i++) {
            total += dataArray[i]
        }
        return total;
    }
    function applyChangesOnActivity(data) {
        $('.list-unstyled').empty();
        //console.log("list-ubstyled");
        for (var i = 0; i < data.length; i++) {
            drawActivity(data[i]);
        }
    }
    function drawChartToView(arr) {
        var chart = document.getElementById("bar-chart");
        $('#bar-chart').empty();
        if (myChart)
            myChart.destroy();
        myChart = new Chart(document.getElementById("bar-chart"), {
            type: 'bar',
            data: {
                labels: ["Pending", "Accepted", "Ready", "Canceled", "Delivered"],
                datasets: [
                  {
                      label: "# Order",
                      backgroundColor: [
                            'rgba(255, 99, 132, 0.2)',
                            'rgba(54, 162, 235, 0.2)',
                            'rgba(255, 206, 86, 0.2)',
                            'rgba(255, 86, 82, 0.72)',
                            'rgba(153, 102, 255, 0.2)'

                      ],
                      data: arr
                  }
                ]
            },
            options: {
                legend: { display: false },
                title: {
                    display: true,
                    text: 'Daily Order '
                }
            }
        });
    }

    function drawLangDeviceUsage(arr) {

        new Chart(document.getElementById("lang-chart"), {
            type: 'doughnut',
            data: {
                labels: ["Arabic", "English"],
                datasets: [
                  {
                      label: "Users",
                      backgroundColor: ["#3498DB", "#1ABB9C"],
                      data: arr
                  }
                ]
            },
            options: {
                title: {
                    display: true,
                    text: 'Language Used'
                }
            }
        });
    }

    function addOrderStateToActivity(data) {
        //console.log("update activity");
        var oActivity = $.grep(glob_activity, function (e) { return e.id == data.id; });
        if (oActivity.length > 0) {
            oActivity[0].state = data.state;
            var arr = glob_activity;
            $('.list-unstyled').empty();
            addOrderToactivity(arr);
        }
        highlightActiveOrder(data.id);
    }

    $(document).on('click', '.title a', function (e) {
        e.preventDefault();

        if ($(this).data('id') != '')
            oDetails.getOrder("order/details/" + $(this).data('id'), {}).then(function (data) {
                var or = data.data;
                $('#modal-ord-dt').modal('show');
                if (or != null) {
                    oDetails.bindToHtml([
                        { key: '.or-m-name', value: or.merchantName },
                        { key: '.or-m-phone', value: or.merchantPhone },
                        { key: '.or-c-address', value: or.customer.address },
                        { key: '.or-c-apt', value: or.customer.apt },
                        { key: '.or-c-mobile', value: or.customer.mobile },
                        { key: '.or-c-name', value: or.customer.name },
                        { key: '.or-c-note', value: or.customer.notes },
                        { key: '.or-status', value: or.status },
                        { key: '.or-acceptedBy', value: or.acceptedBy },
                        { key: '.or-handeledBy', value: or.handeledBy },
                        { key: '.or-ordernumber', value: or.orderNumber },
                        { key: '.or-grandTotal', value: or.grandTotal + 'EG' }]);
                    var _data = data.data.products;
                    var html = '';
                    for (var i = 0; i < _data.length; i++) {
                        var name = (_data[i].nameEN == "") ? _data[i].nameAR : _data[i].nameEN
                        html += '<tr><td>' + name + ' ' + _data[i].size + _data[i].unit + '</td><td>' + _data[i].quantity + 'x</td><td> ' + _data[i].price * _data[i].quantity + ' </td></tr> '
                    }
                    //html += '<tr><td colspan="2"></td><td>Total: ' + data.data.grandTotal + ' <br/>Tip: ' + data.data.tip + '.00 <br/>Discount: ' + data.data.discount + ' %<br/>Tax: ' + data.data.tax + ' %<br/>Service: ' + data.data.serviceFees + ' </td></tr>';
                    $('#productsList-lay').html(html);
                    $('.products-search-Info').removeClass('hide');
                    $('.pg-notfound').addClass('hide');
                    $('.creationDate').html(moment.utc(data.data.creationDate).toDate().toLocaleDateString());
                    getDate(data.data);
                    $('.or-card').show();
                    $('#modal-ord-dt').modal('show');

                } else {
                    $('.or-card').hide();
                    $('.not-found-lay').show();
                }

            }, function (err) {
                $('.or-card').show();
            });
        else {
            $('.or-card').hide();
            $('.not-found-lay').show();
        }
    });
    function bindToHtml(containerId, value) {
        //console.log(containerId, value);
        $('.' + containerId).html(value);
    }

    function highlightActiveOrder(id) {
        $('a[data-id="' + id + '"]').parent().parent().css('background', '#ffe0e6');
    }
    function addOrderToactivity(response) {
        var result = response;
        glob_activity = [];
        for (var i = 0; i < result.length; i++) {
            var data = result[i];
            glob_activity.push(data);
            drawActivity(data);
        }
    }
    function getActivity(id) {
        console.log(glob_activity);
        return $.grep(glob_activity, function (e) { return e.id == id; })[0];
    }
    function drawActivity(data) {
        var mdate = moment(data.creationDate);
        var date = (data.creationDate) ? moment(mdate).format("hh:mm A") : '--';
        var grandTotal = (data.grandTotal) ? data.grandTotal : '--';
        var className = '';
        if (data.state == 'Pending') className = 'label-warning';
        if (data.state == 'Accepted') className = 'label-info';
        if (data.state == 'Ready') className = 'label-success';
        if (data.state == 'Delivered') className = 'label-deliver';
        if (data.state == 'Canceled') className = 'label-danger';
        var html = '<div class ="listview__content oactivity">' +
                           '<div class ="listview__heading title">' +
                           '<a data-toggle="modal" data-id= ' + data.id + ' style="font-weight: 600;cursor: pointer; color: #3e95cd;font-size: 20px;"  data-target="#modal-ord-ds" > ' + data.orderNumber + ' </a>' +
                            '<span class = "status right ' + className + '" > ' + data.state + ' </span></div><p><span style="font-style:normal;"> ' + date + ' </span></p></div>';
        $('.list-unstyled').prepend(html);
    }

    $('a.listview__item').on('click', function (e) {
        e.preventDefault();
    });




    function drawBarsChart() {
        var data = orderObj;
        var dataTable = new google.visualization.DataTable();
        dataTable.addColumn('string', 'Status');
        dataTable.addColumn('number', 'Orders');
        // A column for custom tooltip content
        dataTable.addColumn({ type: 'string', role: 'tooltip' });
        dataTable.addRows([
          ['' + data.pending + ' Pending', data.pending, '' + data.pending + ' Order(s)'],
          ['' + data.accepted + ' Active', data.accepted, '' + data.accepted + ' Order(s)'],
          ['' + data.ready + ' Ready', data.ready, '' + data.ready + ' Order(s)'],
          ['' + data.canceled + ' Canceled', data.canceled, '' + data.canceled + ' Order(s)'],
          ['' + data.delivered + ' Deliverd', data.delivered, '' + data.delivered + ' Order(s)']
        ]);

        var options = {
            legend: 'none',
            animation: {
                duration: 1000,
                easing: 'out',
            },
        };
        var chart = new google.visualization.ColumnChart(document.getElementById('tooltip_action'));
        chart.draw(dataTable, options);
        hideloader('.ords_card');
        lastUpdate();
    }
    function drawDeviceUsage() {
        var data = new google.visualization.DataTable();
        data.addColumn('string', 'Topping');
        data.addColumn('number', 'Slices');
        data.addRows([
          ['iPhone', ios],
          ['Android', android]
        ]);

        // Set chart options
        var options = {
            'title': 'Users Platform',
            'width': 'auto',
            'height': 300
        };

        // Instantiate and draw our chart, passing in some options.
        var chart = new google.visualization.PieChart(document.getElementById('chart_div'));
        chart.draw(data, options);
    }
    function drawLanguageUsage() {
        var data = new google.visualization.DataTable();
        data.addColumn('string', 'Topping');
        data.addColumn('number', 'Slices');
        data.addRows([
          ['Arabic', arlang],
          ['English', enlang]
        ]);

        // Set chart options
        var options = {
            'title': 'Languages',
            'width': 'auto',
            'height': 300
        };

        // Instantiate and draw our chart, passing in some options.
        var chart = new google.visualization.PieChart(document.getElementById('lang_chart_div'));
        chart.draw(data, options);
    }
    function lastUpdate() {
        timer = setInterval(function () {
            var d = moment(new Date());
            $('.card_last_update span').text(moment.duration(orderslastCheck.diff(d)).humanize({ precision: 3 }));

            //if (tt == 0) {
            //    lastCheck = moment(new Date());
            //    tt = dfltTime;
            //    stopOrderTimer();
            //    checkNewOrders();
            //}
        }, 1000);
        orderslastCheck = moment(new Date());
    }
    function showloader(selector) {
        $(selector).append('<div class="card_loader"><img src="/admin/Content/loader.gif" /></div>');
    }
    function hideloader(selector) {
        $(selector + ' .card_loader').remove();
    }

    google.charts.setOnLoadCallback(drawRegionsMap);



    var cityObjs = [], cArr = [], mrchArr = [];
    function drawCityChart() {
        var data = google.visualization.arrayToDataTable(cityObjs);
        var options = {
            title: 'Users per City'

        };
        var chart = new google.visualization.PieChart(document.getElementById('cityChart'));
        chart.draw(data, google.charts.Bar.convertOptions(options));
    }
    function getAccCities(endPoint) {
        callAjax(endPoint, 'POST', { pageNumber: 1, pageSize: 10 }).then(function (response) {
            cityObjs = getArr(response.data.data);
            //cityObjs = response.data.data;
            google.charts.setOnLoadCallback(drawCityChart);
            // $('.preloader-wrapper-regionChart').css('display', 'none');
        }, function (err) {
        })
    }

    function drawAreaChart() {
        var data = google.visualization.arrayToDataTable(cArr);
        var options = {
            title: 'Users per area'
        };
        var chart = new google.visualization.PieChart(document.getElementById('areaChart'));
        chart.draw(data, google.charts.Bar.convertOptions(options));
    }
    function getAccAreas(endPoint) {
        callAjax(endPoint, 'POST', { pageNumber: 1, pageSize: 10 }).then(function (response) {
            cArr = getArr(response.data.data);
            //cityObjs = response.data.data;
            google.charts.setOnLoadCallback(drawAreaChart);
            // $('.preloader-wrapper-regionChart').css('display', 'none');
        }, function (err) {
        })
    }

    function drawMerchantChart() {
        var data = google.visualization.arrayToDataTable(mrchArr);
        var options = {
            title: 'Users per Merchant'
        };
        var chart = new google.visualization.PieChart(document.getElementById('merchantChart'));
        chart.draw(data, google.charts.Bar.convertOptions(options));
    }
    function getAccMerchants(endPoint) {
        callAjax(endPoint, 'POST', { pageNumber: 1, pageSize: 10 }).then(function (response) {
            mrchArr = getArr(response.data.data);
            //console.log(mrchArr);
            google.charts.setOnLoadCallback(drawMerchantChart);
        }, function (err) {
        })
    }


    function getArr(data) {
        var arr = [['Text', 'Total']];
        for (var i = 0; i < data.length; i++) {
            arr.push([data[i].text, parseInt(data[i].total)])
        }
        return arr;
    }


    function initMap() {
        //map = new google.maps.Map(document.getElementById('map'), {
        //    center: { lat: 29.9660593, lng: 31.2493228 },
        //        zoom: 5,
        //       // gestureHandling: 'none',
        //        //zoomControl: false,
        //        //disableDefaultUI: true
        //    });
        getUsers();
    }
    function getUsers() {
        callAjax('dashboard/users/details', 'POST', { pageNumber: 1, pageSize: 10 }).then(function (response) {
            var data = response.data.data;
            //console.log(response);
            $('#totalUsers').html(response.data.total);
            usersArr = [];
            for (var i = 0; i < data.length; i++) {
                usersArr.push(data[i]);
                //console.log(data[i]);
                setMarkers(data[i]);
            };
            //google.charts.setOnLoadCallback(drawusers);
            //$('.preloader-wrapper-table_div').css('display', 'none');
        }, function (err) {

        });

    }
    function drawRegionsMap() {
        var data = google.visualization.arrayToDataTable(cityObjs);
        console.log(cityObjs);
        var options = {
            region: 'EG',
            displayMode: 'markers',
            colorAxis: { colors: ['#00853f', 'black', '#e31b23'] },
            //defaultColor: '#3366cc',
        };

        var chart = new google.visualization.GeoChart(document.getElementById('map'));

        chart.draw(data, options);
    }
    function setMarkers(userObj) {
        var marker = new google.maps.Marker({
            position: { lat: parseFloat(userObj.latitude), lng: parseFloat(userObj.longitude) },
            map: map,
            title: userObj.apt,
            zIndex: userObj.id
        });
        addInfoWindow(marker);
    }
    function addInfoWindow(marker, message) {
        google.maps.event.addListener(marker, 'click', function (evt) {
            if (infoWindow) infoWindow.close();
            infoWindow = new google.maps.InfoWindow({
                content: '<p style="max-width:250px;overflow:hidden;padding:10px;margin:auto;">' + this.title + '</p>'
            });
            infoWindow.open(map, marker);
        });
    }
    function callAjax(endPoint, type, data) {
        //console.log(endPoint, type, data);
        return $.ajax({
            url: WeeloApi + endPoint,
            type: type,
            headers: requestHeaders(),
            dataType: 'json',
            data: data
        })
    }

    function init() {


        google.charts.setOnLoadCallback(drawDeviceUsage);
        google.charts.setOnLoadCallback(drawLanguageUsage);
        google.charts.setOnLoadCallback(drawBarsChart);

        //initMap();


        getAccAreas("dashboard/accounts/area");
        getAccCities("dashboard/accounts/city");
        getAccMerchants("dashboard/merchant/users");
        google.charts.setOnLoadCallback(drawRegionsMap);

    };
    init();
    $(window).resize(function () {
        drawBarsChart();
        drawDeviceUsage();
        drawLanguageUsage();
        drawAreaChart();
        drawCityChart();
        drawMerchantChart();
    });
}());