function GetDataTable(items, apiPath, pageSize, container) {
    $('#preLoader').show(50);
    if (pageSize === null) {
        pageSize = 15;
    }
    $('#' + container).empty().fadeOut(200).append('<div id="subContainer"><table class="table table-responsive table-condensed table-striped"  style="margin-bottom:1px;" id="mTable"><thead><tr id="tHeaders"></tr></thead><div></div><tbody data-bind="foreach: Rows" style="cursor:pointer" id="tMainBody"></tbody></table></div>').fadeIn(200);

    ///Инициализация заголовков таблицы 
    var t = '';
    for (var i in items) {
        t += '<th>' + items[i] + '</th>';
    }
    $('#tHeaders').append(t);

    ///Инициализация тела таблицы
    var tb = '<tr data-bind="click: $parent.AssignDetails">';
    for (var i in items) {
        tb += '<td data-bind="text: ' + i + '"></td>'
    }
    $('#tMainBody').append(tb);

    ///Инициализация футера таблицы
    //var tf = '';
    //for (var i in items) {
    //    tf += '<th>' + i + '</th>'
    //}
    //$('#mtFooter').append(tf);
    var rep = new GetAssignments(apiPath, 'mTable');
    ko.applyBindings(rep, document.getElementById('subContainer'));
    GetAssignments();

   // $('#' + container).append('<div id="testkoappid"><div data-bind="text: name"></div><div data-bind="text:sum"></div></div>')

 //   var script = document.createElement('script');
 //   script.type = 'text/javascript';
 //   script.id = 'scrId';
 //   $("#" + container).append(script);
 //   $('#scrId').append('function koapp() { var self=this; self.name=ko.observable("abc"); self.sum=ko.observable(123.2);} \
 //function start() { \
 //var testKoApp = new koapp();\
 //ko.applyBindings(testKoApp, document.getElementById("testkoappid"));\
 //}\
 //');
    //start();
    
}


function GetAssignments(dataPath, dataDest) { //'/api/assignments/2036531'
    var self = this;
    self.AssignDetails = function (assign) {
        console.log(assign.id)
    }
    self.Rows = ko.observableArray();

    // getData for report
    // —---
    self.getData = function () {
        $.get(dataPath, function (ajaxData) {
            self.Rows.splice(0);
            self.Rows(ajaxData);
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
                    case (1): GetCategories();
                }
                
            }
        }
        );

}