var load = function (keyWord, pageIndex, pageSize) {
    $.ajax({
        url: "/Admin/OrderAd/GetPaging",
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
                str += "<td>" + value.OrderID + "</td>";
                str += "<td>" + value.Ngay + "</td>";
                str += "<td>" + value.TongTien + "</td>";
                str += "<td>" + value.Status + "</td>";          
                str += '<td class="d-flex justify-content-around"><a class="btn btn-info text-white" href="/Admin/OrderAd/Details/' + value.OrderID + '">Xem chi tiết</a>';
                str += '<a class="btn btn-warning" href="/Admin/OrderAd/Edit/' + value.OrderID + '">Cập nhật</a></td>';           
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
