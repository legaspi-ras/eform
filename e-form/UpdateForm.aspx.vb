Imports System.IO
Imports System.Net
Imports MySql.Data.MySqlClient
Public Class UpdateForm
    Inherits System.Web.UI.Page

    Dim connection As MySqlConnection
    Dim command As MySqlCommand
    Public Sub ConnectionString()
        connection = New MySqlConnection
        connection.ConnectionString = ("server='localhost'; port='3306'; username='root'; password='powerhouse'; database='eforms'")
    End Sub
    Private Sub DisplayFiles()
        Dim query As String

        Me.ConnectionString()
        query = ("SELECT formControlnum, formTitle FROM tblforms")
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
    Protected Sub lnkView_Click(sender As Object, e As EventArgs)

        Dim formctrlnum As String = CType(sender, LinkButton).CommandArgument
        Dim filename As String

        Me.ConnectionString()

        Dim query As String
        query = ("SELECT * FROM tblforms where formControlnum = '" & formctrlnum & "'")
        command = New MySqlCommand(query, connection)

        Dim reader As MySqlDataReader
        connection.Open()
        reader = command.ExecuteReader()
        reader.Read()

        filename = reader(5)


        Dim path As String = "\\192.168.1.26\Forms\Template\" + filename
        Dim client As New WebClient()
        Dim buffer As [Byte]() = client.DownloadData(path)

        If buffer IsNot Nothing Then
            Response.ContentType = "application/pdf"
            Response.AddHeader("content-length", buffer.Length.ToString())
            Response.BinaryWrite(buffer)
            Response.End()

        End If

        reader.Close()
        connection.Close()

    End Sub

    Private Sub datatablesSimple_SelectedIndexChanged(sender As Object, e As EventArgs) Handles datatablesSimple.SelectedIndexChanged

        ddlDepartment.Enabled = True
        txtFormctrlnum.Enabled = True
        txtFormtitle.Enabled = True
        txtRevnum.Enabled = True
        FileUpload1.Enabled = True
        btnUpload.Enabled = True

        Dim formctrlnum As String

        formctrlnum = datatablesSimple.SelectedRow.Cells(0).Text

        Dim query As String

        Me.ConnectionString()
        query = ("SELECT * FROM tblforms WHERE formControlnum = '" & formctrlnum & "'")
        command = New MySqlCommand(query, connection)
        connection.Open()

        Dim reader As MySqlDataReader
        reader = command.ExecuteReader()
        reader.Read()

        If reader.HasRows Then

            ddlDepartment.SelectedValue = reader(1)
            txtFormctrlnum.Text = reader(2)
            txtFormtitle.Text = reader(3)
            txtRevnum.Text = reader(4)

            reader.Close()
            connection.Close()

        Else
            'can insert messagebox for empty
        End If

        reader.Close()
        connection.Close()

    End Sub

    Protected Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click

        Dim filename As String
        filename = Path.GetFileName(FileUpload1.FileName)

        Dim directory As String
        Dim contentType As String
        contentType = FileUpload1.PostedFile.ContentType
        directory = Server.MapPath("\\192.168.1.26\Forms\Template" + filename)

        If FileUpload1.HasFile Then

            If contentType = "application/pdf" Then

                If File.Exists(directory) Then

                    File.Delete(directory)

                    Dim deptarea As String
                    Dim formctrlnum As String
                    Dim frmtitle As String
                    Dim revnum As String


                    deptarea = ddlDepartment.SelectedItem.Text
                    formctrlnum = txtFormctrlnum.Text
                    frmtitle = txtFormtitle.Text
                    revnum = txtRevnum.Text

                    Dim query As String

                    Me.ConnectionString()

                    query = ("UPDATE tblforms SET formDepartment = '" & deptarea & "', formControlnum = '" & formctrlnum & "', formTitle = '" & frmtitle & "', formRevisionnum = '" & revnum & "', formFilename = '" & filename & "'  WHERE  formControlNum = '" & formctrlnum & "'")
                    command = New MySqlCommand(query, connection)

                    Dim reader As MySqlDataReader
                    connection.Open()
                    reader = command.ExecuteReader()
                    reader.Read()

                    FileUpload1.SaveAs("\\192.168.1.26\Forms\Template" + filename)

                    reader.Close()
                    connection.Close()

                    ' MsgBox("File succesfully updated from the system.")
                    lblsuccess.Visible = True
                Else

                    ' MsgBox("Please select a PDF file for upload! ")

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


            deptarea = ddlDepartment.SelectedItem.Text
            formctrlnum = txtFormctrlnum.Text
            frmtitle = txtFormtitle.Text
            revnum = txtRevnum.Text

            Dim query As String

            Me.ConnectionString()

            query = ("UPDATE tblforms SET formDepartment = '" & deptarea & "', formControlnum = '" & formctrlnum & "', formTitle = '" & frmtitle & "', formRevisionnum = '" & revnum & "' WHERE  formControlNum = '" & formctrlnum & "'")
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