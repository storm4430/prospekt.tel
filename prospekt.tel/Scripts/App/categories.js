// #region Global Variables

var __catId = '';
var __subCatId = '';
var __catName = '';
var __subCatName = '';

// #endregion

// #region Категории

//-----------------------------Категории-------------------
function GetCategories(substr) {
    GetDataTable(
        {
            fields: [{
                "id": "Код",
                "category_desc": "Наименование",
                "created": "Создана",
                "updated": "Обновлена",
                "createdBy": "Автор"
            }],
            actions: [{
                "Details": "'CategoryDetails'",
                "Delete": "'Category'",
                "Update": "'Category'"
            }

            ]
        }, '/api/categories/?substr=' + substr, 15, 'tree')
}

function CategoryEdit(objId) {
    $.get('/category/edit/' + objId, function (data) {
        $('#mt').text('РЕДАКТИРОВАНИЕ ЗАПИСИ');
        $('#mb').empty().html(data);
        $('#categoryMessages').modal({
            keyboard: false,
            backdrop: false
        }, 'show');
    })
}

function CategoryDelete(objId) {
    $.get('/category/delete/' + objId, function (data) {
        $('#mt').text('УДАЛЕНИЕ ЗАПИСИ');
        $('#mb').empty().html(data);
        $('#categoryMessages').modal({
            keyboard: false,
            backdrop: false
        }, 'show');
    })
}

function CategoryToSubdirectory(objId) {
    __catId = objId;
    console.log(__catId)
    $.get('/subcategory/index/?categoryid=' + objId, function (data) {
        $('#ajaxpage').hide();
        $('#subCategory').empty().html(data).show();
    })
}

//============================================================

// #endregion

// #region Подкатегории

//-----------------------------Подкатегории-------------------
function GetSubCategories(catid, substr) {
    GetDataTable(
        {
            fields: [{
                "id": "Код",
                "subcategory_desc": "Наименование",
                "category_desc": "Категория",
                "created": "Создана",
                "updated": "Обновлена",
                "createdBy": "Автор"
            }],
            actions: [{
                "Details": "'subCategoryDetails'",
                "Delete": "'subCategory'",
                "Update": "'subCategory'"
            }

            ]
        }, '/api/subcategories/?catid=' + catid + '&substr=' + substr, 15, 'subtree');
}
function SubCategoryEdit(objId) {
    $.get('/subcategory/edit/' + objId, function (data) {
        $('#mt').text('РЕДАКТИРОВАНИЕ ЗАПИСИ');
        $('#mb').empty().html(data);
        $('#categoryMessages').modal({
            keyboard: false,
            backdrop: false
        }, 'show');
    })
}

function SubCategoryDelete(objId) {
    $.get('/subcategory/delete/' + objId, function (data) {
        $('#mt').text('УДАЛЕНИЕ ЗАПИСИ');
        $('#mb').empty().html(data);
        $('#categoryMessages').modal({
            keyboard: false,
            backdrop: false
        }, 'show');
    })
}

function SubdirectoryToCategory() {
    $('#subCategory').hide();
    $('#ajaxpage').show();
}


// #endregion

// #region Номенклатура

//-----------------------------Номенклатура-------------------
function Getproducts(scatId, substr) {
    GetDataTable(
        {
            fields: [{
                "id": "Код",
                "product_name": "Наименование",
                "category_desc": "Категория",
                "subcategory_desc": "Подкатегория",
                "created": "Создана",
                "updated": "Обновлена",
                "createdBy": "Автор"
            }],
            actions: [{
                "Details": "'Product'",
                "Delete": "'Product'",
                "Update": "'Product'"
            }

            ]
        }, '/api/products/?id=' + scatId + '&substr=' + substr, 15, 'producttree')
}

function ProductEdit(objId) {
    $.get('/Product/edit/' + objId, function (data) {
        $('#mt').text('РЕДАКТИРОВАНИЕ ЗАПИСИ');
        $('#mb').empty().html(data);
        $('#categoryMessages').modal({
            keyboard: false,
            backdrop: false
        }, 'show');
    })
}

