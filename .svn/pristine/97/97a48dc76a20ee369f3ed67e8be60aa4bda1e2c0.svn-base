   (function() {
       var datatable;
       var cuurItemId;
       var merchantId;
       var imgs;
       var _dt = new DataTableEntry();
       var _merchant = new Merchant();
       _merchant.bindMerchantsToSelect('merchantList');
       $('#outOfStock').hide();
       $('select').on('change', function(e) {
           merchantId = $(this).val();
           if (merchantId) {
               $('#outOfStock').show();
               fillDataTable(merchantId);
           };

       });
       $(document).on('click', '#updateStock', function() {
           cuurItemId = $(this).data("id");
       });
       $('#addQuantityToStock').on('click', function() {
           var data = { merchantId: parseInt(merchantId), itemId: cuurItemId };
           _merchant.getData('merchant/updateStock', "POST", data).then(function(data) {
               $('button[data-id="' + cuurItemId + '"]').css('display', 'none');
               $('button[data-id="' + cuurItemId + '"]').parent().parent().css('background', '#9eff9e');
           }, function(err) {});
       })

       function fillDataTable(merchantId) {
           $('.pg-notfound').hide();
           if (datatable) datatable.destroy();
           datatable = _dt.bindDataTable('#outOfStock', [0, 1, 2, 3, 4, 5, 6, 7],
               function(data, a, b, c) {
                   if (c.col == 3) {
                       var urlChunks = data.split('/');
                       var imgName = urlChunks[urlChunks.length - 1];
                       var imgSrc = (imgName != '') ? "http://weelo.s3-website-us-east-1.amazonaws.com/155x155/" + imgName : '/store/content/images/no-image-icon-15.png';
                       var resHtml = (imgName != '') ? '<div class ="progress sm"><img src="/store/content/images/loader.gif"/></div><img class="prog" style="height: 76px;" src="' + imgSrc + '" />' : '<img class="prog" style="height: 76px;" src="' + imgSrc + '" />'

                       return resHtml;
                   }
                   if (c.col == 7) {
                       return '<button class="btn btn-primary" id="updateStock" data-id="' + b.id + '" data-toggle="modal" data-target="#outOfStockModal">Add</button>'
                   } else
                       return data;
               }, 'merchant/outOfStock', 'POST', { merchantId: merchantId }, [
                   { "data": "barcode" },
                   { "data": "nameAR" },
                   { "data": "nameEN" },
                   { "data": "imageUrl" },
                   { "data": "volume" },
                   { "data": "price" },
                   { "data": "subCategoryName" },
                   { "data": "quantity" }
               ], function (settings, json) {
                   
               });
           $('#outOfStock').on('draw.dt', function () {
               data = $('#outOfStock tbody tr');
               $('img').on('load', function () { var _this = $(this); _this.parent().find('.progress').hide(); })
           });
       }
      
       $('input').on('keyup', function (e) {
           var searchVal = $(this).val();
           var objArr = [];
           for (var i = 0; i < data.length; i++) {
               if (data[i].cells[0].innerText.includes(searchVal)) objArr.push(data[i]);
           }
           if (objArr.length == 0) objArr.push('<tr><td colspan="9" class="dataTables_empty">No Match</td></tr>')
           $('#outOfStock tbody').empty();
           for (var i = 0; i < objArr.length; i++) {
               $('#outOfStock tbody').append(objArr[i]);
           }
           $('#outOfStock tbody').html(objArr);
       });
   }());