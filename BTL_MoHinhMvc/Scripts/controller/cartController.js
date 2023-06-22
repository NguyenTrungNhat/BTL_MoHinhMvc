var cart = {
    init: function () {
        cart.regEvents();
    },
    regEvents: function () {

        $('#btnUpdate').off('click').on('click', function () {
            var listProduct = $('.cart-plus-minus-box');
            var cartList = [];
            $.each(listProduct, function (i, item) {
                cartList.push({
                    Quantity: $(item).val(),
                    Product: {
                        ProductNumber: $(item).data('id')
                    }
                });
            });

            $.ajax({
                url: '/Cart/Update',
                data: { cartModel: JSON.stringify(cartList) },
                dataType: 'json',
                type: 'POST',
                sucsses: function (res) {
                    if (res.status == true) {
                        window.location.href = '/Cart/Index';
                    }
                }
            });
        });
        $('#btnDeleteAll').off('click').on('click', function () {
            $.ajax({
                url: '/Cart/DeleteAll',
                dataType: 'json',
                type: 'POST',
                sucsses: function (res) {
                    if (res.status == true) {
                    }
                }
            });
        });
        $('.li-product-remove-class').off('click').on('click', function (e) {
            $.ajax({
                data: { id: $(this).data('id') },
                url: '/Cart/Delete',
                dataType: 'json',
                type: 'POST',
                sucsses: function (res) {
                    if (res.status == true) {
                        window.location.href = '/Cart/Index';
                    }
                }
            });
        });
    }
}
cart.init()

var listTotal = $('.product-subtotal > .amount');
console.log(listTotal)
var Total = 0;
for (let i = 0; i < listTotal.length; i++) {
    Total += parseFloat(listTotal[i].innerHTML);
}
$('#Cart-total-span').text(Total.toString());