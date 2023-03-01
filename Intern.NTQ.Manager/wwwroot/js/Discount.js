class Discount {
    constructor(gIndex, gNameSearch, gStatus, gGetUrlDiscounts) {
        this.GetDiscount(gIndex, gNameSearch, gStatus, gGetUrlDiscounts)
        this.Create();
        this.Edit();
    }
    // Tạo
    Create() {
        let _this = this

        $('#create').on('click', function () {
            var name = $('#name').val();
            var percent = $('#percent').val();
            var fromDate = $('#fromDate').val();
            var toDate = $('#toDate').val();
            var percent_regex = /^\d*$/;
            var check = true;
            if (name == "") {
                $('#textName').html("Nhập lại tên ");
                check = false
            }
            if (fromDate == "") {
                $('#textFromDate').html("Nhập lại Ngày");
                check = false
            }
            if (toDate == "") {
                $('#textToDate').html("Nhập lại Ngày");
                check = false
            }
            else if (percent_regex.test(percent) == false) {
                $('#textPercent').html('Số phần trăm của bạn không đúng định dạng!');
                check = false
            }
            if (check == true) {
                $.ajax({
                    type: "POST",
                    url: "https://localhost:7139/api/Discount/create-discount",
                    data: JSON.stringify({
                        Name: name,
                        Percent: percent,
                        FromDate: fromDate,
                        ToDate: toDate,
                    })
                    ,
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    success: function (response) {
                        localStorage.setItem('mess', "Thêm thành công");

                        window.location.href = '/Admin/Discount';
                    }
                }).fail(function (jqXHR, textStatus, errorThrown) {
                    $('#warning').html("")
                });
            }
        });
    }
    //Sửa GET và PUT
    EditGet() {
        $('.edit').off('click').on('click', function () {
            console.log("okokokok")
            var Id = $(this).data("id");
            $.ajax({
                type: "GET",
                url: "https://localhost:7139/api/Discount/get-by-id?id=" + Id,
                data: {}
                ,
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (response) {
                    window.location.href = "Discount/Edit?idPromotion=" + response.idPromotion + "&name=" + response.name + "&percent=" + response.percent + "&fromDate=" + response.fromDate + "&toDate=" + response.toDate + "";
                }
            }).fail(function (jqXHR, textStatus, errorThrown) {
                $('#warning').html("")
            });
        });
    }
    Edit() {
        let _this = this
        $('#edit').on('click', function () {
            var id = $('#prime').data("id");
            var name = $('#name').val();
            var percent = $('#percent').val();
            var fromDate = $('#fromDate').val();
            var toDate = $('#toDate').val();
            var percent_regex = /^\d*$/;
            var check = true;
            if (name == "") {
                $('#textName').html("Nhập lại tên ");
                check = false
            }
            if (fromDate == "") {
                $('#textFromDate').html("Nhập lại Ngày");
                check = false
            }
            if (toDate == "") {
                $('#textToDate').html("Nhập lại Ngày");
                check = false
            }
            else if (percent_regex.test(percent) == false) {
                $('#textPercent').html('Số phần trăm của bạn không đúng định dạng!');
                check = false
            }
            if (check == true) {
                $.ajax({
                    type: "PUT",
                    url: "https://localhost:7139/api/Discount/update-discount?id=" + id,
                    data: JSON.stringify({
                        Name: name,
                        Percent: percent,
                        FromDate: fromDate,
                        ToDate: toDate,
                    })
                    ,
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    success: function (response) {
                        localStorage.setItem('mess', "Sửa thành công");

                        window.location.href = '/Admin/Discount';
                    }
                }).fail(function (jqXHR, textStatus, errorThrown) {
                    $('#warning').html("")
                });
            }
        });
    }
    //Tìm
    Search(gIndex, gNameSearch, gStatus, gGetUrlDiscounts) {
        let _this = this
        $('#search-press').off('click').on('click', function () {
            var click = $(this);
            var P_Index = click.data('id');
            console.log(P_Index);
            gNameSearch = $('#search').val();
            if (gStatus == true) {
                _this.GetDiscount(1, gNameSearch, true);
            } else {
                _this.GetDiscount(1, gNameSearch, false);
            }

        })
    }
    //Xóa
    Delete() {
        let _this = this;
        $('.btn-delete').off('click').on('click', function () {
            console.log("hehehe")
            var Id = $(this).data("id");
            $.ajax({
                type: "Delete",
                url: "https://localhost:7139/api/Discount/delete-discount?id=" + Id,
                data: {}
                ,
                dataType: "json",
                contentType: "application/json; charset=utf-8",

                success: function (response) {
                    localStorage.setItem('mess', "Xóa thành công");
                    let mess = localStorage.getItem("mess");
                    var row =
                        `<div id="msgAlert" class="alert alert-success" role="alert">
                                    ${mess}
                                 </div>`
                    $('#msgAlert').html(row)
                    _this.GetDiscount()
                }
            }).fail(function (jqXHR, textStatus, errorThrown) {
                $('#canhbao').html("Hong biet nua")
            });
        });
    }
    //Khôi Phục
    Restore() {
        let _this = this
        $('.restore').off('click').on('click', function () {
            var Id = $(this).data("id");
            $.ajax({
                type: "PUT",
                url: "https://localhost:7139/api/Discount/restore?id=" + Id,
                data: {}
                ,
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (response) {
                    localStorage.setItem('mess', "Khôi phục thành công");
                    let mess = localStorage.getItem("mess");
                    $("#get-discount").addClass('btn-dark');
                    $("#get-deleted-discount").removeClass('btn-dark');
                    var row =
                        `   <div id="msgAlert" class="alert alert-success" role="alert">
                                    ${mess}
                                 </div>
                            `
                    $('#msgAlert').html(row)
                    _this.GetDiscount();

                }
            }).fail(function (jqXHR, textStatus, errorThrown) {
                $('#warning').html("")
            });
        });
    }
    //Phân trang
    PageLink(gIndex, gNameSearch, gStatus, gGetUrlDiscounts) {
        let _this = this
        $('.page-link').off('click').on('click', function () {
            var click = $(this);
            var P_Index = click.data('id');
            if (gStatus == true) {
                $.ajax({
                    type: "GET",
                    url: gGetUrlDiscounts,
                    data: ({
                        PageSize: 5,
                        PageIndex: P_Index,
                        Name: gNameSearch
                    }),
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    success: function (response) {
                        if (response.items.length > 0) {
                            var rows = ""
                            rows = response.items.map((item) =>
                                `
                                <tr>
                                        <td>
                                    <p>${item.idPromotion}</p>
                                        </td>
                                        <td>
                                            <p>${item.name}</p>
                                        </td>
                                        <td>
                                            <p>${item.percent}</p>
                                        </td>
                                        <td>
                                            <p>${item.fromDate}</p>
                                        </td>
                                        <td>
                                            <p>${item.toDate}</p>
                                        </td>
                                        <td>
                                            <p>${item.createdAt}</p>
                                        </td>
                                        <td>
                                            <p>${item.updatedAt}</p>
                                        </td>
                                        <td>
                                            <button class="btn btn-success edit" data-id="${item.idPromotion}" >Sửa</button> |
                                            <button class="btn btn-danger btndelete" data-id="${item.idPromotion}" id = "delete" >Xóa</button> |
                                        </td>
                                    </tr>
                            `
                            )
                        }
                        $('.render-discount').html(rows);
                        $('.render-deleted-discount').html("");
                        //paging

                        gIndex = P_Index;
                        _this.GetDiscount(gIndex, gNameSearch, true, gGetUrlDiscounts)
                    }
                }).fail(function (jqXHR, textStatus, errorThrown) {
                    $('#canhbao').html("Hong biet nua")
                });
            } else {
                $.ajax({
                    type: "GET",
                    url: "https://localhost:7139/api/Discount/get-deleted-discounts",
                    data: ({
                        PageSize: 5,
                        PageIndex: P_Index,
                        Name: gNameSearch
                    }),
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    success: function (response) {
                        if (response.items.length > 0) {
                            var rows = ""
                            rows = response.items.map((item) =>
                                `
                                <tr>
                                         <td>
                                    <p>${item.idPromotion}</p>
                                        </td>
                                        <td>
                                            <p>${item.name}</p>
                                        </td>
                                        <td>
                                            <p>${item.percent}</p>
                                        </td>
                                        <td>
                                            <p>${item.fromDate}</p>
                                        </td>
                                        <td>
                                            <p>${item.toDate}</p>
                                        </td>
                                        <td>
                                            <p>${item.createdAt}</p>
                                        </td>
                                        <td>
                                            <p>${item.updatedAt}</p>
                                        </td>
                                        <td>
                                           <button class="btn btn-success restore" data-id="${item.idPromotion}" >Khôi phục</button> |
                                        </td>
                                    </tr>
                            `
                            )
                        }
                        $('.render-discount').html("");
                        $('.render-deleted-discount').html(rows);
                        gIndex = P_Index;
                        //paging
                        /*new Supplier(gIndex, gNameSearch, false, gGetUrlDiscounts);*/
                        _this.GetDiscount(gIndex, gNameSearch, false, gGetUrlDiscounts)
                    }
                }).fail(function (jqXHR, textStatus, errorThrown) {
                    $('#canhbao').html("Hong biet nua")
                });
            }
        })
    }
    //Phân trang
    GetDiscount(gIndex, gNameSearch, gStatus, gGetUrlDiscounts) {
        if (localStorage.getItem("mess") != "" && localStorage.getItem("mess") != null) {
            console.log("v")
            var row =
                `   <div id="msgAlert" class="alert alert-success" role="alert">
                                    ${localStorage.getItem("mess")}
                                 </div>
                            `
            $('#msgAlert').html(row)
            $('#msgAlert').css("display", "block");;
            setTimeout(function () {
                $('#msgAlert').fadeOut('slow');
            }, 2000);

        }
        localStorage.setItem("mess", "")
        let _this = this
        if (gIndex == null || gIndex == 0) {
            gIndex = 1;
        }
        if (gStatus == null) {
            $("#get-discount").addClass('btn-dark');
            gStatus = true
        }
        if (gGetUrlDiscounts == null || gGetUrlDiscounts == "") {
            gGetUrlDiscounts = "https://localhost:7139/api/Discount/get-all-discount"
        }
        var P_Index = gIndex;
        gNameSearch = $('#search').val();
        $.ajax({
            type: "GET",
            url: gGetUrlDiscounts,
            data: ({
                PageSize: 5,
                PageIndex: P_Index,
                Name: gNameSearch
            }),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                if (response.items.length >= 0) {
                    var rows = ""
                    for (var i = 0; i < response.items.length; i++) {
                        var item = response.items[i];
                        console.log(item)
                        if (item.isEnable == true) {
                            rows += `
                        <tr>
                               <td>
                                    <p>${item.idPromotion}</p>
                                        </td>
                                        <td>
                                            <p>${item.name}</p>
                                        </td>
                                        <td>
                                            <p>${item.percent}</p>
                                        </td>
                                        <td>
                                            <p>${item.fromDate}</p>
                                        </td>
                                        <td>
                                            <p>${item.toDate}</p>
                                        </td>
                                        <td>
                                            <p>${item.createdAt}</p>
                                        </td>
                                        <td>
                                            <p>${item.updatedAt}</p>
                                        </td>
                                        <td>
                                    <button class="btn btn-success edit" data-id="${item.idPromotion}" >Sửa</button> |
                                    <button class="btn btn-danger btn-delete" data-id="${item.idPromotion}" id = "delete" >Xóa</button> |
                                </td>
                            </tr>
                    `
                        }
                        else {
                            rows += `
                         <tr>
                               <td>
                                    <p>${item.idPromotion}</p>
                                        </td>
                                        <td>
                                            <p>${item.name}</p>
                                        </td>
                                        <td>
                                            <p>${item.percent}</p>
                                        </td>
                                        <td>
                                            <p>${item.fromDate}</p>
                                        </td>
                                        <td>
                                            <p>${item.toDate}</p>
                                        </td>
                                        <td>
                                            <p>${item.createdAt}</p>
                                        </td>
                                        <td>
                                            <p>${item.updatedAt}</p>
                                        </td>
                                        <td>
                                    <button class="btn btn-success restore" data-id="${item.idPromotion}">Khôi phục</button> 
                                </td>
                            </tr>
                    `
                        }
                    }
                }
                //paging
                var pag = "";
                if (response.pageCount == P_Index) {
                    if (P_Index != 1) {
                        pag = `
                                    <li data-id="${response.pageIndex - 1}" class="page-item"><button data-id="${response.pageIndex - 1}" class="page-link" >Previous</button></li>
                                    <li data-id="${response.pageIndex - 1}" class="page-item"><button data-id="${response.pageIndex - 1}" class="page-link" >${response.pageIndex - 1}</button></li>
                                    <li data-id="${response.pageIndex}" class="page-item"><button style="background-color: gray;" data-id="${response.pageIndex}" class="page-link" >${response.pageIndex}</button></li>
                                `
                    }
                    else if (P_Index == 1) {
                        pag = "";
                    }
                    else {
                        pag = `
                                    <li data-id="${response.pageIndex}" class="page-item"><button style="background-color: gray;" data-id="${response.pageIndex}" class="page-link" >${response.pageIndex}</button></li>
                                   `
                    }
                }
                else if (P_Index == 1) {
                    if (response.totalRecord == 0) {
                        pag = "";
                    }
                    else {
                        pag = `
                                
                                <li data-id="${response.pageIndex}" class="page-item"><button style="background-color: gray;" data-id="${response.pageIndex}" class="page-link" >${response.pageIndex}</button></li>
                                <li data-id="${response.pageIndex + 1}" class="page-item"><button data-id="${response.pageIndex + 1}" class="page-link" >${response.pageIndex + 1}</button></li>
                                <li data-id="${response.pageIndex + 1}" class="page-item"><button data-id="${response.pageIndex + 1}" class="page-link" >Next</button></li>
                            `
                    }
                }
                else {
                    pag = `
                                <li data-id="${response.pageIndex - 1}" class="page-item"><button data-id="${response.pageIndex - 1}" class="page-link" >Previous</button></li>
                                <li data-id="${response.pageIndex - 1}" class="page-item"><button data-id="${response.pageIndex - 1}" class="page-link" >${response.pageIndex - 1}</button></li>
                                <li data-id="${response.pageIndex}" class="page-item"><button style="background-color: gray;" data-id="${response.pageIndex}" class="page-link" >${response.pageIndex}</button></li>
                                <li data-id="${response.pageIndex + 1}" class="page-item"><button data-id="${response.pageIndex + 1}" class="page-link" >${response.pageIndex + 1}</button></li>
                                <li data-id="${response.pageIndex + 1}" class="page-item"><button data-id="${response.pageIndex + 1}" class="page-link" >Next</button></li>
                            `
                }
                $('.render-discount').html(rows);
                $('.render-deleted-discount').html("");
                $('.pagination').html(pag);
                _this.PageLink(gIndex, gNameSearch, gStatus, gGetUrlDiscounts);
                _this.Search(1, gNameSearch, gStatus, gGetUrlDiscounts);
                _this.Delete();
                _this.Restore();
                _this.EditGet();
                //GetDeletedDiscount

                /*_this.GetDiscount(gIndex, gNameSearch, gStatus, gGetUrlDiscounts)*/
            }
        }).fail(function (jqXHR, textStatus, errorThrown) {
            $('#canhbao').html("Hong biet nua")
        });
        $('#Role').html("Admin");
        $('#Controller').html("Discount");
        $('#get-deleted-discount').off('click').on('click', function () {
            $(this).addClass('btn-dark');
            $("#get-discount").removeClass('btn-dark');
            gStatus = false
            gGetUrlDiscounts = "https://localhost:7139/api/Discount/get-deleted-discounts"
            _this.GetDiscount(1, gNameSearch, gStatus, gGetUrlDiscounts)
        })
        $('#get-discount').off('click').on('click', function () {
            $(this).addClass('btn-dark');
            $("#get-deleted-discount").removeClass('btn-dark');
            gStatus = true
            gGetUrlDiscounts = "https://localhost:7139/api/Discount/get-all-discount"
            _this.GetDiscount(1, gNameSearch, gStatus, gGetUrlDiscounts)
        })
    }
}
new Discount()


