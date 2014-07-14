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

$('.table').dataTable({
    "sPaginationType": "full_numbers",
    "oLanguage": {
        "sLengthMenu": 'Mostrar _MENU_ registros por páginas',
        "sZeroRecords": 'No hay registros entontrados.',
        "sInfo": 'Mostrando _START_ a _END_ de _TOTAL_ registros',
        "sInfoEmpty": 'Mostrando 0 a 0 de 0 records',
        "sInfoFiltered": '(filtrado desde _MAX_ total registros)',
        "sSearch": "",
        "oPaginate": {
            "sFirst": 'Primero',
            "sPrevious": 'Anterior',
            "sNext": 'Siguiente',
            "sLast": 'Último'
        },
        "bJQueryUI": true
    }
});

$(function () {
    $('.table').each(function () {
        var datatable = $(this);
        // estilo y label inline para el input de busqueda
        var search_input = datatable.closest('.dataTables_wrapper').find('div[id$=_filter] input');
        search_input.attr('placeholder', 'Buscar');
        search_input.addClass('form-control input-sm');

        // posicionamiento de la informacion de los records filtrados
        var length_sel = datatable.closest('.dataTables_wrapper').find('div[id$=_info]');
        length_sel.css('margin-top', '2px');
    });
});

$(document).ready(function () {
    //Inicializacion de mascarillas usando el plugin Jquery Input mask -- Saúl H. S.
    var telefono = '(999) 999-9999'; //telefono
    var cedula = '999-9999999-9'; //cedula
    var quantity = '9[9][9]'; //mascara para las solicitudes
    //Remover datos si estos no estan completos.
    $('.telefono').inputmask(telefono, { "clearIncomplete": true });
    $('.cedula').inputmask(cedula, { "clearIncomplete": true });
    $('.txtQuantity').inputmask(quantity, { "clearIncomplete": true });

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