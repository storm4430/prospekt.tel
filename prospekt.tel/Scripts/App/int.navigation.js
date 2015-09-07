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
$('.actionhref').click(function (e) {
    var __catId = '';
    var __subCatId = '';
    var __catName = '';
    var __subCatName = '';
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
    e.preventDefault();
})

$('.modalCont').click(function () {
    var path = $(this).data('ajaxpath');
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
};

TypeAhead = {
    // suggestionTemplate = {
    //  handlebars: '#id' | 'строка' - т.е. id элемента с шаблоном или можно подавать сразу строку, например '{{FirstName}} {{LastName}}'
    //  ko: '#id' - id элемента с шаблоном
    //  titleField: 'FullFio' - имя поля для отображения
    // emptyTemplate - '' | строка
    //}
    init: function (containerID, remoteUrl, minLen, limit, idFieldName, suggestionTemplate, emptyTemplate, selectCallback) {

        var paclist = new Bloodhound({
            datumTokenizer: Bloodhound.tokenizers.obj.whitespace(idFieldName, suggestionTemplate.titleField),
            queryTokenizer: Bloodhound.tokenizers.whitespace,
            limit: limit,
            remote: {
                url: remoteUrl,
                wildcard: '%QUERY',
                replace: function (url, query) {
                    $(containerID).prev('.tt-hint')
                        .css('background-image', 'url("/content/img/loading16.gif")')
                        .css('background-repeat', 'no-repeat')
                        .css('background-origin', 'content-box')
                        .css('-moz-background-origin', 'content-box')
                        .css('-webkit-background-origin', 'content-box')
                        .css('background-position', 'right');
                    return url.replace(this.wildcard, encodeURIComponent(query));
                },
                filter: function (parsedResponse) {
                    $(containerID).prev('.tt-hint').css('background-image', 'none');
                    return parsedResponse;
                }
            }
        });

        if (emptyTemplate == '') {
            emptyTemplate =
                '<div class="empty-message">\
                <div class="alert alert-danger" role="alert" style="margin:5px"><span class="fa fa-exclamation-circle"></span>Поиск не вернул результатов</div>\
                </div>';
        }

        paclist.initialize();

        $(containerID).typeahead('destroy');

        $(containerID).typeahead({
            highlight: true,
            minLength: minLen,
        },
        {
            source: paclist.ttAdapter(),
            name: idFieldName,
            display: function (v) {
                var str = this.templates.suggestion(v);

                if (str == undefined)
                    return '';

                var regex = /(<([^>]+)>)/ig,
                    body = str.trim(),
                    result = body.replace(regex, "");
                return result;
            },
            templates: {
                suggestion: function (item) {
                    if (suggestionTemplate) {
                        if (suggestionTemplate['handlebars'] != undefined) {
                            var source = '';

                            if (suggestionTemplate['handlebars'].substr(0, 1) == '#')
                                source = $(suggestionTemplate.handlebars).html();
                            else
                                source = suggestionTemplate.handlebars;

                            if (source == undefined || source == '')
                                return item;

                            var template = Handlebars.compile(source);
                            return template(item);
                        }
                        else if (suggestionTemplate['ko'] != undefined) {
                            var temp = document.createElement("div");
                            //var model = item;
                            ko.renderTemplate(suggestionTemplate['ko'], item, null, temp, "replaceChildren");
                            return temp.innerHTML;
                        }
                        else if (suggestionTemplate['ko'] != undefined) {
                            var temp = document.createElement("div");
                            //var model = item;
                            ko.renderTemplate(suggestionTemplate['ko'], item, null, temp, "replaceChildren");
                            return temp.innerHTML;
                        }
                        else if (suggestionTemplate['titleField'] != undefined) {
                            return item[suggestionTemplate['titleField']];
                        }
                    }

                    return item;
                },
                empty: emptyTemplate

            }
        }).on('typeahead:selected', function (event, data) {
            selectCallback(event, data);
        });
    },

    disable: function (el) {
        $(el).css("background-color", "#eee");
    },
    enable: function (el) {
        $(el).css("background-color", "");
    },

    substringMatcher: function (strs, titleFieldName) {
        return function findMatches(q, cb) {
            var matches, substringRegex;

            // an array that will be populated with substring matches
            matches = [];

            // regex used to determine if a string contains the substring `q`
            substrRegex = new RegExp(q, 'i');

            // iterate through the pool of strings and for any string that
            // contains the substring `q`, add it to the `matches` array
            $.each(strs, function (i, str) {
                if (substrRegex.test(str[titleFieldName])) {
                    matches.push(str);
                }
            });

            cb(matches);
        };
    },

    initlocal: function (containerID, localdata, minLen, limit, idFieldName, titleFieldName, selectCallback) {
        // constructs the suggestion engine
        //var localBloodhound = new Bloodhound({
        //    datumTokenizer: Bloodhound.tokenizers.obj.whitespace(idFieldName, titleFieldName),
        //    queryTokenizer: Bloodhound.tokenizers.whitespace,
        //    local: localdata
        //});

        //localBloodhound.initialize();

        $(containerID).typeahead('destroy');

        $(containerID).typeahead({
            //hint: true,
            highlight: true,
            minLength: minLen
        },
        {
            name: idFieldName,
            display: titleFieldName,
            //source: localBloodhound.ttAdapter(),
            source: TypeAhead.substringMatcher(localdata, titleFieldName)
        }).on('typeahead:selected', function (event, data) {
            selectCallback(event, data);
        });
    }
}
