
function SetModal() {

    $(document).ready(function () {
        $(function () {
            $.ajaxSetup({ cache: false });

            $("a[data-modal]").on("click",
                function (e) {
                    $('#myModalContent').load(this.href,
                        function () {
                            $('#myModal').modal({
                                keyboard: true
                            },
                                'show');
                            bindForm(this);
                        });
                    return false;
                });
        });
    });
}

function bindForm(dialog) {
    $('form', dialog).submit(function () {
        $.ajax({
            url: this.action,
            type: this.method,
            data: $(this).serialize(),
            success: function (result) {
                if (result.success) {
                    $('#myModal').modal('hide');
                    $('#AddressTarget').load(result.url);
                    SetModal();
                } else {
                    $('#myModalContent').html(result);
                    bindForm(dialog);
                }
            }
        });

        SetModal();
        return false;
    });
}

function SearchZipCode() {
    $(document).ready(function () {

        function clean_form_address() {
            $("#Address_Street").val("");
            $("#Address_District").val("");
            $("#Address_City").val("");
            $("#Address_State").val("");
        }

        $("#Address_ZipCode").blur(function () {

            //new var "zipCode" just with numbers.
            var zipCode = $(this).val().replace(/\D/g, '');

            if (zipCode != "") {

                var cepValidate = /^[0-9]{8}$/;

                if (cepValidate.test(zipCode)) {

                    $.getJSON("https://viacep.com.br/ws/" + zipCode + "/json/?callback=?",
                        function (data) {

                            if (!("erro" in data)) {
                                $("#Address_Street").val(data.logradouro);
                                $("#Address_District").val(data.bairro);
                                $("#Address_City").val(data.localidade);
                                $("#Address_State").val(data.uf);
                            } 
                            else {
                                clean_form_address();
                                alert("Zip Code not found.");
                            }
                        });
                }
            }
            else {
                clean_form_address();
            }
        });
    });
}