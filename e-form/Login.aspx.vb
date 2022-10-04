Imports MySql.Data.MySqlClient

Public Class Login
    Inherits System.Web.UI.Page
    Dim connection As MySqlConnection
    Dim command As MySqlCommand
    Public Sub ConnectionString()
        connection = New MySqlConnection
        connection.ConnectionString = ("server='127.0.0.1'; port='3306'; username='root'; password='POWERHOUSE'; database='eforms'")
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Dim query As String

        Me.ConnectionString()

        Dim username As String
        Dim password As String

        username = inputUsername.Value
        password = inputPassword.Value

        query = ("SELECT * FROM tbluser_account WHERE BINARY acc_username = '" & username & "' AND BINARY acc_password = '" & password & "'")

        command = New MySqlCommand(query, connection)
        connection.Open()

        Dim reader As MySqlDataReader
        reader = command.ExecuteReader()
        reader.Read()

        If reader.HasRows Then
            reader.Close()
            connection.Close()

            HttpContext.Current.Session("logStatus") = "Login"
            Response.Redirect("AdminDashboard.aspx")
        Else
            reader.Close()
            connection.Close()
            lblAlert1.Visible = True
        End If
    End Sub
End Class