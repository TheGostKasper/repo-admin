 (function() {
     var oDetails = new OrderDetails();
     var intv;
     var dateObj = {}
     var myChart;
     $('#orderTextSearch').on('keypress', function (evt) {
         var val = $(this).val();
         if (evt.which < 48 || evt.which > 57) {
             evt.preventDefault();
         }
         else if (val.toString().length === 3 && evt.which != 8 && val.indexOf('-') < 0) {
             $(this).val(val + '-');
         }
     });

     $('#orderTextSearch').on('keyup', function (e) {
         if (e.keyCode == 13) {
             if ($(this).val() != '')
                 oDetails.getOrder("order/details/" + $(this).val(), {}).then(function (data) {
                     var or = data.data;
                     if (or != null) {
                         bindToHtml('or-m-name', or.merchantName);
                         bindToHtml('or-m-phone', or.merchantPhone);
                         bindToHtml('or-c-notes', or.customer.notes);
                         bindToHtml('or-c-address', or.customer.address);
                         bindToHtml('or-c-apt', or.customer.apt);
                         bindToHtml('or-c-mobile', or.customer.mobile);
                         bindToHtml('or-c-name', or.customer.name);
                         bindToHtml('or-status', or.state);
                         bindToHtml('or-acceptedBy', or.supervisorName);
                         bindToHtml('or-handeledBy', or.courierName);
                         bindToHtml('or-ordernumber', or.orderNumber);
                         bindToHtml('or-grandTotal', or.grandTotal);
                         var _data = data.data.products;
                        
                         var html = '';
                         for (var i = 0; i < _data.length; i++) {
                             var name = (_data[i].nameEN == "") ? _data[i].nameAR : _data[i].nameEN
                             html += '<tr><td>' + name + ' ' + _data[i].size + _data[i].unit + '</td><td>' + _data[i].quantity + 'x</td><td> ' + _data[i].price * _data[i].quantity + ' </td></tr> '
                         }
                         //html += '<tr><td colspan="2"></td><td>Total: ' + data.data.grandTotal + ' <br/>Tip: ' + data.data.tip + '.00 <br/>Discount: ' + data.data.discount + ' %<br/>Tax: ' + data.data.tax + ' %<br/>Service: ' + data.data.serviceFees + ' </td></tr>';

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

                 }, function(err) {
                     $('#or-card').show();
                 });
             else {
                 $('#or-card').hide();
                 $('.not-found').show();
             }
         }
     });

     $('#orderTextSearch').on('keypress', function (evt) {
         var val = $(this).val();
         if ((evt.which < 48 || evt.which > 57)) {
             evt.preventDefault();
         }
         else if (val.toString().length === 3 && evt.which != 8 && val.indexOf('-') < 0) {
             $(this).val(val + '-');
         }
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

     notificationHub.client.updateOrderStatus = function(state, id) {
         if (state == "Delivered" && getId(id) == id) {
             dateObj.deliveredDateTime = new Date().toString();
             updateChart(dateObj);
         }
     };
     notificationHub.client.updateReadyOrder = function(data) {
         if (data.state == "Ready" && getId(data.id) == data.id) {
             dateObj.assignedToDriverDateTime = new Date().toString();
             updateChart(dateObj);

         }
     }
     notificationHub.client.updateAcceptOrder = function(data) {
         if (data.state == "Accepted" && parseInt(getId(data.id)) == data.id) {
             dateObj.acceptedDateTime = new Date().toString();
             updateChart(dateObj);
         }
     }
     notificationHub.client.updateCancelOrder = function(orderId) {
         if (getId(orderId) == orderId) {}
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
     function bindToHtml(containerId, value) {
         $('#' + containerId).html(value);
     }
 }());