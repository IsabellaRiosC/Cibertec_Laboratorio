function upload(url) {
    var formData = new FormData();
    formData.append("file", $("#file")[0].files[0]);
    formData.append("picture", { 'businessEntityID': $('#BusinessEntityID').val() });
    $.ajax({
        async: true,
        type: 'POST',
        url: url,
        data: formData,
        success: closeModal,
        cache: false,
        contentType: false,
        processData: false
    });
}