function GetDataTable(items, apiPath, pageSize, container) {
    console.log(container);
    //ko.cleanNode(document.getElementById('subContainer'));
    
    var presets = items.fields[0];
    var actions = items.actions[0];
    $('#preLoader').show(50);
    if (pageSize === null) {
        pageSize = 15;
    }
    $('#' + container).empty().fadeOut(500).html('<div id="subContainer_' + container + '"><table class="table table-responsive table-condensed table-striped"  style="margin-bottom:1px;" id="mTable' + container+ '"><thead><tr id="tHeaders' + container+ '"></tr></thead><div></div><tbody data-bind="foreach: Rows" style="cursor:pointer" id="tMainBody' + container+ '"></tbody></table></div>').fadeIn(200);

    ///Инициализация заголовков таблицы 
    var t = '';
    for (var i in presets) {
        t += '<th>' + presets[i] + '</th>';
    }
    t += '<th></th>'
    $('#tHeaders' + container).append(t);
    

    ///Инициализация тела таблицы
    var tb = '<tr>';
    for (var i in presets) {
        tb += '<td  data-bind="click: function(){$parent.AssignDetails($data, ' + actions.Update + ')};, text: ' + i + '"></td>'
    }
    
    tb += '<td>\
        <button data-bind="click: function(){$parent.EditObject($data, ' + actions.Update + ')};" class="btn btn-xs btn-default btn-circle" title="Редактировать"><span class="fa fa-edit"></span></button>\
        <button data-bind="click: function(){$parent.RemoveObject($data,' + actions.Delete + ')};" class="btn btn-xs btn-warning btn-circle" title="Удалить"><span class="fa fa-trash"></span></button></td>';
    $('#tMainBody' + container).append(tb);
    ///Инициализация футера таблицы
    //var tf = '';
    //for (var i in items) {
    //    tf += '<th>' + i + '</th>'
    //}
    //$('#mtFooter').append(tf);
    //ko.cleanNode(document.getElementById('subContainer_' + container));
    ko.applyBindings(new GetAssignments(apiPath), document.getElementById('subContainer_' + container));

}


function GetAssignments(dataPath) { //'/api/assignments/2036531'
    var self = this;
    
    self.AssignDetails = function (assign, act) {
        switch (act) {
            case ('Category'): { CategoryToSubdirectory(assign.id) };
            default:
        }
    }
    self.RemoveObject = function (assign, act) {
        switch (act) {
            case ('Category'): { CategoryDelete(assign.id) };
            default:
        }
    }
    self.EditObject = function (assign, act) {
        switch (act) {
            case ('Category'): { CategoryEdit(assign.id) };
            default:

        }
    }
    self.Rows = ko.observableArray();

    // getData for report
    // —---
    self.getData = function () {
        $.get(dataPath, function (ajaxData) {
            //self.Rows.removeAll();
            self.Rows.splice(0);
            //self.Rows.valueHasMutated();
            self.Rows(ajaxData);
            $('#tfoot').empty().html('<p>Записей в таблице: ' + ajaxData.length);
        }).success(function () {
            $('#preLoader').hide(200);

        });
    }

    //------------------------------------------------------------------------
    // Init
    //------------------------------------------------------------------------
    self.init = function () {
        self.getData();
    }

    self.init();
};

function NewObject(dataURL, obj, objEntity) {
    $.ajax(dataURL,
        {
            method: "POST",
            data: obj,
            success: function () {
                switch (objEntity) {
                    case (1): GetCategories('');
                }
            }
        });
}

function UpdateObject(dataURL, obj, objEntity) {
    $.ajax(dataURL,
        {
            method: "PUT",
            data: obj,
            success: function () {
                switch (objEntity) {
                    case (1): GetCategories('');
                }
            }
        });
}

function DeleteObject(dataURL, objEntity) {
    $.ajax(dataURL,
        {
            method: "DELETE",
            success: function () {
                switch (objEntity) {
                    case (1): GetCategories('');
                }
            }
        });
}