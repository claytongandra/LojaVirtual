/*default.js*/

//$(document).ready(function () {

//});

var app = {};

$(function () {
    app.iniciarlizar();


    
});


app.iniciarlizar = function () {

    app.Search();
    app.RandomIconSports();
    app.ObterEsportes();
    app.ObterMarcas();
    app.ObterTimes();
    app.AdicionarRemoverItemCarrinho();

}


app.AdicionarRemoverItemCarrinho = function () {

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
}



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


app.Search = function () {
  
    $('a[href="#search"]').on('click', function (event) {
        event.preventDefault();
        $('#search').addClass('open');
        $('#search > form > input[type="search"]').focus();
    });

    $('#search, #search button.close').on('click keyup', function (event) {
        if (event.target == this || event.target.className == 'close' || event.keyCode == 27) {
            $(this).removeClass('open');
        }
    });
           
}

app.RandomIconSports = function () {
    var iconArray = new Array(  //"fa fa-futbol-o",
                                "ionicons ion-ios-tennisball",
                                "ionicons ion-ios-basketball",
                                "ionicons ion-ios-baseball",
                                "ionicons ion-ios-americanfootball",
                                "ionicons ion-ios-football",
                                "ionicons ion-ios-tennisball-outline",
                                "ionicons ion-ios-basketball-outline",
                                "ionicons ion-ios-americanfootball-outline",
                                "ionicons ion-ios-football-outline"
                                );
    var randIcon = Math.floor(Math.random() * (iconArray.length));
    $('#icon-front').addClass(iconArray[randIcon]);
    //console.log(randIcon + " " + iconArray[randIcon]);
}

app.ObterEsportes = function () {

    $.getJSON('/menu/obteresportes', function (data) {

        var vjsHtml = "";
        var vjsQtdItensColunas = 8;
        var vjsColunas = 1;
        var vjsCriarColuna = 1;
        var vjsNovaColuna = 1;
        var vjsQtdEmColuna = 0;
        var vjsQtdFinal = 0;

        while ((data.length / vjsColunas) > vjsQtdItensColunas) {
             vjsColunas++;
        }

        $(data).each(function () {

            if (vjsCriarColuna == vjsNovaColuna)
            {
                vjsHtml = vjsHtml + "<div class='nav-column col-md-4'>";

                if (vjsCriarColuna == 1) {
                    vjsHtml = vjsHtml + "   <h3>Esportes</h3>";
                }
                else {
                    vjsHtml = vjsHtml + "   <hr class='hrMenu' />";
                }
                vjsHtml = vjsHtml + "   <ul>";
                vjsNovaColuna++;
             }

            vjsHtml = vjsHtml + "       <li><a href='#'>" + this.CategoriaDescricao + "</a></li>";

            vjsQtdFinal++;
            vjsQtdEmColuna++;

            if (vjsQtdEmColuna == vjsQtdItensColunas || vjsQtdFinal == data.length) {

                vjsHtml = vjsHtml + "   </ul>";
                vjsHtml = vjsHtml + "</div>";
                vjsQtdEmColuna = 0;
                vjsCriarColuna++;

                if (vjsCriarColuna < vjsColunas) {
                    vjsNovaColuna = vjsCriarColuna;
                }
            }
           
        });

        $("#esportes").append(vjsHtml);
    });

};


app.ObterMarcas = function () {
    
    $.getJSON('/menu/obtermarcas', function (data) {

        //var vjsHtml = "";
        //var vjsQtdItensColunas = 8;
        //var vjsColunas = 1;
        //var vjsCriarColuna = 1;
        //var vjsNovaColuna = 1;
        //var vjsQtdEmColuna = 0;
        //var vjsQtdFinal = 0;

        //while ((data.length / vjsColunas) > vjsQtdItensColunas) {
        //    vjsColunas++;
        //}

        //$(data).each(function () {

        //    if (vjsCriarColuna == vjsNovaColuna) {
        //        vjsHtml = vjsHtml + "<div class='nav-column col-md-4'>";

        //        if (vjsCriarColuna == 1) {
        //            vjsHtml = vjsHtml + "   <h3>Marcas</h3>";
        //        }
        //        else {
        //            vjsHtml = vjsHtml + "   <hr class='hrMenu' />";
        //        }
        //        vjsHtml = vjsHtml + "   <ul>";
        //        vjsNovaColuna++;
        //    }

        //    vjsHtml = vjsHtml + "       <li><a href='#'>" + this.MarcaDescricao + "</a></li>";

        //    vjsQtdFinal++;
        //    vjsQtdEmColuna++;

        //    if (vjsQtdEmColuna == vjsQtdItensColunas || vjsQtdFinal == data.length) {

        //        vjsHtml = vjsHtml + "   </ul>";
        //        vjsHtml = vjsHtml + "</div>";
        //        vjsQtdEmColuna = 0;
        //        vjsCriarColuna++;

        //        if (vjsCriarColuna < vjsColunas) {
        //            vjsNovaColuna = vjsCriarColuna;
        //        }
        //    }

        //});

        vjsHtml = fnGerarColunasMenu(data, 6, "Marcas", "/nav/marca/");
      // alert(vjsHtml);
        $(".marcas").append(vjsHtml);
    });

};

app.ObterTimes = function () {
    var vjsHtml = "";

    $.getJSON('/menu/obterclubesnacionais', function (data) {
        vjsHtml = fnGerarColunasMenu(data, 6, "Clubes Nacionais", "/nav/times/");

        $("#times").append("<div = class='row'>" + vjsHtml + "</div>");
    });
    
    

    $.getJSON('/menu/obterclubesinternacionais', function (data) {
        vjsHtml = fnGerarColunasMenu(data, 6, "Clubes Internacionais", "/nav/times/");
        $("#times").append("<div = class='row'>"+vjsHtml+"</div>");
    });


    
};

function fnGerarColunasMenu(prmDataJson, prmQtdItensColunas, prmTextoMenu, prmUrlBase) {
    var vjsHtml = "";
   // var vjsQtdItensColunas = 8;
    var vjsColunas = 1;
    var vjsCriarColuna = 1;
    var vjsNovaColuna = 1;
    var vjsQtdEmColuna = 0;
    var vjsQtdFinal = 0;

    while ((prmDataJson.length / vjsColunas) > prmQtdItensColunas) {
        vjsColunas++;
    }

    $(prmDataJson).each(function () {

        if (vjsCriarColuna == vjsNovaColuna) {
            vjsHtml = vjsHtml + "<div class='nav-column col-md-4'>";

            if (vjsCriarColuna == 1) {
                vjsHtml = vjsHtml + "   <h3>" + prmTextoMenu + "</h3>";
            }
            else {
                vjsHtml = vjsHtml + "   <hr class='hrMenu' />";
            }
            vjsHtml = vjsHtml + "   <ul>";
            vjsNovaColuna++;
        }

        vjsHtml = vjsHtml + "       <li><a href='" + prmUrlBase + this.Codigo + "/"+this.UrlSeo + "'>" + this.Descricao + "</a></li>";

        vjsQtdFinal++;
        vjsQtdEmColuna++;

        if (vjsQtdEmColuna == prmQtdItensColunas || vjsQtdFinal == prmDataJson.length) {

            vjsHtml = vjsHtml + "   </ul>";
            vjsHtml = vjsHtml + "</div>";
            vjsQtdEmColuna = 0;
            vjsCriarColuna++;

            if (vjsCriarColuna < vjsColunas) {
                vjsNovaColuna = vjsCriarColuna;
            }
        }

    });
    return(vjsHtml)
}