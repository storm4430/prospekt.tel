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
    $.get('/subcategory/index/?categoryid=' + objId, function (data) {
        $('#ajaxpage').hide(200);
        $('#subCategory').empty().html(data).show(200);
    })

}


//-----------------------------Подкатегории-------------------
function GetSubCategories(catid, substr) {
    GetDataTable(
        {
            fields: [{
                "id": "Код",
                "subcategory_desc": "Наименование",
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
        }, '/api/subcategories/?catid=' + catid + '&substr=' + substr, 15, 'subtree')
}