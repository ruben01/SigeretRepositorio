<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Register Assembly="Telerik.ReportViewer.WebForms, Version=8.0.14.507, Culture=neutral, PublicKeyToken=a9d7983dfcc261be" Namespace="Telerik.ReportViewer.WebForms" TagPrefix="telerik" %>

<!DOCTYPE html>
<script runat="server">

    protected void ReportViewer1_Load(object sender, EventArgs e)
    {
        var type = String.Format("Sigeret.Reportes.{0}", ViewBag.ReportToRender);
        var ReportType = SIGERET.CustomCode.GlobalHelpers.GetType(type);
        //Telerik.Reporting.IReportDocument Report = (Telerik.Reporting.IReportDocument)Activator.CreateInstance(ReportType);
        //ReportViewer1.Report = Report;
        Telerik.Reporting.ReportSource Report = Activator.CreateInstance(ReportType);
        ReportViewer1.ReportSource = Report;
        ReportViewer1.RefreshReport();
    }
</script>

<html>
<head id="Head1" runat="server">
    <meta name="viewport" content="width=device-width" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <telerik:ReportViewer ID="ReportViewer1" runat="server" Height="600px" Width="100%" Skin="WebBlue" ZoomMode="FullPage"  ViewMode="PrintPreview" OnLoad="ReportViewer1_Load">
                <typereportsource typename="Sigeret.Reportes.ReporteUsuarios, Sigeret, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"></typereportsource>           
            </telerik:ReportViewer>
        </div>
    </form>
</body>
</html>
