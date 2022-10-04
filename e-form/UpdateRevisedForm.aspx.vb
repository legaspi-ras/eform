Imports System.IO
Imports System.Net
Imports MySql.Data.MySqlClient
Public Class UpdateRevisedForm
    Inherits System.Web.UI.Page

    Dim connection As MySqlConnection
    Dim command As MySqlCommand
    Public Sub ConnectionString()
        connection = New MySqlConnection
        connection.ConnectionString = ("server='127.0.0.1'; port='3306'; username='root'; password='POWERHOUSE'; database='eforms'")
    End Sub
    Private Sub DisplayFiles()
        Dim query As String

        Me.ConnectionString()
        query = ("SELECT formControlnum, formTitle FROM tblfilled_forms")
        command = New MySqlCommand(query, connection)
        connection.Open()

        Dim reader As MySqlDataReader
        reader = command.ExecuteReader()
        reader.Read()

        If reader.HasRows Then
            reader.Close()
            connection.Close()
            Using sda As New MySqlDataAdapter(command)
                Dim dt As New DataTable()
                sda.Fill(dt)
                datatablesSimple.DataSource = dt
                datatablesSimple.DataBind()

                datatablesSimple.UseAccessibleHeader = True
                datatablesSimple.HeaderRow.TableSection = TableRowSection.TableHeader
            End Using
        Else
            'can insert messagebox for empty
        End If

        reader.Close()
        connection.Close()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            Me.DisplayFiles()
        End If
    End Sub

    Private Sub datatablesSimple_SelectedIndexChanged(sender As Object, e As EventArgs) Handles datatablesSimple.SelectedIndexChanged
        ddlDepartment.Enabled = True
        txtFormctrlnum.Enabled = True
        txtFormtitle.Enabled = True
        txtRevnum.Enabled = True
        txtOriginator.Enabled = True
        txtRevdate.Enabled = True
        FileUpload1.Enabled = True
        btnUpdate.Enabled = True

        Dim formctrlnum As String

        formctrlnum = datatablesSimple.SelectedRow.Cells(0).Text

        Dim query As String

        Me.ConnectionString()
        query = ("SELECT * FROM tblfilled_forms WHERE formControlnum = '" & formctrlnum & "'")
        command = New MySqlCommand(query, connection)
        connection.Open()

        Dim reader As MySqlDataReader
        reader = command.ExecuteReader()
        reader.Read()

        If reader.HasRows Then

            txtFilename.Text = reader(7)
            ddlDepartment.SelectedValue = reader(1)
            txtFormctrlnum.Text = reader(2)
            txtFormtitle.Text = reader(3)
            txtRevnum.Text = reader(4)
            txtRevdate.Text = reader(6)
            txtOriginator.Text = reader(5)

            reader.Close()
            connection.Close()

        Else
            'can insert messagebox for empty
        End If

        reader.Close()
        connection.Close()
    End Sub

    Protected Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
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

                    File.Delete(directory)

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

                    query = ("UPDATE tblfilled_forms SET formDepartment = '" & deptarea & "', formControlnum ='" & formctrlnum & "', formTitle ='" & frmtitle & "', formRevisionnum ='" & revnum & "', formRevisiondate ='" & revdate & "', formOriginator ='" & frmoriginator & "', formFilename = '" & filename & "' WHERE formControlnum ='" & formctrlnum & "' ")
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

                    deptarea = ddlDepartment.SelectedItem.Text
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

            deptarea = ddlDepartment.SelectedItem.Text
            formctrlnum = txtFormctrlnum.Text
            frmtitle = txtFormtitle.Text
            revnum = txtRevnum.Text
            revdate = txtRevdate.Text
            frmoriginator = txtOriginator.Text
            filled_filename = txtFilename.Text

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
End Class