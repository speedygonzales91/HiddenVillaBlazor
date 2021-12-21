window.ShowToastr = (type, message) => {
    if (type === "succes") {
        toastr.success(message, 'Operation successful')
    }
    if (type === "error") {
        toastr.error(message, 'Operation failed')
    }
}


window.ShowSwal = (type, message, submessage) => {
    if (type === "succes") {
        Swal.fire(
            message,
            submessage,
            'success'
        )
    }
    if (type === "error") {
        Swal.fire(
            message,
            submessage,
            'error'
        )
    }
}

function ShowDeleteConfirmationModal() {
    $('#deleteConfirmationModal').modal('show');
}

function HideDeleteConfirmationModal() {
    $('#deleteConfirmationModal').modal('hide');
}