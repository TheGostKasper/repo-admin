 (function() {
     var dateToPass = "";
     var datatable;
     var _orderId;
     var _dt = new DataTableEntry();
     var oDetails = new OrderDetails();
     var _duration = $('.selectDataTable').val();
     if (_duration)
         fillDataTable(_duration);

     $('#searchRange').on('click', function() {
         var frommyDate = new Date($('#datepicker2From').val());
         var tomyDate = new Date($('#datepicker2To').val());
         var fromDate = _dt.getStringDate(frommyDate);
         var toDate = _dt.getStringDate(tomyDate);
         if ($('#datepicker2To').val() != '' && $('#datepicker2From').val() != '') {

             var timeDiff = Math.abs(tomyDate.getTime() - frommyDate.getTime());
             var diffDays = Math.ceil(timeDiff / (1000 * 3600 * 24));
             dateToPass = toDate;
             fillDataTable(diffDays);
             // $('#colelctionModal').closeModal();
         } else {
             alert('Something went wrong');
         }

     })
     $('#searchOrder').on('click', function() {
         $('#order-search tbody').html('');
         $('#myOrderModal').modal('show');
     });
     $('#searchOrderByNumberOrId').on('click', function() {
         $('#order-search tbody').html('');
         oDetails.getOnlyData("order/details/" + $('#order-number').val(), {}, "GET").then(function(data) {
             if (data.data != null) {
                 var _data = data.data;
                 var html = '<tr><td>' + _data.id + '</td><td>' + _data.orderNumber + '</td><td>' + _data.products.length + '</td><td>' + _data.grandTotal + '</td><td>' + _data.merchantName + '</td><td><a href="/admin/home/orderdetails/' + _data.orderNumber + '" target="_blank" class="x-right">Details</a></td></tr>';
                 $('#order-search tbody').html(html);

                 $('.products-search-Info').removeClass('hide');

             } else {
                 $('.pg-notfound').removeClass('hide');
                 $('.products-search-Info').addClass('hide');
             }

         }, function(err) {
             $('.pg-notfound').removeClass('hide');
             $('.products-search-Info').addClass('hide');
         });
     })
     $('select').on('change', function(e) {
         var duration = $(this).val();
         dateToPass = "";
         if (duration > -1) {
             fillDataTable(duration);

             $('#datepicker2To').val('');
             $('#datepicker2From').val('');
         } else {
             $('#myModal').modal('show')
         }


     })


     function fillDataTable(duration) {
         if (datatable) datatable.destroy();

         var odNum;
         datatable = _dt.bindDataTable('#OrderTransaction', [0, 1, 2, 3, 4, 5, 6, 7, 8],
             function(data, a, b, c) {
                 if (c.col == 0) {
                     odNum = data;
                     return data;//orderNumberFormat(data);
                 }
                 if (c.col == 4 || c.col == 5) {
                     var orderTime = moment.utc(data).toDate();
                     if (orderTime != "Invalid Date")
                         return moment(data).format('DD/MM/YYYY hh:mm:ss A');
                     else return "--";
                 }
                 if (c.col == 7) {
                     if (data == true)
                         return '<a class="btn green"><i class ="material-icons">Yes</i></a>'
                     else
                         return '<i class ="material-icons">Not Yet</i>'
                 }
                 if (c.col == 8)
                     return '<button class="btn btn-primary btnCollect"  data-id=' + odNum + '>Collect</button>'
                 else
                     return data;
             },
             'merchant/transaction', 'POST', { duration: duration, dateToPass: dateToPass }, [
                 { "data": "orderNumber" },
                 { "data": "nameEN" },
                 { "data": "name" },
                 { "data": "grandTotal" },
                 //{ "data": "creationDate" },
                 { "data": "collectionDate" },
                 { "data": "collectionDueDate" },
                 { "data": "notes" },
                 { "data": "isCollected" },
             ]);
     }

     $(document).on('click', '.btnCollect', function() {
         _orderId = $(this).data('id');
         $('#collectModal').modal('show');
     });

     $('#order-collect').on('click', function() {
         oDetails.getOnlyData("merchant/transaction/update/" + _orderId, {}, "GET").then(function(data) {
             if (data.data != null) {
                 var _data = data.data;
                 $('#collectModal').modal('hide');
                
                 $('.btnCollect[data-id="' + _orderId + '"]').parent().parent().css('background-color', '#fff3d2');
                 $('.btnCollect[data-id="' + _orderId + '"]').hide();
                // datatable.draw();
             } else {
                 alert("something went wrong");
             }

         }, function(err) {
             alert("something went wrong");
         });



     })
 }());