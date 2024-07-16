 console.log("Jtable EntryType");
$(document).ready(function () {
    $('#EntryTypeTableContainer').jtable({
        title: 'Table of Entry Types',
        actions: {
            listAction: '/Admin/EntryType/EntityList',
            createAction: '/Admin/EntryType/CreateEntity',
            updateAction: '/Admin/EntryType/UpdateEntity',
            deleteAction: '/Admin/EntryType/DeleteEntity'
        },
        fields: {
            Id: {
                key: true,
                list: false
            },
            Name: {
                title: 'Entry Type Name',
                width: '30%'
            },
            DisplayOrder: {
                title: 'Display Order',
                width: '20%'
            },
        }
    });


    $('#EntryTypeTableContainer').jtable('load');
});
