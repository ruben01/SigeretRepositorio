/*********************************************
*                                            *
* Saúl H. Sánchez                            *
* Código javascript global de la aplicación  *
*                                            *
**********************************************/

// Saúl H. Sánchez
// Configuración global para errores en el cliente
// utilizando estilos de bootstrap, para la validación
// de Jquery.
//
//Configuración del Jquery validate para soportar los estilos de bootstrap -- Saúl H. S.
$.validator.setDefaults({
    highlight: function (element) { //añadir clases de error
        $(element).closest('.form-group').addClass('has-error');
        $(element).closest('.field-validation-error').addClass('help-block');
    },
    unhighlight: function (element) { //remover clases de error
        $(element).closest('.form-group').removeClass('has-error');
    },
    errorElement: 'span', //elemento contenedor de los mensajes de validacion
    errorClass: 'help-block', //clase css para dar estilos de rror
    errorPlacement: function (error, element) { //logica para el posicionamiento de los errores
        if (element.parent('.input-group').length) {
            error.insertAfter(element.parent());
        } else {
            error.insertAfter(element);
        }
    },
    ignore: '.ignore', //ignorar validacion en elementos con dicha clase
    onkeyup: false, //no activar validacion al presionado de cada tecla
    //onfocusout: false,
    onsubmit: true //activar validacion en cada submit de formulario
});

//Inicialización por defecto para los elementos chosen single. -- Saúl H. S.
$(".chosen").chosen({ width: '100%', allow_single_deselect: true });
$(".chosen").attr('data-placeholder', 'Seleccione una opción');
$(".chosen").chosen().change();
$(".chosen").trigger('chosen:updated');
$(".chosen-multiple").chosen({ width: '100%' });
$(".chosen-multiple").attr('data-placeholder', 'Seleccione una opción');
$(".chosen-multiple").chosen().change();
$(".chosen-multiple").trigger('chosen:updated');

function fnValidateDynamicContent(form) {
    form.removeData("validator");
    form.removeData("unobtrusiveValidation");
    $.validator.unobtrusive.parse(form);
    // This line is important and added for client side validation to trigger, 
    // without this it didn't fire client side errors.
    form.validate();
}

function fnClearForm(elementClass, isForm) {
    if (isForm) {
        $(elementClass)[0].reset();
    }
    else {
        $(elementClass + " :input").each(function () {
            $(this).val('');
        });
    }
}

$(document).ready(function () {
    //Inicializacion de mascarillas usando el plugin Jquery Input mask -- Saúl H. S.
    var telefono = '(999) 999-9999'; //telefono
    var cedula = '999-9999999-9'; //cedula

    //Remover datos si estos no estan completos.
    $('.telefono').inputmask(telefono, { "clearIncomplete": true });
    $('.cedula').inputmask(cedula, { "clearIncomplete": true });

    //quitar mascara de inputs luego del submit -- Saúl H. S.
    $('form').submit(function () {
        if ($(this).valid()) {
            $('.telefono').inputmask('remove');
            $('.cedula').inputmask('remove');
        }
    });

    //Activación de tooltips -- Saúl H. S.
    $(document.body).tooltip({
        selector: '[data-toggle="tooltip"]'
    });

    //Activación de popovers -- Saúl H. S.
    $(document.body).popover({
        selector: '[data-toggle="popover"]'
    });

    //datepickers, configuración -- Saúl H. S.
    $('.fechaFutura').datepicker({
        dateFormat: 'yy/mm/dd',
        changeMonth: true,
        changeYear: true,
        minDate: 1
    });

    $('.fecha').datepicker({
        dateFormat: 'yy/mm/dd',
        changeMonth: true,
        changeYear: true
    });

    $('.CtimePicker').timepicker({
        hourMin: 8,
        hourMax: 22,
        timeFormat: "HH:mm:ss",
        addSliderAccess: true,
        sliderAccessArgs: { touchonly: false }
    });
});

function updateDropdown(data, url, dropdownElement, chosenElement) {
    var html = '<option value="" ></option>';
    var select = "";
    $.ajax({
        url: url,
        type: 'POST',
        data: JSON.stringify(data),
        dataType: 'json',
        contentType: 'application/json',
        success: function (data) {
            if (data == undefined || data == "") {
                dropdownElement.html(html);

            }
            else {
                $.each(data, function (key, row) {
                    //fill the dropdown
                    select = (row.Selected) ? ' selected = "true" ' : "";
                    html +=
                        '<option value="' + row.Value + '"' + select + '>'
                        + row.Text +
                        '</option>';
                });
                dropdownElement.html(html);
            }
            if (chosenElement != null) {
                chosenElement.trigger('chosen:updated');
            }
        }
    });
}