 var coords = {};

 function init() {
     navigator.geolocation.getCurrentPosition(function(position) {
         coords = { latitude: position.coords.latitude, longitude: position.coords.longitude };
         initMap(coords);
     });
 }
 var coords = {}

 function initMap(data) {
     var map = new google.maps.Map(document.getElementById('map'), {
         zoom: 18,
         center: new google.maps.LatLng(data.latitude, data.longitude),
         mapTypeId: google.maps.MapTypeId.ROADMAP,
         mapTypeControl: false,
         streetViewControl: false
     });
     var marker;

     marker = new google.maps.Marker({
         position: new google.maps.LatLng(data.latitude, data.longitude),
         draggable: true,
         //icon: '/Content/images/w_marker.png',
         map: map
     });
     google.maps.event.addListener(marker, 'dragend', function(event) {
         coords = { latitude: this.getPosition().lat(), longitude: this.getPosition().lng() };
         getList(coords);
     });
     marker.setMap(map);

     google.maps.event.addListenerOnce(map, 'idle', function() {
         $('#pac-input').fadeIn(1000);
         getList(data);
     });
     google.maps.event.addListener(map, 'click', function(event) {
         coords = { latitude: event.latLng.lat(), longitude: event.latLng.lng() };
         getList(coords);

         marker.setMap(null);
         marker = new google.maps.Marker({
             position: new google.maps.LatLng(coords.latitude, coords.longitude),
             draggable: true,
             //icon: '/Content/images/w_marker.png',
             map: map
         });
         marker.setMap(map);


         google.maps.event.addListener(marker, 'dragend', function(event) {
             coords = { latitude: this.getPosition().lat(), longitude: this.getPosition().lng() };
             getList(coords);
         });
     });
     var input = document.getElementById('pac-input');
     var autocomplete = new google.maps.places.Autocomplete(input);
     map.controls[google.maps.ControlPosition.TOP_LEFT].push(input);


     autocomplete.bindTo('bounds', map);
     autocomplete.addListener('place_changed', function() {

         marker.setVisible(false);
         var place = autocomplete.getPlace();
         if (!place.geometry) {
             // User entered the name of a Place that was not suggested and
             // pressed the Enter key, or the Place Details request failed.
             window.alert("No details available for input: '" + place.name + "'");
             return;
         }

         // If the place has a geometry, then present it on a map.
         if (place.geometry.viewport) {
             map.fitBounds(place.geometry.viewport);
         } else {
             map.setCenter(place.geometry.location);
             map.setZoom(17); // Why 17? Because it looks good.
         }
         marker.setPosition(place.geometry.location);
         marker.setVisible(true);

         var address = '';
         if (place.address_components) {
             address = [
                 (place.address_components[0] && place.address_components[0].short_name || ''),
                 (place.address_components[1] && place.address_components[1].short_name || ''),
                 (place.address_components[2] && place.address_components[2].short_name || '')
             ].join(' ');
         }
     });

 }

 function getList(data, callback) {
     $('.wloader').show();
     $('.coords').text(data.latitude + ',' + data.longitude).select();
     data.lang = 2;
     $.ajax({
         url: WeeloApi + 'user/store',
         type: "POST",
         data: data,
         success: function(response) {
             loadData(response);
             $('.wloader').hide();
         },
         headers: requestHeaders(),
         dataType: "json",
     });
 }

 function loadData(o) {
     if (o.data != null) {
         $('#map-country').text(o.data.country);
         $('#map-city').text(o.data.city);
         $('#map-area').text(o.data.area);
         $('#map-street').text(o.data.street);
         $('#server_msg').empty().hide();
         $('#rst_data').show();
     } else {
         $('#rst_data').hide();
         $('#server_msg').show().text(o.message);
     }
     $('.map-data').animate({ opacity: 1, top: "0" }, 300, function() {});
 }
 init();