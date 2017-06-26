<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class form_OutputOptions
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
        Me.lbl_CWTdb3 = New System.Windows.Forms.Label()
        Me.btn_Return1 = New System.Windows.Forms.Button()
        Me.ck_mergeCodes = New System.Windows.Forms.CheckBox()
        Me.ck_draftfish = New System.Windows.Forms.CheckBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.ck_bnBroodWt = New System.Windows.Forms.CheckBox()
        Me.ck_winBYwt = New System.Windows.Forms.CheckBox()
        Me.ck_mergeBtwnBY = New System.Windows.Forms.CheckBox()
        Me.ck_BigTable = New System.Windows.Forms.CheckBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.ck_CWTAll = New System.Windows.Forms.CheckBox()
        Me.ck_database = New System.Windows.Forms.CheckBox()
        Me.ck_CWTfile = New System.Windows.Forms.CheckBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.ck_LengthOnly = New System.Windows.Forms.CheckBox()
        Me.ck_LengthToo = New System.Windows.Forms.CheckBox()
        Me.btn_CreateOut = New System.Windows.Forms.Button()
        Me.tip_MapWrite = New System.Windows.Forms.ToolTip(Me.components)
        Me.btn_SetDir = New System.Windows.Forms.Button()
        Me.BGworker_output = New System.ComponentModel.BackgroundWorker()
        Me.btn_CatSam = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'lbl_CWTdb3
        '
        Me.lbl_CWTdb3.AutoSize = True
        Me.lbl_CWTdb3.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_CWTdb3.Location = New System.Drawing.Point(12, 436)
        Me.lbl_CWTdb3.Name = "lbl_CWTdb3"
        Me.lbl_CWTdb3.Size = New System.Drawing.Size(156, 14)
        Me.lbl_CWTdb3.TabIndex = 2
        Me.lbl_CWTdb3.Text = "Database:  (None Selected)"
        '
        'btn_Return1
        '
        Me.btn_Return1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btn_Return1.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_Return1.Location = New System.Drawing.Point(347, 354)
        Me.btn_Return1.Name = "btn_Return1"
        Me.btn_Return1.Size = New System.Drawing.Size(202, 58)
        Me.btn_Return1.TabIndex = 6
        Me.btn_Return1.Text = "Return To Main Menu"
        Me.btn_Return1.UseVisualStyleBackColor = False
        '
        'ck_mergeCodes
        '
        Me.ck_mergeCodes.AutoSize = True
        Me.ck_mergeCodes.Checked = True
        Me.ck_mergeCodes.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ck_mergeCodes.Location = New System.Drawing.Point(14, 44)
        Me.ck_mergeCodes.Name = "ck_mergeCodes"
        Me.ck_mergeCodes.Size = New System.Drawing.Size(130, 17)
        Me.ck_mergeCodes.TabIndex = 7
        Me.ck_mergeCodes.Text = "Merge Codes w/in BY"
        Me.ck_mergeCodes.UseVisualStyleBackColor = True
        '
        'ck_draftfish
        '
        Me.ck_draftfish.AutoSize = True
        Me.ck_draftfish.Location = New System.Drawing.Point(21, 27)
        Me.ck_draftfish.Name = "ck_draftfish"
        Me.ck_draftfish.Size = New System.Drawing.Size(126, 17)
        Me.ck_draftfish.TabIndex = 8
        Me.ck_draftfish.Text = "Include draft fisheries"
        Me.ck_draftfish.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.ck_bnBroodWt)
        Me.GroupBox1.Controls.Add(Me.ck_winBYwt)
        Me.GroupBox1.Controls.Add(Me.ck_mergeBtwnBY)
        Me.GroupBox1.Controls.Add(Me.ck_BigTable)
        Me.GroupBox1.Controls.Add(Me.ck_mergeCodes)
        Me.GroupBox1.Location = New System.Drawing.Point(15, 23)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(250, 208)
        Me.GroupBox1.TabIndex = 12
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Mapping and Merging Options"
        '
        'ck_bnBroodWt
        '
        Me.ck_bnBroodWt.AutoSize = True
        Me.ck_bnBroodWt.Location = New System.Drawing.Point(14, 142)
        Me.ck_bnBroodWt.Name = "ck_bnBroodWt"
        Me.ck_bnBroodWt.Size = New System.Drawing.Size(217, 56)
        Me.ck_bnBroodWt.TabIndex = 20
        Me.ck_bnBroodWt.Text = "Use db wts + rules for b/n BY merge?" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "  flag = 1 is unweighted" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "  flag = 2 OR no " & _
    "flag is rec's wtd (default)" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "  flag = 3 is user-spec'd wt" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.ck_bnBroodWt.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.ck_bnBroodWt.UseVisualStyleBackColor = True
        '
        'ck_winBYwt
        '
        Me.ck_winBYwt.AutoSize = True
        Me.ck_winBYwt.Location = New System.Drawing.Point(14, 94)
        Me.ck_winBYwt.Name = "ck_winBYwt"
        Me.ck_winBYwt.Size = New System.Drawing.Size(230, 43)
        Me.ck_winBYwt.TabIndex = 9
        Me.ck_winBYwt.Text = "Use db wts + rules for w/in BY merge?" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "  flag = 1 OR no flag is unweighted (defau" & _
    "lt)" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "  flag = 2 is user-spec'd wt" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.ck_winBYwt.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.ck_winBYwt.UseVisualStyleBackColor = True
        '
        'ck_mergeBtwnBY
        '
        Me.ck_mergeBtwnBY.AutoSize = True
        Me.ck_mergeBtwnBY.Checked = True
        Me.ck_mergeBtwnBY.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ck_mergeBtwnBY.Location = New System.Drawing.Point(14, 69)
        Me.ck_mergeBtwnBY.Name = "ck_mergeBtwnBY"
        Me.ck_mergeBtwnBY.Size = New System.Drawing.Size(131, 17)
        Me.ck_mergeBtwnBY.TabIndex = 19
        Me.ck_mergeBtwnBY.Text = "Merge Codes b/n BYs"
        Me.ck_mergeBtwnBY.UseVisualStyleBackColor = True
        '
        'ck_BigTable
        '
        Me.ck_BigTable.AutoSize = True
        Me.ck_BigTable.Checked = True
        Me.ck_BigTable.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ck_BigTable.Location = New System.Drawing.Point(14, 20)
        Me.ck_BigTable.Name = "ck_BigTable"
        Me.ck_BigTable.Size = New System.Drawing.Size(150, 17)
        Me.ck_BigTable.TabIndex = 18
        Me.ck_BigTable.Text = "Create mapped table in db"
        Me.ck_BigTable.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.ck_CWTAll)
        Me.GroupBox2.Controls.Add(Me.ck_database)
        Me.GroupBox2.Controls.Add(Me.ck_CWTfile)
        Me.GroupBox2.Location = New System.Drawing.Point(284, 23)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(151, 208)
        Me.GroupBox2.TabIndex = 13
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Output Format Options"
        '
        'ck_CWTAll
        '
        Me.ck_CWTAll.AutoSize = True
        Me.ck_CWTAll.Checked = True
        Me.ck_CWTAll.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ck_CWTAll.Location = New System.Drawing.Point(20, 90)
        Me.ck_CWTAll.Name = "ck_CWTAll"
        Me.ck_CWTAll.Size = New System.Drawing.Size(128, 17)
        Me.ck_CWTAll.TabIndex = 9
        Me.ck_CWTAll.Text = "Write to CWTAll table"
        Me.ck_CWTAll.UseVisualStyleBackColor = True
        '
        'ck_database
        '
        Me.ck_database.AutoSize = True
        Me.ck_database.Checked = True
        Me.ck_database.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ck_database.Location = New System.Drawing.Point(20, 58)
        Me.ck_database.Name = "ck_database"
        Me.ck_database.Size = New System.Drawing.Size(110, 17)
        Me.ck_database.TabIndex = 8
        Me.ck_database.Text = "Write to database"
        Me.ck_database.UseVisualStyleBackColor = True
        '
        'ck_CWTfile
        '
        Me.ck_CWTfile.AutoSize = True
        Me.ck_CWTfile.Enabled = False
        Me.ck_CWTfile.Location = New System.Drawing.Point(20, 27)
        Me.ck_CWTfile.Name = "ck_CWTfile"
        Me.ck_CWTfile.Size = New System.Drawing.Size(91, 17)
        Me.ck_CWTfile.TabIndex = 7
        Me.ck_CWTfile.Text = "*CWT text file"
        Me.ck_CWTfile.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.ck_LengthOnly)
        Me.GroupBox3.Controls.Add(Me.ck_LengthToo)
        Me.GroupBox3.Controls.Add(Me.ck_draftfish)
        Me.GroupBox3.Location = New System.Drawing.Point(447, 23)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(189, 208)
        Me.GroupBox3.TabIndex = 14
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Other Processing Options"
        '
        'ck_LengthOnly
        '
        Me.ck_LengthOnly.AutoSize = True
        Me.ck_LengthOnly.Location = New System.Drawing.Point(22, 89)
        Me.ck_LengthOnly.Name = "ck_LengthOnly"
        Me.ck_LengthOnly.Size = New System.Drawing.Size(146, 17)
        Me.ck_LengthOnly.TabIndex = 10
        Me.ck_LengthOnly.Text = "Prepare length file ONLY."
        Me.ck_LengthOnly.UseVisualStyleBackColor = True
        '
        'ck_LengthToo
        '
        Me.ck_LengthToo.AutoSize = True
        Me.ck_LengthToo.Location = New System.Drawing.Point(22, 58)
        Me.ck_LengthToo.Name = "ck_LengthToo"
        Me.ck_LengthToo.Size = New System.Drawing.Size(132, 17)
        Me.ck_LengthToo.TabIndex = 9
        Me.ck_LengthToo.Text = "Prepare length file too."
        Me.ck_LengthToo.UseVisualStyleBackColor = True
        '
        'btn_CreateOut
        '
        Me.btn_CreateOut.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_CreateOut.Location = New System.Drawing.Point(30, 350)
        Me.btn_CreateOut.Name = "btn_CreateOut"
        Me.btn_CreateOut.Size = New System.Drawing.Size(287, 67)
        Me.btn_CreateOut.TabIndex = 15
        Me.btn_CreateOut.Text = "Create Mapped Output Files"
        Me.btn_CreateOut.UseVisualStyleBackColor = True
        '
        'btn_SetDir
        '
        Me.btn_SetDir.Enabled = False
        Me.btn_SetDir.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_SetDir.Location = New System.Drawing.Point(30, 265)
        Me.btn_SetDir.Name = "btn_SetDir"
        Me.btn_SetDir.Size = New System.Drawing.Size(287, 67)
        Me.btn_SetDir.TabIndex = 16
        Me.btn_SetDir.Text = "Set Output Directory"
        Me.btn_SetDir.UseVisualStyleBackColor = True
        Me.btn_SetDir.Visible = False
        '
        'BGworker_output
        '
        '
        'btn_CatSam
        '
        Me.btn_CatSam.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btn_CatSam.Enabled = False
        Me.btn_CatSam.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_CatSam.Location = New System.Drawing.Point(347, 265)
        Me.btn_CatSam.Name = "btn_CatSam"
        Me.btn_CatSam.Size = New System.Drawing.Size(202, 58)
        Me.btn_CatSam.TabIndex = 17
        Me.btn_CatSam.Text = "LL's Catch Sample Button"
        Me.btn_CatSam.UseVisualStyleBackColor = False
        Me.btn_CatSam.Visible = False
        '
        'form_OutputOptions
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(662, 459)
        Me.Controls.Add(Me.btn_CatSam)
        Me.Controls.Add(Me.btn_SetDir)
        Me.Controls.Add(Me.btn_CreateOut)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btn_Return1)
        Me.Controls.Add(Me.lbl_CWTdb3)
        Me.Name = "form_OutputOptions"
        Me.Text = "OutputOptions"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

