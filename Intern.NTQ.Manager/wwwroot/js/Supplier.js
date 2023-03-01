class Supplier {
    constructor(gIndex, gNameSearch, gStatus, gGetUrlSuppliers) {
        this.GetSupplier(gIndex, gNameSearch, gStatus, gGetUrlSuppliers);
        this.Create()
    }
    GetSupplier(gIndex, gNameSearch, gStatus, gGetUrlSuppliers) {
        if (localStorage.getItem("mess") != "" && localStorage.getItem("mess") != null) {
            console.log("v")
            $('#msgAlert').css("display", "block");;
            setTimeout(function () {
                $('#msgAlert').fadeOut('slow');
            }, 2000);
            
        }
        localStorage.setItem("mess","")
        let _this = this

        if (gIndex == null || gIndex == 0) {
            gIndex = 1;
        }
        if (gStatus == null) {
            $("#get-suppliers").addClass('btn-dark');
            gStatus = true
        }
        if (gGetUrlSuppliers == null || gGetUrlSuppliers == "") {
            gGetUrlSuppliers = "https://localhost:7139/api/Suppliers/get-by-name-supplier"
        }
        var P_Index = gIndex;
        gNameSearch = $('#search').val();
        console.log("ok");
        $.ajax({
            type: "GET",
            url: gGetUrlSuppliers,
            data: ({
                PageSize: 5,
                PageIndex: P_Index,
                Name: gNameSearch
            }),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                console.log(response)
                if (response.items.length >= 0) {
                    var rows = ""
                        for (var i = 0; i < response.items.length; i++) {
                            var item = response.items[i];
                            if (item.isEnable == true) {
                                rows += `
                        <tr>
                                <td>
                                    <p>${item.id}</p>
                                </td>
                                <td>
                                    <p>${item.name}</p>
                                </td>
                                <td>
                                    <p>${item.phone}</p>
                                </td>
                                <td>
                                    <p>${item.address}</p>
                                </td>
                                <td>
                                    <p>${item.email}</p>
                                </td>
                                <td>
                                    <p>${item.createdAt}</p>
                                </td>
                                <td>
                                    <p>${item.updatedAt}</p>
                                </td>
                                <td>
                                    <p class="btn btn-success">Đang Dùng</p>
                                </td>
                                <td>
                                    <button class="btn btn-success edit" data-id="${item.id}" >Sửa</button> |
                                    <button class="btn btn-danger btn-delete" data-id="${item.id}" id = "delete" >Xóa</button> |
                                </td>
                            </tr>
                    `
                            }
                            else {
                                rows += `
                        <tr>
                                <td>
                                    <p>${item.id}</p>
                                </td>
                                <td>
                                    <p>${item.name}</p>
                                </td>
                                <td>
                                    <p>${item.phone}</p>
                                </td>
                                <td>
                                    <p>${item.address}</p>
                                </td>
                                <td>
                                    <p>${item.email}</p>
                                </td>
                                <td>
                                    <p>${item.createdAt}</p>
                                </td>
                                <td>
                                    <p>${item.updatedAt}</p>
                                </td>
                                <td>
                                    <p class="btn btn-danger">Đã Xóa</p>
                                </td>
                                <td>
                                    <button class="btn btn-success restore" data-id="${item.id}">Khôi phục</button> 
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
                    $('.render-suppliers').html(rows);
                    $('.render-deleted-suppliers').html("");
                $('.pagination').html(pag);
                $('.page-link').off('click').on('click', function () {
                    var click = $(this);
                    var P_Index = click.data('id');
                    if (gStatus == true) {
                        $.ajax({
                            type: "GET",
                            url: gGetUrlSuppliers,
                            data: ({
                                PageSize: 5,
                                PageIndex: P_Index,
                                Name: gNameSearch
                            }),
                            dataType: "json",
                            contentType: "application/json; charset=utf-8",
                            success: function (response) {
                                /*if (response.items.length > 0) {
                                    var rows = ""
                                    rows = response.items.map((item) =>
                                        `
                                <tr>
                                        <td>
                                            <p>${item.id}</p>
                                        </td>
                                        <td>
                                            <p>${item.name}</p>
                                        </td>
                                        <td>
                                            <p>${item.phone}</p>
                                        </td>
                                        <td>
                                            <p>${item.address}</p>
                                        </td>
                                        <td>
                                            <p>${item.email}</p>
                                        </td>
                                        <td>
                                            <p>${item.createdAt}</p>
                                        </td>
                                        <td>
                                            <p>${item.updatedAt}</p>
                                        </td>
                                        <td>
                                            <button class="btn btn-success edit" data-id="${item.id}" >Sửa</button> |
                                            <button class="btn btn-danger btn-delete" data-id="${item.id}" id = "delete" >Xóa</button> |
                                        </td>
                                    </tr>
                            `
                                    )
                                }
                                $('.render-suppliers').html(rows);
                                $('.render-deleted-suppliers').html("");*/
                                gIndex = P_Index;
                                $('#paging').html(pag);
                                new Supplier(gIndex);
                            }
                        }).fail(function (jqXHR, textStatus, errorThrown) {
                            $('#warning').html("")
                        });
                    } else {
                        $.ajax({
                            type: "GET",
                            url: "https://localhost:7139/api/Suppliers/get-deleted-suppliers",
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
                                            <p>${item.id}</p>
                                        </td>
                                        <td>
                                            <p>${item.name}</p>
                                        </td>
                                        <td>
                                            <p>${item.phone}</p>
                                        </td>
                                        <td>
                                            <p>${item.address}</p>
                                        </td>
                                        <td>
                                            <p>${item.email}</p>
                                        </td>
                                        <td>
                                            <p>${item.createdAt}</p>
                                        </td>
                                        <td>
                                            <p>${item.updatedAt}</p>
                                        </td>
                                        <td>
                                           <button class="btn btn-success restore" data-id="${item.id}" >Khôi phục</button> |
                                        </td>
                                    </tr>
                            `
                                    )
                                }
                                $('.render-suppliers').html("");
                                $('.render-deleted-suppliers').html(rows);
                                gIndex = P_Index;
                                $('#paging').html(pag);
                                new Supplier(gIndex, gNameSearch, false, gGetUrlSuppliers);
                            }
                        }).fail(function (jqXHR, textStatus, errorThrown) {
                            $('#warning').html("")
                        });
                    }
                    new Supplier(gIndex);
                })
                $('.btn-delete').off('click').on('click', function () {
                    var Id = $(this).data("id");
                    $.ajax({
                        type: "Delete",
                        url: "https://localhost:7139/api/Suppliers/delete-supplier?id=" + Id,
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
                            
                            _this.GetSupplier()
                        }
                    }).fail(function (jqXHR, textStatus, errorThrown) {
                        $('#warning').html("")
                    });
                });
                $('.edit').off('click').on('click', function () {
                    var Id = $(this).data("id");
                    $.ajax({
                        type: "GET",
                        url: "https://localhost:7139/api/Suppliers/get-by-id?id=" + Id,
                        data: {}
                        ,
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        success: function (response) {
                            window.location.href = "Supplier/Edit?Address=" + response.address + "&Email=" + response.email + "&Id=" + response.id + "&Name=" + response.name + "&Phone=" + response.phone + "";
                            /* $.ajax({
                                 type: "POST",
                                 url: "Supplier/Edit?Address=" + response.address + "&Email=" + response.email + "&Id=" + response.id + "&Name=" + response.name + "&Phone=" + response.phone + "",
                                 data:
                                 {
                                     Address: response.address,
                                     Email: response.email,
                                     Id: response.id,
                                     Name: response.name,
                                     Phone: response.phone,
                                 }
                                 ,
                                 dataType: "json",
                                 contentType: "application/json; charset=utf-8",
                                 success: function () {
                                     console.log("okokokokokokok");
                                 }
                             }).fail(function (jqXHR, textStatus, errorThrown) {
                                 $('#warning').html("")
                             });*/
                        }
                    }).fail(function (jqXHR, textStatus, errorThrown) {
                        $('#warning').html("")
                    });
                });
                $('.restore').off('click').on('click', function () {
                    var Id = $(this).data("id");
                    $.ajax({
                        type: "PUT",
                        url: "https://localhost:7139/api/Suppliers/restore?id=" + Id,
                        data: {}
                        ,
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        success: function (response) {
                            /*var mess = "restore"
                            window.location.href = "/Admin/Supplier/SendMess?mess=" + mess;*/
                            localStorage.setItem('mess', "Khôi phục thành công");
                            let mess = localStorage.getItem("mess");
                            var row =
                                `<div id="msgAlert" class="alert alert-success" role="alert">
                                    ${mess}
                                 </div>`
                            $('#msgAlert').html(row)

                            /*setTimeout(function () {
                                $('#msgAlert').fadeOut('slow');
                            }, 2000);*/
                            $("#get-suppliers").addClass('btn-dark');
                            $("#get-deleted-suppliers").removeClass('btn-dark');
                            _this.GetSupplier()

                        }
                    }).fail(function (jqXHR, textStatus, errorThrown) {
                        $('#warning').html("")
                    });
                });
                $('#search-press').off('click').on('click', function () {
                    var click = $(this);
                    var P_Index = click.data('id');
                    console.log(P_Index);
                    gNameSearch = $('#search').val();
                    if (gStatus == true) {
                        $.ajax({
                            type: "GET",
                            url: "https://localhost:7139/api/Suppliers/get-by-name-supplier",
                            data: ({
                                PageSize: 5,
                                PageIndex: 1,
                                Name: gNameSearch
                            }),
                            dataType: "json",
                            contentType: "application/json; charset=utf-8",
                            success: function (response) {
                                console.log(response.items)
                                if (response.items.length > 0) {
                                    var rows = ""
                                    for (var i = 0; i < response.items.length; i++) {
                                        var item = response.items[i];
                                        rows +=
                                            `
                                <tr>
                                        <td>
                                            <p>${item.id}</p>
                                        </td>
                                        <td>
                                            <p>${item.name}</p>
                                        </td>
                                        <td>
                                            <p>${item.phone}</p>
                                        </td>
                                        <td>
                                            <p>${item.address}</p>
                                        </td>
                                        <td>
                                            <p>${item.email}</p>
                                        </td>
                                        <td>
                                            <p>${item.createdAt}</p>
                                        </td>
                                        <td>
                                            <p>${item.updatedAt}</p>
                                        </td>
                                        <td>
                                            <button class="btn btn-success edit" data-id="${item.id}" >Sửa</button> |
                                            <button class="btn btn-success btn-delete" data-id="${item.id}" id = "delete" >Xóa</button> |
                                        </td>
                                    </tr>
                            `
                                    }
                                }
                                $('.render-suppliers').html(rows);
                                gIndex = P_Index;
                                $('#paging').html(pag);
                                new Supplier(1, gNameSearch, true);
                            }
                        }).fail(function (jqXHR, textStatus, errorThrown) {
                            $('#warning').html("")
                        });
                    } else {
                        $.ajax({
                            type: "GET",
                            url: "https://localhost:7139/api/Suppliers/get-deleted-suppliers",
                            data: ({
                                PageSize: 5,
                                PageIndex: 1,
                                Name: gNameSearch
                            }),
                            dataType: "json",
                            contentType: "application/json; charset=utf-8",
                            success: function (response) {
                                console.log(response.items)
                                if (response.items.length > 0) {
                                    var rows = ""
                                    rows = response.items.map((item) =>
                                        `
                                <tr>
                                        <td>
                                            <p>${item.id}</p>
                                        </td>
                                        <td>
                                            <p>${item.name}</p>
                                        </td>
                                        <td>
                                            <p>${item.phone}</p>
                                        </td>
                                        <td>
                                            <p>${item.address}</p>
                                        </td>
                                        <td>
                                            <p>${item.email}</p>
                                        </td>
                                        <td>
                                            <p>${item.createdAt}</p>
                                        </td>
                                        <td>
                                            <p>${item.updatedAt}</p>
                                        </td>
                                        <td>
                                            <button class="btn btn-success restore" data-id="${item.id}">Khôi phục</button> |
                                        </td>
                                    </tr>
                            `
                                    )
                                }
                                $('.render-deleted-suppliers').html(rows);
                                $('.render-suppliers').html("");
                                gIndex = P_Index;
                                $('#paging').html(pag);
                                new Supplier(1, gNameSearch, false);
                            }
                        }).fail(function (jqXHR, textStatus, errorThrown) {
                            $('#warning').html("")
                        });
                    }

                })
                $('#edit').on('click', function () {
                    var id = $('#Prime').data("id");
                    var name = $('#Name').val();
                    var phone = $('#Phone').val();
                    var address = $('#Address').val();
                    var email = $('#Email').val();
                    var createdAt = $('#CreatedAt').val();
                    var isEnable = $('#isEnable').val();
                    var vnf_regex = /((09|03|07|08|05)+([0-9]{8})\b)/g;
                    var email_regex = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
                    var check = true;
                    if (address == "") {
                        $('#textAddress').html("Nhập lại địa chỉ");
                        check = false
                    }
                    else if (name == "") {
                        $('#textName').html("Nhập lại tên");
                        check = false
                    }
                    else if (vnf_regex.test(phone) == false) {
                        $('#textPhone').html('Số điện thoại của bạn không đúng định dạng!');
                        check = false
                    }
                    if (email_regex.test(email) == false) {
                        $('#textEmail').html("Không đúng định dạng Email");
                        check = false
                    }
                    if (check == true) {
                        $.ajax({
                            type: "PUT",
                            url: "https://localhost:7139/api/Suppliers/update/?id=" + id,
                            data: JSON.stringify({
                                Id: id,
                                Name: name,
                                Phone: phone,
                                Address: address,
                                Email: email,
                                CreatedAt: createdAt,
                                IsEnable: Boolean(isEnable)
                            })
                            ,
                            dataType: "json",
                            contentType: "application/json; charset=utf-8",

                            success: function (response) {
                                var mess = "update"
                                window.location.href = "/Admin/Supplier/SendMess?mess=" + mess;
                            }
                        }).fail(function (jqXHR, textStatus, errorThrown) {
                            $('#warning').html("")
                        });
                    }
                });
                //GetDeletedSupplier
                $('#get-deleted-suppliers').off('click').on('click', function () {
                    $(this).addClass('btn-dark');
                    $("#get-suppliers").removeClass('btn-dark');
                    gStatus = false
                    gGetUrlSuppliers = "https://localhost:7139/api/Suppliers/get-deleted-suppliers"
                    new Supplier(1, gNameSearch, gStatus, gGetUrlSuppliers)
                })
                $('#get-suppliers').off('click').on('click', function () {
                    $(this).addClass('btn-dark');
                    $("#get-deleted-suppliers").removeClass('btn-dark');
                    gStatus = true
                    gGetUrlSuppliers = "https://localhost:7139/api/Suppliers/get-by-name-supplier"
                    new Supplier(1, gNameSearch, gStatus, gGetUrlSuppliers)
                })
            }
        }).fail(function (jqXHR, textStatus, errorThrown) {
            $('#warning').html("")
        });
        $('#Role').html("Admin");
        $('#Controller').html("Supplier");
    }
    Create() {
        $('#create').on('click', function () {
            console.log("okokokok")
            var name = $('#name').val();
            var phone = $('#phone').val();
            var address = $('#address').val();
            var email = $('#email').val();
            var vnf_regex = /((09|03|07|08|05)+([0-9]{8})\b)/g;
            var email_regex = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
            var check = true;
            if (address == "") {
                $('#textAddress').html("Nhập lại địa chỉ");
                check = false
            }
            else if (name == "") {
                $('#textName').html("Nhập lại tên");
                check = false
            }
            else if (vnf_regex.test(phone) == false) {
                $('#textPhone').html('Số điện thoại của bạn không đúng định dạng!');
                check = false
            }
            if (email_regex.test(email) == false) {
                $('#textEmail').html("Không đúng định dạng Email");
                check = false
            }
            if (check == true) {
                $.ajax({
                    type: "POST",
                    url: "https://localhost:7139/api/Suppliers/add-supplier",
                    data: JSON.stringify({
                        Name: name,
                        Phone: phone,
                        Address: address,
                        Email: email,

                    })
                    ,
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",

                    success: function (response) {
                        var mess = "create"
                        window.location.href = "/Admin/Supplier/SendMess?mess=" + mess;
                    }
                }).fail(function (jqXHR, textStatus, errorThrown) {
                    $('#warning').html("")
                });
            }
        });
    }
}

new Supplier();

