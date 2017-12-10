function ManageLocation() {
    function bindDataArraySelect2($this, endPoint, data, type, remoteSelection, remoteHiddenClass) {
        var _id = $this.val();
        if (_id > 0)
            getListById(endPoint, data, type)
            .then(function (data) {
                $(remoteHiddenClass).removeClass('hide');
                var _remote = $(remoteSelection);

                _remote.html('').select2({ data: { id: null, text: null } });
                _remote.select2({
                    data: getSelect2Objs(data.data)
                });
            }, function (err) { });
    }

    function getListById(endPoint, data, type) {
        return $.ajax({
            url: WeeloApi + endPoint,
            data: data,
            type: type,
            headers: requestHeaders(),
            dataType: "json",
        });
    }

    function getSelect2Objs(_data) {
        var _List = [];
        _List.push({ id: 0, text: "--Select--" });
        if (_data)
            for (var i = 0; i < _data.length; i++) {
                _List.push({ id: _data[i].id, text: _data[i].name });
            }
        return _List;
    }


    function geocodeLatLng(geocoder, latlng, geocodeResult) {

        geocoder.geocode({ 'location': latlng }, function (results, status) {
            if (status === 'OK') {
                if (results[0]) {
                    var stName = results[0].formatted_address.split(',');
                    $(geocodeResult).val(stName[0] + ',' + stName[1]);
                } else {
                    window.alert('No results found');
                }
            } else {
                window.alert('Geocoder failed due to: ' + status);
            }
        });
    }

    function bindMap(mapId, LatitudeId, LongitudeId, geocoder, geocodeResult) {
        myLatLong = { lat: parseInt($(LatitudeId).val()), lng: parseInt($(LongitudeId).val()) };
        var map;
        if ($(LatitudeId).val() == '') {
            map = new google.maps.Map(document.getElementById(mapId), {
                center: { lat: -34.397, lng: 150.644 },
                zoom: 16
            });
            getCurrentLocation(map);
        } else {
            map = new google.maps.Map(document.getElementById(mapId), {
                center: myLatLong,
                zoom: 16
            });
        }
        var marker = new google.maps.Marker({
            position: myLatLong,
            map: map,
            draggable: true,
            animation: google.maps.Animation.DROP
        });

        clickMap(map, LatitudeId, LongitudeId, marker, geocoder, geocodeResult);
    }
    function clickMap(map, LatitudeId, LongitudeId, marker, geocoder, geocodeResult) {
        google.maps.event.addListener(marker, 'dragend', function (evt) {
            $(LatitudeId).val(evt.latLng.lat());
            $(LongitudeId).val(evt.latLng.lng());
            geocodeLatLng(geocoder, { lat: parseFloat(evt.latLng.lat()), lng: parseFloat(evt.latLng.lng()) }, geocodeResult);
        });
        google.maps.event.addListener(map, 'click', function (evt) {
            var latLong = { lat: parseFloat(evt.latLng.lat()), lng: parseFloat(evt.latLng.lng()) };
            marker.setPosition(latLong);
            $(LatitudeId).val(evt.latLng.lat());
            $(LongitudeId).val(evt.latLng.lng());
            geocodeLatLng(geocoder, latLong, geocodeResult);
        });

    }
    function getCurrentLocation(map) {

        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(function (position) {
                applyChanges(position.coords.latitude, position.coords.longitude)
                map.setCenter(myLatLong);
            }, function () {
            });
        } else {
        }

    }

    function applyChanges(lat, lng) {
        myLatLong = {
            lat: lat, lng: lng
        };
    }

    function showMap(mapId, latlng, LatitudeId, LongitudeId, geocodeResult) {
        var geocoder = new google.maps.Geocoder;
        map = new google.maps.Map(document.getElementById(mapId), {
            center: latlng,
            zoom: 16
        });
        var marker = new google.maps.Marker({
            position: latlng,
            map: map,
            draggable: true,
            animation: google.maps.Animation.DROP
        });

        clickMap(map, LatitudeId, LongitudeId, marker, geocoder, geocodeResult);
    }
    function initAutocomplete(buildingNameId, Latitude, Longitude) {
        var input = document.getElementById(buildingNameId);
        var autocomplete = new google.maps.places.Autocomplete(input);
        autocomplete.addListener('place_changed', function () {

            var place = autocomplete.getPlace();
            if (!place.geometry) {
                alert("No details available for input: '" + place.name + "'");
                return;
            } else {
                for (var i = 0; i < place.address_components.length; i++) {
                    var lat = place.geometry.location.lat();
                    var lng = place.geometry.location.lng();
                    document.getElementById(Latitude).value = lat;
                    document.getElementById(Longitude).value = lng;
                }
            }
        });
    }
    var myLatLong;
    this.bindDataArraySelect2 = bindDataArraySelect2;
    this.getListById = getListById;
    this.getSelect2Objs = getSelect2Objs;
    this.geocodeLatLng = geocodeLatLng;
    this.bindMap = bindMap;
    this.showMap = showMap;
    this.initAutocomplete = initAutocomplete;
}

