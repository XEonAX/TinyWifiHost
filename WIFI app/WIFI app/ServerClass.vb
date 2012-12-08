'This Code is by uno team (http://www.freewebs.com/unoteam/)
'and is released under the terms of the GNU LGPL
'(wich means you can use this shit for every use including commercial or personal use, 
'for free without any guaranty of any kind).
'--------------------------------------------------------------------------------------
Imports System
Imports System.Net
Public Class ServerClass

    Public Structure InMessEvArgs
        Dim message As String
        Dim senderIP As String
        Public Sub New(ByVal Message As String, ByVal SenderIP As String)
            Me.message = Message
            Me.senderIP = SenderIP
        End Sub
    End Structure
    Public Event IncomingMessage(ByVal eventargs As InMessEvArgs)
    Public Event DiagnosticMessage(ByVal Message As String)
    Private Const PacketSize As Integer = 4096

    Private Listener As Sockets.TcpListener
    Private WithEvents Timer As New Timers.Timer(100)
    Private fileListener As Sockets.TcpListener

    Public ReadOnly Property IsRunning() As Boolean
        Get
            Return started
        End Get
    End Property
    Private started As Boolean = False
    Public Property IncomingPath() As String
        Get
            Return DirectoryPath
        End Get
        Set(ByVal Value As String)
            Dim F As New IO.DirectoryInfo(Value)
            DirectoryPath = Value
        End Set
    End Property
    Private DirectoryPath As String = "C:\"
    Public ReadOnly Property LocalIP() As String
        Get
            Return LIP
        End Get
    End Property
    Public Shared LIP As String = ""

    Public Sub New(ByVal port As Integer, ByVal autostart As Boolean, Optional ByVal path As String = "C:\")
        InitServer(port)
        DirectoryPath = path
        If autostart Then StartServer()
    End Sub
    Private Sub InitServer(ByVal Port As Integer)

        Listener = New Sockets.TcpListener(IPAddress.Parse(DemoApp.TextBox1.Text), Port)
    End Sub

    Public Sub StartServer()
        If Not started Then
            Listener.Start()
            Timer.Start()
            Timer.Enabled = True
            started = True
        End If
    End Sub
    Public Sub StopServer()
        If started Then
            Timer.Enabled = False
            Timer.Stop()
            Listener.Stop()
            started = False
        End If
    End Sub

    'This one is called every Timer thik and checks if there are connections pending, if so a thread is started to process the request.
    Private Sub AcceptClients(ByVal o As Object, ByVal e As System.Timers.ElapsedEventArgs) Handles Timer.Elapsed
        If Listener.Pending Then
            Timer.Enabled = False
            Dim DoConnect As New Threading.Thread(AddressOf OnConnectTS)
            DoConnect.Start()
            Timer.Enabled = True
        End If
    End Sub
    'On client connect, first check if the message is a command, else raise the message
    Private Sub OnConnectTS()
        Dim Buffer() As Byte
        Dim Client As System.Net.Sockets.Socket
        Dim messagechars() As Char
        Dim message As String
        Try
            Client = Listener.AcceptSocket
            Dim sizeBuf As Double = receiveBufferLenght(Client)
            ReDim Buffer(sizeBuf)
            Client.Receive(Buffer, Buffer.Length, Sockets.SocketFlags.None)
            messagechars = System.Text.Encoding.Default.GetChars(Buffer)
            message = New String(messagechars, 0, sizeBuf)
        Catch ex As Exception
            RaiseEvent DiagnosticMessage("Connection error: BadResponse or Handshake from Wrong application")
            Client.Close()
            End Try
        If Not message Is Nothing Then
            If EvaluateCommand(message) Then
                Answer(message, Client)
            Else

                Dim REP As System.Net.IPEndPoint = Client.RemoteEndPoint
                Client.Close()
                Dim args As New InMessEvArgs(message, REP.Address.ToString())
                RaiseEvent IncomingMessage(args)
            End If
        Else
            RaiseEvent DiagnosticMessage("Blank message")
        End If
    End Sub

    'Determinate if a message is a command of our type (don't care of our usage of command, u can use more simple)
    Private Function EvaluateCommand(ByRef message As String) As Boolean
        If Not message.StartsWith("**!**") Then Return False
        Dim Check() As String = message.Split("-")
        Try
            Dim b As Byte = Byte.Parse(Check(1))
            b = Byte.Parse(Check(2))
            b = Byte.Parse(Check(3))
            If Not Check(4).Length = 2 Then Return False
            If Check(5).Length < 2 Then Return False
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    'For now there is only a command, the file transfer command :P, and the sub sends the port for incoming file to the client
    Private Sub Answer(ByVal code As String, ByRef TCPCl As System.Net.Sockets.Socket)
        Try
            Dim reply() As Byte
            Select Case code
                Case "**!**-0-2-0-SC-FT"
                    ReDim Preserve reply(7)
                    fileListener = New Sockets.TcpListener(IPAddress.Parse(LIP), 0)
                    fileListener.Start()
                    Dim ep As IPEndPoint = fileListener.LocalEndpoint
                    reply = GetBytes(ep.Port, 8)
                    Dim SaveFile As New Threading.Thread(AddressOf SavefileTS)
                    SaveFile.Start()
                Case Else
            End Select

            TCPCl.Send(reply, reply.Length, Sockets.SocketFlags.None)
            TCPCl.Close()
        Catch ex As Exception
            RaiseEvent DiagnosticMessage("Could not process request")
        End Try
    End Sub
    'Save file trhead sub
    Private Sub SavefileTS()
        RaiseEvent DiagnosticMessage("File transfer session initiated")
        Dim i As Integer = 0
        Dim fl As Sockets.TcpListener = fileListener
        fileListener = Nothing
        Try
            While Not fl.Pending
                i += 1
                Threading.Thread.CurrentThread.Sleep(100)
                If i = 100 Then Throw New Exception
            End While
            While fl.Pending
                Saveonefile(fl)
                Threading.Thread.CurrentThread.Sleep(2000)
            End While
            RaiseEvent DiagnosticMessage("File transfer session terminated OK")
        Catch ex As Exception
            RaiseEvent DiagnosticMessage("File transfer session terminated unepectedly")
        End Try
        fl.Stop()
    End Sub
    'receive first the filenme and then the file
    Private Sub Saveonefile(ByRef fl As Sockets.TcpListener)
        Dim Client As Sockets.Socket
        Dim name As String
        Dim buffer(PacketSize - 1) As Byte
        Dim lingerOption As New Sockets.LingerOption(True, 10)
        Dim wd As Double = 0
        Try
            Client = fl.AcceptSocket
            Client.SetSocketOption(Sockets.SocketOptionLevel.Socket, Sockets.SocketOptionName.Linger, lingerOption)
            Dim dName As Integer = receiveBufferLenght(Client)
            Dim nameBuff(dName - 1) As Byte
            Client.Receive(nameBuff, 0, nameBuff.Length, Sockets.SocketFlags.None)
            name = Text.Encoding.Default.GetString(nameBuff)

            RaiseEvent DiagnosticMessage("Incoming file..." + name)
            Dim incomingfile As System.IO.Stream
            incomingfile = IO.File.Open(DirectoryPath + name, IO.FileMode.OpenOrCreate)
            wd = Client.Receive(buffer, PacketSize, Sockets.SocketFlags.None)
            While wd > 0
                incomingfile.Write(buffer, 0, wd)
                wd = Client.Receive(buffer, PacketSize, Sockets.SocketFlags.None)
            End While
            incomingfile.Close()
            incomingfile = Nothing
            RaiseEvent DiagnosticMessage("File complete!" + DirectoryPath + name)
        Catch ex As Exception

        End Try
        If Not Client Is Nothing Then
            Client.Close()
            Client = Nothing
        End If
    End Sub
End Class