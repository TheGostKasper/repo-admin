function Merchant() {


    function bindMerchantsToSelect(selectParentId) {
        var ele = $('#' + selectParentId);
        return getData('merchant/list', "GET", {}).then(function (data) {
            var result = data.data;
            ele.append('<option value="0">-- Select Merchant --</option> ')
            for (var i = 0; i < result.length; i++) {
                ele.append('<option value="' + result[i].id + '">' + result[i].nameEN + '</option> ')
            }
            return result;
        }, function (err) {
        });
    }
    function bindMerchantsToUl() {
        return getData('merchant/list', "GET", {})
    }
    function getToken(email, password, grant_type) {
        return $.ajax({
            url: WeeloApi + 'token',
            type: 'POST',
            data: { username: email, password: password, grant_type: grant_type },
            dataType: "json",
        });
    }
    function getMerchant(email, password) {
        return getData('merchant', 'POST', { UserName: email, Password: password });
    }
    function getData(endPoint, type, data) {
        return $.ajax({
            url: WeeloApi + endPoint,
            type: type,
            data: data,
            headers: requestHeaders(),
            dataType: "json",
        });
    }
    this.getToken = getToken;
    this.getMerchant = getMerchant;
    this.bindMerchantsToUl = bindMerchantsToUl;
    this.bindMerchantsToSelect = bindMerchantsToSelect;
    this.getData = getData;
}