End Sub
    Friend WithEvents lbl_CWTdb3 As System.Windows.Forms.Label
    Friend WithEvents btn_Return1 As System.Windows.Forms.Button
    Friend WithEvents ck_mergeCodes As System.Windows.Forms.CheckBox
    Friend WithEvents ck_draftfish As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents btn_CreateOut As System.Windows.Forms.Button
    Friend WithEvents tip_MapWrite As System.Windows.Forms.ToolTip
    Friend WithEvents btn_SetDir As System.Windows.Forms.Button
    Public WithEvents ck_database As System.Windows.Forms.CheckBox
    Public WithEvents ck_CWTfile As System.Windows.Forms.CheckBox
    Friend WithEvents BGworker_output As System.ComponentModel.BackgroundWorker
    Friend WithEvents btn_CatSam As System.Windows.Forms.Button
    Friend WithEvents ck_BigTable As System.Windows.Forms.CheckBox
    Friend WithEvents ck_winBYwt As System.Windows.Forms.CheckBox
    Friend WithEvents ck_mergeBtwnBY As System.Windows.Forms.CheckBox
    Friend WithEvents ck_bnBroodWt As System.Windows.Forms.CheckBox
    Friend WithEvents ck_LengthOnly As System.Windows.Forms.CheckBox
    Friend WithEvents ck_LengthToo As System.Windows.Forms.CheckBox
    Public WithEvents ck_CWTAll As System.Windows.Forms.CheckBox
End Class
