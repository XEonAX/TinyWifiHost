'This Code is by uno team (http://www.freewebs.com/unoteam/)
'and is released under the terms of the GNU LGPL
'(wich means you can use this shit for every use including commercial or personal use, 
'for free without any guaranty of any kind).
'--------------------------------------------------------------------------------------


'Well....the scanner is based on MOP (MultiThreadedTimedOutOperation)....so plz refer to 
'http://www.codeproject.com/useritems/Multithreaded_timedout_op.asp

'No time for other comment, sorry :P
'However is simple...no need to comment!
Imports System.Net

Public Class ServerScanner
    Public Event IPfound(ByVal IP As String)
    Public Event PerformPBStep()
    Public Event ScanFinished()
    Private SubnetToScanIP As String = "127.0.0.1"
    Private TCPPort As Integer = 6666
    Private useProgressBarEvent As Boolean = True
    Private progressbarsteps As Integer = 1
    Private maxThread As Integer
    Private WaitTime As Integer
    Private WithEvents checkPortMOP As MultiThreadedTimedOutOperation
    Public Sub New(ByVal SubToScan As String, ByVal port As Integer, Optional ByVal numThreads As Integer = 25, Optional ByVal TimeOut As Integer = 8000, Optional ByVal usePBEvent As Boolean = True, Optional ByVal pbStep As Integer = 1)
        TCPPort = port
        useProgressBarEvent = usePBEvent
        progressbarsteps = pbStep
        maxThread = numThreads
        WaitTime = TimeOut
        Try
            Dim S As String = SubToScan
            Dim b() As String = S.Split(".")
            Dim i As Integer
            Dim bb(2) As Byte
            For i = 0 To 2
                bb(i) = Byte.Parse(b(i))
            Next
            SubnetToScanIP = bb(0).ToString + "." + bb(1).ToString + "." + bb(2).ToString + "."
        Catch ex As Exception
            MsgBox("Parameters are incorrect")
        End Try
    End Sub
    Public Sub StartScan()
        checkPortMOP = New MultiThreadedTimedOutOperation(AddressOf checkPort, maxThread, WaitTime)
        Dim I As Integer
        For I = 1 To 254
            checkPortMOP.QueueJob(SubnetToScanIP + I.ToString)
        Next
        checkPortMOP.StartOperation()
    End Sub
    Private Sub onCheckPortFinish() Handles checkPortMOP.OpFinished
        Dim S As Stack = checkPortMOP.GetResults()
        Dim I As Integer
        For I = 1 To S.Count
            Dim Result As String = S.Pop
            If Not Result.Equals("nothing") Then
                RaiseEvent IPfound(Result)
            End If
        Next
        RaiseEvent ScanFinished()
    End Sub
    'Sub for the progress bar
    Private Sub OnPBStep() Handles checkPortMOP.PerformStep
        RaiseEvent PerformPBStep()
    End Sub
    'Check the port...easy no? (Maybe too much...shit)
    Private Function checkPort(ByVal ip As Object) As Object
        On Error GoTo endd
        Dim iptemp As String = "nothing"
        Dim client As New System.Net.Sockets.TcpClient
        client.Connect(ip, TCPPort)
        iptemp = ip
        client.Close()
endd:
        Return iptemp
    End Function
End Class

' For this shit see http://www.codeproject.com/useritems/Multithreaded_timedout_op.asp :P
Public Class MultiThreadedTimedOutOperation
    Public Delegate Function Job(ByVal InitState As Object) As Object
    Public Event PerformStep()
    Public Event OpFinished()
    Private JobStart As Job
    Private Results As New Stack
    Private TS As New Queue
    Private ActiveThreads As Integer
    Private ReadOnly MaxThreads As Integer = 40
    Private ReadOnly Waittime As Integer = 3000
    Public Sub New(ByVal Method As Job, Optional ByVal maxthreads As Integer = 40, Optional ByVal Timeout As Integer = 3000)
        Me.MaxThreads = maxthreads
        Me.Waittime = Timeout
        JobStart = Method
    End Sub
    Public Sub QueueJob(ByVal UserJobStartState As Object)
        TS.Enqueue(UserJobStartState)
    End Sub
    Public Sub StartOperation()
        Dim t As New Threading.Thread(AddressOf MainJob)
        t.ApartmentState = Threading.ApartmentState.MTA
        t.Start()
    End Sub
    Public Function GetResults() As Stack
        Return Results
    End Function
    Private Sub MainJob()
        While TS.Count > 0
            If ActiveThreads < MaxThreads Then
                Do1Job()
                RaiseEvent PerformStep()
            End If
            System.Threading.Thread.Sleep(25)
        End While
        RaiseEvent OpFinished()
    End Sub
    Private Sub Do1Job()
        Dim t As New Threading.Thread(AddressOf TimingOutJob)
        t.Start()
    End Sub
    Private Sub TimingOutJob()
        ActiveThreads += 1
        Dim t As New Threading.Thread(AddressOf JobInvoke)
        t.Priority = Threading.ThreadPriority.Highest
        t.Start()
        If Not t.Join(Waittime) Then
            If t.ThreadState = Threading.ThreadState.Suspended Then
                t.Resume()
            End If
            Try
                t.Abort()
            Catch
            End Try
        End If
        ActiveThreads -= 1
    End Sub
    Private Sub JobInvoke()
        Try
            Dim ThisJob As String = TS.Dequeue
            Dim Result As Object = JobStart.Invoke(ThisJob)
            If Not Result Is Nothing Then Results.Push(Result)
        Catch

        End Try
    End Sub
End Class
