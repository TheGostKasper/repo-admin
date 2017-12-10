  (function() {
      var _merchant = new Merchant();

      _merchant.bindMerchantsToSelect('merchantList');
      $('span.select2').css('width', "100%");
      $('select').on('change', function(e) {

          merchantId = $(this).val();
          if (merchantId) {
              getMarkers(parseInt(merchantId));
          }
      });

      function getMarkers(merchantId) {
          $.ajax({
              url: WeeloApi + 'admin/servingAreaMarkers?merchantId=' + merchantId,
              type: "GET",
              success: function(data) {
                  initMap(data.data);
              },
              headers: requestHeaders(),
              dataType: "json",
          });
      }

      function initMap(data) {

          var map = new google.maps.Map(document.getElementById('map'), {
              zoom: 2,
              center: new google.maps.LatLng(data[0].latitude, data[0].longitude),
              mapTypeId: google.maps.MapTypeId.ROADMAP
          });

          var infowindow = new google.maps.InfoWindow();

          var marker, i;

          for (i = 0; i < data.length; i++) {
              marker = new google.maps.Marker({
                  position: new google.maps.LatLng(data[i].latitude, data[i].longitude),
                  map: map
              });
              google.maps.event.addListener(marker, 'click', (function(marker, i) {
                  return function() {
                      infowindow.setContent(data[i].name);
                      infowindow.open(map, marker);
                  }
              })(marker, i));
          }
      }
  }())