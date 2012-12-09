Imports System.Net
Imports Microsoft.VisualBasic.Devices
Public Class Form1
    Friend VirtualRouter
    Dim WithEvents vr As New VirtualRouter.Wlan.WlanManager
    Dim listofcclients As String
    Dim ct As Integer = 0
    Dim noconn As Integer
    'Dim arr() As String = {"0"}
    Dim vbdevaud As New Microsoft.VisualBasic.Devices.Audio

    
    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs)
        
    End Sub
    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        If Button1.Text = "Start" Then
            Debug.Print(My.Application.CommandLineArgs.Count)
            If TextBox2.TextLength < 8 Then
                MsgBox("Password length should be >= 8")
                TextBox2.Text = "password"
            End If
            vr.SetConnectionSettings(TextBox1.Text, 10)
            vr.SetSecondaryKey(TextBox2.Text)
            vr.StartHostedNetwork()
            Button1.Text = "Stop"
            vr.QuerySecondaryKey(TextBox2.Text, vbNull, vbNull)
            Timer2.Enabled = True
            ToolTip1.SetToolTip(Button1, "Stop the HostedNetwork")
        Else
            Timer2.Enabled = True
            vr.StopHostedNetwork()
            vr.QuerySecondaryKey(TextBox2.Text, vbNull, vbNull)
            Button1.Text = "Start"
            ToolTip1.SetToolTip(Button1, "Start the HostedNetwork")
        End If
    End Sub
    Dim tempkey As String
    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        If vr.IsHostedNetworkStarted = True Then
            If vr.Stations.Count > noconn Then
                vbdevaud.Play(Application.StartupPath + "\connect.wav")
                noconn = vr.Stations.Count
                ListBox1.Items.Clear()

                ' vr.Stations.Keys.CopyTo(arr, 0)
                listofcclients = ""
                For Each keyy In vr.Stations.Keys
                    ListBox1.Items.Add(keyy)
                    listofcclients += keyy + vbCrLf
                Next
                'exitfor:
                NotifyIcon1.ShowBalloonTip(200, "New Client Connected", "Clients Currently Connected:" + vbCrLf + listofcclients, ToolTipIcon.Info)
            ElseIf vr.Stations.Count < noconn Then
                vbdevaud.Play(Application.StartupPath + "\disconnect.wav")
                noconn = vr.Stations.Count
                ListBox1.Items.Clear()
                listofcclients = ""
                For Each keyy In vr.Stations.Keys
                    ListBox1.Items.Add(keyy)
                    listofcclients += keyy + vbCrLf
                Next
                NotifyIcon1.ShowBalloonTip(200, "Client Disconnected", "Clients Currently Connected:" + vbCrLf + listofcclients, ToolTipIcon.Info)

            End If
            Label3.Text = "No of Connected Clients:" + vr.Stations.Count.ToString
        Else
            Label3.Text = "No of Connected Clients:0"
            noconn = 0
        End If
        If vr.IsHostedNetworkStarted = False Then
            Button1.Text = "Start"
            noconn = 0

            ListBox1.Items.Clear()
            vr.Stations.Clear()
        ElseIf vr.IsHostedNetworkStarted = True Then
            Button1.Text = "Stop"

        ElseIf vr.HostedNetworkState = Global.VirtualRouter.Wlan.WinAPI.WLAN_HOSTED_NETWORK_STATE.wlan_hosted_network_unavailable Then
            Button1.Text = "Stop"
            ToolTip1.SetToolTip(Button1, "Stop the HostedNetwork")
        End If
    End Sub
   
    Private Sub Timer2_Tick(sender As System.Object, e As System.EventArgs) Handles Timer2.Tick
        ct += 1
        Debug.Print(ct.ToString)

        If ct < 8 Then
            vr.QuerySecondaryKey(TextBox2.Text, vbNull, vbNull)
        End If

        If ct > 8 Then
            TextBox1.Text = ""
            TextBox2.Text = ""
            listofcclients = ""
            Debug.Print(My.Application.CommandLineArgs.Count)
            If My.Application.CommandLineArgs.Count > 0 Then
                For Each cmd In My.Application.CommandLineArgs
                    Debug.Print(cmd)
                    If cmd.StartsWith("ssid=") Then TextBox1.Text = cmd.Substring(5)
                    If cmd.StartsWith("pass=") Then TextBox2.Text = cmd.Substring(5)
                Next
            End If
            If TextBox1.Text = "" Then vr.QueryConnectionSettings(TextBox1.Text, vbNull)
            If TextBox2.Text = "" Then vr.QuerySecondaryKey(TextBox2.Text, vbNull, vbNull)


            
            noconn = 0
            Me.WindowState = FormWindowState.Normal
        End If
        If ct > 10 Then

            If My.Application.CommandLineArgs.Contains("autostart") Then
                vr.StopHostedNetwork()
                vr.ForceStop()
                If TextBox2.Text.Length < 8 Then
                    NotifyIcon1.ShowBalloonTip(1000, "Password Error", "Password smaller than 8 characters" + vbCrLf + "Password changed to ""qassword""", ToolTipIcon.Warning)
                    TextBox2.Text = "qassword"
                End If
                Button1_Click(Me, EventArgs.Empty)
                Me.Hide()
            End If
            If TextBox1.Text = "" Then TextBox1.Text = "SSID"
            If TextBox2.Text = "" Then TextBox2.Text = "qassword"
            Timer2.Enabled = False
            ct = 100
        End If
        TextBox4.Text = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName).AddressList(1).ToString()
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        Dim i As Integer
        If Button2.Text = ">>" Then
            For i = 1 To 200
                Me.Width += 1
            Next
            Button2.Text = "<<"
        Else
            For i = 1 To 200
                Me.Width -= 1
            Next
            Button2.Text = ">>"
        End If
    End Sub


    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        Try
            Shell("netsh interface ip set address """ + TextBox3.Text + """ static " + TextBox4.Text + " 255.255.255.0 " + TextBox4.Text + " 2", AppWinStyle.MaximizedFocus)
        Catch ex As Exception
            MsgBox(ex.ToString + vbCrLf + "Try Running as admin.", MsgBoxStyle.Information) '"Try Running as admin.", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly)
        End Try
        Timer2.Enabled = True
    End Sub
    Dim i As Integer
    Private Sub Button4_Click(sender As System.Object, e As System.EventArgs) Handles Button4.Click
        If i < System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName).AddressList.Length Then
            TextBox4.Text = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName).AddressList(i).ToString()
            i += 1
        Else
            i = 0
        End If
    End Sub

    Private Sub Button5_Click(sender As System.Object, e As System.EventArgs) Handles Button5.Click
        DemoApp.Show()
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub Button6_Click(sender As System.Object, e As System.EventArgs) Handles Button6.Click
        DemoApp.Close()
        Me.Close()
        Try
            vr.StopHostedNetwork()
        Catch eex As Exception
        End Try
        End
    End Sub

    Private Sub Button7_Click(sender As System.Object, e As System.EventArgs) Handles Button7.Click
        MsgBox("This Application is created by Sumant Vanage." + vbCrLf + "Special Thanks to Chris Pietschmann and Unoteam", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "AbouT")
    End Sub

    Private Sub Button8_Click(sender As System.Object, e As System.EventArgs) Handles Button8.Click
        Me.Hide()
    End Sub

    Private Sub NotifyIcon1_BalloonTipClicked(sender As Object, e As System.EventArgs) Handles NotifyIcon1.BalloonTipClicked
        Me.Show()
    End Sub

    Private Sub NotifyIcon1_DoubleClick(sender As Object, e As System.EventArgs) Handles NotifyIcon1.DoubleClick
        Me.Show()
    End Sub

    Private Sub NotifyIcon1_Click(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles NotifyIcon1.Click
        NotifyIcon1.ShowBalloonTip(200, "Clients Connected:", listofcclients + " ", ToolTipIcon.Info)

    End Sub

    

    Private Sub Button9_Click(sender As System.Object, e As System.EventArgs) Handles Button9.Click
        Dim i As Integer
        If Button9.Text = "▼" Then
            For i = 1 To 200
                Me.Height += 1
            Next
            Button9.Text = "▲"
        Else
            For i = 1 To 200
                Me.Height -= 1
            Next
            Button9.Text = "▼"
        End If
    End Sub
End Class
