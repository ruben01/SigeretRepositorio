﻿/*********************************************
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
});