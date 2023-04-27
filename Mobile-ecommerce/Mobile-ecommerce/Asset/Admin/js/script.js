
setTimeout(function () {
    $("#msgAlert").fadeOut("slow");
}, 2000);


ShowImageMain = (imageUploader, ImageMain) => {
    if (imageUploader.files && imageUploader.files[0]) {
        var readerFile = new FileReader();
        readerFile.onload = function (e) {
            $(ImageMain).attr('src', e.target.result);
        }
        readerFile.readAsDataURL(imageUploader.files[0]);
    }
}