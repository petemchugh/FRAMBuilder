<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class VerifyTags
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
Me.grdViewRecoveries = New System.Windows.Forms.DataGridView()
Me.btn_Confirm = New System.Windows.Forms.Button()
Me.btn_Reselect = New System.Windows.Forms.Button()
Me.lbl_Verifydb = New System.Windows.Forms.Label()
Me.lbl_nRecs = New System.Windows.Forms.Label()
Me.lbl_nCodes = New System.Windows.Forms.Label()
Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
CType(Me.grdViewRecoveries, System.ComponentModel.ISupportInitialize).BeginInit()
Me.SuspendLayout()
'
'grdViewRecoveries
'
Me.grdViewRecoveries.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
Me.grdViewRecoveries.Location = New System.Drawing.Point(13, 13)
Me.grdViewRecoveries.Name = "grdViewRecoveries"
Me.grdViewRecoveries.ReadOnly = True
Me.grdViewRecoveries.Size = New System.Drawing.Size(859, 229)
Me.grdViewRecoveries.TabIndex = 0
'
'btn_Confirm
'
Me.btn_Confirm.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
Me.btn_Confirm.Location = New System.Drawing.Point(15, 295)
Me.btn_Confirm.Name = "btn_Confirm"
Me.btn_Confirm.Size = New System.Drawing.Size(284, 80)
Me.btn_Confirm.TabIndex = 1
Me.btn_Confirm.Text = "Confirm Selection"
Me.btn_Confirm.UseVisualStyleBackColor = True
'
'btn_Reselect
'
Me.btn_Reselect.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
Me.btn_Reselect.Location = New System.Drawing.Point(317, 295)
Me.btn_Reselect.Name = "btn_Reselect"
Me.btn_Reselect.Size = New System.Drawing.Size(284, 80)
Me.btn_Reselect.TabIndex = 2
Me.btn_Reselect.Text = "Reselect Tag Codes"
Me.btn_Reselect.UseVisualStyleBackColor = True
'
'lbl_Verifydb
'
Me.lbl_Verifydb.AutoSize = True
Me.lbl_Verifydb.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
Me.lbl_Verifydb.Location = New System.Drawing.Point(12, 396)
Me.lbl_Verifydb.Name = "lbl_Verifydb"
Me.lbl_Verifydb.Size = New System.Drawing.Size(63, 15)
Me.lbl_Verifydb.TabIndex = 3
Me.lbl_Verifydb.Text = "Database:"
'
'lbl_nRecs
'
Me.lbl_nRecs.AutoSize = True
Me.lbl_nRecs.Location = New System.Drawing.Point(24, 270)
Me.lbl_nRecs.Name = "lbl_nRecs"
Me.lbl_nRecs.Size = New System.Drawing.Size(75, 13)
Me.lbl_nRecs.TabIndex = 4
Me.lbl_nRecs.Text = "N Recoveries:"
'
'lbl_nCodes
'
Me.lbl_nCodes.AutoSize = True
Me.lbl_nCodes.Location = New System.Drawing.Point(24, 249)
Me.lbl_nCodes.Name = "lbl_nCodes"
Me.lbl_nCodes.Size = New System.Drawing.Size(51, 13)
Me.lbl_nCodes.TabIndex = 5
Me.lbl_nCodes.Text = "N Codes:"
'
'VerifyTags
'
Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
Me.ClientSize = New System.Drawing.Size(884, 420)
Me.Controls.Add(Me.lbl_nCodes)
Me.Controls.Add(Me.lbl_nRecs)
Me.Controls.Add(Me.lbl_Verifydb)
Me.Controls.Add(Me.btn_Reselect)
Me.Controls.Add(Me.btn_Confirm)
Me.Controls.Add(Me.grdViewRecoveries)
Me.Name = "VerifyTags"
Me.Text = "VerifyTags"
CType(Me.grdViewRecoveries, System.ComponentModel.ISupportInitialize).EndInit()
Me.ResumeLayout(False)
Me.PerformLayout()

End Sub
    Friend WithEvents grdViewRecoveries As System.Windows.Forms.DataGridView
    Friend WithEvents btn_Confirm As System.Windows.Forms.Button
    Friend WithEvents btn_Reselect As System.Windows.Forms.Button
    Friend WithEvents lbl_Verifydb As System.Windows.Forms.Label
    Friend WithEvents lbl_nRecs As System.Windows.Forms.Label
    Friend WithEvents lbl_nCodes As System.Windows.Forms.Label
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
End Class
