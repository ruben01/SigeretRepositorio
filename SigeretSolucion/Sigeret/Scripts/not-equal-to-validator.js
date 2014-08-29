$.validator.addMethod(
    'notequalto',
   function (value, element, params) {
       console.debug(element);
       if (!this.optional(element)) {
           var otherProperty = $('#' + params.otherproperty)
           return (otherProperty.val() != value);
       }
       return true;
   });

$.validator.unobtrusive.adapters.add(
    'notequalto', ['otherproperty', 'otherpropertyname'], function (options) {
        var params = {
            otherproperty: options.params.otherproperty,
            otherpropertyname: options.params.otherpropertyname
        };
        options.rules['notequalto'] = params;
        options.messages['notequalto'] = options.message;
    });