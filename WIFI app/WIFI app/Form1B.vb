﻿Imports System.Net
Imports Microsoft.VisualBasic.Devices

Public Class Form1B

    Friend VirtualRouter
    Dim WithEvents vr As New VirtualRouter.Wlan.WlanManager
    Dim WithEvents ics As New IcsMgr.IcsManager
    Dim listofcclients As String = ""
    Dim ct As Integer = 0
    Dim noconn As Integer = 0
    Dim StateHist As Boolean
    'im mytask As System.Threading.Tasks.Task
    'Dim arr() As String = {"0"}
    Dim vbdevaud As New Microsoft.VisualBasic.Devices.Audio
    Dim i As Integer
    Dim j As Integer = 0
    Dim hico, cico As Icon
    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Try
            If Button1.Text = "Start" Then
                'Debug.Print(My.Application.CommandLineArgs.Count)
                Validateinput()

                vr.StopHostedNetwork()
                vr.ForceStop()
                vr.SetConnectionSettings(TextBox1.Text, 20)
                vr.SetSecondaryKey(TextBox2.Text)
                vr.StartHostedNetwork()
                vr.QuerySecondaryKey(TextBox2.Text, vbNull, vbNull)
                Timer2.Enabled = True
                Hosted(True)
            Else
                Timer2.Enabled = True
                vr.StopHostedNetwork()
                vr.ForceStop()
                vr.QuerySecondaryKey(TextBox2.Text, vbNull, vbNull)
                Hosted(False)
            End If
        Catch ex As Exception
            NotifyIcon1.BalloonTipTitle = "Error"
            NotifyIcon1.BalloonTipText = ex.Message.ToString
            NotifyIcon1.ShowBalloonTip(4000)
        End Try
    End Sub

    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        If vr.IsHostedNetworkStarted = True Then
            Hosted(True)
            If vr.Stations.Count > noconn Then
                Try
                    vbdevaud.Play(Application.StartupPath + "\connect.wav")
                Catch ex As Exception
                End Try
                noconn = vr.Stations.Count
                ListBox1.Items.Clear()
                listofcclients = ""
                For Each keyy In vr.Stations.Keys
                    ListBox1.Items.Add(keyy)
                    listofcclients += keyy + vbCrLf
                Next
                NotifyIcon1.ShowBalloonTip(200, "New Client Connected", "Clients Currently Connected:" + vbCrLf + listofcclients, ToolTipIcon.Info)
            ElseIf vr.Stations.Count < noconn Then
                Try
                    vbdevaud.Play(Application.StartupPath + "\disconnect.wav")
                Catch exx As Exception
                End Try


                noconn = vr.Stations.Count
                ListBox1.Items.Clear()
                listofcclients = ""
                For Each keyy In vr.Stations.Keys
                    ListBox1.Items.Add(vr.Stations.Item(keyy).MacAddress.ToString)
                    listofcclients += keyy + vbCrLf
                Next
                NotifyIcon1.ShowBalloonTip(200, "Client Disconnected", "Clients Currently Connected:" + vbCrLf + listofcclients, ToolTipIcon.Info)

            End If
            Label3.Text = "No of Connected Clients:" + vr.Stations.Count.ToString
        Else
            Hosted(False)
        End If
        If vr.IsHostedNetworkStarted = True Then
            Hosted(True)
        ElseIf vr.HostedNetworkState = Global.VirtualRouter.Wlan.WinAPI.WLAN_HOSTED_NETWORK_STATE.wlan_hosted_network_unavailable Then
            Hosted(False)
        End If
        
    End Sub

    Private Sub Timer2_Tick(sender As System.Object, e As System.EventArgs) Handles Timer2.Tick
        ct += 1
        If ct = 4 Then
            hico = Me.Icon
            cico = NotifyIcon1.Icon
            Me.Icon = cico
        End If
        If ct = 9 Then
            TextBox1.Text = ""
            TextBox2.Text = ""
            listofcclients = ""
            'Debug.Print(My.Application.CommandLineArgs.Count)
            If My.Application.CommandLineArgs.Count > 0 Then
                For Each cmd In My.Application.CommandLineArgs
                    'Debug.Print(cmd)
                    If cmd.StartsWith("ssid=") Then TextBox1.Text = cmd.Substring(5)
                    If cmd.StartsWith("pass=") Then TextBox2.Text = cmd.Substring(5)
                Next
            End If
            If TextBox1.Text = "" Then vr.QueryConnectionSettings(TextBox1.Text, vbNull)
            If TextBox2.Text = "" Then vr.QuerySecondaryKey(TextBox2.Text, vbNull, vbNull)
            noconn = 0
            Me.WindowState = FormWindowState.Normal
        End If
        If ct > 9 And Button1.Text = "Start" Then

            If My.Application.CommandLineArgs.Contains("autostart") Then
                vr.StopHostedNetwork()
                vr.ForceStop()
                Validateinput()
                Me.WindowState = FormWindowState.Minimized
                Button1_Click(Me, EventArgs.Empty)

            End If
            Timer2.Enabled = False
            ct = 100
        End If
        '  TextBox4.Text = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName).AddressList(1).ToString()
    End Sub





    'Private Sub Button3_Click(sender As System.Object, e As System.EventArgs)
    '    Try
    '        Shell("netsh interface ip set address """ + TextBox3.Text + """ static " + TextBox4.Text + " 255.255.255.0 " + TextBox4.Text + " 2", AppWinStyle.Hide)
    '    Catch ex As Exception
    '        MsgBox(ex.ToString + vbCrLf + "Try Running as admin.", MsgBoxStyle.Information) '"Try Running as admin.", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly)
    '    End Try
    '    Timer2.Enabled = True
    'End Sub

    'Private Sub Button4_Click(sender As System.Object, e As System.EventArgs)

    '    If j < System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName).AddressList.Length Then
    '        TextBox4.Text = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName).AddressList(j).ToString()
    '        j += 1
    '    Else
    '        j = 0
    '    End If
    'End Sub



    Private Sub Button6_Click(sender As System.Object, e As System.EventArgs) Handles Button6.Click
        'DemoApp.Close()  'remove this to remove chat and
        'DemoApp.Dispose() 'remove this to remove chat and
        Me.Close()
        Try
            vr.StopHostedNetwork()
        Catch eex As Exception
        End Try
        End
    End Sub

    Private Sub Button7_Click(sender As System.Object, e As System.EventArgs) Handles Button7.Click
        MsgBox("This Application is created by Sumant Vanage." + vbCrLf + "Special Thanks to Chris Pietschmann" + vbCrLf + "And Microsoft.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "AbouT")
    End Sub

    Private Sub NotifyIcon1_BalloonTipClicked(sender As Object, e As System.EventArgs) Handles NotifyIcon1.BalloonTipClicked
        Me.WindowState = FormWindowState.Normal
        Me.Focus()
    End Sub

    Private Sub NotifyIcon1_Click(sender As Object, e As System.EventArgs) Handles NotifyIcon1.Click
        
        NotifyIcon1.ShowBalloonTip(3000, "Connected Clients:", listofcclients, ToolTipIcon.Info)

    End Sub

    Private Sub NotifyIcon1_DoubleClick(sender As Object, e As System.EventArgs) Handles NotifyIcon1.DoubleClick
        If Me.Visible = True Then
            Me.Visible = False
        Else
            Me.Visible = True
        End If
    End Sub





    Private Sub Button9_Click(sender As System.Object, e As System.EventArgs) Handles Button9.Click

        If Button9.Text = "Clients && ICS ▼" Then
            For Me.i = 1 To 48
                Me.Height += 4
                Me.Width += 4
                Me.Refresh()
            Next
            Button9.Text = "Clients && ICS ▲"
        Else
            For Me.i = 1 To 192
                Me.Height -= 1
                Me.Width -= 1
            Next
            Button9.Text = "Clients && ICS ▼"
        End If
        
    End Sub

    Private Sub Form1_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Me.Width = 278
        Me.Height = 146
        Dim IntfaceIndex As Integer = 0
        For Each Intface In ics.Connections
            ListBox2.Items.Add(Intface.Name)
            ListBox3.Items.Add(Intface.Name)
            '  If Intface.IsPublic = True Then
            '  Debug.Print(">>" + Intface.Name)
            '   ListBox2.SelectedIndex = IntfaceIndex
            '   End If
            If Intface.IsPrivate = True Then
                'Debug.Print("<<" + Intface.Name)
                ListBox3.SelectedIndex = IntfaceIndex
            End If
            IntfaceIndex += 1
        Next

    End Sub



    Private Sub Form1_Resize(sender As Object, e As System.EventArgs) Handles Me.Resize
        If Me.WindowState = FormWindowState.Minimized Then
            Me.Hide()
        End If
        If Me.WindowState = FormWindowState.Normal Then
            Me.Show()
            Me.Focus()
        End If
    End Sub

    Private Sub ConMenuState_Click(sender As System.Object, e As System.EventArgs) Handles ConMenuState.Click
        Button1_Click(Me, EventArgs.Empty)
    End Sub
    Public Sub Hosted(ByVal started As Boolean)
        If started = True Then
            StateHist = True
            Button1.Text = "Stop"
            ToolTip1.SetToolTip(Button1, "Stop the HostedNetwork")
            ConMenuState.Text = "Stop"
            ConMenuState.ToolTipText = "Stop the HostedNetwork"

            NotifyIcon1.Text = "No of Clients Connected: " + noconn.ToString
            NotifyIcon1.Icon = hico
            Me.Icon = hico
        Else
            StateHist = False
            Button1.Text = "Start"
            ToolTip1.SetToolTip(Button1, "Start the HostedNetwork")
            ConMenuState.Text = "Start"
            ConMenuState.ToolTipText = "Start the HostedNetwork"
            noconn = 0
            ListBox1.Items.Clear()
            vr.Stations.Clear()
            Label3.Text = "No of Connected Clients:0"
            NotifyIcon1.Icon = cico
            Me.Icon = cico
        End If
    End Sub
    Public Sub Validateinput()
        If TextBox1.Text = "" Then
            NotifyIcon1.ShowBalloonTip(1000, "SSID Error", "No SSID Supplied" + vbCrLf + "SSID changed to ""SSID""", ToolTipIcon.Warning)
            TextBox1.Text = "SSID"
            MenSSID.Text = TextBox1.Text
        End If
        If TextBox2.Text.Length < 8 Then
            NotifyIcon1.ShowBalloonTip(1000, "Password Error", "Password smaller than 8 characters" + vbCrLf + "Password changed to ""qassword""", ToolTipIcon.Warning)
            TextBox2.Text = "qassword"
            MenPass.Text = TextBox2.Text
        End If
    End Sub





    Private Sub MenSSID_TextChanged(sender As Object, e As System.EventArgs) Handles MenSSID.TextChanged
        If MenSSID.Focused Then
            TextBox1.Text = MenSSID.Text
        End If
    End Sub

    Private Sub MenPass_TextChanged(sender As Object, e As System.EventArgs) Handles MenPass.TextChanged
        If MenPass.Focused Then
            TextBox2.Text = MenPass.Text
        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As System.EventArgs) Handles TextBox1.TextChanged
        MenSSID.Text = TextBox1.Text
    End Sub
    Private Sub TextBox2_TextChanged(sender As Object, e As System.EventArgs) Handles TextBox2.TextChanged
        MenPass.Text = TextBox2.Text
    End Sub


    'Private Sub Button5_Click(sender As System.Object, e As System.EventArgs) Handles Button5.Click  'remove this to remove chat and
    '    DemoApp.Show() 'remove this to remove chat and
    '    Me.Visible = False 'remove this to remove chat and
    'End Sub 'remove this to remove chat also remove button5

    Private Sub ConMenuExit_Click(sender As System.Object, e As System.EventArgs) Handles ConMenuExit.Click
        Button6_Click(ConMenuExit, EventArgs.Empty)

    End Sub



    Private Sub Button8_Click(sender As System.Object, e As System.EventArgs) Handles Button8.Click
        If ListBox2.SelectedIndex > -1 And ListBox3.SelectedIndex > -1 Then
            Try
                ics.Init()
                ics.EnableIcs(ics.Connections.Item(ListBox2.SelectedIndex).Guid, ics.Connections.Item(ListBox3.SelectedIndex).Guid)

            Catch ex As Exception
                NotifyIcon1.BalloonTipTitle = "Error"
                NotifyIcon1.BalloonTipText = ex.Message.ToString
                NotifyIcon1.ShowBalloonTip(4000)
            End Try
        Else
            MsgBox("Please select the Interface over which to share Internet connection.", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Interface Selected")
        End If


    End Sub

    Private Sub Button10_Click(sender As System.Object, e As System.EventArgs) Handles Button10.Click
        ics.DisableIcsOnAll()
    End Sub

    Private Sub NotifyIcon1_MouseMove(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles NotifyIcon1.MouseMove

    End Sub
End Class