function ProductDelete(objId) {
    $.get('/Product/delete/' + objId, function (data) {
        $('#mt').text('УДАЛЕНИЕ ЗАПИСИ');
        $('#mb').empty().html(data);
        $('#categoryMessages').modal({
            keyboard: false,
            backdrop: false
        }, 'show');
    })
}

function SubdirectoryToProduct(objId) {
    __subCatId = objId;
    $.get('/Product/index/' + objId, function (data) {
        $('#subCategory').hide();
        $('#product').empty().html(data).show();
    })
};

function ProductToSubCategory() {
    $('#product').hide();
    $('#subCategory').show();
}
//============================================================

// #endregion

// #region Контрагент

//-----------------------------Контрагент-------------------
function GetPersons(scatId, substr) {
    GetDataTable(
        {
            fields: [{
                "id": "Код",
                "product_name": "Наименование",
                "category_desc": "Категория",
                "subcategory_desc": "Подкатегория",
                "created": "Создана",
                "updated": "Обновлена",
                "createdBy": "Автор"
            }],
            actions: [{
                "Details": "'Product'",
                "Delete": "'Product'",
                "Update": "'Product'"
            }

            ]
        }, '/api/products/?id=' + scatId + '&substr=' + substr, 15, 'producttree')
}

function PersonEdit(objId) {
    $.get('/Product/edit/' + objId, function (data) {
        $('#mt').text('РЕДАКТИРОВАНИЕ ЗАПИСИ');
        $('#mb').empty().html(data);
        $('#categoryMessages').modal({
            keyboard: false,
            backdrop: false
        }, 'show');
    })
}

function PersonCreate() {
    $.get('/person/create/', function (data) {
        $('#mt').text('СОЗДАНИЕ НОВОГО КОНТРАГЕНТА');
        $('#mb').empty().html(data);
        $('#categoryMessages').modal({
            keyboard: false,
            backdrop: false
        }, 'show');
    })
}

function PersonDelete(objId) {
    $.get('/Product/delete/' + objId, function (data) {
        $('#mt').text('УДАЛЕНИЕ ЗАПИСИ');
        $('#mb').empty().html(data);
        $('#categoryMessages').modal({
            keyboard: false,
            backdrop: false
        }, 'show');
    })
}

function GetSex(dest, callback) {
    $.get('/api/sex/', function (data) {
        $(dest).empty().append('<option></option>')
        $.each(data, function (key, item) {
            $(dest).append('<option value="' + item.id + '">' + item.description + '</option>');
        });
        callback();
    });
};

function FillPersonCard(id, callback) {
    $.get('/api/persons/' + id, function (data) {
        $('#persCard #id').val(data.id);
        $('#persCard #fam').val(data.fam).attr('disabled', 'disabled');
        $('#persCard #im').val(data.im).attr('disabled', 'disabled');
        $('#persCard #ot').val(data.ot).attr('disabled', 'disabled');
        $('#persCard #sexed').val(data.sex).attr('disabled', 'disabled');
        $('#persCard #dr').val(data.dr.substring(0, 10)).attr('disabled', 'disabled');
        $('#persCard #doc_ser').val(data.passport_serie).attr('disabled', 'disabled');
        $('#persCard #doc_num').val(data.passport_num).attr('disabled', 'disabled');
        $('#persCard #cellPhone').val(data.cellPhone).attr('disabled', 'disabled');
        $('#persCard #comment').val(data.person_comment).attr('disabled', 'disabled');
        $('#persCard #cont_info').empty().append('<strong>Уникальный код контрагента:</strong> ' + id)
                                 .append('.    <strong>Запись создана:</strong> ' + data.created)
                                 .append('.    <strong>Последнее редактирование:</strong> ' + data.updated);
        $('#butForPhotoUpdate').hide();
        $('#butForScanUpdate').hide();
        GetPersonPhoto(data.id);
        GetPersonPassScan(data.id);
        $('#editCard').click(function (e) {
            $('#persCard #fam').removeAttr('disabled').focus();
            $('#persCard #im').removeAttr('disabled');
            $('#persCard #ot').removeAttr('disabled');
            $('#persCard #sexed').removeAttr('disabled');
            $('#persCard #dr').removeAttr('disabled');
            $('#persCard #doc_ser').removeAttr('disabled');
            $('#persCard #doc_num').removeAttr('disabled');
            $('#persCard #cellPhone').removeAttr('disabled');
            $('#persCard #comment').removeAttr('disabled');
            $('#actionsButton').hide();
            $('#butForPhotoUpdate').show();
            $('#butForScanUpdate').show();
            $('#_saveCardButton').show();
            e.preventDefault();
        })
        callback();
    });
};

