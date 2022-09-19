Imports System.IO
Imports System.Net
Public Class Approval
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            Dim filePaths() As String = Directory.GetFiles("\\192.168.1.26\Forms\Approval")
            Dim files As List(Of ListItem) = New List(Of ListItem)
            For Each filePath As String In filePaths
                files.Add(New ListItem(Path.GetFileName(filePath), filePath))
            Next
            datatablesSimple.DataSource = files
            datatablesSimple.DataBind()

            datatablesSimple.UseAccessibleHeader = True
            datatablesSimple.HeaderRow.TableSection = TableRowSection.TableHeader
        End If
    End Sub
    Protected Sub DownloadFile(ByVal sender As Object, ByVal e As EventArgs)
        Dim filePath As String = CType(sender, LinkButton).CommandArgument
        Response.ContentType = ContentType
        Response.AppendHeader("Content-Disposition", ("attachment; filename=" + Path.GetFileName(filePath)))
        Response.WriteFile(filePath)
        Response.End()
    End Sub
    Protected Sub ViewFile(ByVal sender As Object, ByVal e As EventArgs)
        Dim filePath As String = CType(sender, LinkButton).CommandArgument

        Dim client As New WebClient()
        Dim buffer As [Byte]() = client.DownloadData(filePath)
        Response.ContentType = "application/pdf"
        Response.AddHeader("content-length", buffer.Length.ToString())
        Response.BinaryWrite(buffer)
        Response.End()

    End Sub
End Class