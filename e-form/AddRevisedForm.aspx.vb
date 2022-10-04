Imports System.IO
Imports System.Net
Imports MySql.Data.MySqlClient

Public Class AddRevisedForm
    Inherits System.Web.UI.Page
    Dim connection As MySqlConnection
    Dim command As MySqlCommand
    Public Sub ConnectionString()
        connection = New MySqlConnection
        connection.ConnectionString = ("server='127.0.0.1'; port='3306'; username='root'; password='POWERHOUSE'; database='eforms'")
    End Sub
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
    Protected Sub DownloadFile(ByVal sender As Object, ByVal e As EventArgs)
        Dim filePath As String = CType(sender, LinkButton).CommandArgument
        Response.ContentType = ContentType
        Response.AppendHeader("Content-Disposition", ("attachment; filename=" + Path.GetFileName(filePath)))
        Response.WriteFile(filePath)
        Response.End()
    End Sub

    Private Sub datatablesSimple_SelectedIndexChanged(sender As Object, e As EventArgs) Handles datatablesSimple.SelectedIndexChanged
        txtFilename.Text = datatablesSimple.SelectedRow.Cells(0).Text
        btnSubmit.Enabled = True

        txtFormctrlnum.Enabled = True
        txtFormtitle.Enabled = True
        txtRevnum.Enabled = True
        FileUpload1.Enabled = True
        txtOriginator.Enabled = True
        txtRevdate.Enabled = True
    End Sub

    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Dim filePath As String
        filePath = "\\192.168.1.26\Forms\Approval\" + txtFilename.Text
        Dim fi As New FileInfo(filePath)
        Dim destinationFile As String
        destinationFile = "\\192.168.1.26\Forms\Final\" + ddlDepartment.SelectedItem.Value + "\" + txtFilename.Text

        Dim filename As String
        Dim ddlarea As String

        ddlarea = ddlDepartment.SelectedValue
        filename = Path.GetFileName(FileUpload1.FileName)

        Dim directory As String
        Dim contentType As String
        contentType = FileUpload1.PostedFile.ContentType
        directory = Server.MapPath("\\192.168.1.26\Forms\Final\" + ddlarea + "\" + filename)

        If FileUpload1.HasFile Then

            If contentType = "application/pdf" Then

                If File.Exists(directory) Then

                    MsgBox("file already exist.")
                Else

                    Dim deptarea As String
                    Dim formctrlnum As String
                    Dim frmtitle As String
                    Dim revnum As String
                    Dim revdate As String
                    Dim frmoriginator As String

                    deptarea = ddlDepartment.SelectedItem.Text
                    formctrlnum = txtFormctrlnum.Text.Trim.ToUpper
                    frmtitle = txtFormtitle.Text
                    revnum = txtRevnum.Text.Trim
                    revdate = txtRevdate.Text
                    frmoriginator = txtOriginator.Text

                    Dim query As String

                    Me.ConnectionString()

                    query = ("INSERT INTO tblfilled_forms (formDepartment, formControlnum, formTitle, formRevisionnum, formRevisiondate, formOriginator, formFilename) VALUES ('" & deptarea & "','" & formctrlnum & "','" & frmtitle & "','" & revnum & "','" & revdate & "','" & frmoriginator & "','" & filename & "')")
                    Command = New MySqlCommand(query, Connection)

                    Dim reader As MySqlDataReader
                    Connection.Open()
                    reader = Command.ExecuteReader()
                    reader.Read()

                    fi.MoveTo(destinationFile)
                    Me.DisplayFiles()
                    lblsuccess.Visible = True

                    reader.Close()
                    Connection.Close()

                    ' MsgBox("File succesfully updated from the system.")
                    lblsuccess.Visible = True

                End If

            Else

                ' MsgBox("Please select a PDF file for upload! ")
                lblAlert1.Visible = True
            End If

        Else
            MsgBox("please select a file")
        End If

    End Sub


End Class