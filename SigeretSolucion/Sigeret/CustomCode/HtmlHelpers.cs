using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Sigeret.CustomCode
{
    public static class HtmlHelpers
    {
        /// <summary>
        /// Html LabelFor helper diseñado para añadir una clase custom
        /// utilizada para los mensajes de error de bootstrap
        /// </summary>
        /// <typeparam name="TModel">Modelo del cual generar</typeparam>
        /// <typeparam name="TValue">Valor del modelo</typeparam>
        /// <param name="html">objeto HtmlHelper referenciado</param>
        /// <param name="expression">Expresion Lambda pra conocer el campo seleccionado</param>
        /// <param name="htmlAttributes">Atributos html a agregar al tag</param>
        /// <returns></returns>
        public static MvcHtmlString CLabelFor<TModel, TValue>(
            this HtmlHelper<TModel> html,
            Expression<Func<TModel, TValue>> expression,
            object htmlAttributes = null)
        {
            IDictionary<string, object> attributes = null;
            addCustomClass(htmlAttributes, out attributes, "control-label");

            return html.LabelFor(expression, attributes);
        }

        public static MvcHtmlString CTextBoxFor<TModel, TProperty>(
            this HtmlHelper<TModel> html,
            Expression<Func<TModel, TProperty>> expression,
            object htmlAttributes = null)
        {
            IDictionary<string, object> attributes = null;
            addCustomClass(htmlAttributes, out attributes, "form-control");

            return html.TextBoxFor(expression, attributes);
        }

        public static MvcHtmlString CTextAreaFor<TModel, TProperty>(
            this HtmlHelper<TModel> html, 
            Expression<Func<TModel, TProperty>> expression,
            object htmlAttributes = null)
        {
            IDictionary<string, object> attributes = null;
            addCustomClass(htmlAttributes, out attributes, "form-control");

            return html.TextAreaFor(expression, attributes);
        }

        public static MvcHtmlString CPasswordFor<TModel, TProperty>(
            this HtmlHelper<TModel> html,
            Expression<Func<TModel, TProperty>> expression,
            object htmlAttributes = null)
        {
            IDictionary<string, object> attributes = null;
            addCustomClass(htmlAttributes, out attributes, "form-control");

            return html.PasswordFor(expression, attributes);
        }

        public static MvcHtmlString CValidationMessageFor<TModel, TProperty>(
            this HtmlHelper<TModel> html,
            Expression<Func<TModel, TProperty>> expression)
        {
            IDictionary<string, object> attributes = null;
            addCustomClass(null, out attributes, "help-block");

            return html.ValidationMessageFor(expression, null, attributes);
        }

        public static MvcHtmlString CTextBox(
            this HtmlHelper html,
            string name,
            object value = null,
           object htmlAttributes = null)
        {
            IDictionary<string, object> attributes = null;
            addCustomClass(htmlAttributes, out attributes, "form-control");

            return html.TextBox(name, value, attributes);
        }

        public static MvcHtmlString CTextArea(
            this HtmlHelper html,
            string name,
           object htmlAttributes = null)
        {
            IDictionary<string, object> attributes = null;
            addCustomClass(htmlAttributes, out attributes, "form-control");

            return html.TextArea(name, attributes);
        }

        public static MvcHtmlString CPassword(
            this HtmlHelper html,
            string name,
            object value = null,
            IDictionary<string, object> htmlAttributes = null)
        {
            IDictionary<string, object> attributes = null;
            addCustomClass(htmlAttributes, out attributes, "form-control");

            return html.Password(name, value, attributes);
        }

        public static SelectList ToSelectList<EnumType>(this EnumType enumObject)
            where EnumType : struct, IComparable, IFormattable, IConvertible
        {
            var values = from EnumType e in Enum.GetValues(typeof(EnumType))
                         select new { Id = e, Name = e.ToString() };
            return new SelectList(values, "Id", "Name", enumObject);
        }

        public static MvcHtmlString CDropDownListFor<TModel, TProperty>(
            this HtmlHelper<TModel> html,
            Expression<Func<TModel, TProperty>> expression, 
            IEnumerable<SelectListItem> selectList = null,
            string optionLabel = null,
            object htmlAttributes = null)
        {
            IDictionary<string, object> attributes = null;
            addCustomClass(htmlAttributes, out attributes, "form-control");

            return html.DropDownListFor(expression, selectList, (optionLabel == null) ? string.Empty : optionLabel, attributes);
        }

        public static MvcHtmlString CListBoxFor<TModel, TProperty>(
            this HtmlHelper<TModel> html,
            Expression<Func<TModel, TProperty>> expression,
            IEnumerable<SelectListItem> selectList,
            object htmlAttributes = null)
        {
            IDictionary<string, object> attributes = null;
            addCustomClass(htmlAttributes, out attributes, "form-control");

            return html.ListBoxFor(expression, selectList, attributes);
        }

        public static MvcHtmlString CDropDownList(
            this HtmlHelper html,
            string name,
            IEnumerable<SelectListItem> selectList,
            string optionLabel = null,
            object htmlAttributes = null)
        {
            IDictionary<string, object> attributes = null;
            addCustomClass(htmlAttributes, out attributes, "form-control");

            return html.DropDownList(name, selectList, (optionLabel == null) ? string.Empty : optionLabel, attributes);
        }

        private static void addCustomClass(
            object htmlAttributes,
            out IDictionary<string, object> attributes,
            string Customclass)
        {
            attributes = (IDictionary<string, object>)HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            object cssClass;
            if (attributes.TryGetValue("class", out cssClass) == false)
            {
                cssClass = "";
            }
            attributes["class"] = cssClass + " " + Customclass;    
        }

        public static MvcHtmlString AuthorizeActionLink
            (
             this HtmlHelper htmlHelper,
             MenuAuthorize menuAuthorize,
             string linkText,
             string actionName,
             string controllerName,
             object routeValues = null,
             object htmlAttributes = null
            )
        {
            if (menuAuthorize.HasPermission(actionName, controllerName))
            {
                return htmlHelper.ActionLink(linkText, actionName, controllerName, routeValues, htmlAttributes);
            }

            return MvcHtmlString.Create("");
        }

        public static MvcHtmlString AuthorizeActionLink
            (
             this HtmlHelper htmlHelper,
             MenuAuthorize menuAuthorize,
             string linkText,
             string actionName,
             string controllerName,
             string actionDescriptor,
             object routeValues = null,
             object htmlAttributes = null
            )
        {
            if (menuAuthorize.HasPermission(actionName, controllerName, actionDescriptor))
            {
                return htmlHelper.ActionLink(linkText, actionName, controllerName, routeValues, htmlAttributes);
            }

            return MvcHtmlString.Create("");
        }

    }
}