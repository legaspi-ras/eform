Imports System.IO
Imports MySql.Data.MySqlClient
Public Class AddNewForm
    Inherits System.Web.UI.Page
    Dim connection As MySqlConnection
    Dim command As MySqlCommand
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    Public Sub ConnectionString()
        connection = New MySqlConnection
        connection.ConnectionString = ("server='127.0.0.1'; port='3306'; username='root'; password='POWERHOUSE'; database='eforms'")
    End Sub
    Protected Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click

        Dim deptarea As String
        Dim formctrlnum As String
        Dim frmtitle As String
        Dim revnum As String

        Dim filename As String
        filename = Path.GetFileName(FileUpload1.FileName)

        Dim directory As String
        Dim contentType As String
        contentType = FileUpload1.PostedFile.ContentType
        directory = "\\192.168.1.26\Forms\Template\" + filename

        ' mag lagay dito ng select / query check yung form ctrl no ay existing na sa database hold ng variable kung meron
        If FileUpload1.HasFile Then

            If contentType = "application/pdf" Then

                If File.Exists(directory) Then
                    '' Response.Write("<script language=javascript>alert('Oops! " & filename & " is already existing. Try to rename the file.');</script>")
                    lblAlert2.Visible = True
                Else

                    deptarea = ddlDepartment.SelectedItem.Text
                    formctrlnum = txtFormctrlnum.Text.Trim().ToUpper()
                    frmtitle = txtFormtitle.Text
                    revnum = txtRevnum.Text.Trim()

                    Dim query As String

                    Me.ConnectionString()

                    query = ("INSERT INTO tblforms (formDepartment, formControlnum, formTitle, formRevisionnum, formFilename) VALUES ('" & deptarea & "', '" & formctrlnum & "', '" & frmtitle & "', '" & revnum & "', '" & filename & "')")

                    command = New MySqlCommand(query, connection)

                    Dim reader As MySqlDataReader
                    connection.Open()
                    reader = command.ExecuteReader()
                    reader.Read()

                    FileUpload1.SaveAs("\\192.168.1.26\Forms\Template\" + filename)

                    reader.Close()
                    connection.Close()

                    lblAlert1.Visible = False
                    lblsuccess.Visible = True
                    lblsuccess.Text = " Update Successful." + " | " + formctrlnum + " | " + frmtitle + " "
                    'MsgBox(filename + " succesfully uploaded from the system.")
                    ' Response.Write("<script language=javascript>alert('Upload successful.');</script>")
                    ' Response.Redirect(Request.Url.AbsoluteUri)
                End If

            Else
                ''  Response.Write("<script language=javascript>alert('Please select the PDF file for upload.');</script>")
                lblAlert1.Visible = True
            End If
        Else
            ' Response.Write("<script language=javascript>alert('Please select the PDF file for upload.');</script>")
        End If
    End Sub
End Class