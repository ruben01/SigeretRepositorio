namespace Sigeret.Reportes
{
    partial class Usuarios
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Usuarios));
            Telerik.Reporting.Group group1 = new Telerik.Reporting.Group();
            Telerik.Reporting.Drawing.StyleRule styleRule1 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule2 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule3 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule4 = new Telerik.Reporting.Drawing.StyleRule();
            this.DsUsuarios = new Telerik.Reporting.SqlDataSource();
            this.labelsGroupHeaderSection = new Telerik.Reporting.GroupHeaderSection();
            this.labelsGroupFooterSection = new Telerik.Reporting.GroupFooterSection();
            this.nombreCaptionTextBox = new Telerik.Reporting.TextBox();
            this.apellidoCaptionTextBox = new Telerik.Reporting.TextBox();
            this.cedulaCaptionTextBox = new Telerik.Reporting.TextBox();
            this.userNameCaptionTextBox = new Telerik.Reporting.TextBox();
            this.matriculaCaptionTextBox = new Telerik.Reporting.TextBox();
            this.contactoCaptionTextBox = new Telerik.Reporting.TextBox();
            this.textBox1 = new Telerik.Reporting.TextBox();
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
            this.apellidoDataTextBox = new Telerik.Reporting.TextBox();
            this.cedulaDataTextBox = new Telerik.Reporting.TextBox();
            this.userNameDataTextBox = new Telerik.Reporting.TextBox();
            this.matriculaDataTextBox = new Telerik.Reporting.TextBox();
            this.contactoDataTextBox = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // DsUsuarios
            // 
            this.DsUsuarios.ConnectionString = "SigeretContext";
            this.DsUsuarios.Name = "DsUsuarios";
            this.DsUsuarios.SelectCommand = resources.GetString("DsUsuarios.SelectCommand");
            // 
            // labelsGroupHeaderSection
            // 
            this.labelsGroupHeaderSection.Height = Telerik.Reporting.Drawing.Unit.Cm(1.1058331727981567D);
            this.labelsGroupHeaderSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.nombreCaptionTextBox,
            this.apellidoCaptionTextBox,
            this.cedulaCaptionTextBox,
            this.userNameCaptionTextBox,
            this.matriculaCaptionTextBox,
            this.contactoCaptionTextBox,
            this.textBox1});
            this.labelsGroupHeaderSection.Name = "labelsGroupHeaderSection";
            this.labelsGroupHeaderSection.PrintOnEveryPage = true;
            // 
            // labelsGroupFooterSection
            // 
            this.labelsGroupFooterSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.71437495946884155D);
            this.labelsGroupFooterSection.Name = "labelsGroupFooterSection";
            this.labelsGroupFooterSection.Style.Visible = false;
            // 
            // nombreCaptionTextBox
            // 
            this.nombreCaptionTextBox.CanGrow = true;
            this.nombreCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.nombreCaptionTextBox.Name = "nombreCaptionTextBox";
            this.nombreCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.1986904144287109D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.nombreCaptionTextBox.StyleName = "Caption";
            this.nombreCaptionTextBox.Value = "Nombre";
            // 
            // apellidoCaptionTextBox
            // 
            this.apellidoCaptionTextBox.CanGrow = true;
            this.apellidoCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(2.3045237064361572D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.apellidoCaptionTextBox.Name = "apellidoCaptionTextBox";
            this.apellidoCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.1986904144287109D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.apellidoCaptionTextBox.StyleName = "Caption";
            this.apellidoCaptionTextBox.Value = "Apellido";
            // 
            // cedulaCaptionTextBox
            // 
            this.cedulaCaptionTextBox.CanGrow = true;
            this.cedulaCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(4.5561308860778809D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.cedulaCaptionTextBox.Name = "cedulaCaptionTextBox";
            this.cedulaCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.1986904144287109D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.cedulaCaptionTextBox.StyleName = "Caption";
            this.cedulaCaptionTextBox.Value = "Cedula";
            // 
            // userNameCaptionTextBox
            // 
            this.userNameCaptionTextBox.CanGrow = true;
            this.userNameCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(6.8077383041381836D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.userNameCaptionTextBox.Name = "userNameCaptionTextBox";
            this.userNameCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.1986904144287109D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.userNameCaptionTextBox.StyleName = "Caption";
            this.userNameCaptionTextBox.Value = "User Name";
            // 
            // matriculaCaptionTextBox
            // 
            this.matriculaCaptionTextBox.CanGrow = true;
            this.matriculaCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(9.0593452453613281D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.matriculaCaptionTextBox.Name = "matriculaCaptionTextBox";
            this.matriculaCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.1986904144287109D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.matriculaCaptionTextBox.StyleName = "Caption";
            this.matriculaCaptionTextBox.Value = "Matricula";
            // 
            // contactoCaptionTextBox
            // 
            this.contactoCaptionTextBox.CanGrow = true;
            this.contactoCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(11.310952186584473D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.contactoCaptionTextBox.Name = "contactoCaptionTextBox";
            this.contactoCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.1986904144287109D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.contactoCaptionTextBox.StyleName = "Caption";
            this.contactoCaptionTextBox.Value = "Contacto";
            // 
            // textBox1
            // 
            this.textBox1.CanGrow = true;
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(13.562559127807617D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.1986904144287109D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.textBox1.StyleName = "Caption";
            this.textBox1.Value = "Tipo Contacto";
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
            this.reportNameTextBox.Value = "Usuarios";
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
            this.currentTimeTextBox.Value = "=NOW()";
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
            this.titleTextBox.Value = "Usuarios";
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
            this.apellidoDataTextBox,
            this.cedulaDataTextBox,
            this.userNameDataTextBox,
            this.matriculaDataTextBox,
            this.contactoDataTextBox,
            this.textBox2});
            this.detail.Name = "detail";
            // 
            // nombreDataTextBox
            // 
            this.nombreDataTextBox.CanGrow = true;
            this.nombreDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.nombreDataTextBox.Name = "nombreDataTextBox";
            this.nombreDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.1986904144287109D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.nombreDataTextBox.StyleName = "Data";
            this.nombreDataTextBox.Value = "=Fields.Nombre";
            // 
            // apellidoDataTextBox
            // 
            this.apellidoDataTextBox.CanGrow = true;
            this.apellidoDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(2.3045237064361572D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.apellidoDataTextBox.Name = "apellidoDataTextBox";
            this.apellidoDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.1986904144287109D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.apellidoDataTextBox.StyleName = "Data";
            this.apellidoDataTextBox.Value = "=Fields.Apellido";
            // 
            // cedulaDataTextBox
            // 
            this.cedulaDataTextBox.CanGrow = true;
            this.cedulaDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(4.5561308860778809D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.cedulaDataTextBox.Name = "cedulaDataTextBox";
            this.cedulaDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.1986904144287109D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.cedulaDataTextBox.StyleName = "Data";
            this.cedulaDataTextBox.Value = "=Fields.Cedula";
            // 
            // userNameDataTextBox
            // 
            this.userNameDataTextBox.CanGrow = true;
            this.userNameDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(6.8077383041381836D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.userNameDataTextBox.Name = "userNameDataTextBox";
            this.userNameDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.1986904144287109D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.userNameDataTextBox.StyleName = "Data";
            this.userNameDataTextBox.Value = "=Fields.UserName";
            // 
            // matriculaDataTextBox
            // 
            this.matriculaDataTextBox.CanGrow = true;
            this.matriculaDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(9.0593452453613281D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.matriculaDataTextBox.Name = "matriculaDataTextBox";
            this.matriculaDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.1986904144287109D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.matriculaDataTextBox.StyleName = "Data";
            this.matriculaDataTextBox.Value = "=Fields.Matricula";
            // 
            // contactoDataTextBox
            // 
            this.contactoDataTextBox.CanGrow = true;
            this.contactoDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(11.310952186584473D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.contactoDataTextBox.Name = "contactoDataTextBox";
            this.contactoDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.1986904144287109D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.contactoDataTextBox.StyleName = "Data";
            this.contactoDataTextBox.Value = "=Fields.Contacto";
            // 
            // textBox2
            // 
            this.textBox2.CanGrow = true;
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(13.562559127807617D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.1986904144287109D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.textBox2.StyleName = "Data";
            this.textBox2.Value = "=Fields.[Tipo Contacto]";
            // 
            // Usuarios
            // 
            this.DataSource = this.DsUsuarios;
            group1.GroupFooter = this.labelsGroupFooterSection;
            group1.GroupHeader = this.labelsGroupHeaderSection;
            group1.Name = "labelsGroup";
            this.Groups.AddRange(new Telerik.Reporting.Group[] {
            group1});
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.labelsGroupHeaderSection,
            this.labelsGroupFooterSection,
            this.pageHeader,
            this.pageFooter,
            this.reportHeader,
            this.reportFooter,
            this.detail});
            this.Name = "Usuarios";
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Mm(25.399999618530273D), Telerik.Reporting.Drawing.Unit.Mm(25.399999618530273D), Telerik.Reporting.Drawing.Unit.Mm(25.399999618530273D), Telerik.Reporting.Drawing.Unit.Mm(25.399999618530273D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Style.BackgroundColor = System.Drawing.Color.White;
            styleRule1.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector("Title")});
            styleRule1.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(103)))), ((int)(((byte)(109)))));
            styleRule1.Style.Font.Name = "Book Antiqua";
            styleRule1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(18D);
            styleRule2.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector("Caption")});
            styleRule2.Style.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(103)))), ((int)(((byte)(109)))));
            styleRule2.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(185)))), ((int)(((byte)(102)))));
            styleRule2.Style.Font.Name = "Book Antiqua";
            styleRule2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            styleRule2.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            styleRule3.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector("Data")});
            styleRule3.Style.Font.Name = "Book Antiqua";
            styleRule3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            styleRule3.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            styleRule4.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector("PageInfo")});
            styleRule4.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
            styleRule4.Style.Font.Name = "Book Antiqua";
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

        private Telerik.Reporting.SqlDataSource DsUsuarios;
        private Telerik.Reporting.GroupHeaderSection labelsGroupHeaderSection;
        private Telerik.Reporting.TextBox nombreCaptionTextBox;
        private Telerik.Reporting.TextBox apellidoCaptionTextBox;
        private Telerik.Reporting.TextBox cedulaCaptionTextBox;
        private Telerik.Reporting.TextBox userNameCaptionTextBox;
        private Telerik.Reporting.TextBox matriculaCaptionTextBox;
        private Telerik.Reporting.TextBox contactoCaptionTextBox;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.GroupFooterSection labelsGroupFooterSection;
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
        private Telerik.Reporting.TextBox apellidoDataTextBox;
        private Telerik.Reporting.TextBox cedulaDataTextBox;
        private Telerik.Reporting.TextBox userNameDataTextBox;
        private Telerik.Reporting.TextBox matriculaDataTextBox;
        private Telerik.Reporting.TextBox contactoDataTextBox;
        private Telerik.Reporting.TextBox textBox2;

    }
}