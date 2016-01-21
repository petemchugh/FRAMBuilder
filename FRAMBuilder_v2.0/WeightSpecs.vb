Public Class WeightSpecs

Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles btn_CancelWts.Click
    wtresult = True
    Me.Visible = False
    form_OutputOptions.BringToFront()
End Sub

Private Sub btn_ConfirmWts_Click(sender As System.Object, e As System.EventArgs) Handles btn_ConfirmWts.Click
    'Copy/store/commit weighting decisions to memory and move on
    'Do some stuff here...
    'Move on
    Me.Visible = False
    form_OutputOptions.BringToFront()
End Sub

Private Sub WeightSpecs_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        'Now populate the grid with weights for codes that are being processed
        'for viewing/editing/confirming
        'Pretty it up for viewing and crosseyed selection
        Dim objAlternatingCellStyle As New DataGridViewCellStyle()
        objAlternatingCellStyle.BackColor = Color.WhiteSmoke
        grd_weights.AlternatingRowsDefaultCellStyle = objAlternatingCellStyle
        grd_weights.AutoGenerateColumns = True
        grd_weights.DataSource = dtWeights
        'grd_weights.DataMember = "CWDBRecovery"
End Sub
End Class