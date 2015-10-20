window.createAdmin = {
    events: function () {
        //$('#Senha').strength();
        $('#Senha').strength({
            strengthClass: 'strength',
            strengthMeterClass: 'strength_meter',
            strengthButtonClass: 'button_strength',
            strengthButtonText: 'Mostre a senha',
            strengthButtonTextToggle: 'Esconda a senha'
        });

        $("#btnVoltar").click(function () {
            location.href = '@Url.Action("Index")';
        });
    },
    init: function () {
        window.createAdmin.events();
    }
};