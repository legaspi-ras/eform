Imports System.IO
Imports System.Net
Imports MySql.Data.MySqlClient
Public Class DeleteForm
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

    Private Sub datatablesSimple_SelectedIndexChanged(sender As Object, e As EventArgs) Handles datatablesSimple.SelectedIndexChanged

        txtFormctrlnum.Enabled = True
        txtFormtitle.Enabled = True
        txtRevnum.Enabled = True
        txtAdminPassword.Enabled = True
        btnDelete.Enabled = True

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

            lblFormId.Text = reader(0)
            txtFormctrlnum.Text = reader(2)
            txtFormtitle.Text = reader(3)
            txtRevnum.Text = reader(4)
            lblFilename.Text = reader(5)

            reader.Close()
            connection.Close()

        Else
            'can insert messagebox for empty
        End If

        reader.Close()
        connection.Close()

    End Sub

    Protected Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click

        Dim formctrlnum As String
        Dim formId As String
        Dim filename As String
        Dim adminPassword As String
        Dim passwordStatus As String

        filename = lblFilename.Text
        formId = lblFormId.Text
        formctrlnum = txtFormctrlnum.Text
        adminPassword = txtAdminPassword.Text

        Dim query As String

        Me.ConnectionString()
        query = ("SELECT * FROM tbluser_account WHERE acc_password = '" & adminPassword & "'")
        command = New MySqlCommand(query, connection)
        connection.Open()

        Dim reader As MySqlDataReader
        reader = command.ExecuteReader()
        reader.Read()

        If reader.HasRows Then

            passwordStatus = "Valid"

            reader.Close()
            connection.Close()

        Else
            passwordStatus = "Invalid"

            reader.Close()
            connection.Close()

            lblAlert1.Visible = True
            'can insert messagebox for empty
        End If

        If passwordStatus = "Valid" Then

            Dim directory As String

            directory = "\\192.168.1.26\Forms\Template\" + filename

            If File.Exists(directory) Then

                File.Delete(directory)

                Me.ConnectionString()
                query = ("DELETE FROM tblforms WHERE id = '" & formId & "' AND formControlnum = '" & formctrlnum & "'")
                command = New MySqlCommand(query, connection)
                connection.Open()

                reader = command.ExecuteReader()
                reader.Read()

                reader.Close()
                connection.Close()

                txtFormctrlnum.Text = ""
                txtFormtitle.Text = ""
                txtRevnum.Text = ""
                txtAdminPassword.Text = ""

                txtFormctrlnum.Enabled = False
                txtFormtitle.Enabled = False
                txtRevnum.Enabled = False
                txtAdminPassword.Enabled = False
                btnDelete.Enabled = False

                lblsuccess.Visible = True
                lblAlert1.Visible = False

                Me.DisplayFiles()

            End If

        End If
    End Sub
End Class