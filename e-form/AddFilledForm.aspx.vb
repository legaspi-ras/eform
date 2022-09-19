Imports System.IO
Imports MySql.Data.MySqlClient
Public Class AddFilledForm
    Inherits System.Web.UI.Page

    Dim connection As MySqlConnection
    Dim command As MySqlCommand
    Public Sub ConnectionString()
        connection = New MySqlConnection
        connection.ConnectionString = ("server='localhost'; port='3306'; username='root'; password='powerhouse'; database='eforms'")
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
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            Me.DisplayFile()
            txtDepartment.Text = ddlDepartment1.SelectedItem.Value
        End If
    End Sub

    Private Sub ddlDepartment1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDepartment1.SelectedIndexChanged
        Dim filePaths() As String = Directory.GetFiles("\\192.168.1.26\Forms\Final\" + ddlDepartment1.SelectedItem.Value)
        Dim files As List(Of ListItem) = New List(Of ListItem)
        For Each filePath As String In filePaths
            files.Add(New ListItem(Path.GetFileName(filePath), filePath))
        Next
        datatablesSimple.DataSource = files
        datatablesSimple.DataBind()

        txtDepartment.Text = ddlDepartment1.SelectedItem.Value

        If datatablesSimple.Rows.Count <> 0 Then
            datatablesSimple.UseAccessibleHeader = True
            datatablesSimple.HeaderRow.TableSection = TableRowSection.TableHeader
        End If

    End Sub
    Private Sub DownloadFile(ByVal sender As Object, ByVal e As EventArgs)
        Dim filePath As String = CType(sender, LinkButton).CommandArgument
        Response.ContentType = ContentType
        Response.AppendHeader("Content-Disposition", ("attachment; filename=" + Path.GetFileName(filePath)))
        Response.WriteFile(filePath)
        Response.End()
    End Sub

    Protected Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Dim filename As String
        Dim ddlarea As String

        ddlarea = txtDepartment.Text
        filename = Path.GetFileName(FileUpload1.FileName)

        Dim directory As String
        Dim contentType As String
        contentType = FileUpload1.PostedFile.ContentType
        directory = Server.MapPath("\\192.168.1.26\Forms\Final\" + ddlarea + "\" + filename)

        If FileUpload1.HasFile Then

            If contentType = "application/pdf" Then

                If File.Exists(directory) Then

                    File.Delete(directory)

                    Dim deptarea As String
                    Dim formctrlnum As String
                    Dim frmtitle As String
                    Dim revnum As String
                    Dim revdate As String
                    Dim frmoriginator As String

                    deptarea = txtDepartment.Text
                    formctrlnum = txtFormctrlnum.Text.Trim.ToUpper
                    frmtitle = txtFormtitle.Text
                    revnum = txtRevnum.Text.Trim
                    revdate = txtRevdate.Text
                    frmoriginator = txtOriginator.Text

                    Dim query As String

                    Me.ConnectionString()

                    query = ("INSERT INTO tblfilled_forms (formDepartment, formControlnum, formTitle, formRevisionnum, formRevisiondate, formOriginator, formFilename) VALUES ('" & deptarea & "','" & formctrlnum & "','" & frmtitle & "','" & revnum & "','" & revdate & "','" & frmoriginator & "','" & filename & "')")
                    command = New MySqlCommand(query, connection)

                    Dim reader As MySqlDataReader
                    connection.Open()
                    reader = command.ExecuteReader()
                    reader.Read()

                    FileUpload1.SaveAs("\\192.168.1.26\Forms\Final\" + deptarea + "\" + filename)

                    reader.Close()
                    connection.Close()

                    ' MsgBox("File succesfully updated from the system.")
                    lblsuccess.Visible = True
                Else

                    Dim deptarea As String
                    Dim formctrlnum As String
                    Dim frmtitle As String
                    Dim revnum As String
                    Dim revdate As String
                    Dim frmoriginator As String

                    deptarea = txtDepartment.Text
                    formctrlnum = txtFormctrlnum.Text.Trim.ToUpper
                    frmtitle = txtFormtitle.Text
                    revnum = txtRevnum.Text.Trim
                    revdate = txtRevdate.Text
                    frmoriginator = txtOriginator.Text

                    Dim query As String

                    Me.ConnectionString()

                    query = ("INSERT INTO tblfilled_forms (formDepartment, formControlnum, formTitle, formRevisionnum, formRevisiondate, formOriginator, formFilename) VALUES ('" & deptarea & "','" & formctrlnum & "','" & frmtitle & "','" & revnum & "','" & revdate & "','" & frmoriginator & "','" & filename & "')")
                    command = New MySqlCommand(query, connection)

                    Dim reader As MySqlDataReader
                    connection.Open()
                    reader = command.ExecuteReader()
                    reader.Read()

                    FileUpload1.SaveAs("\\192.168.1.26\Forms\Final\" + deptarea + "\" + filename)

                    reader.Close()
                    connection.Close()

                    ' MsgBox("File succesfully updated from the system.")
                    lblsuccess.Visible = True

                End If

            Else

                ' MsgBox("Please select a PDF file for upload! ")
                lblAlert1.Visible = True
            End If

        Else
            Dim deptarea As String
            Dim formctrlnum As String
            Dim frmtitle As String
            Dim revnum As String
            Dim revdate As String
            Dim frmoriginator As String
            Dim filled_filename As String

            deptarea = txtDepartment.Text
            formctrlnum = txtFormctrlnum.Text
            frmtitle = txtFormtitle.Text
            revnum = txtRevnum.Text
            revdate = txtRevdate.Text
            frmoriginator = txtOriginator.Text
            filled_filename = lblFilename.Text

            Dim query As String

            Me.ConnectionString()

            query = ("INSERT INTO tblfilled_forms (formDepartment, formControlnum, formTitle, formRevisionnum, formRevisiondate, formOriginator, formFilename) VALUES ('" & deptarea & "','" & formctrlnum & "','" & frmtitle & "','" & revnum & "','" & revdate & "','" & frmoriginator & "','" & filled_filename & "')")
            command = New MySqlCommand(query, connection)

            Dim reader As MySqlDataReader
            connection.Open()
            reader = command.ExecuteReader()
            reader.Read()

            reader.Close()
            connection.Close()

            ' MsgBox("File succesfully updated from the system.")
            lblsuccess.Visible = True
        End If
    End Sub

    Private Sub datatablesSimple_SelectedIndexChanged(sender As Object, e As EventArgs) Handles datatablesSimple.SelectedIndexChanged

        Dim filename As String
        Dim filePath As String

        filename = datatablesSimple.SelectedRow.Cells(0).Text
        filePath = "\\192.168.1.26\Forms\Final\" + ddlDepartment1.SelectedItem.Value + "\" + filename

        txtDepartment.Text = ddlDepartment1.SelectedItem.Value

        Response.ContentType = ContentType
        Response.AppendHeader("Content-Disposition", ("attachment; filename=" + filename))
        Response.WriteFile(filePath)
        Response.End()
    End Sub
End Class