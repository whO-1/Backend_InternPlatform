$(document).ready(function () {
    console.log("dataTable errors")
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
            url: '/admin/dashboard/GetErrors',
            type: 'GET'
        },
        "columns": [
            {
                data: 'Timestamp',
                "width": "25%",
                "render": function (input) {
                    let [datePart, timePart] = input.split(' ');
                    let [hours, minutes] = timePart.split(':');
                    let date = new Date(`${datePart}T${hours}:${minutes}`);
                    let options = {
                        year: 'numeric',
                        month: 'long',
                        day: 'numeric',
                        hour: '2-digit',
                        minute: '2-digit',
                        hour12: false // 24-hour format
                    };
                    return date.toLocaleDateString('en-US', options);
                }
            },
            
            {
                data: 'CallSite',
                "width": "30%",
                "render": function (data, type, row) {

                    return (data !== null) ? data.length > 30 ? data.substring(0, 30) + '...' : data : data;
                }
            },
            {
                data: 'Message',
                "width": "30%",
                "render": function (data, type, row) {

                    return (data !== null) ? data.length > 30 ? data.substring(0, 30) + '...' : data : data;
                }
            },
            {
                data: 'LineNumber',
                "width": "5%",
            },
            {
                data: 'ErrorLogId',
                "render": function (data) {
                    return (`
                        <div class="btn-group " role="group">
                            <a href = "/admin/dashboard/errorview/${data}" class="btn btn-primary" >
                                <i class="bi bi-binoculars-fill"></i>
                            </a>
                            <a onclick = "Delete('/admin/dashboard/errordelete/${data}')" class="btn btn-danger" >
                                <i class="bi bi-trash"></i>
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

