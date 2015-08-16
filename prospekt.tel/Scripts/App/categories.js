function GetCategories(substr) {
    GetDataTable(
        {
            "id": "Код",
            "category_desc": "Наименование",
            "created": "Создана",
            "updated": "Обновлена",
            "createdBy": "Автор",
        }, '/api/categories/?substr=' + substr, 15, 'tree')
}

