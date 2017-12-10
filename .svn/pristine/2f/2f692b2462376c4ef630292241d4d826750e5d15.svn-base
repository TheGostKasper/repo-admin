    (function() {
        var dateToPass = "";
        var datatable;
        var merchantId;
        var duration = $('#durationSelectDataTable').val();
        $('#container-hide').hide();
        $('.select2').css('width', "100%");

        var _dt = new DataTableEntry();
        var _merchant = new Merchant();

        //if ($('#durationSelectDataTable').val() > 0) fillDataTable($('#durationSelectDataTable').val());

        $('#durationSelectDataTable').on('change', function(e) {
            duration = $(this).val();
            if (duration > 0) {
                fillDataTable(duration);
                dateToPass = "";
                $('#datepickerTo').val('');
                $('#datepickerFrom').val('');
            } else {
                $('#myModal').modal('show')
            }

        })

        $('#searchRangeReport').on('click', function() {
            var frommyDate = new Date($('#datepickerFrom').val());
            var tomyDate = new Date($('#datepickerTo').val());

            var fromDate = _dt.getStringDate(frommyDate);
            var toDate = _dt.getStringDate(tomyDate);

            $('#reportModal').closeModal();

            if (tomyDate != '' && frommyDate != '') {
                var timeDiff = Math.abs(tomyDate.getTime() - frommyDate.getTime());
                var diffDays = Math.ceil(timeDiff / (1000 * 3600 * 24));
                dateToPass = toDate;
                fillDataTable(diffDays);
            } else {
                alert('Something went wrong');
            }
        })

        function fillDataTable(duration) {
            if (datatable) datatable.destroy();

            datatable = _dt.bindDataTable('#OrderReport', [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10], function(data, a, b, c) {
                if (c.col >= 9) {
                    if (data!="") return moment(data).format('DD/MM/YYYY hh:mm:ss A');
                    else return "";
                }
                if (c.col == 10) {
                    //if (data == 'Ready') {
                    //    return '<span class="green white-text">Ready</span>';
                    //}
                    //if (data == 'Canceled') {
                    //    return '<i class ="red white-text">Canceled</i>';
                    //}
                    //if (data == 'Accepted') {
                    //    return '<i class ="blue white-text">Accepted</i>';
                    //}
                    //if (data == 'Pending') {
                    //    return '<i class ="orange white-text">Pending</i>';
                    //}
                    //else {
                    //    return '<i class ="purple white-text">Delivered</i>';
                    //}
                } else
                    return data;
            }, 'merchant/support/order', 'POST', { merchantId: merchantId, duration: duration, dateToPass: dateToPass }, [
                { "data": "id" },
                { "data": "courierName" },
                { "data": "courierPhone" },
                { "data": "notes" },
                { "data": "userPhone" },
                { "data": "supervisorName" },
                { "data": "grandTotal" },
                { "data": "paymentMethod" },
                { "data": "tip" },
                { "data": "acceptedDateTime" },
                { "data": "scheduleDateTime" },

            ]);
        }

        var returnedData = _merchant.bindMerchantsToSelect('merchantList');

        $('#merchantList').on('change', function(e) {
            merchantId = $(this).val();

            if (merchantId > 0) {
                _merchant.getData('merchant/support/list/' + merchantId, 'GET', {})
                    .then(function (data) {
                        var _dt = data.data;
                        $('#m-logoUrl').attr('src', _dt.logoUrl);
                        bindToHtml('m-notes', _dt.notes);
                        bindToHtml('m-nameEN', '<a href="/admin/merchant/details/'+_dt.id+'" >'+_dt.nameEN+'</a>');
                        bindToHtml('m-location', _dt.country + ', ' + _dt.latitude + ',' + _dt.longitude);
                        bindToHtml('m-phone', _dt.phone);
                        bindToHtml('m-email', _dt.email);
                        $('span.select2').css('width', '100%');
                        $('#container-hide').show();
                        $('#merchant-panel').show();
                        fillDataTable(duration)
                    }, function(err) {});
            } else {
                $('#container-hide').hide();
                $('#merchant-panel').hide();
            }
        })

        function bindToHtml(containerId, value) {
            $('#' + containerId).html(value);
        }

       
    }())