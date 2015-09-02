var __catId = '';
var __subCatId = '';
var __catName = '';
var __subCatName = '';
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