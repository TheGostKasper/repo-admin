$(document).ready(function () {
    var _dt = new DataTableEntry();
    var datatable;
    if (datatable) datatable.destroy();
    datatable = _dt.bindDataTable('#itemList', [0, 1, 2, 3, 4, 5, 6, 7,8],
        function (data, a, b, c) {
            if (c.col == 1) {
                var urlChunks = data.split('/');
                var imgName = urlChunks[urlChunks.length - 1];
                var imgSrc = (imgName != '') ? "http://weelo.s3-website-us-east-1.amazonaws.com/155x155/" + imgName : '/store/content/images/no-image-icon-15.png';
                var resHtml = (imgName != '') ? '<div class ="progress sm"><img src="/store/content/images/loader.gif"/></div><img class="prog" style="height: 76px;" src="' + imgSrc + '" />' : '<img class="prog" style="height: 76px;" src="' + imgSrc + '" />'
                return resHtml;
            }
            if (c.col == 7) return moment(data).format('DD/MM/YYYY hh:mm:ss A')
            if (c.col == 8)
                return '<div class="pull-right"><a class="btn btn-secondary btn--icon-text" href="/admin/item/Edit/' + b.id + '"><span><i class="zmdi zmdi-edit zmdi-hc-fw"></i></span></a><a class="btn btn-danger btn--icon-text" href="/admin/item/Delete/' + b.id + '" data-did="' + b.id + '"><span><i class="zmdi zmdi-delete zmdi-hc-fw"></i></span></a></div>'
            else
                return data;
        }, 'product/pagination', 'POST', {}, [
            { "data": "barcode" },
            { "data": "imageUrl" },
            { "data": "nameEN" },
            { "data": "nameAR" },
            { "data": "volume" },
            { "data": "subCategoryName" },
            { "data": "unitName" },
        { "data": "creationDate" }
        ], function (setting, json) {
        });
    $('#itemList').on('draw.dt', function () {
        data = $('#itemList tbody tr');
        $('img').on('load', function () { var _this = $(this); _this.parent().find('.progress').hide(); })
    });

    $('input').on('keyup', function (e) {
        var searchVal = $(this).val();
        var objArr = [];
        for (var i = 0; i < data.length; i++) {
            if (data[i].cells[0].innerText.includes(searchVal)) objArr.push(data[i]);
        }
        if (objArr.length == 0) objArr.push('<tr><td colspan="9" class="dataTables_empty">No Match</td></tr>')
        $('#itemList tbody').empty();
        for (var i = 0; i < objArr.length; i++) {
            $('#itemList tbody').append(objArr[i]);
        }
        $('#itemList tbody').html(objArr);
    });
    $('#itemList tfoot th').each(function () {
        var title = $(this).text();
        $(this).html('<input type="text" placeholder="Search ' + title + '" />');
    });
    $("[aria-controls='itemList']").on('keyup', function () {
        var i = $(this).attr('barcode');
        var v = $(this).val();
        datatable.columns(i).search(v).draw();
    });
    $(document).on('click', ".imageContainer", function () {
        $('.modal-title').html($(this).parent().parent().next().html());
        var imgSrc = "/admin/Content/images/placeholder.gif";
        if ($(this).attr('src') != imgSrc) {
            var urlChunks = $(this).attr('src').split('/');
            var imgName = urlChunks[urlChunks.length - 1];
            //imgSrc = "http://weelo.s3-website-us-east-1.amazonaws.com/500x500/" + imgName;
            imgSrc=$(this).attr('src')
        }
        $('#image-preview').attr('src', imgSrc);
        $('#myModal').modal('show')
    });
});