<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class WeightSpecs
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.grd_weights = New System.Windows.Forms.DataGridView()
        Me.btn_ConfirmWts = New System.Windows.Forms.Button()
        Me.btn_CancelWts = New System.Windows.Forms.Button()
        Me.lbl_methods = New System.Windows.Forms.Label()
        CType(Me.grd_weights, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grd_weights
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grd_weights.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.grd_weights.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grd_weights.DefaultCellStyle = DataGridViewCellStyle2
        Me.grd_weights.Location = New System.Drawing.Point(24, 13)
        Me.grd_weights.Name = "grd_weights"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grd_weights.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.grd_weights.Size = New System.Drawing.Size(451, 261)
        Me.grd_weights.TabIndex = 0
        '
        'btn_ConfirmWts
        '
        Me.btn_ConfirmWts.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btn_ConfirmWts.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_ConfirmWts.Location = New System.Drawing.Point(28, 292)
        Me.btn_ConfirmWts.Name = "btn_ConfirmWts"
        Me.btn_ConfirmWts.Size = New System.Drawing.Size(140, 58)
        Me.btn_ConfirmWts.TabIndex = 6
        Me.btn_ConfirmWts.Text = "Wts Confirmed." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Process Tags"
        Me.btn_ConfirmWts.UseVisualStyleBackColor = False
        '
        'btn_CancelWts
        '
        Me.btn_CancelWts.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btn_CancelWts.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_CancelWts.Location = New System.Drawing.Point(190, 292)
        Me.btn_CancelWts.Name = "btn_CancelWts"
        Me.btn_CancelWts.Size = New System.Drawing.Size(140, 58)
        Me.btn_CancelWts.TabIndex = 7
        Me.btn_CancelWts.Text = "Cancel"
        Me.btn_CancelWts.UseVisualStyleBackColor = False
        '
        'lbl_methods
        '
        Me.lbl_methods.AutoSize = True
        Me.lbl_methods.Location = New System.Drawing.Point(349, 292)
        Me.lbl_methods.Name = "lbl_methods"
        Me.lbl_methods.Size = New System.Drawing.Size(115, 52)
        Me.lbl_methods.TabIndex = 8
        Me.lbl_methods.Text = "BY weighting methods:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "1 = User Specified" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "2 = Total BY Recs" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "3 = No Weighting"
        '
        'WeightSpecs
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(509, 362)
        Me.Controls.Add(Me.lbl_methods)
        Me.Controls.Add(Me.btn_CancelWts)
        Me.Controls.Add(Me.btn_ConfirmWts)
        Me.Controls.Add(Me.grd_weights)
        Me.Name = "WeightSpecs"
        Me.Text = "Specify BY Weighting"
        CType(Me.grd_weights, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

End Sub
    Friend WithEvents grd_weights As System.Windows.Forms.DataGridView
    Friend WithEvents btn_ConfirmWts As System.Windows.Forms.Button
    Friend WithEvents btn_CancelWts As System.Windows.Forms.Button
    Friend WithEvents lbl_methods As System.Windows.Forms.Label
End Class
