Imports System.Net
Imports System.IO
Imports MySql.Data.MySqlClient
Public Class RevisedFormClient
    Inherits System.Web.UI.Page
    Dim connection As MySqlConnection
    Dim command As MySqlCommand
    Public Sub ConnectionString()
        connection = New MySqlConnection
        connection.ConnectionString = ("server='127.0.0.1'; port='3306'; username='root'; password='POWERHOUSE'; database='eforms'")
    End Sub
    Private Sub DisplayFile()
        Dim query As String

        Me.ConnectionString()
        query = ("SELECT * FROM tblfilled_forms")
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
            Me.DisplayFile()
        End If
    End Sub
    Protected Sub lnkView_Click(sender As Object, e As EventArgs)

        Dim formctrlnum As String = CType(sender, LinkButton).CommandArgument
        Dim filename As String
        Dim formDept As String

        Me.ConnectionString()

        Dim query As String
        query = ("SELECT * FROM tblfilled_forms where formControlnum = '" & formctrlnum & "'")
        command = New MySqlCommand(query, connection)

        Dim reader As MySqlDataReader
        connection.Open()
        reader = command.ExecuteReader()
        reader.Read()

        filename = reader(7)
        formDept = reader(1)

        Dim path As String = "\\192.168.1.26\Forms\Final\" + formDept + "\" + filename
        If File.Exists(path) Then

            Dim client As New WebClient()
            Dim buffer As [Byte]() = client.DownloadData(path)


            If buffer IsNot Nothing Then
                Response.ContentType = "application/pdf"
                Response.AddHeader("content-length", buffer.Length.ToString())
                Response.BinaryWrite(buffer)
                Response.End()

            End If

        Else
            MsgBox("File doesnt exist inside file server")
        End If

        reader.Close()
        connection.Close()

    End Sub
    Protected Sub lnkDownload_Click(sender As Object, e As EventArgs)

        Dim formctrlnum As String = CType(sender, LinkButton).CommandArgument
        Dim filename As String
        Dim formDept As String

        Me.ConnectionString()

        Dim query As String
        query = ("SELECT * FROM tblfilled_forms where formControlnum = '" & formctrlnum & "'")
        command = New MySqlCommand(query, connection)

        Dim reader As MySqlDataReader
        connection.Open()
        reader = command.ExecuteReader()
        reader.Read()

        If reader.HasRows Then
            filename = reader(6)
            formDept = reader(1)

            Dim path As String = "\\192.168.1.26\Forms\Final\" + formDept + "\" + filename
            Dim client As New WebClient()
            Dim buffer As [Byte]() = client.DownloadData(path)

            ContentType = "application/pdf"
            Response.Clear()
            Response.Buffer = True
            Response.Charset = ""
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Response.ClearContent()
            Response.ClearHeaders()
            Response.ContentType = ContentType
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename)
            Response.TransmitFile("\\192.168.1.26\Forms\Final\" + formDept + "\" + filename)
            Response.BinaryWrite(buffer)
            Response.Flush()
            Response.Close()
            Response.End()
            connection.Close()
        Else
            'can insert messagebox for empty
        End If

        reader.Close()
        connection.Close()

    End Sub
End Class