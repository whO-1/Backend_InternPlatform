$(document).ready(function () {
    console.log("dataTable users")
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
        "ajax": { url: '/Admin/Dashboard/GetUsers' },
        "columns": [
            
            { data: 'User.UserName', "width": "20%" },
            { data: 'Role', "width": "25%" },
            {
                data: 'User.Id',
                "render": function (data) {
                    return (`
                        <div class="btn-group " role="group">
                            <a href = "/Admin/Dashboard/UpdateUser/${data}" class="btn text-success" >
                                Edit
                            </a>
                            <a onclick = "Delete('/Admin/Dashboard/DeleteUser/${data}')" class="btn text-danger" >
                                Delete
                            </a>
                            <a href = "/Admin/Dashboard/LockUser/${data}" class="btn text-warning" >
                                Lock
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

