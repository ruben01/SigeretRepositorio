<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>


<%@ Register Assembly="Telerik.ReportViewer.WebForms, Version=8.1.14.804, Culture=neutral, PublicKeyToken=a9d7983dfcc261be" Namespace="Telerik.ReportViewer.WebForms" TagPrefix="telerik" %>

<!DOCTYPE html>
<script runat="server">

    protected void ReportViewer1_Load(object sender, EventArgs e)
    {
        if (ViewBag.IsPost != null)
        {
            Sigeret.Reportes.PrestamosUsuarios rpt = new Sigeret.Reportes.PrestamosUsuarios();
            rpt.ReportParameters["FechaInicio"].Value = Convert.ToDateTime(ViewBag.FechaInicio);
            rpt.ReportParameters["FechaFin"].Value = Convert.ToDateTime(ViewBag.FechaFin);
            ReportViewer1.ReportSource = rpt;
            ReportViewer1.RefreshReport();
        }
    }
</script>


<html>
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>Prestamos Usuarios</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        
        <telerik:ReportViewer ID="ReportViewer1" runat="server" Height="600px" Width="100%" OnLoad="ReportViewer1_Load" ViewMode="PrintPreview" ZoomMode="FullPage">
<typereportsource typename="Sigeret.Reportes.PrestamosUsuarios, Sigeret, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"></typereportsource>
</telerik:ReportViewer>
        
    </div>
    </form>
</body>
</html>
