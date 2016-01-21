<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class form_WelcomeAndConnect
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
        Me.components = New System.ComponentModel.Container()
        Me.btn_ConnectDB = New System.Windows.Forms.Button()
        Me.lbl_CWTdb = New System.Windows.Forms.Label()
        Me.btn_viewCodes = New System.Windows.Forms.Button()
        Me.btn_Options = New System.Windows.Forms.Button()
        Me.txt_WelcomeHeader = New System.Windows.Forms.TextBox()
        Me.btn_Exit = New System.Windows.Forms.Button()
        Me.tip_ChooseDB = New System.Windows.Forms.ToolTip(Me.components)
        Me.tip_MakeCodeSelection = New System.Windows.Forms.ToolTip(Me.components)
        Me.tip_SpecifyFilePrepOpts = New System.Windows.Forms.ToolTip(Me.components)
        Me.SuspendLayout()
        '
        'btn_ConnectDB
        '
        Me.btn_ConnectDB.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_ConnectDB.Location = New System.Drawing.Point(54, 69)
        Me.btn_ConnectDB.Name = "btn_ConnectDB"
        Me.btn_ConnectDB.Size = New System.Drawing.Size(287, 64)
        Me.btn_ConnectDB.TabIndex = 0
        Me.btn_ConnectDB.Text = "Select CWT Database"
        Me.btn_ConnectDB.UseVisualStyleBackColor = True
        '
        'lbl_CWTdb
        '
        Me.lbl_CWTdb.AutoSize = True
        Me.lbl_CWTdb.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_CWTdb.Location = New System.Drawing.Point(12, 422)
        Me.lbl_CWTdb.Name = "lbl_CWTdb"
        Me.lbl_CWTdb.Size = New System.Drawing.Size(153, 14)
        Me.lbl_CWTdb.TabIndex = 1
        Me.lbl_CWTdb.Text = "Database: (None Selected)"
        '
        'btn_viewCodes
        '
        Me.btn_viewCodes.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_viewCodes.Location = New System.Drawing.Point(53, 152)
        Me.btn_viewCodes.Name = "btn_viewCodes"
        Me.btn_viewCodes.Size = New System.Drawing.Size(287, 70)
        Me.btn_viewCodes.TabIndex = 2
        Me.btn_viewCodes.Text = "Select and View CWT Data"
        Me.btn_viewCodes.UseVisualStyleBackColor = True
        '
        'btn_Options
        '
        Me.btn_Options.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_Options.Location = New System.Drawing.Point(53, 243)
        Me.btn_Options.Name = "btn_Options"
        Me.btn_Options.Size = New System.Drawing.Size(287, 66)
        Me.btn_Options.TabIndex = 3
        Me.btn_Options.Text = "Set Output Options and Run"
        Me.btn_Options.UseVisualStyleBackColor = True
        '
        'txt_WelcomeHeader
        '
        Me.txt_WelcomeHeader.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.txt_WelcomeHeader.Font = New System.Drawing.Font("Calibri", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_WelcomeHeader.Location = New System.Drawing.Point(25, 12)
        Me.txt_WelcomeHeader.Multiline = True
        Me.txt_WelcomeHeader.Name = "txt_WelcomeHeader"
        Me.txt_WelcomeHeader.Size = New System.Drawing.Size(352, 42)
        Me.txt_WelcomeHeader.TabIndex = 5
        Me.txt_WelcomeHeader.Text = "Welcome to FRAMBuilder 2.0!"
        Me.txt_WelcomeHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'btn_Exit
        '
        Me.btn_Exit.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_Exit.Location = New System.Drawing.Point(54, 333)
        Me.btn_Exit.Name = "btn_Exit"
        Me.btn_Exit.Size = New System.Drawing.Size(287, 67)
        Me.btn_Exit.TabIndex = 6
        Me.btn_Exit.Text = "Exit Application"
        Me.btn_Exit.UseVisualStyleBackColor = True
        '
        'form_WelcomeAndConnect
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(400, 444)
        Me.Controls.Add(Me.btn_Exit)
        Me.Controls.Add(Me.txt_WelcomeHeader)
        Me.Controls.Add(Me.btn_Options)
        Me.Controls.Add(Me.btn_viewCodes)
        Me.Controls.Add(Me.lbl_CWTdb)
        Me.Controls.Add(Me.btn_ConnectDB)
        Me.Name = "form_WelcomeAndConnect"
        Me.Text = "FRAM Builder: Main Menu"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btn_ConnectDB As System.Windows.Forms.Button
    Friend WithEvents lbl_CWTdb As System.Windows.Forms.Label
    Friend WithEvents btn_viewCodes As System.Windows.Forms.Button
    Friend WithEvents btn_Options As System.Windows.Forms.Button
    Friend WithEvents txt_WelcomeHeader As System.Windows.Forms.TextBox
    Friend WithEvents btn_Exit As System.Windows.Forms.Button
    Friend WithEvents tip_ChooseDB As System.Windows.Forms.ToolTip
    Friend WithEvents tip_MakeCodeSelection As System.Windows.Forms.ToolTip
    Friend WithEvents tip_SpecifyFilePrepOpts As System.Windows.Forms.ToolTip

End Class
