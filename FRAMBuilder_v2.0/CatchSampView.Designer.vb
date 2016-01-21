<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class form_CatchSampleViewer
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
        Me.grd_CatView = New System.Windows.Forms.DataGridView()
        Me.btn_ReturnToOutput = New System.Windows.Forms.Button()
        Me.btn_RetMainMenu = New System.Windows.Forms.Button()
        Me.btn_ExportData = New System.Windows.Forms.Button()
        CType(Me.grd_CatView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grd_CatView
        '
        Me.grd_CatView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grd_CatView.Location = New System.Drawing.Point(13, 13)
        Me.grd_CatView.Name = "grd_CatView"
        Me.grd_CatView.Size = New System.Drawing.Size(989, 365)
        Me.grd_CatView.TabIndex = 0
        '
        'btn_ReturnToOutput
        '
        Me.btn_ReturnToOutput.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btn_ReturnToOutput.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_ReturnToOutput.Location = New System.Drawing.Point(581, 397)
        Me.btn_ReturnToOutput.Name = "btn_ReturnToOutput"
        Me.btn_ReturnToOutput.Size = New System.Drawing.Size(202, 58)
        Me.btn_ReturnToOutput.TabIndex = 7
        Me.btn_ReturnToOutput.Text = "Return To Output Opts"
        Me.btn_ReturnToOutput.UseVisualStyleBackColor = False
        '
        'btn_RetMainMenu
        '
        Me.btn_RetMainMenu.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btn_RetMainMenu.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_RetMainMenu.Location = New System.Drawing.Point(800, 397)
        Me.btn_RetMainMenu.Name = "btn_RetMainMenu"
        Me.btn_RetMainMenu.Size = New System.Drawing.Size(202, 58)
        Me.btn_RetMainMenu.TabIndex = 8
        Me.btn_RetMainMenu.Text = "Return To Main Menu"
        Me.btn_RetMainMenu.UseVisualStyleBackColor = False
        '
        'btn_ExportData
        '
        Me.btn_ExportData.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btn_ExportData.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_ExportData.Location = New System.Drawing.Point(361, 397)
        Me.btn_ExportData.Name = "btn_ExportData"
        Me.btn_ExportData.Size = New System.Drawing.Size(202, 58)
        Me.btn_ExportData.TabIndex = 9
        Me.btn_ExportData.Text = "Export Catch Data"
        Me.btn_ExportData.UseVisualStyleBackColor = False
        '
        'form_CatchSampleViewer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1014, 465)
        Me.Controls.Add(Me.btn_ExportData)
        Me.Controls.Add(Me.btn_RetMainMenu)
        Me.Controls.Add(Me.btn_ReturnToOutput)
        Me.Controls.Add(Me.grd_CatView)
        Me.Name = "form_CatchSampleViewer"
        Me.Text = "Catch Sample View"
        CType(Me.grd_CatView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grd_CatView As System.Windows.Forms.DataGridView
    Friend WithEvents btn_ReturnToOutput As System.Windows.Forms.Button
    Friend WithEvents btn_RetMainMenu As System.Windows.Forms.Button
    Friend WithEvents btn_ExportData As System.Windows.Forms.Button
End Class
