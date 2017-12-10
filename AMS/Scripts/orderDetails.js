function OrderDetails() {
    var apiUrl = WeeloApi;
    function getOrder(endPoint, data) {
        $('#ploader').show();
        return $.ajax({
            url: apiUrl + endPoint,
            type: "GET",
            headers: requestHeaders(),
            data: data,
            dataType: 'json',
            success: function (response) {
                var result = response.data;
                if (result != null) {
                    fillData(result);
                }
                else {
                    $('.pg-notfound').show();
                }
                $('#ploader').hide();
                return response;
            }

        });
    }
    function fillData(data) {
        $('.merchantName').text(data.merchantName);
        $('.merchantPhone').text(data.merchantPhone);

        $('.orderNumber').text(orderNumberFormat(data.id));
        var state = '<div class="new badge">' + data.state + '</div>'
        var stateclass = (data.state == 'pending' ? 'orange darken-1 white-text' : (data.state == 'Accepted' ? 'blue darken-3 white-text' : (data.state == 'Ready' ? 'green lighten-1 white-text' : (data.state == 'Canceled' ? 'red darken-2 white-text' : (data.state == 'Delivered' ? 'green darken-2 white-text' : '')))));
        $('.orderstate').addClass(stateclass).text(data.state);
        $('.acceptedBy').text(data.supervisorName);
        $('.handeledBy').text(data.courierName);
        $('.orderstate').text(data.state);
        //$('.total').text('Total: ' + data.total.toFixed(2));
        $('.tip').text('Tip: ' + data.tip.toFixed(2));
        $('.tax').text('Tax: ' + data.tax + '%');
        $('.service').text('Service: ' + data.serviceFees);
        $('.discount').text('Discount: ' + data.discount + '%');

        $('.customerName').text(data.customer.name);
        $('.customerAddress').text(data.customer.address);
        $('.customerApt').text(data.customer.apt);
        $('.creationDate').text(convertToLocalDate(data.creationDate));

        fillProduct(data)
        $('.pg-details').show();
    }

    function fillProduct(data) {
        var html = '', _subTot = 0, _total = 0, _grangTot = 0;
        var _data = data.products;
        for (var i = 0; i < _data.length; i++) {
            if (_data[i].state != 'NotAvailable') {
                _subTot = parseFloat((_data[i].quantity * _data[i].price)).toFixed(2);
                //_total = (parseFloat(_subTot) + parseFloat(_total)).toFixed(2);
                html += '<div>' +
                '<div class="col s1 right-align"><span class="quantity">' + _data[i].quantity + 'x</span></div>' +
                //'<div class="col s2"><img class="list-pro-img" src="' + _data[i].imageUrl + '" /></div>' +
                '<div class="col s9"><span class="itemName">' + _data[i].nameEN + ' <span class="itemSize">' + _data[i].size + ' ' + _data[i].unit + '</span></span></div>' +
                '<div class="col s2 right-align"><span class="subTotal">' + _subTot + '</span></div>' +
            '</div>';
            }
        }
        $('.products').empty().append(html);
        $('.total').text('Total: ' + data.total.toFixed(2));

        _total = parseFloat(_total);
        //_grangTot = (_total + (data.tip + merchant.serviceFees) + (_total * (merchant.tax / 100)) - (_total * (merchant.discount / 100))).toFixed(2);
        $('.grand-total').text('Grand total: ' + data.grandTotal.toFixed(2));

    }

    function applyEndPoint(id) {
        var urlChunks = window.location.href.split('/');
        var orderId = urlChunks[urlChunks.length - 1];
        if (!orderId.includes('searchOrder'))
            getOrder("order/" + orderId, {});
        else
            getOrder("order/details/" + id, {});
    }
    function getOnlyData(endPoint, data, type) {
        $('#ploader').show();
        return $.ajax({
            url: apiUrl + endPoint,
            type: type,
            headers: requestHeaders(),
            data: data,
            dataType: 'json',
            success: function (response) {
                $('#ploader').hide();
                return response;
            }
        });
    }

    function orderNumberFormat(text) {
        return "OR00" + text;
    }
    function convertToLocalDate(date) {
        var d = moment.utc(date).toDate()
        return moment(d).format('DD/MM/YYYY hh:mm:ss A')
    }
    function bindToHtml(objArr) {
        for (var i = 0; i < objArr.length; i++) {
            $(objArr[i].key).html(objArr[i].value);
        }

    }

    this.getOrder = getOrder;
    this.fillData = fillData;
    this.fillProduct = fillProduct;
    this.getOnlyData = getOnlyData;
    this.bindToHtml = bindToHtml;
};