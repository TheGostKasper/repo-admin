 (function() {
     var od = new OrderDetails();
     var arr;
     var categories;
     od.getOnlyData('category/list', {}, 'GET').then(function(data) {
         var remote = $('#sortable');
         categories = data.data;
         for (var i = 0; i < categories.length; i++) {
             remote.append('<li class="ui-state-default" id="' + categories[i].id + '" data-displayOrder="' + categories[i].displayOrder + '"><img class="img-cat" src="' + categories[i].imageUrl + '"/><span class="ui-icon ui-icon-arrowthick-2-n-s"></span>' + categories[i].nameEN + '</li>')
         }
     }, function(err) {

     })
     $("#sortable").sortable({
         start: function() {
             $("span").text("");
         },
         stop: function(event, ui) {
             arr = $.map($(this).find('li'), function(el) {
                 return el.id;
             });
         },
         change: function(event, ui) {
             // When the item postion is changed while dragging a item, I want it's position
             //$("span").text(ui.placeholder.index());

         }
     });
     $("#sortable").disableSelection();
     $('#displayOrderChanges').on('click', function() {
         var result = displayOrd_Obj(arr);
         var obj = {
             Id: 0,
             Ctgs: result
         }
         $.post('UpdateOrderDisplay', obj, function (data) { alert('done');});
     })

     function displayOrd_Obj(arr) {
         var result = [];
         arr.forEach(function(e, indx) {
             result.push({
                 id: e,
                 index: indx
             });
         });
         return result;
     }
 }());