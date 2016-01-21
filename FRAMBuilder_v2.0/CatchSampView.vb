Public Class form_CatchSampleViewer

    Private Sub btn_RetMainMenu_Click(sender As System.Object, e As System.EventArgs) Handles btn_RetMainMenu.Click
        Me.Visible = False
        form_WelcomeAndConnect.Visible = True
    End Sub

    Private Sub btn_ReturnToOutput_Click(sender As System.Object, e As System.EventArgs) Handles btn_ReturnToOutput.Click
        Me.Visible = False
        form_OutputOptions.Visible = True
    End Sub

    Private Sub btn_ExportData_Click(sender As System.Object, e As System.EventArgs) Handles btn_ExportData.Click

        Dim FileChoose As New FolderBrowserDialog
        FileChoose.Description = "Choose Output Directory"
        FileChoose.SelectedPath = "C:\"
        FileChoose.RootFolder = Environment.SpecialFolder.Desktop
        If FileChoose.ShowDialog() = Windows.Forms.DialogResult.OK Then
            fpath = FileChoose.SelectedPath
        End If
        'Code to export to a spreadsheet a catch sample table...
        Module1.ExportToExcel("Out", fpath, dtPivCat)

    End Sub

    Private Sub form_CatchSampleViewer_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        'Define the new grid's layout for pretty viewing
        Dim objAlternatingCellStyle As New DataGridViewCellStyle()
        objAlternatingCellStyle.BackColor = Color.WhiteSmoke
        grd_CatView.AlternatingRowsDefaultCellStyle = objAlternatingCellStyle
        grd_CatView.AutoResizeColumns()
        grd_CatView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells

        grd_CatView.AutoGenerateColumns = True
        grd_CatView.DataSource = dtPivCat

    End Sub
End Class