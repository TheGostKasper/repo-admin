  (function() {

      $('span.select2').css('float', 'right');
      var status = true;
      var statusUm = true;
      var falge = false;
      var progress = 0;
      var files, id, img_src, merchantDataTable, datatable;
      var width = 5;
      var merchantId = 0;
      var path = "";
      var MatchedProduct = [];
      var UnMatchedProduct = [];

      var _dt = new DataTableEntry();
      if (datatable) datatable.destroy();

      var elem = document.getElementById("myBar");

      $('input[type=file]').on('change', prepareUpload);

      function prepareUpload(event) {
          files = event.target.files;
          $('#inputFile').val(event.target.files[0].name);
      }

      $('#MerchantDetails').on('change', function() {
          $('.progress').hide();
          $('#Submit').removeAttr("disabled");
          merchantId = $(this).val();
          if ($(this).val() != '') {
              $.get("MerchantProduct/Merchant", { id: merchantId },
                  function(data) {
                      $('#conDiv').removeClass('hidden');
                      $('#loaderGif').addClass('hidden');

                  })
          } else {
              $('#conDiv').addClass('hidden');
          }
      })
      $('#fileForm').on('submit', uploadFiles);

      function uploadFiles(event) {
          event.stopPropagation();
          event.preventDefault();
          NProgress.start();
          var formElement = document.querySelector("#fileForm");
          var formData = new FormData(formElement);

          $.ajax({
              url: "MerchantProduct/Upload",
              data: formData,
              processData: false,
              type: 'POST',
              contentType: false,
              processData: false,
              success: function(data) {
                  path = data;
                  if (falge) dataTable.draw();
                  else getExcelData();
                  NProgress.done();
                  $('#loaderGif').addClass('hidden');

              }
          });
          status = true;
      }
      $('#ReadyToInsert').on('click', function ReadyToInsert() {
          if (!status)
              InsertProd();
          else
              alert("Check un-matched tab!");
      });
      $('#rdMatced').on('click', function ReadyToInsert() {
          if (!status)
              InstMatched();
          else
              alert("Check un-matched tab!");
      });

      function getExcelData() {
          falge = true;
          $('#loaderGif').removeClass('hidden');
          datatable = _dt.bindDataTableAction('#originalData2', [],
              function(data, a, b, c) {
                  return data;
              }, 'MerchantProduct/ReadExcelDatatable', 'POST', { Path: path }, [
                  { "data": "Price" },
                  { "data": "Barcode" },
                  { "data": "NameAR" },
              ]);
          $('#tabs').removeClass('hidden');
          $('#loaderGif').addClass('hidden');
      }

      $("#matchProduct").on('click', function() {
          if (status) {
              NProgress.start();
              $('#loaderGif').removeClass('hidden');
              $.post('GetProductStatusByNames', { path: path }).then(function(data) {
                  var requestedData = JSON.parse(data);
                  MatchedProduct = requestedData.MatchedProduct;
                  BindData(requestedData.MatchedProduct, "#Matched");
                  $('#loaderGif').addClass('hidden');
                  NProgress.done();
              }, function(err) { alert("Try again later!"); });

              status = false;
          }
      });

      $("#umnatchedProduct").on('click', function() {
          if (statusUm) {
              NProgress.start();
              $('#loaderGif').removeClass('hidden');
              $.post('GetProductUnMatchedStatusByNames', { path: path }).then(function(data) {
                  var requestedData = JSON.parse(data);
                  UnMatchedProduct = requestedData.UnmatchedProduct;
                  BindData(requestedData.UnmatchedProduct, "#UnMatched");
                  $('#loaderGif').addClass('hidden');
                  NProgress.done();
              }, function(err) { alert("Try again later!"); });
              statusUm = false;
          }
      });

      function BindData(requestedData, tableId) {
          merchantDataTable = $(tableId).DataTable({
              data: JSON.parse(JSON.stringify(requestedData)),
              columnDefs: [{
                  searchable: true,
                  sortable: true,
                  targets: [0, 1, 2, 3],
                  render: function(data, a, b, c) {
                      if (c.col == 3) {
                          img_src = (data) ? data : "Content/images/placeholder.gif"
                          return '<img class="imageContainer" style="width:90px;height:80px;" src="' + img_src + '"/>'
                      } else return data;
                  }
              }],
              columns: [
                  { "data": "Price" },
                  { "data": "Barcode" },
                  { "data": "NameAR" },
                  { "data": "ImageUrl" }
              ]
          });
      }

      function InstMatched() {
          $('#myProgress').removeClass('hidden');
          id = setInterval(function() {
              width += 1;
              $('#myBar').css('width', width + 'px');
          }, 1000);
          $('#loaderGif').removeClass('hidden');
          var _merchantComparedProducts = {
              MatchedProduct: MatchedProduct,
              UnmatchedProduct: UnMatchedProduct,
              MerchantId: parseInt(merchantId)
          }
          $.ajax({
              url: "MerchantProduct/InsertData",
              data: _merchantComparedProducts,
              type: 'POST',
              success: function(data) {
                  clearInterval(id);
                  $('#myProgress').addClass('hidden');
                  $('#loaderGif').addClass('hidden');
              }
          });

      }

      function InsertProd() {
          $('#myProgress').removeClass('hidden');
          NProgress.start();
          id = setInterval(function() {
              width += 1;
              $('#myBar').css('width', width + 'px');
          }, 1000);
          $('#loaderGif').removeClass('hidden');

          var _merchantComparedProducts = {
              MatchedProduct: MatchedProduct,
              UnmatchedProduct: UnMatchedProduct,
              MerchantId: parseInt(merchantId)
          }

          $.ajax({
              url: "MerchantProduct/InsertUnMatechedData",
              data: _merchantComparedProducts,
              type: 'POST',
              success: function(data) {
                  NProgress.done();
                  clearInterval(id);
                  $('#myProgress').addClass('hidden');
                  $('#loaderGif').addClass('hidden');
              }
          });
      }
      $(document).on('click', ".imageContainer", function () {
          $('.modal-title').html($(this).parent().prev().html());
          $('#image-preview').attr('src', $(this).attr('src'));
          $('#myModal').modal('show')
      });
      $.getScript("/Scripts/jquery-ui.min.js").then(function () {
          $("#tabs").tabs();
      })
  }());