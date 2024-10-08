$(document).ready(function () {
    console.log("dataTable events")
    try {
        loadDataTable();

    }
    catch (err) {
        console.error(err);
    }
});

let dataTable;

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "serverSide": true,
        "processing": true,
        "ajax": {
            url: '/admin/dashboard/eventsgetall',
            type: 'GET'
        },
        "columns": [
            {
                data: 'LastUpdate',
                "width": "5%",
                "render": function (data) {
                            // Parse the /Date(xxxxx)/ format and convert to Date object
                            const timestamp = parseInt(data.replace(/\/Date\((-?\d+)\)\//, '$1'));
                            const date = new Date(timestamp);
                            // Format the date as needed (e.g., yyyy-MM-dd)
                            const day = date.getDate().toString().padStart(2, '0');
                            const month = (date.getMonth() + 1).toString().padStart(2, '0');
                            const year = date.getFullYear();
                            return `${day}.${month}.${year}`;
                }
            },
            { data: 'Title', "width": "20%" },
            {
                data: 'Description',
                "width": "20%",
                "render": function (data, type, row) {
                    
                    return (data !== null)? data.length > 30 ? data.substring(0, 30) + '...' : data: data;
                }
            },
            { data: 'Author', "width": "15%" },
            {
                data: 'StartDate',
                "width": "5%",
                "render": function (data) {
                    // Parse the /Date(xxxxx)/ format and convert to Date object
                    const timestamp = parseInt(data.replace(/\/Date\((-?\d+)\)\//, '$1'));
                    const date = new Date(timestamp);
                    // Format the date as needed (e.g., yyyy-MM-dd)
                    const day = date.getDate().toString().padStart(2, '0');
                    const month = (date.getMonth() + 1).toString().padStart(2, '0');
                    const year = date.getFullYear();
                    return `${day}.${month}.${year}`;
                }
            },
            { data: 'EntryType', "width": "5%" },
            { data: 'AgeGroup', "width": "5%" },
            { data: 'SelectedCategories', "width": "5%" },
            { data: 'SpecialGuests', "width": "10%" },
            {
                data: 'Id',
                "render": function (data) {
                    return (`
                        <div class="btn-group " role="group">
                            <a href = "/admin/dashboard/eventupsert/${data}" class="btn btn-primary" >
                                <i class="bi bi-pencil"></i>Edit
                            </a>
                            <a onclick = "Delete('/admin/dashboard/eventdelete/${data}')" class="btn btn-danger" >
                                <i class="bi bi-trash"></i>Delete
                            </a>
                        </div>
                        `);
                },
                "width": "10%"
            }
        ]
    });
}


function Delete(url) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: "POST",
                success: function (data) {
                    dataTable.ajax.reload();
                    toastr.success(data.message);
                },
                error: function (error) {
                    alert(url)
                }

            })
        }
    });
}                 

