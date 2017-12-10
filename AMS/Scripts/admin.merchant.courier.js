$(document).ready(function () {
    $('.select2').css('width', "100%");
    var datatable, data;
    var _dt = new DataTableEntry();
    var _merchant = new Merchant();
    var mrCid = localStorage.getItem('merchantCourierId')
    var merchArr = [];
    _merchant.bindMerchantsToSelect('merchantList').then(function (response) {
        merchArr = response;
        if (mrCid) {
            fillDataTable(mrCid);
            $('#merchantList').select2({
                data: merchArr.sort(function (a, b) {
                    if (a.text < b.text) return -1;
                    if (a.text > b.text) return 1;
                    return 0;
                })
            }).val(parseInt(mrCid)).trigger('change.select2');
        } else {
            fillDataTable($('#merchantList').val());
        }
    });
    
    $('#merchantList').on('change', function (e) {
        var merchantId = $(this).val();
        if (merchantId) {
            $('#courierList').show();
            fillDataTable(merchantId);
            localStorage.setItem('merchantCourierId', merchantId);

        }
    });

    function fillDataTable(merchantId) {
        $('.pg-notfound').hide();
        if (datatable) datatable.destroy();
        datatable = _dt.bindDataTable('#courierList', [0, 1, 2, 3],
            function (data, a, b, c) {
                if (c.col == 3)
                    return `<div class="pull-right">
                    <a class ="btn btn-secondary btn--icon-text" href= "merchantcourier/Edit/`+ b.id + `" ><span><i class ="zmdi zmdi-edit zmdi-hc-fw"></i></span></a>
                    <a class ="btn btn-danger btn--icon-text" data-did="`+ b.id + `" href= "merchantcourier/Delete/` + b.id + `" ><span><i class ="zmdi zmdi-delete zmdi-hc-fw"></i></span></a>
               </div> `
                else
                    return data;
            }, 'merchant/courier/list', 'POST', { merchantId: merchantId }, [
                //{ "data": "id" },
                { "data": "name" },
                { "data": "mobile" },
                { "data": "merchant" }
            ]);
        $('#courierList').on('draw.dt', function () {
            data = $('#courierList tbody tr');
        });
    }
    $('input').on('keyup', function (e) {
        var searchVal = $(this).val();
        var objArr = [];
        for (var i = 0; i < data.length; i++) {
            if (data[i].cells[0].innerText.toLocaleLowerCase().includes(searchVal.toLocaleLowerCase())) objArr.push(data[i]);
        }
        if (objArr.length == 0) objArr.push('<tr><td colspan="9" class="dataTables_empty">No Match</td></tr>')
        $('#courierList tbody').empty();
        for (var i = 0; i < objArr.length; i++) {
            $('#courierList tbody').append(objArr[i]);
        }
        $('#courierList tbody').html(objArr);
    });
});
//<a class ="btn btn-secondary btn--icon-text" href= "Details/`+b.id+`" ><span><i class ="zmdi zmdi-more zmdi-hc-fw"></i></span></a>