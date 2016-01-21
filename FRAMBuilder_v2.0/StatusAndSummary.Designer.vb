<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class form_StatusAndSummary
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
        Me.lbl_CWTdb4 = New System.Windows.Forms.Label()
        Me.btn_Return1 = New System.Windows.Forms.Button()
        Me.pb_statussumm = New System.Windows.Forms.ProgressBar()
        Me.lbl_StatusUpd = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'lbl_CWTdb4
        '
        Me.lbl_CWTdb4.AutoSize = True
        Me.lbl_CWTdb4.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_CWTdb4.Location = New System.Drawing.Point(12, 190)
        Me.lbl_CWTdb4.Name = "lbl_CWTdb4"
        Me.lbl_CWTdb4.Size = New System.Drawing.Size(153, 14)
        Me.lbl_CWTdb4.TabIndex = 2
        Me.lbl_CWTdb4.Text = "Database: (None Selected)"
        '
        'btn_Return1
        '
        Me.btn_Return1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btn_Return1.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_Return1.Location = New System.Drawing.Point(322, 124)
        Me.btn_Return1.Name = "btn_Return1"
        Me.btn_Return1.Size = New System.Drawing.Size(202, 58)
        Me.btn_Return1.TabIndex = 6
        Me.btn_Return1.Text = "Return To Main Menu"
        Me.btn_Return1.UseVisualStyleBackColor = False
        '
        'pb_statussumm
        '
        Me.pb_statussumm.Location = New System.Drawing.Point(12, 67)
        Me.pb_statussumm.MarqueeAnimationSpeed = 1
        Me.pb_statussumm.Maximum = 1000000
        Me.pb_statussumm.Name = "pb_statussumm"
        Me.pb_statussumm.Size = New System.Drawing.Size(512, 42)
        Me.pb_statussumm.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.pb_statussumm.TabIndex = 8
        '
        'lbl_StatusUpd
        '
        Me.lbl_StatusUpd.AutoSize = True
        Me.lbl_StatusUpd.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_StatusUpd.Location = New System.Drawing.Point(12, 37)
        Me.lbl_StatusUpd.Name = "lbl_StatusUpd"
        Me.lbl_StatusUpd.Size = New System.Drawing.Size(60, 20)
        Me.lbl_StatusUpd.TabIndex = 9
        Me.lbl_StatusUpd.Text = "Status:"
        '
        'form_StatusAndSummary
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(536, 227)
        Me.Controls.Add(Me.lbl_StatusUpd)
        Me.Controls.Add(Me.pb_statussumm)
        Me.Controls.Add(Me.btn_Return1)
        Me.Controls.Add(Me.lbl_CWTdb4)
        Me.Name = "form_StatusAndSummary"
        Me.Text = "StatusAndSummary"
        Me.ResumeLayout(False)
        Me.PerformLayout()

End Sub
    Friend WithEvents lbl_CWTdb4 As System.Windows.Forms.Label
    Friend WithEvents btn_Return1 As System.Windows.Forms.Button
    Public WithEvents lbl_StatusUpd As System.Windows.Forms.Label
    Public WithEvents pb_statussumm As System.Windows.Forms.ProgressBar
End Class
