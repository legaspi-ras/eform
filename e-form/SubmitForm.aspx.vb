Imports System.IO
Imports System.Net
Public Class Completed
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            Me.DisplayFiles()
        End If
    End Sub
    Private Sub DisplayFiles()
        Dim filePaths() As String = Directory.GetFiles("\\192.168.1.26\Forms\Approval")
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

    Private Sub datatablesSimple_SelectedIndexChanged(sender As Object, e As EventArgs) Handles datatablesSimple.SelectedIndexChanged
        txtFilename.Text = datatablesSimple.SelectedRow.Cells(0).Text
        btnSubmit.Enabled = True
    End Sub

    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click

        Dim filePath As String
        filePath = "\\192.168.1.26\Forms\Approval\" + txtFilename.Text
        Dim fi As New FileInfo(filePath)
        Dim destinationFile As String
        destinationFile = "\\192.168.1.26\Forms\Final\" + ddlDepartment.SelectedItem.Value + "\" + txtFilename.Text


        If File.Exists(destinationFile) Then
            lblAlert1.Visible = True
        Else
            '' fi.CopyTo(destinationFile)
            fi.MoveTo(destinationFile)
            Me.DisplayFiles()
            lblsuccess.Visible = True
        End If

    End Sub
End Class