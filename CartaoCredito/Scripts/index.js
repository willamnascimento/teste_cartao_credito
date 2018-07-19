$(document).ready(function () {
    $(".input-numero-cartao").mask("0000 0000 0000 0000");

    $(".btn-validar").click(function () {
        var input = $(".input-numero-cartao").val();
        if (input.length < 16) {
            alert("Informe o número do cartão corretamente!");
            return false;
        }
    });

    $("#form-valida-cartao-credito").on("submit", function (e) {
        e.preventDefault();
        var form = $(this);
        $.ajax({
            method: 'POST',
            url: form[0].action,
            data: form.serialize(),
            success: function (data) {
                $(".retorno-validacao").text(data);
            }
        });
    });
});