var errPage = '<div class="row">\
					<div class="panel panel-red" style="margin-top:35px">\
                        <div class="panel-heading">\
                            ОШИБКА\
                        </div>\
                        <div class="panel-body">\
                            <p>В процессе получения данных произошла ошибка</p>\
                        </div>\
                    </div>\
				</div>'
$('.actionhref').click(function () {
	var sel = this.id;
	var path = $(this).data('url');
	//$('#mPBar').show(100);
	var ucnavUrl = path;
	$.get(ucnavUrl, function (data) {
		$('#page-wrapper').empty().html(data);
	}).success(function () {
		//$('#mPBar').hide(100);
	}).error(function () {
		$('#page-wrapper').empty().html(errPage);
	})
})
