﻿Imports System.Net
Imports Microsoft.VisualBasic.Devices
Imports System.Reflection
Imports System.Xml
Imports System.IO
Imports System.Resources
Imports System.Data

Public Class Form1B
    Dim alertcount As Integer = 100
    Dim devices As New Hashtable
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
    Dim table As New DataTable


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
                Timer2.Enabled = False
                vr.StopHostedNetwork()
                vr.ForceStop()
                vr.QuerySecondaryKey(TextBox2.Text, vbNull, vbNull)
                Hosted(False)
                Timer1_Tick(Button1, System.EventArgs.Empty)
                Timer1.Enabled = False

            End If
        Catch ex As Exception
            NotifyIcon1.BalloonTipTitle = "Error"
            Debug.Print("1341")
            NotifyIcon1.BalloonTipText = ex.Message.ToString & " " & vbCrLf & "Try running this command in an elevated command prompt." & vbCrLf & "'netsh wlan set hostednetwork mode=allow'"
            NotifyIcon1.ShowBalloonTip(5000)
        End Try
    End Sub

    Public Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick

        If vr.IsHostedNetworkStarted = True Then
            Hosted(True) 'Show hosted network started
            If vr.Stations.Count > noconn Then   'new client connected
                Try
                    vbdevaud.Play(Application.StartupPath + "\connect.wav")
                Catch ex As Exception
                End Try
                noconn = vr.Stations.Count
                ListBox1.Items.Clear()
                listofcclients = ""
                For Each station In vr.Stations
                    If IsNothing(devices.Item(station.Key)) Then
                        devices.Add(station.Key, "Unnamed")
                        SaveDevices()
                        ListBox1.Items.Add(station.Key & " - " & devices.Item(station.Key).ToString)
                        listofcclients += vbCrLf + station.Key
                    Else
                        ListBox1.Items.Add(station.Key & " - " & devices.Item(station.Key).ToString)
                        listofcclients += vbCrLf + devices.Item(station.Key).ToString
                    End If
                Next
                NotifyIcon1.ShowBalloonTip(200, "New Client Connected", "Clients Currently Connected:" + Trim(listofcclients & " "), ToolTipIcon.Info)

            ElseIf vr.Stations.Count < noconn Then
                Try
                    vbdevaud.Play(Application.StartupPath + "\disconnect.wav")
                Catch exx As Exception
                End Try
                noconn = vr.Stations.Count
                ListBox1.Items.Clear()
                listofcclients = ""
                For Each station In vr.Stations
                    If IsNothing(devices.Item(station.Key)) Then
                        devices.Add(station.Key, "Unnamed")
                        SaveDevices()
                        ListBox1.Items.Add(station.Key & " - " & devices.Item(station.Key).ToString)
                        listofcclients += vbCrLf + devices.Item(station.Key).ToString
                    Else
                        ListBox1.Items.Add(station.Key & " - " & devices.Item(station.Key).ToString)
                        listofcclients += vbCrLf + devices.Item(station.Key).ToString
                    End If
                Next
                NotifyIcon1.ShowBalloonTip(200, "Client Disconnected", "Clients Currently Connected:" + Trim(listofcclients & " "), ToolTipIcon.Info)
            End If
            If vr.Stations.Count > alertcount Then
                Try
                    vbdevaud.Play(Application.StartupPath + "\warn.wav")
                Catch exx As Exception
                End Try
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
                    If cmd.StartsWith("warn=") Then alertcount = cmd.Substring(5)

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
        Try
            Process.Start("http://wifihnc.codeplex.com/")
        Catch ex As Exception
            NotifyIcon1.BalloonTipTitle = "Error"
            Debug.Print("12sdfdf1")
            NotifyIcon1.BalloonTipText = ex.Message.ToString & " "
            NotifyIcon1.ShowBalloonTip(5000)
        End Try
        ' MsgBox("This Application is created by Sumant Vanage." + vbCrLf + "Special Thanks to Chris Pietschmann" + vbCrLf + "And Microsoft.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "AbouT")
    End Sub

    Private Sub NotifyIcon1_BalloonTipClicked(sender As Object, e As System.EventArgs) Handles NotifyIcon1.BalloonTipClicked
        If Me.Visible = True Then
            Me.Visible = False
            Me.WindowState = FormWindowState.Minimized
        Else
            Me.Visible = True
            Me.WindowState = FormWindowState.Normal
        End If
    End Sub

    Private Sub NotifyIcon1_Click(sender As Object, e As System.EventArgs) Handles NotifyIcon1.Click
        NotifyIcon1.ShowBalloonTip(800, "Clients Connected", listofcclients + " ", ToolTipIcon.Info)

    End Sub






    Private Sub NotifyIcon1_DoubleClick(sender As Object, e As System.EventArgs) Handles NotifyIcon1.DoubleClick
        If Me.Visible = True Then
            Me.Visible = False
            Me.WindowState = FormWindowState.Minimized
        Else
            Me.Visible = True
            Me.WindowState = FormWindowState.Normal
        End If
    End Sub
    Private Sub Button9_Click(sender As System.Object, e As System.EventArgs) Handles Button9.Click

        If Button9.Text = "Clients && ICS..." Then
            For Me.i = 1 To 24
                Me.Height += 8
                Me.Width += 12
                Me.Refresh()
            Next
            Button9.Text = "Clients && ICS."
        Else
            For Me.i = 1 To 24
                Me.Height -= 8
                Me.Width -= 12
                Me.Refresh()
            Next
            Button9.Text = "Clients && ICS..."
        End If

    End Sub

    Private Sub Form1B_Disposed(sender As Object, e As System.EventArgs) Handles Me.Disposed
        NotifyIcon1.Dispose()
    End Sub

    Private Sub Form1B_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        'LoadDevices()
        Try
            table.ReadXmlSchema("devices.xsd")
            table.ReadXml("devices.xml")
            For Each row As DataRow In table.Rows
                devices.Add(row.Item(0), row.Item(1))
            Next

            cico = NotifyIcon1.Icon
            hico = Me.Icon
            Me.Width = 278
            Me.Height = 146
            Dim IntfaceIndex As Integer = 0
            For Each Intface In ics.Connections
                ListBox2.Items.Add(Intface.Name & " - " & Intface.DeviceName)
                ListBox3.Items.Add(Intface.Name & " - " & Intface.DeviceName)
                '  If Intface.IsPublic = True Then
                '  Debug.Print(">>" + Intface.Name)
                '   ListBox2.SelectedIndex = IntfaceIndex
                '   End If
                Debug.Print(Intface.Name & " >>>> " & Intface.DeviceName)
                If Intface.IsPrivate = True Then
                    'Debug.Print("<<" + Intface.Name)
                    ListBox3.SelectedIndex = IntfaceIndex
                End If
                IntfaceIndex += 1
            Next
        Catch ex As Exception
            Debug.Print(ex.Message.ToString)
            NotifyIcon1.BalloonTipTitle = "Error"
            Debug.Print("12erheh1")
            NotifyIcon1.BalloonTipText = ex.Message.ToString & " " & vbCrLf & "Try running this program as administrator."
            NotifyIcon1.ShowBalloonTip(5000)
        End Try

    End Sub



    Private Sub Form1_Resize(sender As Object, e As System.EventArgs) Handles Me.Resize
        If Me.WindowState = FormWindowState.Normal Then
            Me.ShowInTaskbar = True
        End If
        If Me.WindowState = FormWindowState.Minimized Then
            Me.ShowInTaskbar = False
            NotifyIcon1.Visible = True

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
                Debug.Print("121")
                NotifyIcon1.BalloonTipText = ex.Message.ToString & " "
                NotifyIcon1.ShowBalloonTip(5000)
            End Try
        Else
            MsgBox("Please select the Interface over which to share Internet connection.", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Interface Selected")
        End If


    End Sub

    Private Sub Button10_Click(sender As System.Object, e As System.EventArgs) Handles Button10.Click
        ics.DisableIcsOnAll()
    End Sub













    Private Sub ListBox1_DoubleClick(sender As Object, e As System.EventArgs) Handles ListBox1.DoubleClick
        Dim devname = ""
        'Debug.Print(ListBox1.SelectedIndex)
        If ListBox1.SelectedIndex = -1 And ListBox1.Items.Count > 0 Then ListBox1.SelectedIndex = 0
        If ListBox1.SelectedIndex >= 0 Then
            devname = InputBox("Name:", "Enter Device Name", Trim(Mid(ListBox1.SelectedItem.ToString, 20)))

        End If
        If Not devname = "" Then
            devices.Item(Trim(Mid(ListBox1.SelectedItem.ToString, 1, 17))) = devname
            SaveDevices()
            ListBox1.Items.Clear()
            listofcclients = ""
            For Each station In vr.Stations

                If IsNothing(devices.Item(station.Key)) Then
                    devices.Add(station.Key, "Unnamed")
                    SaveDevices()
                    ListBox1.Items.Add(station.Key & " - " & devices.Item(station.Key).ToString)
                    listofcclients += vbCrLf + station.Key
                Else
                    ListBox1.Items.Add(station.Key & " - " & devices.Item(station.Key).ToString)
                    listofcclients += vbCrLf + devices.Item(station.Key).ToString
                End If

            Next

        End If

    End Sub



    Public Sub SaveDevices()
        table.Clear()
        For Each key In devices.Keys

            table.Rows.Add(key, devices.Item(key))
        Next

        table.AcceptChanges()
        table.WriteXml("devices.xml")
    End Sub
    Public Sub LoadDevices()
        'Checking if file exists, if no exit method
        If File.Exists("Devices.dat") = False Then Exit Sub
        'open file through stream
        Dim fs As New FileStream("Devices.dat", FileMode.Open)
        'blabla binary formater
        Dim bf As New Runtime.Serialization.Formatters.Binary.BinaryFormatter()
        'desserializing and fill the hashtable with data from it
        devices = bf.Deserialize(fs)
        'close stream
        fs.Close()
    End Sub



  
End Class
