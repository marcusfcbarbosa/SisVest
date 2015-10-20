window.createAdmin = {


    events: function () {
        $('#Senha').strength();

        $('#Senha').strength({
            strengthClass: 'strength',
            strengthMeterClass: 'strength_meter',
            strengthButtonClass: 'button_strength',
            strengthButtonText: 'Show password',
            strengthButtonTextToggle: 'Hide Password'
        });
    },
    init: function () {
        window.createAdmin.events();
    }

};