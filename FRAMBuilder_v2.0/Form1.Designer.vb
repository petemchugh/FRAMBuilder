<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Me.ConnectDB = New System.Windows.Forms.Button()
        Me.lbl_CWTdb = New System.Windows.Forms.Label()
        Me.btn_viewCodes = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'ConnectDB
        '
        Me.ConnectDB.Location = New System.Drawing.Point(27, 12)
        Me.ConnectDB.Name = "ConnectDB"
        Me.ConnectDB.Size = New System.Drawing.Size(182, 36)
        Me.ConnectDB.TabIndex = 0
        Me.ConnectDB.Text = "Connect Database"
        Me.ConnectDB.UseVisualStyleBackColor = True
        '
        'lbl_CWTdb
        '
        Me.lbl_CWTdb.AutoSize = True
        Me.lbl_CWTdb.Location = New System.Drawing.Point(12, 482)
        Me.lbl_CWTdb.Name = "lbl_CWTdb"
        Me.lbl_CWTdb.Size = New System.Drawing.Size(39, 13)
        Me.lbl_CWTdb.TabIndex = 1
        Me.lbl_CWTdb.Text = "Label1"
        '
        'btn_viewCodes
        '
        Me.btn_viewCodes.Location = New System.Drawing.Point(30, 55)
        Me.btn_viewCodes.Name = "btn_viewCodes"
        Me.btn_viewCodes.Size = New System.Drawing.Size(179, 37)
        Me.btn_viewCodes.TabIndex = 2
        Me.btn_viewCodes.Text = "View tag codes"
        Me.btn_viewCodes.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(612, 504)
        Me.Controls.Add(Me.btn_viewCodes)
        Me.Controls.Add(Me.lbl_CWTdb)
        Me.Controls.Add(Me.ConnectDB)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ConnectDB As System.Windows.Forms.Button
    Friend WithEvents lbl_CWTdb As System.Windows.Forms.Label
    Friend WithEvents btn_viewCodes As System.Windows.Forms.Button

End Class
