<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ViewCodes
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
        Me.lbl_CWTdb2 = New System.Windows.Forms.Label()
        Me.grdCodeView = New System.Windows.Forms.DataGridView()
        Me.btn_LoadCodes = New System.Windows.Forms.Button()
        CType(Me.grdCodeView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lbl_CWTdb2
        '
        Me.lbl_CWTdb2.AutoSize = True
        Me.lbl_CWTdb2.Location = New System.Drawing.Point(12, 583)
        Me.lbl_CWTdb2.Name = "lbl_CWTdb2"
        Me.lbl_CWTdb2.Size = New System.Drawing.Size(39, 13)
        Me.lbl_CWTdb2.TabIndex = 2
        Me.lbl_CWTdb2.Text = "Label1"
        '
        'grdCodeView
        '
        Me.grdCodeView.AllowUserToOrderColumns = True
        Me.grdCodeView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdCodeView.Location = New System.Drawing.Point(12, 12)
        Me.grdCodeView.Name = "grdCodeView"
        Me.grdCodeView.Size = New System.Drawing.Size(472, 550)
        Me.grdCodeView.TabIndex = 3
        '
        'btn_LoadCodes
        '
        Me.btn_LoadCodes.Location = New System.Drawing.Point(550, 22)
        Me.btn_LoadCodes.Name = "btn_LoadCodes"
        Me.btn_LoadCodes.Size = New System.Drawing.Size(202, 58)
        Me.btn_LoadCodes.TabIndex = 4
        Me.btn_LoadCodes.Text = "Load Tag Codes"
        Me.btn_LoadCodes.UseVisualStyleBackColor = True
        '
        'ViewCodes
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(851, 605)
        Me.Controls.Add(Me.btn_LoadCodes)
        Me.Controls.Add(Me.grdCodeView)
        Me.Controls.Add(Me.lbl_CWTdb2)
        Me.Name = "ViewCodes"
        Me.Text = "FRAM Builder: View Tags"
        CType(Me.grdCodeView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lbl_CWTdb2 As System.Windows.Forms.Label
    Friend WithEvents grdCodeView As System.Windows.Forms.DataGridView
    Friend WithEvents btn_LoadCodes As System.Windows.Forms.Button
End Class
