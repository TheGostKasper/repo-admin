(function () {





    $('.pg-notfound').hide();
    $('#ReadyOrders').hide();

    var _dt = new DataTableEntry();
    var datatable;
    var data;
    var _duration = $('#selectOrderstate').val();
    if (_duration) {
        $('#ReadyOrders').show();
        fillDataTable(_duration);
    }

    $('#selectOrderstate').css('display', 'block');
    // $('#ReadyOrders').css('display', 'none');
    $('.select2').css('width', "100%");
    $('#selectOrderstate').on('change', function (e) {
        $('#ReadyOrders').show();
       
        var orderState = $(this).val();
        if (orderState)
            fillDataTable(orderState);
    })

    function fillDataTable(orderState) {
        if (datatable) datatable.destroy();
        // $('#ReadyOrders').css('display', 'block');
        datatable = _dt.bindDataTable('#ReadyOrders', [0, 1, 2, 3, 4, 5, 6, 7, 8], function (data, a, b, c) {
            if (c.col == 0)
                return data; //orderNumberFormat(data);
            if (c.col == 8) {
                if (b.state == 'Delivered') {
                    return '<i class="fa fa-check"></i><a href="/admin/home/orderdetails/' + b.orderNumber + '" class="x-right">Details</a>';
                }
                if (b.state == 'Canceled') {
                    return '<i class ="fa fa-close"></i>';
                } else {
                    return '<button class="btn deliverd-btn vsm-btn green accent-3" type="button" name="action" data-id="' + b.id + '"><i class="zmdi zmdi-check"></i></button>' +
                        '<a href="/admin/home/orderdetails/' + b.orderNumber + '" class="x-right">Details</a>';
                }
            } else
                return data;
        }, 'merchant/readyOrders', 'POST', { orderState: orderState }, [
            { "data": "orderNumber" },
            { "data": "grandTotal" },
            { "data": "paymentMethod" },
            { "data": "notes" },
            { "data": "userPhone" },
            { "data": "courierName" },
            { "data": "supervisorName" },
            { "data": "tip" },
            { "data": "state" },
        ], function (settings, json) {
            data = $('#ReadyOrders tbody tr');
        });
    }
    $('input').on('keyup', function (e) {
        var searchVal = $(this).val();
        var objArr = [];
        for (var i = 0; i < data.length; i++) {
            if (data[i].cells[0].innerText.includes(searchVal)) objArr.push(data[i]);
        }
        if (objArr.length == 0) objArr.push('<tr><td colspan="9" class="dataTables_empty">No Match</td></tr>')
        $('#ReadyOrders tbody').empty();
        for (var i = 0; i < objArr.length; i++) {
            $('#ReadyOrders tbody').append(objArr[i]);
        }
        $('#ReadyOrders tbody').html(objArr);
    });
    $(document).on('click', '.deliverd-btn', function () {
        var notify = $.notify('<strong>Saving</strong> Do not close this page...', {
            allow_dismiss: false,
            showProgressbar: true
        });

        var id = $(this).data('id');
        var data = { OrderId: id, OrderState: 5 };
        $.ajax({
            url: WeeloApi + "order/status",
            type: "post",
            headers: requestHeaders(),
            dataType: 'json',
            data: data,
            success: function (response) {
                notify.update({ 'type': 'success', 'message': '<strong>Success</strong> Your page has been saved!', 'progress': 100 });
                datatable.draw();
            }
        });
    });
}());