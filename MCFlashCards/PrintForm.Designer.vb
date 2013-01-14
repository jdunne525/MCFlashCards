<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PrintForm
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
        Me.btnPrintFronts = New System.Windows.Forms.Button
        Me.btnPrintBacks = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.btnDoubleSided = New System.Windows.Forms.Button
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnPrintFronts
        '
        Me.btnPrintFronts.Location = New System.Drawing.Point(26, 94)
        Me.btnPrintFronts.Name = "btnPrintFronts"
        Me.btnPrintFronts.Size = New System.Drawing.Size(92, 32)
        Me.btnPrintFronts.TabIndex = 0
        Me.btnPrintFronts.Text = "Print ""Fronts"""
        Me.btnPrintFronts.UseVisualStyleBackColor = True
        '
        'btnPrintBacks
        '
        Me.btnPrintBacks.Location = New System.Drawing.Point(172, 94)
        Me.btnPrintBacks.Name = "btnPrintBacks"
        Me.btnPrintBacks.Size = New System.Drawing.Size(92, 32)
        Me.btnPrintBacks.TabIndex = 0
        Me.btnPrintBacks.Text = "Print ""Backs"""
        Me.btnPrintBacks.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(176, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Choose how to print the flash cards:"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.btnPrintBacks)
        Me.GroupBox1.Controls.Add(Me.btnPrintFronts)
        Me.GroupBox1.Location = New System.Drawing.Point(16, 39)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(294, 149)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Single Sided Printer"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(6, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(266, 75)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Pages are printed in 2 stages.  First Click Print ""Fronts,"" then put those pages " & _
            "back into the printer and click Print ""Backs"""
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.btnDoubleSided)
        Me.GroupBox2.Location = New System.Drawing.Point(319, 39)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(165, 149)
        Me.GroupBox2.TabIndex = 3
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Double Sided printer"
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(6, 16)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(153, 75)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Pages come out alternating front then back"
        '
        'btnDoubleSided
        '
        Me.btnDoubleSided.Location = New System.Drawing.Point(32, 94)
        Me.btnDoubleSided.Name = "btnDoubleSided"
        Me.btnDoubleSided.Size = New System.Drawing.Size(92, 32)
        Me.btnDoubleSided.TabIndex = 1
        Me.btnDoubleSided.Text = "Double Sided"
        Me.btnDoubleSided.UseVisualStyleBackColor = True
        '
        'PrintForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(496, 200)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label1)
        Me.Name = "PrintForm"
        Me.Text = "PrintForm"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnPrintFronts As System.Windows.Forms.Button
    Friend WithEvents btnPrintBacks As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnDoubleSided As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
End Class
