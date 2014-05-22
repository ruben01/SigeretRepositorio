namespace Sigeret.Reportes
{
    partial class PrestamosUsuarios
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Telerik.Reporting.Group group1 = new Telerik.Reporting.Group();
            Telerik.Reporting.Group group2 = new Telerik.Reporting.Group();
            Telerik.Reporting.ReportParameter reportParameter1 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter2 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.Drawing.StyleRule styleRule1 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule2 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule3 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule4 = new Telerik.Reporting.Drawing.StyleRule();
            this.DsPrestamosUsuarios = new Telerik.Reporting.SqlDataSource();
            this.labelsGroupHeaderSection = new Telerik.Reporting.GroupHeaderSection();
            this.labelsGroupFooterSection = new Telerik.Reporting.GroupFooterSection();
            this.usuarioCaptionTextBox = new Telerik.Reporting.TextBox();
            this.nombreCaptionTextBox = new Telerik.Reporting.TextBox();
            this.descripcionCaptionTextBox = new Telerik.Reporting.TextBox();
            this.edificioCaptionTextBox = new Telerik.Reporting.TextBox();
            this.aulaCaptionTextBox = new Telerik.Reporting.TextBox();
            this.estatusCaptionTextBox = new Telerik.Reporting.TextBox();
            this.fechaCaptionTextBox = new Telerik.Reporting.TextBox();
            this.horaInicioCaptionTextBox = new Telerik.Reporting.TextBox();
            this.horaFinCaptionTextBox = new Telerik.Reporting.TextBox();
            this.equipoCaptionTextBox = new Telerik.Reporting.TextBox();
            this.cantidadCaptionTextBox = new Telerik.Reporting.TextBox();
            this.usuarioGroupHeaderSection = new Telerik.Reporting.GroupHeaderSection();
            this.usuarioGroupFooterSection = new Telerik.Reporting.GroupFooterSection();
            this.usuarioDataTextBox = new Telerik.Reporting.TextBox();
            this.pageHeader = new Telerik.Reporting.PageHeaderSection();
            this.reportNameTextBox = new Telerik.Reporting.TextBox();
            this.pageFooter = new Telerik.Reporting.PageFooterSection();
            this.currentTimeTextBox = new Telerik.Reporting.TextBox();
            this.pageInfoTextBox = new Telerik.Reporting.TextBox();
            this.reportHeader = new Telerik.Reporting.ReportHeaderSection();
            this.titleTextBox = new Telerik.Reporting.TextBox();
            this.reportFooter = new Telerik.Reporting.ReportFooterSection();
            this.detail = new Telerik.Reporting.DetailSection();
            this.nombreDataTextBox = new Telerik.Reporting.TextBox();
            this.descripcionDataTextBox = new Telerik.Reporting.TextBox();
            this.edificioDataTextBox = new Telerik.Reporting.TextBox();
            this.aulaDataTextBox = new Telerik.Reporting.TextBox();
            this.estatusDataTextBox = new Telerik.Reporting.TextBox();
            this.fechaDataTextBox = new Telerik.Reporting.TextBox();
            this.horaInicioDataTextBox = new Telerik.Reporting.TextBox();
            this.horaFinDataTextBox = new Telerik.Reporting.TextBox();
            this.equipoDataTextBox = new Telerik.Reporting.TextBox();
            this.cantidadDataTextBox = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // DsPrestamosUsuarios
            // 
            this.DsPrestamosUsuarios.ConnectionString = "SigeretContext";
            this.DsPrestamosUsuarios.Name = "DsPrestamosUsuarios";
            this.DsPrestamosUsuarios.Parameters.AddRange(new Telerik.Reporting.SqlDataSourceParameter[] {
            new Telerik.Reporting.SqlDataSourceParameter("@FechaInicio", System.Data.DbType.Date, "=Parameters.FechaInicio.Value"),
            new Telerik.Reporting.SqlDataSourceParameter("@FechaFin", System.Data.DbType.Date, "=Parameters.FechaFin.Value")});
            this.DsPrestamosUsuarios.SelectCommand = "dbo.Usp_PrestamosUsuarios";
            this.DsPrestamosUsuarios.SelectCommandType = Telerik.Reporting.SqlDataSourceCommandType.StoredProcedure;
            // 
            // labelsGroupHeaderSection
            // 
            this.labelsGroupHeaderSection.Height = Telerik.Reporting.Drawing.Unit.Cm(1.1058331727981567D);
            this.labelsGroupHeaderSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.usuarioCaptionTextBox,
            this.nombreCaptionTextBox,
            this.descripcionCaptionTextBox,
            this.edificioCaptionTextBox,
            this.aulaCaptionTextBox,
            this.estatusCaptionTextBox,
            this.fechaCaptionTextBox,
            this.horaInicioCaptionTextBox,
            this.horaFinCaptionTextBox,
            this.equipoCaptionTextBox,
            this.cantidadCaptionTextBox});
            this.labelsGroupHeaderSection.Name = "labelsGroupHeaderSection";
            this.labelsGroupHeaderSection.PrintOnEveryPage = true;
            // 
            // labelsGroupFooterSection
            // 
            this.labelsGroupFooterSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.71437495946884155D);
            this.labelsGroupFooterSection.Name = "labelsGroupFooterSection";
            this.labelsGroupFooterSection.Style.Visible = false;
            // 
            // usuarioCaptionTextBox
            // 
            this.usuarioCaptionTextBox.CanGrow = true;
            this.usuarioCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.usuarioCaptionTextBox.Name = "usuarioCaptionTextBox";
            this.usuarioCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.3799242973327637D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.usuarioCaptionTextBox.StyleName = "Caption";
            this.usuarioCaptionTextBox.Value = "Usuario";
            // 
            // nombreCaptionTextBox
            // 
            this.nombreCaptionTextBox.CanGrow = true;
            this.nombreCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(1.48575758934021D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.nombreCaptionTextBox.Name = "nombreCaptionTextBox";
            this.nombreCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.3799242973327637D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.nombreCaptionTextBox.StyleName = "Caption";
            this.nombreCaptionTextBox.Value = "Nombre";
            // 
            // descripcionCaptionTextBox
            // 
            this.descripcionCaptionTextBox.CanGrow = true;
            this.descripcionCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(2.9185984134674072D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.descripcionCaptionTextBox.Name = "descripcionCaptionTextBox";
            this.descripcionCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.3799242973327637D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.descripcionCaptionTextBox.StyleName = "Caption";
            this.descripcionCaptionTextBox.Value = "Descripcion";
            // 
            // edificioCaptionTextBox
            // 
            this.edificioCaptionTextBox.CanGrow = true;
            this.edificioCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(4.3514394760131836D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.edificioCaptionTextBox.Name = "edificioCaptionTextBox";
            this.edificioCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.3799242973327637D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.edificioCaptionTextBox.StyleName = "Caption";
            this.edificioCaptionTextBox.Value = "Edificio";
            // 
            // aulaCaptionTextBox
            // 
            this.aulaCaptionTextBox.CanGrow = true;
            this.aulaCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(5.7842803001403809D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.aulaCaptionTextBox.Name = "aulaCaptionTextBox";
            this.aulaCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.3799242973327637D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.aulaCaptionTextBox.StyleName = "Caption";
            this.aulaCaptionTextBox.Value = "Aula";
            // 
            // estatusCaptionTextBox
            // 
            this.estatusCaptionTextBox.CanGrow = true;
            this.estatusCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(7.2171211242675781D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.estatusCaptionTextBox.Name = "estatusCaptionTextBox";
            this.estatusCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.3799242973327637D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.estatusCaptionTextBox.StyleName = "Caption";
            this.estatusCaptionTextBox.Value = "Estatus";
            // 
            // fechaCaptionTextBox
            // 
            this.fechaCaptionTextBox.CanGrow = true;
            this.fechaCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(8.6499624252319336D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.fechaCaptionTextBox.Name = "fechaCaptionTextBox";
            this.fechaCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.3799242973327637D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.fechaCaptionTextBox.StyleName = "Caption";
            this.fechaCaptionTextBox.Value = "Fecha";
            // 
            // horaInicioCaptionTextBox
            // 
            this.horaInicioCaptionTextBox.CanGrow = true;
            this.horaInicioCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(10.082802772521973D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.horaInicioCaptionTextBox.Name = "horaInicioCaptionTextBox";
            this.horaInicioCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.3799242973327637D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.horaInicioCaptionTextBox.StyleName = "Caption";
            this.horaInicioCaptionTextBox.Value = "Hora Inicio";
            // 
            // horaFinCaptionTextBox
            // 
            this.horaFinCaptionTextBox.CanGrow = true;
            this.horaFinCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(11.515644073486328D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.horaFinCaptionTextBox.Name = "horaFinCaptionTextBox";
            this.horaFinCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.3799242973327637D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.horaFinCaptionTextBox.StyleName = "Caption";
            this.horaFinCaptionTextBox.Value = "Hora Fin";
            // 
            // equipoCaptionTextBox
            // 
            this.equipoCaptionTextBox.CanGrow = true;
            this.equipoCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(12.948484420776367D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.equipoCaptionTextBox.Name = "equipoCaptionTextBox";
            this.equipoCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.3799242973327637D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.equipoCaptionTextBox.StyleName = "Caption";
            this.equipoCaptionTextBox.Value = "Equipo";
            // 
            // cantidadCaptionTextBox
            // 
            this.cantidadCaptionTextBox.CanGrow = true;
            this.cantidadCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(14.381325721740723D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.cantidadCaptionTextBox.Name = "cantidadCaptionTextBox";
            this.cantidadCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.3799242973327637D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.cantidadCaptionTextBox.StyleName = "Caption";
            this.cantidadCaptionTextBox.Value = "Cantidad";
            // 
            // usuarioGroupHeaderSection
            // 
            this.usuarioGroupHeaderSection.Height = Telerik.Reporting.Drawing.Unit.Cm(1.1058331727981567D);
            this.usuarioGroupHeaderSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.usuarioDataTextBox});
            this.usuarioGroupHeaderSection.Name = "usuarioGroupHeaderSection";
            // 
            // usuarioGroupFooterSection
            // 
            this.usuarioGroupFooterSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.71437495946884155D);
            this.usuarioGroupFooterSection.Name = "usuarioGroupFooterSection";
            // 
            // usuarioDataTextBox
            // 
            this.usuarioDataTextBox.CanGrow = true;
            this.usuarioDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.usuarioDataTextBox.Name = "usuarioDataTextBox";
            this.usuarioDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.3799242973327637D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.usuarioDataTextBox.StyleName = "Data";
            this.usuarioDataTextBox.Value = "=Fields.Usuario";
            // 
            // pageHeader
            // 
            this.pageHeader.Height = Telerik.Reporting.Drawing.Unit.Cm(1.1058331727981567D);
            this.pageHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.reportNameTextBox});
            this.pageHeader.Name = "pageHeader";
            // 
            // reportNameTextBox
            // 
            this.reportNameTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.reportNameTextBox.Name = "reportNameTextBox";
            this.reportNameTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(15.708333015441895D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.reportNameTextBox.StyleName = "PageInfo";
            this.reportNameTextBox.Value = "PrestamosUsuarios";
            // 
            // pageFooter
            // 
            this.pageFooter.Height = Telerik.Reporting.Drawing.Unit.Cm(1.1058331727981567D);
            this.pageFooter.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.currentTimeTextBox,
            this.pageInfoTextBox});
            this.pageFooter.Name = "pageFooter";
            // 
            // currentTimeTextBox
            // 
            this.currentTimeTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.currentTimeTextBox.Name = "currentTimeTextBox";
            this.currentTimeTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(7.8277082443237305D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.currentTimeTextBox.StyleName = "PageInfo";
            this.currentTimeTextBox.Value = "SIGERET";
            // 
            // pageInfoTextBox
            // 
            this.pageInfoTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(7.9335417747497559D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.pageInfoTextBox.Name = "pageInfoTextBox";
            this.pageInfoTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(7.8277082443237305D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.pageInfoTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.pageInfoTextBox.StyleName = "PageInfo";
            this.pageInfoTextBox.Value = "=PageNumber";
            // 
            // reportHeader
            // 
            this.reportHeader.Height = Telerik.Reporting.Drawing.Unit.Cm(2.0529167652130127D);
            this.reportHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.titleTextBox});
            this.reportHeader.Name = "reportHeader";
            // 
            // titleTextBox
            // 
            this.titleTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.titleTextBox.Name = "titleTextBox";
            this.titleTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(15.814167022705078D), Telerik.Reporting.Drawing.Unit.Cm(2D));
            this.titleTextBox.StyleName = "Title";
            this.titleTextBox.Value = "Prestamos Aprovados Registrados";
            // 
            // reportFooter
            // 
            this.reportFooter.Height = Telerik.Reporting.Drawing.Unit.Cm(0.71437495946884155D);
            this.reportFooter.Name = "reportFooter";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Cm(1.1058331727981567D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.nombreDataTextBox,
            this.descripcionDataTextBox,
            this.edificioDataTextBox,
            this.aulaDataTextBox,
            this.estatusDataTextBox,
            this.fechaDataTextBox,
            this.horaInicioDataTextBox,
            this.horaFinDataTextBox,
            this.equipoDataTextBox,
            this.cantidadDataTextBox});
            this.detail.Name = "detail";
            // 
            // nombreDataTextBox
            // 
            this.nombreDataTextBox.CanGrow = true;
            this.nombreDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(1.48575758934021D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.nombreDataTextBox.Name = "nombreDataTextBox";
            this.nombreDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.3799242973327637D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.nombreDataTextBox.StyleName = "Data";
            this.nombreDataTextBox.Value = "=Fields.Nombre";
            // 
            // descripcionDataTextBox
            // 
            this.descripcionDataTextBox.CanGrow = true;
            this.descripcionDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(2.9185984134674072D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.descripcionDataTextBox.Name = "descripcionDataTextBox";
            this.descripcionDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.3799242973327637D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.descripcionDataTextBox.StyleName = "Data";
            this.descripcionDataTextBox.Value = "=Fields.Descripcion";
            // 
            // edificioDataTextBox
            // 
            this.edificioDataTextBox.CanGrow = true;
            this.edificioDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(4.3514394760131836D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.edificioDataTextBox.Name = "edificioDataTextBox";
            this.edificioDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.3799242973327637D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.edificioDataTextBox.StyleName = "Data";
            this.edificioDataTextBox.Value = "=Fields.Edificio";
            // 
            // aulaDataTextBox
            // 
            this.aulaDataTextBox.CanGrow = true;
            this.aulaDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(5.7842803001403809D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.aulaDataTextBox.Name = "aulaDataTextBox";
            this.aulaDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.3799242973327637D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.aulaDataTextBox.StyleName = "Data";
            this.aulaDataTextBox.Value = "=Fields.Aula";
            // 
            // estatusDataTextBox
            // 
            this.estatusDataTextBox.CanGrow = true;
            this.estatusDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(7.2171211242675781D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.estatusDataTextBox.Name = "estatusDataTextBox";
            this.estatusDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.3799242973327637D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.estatusDataTextBox.StyleName = "Data";
            this.estatusDataTextBox.Value = "=Fields.Estatus";
            // 
            // fechaDataTextBox
            // 
            this.fechaDataTextBox.CanGrow = true;
            this.fechaDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(8.6499624252319336D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.fechaDataTextBox.Name = "fechaDataTextBox";
            this.fechaDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.3799242973327637D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.fechaDataTextBox.StyleName = "Data";
            this.fechaDataTextBox.Value = "=Fields.Fecha";
            // 
            // horaInicioDataTextBox
            // 
            this.horaInicioDataTextBox.CanGrow = true;
            this.horaInicioDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(10.082802772521973D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.horaInicioDataTextBox.Name = "horaInicioDataTextBox";
            this.horaInicioDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.3799242973327637D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.horaInicioDataTextBox.StyleName = "Data";
            this.horaInicioDataTextBox.Value = "=Fields.HoraInicio";
            // 
            // horaFinDataTextBox
            // 
            this.horaFinDataTextBox.CanGrow = true;
            this.horaFinDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(11.515644073486328D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.horaFinDataTextBox.Name = "horaFinDataTextBox";
            this.horaFinDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.3799242973327637D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.horaFinDataTextBox.StyleName = "Data";
            this.horaFinDataTextBox.Value = "=Fields.HoraFin";
            // 
            // equipoDataTextBox
            // 
            this.equipoDataTextBox.CanGrow = true;
            this.equipoDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(12.948484420776367D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.equipoDataTextBox.Name = "equipoDataTextBox";
            this.equipoDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.3799242973327637D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.equipoDataTextBox.StyleName = "Data";
            this.equipoDataTextBox.Value = "=Fields.Equipo";
            // 
            // cantidadDataTextBox
            // 
            this.cantidadDataTextBox.CanGrow = true;
            this.cantidadDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(14.381325721740723D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.cantidadDataTextBox.Name = "cantidadDataTextBox";
            this.cantidadDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.3799242973327637D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.cantidadDataTextBox.StyleName = "Data";
            this.cantidadDataTextBox.Value = "=Fields.Cantidad";
            // 
            // PrestamosUsuarios
            // 
            this.DataSource = this.DsPrestamosUsuarios;
            group1.GroupFooter = this.labelsGroupFooterSection;
            group1.GroupHeader = this.labelsGroupHeaderSection;
            group1.Name = "labelsGroup";
            group2.GroupFooter = this.usuarioGroupFooterSection;
            group2.GroupHeader = this.usuarioGroupHeaderSection;
            group2.Groupings.Add(new Telerik.Reporting.Grouping("=Fields.Usuario"));
            group2.Name = "usuarioGroup";
            this.Groups.AddRange(new Telerik.Reporting.Group[] {
            group1,
            group2});
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.labelsGroupHeaderSection,
            this.labelsGroupFooterSection,
            this.usuarioGroupHeaderSection,
            this.usuarioGroupFooterSection,
            this.pageHeader,
            this.pageFooter,
            this.reportHeader,
            this.reportFooter,
            this.detail});
            this.Name = "PrestamosUsuarios";
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Mm(25.399999618530273D), Telerik.Reporting.Drawing.Unit.Mm(25.399999618530273D), Telerik.Reporting.Drawing.Unit.Mm(25.399999618530273D), Telerik.Reporting.Drawing.Unit.Mm(25.399999618530273D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            reportParameter1.Name = "FechaInicio";
            reportParameter1.Text = "FechaInicio";
            reportParameter1.Type = Telerik.Reporting.ReportParameterType.DateTime;
            reportParameter1.Value = "= Date(2014, 01, 01)";
            reportParameter2.Name = "FechaFin";
            reportParameter2.Text = "FechaFin";
            reportParameter2.Type = Telerik.Reporting.ReportParameterType.DateTime;
            reportParameter2.Value = "= Now()";
            this.ReportParameters.Add(reportParameter1);
            this.ReportParameters.Add(reportParameter2);
            this.Style.BackgroundColor = System.Drawing.Color.White;
            styleRule1.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector("Title")});
            styleRule1.Style.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(238)))), ((int)(((byte)(232)))));
            styleRule1.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            styleRule1.Style.Font.Name = "Verdana";
            styleRule1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(18D);
            styleRule2.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector("Caption")});
            styleRule2.Style.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            styleRule2.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(222)))), ((int)(((byte)(209)))));
            styleRule2.Style.Font.Name = "Verdana";
            styleRule2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            styleRule2.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            styleRule3.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector("Data")});
            styleRule3.Style.Font.Name = "Verdana";
            styleRule3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            styleRule3.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            styleRule4.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector("PageInfo")});
            styleRule4.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
            styleRule4.Style.Font.Name = "Verdana";
            styleRule4.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            styleRule4.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.StyleSheet.AddRange(new Telerik.Reporting.Drawing.StyleRule[] {
            styleRule1,
            styleRule2,
            styleRule3,
            styleRule4});
            this.Width = Telerik.Reporting.Drawing.Unit.Cm(15.814167022705078D);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.SqlDataSource DsPrestamosUsuarios;
        private Telerik.Reporting.GroupHeaderSection labelsGroupHeaderSection;
        private Telerik.Reporting.TextBox usuarioCaptionTextBox;
        private Telerik.Reporting.TextBox nombreCaptionTextBox;
        private Telerik.Reporting.TextBox descripcionCaptionTextBox;
        private Telerik.Reporting.TextBox edificioCaptionTextBox;
        private Telerik.Reporting.TextBox aulaCaptionTextBox;
        private Telerik.Reporting.TextBox estatusCaptionTextBox;
        private Telerik.Reporting.TextBox fechaCaptionTextBox;
        private Telerik.Reporting.TextBox horaInicioCaptionTextBox;
        private Telerik.Reporting.TextBox horaFinCaptionTextBox;
        private Telerik.Reporting.TextBox equipoCaptionTextBox;
        private Telerik.Reporting.TextBox cantidadCaptionTextBox;
        private Telerik.Reporting.GroupFooterSection labelsGroupFooterSection;
        private Telerik.Reporting.GroupHeaderSection usuarioGroupHeaderSection;
        private Telerik.Reporting.TextBox usuarioDataTextBox;
        private Telerik.Reporting.GroupFooterSection usuarioGroupFooterSection;
        private Telerik.Reporting.PageHeaderSection pageHeader;
        private Telerik.Reporting.TextBox reportNameTextBox;
        private Telerik.Reporting.PageFooterSection pageFooter;
        private Telerik.Reporting.TextBox currentTimeTextBox;
        private Telerik.Reporting.TextBox pageInfoTextBox;
        private Telerik.Reporting.ReportHeaderSection reportHeader;
        private Telerik.Reporting.TextBox titleTextBox;
        private Telerik.Reporting.ReportFooterSection reportFooter;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.TextBox nombreDataTextBox;
        private Telerik.Reporting.TextBox descripcionDataTextBox;
        private Telerik.Reporting.TextBox edificioDataTextBox;
        private Telerik.Reporting.TextBox aulaDataTextBox;
        private Telerik.Reporting.TextBox estatusDataTextBox;
        private Telerik.Reporting.TextBox fechaDataTextBox;
        private Telerik.Reporting.TextBox horaInicioDataTextBox;
        private Telerik.Reporting.TextBox horaFinDataTextBox;
        private Telerik.Reporting.TextBox equipoDataTextBox;
        private Telerik.Reporting.TextBox cantidadDataTextBox;

    }
}