$(document).ready(function () {
    $(".btn-removeCart").click(function (e) {
        e.preventDefault();
        $.ajax({
            type: 'DELETE',
            url: '/Customer/Cart/RemoveCart',
            data:''
        })
        console.log(this);
    });
});