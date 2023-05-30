
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class XtraMain
        Inherits DevExpress.XtraEditors.XtraForm

        'Form overrides dispose to clean up the component list.
        <System.Diagnostics.DebuggerNonUserCode()>
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        'Required by the Windows Form Designer
        Private components As System.ComponentModel.IContainer

        'NOTE: The following procedure is required by the Windows Form Designer
        'It can be modified using the Windows Form Designer.  
        'Do not modify it using the code editor.
        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.lbstatserver = New System.Windows.Forms.Label()
        Me.lbmyEvent = New System.Windows.Forms.Label()
        Me.lb_statserver = New System.Windows.Forms.Label()
        Me.lb_username = New System.Windows.Forms.Label()
        Me.lbusername = New System.Windows.Forms.Label()
        Me.lboperation = New System.Windows.Forms.Label()
        Me.BgwFlashfirmware = New System.ComponentModel.BackgroundWorker()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.iReverse_Ultimate_Client.My.Resources.Resources.logoireverse
        Me.PictureBox1.Location = New System.Drawing.Point(78, 12)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(147, 207)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'lbstatserver
        '
        Me.lbstatserver.AutoSize = True
        Me.lbstatserver.Location = New System.Drawing.Point(93, 263)
        Me.lbstatserver.Name = "lbstatserver"
        Me.lbstatserver.Size = New System.Drawing.Size(71, 13)
        Me.lbstatserver.TabIndex = 2
        Me.lbstatserver.Text = "Disconnected"
        '
        'lbmyEvent
        '
        Me.lbmyEvent.AutoSize = True
        Me.lbmyEvent.Location = New System.Drawing.Point(199, 263)
        Me.lbmyEvent.Name = "lbmyEvent"
        Me.lbmyEvent.Size = New System.Drawing.Size(97, 13)
        Me.lbmyEvent.TabIndex = 2
        Me.lbmyEvent.Text = "client-ireverse-001"
        '
        'lb_statserver
        '
        Me.lb_statserver.AutoSize = True
        Me.lb_statserver.Location = New System.Drawing.Point(7, 263)
        Me.lb_statserver.Name = "lb_statserver"
        Me.lb_statserver.Size = New System.Drawing.Size(80, 13)
        Me.lb_statserver.TabIndex = 2
        Me.lb_statserver.Text = "Status Server :"
        '
        'lb_username
        '
        Me.lb_username.AutoSize = True
        Me.lb_username.Location = New System.Drawing.Point(9, 240)
        Me.lb_username.Name = "lb_username"
        Me.lb_username.Size = New System.Drawing.Size(77, 13)
        Me.lb_username.TabIndex = 2
        Me.lb_username.Text = "Username      :"
        '
        'lbusername
        '
        Me.lbusername.AutoSize = True
        Me.lbusername.Location = New System.Drawing.Point(93, 240)
        Me.lbusername.Name = "lbusername"
        Me.lbusername.Size = New System.Drawing.Size(36, 13)
        Me.lbusername.TabIndex = 2
        Me.lbusername.Text = "test-1"
        '
        'lboperation
        '
        Me.lboperation.AutoSize = True
        Me.lboperation.Location = New System.Drawing.Point(199, 240)
        Me.lboperation.Name = "lboperation"
        Me.lboperation.Size = New System.Drawing.Size(55, 13)
        Me.lboperation.TabIndex = 2
        Me.lboperation.Text = "Operation"
        '
        'BgwFlashfirmware
        '
        Me.BgwFlashfirmware.WorkerReportsProgress = True
        Me.BgwFlashfirmware.WorkerSupportsCancellation = True
        '
        'XtraMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(306, 286)
        Me.Controls.Add(Me.lbmyEvent)
        Me.Controls.Add(Me.lboperation)
        Me.Controls.Add(Me.lb_username)
        Me.Controls.Add(Me.lb_statserver)
        Me.Controls.Add(Me.lbusername)
        Me.Controls.Add(Me.lbstatserver)
        Me.Controls.Add(Me.PictureBox1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "XtraMain"
        Me.Opacity = 0.98R
        Me.Text = "iReverse Ultimate Client - @HadiK IT"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents lbstatserver As Label
    Friend WithEvents lbmyEvent As Label
    Friend WithEvents lb_statserver As Label
    Friend WithEvents lb_username As Label
    Friend WithEvents lbusername As Label
    Friend WithEvents lboperation As Label
    Public WithEvents BgwFlashfirmware As System.ComponentModel.BackgroundWorker
End Class
