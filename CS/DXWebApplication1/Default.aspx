<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="DXWebApplication1.Default" %>

<%@ Register Assembly="DevExpress.XtraReports.v23.1.Web.WebForms, Version=23.1.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
          <dx:ASPxReportDesigner ID="ASPxReportDesigner1" runat="server"></dx:ASPxReportDesigner>
        </div>
    </form>
</body>
</html>
