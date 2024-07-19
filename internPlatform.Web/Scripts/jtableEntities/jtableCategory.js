 console.log("Jtable Category");
$(document).ready(function () {
    $('#CategoryTableContainer').jtable({
        title: 'Table of Event Categories',
        actions: {
            listAction: '/Admin/Category/EntityList',
            createAction: '/Admin/Category/CreateEntity',
            updateAction: '/Admin/Category/UpdateEntity',
            deleteAction: '/Admin/Category/DeleteEntity'
        },
        fields: {
            Id: {
                key: true,
                list: false
            },
            Name: {
                title: 'Category Name',
                width: '30%'
            },
            DisplayOrder: {
                title: 'Display Order',
                width: '20%'
            },
        }
    });


    $('#CategoryTableContainer').jtable('load');
});
