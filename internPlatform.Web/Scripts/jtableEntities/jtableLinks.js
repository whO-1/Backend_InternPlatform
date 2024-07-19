 console.log("Jtable Links");
$(document).ready(function () {
    initialize();
});


function initialize() {
    $('#LinksTableContainer').jtable({
        title: 'Table of Navbar links',
        actions: {
            listAction: '/Admin/Link/EntityList',
            createAction: '/Admin/Link/CreateEntity',
            updateAction: '/Admin/Link/UpdateEntity',
            deleteAction: '/Admin/Link/DeleteEntity',
        },
        fields: {
            Id: {
                key: true,
                list: false
            },
            LinkTitle: {
                title: 'Navbar Link Name',
                width: '30%',
                inputClass: 'validate[required,minSize[1],maxSize[20]]',
            },
            LinkUrl: {
                title: 'Url',
                width: '20%',
                inputClass: 'validate[required]',
            },
            DisplayOrder: {
                title: 'Display Order',
                width: '30%',
                inputClass: 'validate[required,custom[number]]',
            },
            HeadId: {
                title: 'Head Link',
                width: '20%',
                options: function (data) {
                    return '/Admin/Link/GetOptions';
                },
                type: 'dropdown',
                defaultValue: 'null'
            }
        },
        //recordAdded: async function (event, data) {
           
        //    await destroy();
        //    await initialize();

        //},

        //recordDeleted: async function (event, data) {
            
        //    await  destroy();
        //    await  initialize();
        //},

    });
    $('#LinksTableContainer').jtable('load');
}

function destroy() {
    $('#LinksTableContainer').jtable({});
    $('#LinksTableContainer').jtable('destroy');
}

