console.log("Jtable EntryType");
$(document).ready(function () {
    $('#FaqTableContainer').jtable({
        title: 'Table of FAQs',
        actions: {
            listAction: '/Admin/Faq/EntityList',
            createAction: '/Admin/Faq/CreateEntity',
            updateAction: '/Admin/Faq/UpdateEntity',
            deleteAction: '/Admin/Faq/DeleteEntity'
        },
        fields: {
            FaqId: {
                key: true,
                list: false
            },
            Title: {
                title: 'Question',
                width: '30%'
            },
            Description: {
                title: 'Answer',
                width: '70%'
            },
        }
    });


    $('#FaqTableContainer').jtable('load');
});