function GetPersonPhoto(id) {
    $.get('/api/personphotos/' + id, function (data) {
        $("#srcfped").fadeIn("fast").attr('src', data);
    });
};

function GetPersonPassScan(id) {
    $.get('/api/personscans/' + id, function (data) {
        $("#pass_scan").fadeIn("fast").text(data).attr('data-id', id).attr('onclick', 'GetGullScan(this)');
    });
};

function GetGullScan(obj) {
    var sId = $(obj).data('id');
    window.open('/api/PersonFullScans/' + sId, 'blank')
}
//============================================================

// #endregion

// #region Users

//-----------------------------Users-------------------
function GetUsers(substr) {
    GetDataTable(
        {
            fields: [{
                "id": "Код",
                "FIO": "ФИО сотрудника",
                "Name": "Роль",
                "orgName": "Отделение",
            }],
            actions: [{
                "Details": "'User'",
                "Delete": "'User'",
                "Update": "'User'"
            }

            ]
        }, '/api/user/?substr=' + substr, 15, 'usertree')
}
//=======================================================================

// #endregion

// #region Orgs

//-----------------------------Orgs-------------------
function GetOrgs(substr) {
    GetDataTable(
        {
            fields: [{
                //"id": "Код",
                "orgName": "Наименование",
                "orgAdress": "Адрес",
                "orgINN": "ИНН",
                "orgOGRN": "ОГРН",
                "orgRuk": "Руководитель",
            }],
            actions: [{
                "Details": "'Org'",
                "Delete": "'Org'",
                "Update": "'Org'"
            }

            ]
        }, '/api/orgs/?substr=' + substr, 15, 'orgtree')
};

function GetOrgsForRegister(dest) {
    $.get('/api/orgs/?substr=', function (data) {
        $(dest).empty().append('<option></option>')
        $.each(data, function (key, item) {
            $(dest).append('<option value="' + item.id + '">' + item.orgName + '</option>');
        });
    });
};
//=======================================================================

// #endregion

// #region Contracts

//-----------------------------Contracts-------------------
function GetContracts(substr) {
    GetDataTable(
        {
            fields: [{
                "id": "Код",
                "orgName": "Отделение",
                "FIO": "Контрагент",
                "order_num": "Номер",
                "order_date": "Дата",
                "order_summ": "Сумма",
                "estimated_close": "Срок",
            }],
            actions: [{
                "Details": "'Org'",
                "Delete": "'Org'",
                "Update": "'Org'"
            }

            ]
        }, '/api/contracts/?substr=' + substr, 15, 'contracttree')
};

function GetOrgsForRegister(dest) {
    $.get('/api/orgs/?substr=', function (data) {
        $(dest).empty().append('<option></option>')
        $.each(data, function (key, item) {
            $(dest).append('<option value="' + item.id + '">' + item.orgName + '</option>');
        });
    });
};

function GetUserRolesForRegister(dest) {
    $.get('/api/userRoles/', function (data) {
        $(dest).empty().append('<option></option>')
        $.each(data, function (key, item) {
            $(dest).append('<option value="' + item.Name + '">' + item.Name + '</option>');
        });
    });
};

//=======================================================================

// #endregion