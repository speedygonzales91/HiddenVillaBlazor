window.ShowToastr = (type, message) => {
    if (type === "succes") {
        toastr.success(message, 'Operation successful')
    }
    if (type === "error") {
        toastr.error(message, 'Operation failed')
    }
}
