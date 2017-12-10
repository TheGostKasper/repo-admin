$(document).ready(function () {
	var _dt = new DataTableEntry();
	var datatable;
	if (datatable) datatable.destroy();
	datatable = _dt.bindDataTable('#merchantList', [0, 1, 2, 3, 4, 5, 6],
        function (data, a, b, c) {
        	if (c.col == 0) {
        		data = (data != "") ? data : "/Content/images/placeholder.gif";
        		return '<div ><img class="imageContainer" src="' + data + '" style="width:90px; height:90px;"/></div>'
        	}
        	if (c.col == 5)
        		return '<div class="btn-group btn-group-sm btn-group-right pull-right" role="group" aria-label="..."><div class="btn-group" role="group"><a class="btn btn-sm btn-default" href="~/merchant/Edit/' + b.id + '">Edit</a></div><div class="btn-group" role="group"><a class ="btn btn-sm btn-default" href= "~/merchant/Details/' + b.id + '" >Details</a></div></div>'
        	if (c.col == 6) {
        		var orderTime = moment.utc(data).toDate();
        		return moment(orderTime).format('DD/MM/YYYY hh:mm:ss A')
			}
        	else
        		return data;
        }, 'product/pagination', 'POST', {}, [
            { "data": "nameEN" },
            { "data": "nameAR" },
            { "data": "phone" },
            { "data": "Status" },
            { "data": "Currency" },
            { "data": "Creation Date" }
        ]);
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
		$('#image-preview').attr('src', $(this).attr('src'));
		$('#myModal').modal('show')
	});
});