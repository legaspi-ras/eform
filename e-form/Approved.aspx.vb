Imports System.IO
Imports System.Net

Public Class Approved
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("load") = "1"
        If Not Me.IsPostBack Then
            If Session("load") = 1 Then
                Me.DisplayFile()
                Session("load") = 2
            End If
        End If
    End Sub

    Private Sub DisplayFile()
        Dim filePaths() As String = Directory.GetFiles("\\192.168.1.26\Forms\Final\mis")
        Dim files As List(Of ListItem) = New List(Of ListItem)
        For Each filePath As String In filePaths
            files.Add(New ListItem(Path.GetFileName(filePath), filePath))
        Next
        datatablesSimple.DataSource = files
        datatablesSimple.DataBind()

        datatablesSimple.UseAccessibleHeader = True
        datatablesSimple.HeaderRow.TableSection = TableRowSection.TableHeader
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

    Protected Sub ddlDepartment_SelectedIndexChanged(sender As Object, e As EventArgs)

        Dim filePaths() As String = Directory.GetFiles("\\192.168.1.26\Forms\Final\" + ddlDepartment.SelectedItem.Value)
        Dim files As List(Of ListItem) = New List(Of ListItem)
        For Each filePath As String In filePaths
            files.Add(New ListItem(Path.GetFileName(filePath), filePath))
        Next
        datatablesSimple.DataSource = files
        datatablesSimple.DataBind()

        If datatablesSimple.Rows.Count <> 0 Then
            datatablesSimple.UseAccessibleHeader = True
            datatablesSimple.HeaderRow.TableSection = TableRowSection.TableHeader
        End If

    End Sub
End Class