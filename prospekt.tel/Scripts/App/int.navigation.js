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
    ProgressInAction('1');
    $.get(ucnavUrl, function (data) {
        $('#mainContainer').empty().html(data);
    }).success(function () {
        ProgressInAction('0');
    }).error(function () {
        $('#mainContainer').empty().html(errPage);
        ProgressInAction('0');
    })
})

$('.modalCont').click(function () {
    var path = $(this).data('ajaxpath');
    console.log(path);
    $('#ModMessages').modal({
        keyboard: false
    }, 'show');
})

function ProgressInAction(act) {

    if (act == '1') {
        $('#progCont').attr('hidden', false);
        $('#progBody').css('width', '50%');
    }
    else {
        $('#progBody').css('width', '100%');
        setTimeout(function () {
            $('#progCont').attr('hidden', true);
        }, 500);

    }
}
