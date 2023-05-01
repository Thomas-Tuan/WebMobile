var load = function (keyWord, pageIndex, pageSize) {
    $.ajax({
        url: "/Admin/Products/GetPaging",
        data: {
            keyWord: keyWord,
            pageIndex: pageIndex,
            pageSize: pageSize
        },
        type: "GET",
        success: function (response) {
            var pageCurrent = response.pageCurrent;
            var totalPage = response.totalPage;

            var str = "";
            var info = `Trang ${pageCurrent} / ${totalPage}`;
            $("#selection-datatable_info").text(info);
            $.each(response.data, function (index, value) {              
                str += "<tr>";
                str += "<td>" + value.ProductID + "</td>";
                str += "<td>" + value.ProductName + "</td>";
                str += "<td>" + value.Category + "</td>";
                str += "<td>" + value.Price + "</td>";          
                str += "<td class='banner'><img class='img-banner' src=" + value.Image + " /</td>";              
                str += "<td>" + value.Quantity + "</td>";
                if (value.Quantity >= 1) {
                    str += "<td>" + "Còn hàng" + "</td>";
                }
                else {
                    str += "<td>" + "Hết hàng" + "</td>";
                }
                str += '<td class="d-flex justify-content-around"><a class="btn btn-warning" href="/Admin/Products/Edit/' + value.ProductID + '">Cập nhật</a>';
                str += '<a class="btn btn-danger" href="#" data-user=' + value.ProductID + '>Xóa</a></td>';
                str += "</tr>";

                //create pagination
                var pagination_string = "";

                if (pageCurrent > 1) {
                    var pagePrevious = pageCurrent - 1;
                    pagination_string += '<li class="paginate_button page-item previous"><a href="#" class="page-link" data-page="' + pagePrevious + '">‹</a></li>';
                }
                for (var i = 1; i <= totalPage; i++) {
                    if (i == pageCurrent) {
                        pagination_string += '<li class="paginate_button page-item active"><a class="page-link" href="#" data-page=' + i + '>' + i + '</a></li>';
                    } else {
                        pagination_string += '<li class="paginate_button page-item"><a href="#" class="page-link" data-page=' + i + '>' + i + '</a></li>';
                    }
                }
                //create button next
                if (pageCurrent > 0 && pageCurrent < totalPage) {
                    var pageNext = pageCurrent + 1;
                    pagination_string += '<li class="paginate_button page-item next"><a href="#" class="page-link" data-page=' + pageNext + '>›</a></li>';
                }
                $("#load-pagination").html(pagination_string);
            });
            //load str to class="load-list"
            $("#datatablesSimple > tbody").html(str);
        }
    });
}

//click delete button
$("body").on("click", "#datatablesSimple a.btn.btn-danger", function (event) {
    event.preventDefault();
    var pro_delete = $(this).attr('data-user');
    if (confirm("Bạn có muốn xóa sản phẩm có id = " + pro_delete + " này không?")) {
        $.ajax({
            url: "/Admin/Products/Delete",
            type: "POST",
            data: { id: pro_delete },
            dataType: "json",
            success: (result) => {
                location.reload();
            }
        });
    }
});