  (function() {
      var _merchant = new Merchant();
      var merchantId, email, password, conId;
      var hub = new SignalREntry();
      var notificationHub = hub.getConnectionHub();
      var startHub = hub.getHubStarted();
      _merchant.bindMerchantsToUl('merchantList').then(function(data) {
          var ele = $('#merchantList');
          var result = data.data;
          console.log(result);
          for (var i = 0; i < result.length; i++) {

              var merch = `
                   <a class ="listview__item chat__available" data-logourl="`+ result[i].logoUrl+`" data-isonline="`+result[i].isOnline+`" data-name= "`+result[i].nameEN+`" data-email= "`+result[i].email+`" data-id= "`+  result[i].id + `"data-conid="` + result[i].connectionId + `" >
                        <img src="`+result[i].logoUrl+`" class="listview__img" alt="">
                            <h3 class="listview__heading">`+result[i].nameEN+`</h3>
                    </a>
                  `
             
              //'<li class="listview__heading" style="cursor:pointer;" data-id="' + result[i].id + '" data-conid="' + result[i].connectionId + '" data-email="' + result[i].email + '">' + result[i].nameEN + '</li> '
             
              ele.append(merch)
          }
          $('button').attr('disabled', 'true');
          //console.log($('a[data-isonline=0]'));
          //$('a[data-isonline=0]').next('.chat__available:before').css('background-color', '#efefef')
      }, function(err) {});
      $(document).on('click', '#merchantList a', function() {
          merchantId = $(this).data('id');
          email = $(this).data('email');
          name = $(this).data('name');
          conId = $(this).data('conid');
          logoUrl=$(this).data('logourl');
          status = (parseInt($(this).data('isonline')) === 0) ? 'Offline' : 'Online';

          $('button').removeAttr('disabled');
          $('.section').show();
          $('.merch').hide();
          $('#m-img').attr('src',logoUrl);
          $('#m-email').html(email);
          $('#m-name').html(name);
          $('#m-status').html(status);
      });

      notificationHub.client.userDeleteCookies = function(cookieName) {
          Cookies.remove(cookieName);
          location.reload();
      }
      notificationHub.client.userAddCookies = function(cookieName, value) {
          Cookies.set(cookieName, value, { expires: 365 });
          location.reload();
      }
      notificationHub.client.connect = function (id, updatedConId) {
          $("a[data-id=" + id + "]").data("conid", updatedConId);
          conId = updatedConId;
      }

      startHub.done(function() {
          $('#signIn').on('click', function() {
              loginMerchant();
          });
          $('#logOut').on('click', function() {
              deleteMerchant();
          });
      });

      function deleteMerchant() {
          console.log(merchantId);
          notificationHub.server.userCookies("_token", merchantId, '', "Delete");
          notificationHub.server.userCookies("_weeloAUTH", merchantId, '', "Delete");
          $('.merchant-container').css('display', 'none');
      }

      function loginMerchant() {
          _merchant.getData("admin/mercahnt/signIn", "GET", { id: merchantId }).then(function(data) { console.log("done"); }, function(err) { console.log(err); })
          $('.merchant-container').css('display', 'none');
      }
  }());