Imports System.Net
Imports Microsoft.VisualBasic.Devices
Public Class Form1
    Friend VirtualRouter
    Dim WithEvents vr As New VirtualRouter.Wlan.WlanManager
    Dim ct As Integer
    Dim noconn As Integer
    Dim arr() As String = {"0"}
    Dim vbdevaud As New Microsoft.VisualBasic.Devices.Audio
    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs)
        vr.QueryConnectionSettings(TextBox1.Text, vbNull)
        vr.QuerySecondaryKey(TextBox2.Text, vbNull, vbNull)
        ct = 0
        noconn = 0
    End Sub
    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        If Button1.Text = "Start" Then

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
        Else
            Timer2.Enabled = True
            vr.StopHostedNetwork()
            vr.QuerySecondaryKey(TextBox2.Text, vbNull, vbNull)
            Button1.Text = "Start"
        End If
    End Sub
    Dim tempkey As String
    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        If vr.IsHostedNetworkStarted = True Then
            If vr.Stations.Count > noconn Then
                vbdevaud.Play(Application.StartupPath + "\connect.wav")
                noconn = vr.Stations.Count
                ' vr.Stations.Keys.CopyTo(arr, 0)
                For Each keyy In vr.Stations.Keys

                    Debug.Print(keyy.ToString)
                    Debug.Print(noconn.ToString + ":::::" + vr.Stations.Count.ToString)
                    If tempkey = keyy Then GoTo exitfor
                    tempkey = keyy
                Next
exitfor:
            ElseIf vr.Stations.Count < noconn Then
                vbdevaud.Play(Application.StartupPath + "\disconnect.wav")
                noconn = vr.Stations.Count
            End If
            Label3.Text = "No of Connected Clients:" + vr.Stations.Count.ToString
        Else
            Label3.Text = "No of Connected Clients:" + 0
            noconn = 0
        End If
        If vr.IsHostedNetworkStarted = False Then
            Button1.Text = "Start"
            noconn = 0
        ElseIf vr.IsHostedNetworkStarted = True Then
            Button1.Text = "Stop"
        ElseIf vr.HostedNetworkState = Global.VirtualRouter.Wlan.WinAPI.WLAN_HOSTED_NETWORK_STATE.wlan_hosted_network_unavailable Then
            Button1.Text = "Stop"
        End If
    End Sub
   
    Private Sub Timer2_Tick(sender As System.Object, e As System.EventArgs) Handles Timer2.Tick
        ct += 1
        vr.QuerySecondaryKey(TextBox2.Text, vbNull, vbNull)
        If ct > 10 Then
            Timer2.Enabled = False
            ct = 0
        End If
        TextBox4.Text = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName).AddressList(1).ToString()
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
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

    Private Sub Button2_MouseHover(sender As Object, e As System.EventArgs) Handles Button2.MouseHover
        ToolTip1.SetToolTip(Button2, "Settings for Host IP.")
    End Sub

    Private Sub Button5_MouseHover(sender As Object, e As System.EventArgs) Handles Button5.MouseHover
        ToolTip1.SetToolTip(Button5, "Start IP Chat")

    End Sub

    

    Private Sub Button1_MouseHover(sender As Object, e As System.EventArgs) Handles Button1.MouseHover
        ToolTip1.SetToolTip(Button1, "Start/Stop the HostedNetwork")

    End Sub

    Private Sub Button6_Click(sender As System.Object, e As System.EventArgs) Handles Button6.Click
        DemoApp.Close()
        Me.Close()
        End

    End Sub

    Private Sub Button7_Click(sender As System.Object, e As System.EventArgs) Handles Button7.Click
        MsgBox("This Application is created by Sumant Vanage." + vbCrLf + "Special Thanks to Chris Pietschmann and Unoteam", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "AbouT")
    End Sub

    Private Sub TextBox3_MouseHover(sender As Object, e As System.EventArgs) Handles TextBox3.MouseHover
        ToolTip1.SetToolTip(TextBox3, "Name of the Interface which connects using Microsoft Virtual WiFi Miniport Adapter." + vbCrLf + "Or Name of the Adapter whose IP you want to Change.")
    End Sub

    Private Sub TextBox4_MouseHover(sender As Object, e As System.EventArgs) Handles TextBox4.MouseHover
        ToolTip1.SetToolTip(TextBox4, "IP address to be set to the above interface." + vbCrLf + "Preferably ending with "".1"" If you are hosting the network." + vbCrLf + "And not ending with "".1"" If you are not hosting the network.")

    End Sub

    Private Sub Button3_MouseHover(sender As Object, e As System.EventArgs) Handles Button3.MouseHover
        ToolTip1.SetToolTip(Button3, "If this does not work. Try running as Admin.")

    End Sub

    
    Private Sub ToolTip1_Popup(sender As System.Object, e As System.Windows.Forms.PopupEventArgs) Handles ToolTip1.Popup

    End Sub

    Private Sub Button8_Click(sender As System.Object, e As System.EventArgs) Handles Button8.Click
        Me.Hide()
        NotifyIcon1.Visible = True

    End Sub

    Private Sub NotifyIcon1_MouseClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles NotifyIcon1.MouseClick
        Me.Show()
        NotifyIcon1.Visible = False

    End Sub

    Private Sub NotifyIcon1_MouseDoubleClick(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles NotifyIcon1.MouseDoubleClick
       
    End Sub

  

    
End Class
