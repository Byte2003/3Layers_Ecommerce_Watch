// Loading DataTable with Ajax
var dataTable;
$(document).ready(function () {
    loadDataTable();
})
function loadDataTable() {
    dataTable = $('#discountTable').DataTable({
        "ajax": {
            "url": "/Admin/ProductDiscount/GetAllDiscounts"
        },
        "columns": [
            { "data": "product.name", "width": "50%" },
            { "data": "percentage", "width": "25%" },
            {
                "data": "pDiscountID",
                "render": function (data) {
                    return `
                       
                            <a href="/Admin/ProductDiscount/Update?discount_id=${data}"
                            class="btn btn-primary mx-2"> <i class="bi bi-pencil-square"></i> Edit</a>
                            <a onClick=Delete('/Admin/ProductDiscount/Delete?discount_id=${data}')
                            class="btn btn-danger mx-2"> <i class="bi bi-trash-fill"></i> Delete</a>
					   
                        `
                },
                "width": "25%"
            }
        ]
    });
}

// SweetAlert library
function Delete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {           
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    if (data.success) {
                        dataTable.ajax.reload();
                        Swal.fire(
                            'Deleted!',
                            'This discount has been deleted.',
                            'success'
                        )
                        //toastr.success(data.message);
                    } else {
                        //toastr.error(data.message);
                    }
                }
            })
        }
    })
}