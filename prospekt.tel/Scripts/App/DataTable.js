function GetDataTable(items, apiPath, pageSize, container) {
    //ko.cleanNode(document.getElementById('subContainer'));
    
    var presets = items.fields[0];
    var actions = items.actions[0];
    $('#preLoader').show(50);
    if (pageSize === null) {
        pageSize = 15;
    }
    $('#' + container).empty().html('<div id="subContainer_' + container + '"><table class="table table-responsive table-condensed table-striped"  style="margin-bottom:1px;" id="mTable' + container+ '"><thead><tr id="tHeaders' + container+ '"></tr></thead><div></div><tbody data-bind="foreach: Rows" style="cursor:pointer" id="tMainBody' + container+ '"></tbody></table></div>');

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
            case ('Category'): { __catName = assign.category_desc; CategoryToSubdirectory(assign.id) }; break;
            case ('subCategory'): { __subCatName = assign.subcategory_desc; SubdirectoryToProduct(assign.id) }; break;
            default:
        }
    }
    self.RemoveObject = function (assign, act) {
        switch (act) {
            case ('Category'): { CategoryDelete(assign.id) }; break;
            case ('subCategory'): { SubCategoryDelete(assign.id) }; break;
            case ('Product'): { ProductDelete(assign.id) }; break;
            default:
        }
    }
    self.EditObject = function (assign, act) {
        switch (act) {
            case ('Category'): { CategoryEdit(assign.id) }; break;
            case ('subCategory'): { SubCategoryEdit(assign.id) }; break;
            case ('Product'): { ProductEdit(assign.id) }; break;
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

function NewObject(dataURL, obj, objEntity, opt) {
    $.ajax(dataURL,
        {
            method: "POST",
            data: obj,
            success: function () {
                switch (objEntity) {
                    case (1): GetCategories(''); break;
                    case (2): GetSubCategories(opt, ''); break;
                    case (3): Getproducts(opt, ''); break;
                    case (4): break;
                    case (5): GetOrgs(''); break;
                }
            }
        });
};

function NewInvocObject(dataURL, obj, objEntity, opt) {
    $.ajax(dataURL,
        {
            method: "POST",
            data: obj,
            processData: false,
            contentType: false,
            success: function () {
                switch (objEntity) {
                    case (1): GetCategories(''); break;
                    case (2): GetSubCategories(opt, ''); break;
                    case (3): Getproducts(opt, ''); break;
                    case (4): $('#categoryMessages').modal('hide'); break;
                }
            }
        });
}

function UpdateObject(dataURL, obj, objEntity, opt) {
    $.ajax(dataURL,
        {
            method: "PUT",
            data: obj,
            success: function () {
                switch (objEntity) {
                    case (1): GetCategories(''); break;
                    case (2): GetSubCategories(opt, ''); break;
                    case (3): Getproducts(opt, ''); break;
                    case (4): FillPersonCard(opt, function () {

                    }); break;
                }
            }
        });
}

function DeleteObject(dataURL, objEntity,opt) {
    $.ajax(dataURL,
        {
            method: "DELETE",
            success: function () {
                switch (objEntity) {
                    case (1): GetCategories(''); break;
                    case (2): GetSubCategories(opt, ''); break;
                    case (3): Getproducts(opt, ''); break;
                }
            }
        });
}