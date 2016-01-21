<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class form_ViewCodes
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
Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
Me.lbl_CWTdb2 = New System.Windows.Forms.Label()
Me.grd_CodeView = New System.Windows.Forms.DataGridView()
Me.btn_SelectUnselect = New System.Windows.Forms.Button()
Me.btn_Return1 = New System.Windows.Forms.Button()
Me.btn_LoadTags = New System.Windows.Forms.Button()
Me.tip_SelectUnselect = New System.Windows.Forms.ToolTip(Me.components)
Me.pb_Loading = New System.Windows.Forms.ProgressBar()
Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
Me.dom_BroodYearChoose = New System.Windows.Forms.DomainUpDown()
Me.btn_LoadCodes = New System.Windows.Forms.Button()
Me.btn_BypassView = New System.Windows.Forms.Button()
Me.lbl_queryrunning = New System.Windows.Forms.Label()
CType(Me.grd_CodeView, System.ComponentModel.ISupportInitialize).BeginInit()
Me.SuspendLayout()
'
'lbl_CWTdb2
'
Me.lbl_CWTdb2.AutoSize = True
Me.lbl_CWTdb2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
Me.lbl_CWTdb2.Location = New System.Drawing.Point(12, 589)
Me.lbl_CWTdb2.Name = "lbl_CWTdb2"
Me.lbl_CWTdb2.Size = New System.Drawing.Size(155, 15)
Me.lbl_CWTdb2.TabIndex = 2
Me.lbl_CWTdb2.Text = "Database: (None Selected)"
'
'grd_CodeView
'
Me.grd_CodeView.AllowUserToOrderColumns = True
DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
Me.grd_CodeView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
Me.grd_CodeView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
Me.grd_CodeView.DefaultCellStyle = DataGridViewCellStyle2
Me.grd_CodeView.Location = New System.Drawing.Point(12, 49)
Me.grd_CodeView.Name = "grd_CodeView"
DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
Me.grd_CodeView.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
Me.grd_CodeView.Size = New System.Drawing.Size(1072, 429)
Me.grd_CodeView.TabIndex = 3
'
'btn_SelectUnselect
'
Me.btn_SelectUnselect.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
Me.btn_SelectUnselect.Location = New System.Drawing.Point(515, 513)
Me.btn_SelectUnselect.Name = "btn_SelectUnselect"
Me.btn_SelectUnselect.Size = New System.Drawing.Size(117, 58)
Me.btn_SelectUnselect.TabIndex = 4
Me.btn_SelectUnselect.Text = "Select or Unselect All"
Me.btn_SelectUnselect.UseVisualStyleBackColor = True
'
'btn_Return1
'
Me.btn_Return1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
Me.btn_Return1.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
Me.btn_Return1.Location = New System.Drawing.Point(919, 513)
Me.btn_Return1.Name = "btn_Return1"
Me.btn_Return1.Size = New System.Drawing.Size(140, 58)
Me.btn_Return1.TabIndex = 5
Me.btn_Return1.Text = "Return To Main Menu"
Me.btn_Return1.UseVisualStyleBackColor = False
'
'btn_LoadTags
'
Me.btn_LoadTags.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
Me.btn_LoadTags.Location = New System.Drawing.Point(644, 513)
Me.btn_LoadTags.Name = "btn_LoadTags"
Me.btn_LoadTags.Size = New System.Drawing.Size(120, 58)
Me.btn_LoadTags.TabIndex = 6
Me.btn_LoadTags.Text = "Import and View (SLOW)"
Me.btn_LoadTags.UseVisualStyleBackColor = True
'
'pb_Loading
'
Me.pb_Loading.Location = New System.Drawing.Point(16, 524)
Me.pb_Loading.MarqueeAnimationSpeed = 1
Me.pb_Loading.Maximum = 1000
Me.pb_Loading.Name = "pb_Loading"
Me.pb_Loading.Size = New System.Drawing.Size(483, 52)
Me.pb_Loading.Step = 1
Me.pb_Loading.Style = System.Windows.Forms.ProgressBarStyle.Marquee
Me.pb_Loading.TabIndex = 7
'
'BackgroundWorker1
'
'
'dom_BroodYearChoose
'
Me.dom_BroodYearChoose.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
Me.dom_BroodYearChoose.Location = New System.Drawing.Point(16, 10)
Me.dom_BroodYearChoose.Name = "dom_BroodYearChoose"
Me.dom_BroodYearChoose.Size = New System.Drawing.Size(338, 31)
Me.dom_BroodYearChoose.TabIndex = 9
Me.dom_BroodYearChoose.Text = "(Choose Tag Subset)"
'
'btn_LoadCodes
'
Me.btn_LoadCodes.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
Me.btn_LoadCodes.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
Me.btn_LoadCodes.Location = New System.Drawing.Point(360, 10)
Me.btn_LoadCodes.Name = "btn_LoadCodes"
Me.btn_LoadCodes.Size = New System.Drawing.Size(140, 31)
Me.btn_LoadCodes.TabIndex = 10
Me.btn_LoadCodes.Text = "Load 'em up"
Me.btn_LoadCodes.UseVisualStyleBackColor = False
'
'btn_BypassView
'
Me.btn_BypassView.Enabled = False
Me.btn_BypassView.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
Me.btn_BypassView.Location = New System.Drawing.Point(775, 513)
Me.btn_BypassView.Name = "btn_BypassView"
Me.btn_BypassView.Size = New System.Drawing.Size(131, 58)
Me.btn_BypassView.TabIndex = 11
Me.btn_BypassView.Text = "Import and Map (skip view)"
Me.btn_BypassView.UseVisualStyleBackColor = True
'
'lbl_queryrunning
'
Me.lbl_queryrunning.AutoSize = True
Me.lbl_queryrunning.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
Me.lbl_queryrunning.Location = New System.Drawing.Point(19, 491)
Me.lbl_queryrunning.Name = "lbl_queryrunning"
Me.lbl_queryrunning.Size = New System.Drawing.Size(57, 20)
Me.lbl_queryrunning.TabIndex = 12
Me.lbl_queryrunning.Text = "Label1"
'
'form_ViewCodes
'
Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
Me.ClientSize = New System.Drawing.Size(1094, 616)
Me.Controls.Add(Me.lbl_queryrunning)
Me.Controls.Add(Me.btn_BypassView)
Me.Controls.Add(Me.btn_LoadCodes)
Me.Controls.Add(Me.dom_BroodYearChoose)
Me.Controls.Add(Me.pb_Loading)
Me.Controls.Add(Me.btn_LoadTags)
Me.Controls.Add(Me.btn_Return1)
Me.Controls.Add(Me.btn_SelectUnselect)
Me.Controls.Add(Me.grd_CodeView)
Me.Controls.Add(Me.lbl_CWTdb2)
Me.Name = "form_ViewCodes"
Me.Text = "FRAM Builder: View Tags"
CType(Me.grd_CodeView, System.ComponentModel.ISupportInitialize).EndInit()
Me.ResumeLayout(False)
Me.PerformLayout()

End Sub
    Friend WithEvents lbl_CWTdb2 As System.Windows.Forms.Label
    Friend WithEvents btn_SelectUnselect As System.Windows.Forms.Button
    Friend WithEvents btn_Return1 As System.Windows.Forms.Button
    Friend WithEvents btn_LoadTags As System.Windows.Forms.Button
    Friend WithEvents tip_SelectUnselect As System.Windows.Forms.ToolTip
    Public WithEvents grd_CodeView As System.Windows.Forms.DataGridView
    Friend WithEvents pb_Loading As System.Windows.Forms.ProgressBar
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents dom_BroodYearChoose As System.Windows.Forms.DomainUpDown
   Friend WithEvents btn_LoadCodes As System.Windows.Forms.Button
   Friend WithEvents btn_BypassView As System.Windows.Forms.Button
   Friend WithEvents lbl_queryrunning As System.Windows.Forms.Label
End Class
