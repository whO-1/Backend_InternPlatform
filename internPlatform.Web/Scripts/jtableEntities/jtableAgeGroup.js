 console.log("Jtable AgeGroup ");
$(document).ready(function () {
    $('#AgeGroupTableContainer').jtable({
        title: 'Table of Age Groups',
        actions: {
            listAction: '/Admin/AgeGroup/EntityList',
            createAction: '/Admin/AgeGroup/CreateEntity',
            updateAction: '/Admin/AgeGroup/UpdateEntity',
            deleteAction: '/Admin/AgeGroup/DeleteEntity'
        },
        fields: {
            Id: {
                key: true,
                list: false
            },
            Name: {
                title: 'Age Group Name',
                width: '30%'
            },
            DisplayOrder: {
                title: 'Display Order',
                width: '20%'
            },
        }
    });


    $('#AgeGroupTableContainer').jtable('load');
});
