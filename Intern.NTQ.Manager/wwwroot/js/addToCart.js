
$(function () {
    $(".side-cart-wrap").css('display', 'none');
    $('#cd-cart-trigger').click(function () {
        var x = $(".side-cart-wrap").css('display');
        if (x == 'none') {
            $(".side-cart-wrap").fadeIn();
        }
    });
});
$(function () {
    $('#cls-side-cart').click(function () {
        var x = $(".side-cart-wrap").css('display');
        if (x != 'none') {
            $(".side-cart-wrap").fadeOut();
        }
    });
});

$(document).on('click', '.product-list .product .btn-buy', function () {
    let id = $(this).attr('id').replaceAll('btn', 'pr');
    console.log(id);
    let parents_item = $(this).parents('.product');

    let img = parents_item.find('.card-img-top').attr('src');
    let name = parents_item.find('.card-text').html();
    let price = parents_item.find('.new-price').html();
    if ($('.list-product-cart #' + id).length) {
        let count = $('.list-product-cart #' + id).find('.quantity').html();

        $('.list-product-cart #' + id).find('.quantity').html(parseInt(count) + 1);
    } else {
        let html = `<div class="product-cart" id="${id}">
                        <div class="img-product">
                            <img src="${img}" alt="">
                        </div>
                        <div class="name-price-product">
                            <div class="name-product">
                                <a href="">${name}</a>
                            </div>
                        <div class="price-product">
                            <span class="quantity">1</span>
                            <span>x</span>
                            <span class="price">${price}</span>
                            <span>₫</span>
                        </div>
                    </div>
                    <div class="delete-icon">
                        <i class="fa fa-times-circle-o" aria-hidden="true"></i>
                    </div>
                    </div>`;

        $('.list-product-cart').append(html);
    }

    var count_cart = $('.count_cart').html();
    $('.count_cart').html(parseInt(count_cart) + 1);

    var sum_money = $('#total_money').html();
    let sum_money_total = parseFloat(sum_money.replaceAll(',', '')) + parseFloat(price.replaceAll(',', ''));
    console.log(sum_money)
    $('#total_money').html(sum_money_total.toFixed(1).replace(/(\d)(?=(\d{3})+\.)/g, "$1,").toString());
    console.log(sum_money_total)
});

$(document).on('click', ' .list-product-cart .product-cart .delete-icon', function () {
    var id = $(this).parents('.product-cart').attr('id');
    $('.list-product-cart #' + id).remove();

    // tính toán tiền giảm
    let parents_item = $(this).parents('.product-cart');
    let price = parents_item.find('.price').html().replaceAll(',', '');
    let count = parents_item.find('.quantity').html().replaceAll(',', '');
    var sum_money = $('#total_money').html().replaceAll(',', '');
    let sum_money_total = parseFloat(sum_money) - parseFloat(price) * parseFloat(count);
    console.log(sum_money, sum_money_total, parseFloat(price) * parseFloat(count));
    $('#total_money').html(sum_money_total.toFixed(1).replace(/(\d)(?=(\d{3})+\.)/g, "$1,").toString());

    // giảm số lượng trên giỏ hàng
    var count_cart = $('.count_cart').html();
    $('.count_cart').html(parseInt(count_cart) - parseInt(count));
});