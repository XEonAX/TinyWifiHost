'This Code is by uno team (http://www.freewebs.com/unoteam/)
'and is released under the terms of the GNU LGPL
'(wich means you can use this shit for every use including commercial or personal use, 
'for free without any guaranty of any kind).
'--------------------------------------------------------------------------------------
Option Strict On
Imports System
Imports System.Net

Public Module NetInterop
    Private LocalIP As String = ""
    Private NetPresent As Boolean = False
    'Get local ip.....
    Public Function GetLocalIP() As String
        If NetPresent Then Return LocalIP
        TimedOutLocalIP()
        If NetPresent Then Return LocalIP
        Return "127.0.0.1"
    End Function
    'Timout for local ip thread sub
    Private Sub TimedOutLocalIP()
        Dim t As New Threading.Thread(AddressOf LocalIPThreadSub)
        t.Priority = Threading.ThreadPriority.Highest
        t.Start()
        System.Threading.Thread.CurrentThread.Sleep(1200)
        t.Abort()
    End Sub
    'Local ip thread sub
    Private Sub LocalIPThreadSub()
        Try
            Dim Dns As System.Net.Dns
            LocalIP = Dns.Resolve(System.Net.Dns.GetHostName).AddressList(0).ToString()
            NetPresent = True
        Catch ex As Exception
        End Try
    End Sub
#Region "Bytes to num conversion"
    'This converts a sequence of bytes to a positive int number
    Public Function GetLength(ByVal Bytes() As Byte) As Double
        If Not Bytes Is Nothing Then
            Dim ByteNum As Integer = Bytes.Length
            Dim i As Integer
            Dim v As Double = 0
            Dim m As Math
            For i = 0 To ByteNum - 1
                Dim d As Double = m.Pow(256, (ByteNum - 1) - i)
                Dim tv As Double = Bytes(i) * d
                v += tv
            Next
            Return v
        Else
            Throw New ArgumentException("The Bytes array cannot be null")
        End If
    End Function
    'This returns the maximum positive int value representable with n bytes
    Public Function GetMaxVal(ByVal ByteNum As Integer) As Double
        Try
            Dim m As Math
            Dim d As Double = m.Pow(256, ByteNum)
            d -= 1
            Return d
        Catch ex As Exception
            Throw New Exception("The byte number is too large for representing double values")
        End Try
    End Function
    'This converts a positive int number in a sequence of bytes of lenght ByteNum
    Public Function GetBytes(ByVal Val As Double, ByVal ByteNum As Integer) As Byte()
        Dim m As Math
        Dim chkv As Double = m.Floor(Val)
        If Val <= GetMaxVal(ByteNum) And Val = chkv Then
            Dim B(ByteNum - 1) As Byte
            Dim v As Double = Val
            Dim i As Integer
            For i = 0 To ByteNum - 1
                Dim d As Double = m.Pow(256, (ByteNum - 1) - i)
                Dim dd As Double = Double.Parse(d.ToString)
                Dim bl As Double = v / dd
                bl = m.Floor(bl)
                B(i) = Byte.Parse(bl.ToString)
                v -= B(i) * dd
            Next
            Return B
        Else
            Throw New ArgumentException("Value " + Val.ToString + " cannot be represented using " + ByteNum.ToString + " bytes")
        End If
    End Function
#End Region
    'Send buffer lenght....
    Public Sub sendBufferLenght(ByVal Lenght As Double, ByRef cl As System.Net.Sockets.TcpClient)
        Dim bufDim(7) As Byte
        bufDim = GetBytes(Lenght, 8)
        cl.GetStream.Write(bufDim, 0, bufDim.Length)
    End Sub
    Public Sub sendBufferLenght(ByVal Lenght As Double, ByRef cl As System.Net.Sockets.Socket)
        Dim bufDim(7) As Byte
        bufDim = GetBytes(Lenght, 8)
        cl.SetSocketOption(Sockets.SocketOptionLevel.Socket, Sockets.SocketOptionName.SendTimeout, 10000)
        cl.Send(bufDim, 0, bufDim.Length, Sockets.SocketFlags.None)
    End Sub
    'Receive buffer lenght...
    Public Function receiveBufferLenght(ByRef remClient As System.Net.Sockets.TcpClient) As Double
        Dim recBuffer(7) As Byte
        remClient.ReceiveTimeout = 10000
        Try
            remClient.GetStream.Read(recBuffer, 0, recBuffer.Length)
        Catch
            Return 0
        End Try
        Return GetLength(recBuffer)
    End Function
    Public Function receiveBufferLenght(ByRef remClient As System.Net.Sockets.Socket) As Double
        Dim recBuffer(7) As Byte
        remClient.SetSocketOption(Sockets.SocketOptionLevel.Socket, Sockets.SocketOptionName.ReceiveTimeout, 10000)
        Try
            remClient.Receive(recBuffer, 0, recBuffer.Length, Sockets.SocketFlags.None)
        Catch
            Return 0
        End Try
        Return GetLength(recBuffer)
    End Function
End Module