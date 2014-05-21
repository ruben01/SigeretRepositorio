<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<%@ Register assembly="Telerik.ReportViewer.WebForms, Version=8.0.14.507, Culture=neutral, PublicKeyToken=a9d7983dfcc261be" namespace="Telerik.ReportViewer.WebForms" tagprefix="telerik" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>ReporteUsuarios</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        
        <telerik:ReportViewer ID="ReportViewer1" runat="server" Height="600px" Width="800px" Skin="WebBlue" ZoomMode="FullPage">
<typereportsource typename="Sigeret.Reportes.ReporteUsuarios, Sigeret, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"></typereportsource>
</telerik:ReportViewer>
        
    </div>
    </form>
</body>
</html>
