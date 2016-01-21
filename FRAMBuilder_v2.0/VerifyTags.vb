Public Class VerifyTags

    'Display the recoveries for the selected subset, for confirmation and verification
    Public Sub VerifyTags_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        lbl_Verifydb.Text = CWTdatabasename 'display database name
        lbl_nCodes.Text = numCodes & " CWT codes selected" 'display number of codes selected

        'Now populate the grid for data viewing
        'Pretty it up for viewing and crosseyed selection
        Dim objAlternatingCellStyle As New DataGridViewCellStyle()
        objAlternatingCellStyle.BackColor = Color.WhiteSmoke
        'grdViewRecoveries.AlternatingRowsDefaultCellStyle = objAlternatingCellStyle
        grdViewRecoveries.AutoGenerateColumns = True
        grdViewRecoveries.DataSource = dsRecoveries
        grdViewRecoveries.DataMember = "CWDBRecovery"
        lbl_nRecs.Text = "Number of Recoveries = " & grdViewRecoveries.RowCount - 1 'Display the number of recoveries for the code(s) in question

        'These autosize functions make this an unbearably slow process...
        'grdViewRecoveries.AutoResizeColumns()
        'grdViewRecoveries.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells

    End Sub


    Public Sub btnConfirm_Click(sender As System.Object, e As System.EventArgs) Handles btn_Confirm.Click

        '-------------------------------------------------------------
         'Just a token 'yes' after an opp'ty to look at raw recs
        '-------------------------------------------------------------

        Me.Close() 'Not sure if I need to dump something here...
        form_ViewCodes.Close() 'Close the tag viewer screen
        dsWireTagCodemerge = dsWireTagCodedisplay.Copy()
        dsWireTagCodedisplay.Clear()
        form_ViewCodes.grd_CodeView.Columns.Remove("chk")
        form_WelcomeAndConnect.Visible = True
        form_WelcomeAndConnect.BringToFront()

    End Sub


    Private Sub btnReselect_Click(sender As System.Object, e As System.EventArgs) Handles btn_Reselect.Click
        '-------------------------------------------------------------
         'Back to square 1 if you for some reason want to start over
        '-------------------------------------------------------------

        'dsWireTagCodedisplay.Clear() 'Dump the CWT releases dataset
        'form_ViewCodes.grd_CodeView.Columns.Remove("chk")
        'form_ViewCodes.grd_CodeView.DataSource = Nothing

        ''Just to make sure it's never stuffed with goodies that don't belong there.
        ''i.e., you need to reload if you come back here after selecting/verifying
        'If dsRecoveries.Tables.Count > 0 Then
        '    If dsRecoveries.Tables(0).Rows.Count > 0 Then
        '        dsRecoveries.Tables.Clear()
        '    End If
        'End If

        'form_ViewCodes.Show()
        form_ViewCodes.grd_CodeView.Columns.Remove("chk")
        form_ViewCodes.ViewCodes_Load(WindowsApplication2.form_ViewCodes, EventArgs.Empty)
        form_ViewCodes.Visible = True
        form_ViewCodes.BringToFront()
        Me.Close() 'Not sure if I need to dump something here...

    End Sub


End Class