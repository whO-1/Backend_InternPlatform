$(document).ready(function () {
    console.log("loaded")
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
        "ajax": { url: '/admin/dashboard/eventsgetall' },
        "columns": [
            {
                data: 'TimeStamp.UpdateDate',
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
            { data: 'Description', "width": "25%" },
            { data: 'AuthorId', "width": "15%" },
            { data: 'Entry.Name', "width": "5%" },
            { data: 'Age.Name', "width": "5%" },
            { data: 'Categories', "width": "5%" },
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

function parseDotNetDate(dotNetDate) {
    // Extract the milliseconds from the /Date(xxxxx)/ format
    const timestamp = parseInt(dotNetDate.replace(/\/Date\((-?\d+)\)\//, '$1'));

    // Create a new Date object using the timestamp
    const date = new Date(timestamp);

    // Format the date as needed (e.g., yyyy-MM-dd)
    const formattedDate = date.toISOString().split('T')[0]; // Example: 1919-12-31

    return formattedDate;
}