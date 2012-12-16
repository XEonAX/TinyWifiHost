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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.TextBox4 = New System.Windows.Forms.TextBox()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Button9 = New System.Windows.Forms.Button()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.Button7 = New System.Windows.Forms.Button()
        Me.NotifyIcon1 = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ConMenuState = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenItConf = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenSSID = New System.Windows.Forms.ToolStripTextBox()
        Me.MenPass = New System.Windows.Forms.ToolStripTextBox()
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(102, 86)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(43, 27)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "..."
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 1000
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(79, 9)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(184, 20)
        Me.TextBox1.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(38, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(35, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "SSID:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(17, 43)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(56, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Password:"
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(79, 40)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(184, 20)
        Me.TextBox2.TabIndex = 4
        '
        'Timer2
        '
        Me.Timer2.Enabled = True
        Me.Timer2.Interval = 10
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 70)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(88, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "                           "
        '
        'Button2
        '
        Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.Location = New System.Drawing.Point(224, 86)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(39, 27)
        Me.Button2.TabIndex = 6
        Me.Button2.Text = ">>"
        Me.ToolTip1.SetToolTip(Me.Button2, "Settings for Host IP.")
        Me.Button2.UseVisualStyleBackColor = True
        '
        'TextBox3
        '
        Me.TextBox3.Location = New System.Drawing.Point(284, 9)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(171, 20)
        Me.TextBox3.TabIndex = 7
        Me.TextBox3.Text = "Wireless Network Connection 2"
        Me.ToolTip1.SetToolTip(Me.TextBox3, "Name of the Interface which connects using Microsoft Virtual WiFi Miniport Adapte" & _
        "r" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Or Name of the Adapter whose IP you want to Change.")
        '
        'TextBox4
        '
        Me.TextBox4.Location = New System.Drawing.Point(284, 40)
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.Size = New System.Drawing.Size(109, 20)
        Me.TextBox4.TabIndex = 8
        Me.ToolTip1.SetToolTip(Me.TextBox4, "IP address to be set to the above interface" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Preferably ending with """".1"""" If you" & _
        " are hosting the network." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "And not ending with """".1"""" If you are not hosting the" & _
        " network.")
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(284, 86)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(95, 27)
        Me.Button3.TabIndex = 9
        Me.Button3.Text = "Change IP"
        Me.ToolTip1.SetToolTip(Me.Button3, "If this does not work. Try running as Admin.")
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(403, 40)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(52, 20)
        Me.Button4.TabIndex = 10
        Me.Button4.Text = "Refresh"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Button9
        '
        Me.Button9.Location = New System.Drawing.Point(151, 86)
        Me.Button9.Name = "Button9"
        Me.Button9.Size = New System.Drawing.Size(67, 27)
        Me.Button9.TabIndex = 16
        Me.Button9.Text = "Clients ▼"
        Me.ToolTip1.SetToolTip(Me.Button9, "List of Connected Clients")
        Me.Button9.UseVisualStyleBackColor = True
        '
        'Button6
        '
        Me.Button6.Location = New System.Drawing.Point(61, 86)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(35, 27)
        Me.Button6.TabIndex = 12
        Me.Button6.Text = "Exit"
        Me.Button6.UseVisualStyleBackColor = True
        '
        'Button7
        '
        Me.Button7.Location = New System.Drawing.Point(385, 86)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(70, 26)
        Me.Button7.TabIndex = 13
        Me.Button7.Text = "About"
        Me.Button7.UseVisualStyleBackColor = True
        '
        'NotifyIcon1
        '
        Me.NotifyIcon1.ContextMenuStrip = Me.ContextMenuStrip1
        Me.NotifyIcon1.Icon = CType(resources.GetObject("NotifyIcon1.Icon"), System.Drawing.Icon)
        Me.NotifyIcon1.Text = " Wifi Host"
        Me.NotifyIcon1.Visible = True
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ConMenuState, Me.MenItConf})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(111, 48)
        Me.ContextMenuStrip1.Text = "etet"
        '
        'ConMenuState
        '
        Me.ConMenuState.Name = "ConMenuState"
        Me.ConMenuState.Size = New System.Drawing.Size(110, 22)
        Me.ConMenuState.Text = "start"
        '
        'MenItConf
        '
        Me.MenItConf.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MenSSID, Me.MenPass})
        Me.MenItConf.Name = "MenItConf"
        Me.MenItConf.Size = New System.Drawing.Size(110, 22)
        Me.MenItConf.Text = "Config"
        '
        'MenSSID
        '
        Me.MenSSID.Name = "MenSSID"
        Me.MenSSID.Size = New System.Drawing.Size(100, 23)
        '
        'MenPass
        '
        Me.MenPass.Name = "MenPass"
        Me.MenPass.Size = New System.Drawing.Size(100, 23)
        '
        'ListBox1
        '
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.Location = New System.Drawing.Point(8, 144)
        Me.ListBox1.MultiColumn = True
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(255, 173)
        Me.ListBox1.TabIndex = 15
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(5, 128)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(127, 13)
        Me.Label4.TabIndex = 17
        Me.Label4.Text = "List of Connected Clients:"
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(12, 86)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(43, 27)
        Me.Button5.TabIndex = 18
        Me.Button5.Text = "Chat"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(272, 120)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Button9)
        Me.Controls.Add(Me.ListBox1)
        Me.Controls.Add(Me.Button7)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button6)
        Me.Controls.Add(Me.TextBox4)
        Me.Controls.Add(Me.TextBox3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Button1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "Form1"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.Text = "Wifi Host"
        Me.TopMost = True
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents Timer2 As System.Windows.Forms.Timer
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox4 As System.Windows.Forms.TextBox
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents Button6 As System.Windows.Forms.Button
    Friend WithEvents Button7 As System.Windows.Forms.Button
    Friend WithEvents NotifyIcon1 As System.Windows.Forms.NotifyIcon
    Friend WithEvents ListBox1 As System.Windows.Forms.ListBox
    Friend WithEvents Button9 As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ConMenuState As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenItConf As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenSSID As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents MenPass As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents Button5 As System.Windows.Forms.Button

End Class
