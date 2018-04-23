using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
// ...

namespace WebApplication1 {
    public partial class WebForm1 : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            InitializeReportDesigner();
        }

        private void InitializeReportDesigner() {
            string reportParameterName = "reportParameter1";
            // The data source parameter name should match the name of the variable passed to the data source constructor.
            string dataSourceParameterName = "p1";
            var objectDS = GenerateObjectDataSource(reportParameterName, dataSourceParameterName);

            // Uncomment the following line to register the object data source
            // (if end-users are allowed to create new reports bound to this data source).
            // ASPxReportDesigner1.DataSources.Add("ObjectDataSource1", objectDS);
            ASPxReportDesigner1.OpenReport(GenerateReport("reportParameter1", objectDS));
        }

        private XtraReport GenerateReport(string parameterName, object dataSource) {
            XtraReport report = new XtraReport();
            report.DataSource = dataSource;
            report.Parameters.Add(new DevExpress.XtraReports.Parameters.Parameter() {
                Name = parameterName,
                Type = typeof(int),
                Value = 5 // Specify the default value for the report parameter.
            });
            report.RequestParameters = false;
            CreateReportControls(parameterName, report);
            return report;
        }

        private static void CreateReportControls(string parameterName, XtraReport report) {
            var pageHeader = new PageHeaderBand();
            var paramValueLbl = new XRLabel() {
                Name = "label1",
                Borders = DevExpress.XtraPrinting.BorderSide.Bottom,
                Font = new System.Drawing.Font("Calibri", 18f),
                SizeF = new System.Drawing.SizeF(200, 50),
                LocationF = new System.Drawing.PointF(5, 5)
            };
            paramValueLbl.DataBindings.Add(new XRBinding(report.Parameters[parameterName], "Text", "Parameter value:{0}"));
            pageHeader.Controls.Add(paramValueLbl);
            report.Bands.Add(pageHeader);

            var detail1 = new DetailBand();
            var xrLabel1 = new XRLabel() {
                Name = "label1",
                Font = new System.Drawing.Font("Calibri", 18f),
                SizeF = new System.Drawing.SizeF(200, 50),
                LocationF = new System.Drawing.PointF(0, 0)
            };
            xrLabel1.DataBindings.Add(new XRBinding("Text", null, "Name", "Name: {0}"));
            detail1.Controls.Add(xrLabel1);
            report.Bands.Add(detail1);
        }
        private object GenerateObjectDataSource(string reportParamName, string dataSourceParamName) {
            DevExpress.DataAccess.ObjectBinding.ObjectDataSource objds = new DevExpress.DataAccess.ObjectBinding.ObjectDataSource();
            objds.Name = "ObjectDataSource1";
            objds.DataMember = "Items";
            objds.DataSource = typeof(DemoModel);

            // Uncomment the following line to explicitly specify the object data source parameter value.
            // var p = new DevExpress.DataAccess.ObjectBinding.Parameter("p1", typeof(int), 3);

            // The following code maps a data source parameter to the report parameter.
            var p = new DevExpress.DataAccess.ObjectBinding.Parameter(
                dataSourceParamName,
                typeof(DevExpress.DataAccess.Expression),
                new DevExpress.DataAccess.Expression("[Parameters." + reportParamName + "]", typeof(int)));

            objds.Constructor = new DevExpress.DataAccess.ObjectBinding.ObjectConstructorInfo(p);
            return objds;
        }
    }

    #region Demo Data
    public class DemoModel {
        public ItemList Items { get; set; }
        public DemoModel(int p1) {
            Items = new ItemList(p1);
        }
    }
    public class ItemList : List<Item> {
        public ItemList()
            : this(10) {

        }
        public ItemList(int itemNumber) {
            for (int i = 0; i < itemNumber; i++) {
                Add(new Item() { Name = i.ToString() });
            }
        }
    }
    public class Item {
        public string Name { get; set; }
    }
    #endregion
}