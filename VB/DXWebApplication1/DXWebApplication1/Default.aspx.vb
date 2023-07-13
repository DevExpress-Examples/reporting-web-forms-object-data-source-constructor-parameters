Imports DevExpress.XtraReports.UI

Public Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        InitializeReportDesigner()
    End Sub
    Private Sub InitializeReportDesigner()
        Dim reportParameterName As String = "reportParameter1"
        ' The data source parameter name should match the name of the variable passed to the data source constructor.
        Dim dataSourceParameterName As String = "p1"
        Dim objectDS = GenerateObjectDataSource(reportParameterName, dataSourceParameterName)

        ' Uncomment the following line to register the object data source
        ' (if end-users are allowed to create new reports bound to this data source).
        ' ASPxReportDesigner1.DataSources.Add("ObjectDataSource1", objectDS);
        ASPxReportDesigner1.OpenReport(GenerateReport("reportParameter1", objectDS))
    End Sub

    Private Function GenerateReport(ByVal parameterName As String, ByVal dataSource As Object) As XtraReport
        Dim report As New XtraReport()
        report.DataSource = dataSource
        report.Parameters.Add(New DevExpress.XtraReports.Parameters.Parameter() With {
                                  .Name = parameterName,
                                  .Type = GetType(Integer),
                                  .Value = 5}) ' Specify the default value for the report parameter.
        report.RequestParameters = False
        CreateReportControls(parameterName, report)
        Return report
    End Function

    Private Shared Sub CreateReportControls(ByVal parameterName As String, ByVal report As XtraReport)
        Dim pageHeader = New PageHeaderBand()
        Dim paramValueLbl = New XRLabel() With {
                .Name = "label1",
                .Borders = DevExpress.XtraPrinting.BorderSide.Bottom,
                .Font = New System.Drawing.Font("Calibri", 18.0F),
                .SizeF = New System.Drawing.SizeF(200, 50),
                .LocationF = New System.Drawing.PointF(5, 5)}
        paramValueLbl.ExpressionBindings.Add(New ExpressionBinding("Text", "?" + parameterName))
        paramValueLbl.TextFormatString = "Parameter value:{0}"
        pageHeader.Controls.Add(paramValueLbl)
        report.Bands.Add(pageHeader)

        Dim detail1 = New DetailBand()
        Dim xrLabel1 = New XRLabel() With {
                .Name = "label1",
                .Font = New System.Drawing.Font("Calibri", 18.0F),
                .SizeF = New System.Drawing.SizeF(200, 50),
                .LocationF = New System.Drawing.PointF(0, 0)}
        xrLabel1.ExpressionBindings.Add(New ExpressionBinding("Text", "[Name]"))
        xrLabel1.TextFormatString = "Name: {0}"
        detail1.Controls.Add(xrLabel1)
        report.Bands.Add(detail1)
    End Sub
    Private Function GenerateObjectDataSource(ByVal reportParamName As String, ByVal dataSourceParamName As String) As Object
        Dim objds As New DevExpress.DataAccess.ObjectBinding.ObjectDataSource()
        objds.Name = "ObjectDataSource1"
        objds.DataMember = "Items"
        objds.DataSource = GetType(DemoModel)

        ' Uncomment the following line to explicitly specify the object data source parameter value.
        ' var p = new DevExpress.DataAccess.ObjectBinding.Parameter("p1", typeof(int), 3);

        ' The following code maps a data source parameter to the report parameter.
        Dim p = New DevExpress.DataAccess.ObjectBinding.Parameter(
                    dataSourceParamName,
                    GetType(DevExpress.DataAccess.Expression),
                    New DevExpress.DataAccess.Expression("[Parameters." & reportParamName & "]", GetType(Integer)))

        objds.Constructor = New DevExpress.DataAccess.ObjectBinding.ObjectConstructorInfo(p)
        Return objds
    End Function
End Class

#Region "Demo Data"
Public Class DemoModel
    Public Property Items() As ItemList
    Public Sub New(ByVal p1 As Integer)
        Items = New ItemList(p1)
    End Sub
End Class
Public Class ItemList
    Inherits List(Of Item)

    Public Sub New()
        Me.New(10)

    End Sub
    Public Sub New(ByVal itemNumber As Integer)
        For i As Integer = 0 To itemNumber - 1
            Add(New Item() With {.Name = i.ToString()})
        Next i
    End Sub
End Class
Public Class Item
    Public Property Name() As String
End Class
#End Region
