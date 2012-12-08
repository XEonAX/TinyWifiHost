'This Code is by uno team (http://www.freewebs.com/unoteam/)
'and is released under the terms of the GNU LGPL
'(wich means you can use this shit for every use including commercial or personal use, 
'for free without any guaranty of any kind).
'--------------------------------------------------------------------------------------
Imports System.Net
Public Class ClientClass
    Const PacketSize As Long = 4096
    Public Event DiagnosticMessage(ByVal Message As String)
    Private CurrFileAddr As System.Net.IPAddress
    Private FilePort As Integer
    Private FileQueque As New Stack
    Private isSendingFile As Boolean = False


    'Send Message to specific Ip and Port (first send buffer lenght and then the buffer holding message)
    Public Sub SendMessage(ByVal Ip As String, ByVal Port As Integer, ByVal Message As String)
        Dim tempclient As System.Net.Sockets.TcpClient
        Dim Buffer() As Byte = System.Text.Encoding.Default.GetBytes(Message.ToCharArray)
        If Not checkip(Ip) Then RaiseEvent DiagnosticMessage("Ip is not in the right format")
        Try
            Debug.Print("4444")
            tempclient = New System.Net.Sockets.TcpClient
            tempclient.Connect(Ip, Port)
            sendBufferLenght(Buffer.Length, tempclient)
            tempclient.GetStream.Write(Buffer, 0, Buffer.Length)
            tempclient.Close()
        Catch
            tempclient.Close()
            RaiseEvent DiagnosticMessage("Remote address not reachable")
        End Try
    End Sub
    'Send one or more file to specific Ip and Port
    Public Sub SendFiles(ByVal Ip As String, ByVal Port As Integer, ByVal filenames() As String)
        Dim CODE As String = "**!**-0-2-0-SC-FT" 'Command to send to sevrer for receving files
        Dim i As Integer
        ' if client is already sending a file, the new file will be enqueued
        If isSendingFile = True Then
            If CurrFileAddr.ToString = Ip Then
                For i = 0 To filenames.Length - 1
                    FileQueque.Push(filenames(i))
                Next
            End If
        Else
            'Obtain from the server port for incoming files
            FilePort = RequestPort(Ip, Port, CODE)
            If FilePort = 0 Then RaiseEvent DiagnosticMessage("Error retriving port from server")
            For i = 0 To filenames.Length - 1
                FileQueque.Push(filenames(i))
            Next
            CurrFileAddr = System.Net.Dns.Resolve(Ip).AddressList(0)
            Dim FileTransferThread As New System.Threading.Thread(AddressOf SendFilesTS)
            FileTransferThread.Start()
        End If
    End Sub
    'Send file thread, for each file send first the name and then the file
    Private Sub SendFilesTS()
        isSendingFile = True
        Dim FullFileName As String = FileQueque.Pop
        Dim tstrings As String() = FullFileName.Split("\")
        Dim FileName As String = tstrings(tstrings.Length - 1)
        Dim wd As Integer = 0
        Dim fileStream As System.IO.Stream
        Dim client As New System.Net.Sockets.Socket(Sockets.AddressFamily.InterNetwork, Sockets.SocketType.Stream, Sockets.ProtocolType.Tcp)
        Dim rep As New System.Net.IPEndPoint(CurrFileAddr, FilePort)
        Try
            client.Connect(rep)
            fileStream = IO.File.Open(FullFileName, IO.FileMode.Open, IO.FileAccess.Read)
        Catch SEx As Sockets.SocketException
            RaiseEvent DiagnosticMessage("Connection fail:" + rep.ToString)
        Catch IOEx As IO.IOException
            RaiseEvent DiagnosticMessage("Unable to open file")
        End Try
        sendBufferLenght(FileName.Length, client)
        Dim nameBuff() As Byte = System.Text.Encoding.Default.GetBytes(FileName.ToCharArray)
        client.Send(nameBuff, 0, nameBuff.Length, Sockets.SocketFlags.None)
        Dim buffer(PacketSize - 1) As Byte
        wd = fileStream.Read(buffer, 0, PacketSize)
        While wd > 0
            client.Send(buffer, wd, Sockets.SocketFlags.None)
            wd = fileStream.Read(buffer, 0, PacketSize)
        End While
        fileStream.Close()
        fileStream = Nothing
        buffer = Nothing
        client.Close()
        Try
            FileQueque.Peek()
            SendFilesTS()
        Catch ex As Exception
        End Try
        isSendingFile = False
    End Sub
    'Return port from server for incoming file
    Private Function RequestPort(ByVal ip As String, ByVal port As Integer, ByVal Code As String) As Integer
        Try
            Dim tempclient As New System.Net.Sockets.TcpClient
            tempclient.SendTimeout = 10000
            tempclient.ReceiveTimeout = 10000
            tempclient.Connect(ip, port)
            'Send the code for comunicate server that we want transfer file
            Dim Buffer() As Byte = System.Text.Encoding.Default.GetBytes(Code.ToCharArray)
            sendBufferLenght(Buffer.Length, tempclient)
            tempclient.GetStream.Write(Buffer, 0, Buffer.Length)
            'the server will return the port for incoming file
            Dim filePort As Integer = receiveBufferLenght(tempclient) 'receiveBufferLenght used improperly to receive port (^_^)
            tempclient.Close()
            Return filePort
        Catch ex As Exception
            Return 0
        End Try
    End Function
    'Check ip string to be an ip address...nothing more
    Private Function checkip(ByVal ip As String) As Boolean
        Try
            IPAddress.Parse(ip)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
End Class