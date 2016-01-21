Imports System.Data.OleDb
Imports System.Data
Imports System.IO
Imports System.Windows
Imports System.Text
Imports System.IO.File


Public Class Form1

    Private Sub ConnectDB_Click(sender As System.Object, e As System.EventArgs) Handles ConnectDB.Click
        Dim OpenDatabase As New OpenFileDialog()

        CWTdatabasename = ""
        OpenDatabase.Filter = "DataBase Files (*.mdb)|*.mdb|All files (*.*)|*.*"
        OpenDatabase.FilterIndex = 1
        OpenDatabase.RestoreDirectory = True
        If OpenDatabase.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Try
                CWTdatabasename = OpenDatabase.FileName
                CWTshortname = My.Computer.FileSystem.GetFileInfo(CWTdatabasename).Name
                CWTdatabasepath = My.Computer.FileSystem.GetFileInfo(CWTdatabasename).DirectoryName
            Catch Ex As Exception
                MessageBox.Show("Cannot read file from disk. Original error: " & Ex.Message)
            End Try
        End If

        If CWTdatabasename.Length > 50 Then
            lbl_CWTdb.Text = CWTshortname
        Else
            lbl_CWTdb.Text = CWTdatabasename
        End If
        If CWTdatabasename = "" Then Exit Sub

    End Sub

    Private Sub btn_viewCodes_Click(sender As System.Object, e As System.EventArgs) Handles btn_viewCodes.Click
        ViewCodes.ShowDialog()
    End Sub
End Class
