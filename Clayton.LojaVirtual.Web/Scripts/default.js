/*default.js*/

$(document).ready(function () {
    var quantitiy = 0;
    $('.quantity-right-plus').click(function (e) {
        // Stop acting like a button
        e.preventDefault();

        // Get the field name
        var vjsFild = $(this).data("field");
        var vjsProdutoId = $('#ProdutoId' + vjsFild).val();
        var vjsReturnUrl = $('#ReturnUrl' + vjsFild).val();

        $.redirectPost("Adicionar", { "produtoId": vjsProdutoId, "returnUrl": vjsReturnUrl });


    });

    $('.quantity-left-minus').click(function (e) {
        // Stop acting like a button
        e.preventDefault();
        // Get the field name
        var vjsFild = $(this).data("field");

        var quantity = parseInt($('#quantidade-' + vjsFild).val());
     
        if (quantity > 0) {
            
            var vjsQtd = (quantity - 1);
            var vjsProdutoId = $('#ProdutoId' + vjsFild).val();
            var vjsReturnUrl = $('#ReturnUrl' + vjsFild).val();
   
            $.redirectPost("Remover", { "produtoId": vjsProdutoId, "returnUrl": vjsReturnUrl, "quantidade": vjsQtd });

   //         $('#quantidade-' + vjsFild).val(vjsQtd);
        }
    });



    

    // jquery extend function
    $.extend(
    {
        redirectPost: function (location, args) {
            var formContent = '';
            var form = "";
            $.each(args, function (key, value) {
                formContent += '<input type="hidden" name="' + key + '" value="' + value + '">';
            });

            form = '<form id="formQtdItem" action="' + location + '" method="POST">' + formContent + '</form>';
            $(document.body).append(form);

            $("#formQtdItem").submit();
        }
    });


    function fnAlterarQuantidadeCarrinho(prmJsUrlPost, prmJsId, prmJsQtd, prmJsReturnUrl) {
         $.ajax({
                type: "Post",
                url: prmJsUrlPost,
                contentType: 'application/x-www-form-urlencoded;',
                dataType: "html",
                cache: false,
                global: true,
                data: { "produtoId": prmJsId, "returnUrl": prmJsReturnUrl, "quantidade": prmJsQtd },
                beforeSend: function () {
                    console.log("beforeSend");

                },
                success: function (data) {
                    //console.log("success");
                    alert("success");
                   
                },
                error: function (xhr, errorMessage, thrownError) {
                    //console.log("error");
                    alert("error");
                }
             });
     }



       
    
});