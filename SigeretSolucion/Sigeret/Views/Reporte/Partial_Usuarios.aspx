<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>


<%@ Register Assembly="Telerik.ReportViewer.WebForms, Version=8.1.14.804, Culture=neutral, PublicKeyToken=a9d7983dfcc261be" Namespace="Telerik.ReportViewer.WebForms" TagPrefix="telerik" %>

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
